﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5420
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.CompactFramework.Design.Data, Version 2.0.50727.5420.
// 
namespace TSD.WS {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WSSoap", Namespace="http://tempuri.org/")]
    public partial class WS : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public WS() {
            this.Url = "http://ch.sd2.com.ua/WS/WS.asmx";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HelloWorld", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string HelloWorld() {
            object[] results = this.Invoke("HelloWorld", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginHelloWorld(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("HelloWorld", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public string EndHelloWorld(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Get_Shop_On_Guid", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Get_Shop_On_Guid(string guid, string data, int num_base) {
            object[] results = this.Invoke("Get_Shop_On_Guid", new object[] {
                        guid,
                        data,
                        num_base});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGet_Shop_On_Guid(string guid, string data, int num_base, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Get_Shop_On_Guid", new object[] {
                        guid,
                        data,
                        num_base}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndGet_Shop_On_Guid(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Get_Document_1c_Json", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Get_Document_1c_Json(string guid, string data, int num_base) {
            object[] results = this.Invoke("Get_Document_1c_Json", new object[] {
                        guid,
                        data,
                        num_base});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGet_Document_1c_Json(string guid, string data, int num_base, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Get_Document_1c_Json", new object[] {
                        guid,
                        data,
                        num_base}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndGet_Document_1c_Json(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Get_Document_1c", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Get_Document_1c(string guid, string data, int num_base) {
            object[] results = this.Invoke("Get_Document_1c", new object[] {
                        guid,
                        data,
                        num_base});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGet_Document_1c(string guid, string data, int num_base, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Get_Document_1c", new object[] {
                        guid,
                        data,
                        num_base}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndGet_Document_1c(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Get_Document_1c_Box", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Get_Document_1c_Box(string guid, string data, int num_base) {
            object[] results = this.Invoke("Get_Document_1c_Box", new object[] {
                        guid,
                        data,
                        num_base});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGet_Document_1c_Box(string guid, string data, int num_base, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Get_Document_1c_Box", new object[] {
                        guid,
                        data,
                        num_base}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndGet_Document_1c_Box(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetTMCForTSD", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetTMCForTSD(string guid, string data, int num_base) {
            object[] results = this.Invoke("GetTMCForTSD", new object[] {
                        guid,
                        data,
                        num_base});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetTMCForTSD(string guid, string data, int num_base, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetTMCForTSD", new object[] {
                        guid,
                        data,
                        num_base}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndGetTMCForTSD(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetTMCForTSDJson", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] GetTMCForTSDJson(string guid, string data, int num_base, int vid) {
            object[] results = this.Invoke("GetTMCForTSDJson", new object[] {
                        guid,
                        data,
                        num_base,
                        vid});
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetTMCForTSDJson(string guid, string data, int num_base, int vid, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetTMCForTSDJson", new object[] {
                        guid,
                        data,
                        num_base,
                        vid}, callback, asyncState);
        }
        
        /// <remarks/>
        public byte[] EndGetTMCForTSDJson(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/UploadDocumentTSD", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string UploadDocumentTSD(string guid, string data, int num_base) {
            object[] results = this.Invoke("UploadDocumentTSD", new object[] {
                        guid,
                        data,
                        num_base});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginUploadDocumentTSD(string guid, string data, int num_base, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("UploadDocumentTSD", new object[] {
                        guid,
                        data,
                        num_base}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndUploadDocumentTSD(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetExistDocumentTSD", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetExistDocumentTSD(string guid, string data, int num_base) {
            object[] results = this.Invoke("GetExistDocumentTSD", new object[] {
                        guid,
                        data,
                        num_base});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetExistDocumentTSD(string guid, string data, int num_base, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetExistDocumentTSD", new object[] {
                        guid,
                        data,
                        num_base}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndGetExistDocumentTSD(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ExistsUpdateProrgam", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ExistsUpdateProrgam(string guid, string data, int num_base) {
            object[] results = this.Invoke("ExistsUpdateProrgam", new object[] {
                        guid,
                        data,
                        num_base});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginExistsUpdateProrgam(string guid, string data, int num_base, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ExistsUpdateProrgam", new object[] {
                        guid,
                        data,
                        num_base}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndExistsUpdateProrgam(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetUpdateProgram", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] GetUpdateProgram(string guid, string data, int num_base) {
            object[] results = this.Invoke("GetUpdateProgram", new object[] {
                        guid,
                        data,
                        num_base});
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetUpdateProgram(string guid, string data, int num_base, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetUpdateProgram", new object[] {
                        guid,
                        data,
                        num_base}, callback, asyncState);
        }
        
        /// <remarks/>
        public byte[] EndGetUpdateProgram(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((byte[])(results[0]));
        }
    }
}
