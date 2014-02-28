using System;

namespace CSharp.Util.Text
{
    /// <summary>
    /// 字符串操作辅助处理类
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// 截取指定长度的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string SubString(string str, int len)
        {
            if (string.IsNullOrEmpty(str) || str.Length <= len) return str;
            else return str.Substring(0, len);
        }

        /// <summary>
        /// 按字节截取指定长度的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string SubStringByBytes(string str, int len)
        {
            //byte[] buffer = System.Text.Encoding.Default.GetBytes(str);

            //if (buffer.Length > len)
            //{
            //    str = System.Text.Encoding.Default.GetString(buffer, 0, len);
            //}
            //return str.TrimEnd('?');

            if (string.IsNullOrEmpty(str)) return str;

            str = str.Trim();
            byte[] myByte = System.Text.Encoding.Default.GetBytes(str);
            if (myByte.Length > len)
            {
                string resultStr = "";
                for (int i = 0; i < str.Length; i++)
                {
                    byte[] tempByte = System.Text.Encoding.Default.GetBytes(resultStr);
                    if (tempByte.Length < len)
                    {
                        resultStr += str.Substring(i, 1);
                    }
                    else
                    {
                        break;
                    }
                }
                return resultStr;
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// 过滤字符窜前缀(忽略前缀大小写)
        /// </summary>
        /// <param name="str">要过滤的字符窜</param>
        /// <param name="prefix">要过滤的前缀</param>
        /// <returns></returns>
        public static string FilterPrefix(string str, string prefix)
        {
            return FilterPrefix(str, prefix, true);
        }

        /// <summary>
        /// 过滤字符窜前缀
        /// </summary>
        /// <param name="str">要过滤的字符窜</param>
        /// <param name="prefix">要过滤的前缀</param>
        /// <param name="ignoreCase">忽略大小写</param>
        /// <returns></returns>
        public static string FilterPrefix(string str, string prefix, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            //若过滤前缀为空或者字符窜长度小于过滤前缀
            if (string.IsNullOrEmpty(prefix) || str.Length < prefix.Length) return str;
            //如果忽略大小写
            if (ignoreCase)
            {
                if (str.Substring(0, prefix.Length).ToLower() == prefix.ToLower())
                {
                    str = str.Substring(prefix.Length);
                }
            }//如果不忽略大小写
            else
            {
                if (str.StartsWith(prefix)) str = str.Substring(prefix.Length);
            }
            return str;
        }

        /// <summary>
        /// 将制定的字符串转换为首字母大写
        /// </summary>
        /// <param name="str">要转换的字符窜</param>
        /// <returns></returns>
        public static string ToTitleCase(string str)
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str);
        }

        public static string[] Split(string str, string separator, StringSplitOptions options)
        {
            return str.Split(new string[] { separator }, options);
        }

        public static int BytesLength(string str)
        {
            //byte[] buffer = System.Text.Encoding.Default.GetBytes(str);

            //if (buffer.Length > len)
            //{
            //    str = System.Text.Encoding.Default.GetString(buffer, 0, len);
            //}
            //return str.TrimEnd('?');

            if (string.IsNullOrEmpty(str)) return 0;

            return System.Text.Encoding.Default.GetBytes(str).Length;
        }

    }
}