//---------------------------------------------------------------------
// File: AssemblyExecuteAdapterAdapterWorkItem.cs
// 
// Summary: Implementation of an adapter framework sample adapter. 
//
// Sample: AssemblyExecuteAdapter Transmit Adapter, demonstrating solicit-response.
//
//
//---------------------------------------------------------------------
// This file is part of the Microsoft BizTalk Server 2006 SDK
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// This source code is intended only as a supplement to Microsoft BizTalk
// Server 2006 release and/or on-line documentation. See these other
// materials for detailed information regarding Microsoft code samples.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
// PURPOSE.
//---------------------------------------------------------------------

using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Net;
using System.Collections;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

using Microsoft.BizTalk.Streaming;
using Microsoft.BizTalk.TransportProxy.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.Samples.BizTalk.Adapter.Common;
using Microsoft.XLANGs.BaseTypes;
using BizTalk.Adapter.AssemblyExecute.Interface;


namespace BizTalk.Adapters.Runtime.AssemblyExecuteAdapterTransmitter
{
    /// <summary>
    /// There is one instance of AssemblyExecuteAdapterTransmitterEndpoint class for each every static send port.
    /// Messages will be forwarded to this class by AsyncTransmitterBatch (via AssemblyExecuteAdapterTransmitAdapterBatch)
    /// </summary>
    internal class AssemblyExecuteAdapterTransmitterEndpoint: AsyncTransmitterEndpoint
    {
        //private IBaseMessage		solicitMsg			= null;
        private IBTTransportProxy	transportProxy		= null;
        private AsyncTransmitter	asyncTransmitter	= null;
        private string propertyNamespace;

        public AssemblyExecuteAdapterTransmitterEndpoint(AsyncTransmitter asyncTransmitter) : base(asyncTransmitter)
        {
            this.asyncTransmitter = asyncTransmitter;
            this.transportProxy = asyncTransmitter.TransportProxy;
        }

        public override void Open(EndpointParameters endpointParameters, IPropertyBag handlerPropertyBag, string propertyNamespace)
        {
            this.propertyNamespace = propertyNamespace;
        }


        /// <summary>
        /// Implementation for AsyncTransmitterEndpoint::ProcessMessage
        /// Transmit the message and optionally return the response message
        /// </summary>
        public override IBaseMessage ProcessMessage(IBaseMessage message)
        {
            //this.solicitMsg = message;

            AssemblyExecuteAdapterTransmitProperties props = new AssemblyExecuteAdapterTransmitProperties(message, this.propertyNamespace);
            IBaseMessage responseMsg = null;
            
            if ( props.IsTwoWay )
            {
                Stream response = SendAssemblyExecuteAdapterRequest(message, props);
				responseMsg = BuildResponseMessage(response, message.Context);
            }
            else
            {
				Stream response = SendAssemblyExecuteAdapterRequest(message, props);
				if (response != null)
				{
					response.Close();
				}
            }

            return responseMsg;
        }

        private IBaseMessage BuildResponseMessage(Stream response,IBaseMessageContext context)
        {
            IBaseMessage btsResponse = null;

            // Get the response stream, create a new message attaching
            // the response stream...
			using (Stream s = response)
            {
                // NOTE: 
                // Copy the network stream into a virtual stream stream. If we were
                // to use a forward only stream (as in the response stream) we would 
                // not be able to suspend the response data on failure. The virtual 
                // stream will overflow to disc when it reaches a given threshold
                VirtualStream vs = new VirtualStream();
                int bytesRead = 0;
                byte[] buff = new byte[8 * 1024];
                while ((bytesRead = s.Read(buff, 0, buff.Length)) > 0)
                    vs.Write(buff, 0, bytesRead);

				response.Seek(0, SeekOrigin.Begin);
				response.Close();

                // Seek the stream back to the start...
                vs.Position = 0;

                // Build BTS message from the stream
                IBaseMessageFactory mf = transportProxy.GetMessageFactory();
                btsResponse = mf.CreateMessage();
                IBaseMessagePart body = mf.CreateMessagePart();
                body.Data = vs;
                btsResponse.AddPart("Body", body, true);
				btsResponse.Context = context;
            }

            return btsResponse;
        }

        private Stream SendAssemblyExecuteAdapterRequest(IBaseMessage msg, AssemblyExecuteAdapterTransmitProperties config)
        {
			VirtualStream responseStream = null;
            string charset = string.Empty;
            IBaseMessagePart bodyPart = msg.BodyPart;
            Stream btsStream;
            string messageid = msg.MessageID.ToString("D");
            if (null != bodyPart && (null != (btsStream = bodyPart.GetOriginalDataStream())))
			{
                try
                {
                    Type assemblyExecuteType = Type.GetType(config.AssemblyName);
                    IAssemblyExecute assemblyexecute = (IAssemblyExecute)Activator.CreateInstance(assemblyExecuteType);
                    object inputparameters = null;
                    if (!string.IsNullOrEmpty(config.InputParameterXml))
                    {
                        XmlDocument inputXml = new XmlDocument();
                        inputXml.LoadXml(config.InputParameterXml);
                        inputparameters = assemblyexecute.GetInputParameter(inputXml);
                    }
                    Stream responsestream = assemblyexecute.ExecuteResponse(btsStream, inputparameters);
                    #region saveresponsemessage
                    string responsefilename = string.Empty;
                    if (config.SaveResponseMessagePath != string.Empty && config.SaveResponseMessagePath != "N" && responsestream!=null)
                    {
                        if (!Directory.Exists(config.SaveResponseMessagePath))
                            Directory.CreateDirectory(config.SaveResponseMessagePath);

                        responsefilename = Path.Combine(config.SaveResponseMessagePath, "res_" + messageid + ".txt");
                        SaveFile(responsefilename, responsestream);
                        responsestream.Seek(0, SeekOrigin.Begin);
                    }
                    #endregion
                    if (config.IsTwoWay)
                    {
                        responseStream = new VirtualStream(responsestream);
                    }
                }
                catch(Exception e)
                {
                    #region saveerrormessage
                    string errorfilename = string.Empty;
                    if (config.SaveErrorMessagePath != string.Empty && config.SaveErrorMessagePath != "N") {
                        if (!Directory.Exists(config.SaveErrorMessagePath))
                            Directory.CreateDirectory(config.SaveErrorMessagePath);

                        errorfilename = Path.Combine(config.SaveErrorMessagePath ,"req_"+messageid + ".txt");
                        SaveFile(errorfilename, btsStream);
                    }


                    #endregion
                    string Source = "AssemblyExecuteAdapter";
                    string Log = "Application";
                    string Event = e.Message + "\r\n request message saved :" + errorfilename;
                    if (!EventLog.SourceExists(Source))
                        EventLog.CreateEventSource(Source, Log);

                    EventLog.WriteEntry(Source, Event, EventLogEntryType.Error);
                    throw;
                }
			}
            return responseStream;
        }

        private void SaveFile(String path, Stream stream)
        {
            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
                fileStream.Flush();
                fileStream.Close();
            }
        }
    }
}
