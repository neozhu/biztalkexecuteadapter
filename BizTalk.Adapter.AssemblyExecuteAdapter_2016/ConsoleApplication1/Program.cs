using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Xml;
using BizTalk.Adapter.AssemblyExecute.httpsoappost;
using BizTalk.Adapter.AssemblyExecute.aliyuncsb;
using BizTalk.Adapter.AssemblyExecute.httppost;
using System.IO;
using BizTalk.Adapter.AssemblyExecute.Interface;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Type assemblyExecuteType = Type.GetType("BizTalk.Adapter.AssemblyExecute.szhjyy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            //IAssemblyExecute assemblyexecute = (IAssemblyExecute)Activator.CreateInstance(assemblyExecuteType);

            //            var doc1 =   XDocument.Parse(@"<AssemblyExecuteAdapter>
            //  <InParameters>
            //    <connectionstring>Data Source=UNTESTDB;User Id=edib2b;Password=edib2b;</connectionstring>
            //    <url>http://113.204.199.20:8081/slc/SysWMSLotStatusToMES.ashx</url>
            //    <soapaction>LOT_STATUS</soapaction>
            //    <tag>compalwh</tag>
            //    <key1xpath>LOT_NO</key1xpath>
            //    <key2xpath>LINE</key2xpath>
            //    <key3xpath>STATUS</key3xpath>
            //    <res1xpath>PROCESS_STATUS</res1xpath>
            //    <res2xpath>REMARK</res2xpath>
            //  </InParameters>
            //  </AssemblyExecuteAdapter>");
            //            var doc2 = XDocument.Load("D:\\Map1_output.xml");
            //            var stream1 = new System.IO.MemoryStream();
            //            doc2.Save(stream1);
            //            var c1 = new HttpExecute();
            //            var p = c1.GetInputParameter(doc1);
            //            var r = c1.ExecuteResponse(stream1, p);



            //            XDocument doc = XDocument.Load("D:\\B2BPlatForm\\CompalWH\\WMSID_0000012534.xml");

            //            var q = from el in doc.Descendants()
            //                    where el.Name.LocalName == "EBELN"
            //                    select el;
            //            var item = q.First().Value;
            //            Console.Write(q);



            //            string js = "{\"REQUESTDATA\":{\"INTERFACEID\":\"INT_PI172\",\"TRANSID\":\"\",\"MESSAGEID\":\"\",\"SENDER\":\"HUASONG\",\"RECEIVER\":\"ERP\",\"ITEM\":{\"HUB\":\"\",\"CITY\":\"\",\"RKDHH\":\"\",\"RKDHI\":\"\",\"RTYPE\":\"\",\"SHKZG\":\"\",\"MATNR\":\"\",\"MENGE\":\"\",\"ERFME\":\"EA\",\"KUNNR\":\"\",\"NAME1\":\"\",\"CPUDT\":\"20170803\",\"CPUTM\":\"152123\",\"BUDAT\":\"20170803\",\"BUDTM\":\"152203\",\"LGORT\":\"\",\"ZFPHM\":\"\",\"ZITEM\":\"BOE\u00BF\u00C9\u00D2\u00D4\u00BD\u00D3\u00CA\u00DC\u00BF\u00D5\u00D6\u00B5\",\"KDMAT\":\"\",\"BZXX\":\"\u00C7\u00EB\u00BF\u00D8\u00D6\u00C6\u00D4\u00DA40\u00B8\u00F6\u00D7\u00D6\u00B7\u00FB\u00D2\u00D4\u00C4\u00DA\",\"YLZD1\":\"\",\"YLZD2\":\"\",\"YLZD3\":\"\",\"YLZD4\":\"\",\"YLZD5\":\"\",\"RMAN\":\"\",\"ERDAT\":\"20170803\",\"UZEIT\":\"150723\",\"VBELN\":\"\",\"POSNR\":\"\"}}}";
            //            var obj = JsonConvert.DeserializeXmlNode(js, "MT_BOE_HUBTOECC_GR");

            //var xd = GRTest();
            //var xd = STOCKOUTTest();
            //var jss = JsonConvert.SerializeXmlNode(xd, Newtonsoft.Json.Formatting.None, false);
            //Console.Write(jss);
            // DBTest();
            //ncpdTest();


            test1001("1003");
        }
        private static void test1001(string funName) {
            var pdoc = new XmlDocument();
            string xmlStr = string.Format(@"<AssemblyExecuteAdapter>
<InParameters>
    <connectionstring>Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=compdb.feili.com)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=feiliextsrv)));Persist Security Info=True;User ID=feiliext;Password=sap#ext2016;</connectionstring>
    <user>FEILIKS</user>
    <password>FLDmnsped</password>
    <url>http://www.jyy56.cn:8589/api</url>
    <functionname>{0}</functionname>
    <saverespath>D:\B2BPlatform\szhjyy\1004\resjson</saverespath>
    <acct>FEILIKS</acct>
    <tag>szhjyy</tag>
    <path>D:\B2BPlatform\szhjyy\1005</path>
    <oaserviceurl>http://172.20.70.218:8088/service/ecologyservice.asmx</oaserviceurl>
  </InParameters>
</AssemblyExecuteAdapter>", funName);




            pdoc.LoadXml(xmlStr);            
            var stream = new System.IO.MemoryStream();
            var xd = new XmlDocument();
            xd.Load(@"D:\\1.xml");
            xd.Save(stream);

            BizTalk.Adapter.AssemblyExecute.szhjyy.zshHttpExecute d = new BizTalk.Adapter.AssemblyExecute.szhjyy.zshHttpExecute();
            var dd = d.GetInputParameter(pdoc);
            stream.Seek(0, SeekOrigin.Begin);
            var res = d.ExecuteResponse(stream, dd);
            Console.ReadLine();
            

        }



        private static XmlDocument STOCKOUTTest()
        {
            var pdoc = new XmlDocument();
            pdoc.LoadXml(@"<AssemblyExecuteAdapter>
                                <InParameters>
    <ak>94717a0403804b039834a7cab929d4a4</ak>
    <sk>zJke7U3yGMG7kGQ9SfxfU/hzzBg=</sk>
    <url>https://api-csb-broker.boe.com.cn:443/test</url>
    <proxyurl>http://172.20.70.87:8080/axis2/services/JAxis2Tunnel.JAxis2TunnelHttpSoap11Endpoint/</proxyurl>
    <apiname>boe_hubtoecc_stock</apiname>
    <functionname>MT_BOE_HUBTOECC_Stock</functionname>
    <version>1.0.0</version>
    <method>post</method>
    <tag>BOE</tag>
    <connectionstring>Data Source=UNTESTDB;User Id=edib2b;Password=edib2b;</connectionstring>
    <key1xpath>RKDHH</key1xpath>
    <key2xpath>RKDHI</key2xpath>
    <key3xpath>INTERFACEID</key3xpath>
    <res1xpath>code</res1xpath>
    <res2xpath>text</res2xpath>
    <saveerrorresponse>D:\B2BPlatForm\BOE\ErrorBackup\</saveerrorresponse>
  </InParameters>
                           </AssemblyExecuteAdapter>");

            var xd = new XmlDocument();
            xd.Load(@"D:\\3.xml");

            var stream = new System.IO.MemoryStream();
            xd.Save(stream);

            BizTalk.Adapter.AssemblyExecute.aliyuncsb.CSBSDK d = new BizTalk.Adapter.AssemblyExecute.aliyuncsb.CSBSDK();
            var dd = d.GetInputParameter(pdoc);
            var res = d.ExecuteResponse(stream, dd);
            Console.ReadLine();
            return xd;
        }
        private static void ncpdTest()
        {
            var pdoc = new XmlDocument();
            pdoc.LoadXml(@"<AssemblyExecuteAdapter>
  <InParameters>
    <connectionstring>Data Source=UNTESTDB;User Id=edib2b;Password=edib2b;</connectionstring>
    <url>http://58.240.32.198:8000/sap/bc/srt/rfc/sap/znjhk_if_wms_mm003/310/znjhk_if_wms_mm003/if_wms_mm003</url>
    <username>ITFUSER</username>
    <password>panda001</password>
    <apiname>boe_hubtoecc_gr_test</apiname>
    <functionname>MM-I-003</functionname>
    <tag>NCPD</tag>
    <saveerrorresponse>D:\B2BPlatForm\NCPD\ErrorBackup\</saveerrorresponse>
    <saverequestdata>D:\B2BPlatForm\NCPD\RequestData\</saverequestdata>
  </InParameters>
</AssemblyExecuteAdapter>");

           // var xd = new XmlDocument();
           // xd.Load(@"D:\\3.xml");

            //var stream = new System.IO.MemoryStream();
          //  xd.Save(stream);
            FileStream openFile = new FileStream(@"D:\B2BPlatForm\ncpd\OutToMMI003\mm-003.json", FileMode.Open);
            byte[] bytes = new byte[openFile.Length];
            openFile.Read(bytes, 0, bytes.Length);
            openFile.Close();
            Stream stream = new MemoryStream(bytes);   
           
            BizTalk.Adapter.AssemblyExecute.aliyuncsb.pandaclienthelper d = new BizTalk.Adapter.AssemblyExecute.aliyuncsb.pandaclienthelper();
            var dd = d.GetInputParameter(pdoc);
            var res = d.ExecuteResponse(stream, dd);
            Console.ReadLine();
            
        }
        private static XmlDocument GRTest()
        {
            var pdoc = new XmlDocument();
            pdoc.LoadXml(@"<AssemblyExecuteAdapter>
                                <InParameters>
                                <ak>94717a0403804b039834a7cab929d4a4</ak>
                                <sk>zJke7U3yGMG7kGQ9SfxfU/hzzBg=</sk>
                                <url>https://api-csb-broker.boe.com.cn:443/test</url>
                                <proxyurl>http://172.20.70.87:8080/axis2/services/JAxis2Tunnel.JAxis2TunnelHttpSoap11Endpoint/</proxyurl>
                                <apiname>boe_hubtoecc_gr</apiname>
                                <functionname>MT_BOE_HUBTOECC_GR</functionname>
                                <version>1.0.0</version>
                                <method>post</method>
                                <tag>csb</tag>
                                <connectionstring>Data Source=UNTESTDB;User Id=edib2b;Password=edib2b;</connectionstring>
                                <key1xpath>INTERFACEID</key1xpath>
                                <key2xpath>RECEIVER</key2xpath>
                                <key3xpath>RKDHH</key3xpath>
                                <res1xpath>code</res1xpath>
                                <res2xpath>message</res2xpath>
                                </InParameters>
                           </AssemblyExecuteAdapter>");

            var xd = new XmlDocument();
            xd.Load(@"D:\\GR_FL11IN0000069775.XML");

            var stream = new System.IO.MemoryStream();
            xd.Save(stream);

            BizTalk.Adapter.AssemblyExecute.aliyuncsb.CSBSDK d = new BizTalk.Adapter.AssemblyExecute.aliyuncsb.CSBSDK();
            var dd = d.GetInputParameter(pdoc);
            var res = d.ExecuteResponse(stream, dd);
            Console.ReadLine();
            return xd;
        }

        private static XmlDocument DBTest()
        {
            var pdoc = new XmlDocument();
            pdoc.LoadXml(@"<AssemblyExecuteAdapter>
  <InParameters>
    <connectionstring>Data Source=UNTESTDB;User Id=edib2b;Password=edib2b;</connectionstring>
    <url>http://113.204.199.20:8081/slc/SysWMSLotStatusToMES.ashx</url>
    <soapaction>LOT_STATUS</soapaction>
    <tag>CompalWH</tag>
    <key1xpath>LOT_NO</key1xpath>
    <key2xpath>
    </key2xpath>
    <key3xpath>STATUS</key3xpath>
    <res1xpath>PROCESS_STATUS</res1xpath>
    <res2xpath>REMARK</res2xpath>
  </InParameters>
</AssemblyExecuteAdapter>");

            var xd = new XmlDocument();
            xd.Load(@"D:\\req123.txt");

            var stream = new System.IO.MemoryStream();
            xd.Save(stream);

            BizTalk.Adapter.AssemblyExecute.httppost.HttpExecute d = new BizTalk.Adapter.AssemblyExecute.httppost.HttpExecute();
            var dd = d.GetInputParameter(pdoc);
            var res = d.ExecuteResponse(stream, dd);
            Console.ReadLine();
            return xd;
        }

    }
}
