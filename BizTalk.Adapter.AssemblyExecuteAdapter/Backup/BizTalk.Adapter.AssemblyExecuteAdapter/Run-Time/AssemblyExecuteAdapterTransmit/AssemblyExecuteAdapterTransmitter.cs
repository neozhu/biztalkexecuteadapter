//---------------------------------------------------------------------
// File: AssemblyExecuteAdapterTransmitter.cs
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
using System.Xml;
using System.Threading;

using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.TransportProxy.Interop;
using Microsoft.BizTalk.Message.Interop;

using Microsoft.Samples.BizTalk.Adapter.Common;

namespace BizTalk.Adapters.Runtime.AssemblyExecuteAdapterTransmitter
{
    /// <summary>
    /// This is a singleton class for the AssemblyExecuteAdapter send adapter. All the messages, going to various
    /// send ports of this adapter type, will go through this class.
    /// 
    /// Messages will be delivered to this adapter in batches. The batch implementation is provided
    /// by the AssemblyExecuteAdapterTransmitAdapterBatch class.
    /// </summary>
    sealed public class AssemblyExecuteAdapterTransmitAdapter : AsyncTransmitter
    {
        private static string AssemblyExecuteAdapterNamespace = "http://biztalk.adpater/assemblyexecuteadapter";

        public AssemblyExecuteAdapterTransmitAdapter() : base(
                "AssemblyExecuteAdapter Transmit Adapter",
                "1.0",
                "AssemblyExecuteAdapter",
                "AssemblyExecuteAdapter",
                new Guid(""),
                AssemblyExecuteAdapterNamespace, 
                typeof(AssemblyExecuteAdapterTransmitterEndpoint), 
				10)
        {
        }

		protected override IBTTransmitterBatch CreateAsyncTransmitterBatch()
		{
			return new AssemblyExecuteAdapterTransmitAdapterBatch(this.MaxBatchSize, AssemblyExecuteAdapterNamespace, TransportProxy, this);		
		}

        public ConfigProperties CreateProperties(string uri)
        {
            ConfigProperties properties = new AssemblyExecuteAdapterTransmitProperties(uri);
            return properties;
        }

        protected override void HandlerPropertyBagLoaded()
        {
            IPropertyBag config = this.HandlerPropertyBag;
            if (null != config)
            {
                XmlDocument handlerConfigDom = ConfigProperties.IfExistsExtractConfigDom(config);
                if (null != handlerConfigDom)
                {
                    AssemblyExecuteAdapterTransmitProperties.TransmitHandlerConfiguration(handlerConfigDom);
                }
            }
        }
    }
}
