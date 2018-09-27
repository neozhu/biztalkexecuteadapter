using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace BizTalk.Adapter.AssemblyExecute.httpsoappost
{
     public class SoapClient
    {
         public static Stream SendRequest(string url,   Stream requeststream, string soapAction = null,string user=null,string password=null, bool useSOAP12 = false)
         {
            
             // Create the web request
             HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
             webRequest.Timeout = 600000;
             webRequest.ProtocolVersion = HttpVersion.Version11;
             webRequest.Proxy = null;
             webRequest.KeepAlive = false;
             webRequest.Headers.Add("SOAPAction", soapAction ?? url);
             webRequest.ContentType = (useSOAP12) ? "application/soap+xml;charset=\"utf-8\"" : "text/xml;charset=\"utf-8\"";
             webRequest.Accept = (useSOAP12) ? "application/soap+xml" : "text/xml";
             webRequest.Method = "POST";
             if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(password)) {
                 string authInfo = user + ":" + password;
                 authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                 webRequest.Headers["Authorization"] = "Basic " + authInfo;
             }
             requeststream.Seek(0, SeekOrigin.Begin);
             // Insert SOAP envelope
             using (Stream stream = webRequest.GetRequestStream())
             {
                 requeststream.CopyTo(stream);
             }

             var responsestream=new MemoryStream();
             using (WebResponse response = webRequest.GetResponse())
             {
                   response.GetResponseStream().CopyTo(responsestream);
                   return responsestream;
             }
              
         }
    }
}
