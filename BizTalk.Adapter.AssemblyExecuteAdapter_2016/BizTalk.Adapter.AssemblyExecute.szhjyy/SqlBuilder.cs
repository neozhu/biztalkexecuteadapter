using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BizTalk.Adapter.AssemblyExecute.szhjyy
{
    /// <summary>
    /// sql语句组合
    /// </summary>
    public static class SqlBuilder
    {
        /// <summary>
        /// Insert语句,null值忽略
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static string InsertSql<T>(T entity, string tableName = null)
        {
            Type type = entity.GetType();
            if (tableName == null)
            {
                tableName = type.Name;
            }
            string sqlStr = "INSERT INTO " + tableName;
            string columnName = "\t(";
            string columnValue = "\t(";
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                string name = prop.Name;
                object value = prop.GetValue(entity, null);
                if (value != null)
                {
                    columnName += name + ",";
                    if (prop.PropertyType == typeof(string))
                    {
                        columnValue += "'" + value.ToString() + "',";
                    }
                    else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(Nullable<DateTime>))
                    {
                        columnValue += "TO_DATE('" + value.ToString() + "', 'yyyy/mm/dd hh24:mi:ss'),";
                    }
                    else
                    {
                        columnValue += value.ToString() + ",";
                    }
                }
            }
            if (columnName == "(")
            {
                return null;
            }
            columnName = columnName.TrimEnd(',') + ")";
            columnValue = columnValue.TrimEnd(',') + ")";
            sqlStr = sqlStr + "\n" + columnName + "\nVALUES\n" + columnValue;
            return sqlStr;
        }


        public static List<string> InsertSql<T>(List<T> entityList, string tableName = null)
        {
            List<string> sqlList = new List<string>();
            foreach (var entity in entityList)
            {
                var sqlStr = InsertSql(entity);
                if (sqlStr != null)
                {
                    sqlList.Add(sqlStr);
                }
            }
            return sqlList;
        }


        /// <summary>
        /// Update语句
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <param name="keyList">更新条件键值</param>
        /// <param name="columnList">更新字段</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static string UpdateSql<T>(T entity,List<string> keyList, List<string> columnList = null, string tableName = null)
        {
            Type type = entity.GetType();
            if (tableName == null)
            {
                tableName = type.Name;
            }
            StringBuilder sqlSB = new StringBuilder();
            sqlSB.Append("UPDATE " + tableName + "\n   SET ");
            string whereStr = "\n WHERE ";
            PropertyInfo[] props = type.GetProperties();
            bool firstColumn = true;
            foreach (PropertyInfo prop in props)
            {
                string name = prop.Name;
                if (keyList.Contains(prop.Name))
                {
                    object value = prop.GetValue(entity, null);
                    if (whereStr != "\n WHERE ")
                    {
                        whereStr += "\n   AND ";
                    }
                    whereStr += (name + "='" + value + "'");
                }
                else if (columnList == null)
                {
                    object value = prop.GetValue(entity, null);                    
                    if (value != null)
                    {
                        if (firstColumn)
                        {
                            firstColumn = false;
                        }
                        else
                        {
                            sqlSB.Append("\n       ");
                        }
                        if (prop.PropertyType == typeof(string))
                        {
                            sqlSB.AppendFormat("{0}='{1}',", name, value);
                        }
                        else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(Nullable<DateTime>))
                        {
                            sqlSB.AppendFormat("{0}=TO_DATE('{1}','yyyy/mm/dd hh24:mi:ss'),", name, value);
                        }
                        else
                        {
                            sqlSB.AppendFormat("{0}={1},", name, value);
                        }
                    }
                }
                else if (columnList.Contains(name))
                {
                    object value = prop.GetValue(entity, null);
                    if (firstColumn)
                    {
                        firstColumn = false;
                    }
                    else
                    {
                        sqlSB.Append("\n       ");
                    }
                    if (value == null)
                    {
                        sqlSB.AppendFormat("{0}=null,", name);
                    }
                    else if (prop.PropertyType == typeof(string))
                    {
                        sqlSB.AppendFormat("{0}='{1}',", name, value);
                    }
                    else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(Nullable<DateTime>))
                    {
                        sqlSB.AppendFormat("{0}=TO_DATE('{1}','yyyy/mm/dd hh24:mi:ss'),", name, value);
                    }
                    else
                    {
                        sqlSB.AppendFormat("{0}={1},", name, value);
                    }
                }
            }
            if (firstColumn)
            {
                return null;
            }
            sqlSB.Remove(sqlSB.Length - 1, 1);
            return sqlSB.ToString() + whereStr;
        }

        /// <summary>
        /// Update语句
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="entityList">实体对象列表</param>
        /// <param name="keyList">更新条件键值</param>
        /// <param name="columnList">更新字段</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static List<string> UpdateSql<T>(List<T> entityList, List<string> keyList, List<string> columnList = null, string tableName = null)
        {
            List<string> sqlList = new List<string>();
            foreach(var entity in entityList)
            {
                string sqlStr = UpdateSql(entity, keyList, columnList, tableName);
                if (sqlStr != null)
                {
                    sqlList.Add(sqlStr);
                }                
            }
            return sqlList;
        }


        public static string ListToInSql(List<string> strList)
        {
            if (strList == null || strList.Count == 0)
            {
                return "()";
            }
            StringBuilder sb = new StringBuilder("(", 1024);
            foreach(var item in strList)
            {
                sb.AppendFormat("'{0}',", item);
            }
            sb.Replace(',', ')', sb.Length - 1, 1);
            return sb.ToString();
        }





    }
}
