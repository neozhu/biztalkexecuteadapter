﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18063
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
namespace BizTalk.Adapter.AssemblyExecute.goertek.proxy.shipNotice
{
    // 
    // This source code was auto-generated by wsdl, Version=4.0.30319.1.
    // 


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "Gtk_CuVmi_common_ws_provider_shipNotice_shipNoticeProvider_Binder", Namespace = "http://b2bt.goertek.com:5080/Gtk_CuVmi.common.ws.provider.shipNotice:shipNoticePr" +
        "ovider")]
    public partial class Gtk_CuVmicommonwsprovidershipNoticeshipNoticeProvider : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback shipNoticeOperationCompleted;

        /// <remarks/>
        public Gtk_CuVmicommonwsprovidershipNoticeshipNoticeProvider()
        {
            this.Url = "http://b2bt.goertek.com:5080/ws/Gtk_CuVmi.common.ws.provider.shipNotice:shipNotic" +
                "eProvider/Gtk_CuVmi_common_ws_provider_shipNotice_shipNoticeProvider_Port";
        }
        public Gtk_CuVmicommonwsprovidershipNoticeshipNoticeProvider(string url)
        {
            this.Url = url;
        }

        /// <remarks/>
        public event shipNoticeCompletedEventHandler shipNoticeCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("Gtk_CuVmi_common_ws_provider_shipNotice_shipNoticeProvider_Binder_shipNotice", RequestNamespace = "http://b2bt.goertek.com:5080/Gtk_CuVmi.common.ws.provider.shipNotice:shipNoticePr" +
            "ovider", ResponseNamespace = "http://b2bt.goertek.com:5080/Gtk_CuVmi.common.ws.provider.shipNotice:shipNoticePr" +
            "ovider", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("shipNoticeRes", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
        public shipNoticeRes shipNotice([System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)] shipNoticeReq shipNoticeReq)
        {
            object[] results = this.Invoke("shipNotice", new object[] {
                    shipNoticeReq});
            return ((shipNoticeRes)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginshipNotice(shipNoticeReq shipNoticeReq, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("shipNotice", new object[] {
                    shipNoticeReq}, callback, asyncState);
        }

        /// <remarks/>
        public shipNoticeRes EndshipNotice(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((shipNoticeRes)(results[0]));
        }

        /// <remarks/>
        public void shipNoticeAsync(shipNoticeReq shipNoticeReq)
        {
            this.shipNoticeAsync(shipNoticeReq, null);
        }

        /// <remarks/>
        public void shipNoticeAsync(shipNoticeReq shipNoticeReq, object userState)
        {
            if ((this.shipNoticeOperationCompleted == null))
            {
                this.shipNoticeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnshipNoticeOperationCompleted);
            }
            this.InvokeAsync("shipNotice", new object[] {
                    shipNoticeReq}, this.shipNoticeOperationCompleted, userState);
        }

        private void OnshipNoticeOperationCompleted(object arg)
        {
            if ((this.shipNoticeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.shipNoticeCompleted(this, new shipNoticeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://b2bt.goertek.com:5080/Gtk_CuVmi.common.ws.provider.shipNotice:shipNoticePr" +
        "ovider")]
    public partial class shipNoticeReq
    {

        private ShipNotice shipNoticeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ShipNotice ShipNotice
        {
            get
            {
                return this.shipNoticeField;
            }
            set
            {
                this.shipNoticeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://b2bt.goertek.com:5080/Gtk_CuVmi.common.ws.provider.shipNotice:shipNoticePr" +
        "ovider")]
    public partial class ShipNotice
    {

        private Head headField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public Head Head
        {
            get
            {
                return this.headField;
            }
            set
            {
                this.headField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://b2bt.goertek.com:5080/Gtk_CuVmi.common.ws.provider.shipNotice:shipNoticePr" +
        "ovider")]
    public partial class Head
    {

        private string senderQulifierField;

        private string senderIdField;

        private string receiverQualifierField;

        private string receiverIdField;

        private string documentDateField;

        private string documentIdField;

        private string asnNoField;

        private string asnDateField;

        private string shippedDateField;

        private string attribute1Field;

        private string attribute2Field;

        private string attribute3Field;

        private Detail[] detailField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SenderQulifier
        {
            get
            {
                return this.senderQulifierField;
            }
            set
            {
                this.senderQulifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SenderId
        {
            get
            {
                return this.senderIdField;
            }
            set
            {
                this.senderIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ReceiverQualifier
        {
            get
            {
                return this.receiverQualifierField;
            }
            set
            {
                this.receiverQualifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ReceiverId
        {
            get
            {
                return this.receiverIdField;
            }
            set
            {
                this.receiverIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DocumentDate
        {
            get
            {
                return this.documentDateField;
            }
            set
            {
                this.documentDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DocumentId
        {
            get
            {
                return this.documentIdField;
            }
            set
            {
                this.documentIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AsnNo
        {
            get
            {
                return this.asnNoField;
            }
            set
            {
                this.asnNoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AsnDate
        {
            get
            {
                return this.asnDateField;
            }
            set
            {
                this.asnDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ShippedDate
        {
            get
            {
                return this.shippedDateField;
            }
            set
            {
                this.shippedDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Attribute1
        {
            get
            {
                return this.attribute1Field;
            }
            set
            {
                this.attribute1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Attribute2
        {
            get
            {
                return this.attribute2Field;
            }
            set
            {
                this.attribute2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Attribute3
        {
            get
            {
                return this.attribute3Field;
            }
            set
            {
                this.attribute3Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Detail", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public Detail[] Detail
        {
            get
            {
                return this.detailField;
            }
            set
            {
                this.detailField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://b2bt.goertek.com:5080/Gtk_CuVmi.common.ws.provider.shipNotice:shipNoticePr" +
        "ovider")]
    public partial class Detail
    {

        private string customerPoNoField;

        private string grnNoField;

        private string invoiceNoField;

        private string shipToCodeField;

        private string shipToNameField;

        private string siteCodeField;

        private string siteNameField;

        private string supplierCodeField;

        private string supplierNameField;

        private string attribute1Field;

        private string attribute2Field;

        private string attribute3Field;

        private Item[] itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CustomerPoNo
        {
            get
            {
                return this.customerPoNoField;
            }
            set
            {
                this.customerPoNoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string GrnNo
        {
            get
            {
                return this.grnNoField;
            }
            set
            {
                this.grnNoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string InvoiceNo
        {
            get
            {
                return this.invoiceNoField;
            }
            set
            {
                this.invoiceNoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ShipToCode
        {
            get
            {
                return this.shipToCodeField;
            }
            set
            {
                this.shipToCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ShipToName
        {
            get
            {
                return this.shipToNameField;
            }
            set
            {
                this.shipToNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SiteCode
        {
            get
            {
                return this.siteCodeField;
            }
            set
            {
                this.siteCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SiteName
        {
            get
            {
                return this.siteNameField;
            }
            set
            {
                this.siteNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SupplierCode
        {
            get
            {
                return this.supplierCodeField;
            }
            set
            {
                this.supplierCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SupplierName
        {
            get
            {
                return this.supplierNameField;
            }
            set
            {
                this.supplierNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Attribute1
        {
            get
            {
                return this.attribute1Field;
            }
            set
            {
                this.attribute1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Attribute2
        {
            get
            {
                return this.attribute2Field;
            }
            set
            {
                this.attribute2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Attribute3
        {
            get
            {
                return this.attribute3Field;
            }
            set
            {
                this.attribute3Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Item", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public Item[] Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://b2bt.goertek.com:5080/Gtk_CuVmi.common.ws.provider.shipNotice:shipNoticePr" +
        "ovider")]
    public partial class Item
    {

        private string customerItemCodeField;

        private string vendorItemCodeField;

        private string manufactureCodeField;

        private string itemQtyField;

        private string unitField;

        private string attribute1Field;

        private string attribute2Field;

        private string attribute3Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CustomerItemCode
        {
            get
            {
                return this.customerItemCodeField;
            }
            set
            {
                this.customerItemCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VendorItemCode
        {
            get
            {
                return this.vendorItemCodeField;
            }
            set
            {
                this.vendorItemCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ManufactureCode
        {
            get
            {
                return this.manufactureCodeField;
            }
            set
            {
                this.manufactureCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ItemQty
        {
            get
            {
                return this.itemQtyField;
            }
            set
            {
                this.itemQtyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Unit
        {
            get
            {
                return this.unitField;
            }
            set
            {
                this.unitField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Attribute1
        {
            get
            {
                return this.attribute1Field;
            }
            set
            {
                this.attribute1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Attribute2
        {
            get
            {
                return this.attribute2Field;
            }
            set
            {
                this.attribute2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Attribute3
        {
            get
            {
                return this.attribute3Field;
            }
            set
            {
                this.attribute3Field = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://b2bt.goertek.com:5080/Gtk_CuVmi.common.ws.provider.shipNotice:shipNoticePr" +
        "ovider")]
    public partial class Response
    {

        private string documentIdField;

        private string codeField;

        private string messageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DocumentId
        {
            get
            {
                return this.documentIdField;
            }
            set
            {
                this.documentIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://b2bt.goertek.com:5080/Gtk_CuVmi.common.ws.provider.shipNotice:shipNoticePr" +
        "ovider")]
    public partial class shipNoticeRes
    {

        private Response responseField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public Response Response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    public delegate void shipNoticeCompletedEventHandler(object sender, shipNoticeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class shipNoticeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal shipNoticeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public shipNoticeRes Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((shipNoticeRes)(this.results[0]));
            }
        }
    }
}