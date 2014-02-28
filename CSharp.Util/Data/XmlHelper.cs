using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace CSharp.Util.Data
{
    public class XmlHelper
    {

        public static T Deserialize<T>(string xml, string encoding = "utf-8")
        {
            if (string.IsNullOrEmpty(xml)) return default(T);
            XmlSerializer xs = new XmlSerializer(typeof(T));
            MemoryStream memoryStream = new MemoryStream(Encoding.GetEncoding(encoding).GetBytes(xml));
            object result = xs.Deserialize(memoryStream);
            return (T)result;
        }

        public static string Serialize(object data, XmlSerializeSettings settings = null)
        {
            if (data == null) return null;
            if (settings == null) settings = new XmlSerializeSettings();
            XmlRootAttribute root = new XmlRootAttribute(settings.RootElementName) { Namespace = settings.RootNamespace };
            XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
            if (settings.OmitDefaultSchema)
            {
                xsn.Add(string.Empty, settings.RootNamespace);
            }
            XmlSerializer xs = new XmlSerializer(data.GetType(), root);
            using (MemoryStream ms = new MemoryStream())
            {
                using (XmlWriter tw = XmlWriter.Create(ms, new XmlWriterSettings { OmitXmlDeclaration = settings.OmitXmlDeclaration, Indent = true, NewLineChars = "\r\n", Encoding = new UTF8Encoding(false), IndentChars = "   " }))
                {
                    xs.Serialize(tw, data, xsn);
                }
                string xml = Encoding.UTF8.GetString(ms.ToArray());
                //对GBK编码特殊处理
                if (settings.GBKEncoding)
                {
                    xml = xml.Replace("utf-8", "GBK");
                }

                return xml;
            }
        }
    }

    /// <summary>
    /// Xml序列化设置
    /// </summary>
    public class XmlSerializeSettings
    {
        /// <summary>
        /// 根节点名称(默认NULL)
        /// </summary>
        public string RootElementName { get; set; }

        /// <summary>
        /// 根节点命名空间(默认NULL)
        /// </summary>
        public string RootNamespace { get; set; }

        /// <summary>
        /// 忽略默认命名空间(默认TRUE)
        /// </summary>
        public bool OmitDefaultSchema { get; set; }

        /// <summary>
        /// 忽略默认xml声明(默认FALSE)
        /// </summary>
        public bool OmitXmlDeclaration { get; set; }

        /// <summary>
        /// 当需要编码方式为GBK时,请将此值设置为TRUE,默认为False
        /// </summary>
        public bool GBKEncoding { get; set; }

        public XmlSerializeSettings()
        {
            this.OmitDefaultSchema = true;
            this.OmitXmlDeclaration = false;
            this.GBKEncoding = false;
        }
    }
}
