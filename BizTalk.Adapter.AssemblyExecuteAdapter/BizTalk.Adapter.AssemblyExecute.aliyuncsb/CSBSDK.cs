using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizTalk.Adapter.AssemblyExecute.Interface;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Xml;

namespace BizTalk.Adapter.AssemblyExecute.aliyuncsb
{
    public class CSBSDK:IAssemblyExecute
    {
        public object GetInputParameter(System.Xml.XmlDocument xmldoc)
        {
            XDocument doc = XDocument.Parse(xmldoc.OuterXml);
            var   input = from el in doc.Descendants("InParameters")
                                      select new InputParameters
                                      {
                                          ak = el.Element("ak").Value,
                                          apiname = el.Element("apiname").Value,
                                          functionname = el.Element("functionname").Value,
                                          method = el.Element("method").Value,
                                          sk = el.Element("sk").Value,
                                           url = el.Element("url").Value,
                                          version = el.Element("version").Value,
                                          proxyurl = el.Element("proxyurl").Value
                                            }
                                           ;
            return input.First();
        }

        public System.IO.Stream ExecuteResponse(System.IO.Stream stream, object inputparameters)
        {
            var para = (InputParameters)inputparameters;
            csb.JAxis2Tunnel client = new csb.JAxis2Tunnel(para.proxyurl);
            csb.RequestEntity request = new csb.RequestEntity();
            request.ak = para.ak;
            request.apiname = para.apiname;
            request.functionname = para.functionname;
            request.method = para.method;
            request.sk = para.sk;
            request.url = para.url;
            request.version = para.version;
            stream.Seek(0, SeekOrigin.Begin);
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);
            request.jsonstring = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
            string response = client.getResult(request);
            var res = JsonConvert.DeserializeXmlNode(response, para.functionname + "_RES");
            Stream docstream=new MemoryStream();
            res.Save(docstream);
            Console.Write(res.OuterXml);
            docstream.Seek(0, SeekOrigin.Begin);
            return docstream;
        }
    }

    public class InputParameters { 
        public string ak{get;set;}
        public string apiname{get;set;}
        public string functionname{get;set;}
        public string method{get;set;}
        public string sk{get;set;}
        public string url{get;set;}
        public string version{get;set;}
        public string jsonstring{get;set;}
        public string proxyurl { get; set; }
          
    }
}
