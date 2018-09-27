using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizTalk.Adapter.AssemblyExecute.Interface;
using System.Xml.Linq;
using System.IO;

namespace BizTalk.Adapter.AssemblyExecute.goertek
{
    public class goertekb2bproxy : IAssemblyExecute
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
                XDocument requestxml = XDocument.Load(stream);

                string key1 = "";
                string key2 = "";
                string resmessage = "";
                string resstatus = "";
                if (para.functionname == "receiveAdvice")
                {
                    //保存报文
                    SaveRequestData(requestxml, para.saverequestdata, "receiveAdvice");
                    //解析XMLto Class
                    var rows = from el in requestxml.Descendants("record")
                        select new 
                        {
                            SupplierCode = el.Element("SupplierCode").Value,
                            SenderId = el.Element("SenderId").Value,
                            DocumentId = el.Element("DocumentId").Value,
                            CustomerItemCode = el.Element("CustomerItemCode").Value
                            //.....
                        };
                        
                    //获取日志需要key
                    key1 = rows.First().DocumentId;
                    key2 = rows.First().SupplierCode;
                    //赋值生成request 对象
                    BizTalk.Adapter.AssemblyExecute.goertek.proxy.receiveAdvice.receiveAdviceReq req=new proxy.receiveAdvice.receiveAdviceReq();
                    req.ReceiveAdvice = new proxy.receiveAdvice.ReceiveAdvice();
                    req.ReceiveAdvice.Head = new proxy.receiveAdvice.Head();
                    req.ReceiveAdvice.Head.DocumentId = rows.First().DocumentId;
                    req.ReceiveAdvice.Head.SenderId = rows.First().SenderId;
                    //.....
                    var detaillist=new List<BizTalk.Adapter.AssemblyExecute.goertek.proxy.receiveAdvice.Detail>();
                    foreach (var d in rows)
                    {
                        BizTalk.Adapter.AssemblyExecute.goertek.proxy.receiveAdvice.Detail detail = new proxy.receiveAdvice.Detail();
                        detail.SupplierItemCode = d.SupplierCode;
                        //.....
                        detaillist.Add(detail);
                    }
                    req.ReceiveAdvice.Head.Detail = detaillist.ToArray();
                    BizTalk.Adapter.AssemblyExecute.goertek.proxy.receiveAdvice.Gtk_CuVmicommonwsproviderreceiveAdvicereceiveAdviceProvider client = new BizTalk.Adapter.AssemblyExecute.goertek.proxy.receiveAdvice.Gtk_CuVmicommonwsproviderreceiveAdvicereceiveAdviceProvider(para.url);
                    BizTalk.Adapter.AssemblyExecute.goertek.proxy.receiveAdvice.receiveAdviceRes res= client.receiveAdvice(req);
                    resstatus = res.Response.Code;
                    resmessage = res.Response.Message;
                    if (resstatus == "1")
                    {
                        //SaveErrorResponse(response.OUTPUT, para.saveerrorresponse, "res_MM-I-003");
                    }
                    else
                    {

                    }
                    Logger.Write(key1, key2, "receiveAdvice", resstatus, resmessage, para.url, "receiveAdvice", para.tag, para.connectionstring);


                }
                 

                return null;


            }
            catch (Exception e)
            {
                //WriteLog(stream, e.Message, para);
                throw e;
            }
        }




        private void SaveRequestData(XDocument doc, string path, string prefix)
        {
            if (!string.IsNullOrEmpty(path) && path != "N")
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                var filename = Path.Combine(path, prefix + "_" + Guid.NewGuid() + ".xml");
                doc.Save(filename);
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
