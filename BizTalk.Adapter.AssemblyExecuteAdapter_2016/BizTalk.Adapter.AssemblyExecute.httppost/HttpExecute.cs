using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizTalk.Adapter.AssemblyExecute.Interface;
using System.Xml.Linq;
using System.IO;

namespace BizTalk.Adapter.AssemblyExecute.httppost
{
    public class HttpExecute:IAssemblyExecute
    {

        public object GetInputParameter( XDocument xdoc)
        {

            var input = from el in xdoc.Descendants("InParameters")
                        select new InputParameters
                        {
                            connectionstring = el.Element("connectionstring").Value,
                            soapaction = el.Element("soapaction").Value,
                            key1xpath = el.Element("key1xpath").Value,
                            key2xpath = el.Element("key2xpath").Value,
                            key3xpath = el.Element("key3xpath").Value,
                            url = el.Element("url").Value,
                            res1xpath = el.Element("res1xpath").Value,
                            res2xpath = el.Element("res2xpath").Value,
                            tag = el.Element("tag").Value
                        }
                                           ;
            return input.First();
        }
        public object GetInputParameter(System.Xml.XmlDocument xmldoc)
        {
            XDocument doc = XDocument.Parse(xmldoc.OuterXml);
            var input = from el in doc.Descendants("InParameters")
                        select new InputParameters
                        {
                            connectionstring = el.Element("connectionstring").Value,
                            soapaction = el.Element("soapaction").Value,
                            key1xpath = el.Element("key1xpath").Value,
                            key2xpath = el.Element("key2xpath").Value,
                            key3xpath = el.Element("key3xpath").Value,
                            url = el.Element("url").Value,
                            res1xpath = el.Element("res1xpath").Value,
                            res2xpath = el.Element("res2xpath").Value,
                            tag=el.Element("tag").Value                        }
                                           ;
            return input.First();
        }

        public System.IO.Stream ExecuteResponse(System.IO.Stream stream, object inputparameters)
        {
            var para = (InputParameters)inputparameters;
            try
            {
                
                var result = HttpClient.SendRequest(para.url, stream);
                WriteLog(stream, result, para);
                result.Seek(0, SeekOrigin.Begin);
                return result;
            }catch(Exception e)
            {
                WriteLog(stream, e.Message, para);
                throw e;
            }
        }
        public   string StreamToString(Stream stream)
        {
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
        private void WriteLog(Stream reqstream, Stream resstream,InputParameters para) {
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

            Logger.Write(key1, key2, key3, res1, res2, para.url, para.soapaction, para.tag, para.connectionstring);

        }
        private void WriteLog(Stream reqstream, string exmsg, InputParameters para)
        {
            reqstream.Seek(0, SeekOrigin.Begin);
            
            var reqdoc = XDocument.Load(reqstream);
            
            var key1 = GetKey(reqdoc, para.key1xpath);
            var key2 = GetKey(reqdoc, para.key2xpath);
            var key3 = GetKey(reqdoc, para.key3xpath);
            var res1 = exmsg;
            var res2 = "";
            Logger.Write(key1, key2, key3, res1, res2, para.url, para.soapaction, para.tag,para.connectionstring);

        }
        private string GetKey(XDocument doc, string key) {
        
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
        public string url { get; set; }
        public string tag { get; set; }
        public string soapaction { get; set; }
        public string key1xpath { get; set; }
        public string key2xpath { get; set; }
        public string key3xpath { get; set; }
        public string res1xpath { get; set; }
        public string res2xpath { get; set; }

    }
}
