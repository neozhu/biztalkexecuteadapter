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

// 
// This source code was auto-generated by wsdl, Version=4.0.30319.1.
// 


/// <remarks/>
// CODEGEN: 未处理命名空间“http://schemas.xmlsoap.org/ws/2004/09/policy”中的可选 WSDL 扩展元素“Policy”。
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="IF_WMS_MM008", Namespace="urn:sap-com:document:sap:rfc:functions")]
public partial class ZNJHK_IF_WMS_MM008 : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    private System.Threading.SendOrPostCallback CallZNJHK_IF_WMS_MM008OperationCompleted;
    
    /// <remarks/>
    public ZNJHK_IF_WMS_MM008(string url = "http://58.240.32.198:8000/sap/bc/srt/rfc/sap/znjhk_if_wms_mm008/310/znjhk_if_wms_mm008/if_wms_mm008")
    {
        this.Url = url;
    }
    
    /// <remarks/>
    public event CallZNJHK_IF_WMS_MM008CompletedEventHandler CallZNJHK_IF_WMS_MM008Completed;
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:sap-com:document:sap:rfc:functions:ZNJHK_IF_WMS_MM008:ZNJHK_IF_WMS_MM008Reque" +
        "st", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
    [return: System.Xml.Serialization.XmlElementAttribute("ZNJHK_IF_WMS_MM008Response", Namespace="urn:sap-com:document:sap:rfc:functions")]
    public ZNJHK_IF_WMS_MM008Response CallZNJHK_IF_WMS_MM008([System.Xml.Serialization.XmlElementAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")] ZNJHK_IF_WMS_MM0081 ZNJHK_IF_WMS_MM008) {
        object[] results = this.Invoke("CallZNJHK_IF_WMS_MM008", new object[] {
                    ZNJHK_IF_WMS_MM008});
        return ((ZNJHK_IF_WMS_MM008Response)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginCallZNJHK_IF_WMS_MM008(ZNJHK_IF_WMS_MM0081 ZNJHK_IF_WMS_MM008, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("CallZNJHK_IF_WMS_MM008", new object[] {
                    ZNJHK_IF_WMS_MM008}, callback, asyncState);
    }
    
    /// <remarks/>
    public ZNJHK_IF_WMS_MM008Response EndCallZNJHK_IF_WMS_MM008(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((ZNJHK_IF_WMS_MM008Response)(results[0]));
    }
    
    /// <remarks/>
    public void CallZNJHK_IF_WMS_MM008Async(ZNJHK_IF_WMS_MM0081 ZNJHK_IF_WMS_MM008) {
        this.CallZNJHK_IF_WMS_MM008Async(ZNJHK_IF_WMS_MM008, null);
    }
    
    /// <remarks/>
    public void CallZNJHK_IF_WMS_MM008Async(ZNJHK_IF_WMS_MM0081 ZNJHK_IF_WMS_MM008, object userState) {
        if ((this.CallZNJHK_IF_WMS_MM008OperationCompleted == null)) {
            this.CallZNJHK_IF_WMS_MM008OperationCompleted = new System.Threading.SendOrPostCallback(this.OnCallZNJHK_IF_WMS_MM008OperationCompleted);
        }
        this.InvokeAsync("CallZNJHK_IF_WMS_MM008", new object[] {
                    ZNJHK_IF_WMS_MM008}, this.CallZNJHK_IF_WMS_MM008OperationCompleted, userState);
    }
    
    private void OnCallZNJHK_IF_WMS_MM008OperationCompleted(object arg) {
        if ((this.CallZNJHK_IF_WMS_MM008Completed != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.CallZNJHK_IF_WMS_MM008Completed(this, new CallZNJHK_IF_WMS_MM008CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    public new void CancelAsync(object userState) {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:sap-com:document:sap:rfc:functions")]
public partial class ZNJHK_IF_WMS_MM0081 {
    
    private string iNPUTField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string INPUT {
        get {
            return this.iNPUTField;
        }
        set {
            this.iNPUTField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:sap-com:document:sap:rfc:functions")]
public partial class ZNJHK_IF_WMS_MM008Response {
    
    private string oUTPUTField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string OUTPUT {
        get {
            return this.oUTPUTField;
        }
        set {
            this.oUTPUTField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
public delegate void CallZNJHK_IF_WMS_MM008CompletedEventHandler(object sender, CallZNJHK_IF_WMS_MM008CompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class CallZNJHK_IF_WMS_MM008CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal CallZNJHK_IF_WMS_MM008CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public ZNJHK_IF_WMS_MM008Response Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((ZNJHK_IF_WMS_MM008Response)(this.results[0]));
        }
    }
}
