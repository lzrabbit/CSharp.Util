using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace CSharp.Util
{
    /// <summary>
    /// 获取枚举的描述信息
    /// </summary>
    public class EnumDesc
    {
        static readonly Dictionary<string, string> dict = new Dictionary<string, string>();
        public static string GetDesc(Enum value)
        {
            if (value == null) return null;
            Type type = value.GetType();
            string key = string.Format("{0}.{1}", type.ToString(), value.ToString());
            if (!dict.ContainsKey(key))
            {
                var fields = type.GetFields();
                foreach (var field in fields)
                {
                    var attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
                    if (attr != null)
                    {
                        dict.Add(string.Format("{0}.{1}", type.ToString(), field.Name), attr.Description);
                    }
                    else
                    {
                        dict.Add(string.Format("{0}.{1}", type.ToString(), field.Name), field.Name);
                    }
                }
            }
            return dict[key];
        }
    }
}
