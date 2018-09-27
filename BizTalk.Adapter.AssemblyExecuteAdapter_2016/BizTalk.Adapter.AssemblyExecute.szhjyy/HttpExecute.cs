using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BizTalk.Adapter.AssemblyExecute.Interface;
using System.Xml.Linq;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Reflection;
using BizTalk.Adapter.AssemblyExecute.szhjyy.FeiLi.OA.Service;

namespace BizTalk.Adapter.AssemblyExecute.szhjyy
{
    public class zshHttpExecute : IAssemblyExecute
    {


        public object GetInputParameter(System.Xml.XmlDocument xmldoc)
        {
            XDocument doc = XDocument.Parse(xmldoc.OuterXml);
            var input = from el in doc.Descendants("InParameters")
                        select new InputParameters
                        {
                            connectionstring = el.Element("connectionstring").Value,
                            user = el.Element("user").Value,
                            password = el.Element("password").Value,
                            functionname = el.Element("functionname").Value,
                            saverespath = el.Element("saverespath").Value,
                            url = el.Element("url").Value,
                            acct = el.Element("acct").Value,
                            tag = el.Element("tag").Value,
                            path = el.Element("path").Value,
                            oaserviceurl = el.Element("oaserviceurl").Value
                        };
            return input.First();
        }

        public Stream ExecuteResponse(Stream stream, object inputparameters)
        {
            var para = (InputParameters)inputparameters;
            try
            {
                var xdoc = XDocument.Load(stream);
                var ns = xdoc.Root.Name.Namespace;               
                if (para.functionname == "1001")
                {
                    #region  1001
                    List<SZH_TRQROT> trqrotList = new List<SZH_TRQROT>();
                    List<SZH_TRQTD> trqtdList = new List<SZH_TRQTD>();
                    List<SZH_TRQTDFU> trqtdfuList = new List<SZH_TRQTDFU>();

                    foreach (var item in xdoc.Descendants(ns + "INPUT"))
                    {
                        SZH_TRQROT trqrot = new SZH_TRQROT();
                        trqrot.TRQ_ID = item.Element(ns + "TRQ_ID").ElementValueNull();
                        if (string.IsNullOrEmpty(trqrot.TRQ_ID))
                        {
                            continue;
                        }
                        trqrot.TRQ_TYPE = item.Element(ns + "TRQ_TYPE").ElementValueNull();
                        trqrot.ZCMHC = item.Element(ns + "ZCMHC").ElementValueNull();
                        trqrot.ZHCHC = item.Element(ns + "ZHCHC").ElementValueNull();
                        trqrot.ZJHSHRQ = item.Element(ns + "ZJHSHRQ").ToDateOrNull();   //datetime
                        trqrot.ZJDR = item.Element(ns + "ZJDR").ElementValueNull();
                        trqrot.ZSMZQ = item.Element(ns + "ZSMZQ").ElementValueNull();
                        trqrot.ZJHKBSJ_TM = item.Element(ns + "ZJHKBSJ").ToDateOrNull();    //datetime
                        trqrot.ZZYGQHC_TM = item.Element(ns + "ZMT").ElementValueNull();
                        trqrot.ZZZGHC_TM = item.Element(ns + "ZXHG").ElementValueNull();
                        trqrot.Receive();
                        trqrotList.Add(trqrot);

                        foreach (var item1 in item.Elements(ns + "TD"))
                        {
                            SZH_TRQTD trqtd = new SZH_TRQTD();
                            trqtd.TRQ_ID = trqrot.TRQ_ID;
                            trqtd.ZSBTDH = item1.Element(ns + "ZSBTDH").ElementValueNull();
                            if (string.IsNullOrEmpty(trqtd.ZSBTDH))
                            {
                                continue;
                            }
                            trqtd.ZTDJS = item1.Element(ns + "ZJGJS").ElementToIntOrNull();
                            trqtd.ZTDMZ = item1.Element(ns + "ZJGMZ").ElementToDecimalOrNull();
                            trqtd.ZTDTJ = item1.Element(ns + "ZJGTJ").ElementToDecimalOrNull();
                            trqtd.Receive();
                            trqtdList.Add(trqtd);

                            foreach (var item2 in item1.Elements(ns + "TD_FU"))
                            {
                                SZH_TRQTDFU trqtdfu = new SZH_TRQTDFU();
                                trqtdfu.TRQ_ID = trqrot.TRQ_ID;
                                trqtdfu.ZSBTDH = trqtd.ZSBTDH;
                                trqtdfu.TURES_ID = item2.Element(ns + "TURES_ID").ElementValueNull();
                                if (string.IsNullOrEmpty(trqtdfu.TURES_ID))
                                {
                                    continue;
                                }
                                trqtdfu.ZMTFXBJ_TM = item2.Element(ns + "ZMTFXBJ").ElementValueNull();
                                trqtdfu.ZFH1_TM = item2.Element(ns + "ZFH1").ElementValueNull();
                                trqtdfu.Receive();
                                trqtdfuList.Add(trqtdfu);
                            }
                        }
                    }
                    if (trqrotList.Count == 0)
                    {
                        throw new Exception("输入参数错误！无数据！");
                    }
                    var trqrotFirst = trqrotList[0];

                    db.save1001(trqrotFirst, trqtdList, trqtdfuList, para.connectionstring);
                    var jck = trqrotFirst.TRQ_TYPE.StartsWith("OE") ? "E" : trqrotFirst.TRQ_TYPE.StartsWith("OI") ? "I" : "";

                    var jssArr = from r in trqtdList
                                 select new
                                 {
                                     QQLX = 1,          //查询类型  1：提单 2:箱单
                                     KHID = 2,          //公司的编号
                                     KHJC = "飞力达",   //公司的简称,由平台提供
                                     TDH = r.ZSBTDH,   //查询的提单号                                      
                                     JCK = jck         //进出口的标识  I:进口 E:出口
                                 };

                    var jsstring = Newtonsoft.Json.JsonConvert.SerializeObject(jssArr);
                    para.url = para.url + "/hb_ask/PostObjects?strdata=" + jsstring;
                    var s = HttpClient.SendRequest(para.url, para.user, para.password);
                    var jyymsg = StreamToString(s);
                    SaveRequestData(jyymsg, para.saverespath, "1001");
                    db.updatejyymsg1001(trqrotFirst.TRQ_ID, jyymsg, para.connectionstring);
                    #endregion
                }
                else if (para.functionname == "1003")
                {
                    #region 1003
                    para.url = para.url + "/hb_container/GetMyCargos?acct=" + para.acct;
                    var s = HttpClient.SendRequest(para.url, para.user, para.password);
                    var str = StreamToString(s);                
                    SaveRequestData(str, para.saverespath, "1003");
                    var tttt = JObject.Parse(str);
                    JArray jobj = tttt["Result"] as JArray;
                    List<SZH_TRQITM> trqitmList = new List<SZH_TRQITM>();
                    List<SZH_TRQTDFU> trqtdfuList = new List<SZH_TRQTDFU>();
                    List<SZH_TRQTD> trqtdList = new List<SZH_TRQTD>();
                    List<SZH_TRQROT> trqrotList = new List<SZH_TRQROT>();
                    List<string> trqtdIdList = new List<string>();
                    foreach (var item in jobj)
                    {
                        SZH_TRQITM trqitm = new SZH_TRQITM();
                        trqitm.TURES_ID = item["XH"].ToString();
                        var cxcdArr = item["CXCD"].ToString().Split('/');
                        if (cxcdArr != null && cxcdArr.Length > 3)
                        {
                            trqitm.ZQCC = cxcdArr[0].ToDecimalOrNull();
                            trqitm.ZHCC = cxcdArr[1].ToDecimalOrNull();
                            trqitm.ZZCK = cxcdArr[2].ToDecimalOrNull();
                            trqitm.ZYCK = cxcdArr[3].ToDecimalOrNull();
                        }
                       
                        trqitm.ZWXPDJ = item["WXPDJ"].ToString();
                        trqitm.ZLHGBH = item["LHGBH"].ToString();
                        trqitm.ZJGRQSJ1 = item["JGSJ"].ToDateOrNull();
                        trqitm.ZCCRQSJ = item["CGSJ"].ToDateOrNull();
                        trqitm.ZLCWD = item["LCXWD"].ToString();
                        if (item["JCFS"] != null && item["JCFS"].ToString() != "")
                        {
                            trqitm.ZXXSJ = DateTime.Now;
                        }
                       
                        SZH_TRQTD trqtd = new SZH_TRQTD();
                        trqtd.ZSBTDH = item["TDH"].ToString();
                        if (string.IsNullOrEmpty(trqtd.ZSBTDH))
                        {
                            continue;
                        }
                                              
                        SZH_TRQROT trqrot = new SZH_TRQROT();
                        trqrot.ZXHGQ = item["MT"].ToString();
                        trqrot.ZCGS = item["CXR"].ToString();
                        trqrot.ZZZGHC = item["XHGEDI"].ToString();
                        trqrot.ZMDGHC = item["MDGEDI"].ToString();
                        if (item["CCFS"] != null && item["CCFS"].ToString() == "疏运")
                        {
                            trqrot.ZSYSJ  = DateTime.Now;
                            trqitm.SYBZ = "X";
                        }
                        else
                        {
                            trqitm.SYBZ = "";
                        }
                        if (item["CCFS"] != null && item["CCFS"].ToString() == "提进口重箱")
                        {                          
                            trqitm.ZTZXRQSJ = DateTime.Now;
                        }
                        trqrot.UPDATETIME = DateTime.Now;
                        trqrotList.Add(trqrot);
                        trqitmList.Add(trqitm);

                        if (!trqtdIdList.Contains(trqtd.ZSBTDH))
                        {
                            trqtdIdList.Add(trqtd.ZSBTDH);
                        }
                        
                        trqtd.ZEF = item["HGFGBZ"].ToString();
                        trqtd.ZFG = item["HGFG"].ToString();
                        trqtd.ZEFSJ = item["HGBWFSSJ"].ToDateOrNull();
                        trqtd.UPDATETIME = DateTime.Now;
                        trqtdList.Add(trqtd);                                         

                        SZH_TRQTDFU trqtdfu = new SZH_TRQTDFU();
                        trqtdfu.ZSBTDH = item["TDH"].ToString();
                        trqtdfu.TURES_ID = item["XH"].ToString();
                        trqtdfu.ZCXR = item["CXR"].ToString();
                        trqtdfu.ZFH1 = item["QFH"].ToString();
                        trqtdfu.ZYDBJ = item["YDBWFSSJ"].ToString();
                                              
                        trqtdfu.ZMTFXBJ = item["MTFXBZ"].ToString();
                        if (trqtdfu.ZMTFXBJ == "Y")
                        {
                            trqtdfu.ZFXSJ = DateTime.Now;
                        }
                        trqtdfu.ZPZBJ = item["PZBZ"].ToString();
                        if (trqtdfu.ZPZBJ == "Y")
                        {
                            trqtdfu.ZPZSJ = DateTime.Now;
                        }
                        trqtdfu.ZJGJS = item["FJS"].ToDecimalOrNull();
                        trqtdfu.ZJGMZ = item["FZL"].ToDecimalOrNull();
                        trqtdfu.ZJGTJ = item["FTJ"].ToDecimalOrNull();
                        trqtdfu.UPDATETIME = DateTime.Now;
                        trqtdfuList.Add(trqtdfu);
                        
                    }
                    if (trqtdIdList.Count > 0)
                    {
                        var inSql = SqlBuilder.ListToInSql(trqtdIdList);
                        db.save1003(trqrotList, trqtdList, trqtdfuList, trqitmList, inSql, para.connectionstring);
                        saveTMXMLBySql(inSql, para.connectionstring, para.path,para.oaserviceurl);                       
                    }
                    #endregion
                }
                else if (para.functionname == "1004")
                {
                    #region 1004
                    string sqlStr = @"SELECT A.*
  FROM SZH_TRQROT A
 WHERE A.ZSMZQ != '10'
   AND A.ZCMHC IS NOT NULL
   AND A.ZHCHC IS NOT NULL
   AND ((A.TRQ_TYPE like 'OI%' AND A.RECEIVETIME < (SYSDATE + 60) AND
       (A.ZJHSHRQ IS NULL OR A.ZJHSHRQ > (SYSDATE - 20))) OR
       (A.TRQ_TYPE like 'OE%' AND A.RECEIVETIME < (SYSDATE + 180)))";
                    var dt = db.GetDataSet(sqlStr, para.connectionstring);
                    string importUrl = para.url + "/hb_schedule/GetVesselImport?vessel={0}&voyage={1}";
                    string exportUrl = para.url + "/hb_schedule/GetVesselExport?vessel={0}&voyage={1}";
                    List<string> sqlList = new List<string>();
                    List<string> trqrotIdList = new List<string>();
                    List<SZH_TRQROT> trqrotToOAList = new List<SZH_TRQROT>();
                                       
                    foreach (DataRow row in dt.Rows)
                    {
                        string url = "";
                        string trq_type = row["TRQ_TYPE"].ToString();
                        if (trq_type.StartsWith("OI"))
                        {
                            url = string.Format(importUrl, row["ZCMHC"], row["ZHCHC"]);
                        }
                        else if (trq_type.StartsWith("OE"))
                        {
                            url = string.Format(exportUrl, row["ZCMHC"], row["ZHCHC"]);
                        }
                        else
                        {
                            continue;
                        }
                        var s = HttpClient.SendRequest(url, para.user, para.password);
                        var str = StreamToString(s);
                        //string filePath = @"C:\Users\biztalk\Desktop\err\1004_ed18693b-e20b-4bdd-a7c9-98747e814578.xml";
                        //var  str = File.ReadAllText(filePath);
                        SaveRequestData(str, para.saverespath, "1004");
                        var tttt = JObject.Parse(str);
                        JArray jarr = tttt["Result"] as JArray;

                        if (jarr.Count > 0)
                        {
                            int lastUpdateIndex = 0;
                            DateTime maxDateTime = new DateTime();
                            for (var n = 0; n < jarr.Count; ++n)
                            {
                                var lastUpdateDate = jarr[n]["LASTUPDATE"].ToDateOrNull();
                                if (lastUpdateDate.HasValue)
                                {
                                    var temp = (DateTime)lastUpdateDate;
                                    if (n == 0 || temp > maxDateTime)
                                    {
                                        maxDateTime = temp;
                                        lastUpdateIndex = n;
                                    }
                                }
                            }

                            var item = jarr[lastUpdateIndex];                            
                            SZH_TRQROT trqrot = new SZH_TRQROT();
                            trqrot.TRQ_ID = row["TRQ_ID"].ToStringOrNull();
                            trqrot.ZSMZQ = row["ZSMZQ"].ToStringOrNull();
                            trqrot.ZJHKBSJ_TM = row["ZJHKBSJ_TM"].ToDateOrNull();                            
                            trqrot.ZZYGQHC_TM = row["ZZYGQHC_TM"].ToStringOrNull();
                            trqrot.ZJDR = row["ZJDR"].ToStringOrNull();
                            trqrot.TRQ_TYPE = row["TRQ_TYPE"].ToStringOrNull();

                            trqrot.ZKGRQSJHC = item["CKJXKSSJ"].ToDateOrNull();
                            trqrot.ZJGRQSJHC1 = item["CKJXJZSJ"].ToDateOrNull();
                            trqrot.ZJHLBSJ = item["JHLBSJ"].ToDateOrNull();
                            trqrot.ZSJKHRQ = trqrot.ZSJLBSJ = item["SJLBSJ"].ToString();
                            trqrot.ZJHKBSJ = item["JHKBSJ"].ToDateOrNull();
                            trqrot.ZSJKBSJ = trqrot.ZDGRQHJ = item["SJKBSJ"].ToDateOrNull();
                            trqrot.ZZYGQHC = item["KBMT"].ToString();
                            trqrotIdList.Add(trqrot.TRQ_ID);
                            if ((row["ZJHKBSJ"].ToDateOrNull() != trqrot.ZJHKBSJ || row["ZZYGQHC"].ToString() != trqrot.ZZYGQHC) && (trqrot.ZJHKBSJ_TM != trqrot.ZJHKBSJ || trqrot.ZZYGQHC_TM != trqrot.ZZYGQHC))
                            {
                                trqrotToOAList.Add(trqrot);
                            }
                            sqlList.Add(SqlBuilder.UpdateSql(trqrot, new List<string> { "TRQ_ID" },
                                new List<string> { "ZKGRQSJHC", "ZJGRQSJHC1", "ZJHLBSJ", "ZSJLBSJ", "ZSJKHRQ", "ZJHKBSJ", "ZSJKBSJ", "ZDGRQHJ", "ZZYGQHC" }));
                        }
                    }
                    db.ExecuteSqlList(sqlList, para.connectionstring);

                    var rotAllList = ConvertToModel<SZH_TRQROT>(dt);
                    var sendTMList = rotAllList.Where(t => trqrotIdList.Contains(t.TRQ_ID)).ToList();
                    saveTMXML(sendTMList, new List<SZH_TRQTD>(), new List<SZH_TRQTDFU>(), new List<SZH_TRQITM>(), para.path);

                    if (trqrotToOAList.Count > 0)
                    {
                        SendAlert(trqrotToOAList, new List<SZH_TRQTD>(), new List<SZH_TRQTDFU>(), para.oaserviceurl, para.connectionstring);
                    }
                    #endregion
                }
                else if (para.functionname == "debug")
                {
                    //saveTMXMLBySql("('SHFL80213700')", para.connectionstring, para.path, para.oaserviceurl);
                    //string temp11 = "select * from szh_trqrot where trq_id='00000130000000001325'";

                    //var trqrotDT = db.GetDataSet(temp11, para.connectionstring);
                    //var rotList = ConvertToModel<SZH_TRQROT>(trqrotDT);
                    //saveTMXML(rotList, new List<SZH_TRQTD>(), new List<SZH_TRQTDFU>(), new List<SZH_TRQITM>(), para.path);
                }
                return null;
            }
            catch (Exception e)
            {
                //WriteLog(stream, e.Message, para);
                throw e;
            }
        }

        public string StreamToString(Stream stream)
        {
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }


        public void saveTMXMLBySql(string inSql, string connectionString, string path,string  oaserviceurl)
        {
            string sqlStr = "SELECT * FROM SZH_TRQROT WHERE TRQ_ID IN (SELECT TRQ_ID FROM SZH_TRQTD WHERE ZSBTDH IN" + inSql + ")";
            var trqrotDT = db.GetDataSet(sqlStr, connectionString);
            sqlStr = "SELECT * FROM SZH_TRQTD WHERE ZSBTDH IN" + inSql;
            var trqtdDT = db.GetDataSet(sqlStr, connectionString);
            sqlStr = "SELECT * FROM SZH_TRQTDFU WHERE ZSBTDH IN(SELECT ZSBTDH FROM SZH_TRQTD WHERE ZSBTDH IN" + inSql + ")";
            var trqtdfuDT = db.GetDataSet(sqlStr, connectionString);
            var rotList = ConvertToModel<SZH_TRQROT>(trqrotDT);
            var rotIdList = rotList.Select(t => t.TRQ_ID).ToList();
            if (rotIdList.Count == 0)
            {
                return;
            }
            sqlStr = "SELECT* FROM SZH_TRQITM WHERE TRQ_ID IN" + SqlBuilder.ListToInSql(rotIdList);
            var itmDT = db.GetDataSet(sqlStr, connectionString);

            var tdList = ConvertToModel<SZH_TRQTD>(trqtdDT);
            var tdfuList = ConvertToModel<SZH_TRQTDFU>(trqtdfuDT);
            var itmList= ConvertToModel<SZH_TRQITM>(itmDT);
            saveTMXML(rotList, tdList, tdfuList, itmList, path);
            SendAlert(rotList, tdList, tdfuList, oaserviceurl, connectionString);    //通知OA预警
        }

        /// <summary>
        /// datatable转list
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertToModel<T>(DataTable dt)
        {
                         
                List<T> ts = new List<T>();// 定义集合
                Type type = typeof(T); // 获得此模型的类型
                string tempName = "";
                foreach (DataRow dr in dt.Rows)
                {
                    T t = Activator.CreateInstance<T>();
                    PropertyInfo[] propertys = t.GetType().GetProperties();// 获得此模型的公共属性
                    foreach (PropertyInfo pi in propertys)
                    {
                        tempName = pi.Name;
                        if (dt.Columns.Contains(tempName))
                        {
                            if (!pi.CanWrite) continue;
                            object value = dr[tempName];
                            if (value != DBNull.Value)
                            {
                                pi.SetValue(t, value, null);
                            }
                        }
                    }
                    ts.Add(t);
                }
                return ts;
           
        }

        private void SaveRequestData(string str, string path, string prefix)
        {
            if (str.Length > 30 && !string.IsNullOrEmpty(path) && path != "N")
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                var filename = Path.Combine(path, prefix + "_" + Guid.NewGuid() + ".xml");
                File.WriteAllText(filename, str);
            }
        }

        public void saveTMXML(List<SZH_TRQROT> rotList, List<SZH_TRQTD> tdList, List<SZH_TRQTDFU> tdfuList, List<SZH_TRQITM> itmList, string path)
        {
            if (rotList == null || rotList.Count == 0)
            {
                return;
            }

            foreach (var rot in rotList)
            {
                if (rot.ZSMZQ == "10")
                {
                    continue;
                }
                XDocument xDoc = new XDocument();
                XNamespace xns = "http://BizTalk.RFC.szhjyy.Z2FM_GQ_UPDATE.Schemas.INPUT.Z2FM_GQ_UPDATE";
                XElement root = new XElement(xns + "Z2FM_SZH_UPDATE");
                xDoc.Add(root);
                XElement rotElement = new XElement(xns + "IN_PUT");
                root.Add(rotElement);
                rotElement.SetElementValue(xns + "TRQ_ID", rot.TRQ_ID);
                rotElement.SetElementValue(xns + "ZKGRQSJHC", rot.ZKGRQSJHC.DateOrNullToString());
                rotElement.SetElementValue(xns + "ZJGRQSJHC1", rot.ZJGRQSJHC1.DateOrNullToString());
                rotElement.SetElementValue(xns + "ZJHTBSJ", rot.ZJHKBSJ.DateOrNullToString()); 
                rotElement.SetElementValue(xns + "ZJHLBSJ", rot.ZJHLBSJ.DateOrNullToString());
                rotElement.SetElementValue(xns + "ZSJKBSJ", rot.ZSJKBSJ.DateOrNullToString());
                if (rot.ZDGRQHJ.HasValue)
                {
                    rotElement.SetElementValue(xns + "ZDGRQHJ", rot.ZDGRQHJ.Value.ToString("yyyyMMdd"));
                    rotElement.SetElementValue(xns + "ZDGSJ", rot.ZDGRQHJ.Value.ToString("HHmmss"));
                }
                
                rotElement.SetElementValue(xns + "ZXHGQ", rot.ZXHGQ);
                rotElement.SetElementValue(xns + "ZSYSJ", rot.ZSYSJ.DateOrNullToString());
                rotElement.SetElementValue(xns + "ZCGS", rot.ZCGS);
                rotElement.SetElementValue(xns + "ZMDGHC", rot.ZMDGHC);
                rotElement.SetElementValue(xns + "ZSJLBSJ", rot.ZSJLBSJ.DateStrToString());
                var zsjkhtemp = rot.ZSJKHRQ.ToDateOrNull();
                if (zsjkhtemp.HasValue)
                {
                    rotElement.SetElementValue(xns + "ZSJKHRQ", zsjkhtemp.Value.ToString("yyyyMMdd"));
                    rotElement.SetElementValue(xns + "ZKHSJ", zsjkhtemp.Value.ToString("HHmmss"));
                }

                rotElement.SetElementValue(xns + "ZZYGQHC", rot.ZZYGQHC);
                rotElement.SetElementValue(xns + "ZZZGHC", rot.ZZZGHC);


                var rotTDList = tdList.Where(t => t.TRQ_ID == rot.TRQ_ID);
                foreach (var td in rotTDList)
                {
                    XElement tdElement = new XElement(xns + "TD");
                    rotElement.Add(tdElement);

                    tdElement.SetElementValue(xns + "ZSBTDH", td.ZSBTDH);
                    tdElement.SetElementValue(xns + "ZFG", td.ZFG);
                    tdElement.SetElementValue(xns + "ZEF", string.IsNullOrEmpty(td.ZEF) ? "" : "X");
                    tdElement.SetElementValue(xns + "ZEFSJ", td.ZEFSJ.DateOrNullToString());


                    var rotTDFUList = tdfuList.Where(t => t.TRQ_ID == td.TRQ_ID && t.ZSBTDH == td.ZSBTDH);
                    foreach (var tdfu in rotTDFUList)
                    {
                        XElement tdfuElement = new XElement(xns + "TD_FU");
                        tdElement.Add(tdfuElement);

                        tdfuElement.SetElementValue(xns + "TURES_ID", tdfu.TURES_ID);
                        tdfuElement.SetElementValue(xns + "ZMTFXBJ", tdfu.ZMTFXBJ == "Y" ? "X" : "");
                        tdfuElement.SetElementValue(xns + "ZPZSJ", tdfu.ZPZSJ.DateOrNullToString());
                        tdfuElement.SetElementValue(xns + "ZPZBJ", tdfu.ZPZBJ == "Y" ? "X" : "");
                        tdfuElement.SetElementValue(xns + "ZYDBJ", string.IsNullOrEmpty(tdfu.ZYDBJ) ? "" : "X");
                        tdfuElement.SetElementValue(xns + "ZFZL", tdfu.ZJGMZ);
                        tdfuElement.SetElementValue(xns + "ZFTJ", tdfu.ZJGTJ);
                        tdfuElement.SetElementValue(xns + "ZFJS", tdfu.ZJGJS);
                        tdfuElement.SetElementValue(xns + "ZCXR", tdfu.ZCXR);
                        tdfuElement.SetElementValue(xns + "ZFH1", tdfu.ZFH1);
                        tdfuElement.SetElementValue(xns + "ZFXSJ", tdfu.ZFXSJ.DateOrNullToString());

                    }
                }

                var rotITMList = itmList.Where(t => t.TRQ_ID == rot.TRQ_ID);
                foreach (var itm in rotITMList)
                {
                    XElement itmElement = new XElement(xns + "ITEM");
                    rotElement.Add(itmElement);

                    itmElement.SetElementValue(xns + "TURES_ID", itm.TURES_ID);
                    itmElement.SetElementValue(xns + "ZWXPDJ", itm.ZWXPDJ);
                    itmElement.SetElementValue(xns + "ZLHGBH", itm.ZLHGBH);
                    itmElement.SetElementValue(xns + "ZLCWD", itm.ZLCWD);
                    itmElement.SetElementValue(xns + "ZQCC", itm.ZQCC);
                    itmElement.SetElementValue(xns + "ZHCC", itm.ZHCC);
                    itmElement.SetElementValue(xns + "ZZCK", itm.ZZCK);
                    itmElement.SetElementValue(xns + "ZYCK", itm.ZYCK);
                    itmElement.SetElementValue(xns + "ZCG", itm.ZCG);
                    itmElement.SetElementValue(xns + "ZXXSJ", itm.ZJGRQSJ1.DateOrNullToString());
                    itmElement.SetElementValue(xns + "ZJGRQSJ1", itm.ZJGRQSJ1.DateOrNullToString());
                    itmElement.SetElementValue(xns + "ZCCRQSJ", itm.ZCCRQSJ.DateOrNullToString());
                    if (itm.SYBZ != "Y")
                    {
                        itmElement.SetElementValue(xns + "ZTZXRQSJ", itm.ZCCRQSJ.DateOrNullToString());
                    }
                    itmElement.SetElementValue(xns + "ZSYBZ", itm.SYBZ);

                }

                //保存xml文件

                xDoc.Save(path + "\\" + Guid.NewGuid().ToString() + ".xml");
            }


        }

        
        /// <summary>
        /// 发送预警
        /// </summary>
        /// <param name="alertEntity"></param>
        public void SendAlert(List<SZH_TRQROT> rotList, List<SZH_TRQTD> tdList, List<SZH_TRQTDFU> tdfuList, string oaserviceurl,string connectionString)
        {
            if (string.IsNullOrEmpty(oaserviceurl) || oaserviceurl == "N")
            {
                return;
            }           
            List<AlertVO> alertList = new List<AlertVO>();
            List<string> sqlList = new List<string>();  //ZJHKBSJ   更新到  ZJHKBSJ_TM  ；ZMTFXBJ 更新到 ZMTFXBJ_TM

            var ZJHKBSJ_List = rotList.Where(t => t.TRQ_TYPE.StartsWith("OI") && t.ZJHKBSJ != null && t.ZJHKBSJ_TM != null && t.ZJHKBSJ != t.ZJHKBSJ_TM);
            var ZZYGQHC_List = rotList.Where(t => t.TRQ_TYPE.StartsWith("OE") && t.ZZYGQHC != null && t.ZZYGQHC_TM != null && t.ZZYGQHC != t.ZZYGQHC_TM);
            var ZZZGHC_List = rotList.Where(t => t.TRQ_TYPE.StartsWith("OE") && t.ZZZGHC != null && t.ZZZGHC_TM != null && t.ZZZGHC != t.ZZZGHC_TM);

            var ZTDJS_List = tdList.Where(t => t.ZTDJS != null && t.ZJGJS != null && t.ZTDJS != t.ZJGJS);
            var ZTDMZ_List = tdList.Where(t => t.ZTDMZ != null && t.ZJGMZ != null && t.ZTDMZ != t.ZJGMZ);
            var ZTDTJ_List = tdList.Where(t => t.ZTDTJ != null && t.ZJGTJ != null && t.ZTDTJ != t.ZJGTJ);

            var ZMTFXBJ_List = tdfuList.Where(t => t.ZMTFXBJ_TM == "X" && t.ZMTFXBJ != "Y");
            var ZFH1_List = tdfuList.Where(t => t.ZFH1 != null && t.ZFH1_TM != null && t.ZFH1 != t.ZFH1_TM);

            #region rot
            foreach (var item in ZJHKBSJ_List)
            {
                var alertEntity = CreateAlertVO();    
                alertEntity.DBBT = "计划靠泊时间不一致预警";
                alertEntity.DBSX = string.Format("FWO:{0},接单人:{1},TM计划靠泊时间:{2},港区计划靠泊时间:{3}", item.TRQ_ID, item.ZJDR, item.ZJHKBSJ_TM, item.ZJHKBSJ);              
                alertEntity.JSR = item.ZJDR;
                alertList.Add(alertEntity);
                string sqlStr = @"update szh_trqrot set ZJHKBSJ_TM=ZJHKBSJ where trq_id='" + item.TRQ_ID + "'";
                sqlList.Add(sqlStr);
            }

            foreach (var item in ZZYGQHC_List)
            {
                var alertEntity = CreateAlertVO();
                alertEntity.DBBT = "码头字段预警";
                alertEntity.DBSX = string.Format("FWO:{0},接单人:{1},TM装运港区:{2},港区装运港区:{3}", item.TRQ_ID, item.ZJDR, item.ZZYGQHC_TM, item.ZZYGQHC);
                alertEntity.JSR = item.ZJDR;
                alertList.Add(alertEntity);
            }

            foreach (var item in ZZZGHC_List)
            {
                var alertEntity = CreateAlertVO();
                alertEntity.DBBT = "卸货港区不一致预警";
                alertEntity.DBSX = string.Format("FWO:{0},接单人:{1},TM中转港:{2},港区中转港:{3}", item.TRQ_ID, item.ZJDR, item.ZZZGHC_TM, item.ZZZGHC);
                alertEntity.JSR = item.ZJDR;
                alertList.Add(alertEntity);
            }

            #endregion

            #region td
            foreach (var item in ZTDJS_List)
            {
                var firstRot = rotList.First(t => t.TRQ_ID == item.TRQ_ID);
                if (firstRot != null && firstRot.TRQ_TYPE.StartsWith("OE"))
                {
                    var alertEntity = CreateAlertVO();
                    alertEntity.DBBT = "进港件数不一致预警";
                    alertEntity.DBSX = string.Format("FWO:{0},接单人:{1},TM提单件数:{2},港区进港件数:{3}", item.TRQ_ID, firstRot.ZJDR, item.ZTDJS, item.ZJGJS);
                    alertEntity.JSR = firstRot.ZJDR;
                    alertList.Add(alertEntity);
                }
            }
            foreach (var item in ZTDMZ_List)
            {
                var firstRot = rotList.First(t => t.TRQ_ID == item.TRQ_ID);
                if (firstRot != null && firstRot.TRQ_TYPE.StartsWith("OE"))
                {
                    var alertEntity = CreateAlertVO();
                    alertEntity.DBBT = "毛重不一致预警";
                    alertEntity.DBSX = string.Format("FWO:{0},接单人:{1},TM提单重量:{2},港区提单重量:{3}", item.TRQ_ID, firstRot.ZJDR, item.ZTDMZ, item.ZJGMZ);
                    alertEntity.JSR = firstRot.ZJDR;
                    alertList.Add(alertEntity);
                }
            }
            foreach (var item in ZTDTJ_List)
            {
                var firstRot = rotList.First(t => t.TRQ_ID == item.TRQ_ID);
                if (firstRot != null && firstRot.TRQ_TYPE.StartsWith("OE"))
                {
                    var alertEntity = CreateAlertVO();
                    alertEntity.DBBT = "体积不一致预警";
                    alertEntity.DBSX = string.Format("FWO:{0},接单人:{1},TM提单体积:{2},港区进提单积:{3}", item.TRQ_ID, firstRot.ZJDR, item.ZTDTJ, item.ZJGTJ);
                    alertEntity.JSR = firstRot.ZJDR;
                    alertList.Add(alertEntity);
                }
            }
            #endregion

            #region tdfu
            foreach (var item in ZMTFXBJ_List)
            {
                var firstRot = rotList.First(t => t.TRQ_ID == item.TRQ_ID);
                if (firstRot != null && firstRot.TRQ_TYPE.StartsWith("OE"))
                {
                    var alertEntity = CreateAlertVO();
                    alertEntity.DBBT = "码头放行标记不一致预警";
                    alertEntity.DBSX = string.Format("FWO:{0},接单人:{1},TM放行标记:{2},港区放行标记:{3}", item.TRQ_ID, firstRot.ZJDR, item.ZMTFXBJ_TM, item.ZMTFXBJ);
                    alertEntity.JSR = firstRot.ZJDR;
                    alertList.Add(alertEntity);
                    string sqlStr;
                    if (item.ZMTFXBJ == "Y")
                    {
                        sqlStr = @"update szh_trqtdfu set ZMTFXBJ_TM='X'  where trq_id='" + item.TRQ_ID + "' and zsbtdh='" + item.ZSBTDH + "' and TURES_ID='" + item.TURES_ID + "'";
                    }else
                    {
                        sqlStr = @"update szh_trqtdfu set ZMTFXBJ_TM=''  where trq_id='" + item.TRQ_ID + "' and zsbtdh='" + item.ZSBTDH + "' and TURES_ID='" + item.TURES_ID + "'";
                    }
                    sqlList.Add(sqlStr);
                }
            }

            foreach (var item in ZFH1_List)
            {
                var firstRot = rotList.First(t => t.TRQ_ID == item.TRQ_ID);
                if (firstRot != null && firstRot.TRQ_TYPE.StartsWith("OE"))
                {
                    var alertEntity = CreateAlertVO();
                    alertEntity.DBBT = "封号不一致预警";
                    alertEntity.DBSX = string.Format("TRQ_ID:{0},接单人:{1},TM封号:{2},港区封号:{3}", item.TRQ_ID, firstRot.ZJDR, item.ZFH1_TM, item.ZFH1);
                    alertEntity.JSR = firstRot.ZJDR;
                    alertList.Add(alertEntity);
                }
            }

            db.ExecuteSqlList(sqlList, connectionString);
            #endregion

            try
            {
                foreach (var item in alertList)
                {
                    ECologyService service = new ECologyService(oaserviceurl);
                    var temp=service.SendAlert(item);
                }
            }
            catch (Exception)
            {

            }
        }

        private AlertVO CreateAlertVO()
        {
            AlertVO alertEntity = new AlertVO();
            alertEntity.JJCD = "4";
            alertEntity.DBFL = "提醒";
            alertEntity.JSSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            alertEntity.CFXT = "TMxSZH";
            return alertEntity;
        }
    }


    public static class Extensions
    {
        public static string ToStringOrNull(this object data)
        {
            return data == null ? null : data.ToString();
        }
        public static string DateOrNullToString(this DateTime? dt)
        {
            if (dt == null)
            {
                return null;
            }
            return ((DateTime)dt).AddHours(-8).ToString("yyyyMMddHHmmss");
        }
        public static string DateStrToString(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            DateTime result;
            bool isValid = DateTime.TryParse(str, out result);
            if (isValid)
            {
                return result.AddHours(-8).ToString("yyyyMMddHHmmss");
            }
            else
            {
                return null;
            }
        }
        public static string ElementValueNull(this XElement element)
        {
            if (element != null)
                return element.Value;

            return null;
        }

        public static DateTime? ToDateOrNull(this XElement element)
        {
            if (element != null)
            {
                DateTime result;
                bool isValid = DateTime.TryParse(element.Value, out result);

                if (isValid)
                    return result;
            }
            return null;
        }

        public static int? ElementToIntOrNull(this XElement data)
        {
            if (data == null)
                return null;
            int result;
            bool isValid = int.TryParse(data.Value, out result);
            if (isValid)
                return result;
            double result1;
            isValid = double.TryParse(data.Value, out result1);
            if (isValid)
                return (int)result1;
            return null;
        }

        public static double? ElementToDoubleOrNull(this XElement data)
        {
            if (data == null)
                return null;
            double result;
            bool isValid = double.TryParse(data.Value, out result);
            if (isValid)
                return result;
            return null;
        }
        public static decimal? ElementToDecimalOrNull(this XElement data)
        {
            if (data == null)
                return null;
            decimal result;
            bool isValid = decimal.TryParse(data.Value, out result);
            if (isValid)
                return result;
            return null;
        }

        /// <summary>
        /// 转换为可空整型
        /// </summary>
        /// <param name="data">数据</param>
        public static int? ToIntOrNull(this object data)
        {
            if (data == null)
                return null;
            int result;
            bool isValid = int.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }
        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="data">数据</param>
        public static DateTime? ToDateOrNull(this object data)
        {
            if (data == null)
                return null;
            DateTime result;
            bool isValid = DateTime.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }
        /// <summary>
        /// 转换为可空双精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static double? ToDoubleOrNull(this object data)
        {
            if (data == null)
                return null;
            double result;
            bool isValid = double.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }

        public static decimal? ToDecimalOrNull(this object data)
        {
            if (data == null)
                return null;
            decimal result;
            bool isValid = decimal.TryParse(data.ToString(), out result);
            if (isValid)
                return result;
            return null;
        }

    }

    public class InputParameters
    {
        public string connectionstring { get; set; }
        public string user { get; set; }
        public string url { get; set; }
        public string password { get; set; }
        public string functionname { get; set; }
        public string saverespath { get; set; }
        public string acct { get; set; }
        public string tag { get; set; }
        public string path { get; set; }
        public string oaserviceurl { get; set; }
    }


}
