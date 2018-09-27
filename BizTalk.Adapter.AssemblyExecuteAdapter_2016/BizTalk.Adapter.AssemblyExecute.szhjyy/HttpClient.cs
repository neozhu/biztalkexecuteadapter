using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace BizTalk.Adapter.AssemblyExecute.szhjyy
{
     public class HttpClient
    {
         public static Stream SendRequest(string url,string user=null,string password=null)
         {
            
             // Create the web request
             HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
             webRequest.Timeout = 600000;
             webRequest.ProtocolVersion = HttpVersion.Version11;
             webRequest.Proxy = null;
             webRequest.KeepAlive = false;
             webRequest.Method = "GET";
             webRequest.ContentType = "text/xml; encoding='utf-8'";
             webRequest.Proxy = null;
             //webRequest.UserAgent = "MESWebClient";
             webRequest.ReadWriteTimeout = 600000;
             if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(password)) {
                 string authInfo = user + "&" + password + "&" +DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") ;
                 authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                 webRequest.Headers["Authorization"] = "Basic " + authInfo;
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
