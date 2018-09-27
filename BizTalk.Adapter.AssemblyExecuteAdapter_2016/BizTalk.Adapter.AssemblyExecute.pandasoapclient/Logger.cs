using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;

namespace BizTalk.Adapter.AssemblyExecute.aliyuncsb
{
    public class Logger
    {
        public static void Write(string key1, string key2, string key3, string res1, string res2, string url, string action, string tag, string connstr)
        {
            using (OracleConnection conn = new OracleConnection(connstr))
            {
                conn.Open();
                string sql = string.Format(@"insert into messagelog (messagetype,messagename,customer,refno1,refno2,refno3,type,notes,notes1)
values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", url, action, tag, key1, key2, key3, "OUT", res1, res2.Replace("'","''"));
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }
    }
}
