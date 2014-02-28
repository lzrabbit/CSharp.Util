using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Data.OleDb;
using System.IO;

namespace CSharp.Util.Data
{
    /// <summary>
    /// DataTable
    /// </summary>
    public static class DataTableHelper
    {
        static Dictionary<string, Dictionary<string, PropertyInfo>> MapCacheDict = new Dictionary<string, Dictionary<string, PropertyInfo>>();
        #region Map

        public static List<T> Map<T>(DataTable dt) where T : new()
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return new List<T>();
            }
            List<T> modelList = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                modelList.Add(Map<T>(dr));
            }
            return modelList;
        }

        public static T Map<T>(DataRow dr) where T : new()
        {
            if (dr == null) return default(T);

            string tname = typeof(T).FullName;
            Dictionary<string, PropertyInfo> propDict;
            if (MapCacheDict.ContainsKey(tname))
            {
                propDict = MapCacheDict[tname];
            }
            else
            {
                propDict = new Dictionary<string, PropertyInfo>();
                MapCacheDict.Add(tname, propDict);

                var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var prop in props)
                {
                    propDict.Add(prop.Name, prop);
                }
            }

            T model = new T();
            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                PropertyInfo prop = null;
                if (propDict.ContainsKey(dr.Table.Columns[i].ColumnName))
                {
                    prop = propDict[dr.Table.Columns[i].ColumnName];
                }

                if (prop != null && dr[i] != DBNull.Value)
                {
                    if (prop.PropertyType == typeof(int))
                    {
                        prop.SetValue(model, Convert.ToInt32(dr[i]), null);
                    }
                    else if (prop.PropertyType == typeof(decimal))
                    {
                        prop.SetValue(model, Convert.ToDecimal(dr[i]), null);
                    }
                    else if (prop.PropertyType == typeof(double))
                    {
                        prop.SetValue(model, Convert.ToDouble(dr[i]), null);
                    }
                    else if (prop.PropertyType == typeof(bool))
                    {
                        prop.SetValue(model, Convert.ToBoolean(dr[i]), null);
                    }
                    else if (prop.PropertyType == typeof(string))
                    {
                        prop.SetValue(model, dr[i].ToString(), null);
                    }
                    else if (prop.PropertyType == typeof(DateTime))
                    {
                        prop.SetValue(model, Convert.ToDateTime(dr[i]), null);
                    }
                    else
                    {
                        prop.SetValue(model, dr[i], null);
                    }
                }
            }
            return model;
        }

        #endregion

        #region Excel

        /// <summary>
        /// 根据Excel文件自动生成相应的链接字符串
        /// </summary>
        /// <param name="fpath"></param>
        /// <returns></returns>
        private static string GetExcelConnString(string fpath)
        {
            string ext = Path.GetExtension(fpath).ToLower();
            string connString = null;
            if (ext.ToLower() == ".xls")
            {
                connString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES;IMEX=1'", fpath);
            }
            else
            {
                connString = string.Format("Provider=Microsoft.ace.oledb.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'", fpath);
            }

            return connString;
        }

        /// <summary>
        /// 获取Excel所有工作簿名称(根据工作簿名称排序)
        /// </summary>
        /// <param name="fpath">Exce文件</param>
        /// <returns></returns>
        public static List<string> GetExcelWorksheets(string fpath)
        {
            List<string> list = new List<string>();
            string connString = GetExcelConnString(fpath);
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                conn.Open();
                DataTable table = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                conn.Close();
                foreach (DataRow row in table.Rows)
                {
                    list.Add(row[2].ToString());
                }
            }
            return list;
        }

        /// <summary>
        /// 读取Excel文件到DataTable
        /// </summary>
        /// <param name="fpath">Excel文件绝对路径</param>
        /// <param name="worksheet">工作簿名称(不填写默认获取第一个工作表)</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string fpath, string worksheet = null)
        {
            if (string.IsNullOrEmpty(worksheet)) worksheet = GetExcelWorksheets(fpath).FirstOrDefault();
            if (string.IsNullOrEmpty(worksheet)) worksheet = "Sheet1";
            DataTable dt = new DataTable();
            string connString = GetExcelConnString(fpath);
            OleDbDataAdapter ada = new OleDbDataAdapter(string.Format("select * from [{0}$]", worksheet), connString);
            ada.Fill(dt);
            return dt;
        }


        #endregion

        #region CSV

        /// <summary>
        /// 读取CSV文件到DataTable
        /// </summary>
        /// <param name="fpath"></param>
        /// <param name="header">第一行数据是否为表头</param>
        /// <returns></returns>
        public static DataTable CSVToDataTable(string fpath, bool header = true)
        {
            DataTable dt = new DataTable();
            if (File.Exists(fpath))
            {
                DataRow row = null;
                string[] rowArr = null;
                using (Microsoft.VisualBasic.FileIO.TextFieldParser parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(fpath, Encoding.GetEncoding("GB2312")))
                {
                    parser.Delimiters = new string[] { "," };
                    parser.TrimWhiteSpace = true;
                    while (!parser.EndOfData)
                    {
                        if (parser.LineNumber == 1)
                        {
                            rowArr = parser.ReadFields();
                            if (!header)
                            {
                                for (int i = 0; i < rowArr.Length; i++)
                                {
                                    dt.Columns.Add(i.ToString());
                                }
                                row = dt.NewRow();
                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    row[i] = rowArr[i];
                                }
                                dt.Rows.Add(row);
                            }
                            else
                            {
                                foreach (string col in rowArr)
                                {
                                    dt.Columns.Add(col);
                                }
                            }
                        }
                        else
                        {
                            rowArr = parser.ReadFields();
                            row = dt.NewRow();
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                row[i] = rowArr[i];
                            }
                            dt.Rows.Add(row);
                        }
                    }
                    parser.Close();
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
            return dt;
        }

        /// <summary>
        /// DataTable转化为标准的CSV
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToCsv(DataTable dt, bool header = true)
        {

            //以半角逗号（即,）作分隔符，列为空也要表达其存在。
            //列内容如存在半角逗号（即,）则用半角引号（即""）将该字段值包含起来。
            //列内容如存在半角引号（即"）则应替换成半角双引号（""）转义，并用半角引号（即""）将该字段值包含起来。
            StringBuilder sb = new StringBuilder();

            if (header)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    if (sb.Length == 0) sb.Append(col.ColumnName);
                    else sb.Append("," + col.ColumnName);
                }
                sb.AppendLine();
            }
            DataColumn colum;
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    colum = dt.Columns[i];
                    if (i != 0) sb.Append(",");
                    if (colum.DataType == typeof(string) && row[colum].ToString().Contains(","))
                    {
                        sb.Append("\"" + row[colum].ToString().Replace("\"", "\"\"") + "\"");
                    }
                    else
                    {
                        if (row[colum].GetType() == typeof(System.DBNull)) sb.Append("NULL");
                        else sb.Append(row[colum].ToString());
                    }
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// DataTable转化为标准的CSV
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fpath">保存的文件路径</param>
        public static void DataTableToCsv(DataTable dt, string fpath)
        {
            string csv = DataTableToCsv(dt);
            File.WriteAllText(fpath, csv, Encoding.Default);
        }

        #endregion CSV

    }
}
