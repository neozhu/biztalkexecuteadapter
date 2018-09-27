//---------------------------------------------------------------------
// File: AdapterManagement.cs
// 
// Summary: Implementation of adapter framework interfaces for sample
// adapters.
//
// Sample: Adapter framework adapter.
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
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Adapter.Framework;
using Microsoft.Samples.BizTalk.Adapter.Common;

namespace BizTalk.Adapters.AssemblyExecuteAdapterDesignTime
{
    /// <summary>
    /// Class StaticAdapterManagement implements
    /// IAdapterConfig and IStaticAdapterConfig interfaces for
    /// management to illustrate a static adapter that uses the
    /// adapter framework
    /// </summary>
    public class AdapterManagement : AdapterManagementBase ,IAdapterConfig, IStaticAdapterConfig, IAdapterConfigValidation 
	{
		private static ResourceManager resourceManager = new ResourceManager("BizTalk.Adapters.AssemblyExecuteAdapterDesignTime.AssemblyExecuteAdapterAdapterResource", Assembly.GetExecutingAssembly());
																																				  
		/// <summary>
        /// Returns the configuration schema as a string.
        /// (Implements IAdapterConfig)
        /// </summary>
        /// <param name="type">Configuration schema to return</param>
        /// <returns>Selected xsd schema as a string</returns>
        public string GetConfigSchema(ConfigType type) 
		{
            switch (type) 
			{
				case ConfigType.ReceiveHandler:
					return LocalizeSchema(GetResource("BizTalk.Adapters.AssemblyExecuteAdapterDesignTime.ReceiveHandler.xsd"), resourceManager);

				case ConfigType.ReceiveLocation:
					return LocalizeSchema(GetResource("BizTalk.Adapters.AssemblyExecuteAdapterDesignTime.ReceiveLocation.xsd"), resourceManager);

				case ConfigType.TransmitHandler:
					return LocalizeSchema(GetResource("BizTalk.Adapters.AssemblyExecuteAdapterDesignTime.TransmitHandler.xsd"), resourceManager);

				case ConfigType.TransmitLocation:
					return LocalizeSchema(GetResource("BizTalk.Adapters.AssemblyExecuteAdapterDesignTime.TransmitLocation.xsd"), resourceManager);

				default:
					return null;
			}
        }

        /// <summary>
		/// Get the WSDL file name for the selected WSDL in GetServiceOrganization.
		/// Dummy implementation with a file open dialog.
        /// </summary>
        /// <param name="wsdls">place holder</param>
        /// <returns>The selected WSDL or an empty string[]</returns>
        public string[] GetServiceDescription(string[] wsdls) 
		{
			// Simple implementation, assuming the wsdl is on the filesystem
			string[] result = new string[1];
			result[0] = "";
			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Filter = "Service description files (*.wsdl)|*.wsdl|All files (*.*)|*.*";
			if (fileDialog.ShowDialog() == DialogResult.OK)
			{
				using (TextReader tr = new StreamReader(fileDialog.FileName))
				{
					result[0] = tr.ReadToEnd();
					tr.Close();
				}
			}
			return result;
        }

        /// <summary>
        /// This function is called by the add generated items functionality in 
		/// BizTalk (rightclick BizTalk project in VS, Add, etc.) 
		/// Gets the XML instance of TreeView that needs to be rendered
		/// Dummy implementation with a file open dialog.
        /// </summary>
        /// <param name="endPointConfiguration"></param>
        /// <param name="NodeIdentifier"></param>
        /// <returns>TreeView xml instance</returns>
        public string GetServiceOrganization(IPropertyBag endPointConfiguration,
                                             string NodeIdentifier)
		{
			#region Documentation on GetServiceOrganization & GetServiceOrganization
			/* 
			This function should return an XML string in the following format:
			<?xml version="1.0" encoding="utf-8" ?>
			<CategoryTree>
				<DisplayName>Services Organization</DisplayName>
				<DisplayDescription>An organization of application services</DisplayDescription>
				<CategoryTreeNode>
					<DisplayName>Health Care</DisplayName>
					<Description>Services under Health Care</Description>
					<CategoryTreeNode>
						<DisplayName>Administrative</DisplayName>
						<Description>Administrative Health Care Services</Description>
						<ServiceTreeNode>
							<DisplayName>Eligibility</DisplayName>
							<Description>Eligibility Verification Transactions</Description>
							<WSDLReference>SAPService.wsdl</WSDLReference>
						</ServiceTreeNode>
					</CategoryTreeNode>
				</CategoryTreeNode>
			</CategoryTree>
			After this, a dialog box is shown where the user should select the servicetree node.
			Next the GetServiceDescription function is called. The GetServiceDescription
			should find the referred WSDL file (in this example SAPService.wsdl) and return the content
			to BizTalk. Then BizTalk will add the appropriate XSD and otrchestration items to 
			the user's project. 
			*/
			#endregion
			// Simple implementation, assuming the TreeView xml instance is on the filesystem
			string result = "";
			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Filter = "Service description files (*.xml)|*.xml|All files (*.*)|*.*";
			if (fileDialog.ShowDialog() == DialogResult.OK)
			{
				using (TextReader tr = new StreamReader(fileDialog.FileName))
				{
					result = tr.ReadToEnd();
					tr.Close();
				}
			}
            return result;
        }

        /// <summary>
        /// Acquire externally referenced xsd's
        /// </summary>
        /// <param name="xsdLocation">Location of schema</param>
        /// <param name="xsdNamespace">Namespace</param>
        /// <param name="XSDFileName">Schema file name (return)</param>
        /// <returns>Outcome of acquisition</returns>
        public Result GetSchema(string xsdLocation,
                                string xsdNamespace,
								out string xsdSchema) 
		{
            xsdSchema = null;
            return Result.Continue;
        }

        /// <summary>
        /// Validate xmlInstance against configuration.  In this example it does nothing.
        /// </summary>
        /// <param name="type">Type of port or location being configured</param>
        /// <param name="xmlInstance">Instance value to be validated</param>
        /// <returns>Validated configuration.</returns>
        public string ValidateConfiguration(ConfigType configType,
            string xmlInstance) 
		{
            string validXml = String.Empty;

            switch (configType) 
			{
				case ConfigType.ReceiveHandler:
					validXml = xmlInstance; 
					break;

				case ConfigType.ReceiveLocation:
					validXml = ValidateReceiveLocation(xmlInstance); 
					break;

				case ConfigType.TransmitHandler:
					validXml = xmlInstance; 
					break;

				case ConfigType.TransmitLocation:
					validXml = ValidateTransmitLocation(xmlInstance); 
					break;
            }

            return validXml;
        }

        /// <summary>
        /// Helper to get resource from manifest. Replace with 
        /// ResourceManager.GetString if .resources or
        /// .resx files are used for managing this assemblies resources.
        /// </summary>
        /// <param name="resource">Full resource name</param>
        /// <returns>Resource value</returns>
        private string GetResource(string resource) 
		{
            string value = null;
            if (null != resource) 
			{
				Assembly assem = this.GetType().Assembly;
				Stream stream = assem.GetManifestResourceStream(resource);
				StreamReader reader = null;

                using (reader = new StreamReader(stream)) 
				{
                    value = reader.ReadToEnd();
                }
            }

            return value;
        }

		/// <summary>
		/// Generate uri entry based on receive location configuration values
		/// </summary>
		/// <param name="xmlInstance">Instance value to be validated</param>
		/// <returns>Validated configuration.</returns>
        private string ValidateReceiveLocation(string xmlInstance) 
		{
            // Load up document
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlInstance);
            

			XmlNode uri = document.SelectSingleNode("Config/uri");
			if (null == uri) 
			{
				uri = document.CreateElement("uri");
				document.DocumentElement.AppendChild(uri);
			}

                           
            return document.OuterXml;
        }

        /// <summary>
		/// Generate uri entry based on transmit location configuration values
        /// </summary>
        /// <param name="xmlInstance">Instance value to be validated</param>
        /// <returns>Validated configuration.</returns>
        private string ValidateTransmitLocation(string xmlInstance) 
		{
            // Load up document
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlInstance);
            
			string valueOfAssemblyName = ValidateField(document, "AssemblyName");
			string valueOfInputParameterXml = ValidateField(document, "InputParameterXml");
			string valueOfSaveResponseMessagePath = ValidateField(document, "SaveResponseMessagePath");
			string valueOfSaveErrorMessagePath = ValidateField(document, "SaveErrorMessagePath");

			XmlNode uri = document.SelectSingleNode("Config/uri");
			if (null == uri) 
			{
				uri = document.CreateElement("uri");
				document.DocumentElement.AppendChild(uri);
			}
			uri.InnerText = "AssemblyExecuteAdapter://" + valueOfAssemblyName + "/" + valueOfInputParameterXml;
                           
            return document.OuterXml;
        }

		private string ValidateField(XmlDocument document,string partialXPath)
		{
			XmlNode node = document.SelectSingleNode("Config/" + partialXPath);
			// Ensure that the field supplied is not empty
			if ( node == null || node.InnerText == String.Empty ) 
				throw new ApplicationException("Transport properties validation failed. Value for required adapter property \"" + partialXPath + "\" is not specified.");
			return node.InnerText;
		}
	} 
}
