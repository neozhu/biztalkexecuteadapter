//---------------------------------------------------------------------
// File: AssemblyExecuteAdapterAdapterExceptions.cs
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
using System.Runtime.Serialization;
using Microsoft.Samples.BizTalk.Adapter.Common;

namespace BizTalk.Adapters.Runtime.AssemblyExecuteAdapterTransmitter
{
	internal class AssemblyExecuteAdapterAdapterException : ApplicationException
	{
		public static string UnhandledTransmit_Error = "The .Net File Adapter encounted an error transmitting a batch of messages.";

		public AssemblyExecuteAdapterAdapterException () { }

		public AssemblyExecuteAdapterAdapterException (string msg) : base(msg) { }

		public AssemblyExecuteAdapterAdapterException (Exception inner) : base(String.Empty, inner) { }

		public AssemblyExecuteAdapterAdapterException (string msg, Exception e) : base(msg, e) { }

		protected AssemblyExecuteAdapterAdapterException (SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}

