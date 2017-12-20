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

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
          

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
//</AssemblyExecuteAdapter>");
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
            var xd = STOCKOUTTest();
            var jss = JsonConvert.SerializeXmlNode(xd, Newtonsoft.Json.Formatting.None, false);
            Console.Write(jss);

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
                                <apiname>boe_hubtoecc_stockout</apiname>
                                <functionname>MT_BOE_HUBTOECC_StockOut1</functionname>
                                <version>1.0.0</version>
                                <method>post</method>
                                <tag>BOE</tag>
                                <connectionstring>Data Source=UNTESTDB;User Id=edib2b;Password=edib2b;</connectionstring>
                                <key1xpath>YKDHH</key1xpath>
                                <key2xpath>RECEIVER</key2xpath>
                                <key3xpath>INTERFACEID</key3xpath>
                                <res1xpath>code</res1xpath>
                                <res2xpath>text</res2xpath>
                                 <saveerrorresponse>d:\</saveerrorresponse>
                                </InParameters>
                           </AssemblyExecuteAdapter>");

            var xd = new XmlDocument();
            xd.Load(@"D:\\boe\\MT_BOE_HUBTOECC_STOCKOUT_SO.XML");

            var stream = new System.IO.MemoryStream();
            xd.Save(stream);

            BizTalk.Adapter.AssemblyExecute.aliyuncsb.CSBSDK d = new BizTalk.Adapter.AssemblyExecute.aliyuncsb.CSBSDK();
            var dd = d.GetInputParameter(pdoc);
            var res = d.ExecuteResponse(stream, dd);
            Console.ReadLine();
            return xd;
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
    }
}
