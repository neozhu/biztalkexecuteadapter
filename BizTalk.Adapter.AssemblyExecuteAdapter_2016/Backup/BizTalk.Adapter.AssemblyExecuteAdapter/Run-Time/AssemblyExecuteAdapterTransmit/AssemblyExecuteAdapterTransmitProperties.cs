//---------------------------------------------------------------------
// File: AssemblyExecuteAdapterTransmitProperties.cs
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
using System.Net;

using Microsoft.XLANGs.BaseTypes;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.Samples.BizTalk.Adapter.Common;

namespace BizTalk.Adapters.Runtime.AssemblyExecuteAdapterTransmitter
{
    /// <summary>
    /// This class maintains send port properties associated with a message. These properties
    /// will be extracted from the message context for static send ports.
    /// </summary>
    public class AssemblyExecuteAdapterTransmitProperties : ConfigProperties
    {
        private static string protocolAlias = "AssemblyExecuteAdapter://";

		// Handler members...
        private static int batchSize = 50;
		private static int timeOut;

		// Endpoint members...
        private bool isTwoWay;		
        private string uri;
		private string assemblyName;
		private string inputParameterXml;
		private string saveResponseMessagePath;
		private string saveErrorMessagePath;


        private static PropertyBase isSolicitResponseProp = new BTS.IsSolicitResponse();

        // If we needed to use SSO we will need this extra property. It is set in the
        // LocationConfiguration method below.
        // Additionally:
        //   TransmitLocation.xsd in the design-time project must also be edited to
        //   expose the necessary SSO properties.
        //   DotNetFileAsyncTransmitterBatch.cs within the run-time project must be
        //   edited to retrieve and populate the SSOResult class.
        //private string ssoAffiliateApplication;
        //public string AffiliateApplication { get { return ssoAffiliateApplication; } }

        public AssemblyExecuteAdapterTransmitProperties(IBaseMessage message, string propertyNamespace)
        {
            XmlDocument locationConfigDom = null;

            //  get the adapter configuration off the message
            string config = (string) message.Context.Read("AdapterConfig", propertyNamespace);
            this.isTwoWay = (bool) message.Context.Read(isSolicitResponseProp.Name.Name, isSolicitResponseProp.Name.Namespace);

            //  the config can be null all that means is that we are doing a dynamic send
            if (null != config)
            {
                locationConfigDom = new XmlDocument();
                locationConfigDom.LoadXml(config);

                //  For Dynamic Sends the Location config can be null
                //  Location properties - possibly override some handler properties
                this.LocationConfiguration(locationConfigDom);
            }
        }

        public AssemblyExecuteAdapterTransmitProperties(string uri)
        {
            this.uri = uri;
            UpdateUriForDynamicSend();
        }
		
		//Handler properties
		public static int BatchSize	{ get { return AssemblyExecuteAdapterTransmitProperties.batchSize; }	}
		public static int TimeOut { get { return AssemblyExecuteAdapterTransmitProperties.timeOut; } }

		//Endpoint properties
        public bool IsTwoWay { get { return this.isTwoWay; } }
        public string Uri { get { return this.uri; } }
		public string AssemblyName { get { return this.assemblyName; } }
		public string InputParameterXml { get { return this.inputParameterXml; } }
		public string SaveResponseMessagePath { get { return this.saveResponseMessagePath; } }
		public string SaveErrorMessagePath { get { return this.saveErrorMessagePath; } }


        public static void TransmitHandlerConfiguration(XmlDocument configDOM)
        {
			AssemblyExecuteAdapterTransmitProperties.timeOut = ExtractInt(configDOM, "/Config/TimeOut");

        }

        public void LocationConfiguration (XmlDocument configDOM)
        {
			// If we needed to use SSO we will need this extra property
			//this.ssoAffiliateApplication = IfExistsExtract(configDOM, "/Config/ssoAffiliateApplication");

			this.assemblyName = Extract(configDOM, "/Config/AssemblyName", String.Empty);
			this.inputParameterXml = Extract(configDOM, "/Config/InputParameterXml", String.Empty);
			this.saveResponseMessagePath = Extract(configDOM, "/Config/SaveResponseMessagePath", String.Empty);
			this.saveErrorMessagePath = Extract(configDOM, "/Config/SaveErrorMessagePath", String.Empty);

        }


        public void UpdateUriForDynamicSend()
        {
            // Strip off the adapters alias
            if ( this.uri.StartsWith(protocolAlias, StringComparison.OrdinalIgnoreCase))
            {
                string newUri = this.uri.Substring(protocolAlias.Length);
                this.uri = newUri;
            }
        }
    }
}
