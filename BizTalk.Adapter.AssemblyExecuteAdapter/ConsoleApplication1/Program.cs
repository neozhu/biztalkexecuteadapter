using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Xml;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument doc = XDocument.Parse(@"<AssemblyExecuteAdapter>
  <InParameters>
    <ParameterName1>val1</ParameterName1>
    <ParameterName2>val2</ParameterName2>
    <ParameterName3>val3</ParameterName3>
  </InParameters>
</AssemblyExecuteAdapter>");

            var q = from el in doc.Descendants("InParameters")
                    select new { p1 = el.Element("ParameterName1").Value,
                                 p2 = el.Element("ParameterName2").Value,
                    p3 = el.Element("ParameterName3").Value
                   };
            var item = q.First();
            Console.Write(q);



            string js = "{\"REQUESTDATA\":{\"INTERFACEID\":\"INT_PI172\",\"TRANSID\":\"\",\"MESSAGEID\":\"\",\"SENDER\":\"HUASONG\",\"RECEIVER\":\"ERP\",\"ITEM\":{\"HUB\":\"\",\"CITY\":\"\",\"RKDHH\":\"\",\"RKDHI\":\"\",\"RTYPE\":\"\",\"SHKZG\":\"\",\"MATNR\":\"\",\"MENGE\":\"\",\"ERFME\":\"EA\",\"KUNNR\":\"\",\"NAME1\":\"\",\"CPUDT\":\"20170803\",\"CPUTM\":\"152123\",\"BUDAT\":\"20170803\",\"BUDTM\":\"152203\",\"LGORT\":\"\",\"ZFPHM\":\"\",\"ZITEM\":\"BOE\u00BF\u00C9\u00D2\u00D4\u00BD\u00D3\u00CA\u00DC\u00BF\u00D5\u00D6\u00B5\",\"KDMAT\":\"\",\"BZXX\":\"\u00C7\u00EB\u00BF\u00D8\u00D6\u00C6\u00D4\u00DA40\u00B8\u00F6\u00D7\u00D6\u00B7\u00FB\u00D2\u00D4\u00C4\u00DA\",\"YLZD1\":\"\",\"YLZD2\":\"\",\"YLZD3\":\"\",\"YLZD4\":\"\",\"YLZD5\":\"\",\"RMAN\":\"\",\"ERDAT\":\"20170803\",\"UZEIT\":\"150723\",\"VBELN\":\"\",\"POSNR\":\"\"}}}";
            var obj = JsonConvert.DeserializeXmlNode(js, "MT_BOE_HUBTOECC_GR");

            var pdoc = new XmlDocument();
            pdoc.LoadXml(@"<AssemblyExecuteAdapter>
  <InParameters>
    <ak>94717a0403804b039834a7cab929d4a4</ak>
    <sk>zJke7U3yGMG7kGQ9SfxfU/hzzBg=</sk>
    <url>https://api-csb-broker.boe.com.cn:443/test</url>
    <proxyurl>http://172.20.70.87:8080/axis2/services/JAxis2Tunnel.JAxis2TunnelHttpSoap11Endpoint/</proxyurl>
    <apiname>MT_BOE_HUBTOECC_GR</apiname>
    <functionname>MT_BOE_HUBTOECC_GR</functionname>
    <version>1.0.0</version>
    <method>post</method>
  </InParameters>
</AssemblyExecuteAdapter>");

            var stream=new System.IO.MemoryStream();
            obj.Save(stream);
           
            BizTalk.Adapter.AssemblyExecute.aliyuncsb.CSBSDK d = new BizTalk.Adapter.AssemblyExecute.aliyuncsb.CSBSDK();
            var dd=d.GetInputParameter(pdoc);
            d.ExecuteResponse(stream ,dd);
            var xd = new XmlDocument();
            xd.Load(@"d:\test\in\Untitled2.xml");
            var jss = JsonConvert.SerializeXmlNode(xd, Newtonsoft.Json.Formatting.None, true);
            Console.Write(obj.OuterXml);

        }
    }
}
