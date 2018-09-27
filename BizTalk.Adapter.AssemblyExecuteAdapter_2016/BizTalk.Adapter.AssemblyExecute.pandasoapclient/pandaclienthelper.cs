using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizTalk.Adapter.AssemblyExecute.Interface;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace BizTalk.Adapter.AssemblyExecute.aliyuncsb
{
    public class pandaclienthelper : IAssemblyExecute
    {
        public object GetInputParameter(System.Xml.XmlDocument xmldoc)
        {
            XDocument doc = XDocument.Parse(xmldoc.OuterXml);
            var input = from el in doc.Descendants("InParameters")
                        select new InputParameters
                        {
                            functionname = el.Element("functionname").Value,
                            username = el.Element("username").Value,
                            password = el.Element("password").Value,
                            url = el.Element("url").Value,
                         
                            connectionstring = el.Element("connectionstring").Value,
                          
                            tag = el.Element("tag").Value,
                            saveerrorresponse = el.Element("saveerrorresponse").Value,
                            saverequestdata = el.Element("saverequestdata").Value
                           
                        }
                                           ;
            return input.First();
        }

        public System.IO.Stream ExecuteResponse(System.IO.Stream stream, object inputparameters)
        {
            var para = (InputParameters)inputparameters;
            try
            {
                string inputjsonstring = StreamToString(stream);
                
                string key1 = "";
                string key2 = "";
                string resmessage = "";
                string resstatus = "";
                if (para.functionname == "MM-I-003")
                {

                    SaveRequestData(inputjsonstring, para.saverequestdata, "req_MM-I-003");
                    dynamic input = JObject.Parse(inputjsonstring);
                    key1 = input.header.docno.Value;
                    key2 = input.header.ebeln.Value;
                    ZNJHK_IF_WMS_MM003 client = new ZNJHK_IF_WMS_MM003(para.url);
                    client.Credentials = new System.Net.NetworkCredential(para.username, para.password);
                    ZNJHK_IF_WMS_MM0031 request = new ZNJHK_IF_WMS_MM0031();
                    request.INPUT = inputjsonstring;
                    ZNJHK_IF_WMS_MM003Response response = client.CallZNJHK_IF_WMS_MM003(request);
                    dynamic output = JObject.Parse(response.OUTPUT);
                    resstatus = output.type.Value;
                    resmessage = output.message.Value;
                    if (resstatus == "E")
                    {
                        SaveErrorResponse(response.OUTPUT, para.saveerrorresponse, "res_MM-I-003");
                    }
                    else { 
                    
                    }
                    Logger.Write(key1, key2, "MM-I-003", resstatus, resmessage, para.url, "MM-I-003", para.tag, para.connectionstring);
                    

                }
                if (para.functionname == "MM-I-006")
                {

                    SaveRequestData(inputjsonstring, para.saverequestdata, "req_MM-I-006");
                    dynamic input = JObject.Parse(inputjsonstring);
                    key1 = input.header.docno.Value;
                    key2 = input.header.ebeln.Value;
                    ZNJHK_IF_WMS_MM006 client = new ZNJHK_IF_WMS_MM006(para.url);
                    client.Credentials = new System.Net.NetworkCredential(para.username, para.password);
                    ZNJHK_IF_WMS_MM0061 request = new ZNJHK_IF_WMS_MM0061();
                    request.INPUT = inputjsonstring;
                    ZNJHK_IF_WMS_MM006Response response = client.CallZNJHK_IF_WMS_MM006(request);
                    dynamic output = JObject.Parse(response.OUTPUT);
                    resstatus = output.type.Value;
                    resmessage = output.message.Value;
                    if (resstatus == "E")
                    {
                        SaveErrorResponse(response.OUTPUT, para.saveerrorresponse, "res_MM-I-006");
                    }
                    else
                    {

                    }
                    Logger.Write(key1, key2, "MM-I-006", resstatus, resmessage, para.url, "MM-I-006", para.tag, para.connectionstring);


                }
                if (para.functionname == "MM-I-008")
                {

                    SaveRequestData(inputjsonstring, para.saverequestdata, "req_MM-I-008");
                    dynamic input = JObject.Parse(inputjsonstring);
                    key1 = input.docno.Value;
                    key2 = input.budat.Value;
                    ZNJHK_IF_WMS_MM008 client = new ZNJHK_IF_WMS_MM008(para.url);
                    client.Credentials = new System.Net.NetworkCredential(para.username, para.password);
                    ZNJHK_IF_WMS_MM0081 request = new ZNJHK_IF_WMS_MM0081();
                    request.INPUT = inputjsonstring;
                    ZNJHK_IF_WMS_MM008Response response = client.CallZNJHK_IF_WMS_MM008(request);
                    dynamic output = JObject.Parse(response.OUTPUT);
                    resstatus = output.type.Value;
                    resmessage = output.message.Value;
                    if (resstatus == "E")
                    {
                        SaveErrorResponse(response.OUTPUT, para.saveerrorresponse, "res_MM-I-008");
                    }
                    else
                    {

                    }
                    Logger.Write(key1, key2, "MM-I-008", resstatus, resmessage, para.url, "MM-I-008", para.tag, para.connectionstring);


                }
                if (para.functionname == "SD-I-002")
                {

                    SaveRequestData(inputjsonstring, para.saverequestdata, "req_SD-I-002");
                    dynamic input = JObject.Parse(inputjsonstring);
                    key1 = input.header.docno.Value;
                    key2 = input.header.vbeln.Value;
                    ZNJHK_IF_WMS_SD002 client = new ZNJHK_IF_WMS_SD002(para.url);
                    client.Credentials = new System.Net.NetworkCredential(para.username, para.password);
                    ZNJHK_IF_WMS_SD0021 request = new ZNJHK_IF_WMS_SD0021();
                    request.INPUT = inputjsonstring;
                    ZNJHK_IF_WMS_SD002Response response = client.CallZNJHK_IF_WMS_SD002(request);
                    dynamic output = JObject.Parse(response.OUTPUT);
                    resstatus = output.type.Value;
                    resmessage = output.message.Value;
                    if (resstatus == "E")
                    {
                        SaveErrorResponse(response.OUTPUT, para.saveerrorresponse, "res_SD-I-002");
                    }
                    else
                    {

                    }
                    Logger.Write(key1, key2, "SD-I-002", resstatus, resmessage, para.url, "SD-I-002", para.tag, para.connectionstring);


                }

                return null;
                 
                
            }
            catch (Exception e)
            {
                //WriteLog(stream, e.Message, para);
                throw e;
            }
        }
        public   bool IsValidJsonString(string strInput)
        {
            strInput = strInput.Trim();
            if (((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) && strInput.Length >2) //For array
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       private string StreamToString(Stream stream){
           using (StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8))
           {
               return reader.ReadToEnd();
           }
       }
        
        private void SaveErrorResponse(string resjsonstring, string path, string prefix)
        {
            if (!string.IsNullOrEmpty(path) && path != "N")
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var filename = Path.Combine(path, prefix + "_" + Guid.NewGuid() + ".json");
                File.WriteAllText(filename, resjsonstring);
            }
        
        }
        
        
        private void SaveRequestData(string jsonstring, string path, string prefix)
        {
            if (!string.IsNullOrEmpty(path) && path != "N")
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                var filename = Path.Combine(path, prefix + "_" + Guid.NewGuid() + ".json");
                File.WriteAllText(filename, jsonstring);
            }
        }
    }

    public class InputParameters
    {
        public string connectionstring { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string functionname { get; set; }
      
        public string url { get; set; }
       
        public string tag { get; set; }
        
        public string saveerrorresponse { get; set; }
        public string saverequestdata { get; set; }

    }
}
