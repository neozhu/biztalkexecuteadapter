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
    public class CSBSDK : IAssemblyExecute
    {
        public object GetInputParameter(System.Xml.XmlDocument xmldoc)
        {
            XDocument doc = XDocument.Parse(xmldoc.OuterXml);
            var input = from el in doc.Descendants("InParameters")
                        select new InputParameters
                        {
                            ak = el.Element("ak").Value,
                            apiname = el.Element("apiname").Value,
                            functionname = el.Element("functionname").Value,
                            method = el.Element("method").Value,
                            sk = el.Element("sk").Value,
                            url = el.Element("url").Value,
                            version = el.Element("version").Value,
                            proxyurl = el.Element("proxyurl").Value,
                            key1xpath = el.Element("key1xpath").Value,
                            key2xpath = el.Element("key2xpath").Value,
                            key3xpath = el.Element("key3xpath").Value,
                            connectionstring = el.Element("connectionstring").Value,
                            res1xpath = el.Element("res1xpath").Value,
                            res2xpath = el.Element("res2xpath").Value,
                            tag = el.Element("tag").Value,
                            saveerrorresponse = el.Element("saveerrorresponse").Value
                           
                        }
                                           ;
            return input.First();
        }

        public System.IO.Stream ExecuteResponse(System.IO.Stream stream, object inputparameters)
        {
            var para = (InputParameters)inputparameters;
            try
            {
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
                //如果返回不是空就是说明是异常
                if (string.IsNullOrEmpty(response))
                {
                    WriteLog(stream, para);
                    return null;
                }
                else
                {
                    var res = JsonConvert.DeserializeXmlNode(response, para.functionname + "_RES");
                    Stream responsestream = new MemoryStream();
                    res.Save(responsestream);
                    Console.Write(res.OuterXml);
                    var key1 = string.Empty;
                    var key2 = string.Empty;
                    key1 = GetKey(XDocument.Parse(doc.OuterXml), para.key1xpath);
                    key2 = GetKey(XDocument.Parse(doc.OuterXml), para.key2xpath);
                    WriteLog(stream, responsestream, para);
                    SaveErrorResponse(responsestream, para.saveerrorresponse, para.functionname, key1, key2);
                    responsestream.Seek(0, SeekOrigin.Begin);
                    return null;
                  
                }
                
               
                 
                
            }
            catch (Exception e)
            {
                //WriteLog(stream, e.Message, para);
                throw e;
            }
        }
        public string GetError(Stream stream, string xpath)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var xdoc = XDocument.Load(stream);
            var res2 = GetKey(xdoc, xpath);
            return res2;

        }
        private void WriteLog(Stream reqstream, Stream resstream, InputParameters para)
        {
            reqstream.Seek(0, SeekOrigin.Begin);
            resstream.Seek(0, SeekOrigin.Begin);
            var reqdoc = XDocument.Load(reqstream);
            //var str=StreamToString(resstream);
            var resdoc = XDocument.Load(resstream);
            var key1 = GetKey(reqdoc, para.key1xpath);
            var key2 = GetKey(reqdoc, para.key2xpath);
            var key3 = GetKey(reqdoc, para.key3xpath);
            var res1 = GetKey(resdoc, para.res1xpath);
            var res2 = GetKey(resdoc, para.res2xpath);
            Logger.Write(key1, key2, key3, (string.IsNullOrEmpty(res1) ? "OK" : res1), res2, para.url, para.functionname, para.tag, para.connectionstring);
            //if (!string.IsNullOrEmpty(res1)) {
            //    SaveErrorResponse(resdoc, para.saveerrorresponse,para.functionname);

            //}
        }
        private void SaveErrorResponse(Stream stream, string path, string prefix,string key1,string key2)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var doc = XDocument.Load(stream);
            doc.Root.Add(new XElement("key1", key1));
            doc.Root.Add(new XElement("key2", key2));
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var filename = Path.Combine(path, prefix + "_" + Guid.NewGuid() + ".xml");
            using (var filestream = new FileStream(filename, FileMode.CreateNew)) {
                doc.Save(filestream);
                filestream.Flush();
                filestream.Close();
            }
        
        }
        private void WriteLog(Stream reqstream,  InputParameters para)
        {
            reqstream.Seek(0, SeekOrigin.Begin);

            var reqdoc = XDocument.Load(reqstream);

            var key1 = GetKey(reqdoc, para.key1xpath);
            var key2 = GetKey(reqdoc, para.key2xpath);
            var key3 = GetKey(reqdoc, para.key3xpath);
            var res1 = "OK";
            var res2 = "";
            Logger.Write(key1, key2, key3, res1, res2, para.url, para.functionname, para.tag, para.connectionstring);

        }
        private string GetKey(XDocument doc, string key)
        {

            var q = from el in doc.Descendants()
                    where el.Name.LocalName == key
                    select el;
            var item = q.FirstOrDefault();
            return item == null ? "" : item.Value;
        }
    }

    public class InputParameters
    {
        public string connectionstring { get; set; }
        public string ak { get; set; }
        public string apiname { get; set; }
        public string functionname { get; set; }
        public string method { get; set; }
        public string sk { get; set; }
        public string url { get; set; }
        public string version { get; set; }
        public string jsonstring { get; set; }
        public string proxyurl { get; set; }
        public string tag { get; set; }
        public string key1xpath { get; set; }
        public string key2xpath { get; set; }
        public string key3xpath { get; set; }
        public string res1xpath { get; set; }
        public string res2xpath { get; set; }
        public string saveerrorresponse { get; set; }

    }
}
