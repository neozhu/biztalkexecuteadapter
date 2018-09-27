using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using System.Reflection;
using System.Data;

namespace BizTalk.Adapter.AssemblyExecute.szhjyy
{
    public class db
    {

        #region 1001

        public static void save1001(SZH_TRQROT trqrot, List<SZH_TRQTD> trqtdList, List<SZH_TRQTDFU> trqtdfuList, string connectionString)
        {
            List<string> sqlList = new List<string>();
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                string sqlStr = string.Format("select count(*) from SZH_TRQROT where TRQ_ID='{0}'", trqrot.TRQ_ID);
                OracleCommand cmd1 = connection.CreateCommand();
                cmd1.CommandText = sqlStr;
                if (Convert.ToInt32(cmd1.ExecuteScalar()) == 0)
                {
                    sqlList.Add(SqlBuilder.InsertSql(trqrot));
                    sqlList.AddRange(SqlBuilder.InsertSql(trqtdList));
                    sqlList.AddRange(SqlBuilder.InsertSql(trqtdfuList));
                }
                else
                {
                    trqrot.UPDATETIME = DateTime.Now;
                    sqlList.Add(SqlBuilder.UpdateSql(trqrot, new List<string> { "TRQ_ID" },
                            new List<string> { "TRQ_TYPE", "ZCMHC", "ZHCHC", "ZJHSHRQ", "ZJDR", "ZSMZQ", "ZJHKBSJ_TM", "ZZYGQHC_TM", "ZZZGHC_TM", "UPDATETIME" }));

                    foreach (var trqtd in trqtdList)
                    {
                        sqlStr = string.Format(@"SELECT COUNT(*) FROM SZH_TRQTD WHERE TRQ_ID='{0}' AND ZSBTDH='{1}'", trqtd.TRQ_ID, trqtd.ZSBTDH);
                        OracleCommand cmd2 = connection.CreateCommand();
                        cmd2.CommandText = sqlStr;
                        if (Convert.ToInt32(cmd2.ExecuteScalar()) == 0)
                        {
                            sqlList.Add(SqlBuilder.InsertSql(trqtd));
                        } 
                        else
                        {
                            trqtd.UPDATETIME = DateTime.Now;
                            sqlList.Add(SqlBuilder.UpdateSql(trqtd, new List<string> { "TRQ_ID", "ZSBTDH" },
                                new List<string> { "ZTDJS", "ZTDMZ", "ZTDTJ", "UPDATETIME" }));
                        }
                    }

                    foreach (var trqtdfu in trqtdfuList)
                    {
                        sqlStr = string.Format(@"SELECT COUNT(*) FROM SZH_TRQTDFU WHERE TRQ_ID='{0}' AND ZSBTDH='{1}' AND TURES_ID='{2}'", trqtdfu.TRQ_ID, trqtdfu.ZSBTDH, trqtdfu.TURES_ID);
                        OracleCommand cmd2 = connection.CreateCommand();
                        cmd2.CommandText = sqlStr;
                        if (Convert.ToInt32(cmd2.ExecuteScalar()) == 0)
                        {
                            sqlList.Add(SqlBuilder.InsertSql(trqtdfu));
                        }
                        else
                        {
                            trqtdfu.UPDATETIME = DateTime.Now;
                            sqlList.Add(SqlBuilder.UpdateSql(trqtdfu, new List<string> { "TRQ_ID", "ZSBTDH", "TURES_ID" },
                                new List<string> { "ZMTFXBJ_TM", "ZFH1_TM", "UPDATETIME" }));
                        }
                    }
                }
            }

            //删除td-tdfu
            List<string> tdhIdList = trqtdList.Select(t => t.ZSBTDH).ToList();
            if (tdhIdList != null && tdhIdList.Count > 0)
            {
                string strSql = string.Format("DELETE FROM SZH_TRQTD WHERE TRQ_ID = '{0}' AND ZSBTDH NOT IN {1}", trqrot.TRQ_ID, SqlBuilder.ListToInSql(tdhIdList));
                sqlList.Add(strSql);
                foreach (var tdhId in tdhIdList)
                {
                    List<string> tdfuIdList = trqtdfuList.Where(t => t.ZSBTDH == tdhId).Select(t => t.TURES_ID).ToList();
                    if (tdfuIdList != null && tdfuIdList.Count > 0)
                    {
                        string strSql1 = string.Format("DELETE FROM SZH_TRQTDFU WHERE TRQ_ID = '{0}' AND ZSBTDH = '{1}' AND TURES_ID NOT IN {2}", trqrot.TRQ_ID, tdhId, SqlBuilder.ListToInSql(tdfuIdList));
                        sqlList.Add(strSql1);
                    }
                    else
                    {
                        string strSql1 = string.Format("DELETE FROM SZH_TRQTDFU WHERE TRQ_ID = '{0}' AND ZSBTDH = '{1}'", trqrot.TRQ_ID, tdhId);
                        sqlList.Add(strSql1);
                    }
                }
            }
            else
            {
                string strSql = string.Format("DELETE FROM SZH_TRQTD WHERE TRQ_ID = '{0}'", trqrot.TRQ_ID);
                sqlList.Add(strSql);
            }

            ExecuteSqlList(sqlList, connectionString);   //事务执行
        }

        /// <summary>
        /// 更新jyy返回结果到数据库
        /// </summary>
        /// <param name="TRQ_ID"></param>
        /// <param name="jyymeg"></param>
        /// <param name="connectionString"></param>
        public static void updatejyymsg1001(string TRQ_ID, string jyymeg, string connectionString)
        {
            string sqlStr = string.Format(@"UPDATE SZH_TRQTD 
                                               SET JYYMSG = '{0}',
                                                   SENDSZHTIME=TO_DATE('{1}','yyyy/mm/dd hh24:mi:ss') 
                                             WHERE TRQ_ID = '{2}'", jyymeg, DateTime.Now, TRQ_ID);

            ExecuteSql(sqlStr, connectionString);
        }


        #endregion



        #region 1003

        public static void save1003(List<SZH_TRQROT> trqrotList, List<SZH_TRQTD> trqtdList, List<SZH_TRQTDFU> trqtdfuList,List<SZH_TRQITM> trqitmList, string inSql,string connectionString)
        {
            List<string> sqlList = new List<string>();
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                OracleCommand cmd = connection.CreateCommand();              
                for (var n = 0; n < trqtdList.Count; ++n)
                {
                    string sqlStr = string.Format("select TRQ_ID from SZH_TRQTD where ZSBTDH='{0}'", trqtdList[n].ZSBTDH);              
                    cmd.CommandText = sqlStr;
                    var temp = cmd.ExecuteScalar();
                    if (temp == null)
                    {
                        continue;
                    }
                    trqitmList[n].TRQ_ID = trqrotList[n].TRQ_ID = temp.ToString();
                    if (trqitmList.Count(t => t.TRQ_ID == trqitmList[n].TRQ_ID && t.TURES_ID == trqitmList[n].TURES_ID) < 2)
                    {
                        sqlStr = string.Format("select count(1) from SZH_TRQITM where TRQ_ID = '{0}' and TURES_ID = '{1}'", trqitmList[n].TRQ_ID, trqitmList[n].TURES_ID);
                        cmd.CommandText = sqlStr;
                        if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                        {
                            sqlList.Add(SqlBuilder.InsertSql(trqitmList[n]));
                        }
                        else
                        {
                            trqitmList[n].UPDATETIME = DateTime.Now;
                            List<string> itmUpdateList = new List<string> { "ZQCC", "ZHCC", "ZZCK", "ZYCK", "ZWXPDJ", "ZLHGBH", "ZJGRQSJ1", "ZCCRQSJ", "ZLCWD", "UPDATETIME" };
                            if (trqitmList[n].ZXXSJ.HasValue)
                            {
                                itmUpdateList.Add("ZXXSJ");
                            }
                            if (trqitmList[n].ZTZXRQSJ.HasValue)
                            {
                                itmUpdateList.Add("ZTZXRQSJ");
                            }
                            sqlList.Add(SqlBuilder.UpdateSql(trqitmList[n], new List<string> { "TRQ_ID", "TURES_ID" }, itmUpdateList));
                        }
                    }                   
                }
            }
                  
            foreach(var item in trqrotList)
            {
                List<string> rotUpdateList = new List<string> { "ZXHGQ", "ZCGS", "ZZZGHC", "ZMDGHC", "UPDATETIME" };
                if (item.ZSYSJ.HasValue)
                {
                    rotUpdateList.Add("ZSYSJ");
                }
                sqlList.Add(SqlBuilder.UpdateSql(item, new List<string> { "TRQ_ID" }, rotUpdateList));
            }
                        
            sqlList.AddRange(SqlBuilder.UpdateSql(trqtdList, new List<string> { "ZSBTDH" },
                    new List<string> { "ZEF", "ZFG", "ZEFSJ", "UPDATETIME" }));
            sqlList.AddRange(SqlBuilder.UpdateSql(trqtdfuList, new List<string> { "ZSBTDH", "TURES_ID" }));

            string sqlStr1 = @"UPDATE SZH_TRQTD TD
   SET TD.ZJGJS =
       (SELECT SUM(TDFU.ZJGJS)
          FROM SZH_TRQTDFU TDFU
         WHERE TDFU.ZSBTDH = TD.ZSBTDH
           AND TDFU.TRQ_ID = TD.TRQ_ID),
       TD.ZJGMZ =
       (SELECT SUM(TDFU.ZJGMZ)
          FROM SZH_TRQTDFU TDFU
         WHERE TDFU.ZSBTDH = TD.ZSBTDH
           AND TDFU.TRQ_ID = TD.TRQ_ID),
       TD.ZJGTJ =
       (SELECT SUM(TDFU.ZJGTJ)
          FROM SZH_TRQTDFU TDFU
         WHERE TDFU.ZSBTDH = TD.ZSBTDH
           AND TDFU.TRQ_ID = TD.TRQ_ID)
 WHERE TD.ZSBTDH IN " + inSql;
            sqlList.Add(sqlStr1);
            ExecuteSqlList(sqlList, connectionString);   //事务执行
        }



        #endregion



        #region 公共方法

        /// <summary>
        /// 事务执行sql语句列表
        /// </summary>
        /// <param name="sqlList"></param>
        /// <param name="connectionString"></param>
        public static void ExecuteSqlList(List<string> sqlList, string connectionString)
        {
            if (sqlList == null || sqlList.Count == 0)
            {
                return;
            }
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                using (OracleTransaction transaction = conn.BeginTransaction())
                {
                    string temp = null;
                    try
                    {                       
                        OracleCommand cmd = conn.CreateCommand();
                        cmd.Transaction = transaction;
                        foreach (var item in sqlList)
                        {
                            temp = item;
                            cmd.CommandText = item;
                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                }

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlList"></param>
        /// <param name="connectionString"></param>
        public static void ExecuteSql(string sqlStr, string connectionString)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlStr;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        /// <summary>
        /// sql数据查询
        /// </summary>
        /// <param name="sqlStr">查询语句</param>
        /// <param name="connectionString">数据连接</param>
        /// <returns></returns>
        public static DataTable GetDataSet(string sqlStr, string connectionString)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                OracleCommand cmd = new OracleCommand(sqlStr, conn);
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                conn.Open();
                adapter.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
        }

        #endregion


    }


    #region Entity

    /// <summary>
    /// 代运订单
    /// </summary>
    public class SZH_TRQROT : BaseEntity
    {
        /// <summary>
        /// 代运订单/报价或运输申请的标识
        /// </summary>
        public string TRQ_ID { get; set; }
        /// <summary>
        /// 类型  I:进口 E:出口
        /// </summary>
        public string TRQ_TYPE { get; set; }
        /// <summary>
        /// 船名（仅海出）
        /// </summary>
        public string ZCMHC { get; set; }
        /// <summary>
        /// 航次（仅海出）
        /// </summary>
        public string ZHCHC { get; set; }
        /// <summary>
        /// 计划送货日期
        /// </summary>
        public DateTime? ZJHSHRQ { get; set; }
        /// <summary>
        /// 接单人
        /// </summary>
        public string ZJDR { get; set; }
        /// <summary>
        /// 生命周期
        /// </summary>
        public string ZSMZQ { get; set; }
        /// <summary>
        /// 码头
        /// </summary>
        public string ZMT { get; set; }
        /// <summary>
        /// 卸货港
        /// </summary>
        public string ZXHG { get; set; }
        /// <summary>
        /// 开港日期／时间（仅海出） 
        /// </summary>
        public DateTime? ZKGRQSJHC { get; set; }
        /// <summary>
        /// 截港日期／时间（仅海出）
        /// </summary>
        public DateTime? ZJGRQSJHC1 { get; set; }
        /// <summary>
        /// 计划靠泊时间
        /// </summary>
        public DateTime? ZJHKBSJ { get; set; }
        /// <summary>
        /// 计划离泊时间
        /// </summary>
        public DateTime? ZJHLBSJ { get; set; }
        /// <summary>
        /// 实际靠泊时间
        /// </summary>
        public DateTime? ZSJKBSJ { get; set; }
        /// <summary>
        /// 到港日期（仅海进）
        /// </summary>
        public DateTime? ZDGRQHJ { get; set; }
        /// <summary>
        /// 疏运时间
        /// </summary>
        public DateTime? ZSYSJ { get; set; }
       
        /// <summary>
        /// 船公司
        /// </summary>
        public string ZCGS { get; set; }
        /// <summary>
        /// 目的港（仅海出）
        /// </summary>
        public string ZMDGHC { get; set; }
        /// <summary>
        /// 实际离泊时间
        /// </summary>
        public string ZSJLBSJ { get; set; }
        /// <summary>
        /// 实际开航日期
        /// </summary>
        public string ZSJKHRQ { get; set; }
        /// <summary>
        /// 装运港区（仅海出）
        /// </summary>
        public string ZZYGQHC { get; set; }
        /// <summary>
        /// 中转港（仅海出）
        /// </summary>
        public string ZZZGHC { get; set; }

        /// <summary>
        /// 卸货港区
        /// </summary>
        public string ZXHGQ { get; set; }


        /// <summary>
        /// /计划靠泊时间预警
        /// </summary>
        public DateTime? ZJHKBSJ_TM { get; set; }
        /// <summary>
        /// 码头预警
        /// </summary>
        public string ZZYGQHC_TM { get; set; }
        /// <summary>
        /// 卸货港预警
        /// </summary>
        public string ZZZGHC_TM { get; set; }
    }

    /// <summary>
    /// 提单表
    /// </summary>
    public class SZH_TRQTD : BaseEntity
    {
        /// <summary>
        /// 代运订单
        /// </summary>
        public string TRQ_ID { get; set; }
        /// <summary>
        /// 申报提单号
        /// </summary>
        public string ZSBTDH { get; set; }
        /// <summary>
        /// 放关
        /// </summary>
        public string ZFG { get; set; }
        /// <summary>
        /// 二放
        /// </summary>
        public string ZEF { get; set; }
        /// <summary>
        /// 二放时间
        /// </summary>
        public DateTime? ZEFSJ { get; set; }
        /// <summary>
        /// 进港件数
        /// </summary>
        public decimal? ZJGJS { get; set; }
        /// <summary>
        /// 进港件数DW
        /// </summary>
        public string ZJGJSDW { get; set; }
        /// <summary>
        /// 进港毛重
        /// </summary>
        public decimal? ZJGMZ { get; set; }
        public string ZJGMZDW { get; set; }
        /// <summary>
        /// 进港体积
        /// </summary>
        public decimal? ZJGTJ { get; set; }

        public string ZJGTJDW { get; set; }


        /// <summary>
        /// 件数预警
        /// </summary>
        public decimal? ZTDJS { get; set; }
        /// <summary>
        /// 毛重预警
        /// </summary>
        public decimal? ZTDMZ { get; set; }
        /// <summary>
        /// 体积预警
        /// </summary>
        public decimal? ZTDTJ { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class SZH_TRQTDFU : BaseEntity
    {
        /// <summary>
        /// 代运订单
        /// </summary>
        public string TRQ_ID { get; set; }
        /// <summary>
        /// 申报提单号
        /// </summary>
        public string ZSBTDH { get; set; }
        /// <summary>
        /// 运输单元资源
        /// </summary>
        public string TURES_ID { get; set; }
        /// <summary>
        /// 码头放行标志
        /// </summary>
        public string ZMTFXBJ { get; set; }
        /// <summary>
        /// 配载时间
        /// </summary>
        public DateTime? ZPZSJ { get; set; }
        /// <summary>
        /// 配载标志
        /// </summary>
        public string ZPZBJ { get; set; }
        /// <summary>
        /// 运抵标志
        /// </summary>
        public string ZYDBJ { get; set; }
        /// <summary>
        /// 进港件数
        /// </summary>
        public decimal? ZJGJS { get; set; }
        /// <summary>
        /// 进港毛重
        /// </summary>
        public decimal? ZJGMZ { get; set; }
        /// <summary>
        /// 进港体积
        /// </summary>
        public decimal? ZJGTJ { get; set; }
        /// <summary>
        /// 持箱人
        /// </summary>
        public string ZCXR { get; set; }
        /// <summary>
        /// 放行时间
        /// </summary>
        public DateTime? ZFXSJ { get; set; }
        /// <summary>
        /// 铅封号
        /// </summary>
        public string ZFH1 { get; set; }

        /// <summary>
        /// 码头放行标志预警
        /// </summary>
        public string ZMTFXBJ_TM { get; set; }
        /// <summary>
        /// 封号预警
        /// </summary>
        public string ZFH1_TM { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class SZH_TRQITM : BaseEntity
    {
        /// <summary>
        /// 代运订单/报价或运输申请的标识
        /// </summary>
        public string TRQ_ID { get; set; }
        /// <summary>
        /// 运输单元资源
        /// </summary>
        public string TURES_ID { get; set; }
        /// <summary>
        /// 危险品等级 
        /// </summary>
        public string ZWXPDJ { get; set; }
        /// <summary>
        /// 联合国编号 
        /// </summary>
        public string ZLHGBH { get; set; }
        /// <summary>
        /// 冷藏温度(度)
        /// </summary>
        public string ZLCWD { get; set; }
        /// <summary>
        /// 前超长
        /// </summary>
        public decimal? ZQCC { get; set; }
        /// <summary>
        /// 后超长
        /// </summary>
        public decimal? ZHCC { get; set; }
        /// <summary>
        /// 左超宽
        /// </summary>
        public decimal? ZZCK { get; set; }
        /// <summary>
        /// 右超宽
        /// </summary>
        public decimal? ZYCK { get; set; }
        /// <summary>
        /// 超高
        /// </summary>
        public decimal? ZCG { get; set; }
        /// <summary>
        /// 卸箱时间
        /// </summary>
        public DateTime? ZXXSJ { get; set; }
        /// <summary>
        /// 进港日期/时间（仅海出） 
        /// </summary>
        public DateTime? ZJGRQSJ1 { get; set; }
        /// <summary>
        /// 出场日期/时间（仅海出）
        /// </summary>
        public DateTime? ZCCRQSJ { get; set; }
        /// <summary>
        /// 提重箱日期/时间（仅海运进口整柜） 
        /// </summary>
        public DateTime? ZTZXRQSJ { get; set; }
        /// <summary>
        /// 疏运标记
        /// </summary>
        public string SYBZ { get; set; }
    }

    
    public class BaseEntity
    {
        /// <summary>
        /// 接收时间
        /// </summary>
        public DateTime? RECEIVETIME { get; set; }
        /// <summary>
        /// 接收来源
        /// </summary>
        public string SOURCESYSTEM { get; set; }
        /// <summary>
        /// 发送苏浙沪时间
        /// </summary>
        public DateTime? SENDSZHTIME { get; set; }
        /// <summary>
        /// 苏浙沪返回结果
        /// </summary>
        public string JYYMSG { get; set; }
        /// <summary>
        /// 信息更新时间
        /// </summary>
        public DateTime? UPDATETIME { get; set; }
        /// <summary>
        /// 信息同步时间
        /// </summary>
        public DateTime? SYNCTIME { get; set; }


        public void Receive(string sourcesystem = "TM")
        {
            this.SOURCESYSTEM = sourcesystem;
            this.RECEIVETIME = DateTime.Now;
        }

    }


    #endregion

}
