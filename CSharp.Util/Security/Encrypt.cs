using System;
using System.Security.Cryptography;
using System.Text;

namespace CSharp.Util.Security
{
    /// <summary>
    /// 常用加密算法
    /// </summary>
    public sealed class Encrypt
    {
        #region Base64加密

        /// <summary>
        /// 将字符串进行Base64编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Base64Encrypt(string str)
        {
            string result = string.Empty;

            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                result = System.Convert.ToBase64String(bytes);
            }
            catch
            {
                result = str;
            }
            return result;
        }

        /// <summary>
        /// 将字符串进行Base64解码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Base64Decrypt(string str)
        {
            string result = string.Empty;
            try
            {
                byte[] bytes = System.Convert.FromBase64String(str);
                result = Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                //base 64 字符数组为null
                result = str;
            }

            return result;
        }

        #endregion Base64加密

        #region MD5加密

        /// <summary>
        /// 将字符串进行Md5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(string str)
        {
            return MD5(str, Encoding.UTF8);
        }

        /// <summary>
        /// 将字符串进行Md5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(string str, Encoding encode)
        {
            if (string.IsNullOrEmpty(str)) return str;
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] buffer = md5.ComputeHash(encode.GetBytes(str));
            //string encoded = BitConverter.ToString(buffer).Replace("-", "");
            //return encoded.ToLower();
            StringBuilder sb = new StringBuilder();
            foreach (var b in buffer)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        #endregion MD5加密

        #region SHA1加密

        /// <summary>
        /// 将字符串进行SHA1加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SHA1(string str)
        {
            System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
            string encoded = BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(str))).Replace("-", "");
            return encoded;
        }

        #endregion SHA1加密

        #region DES加密

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="sInputString">输入字符</param>
        /// <param name="sKey">Key(8位长度字符串)</param>
        /// <param name="IV">偏移向量(8位长度字符串)</param>
        /// <returns>加密结果</returns>
        public static string DesEncrypt(string str, string key, string IV)
        {
            byte[] data = Encoding.UTF8.GetBytes(str);
            byte[] result;
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = Encoding.UTF8.GetBytes(key);
            DES.IV = Encoding.UTF8.GetBytes(IV);
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="sInputString">输入字符</param>
        /// <param name="sKey">Key(8位长度字符串)</param>
        /// <param name="IV">偏移向量(8位长度字符串)</param>
        /// <returns>解密结果</returns>
        public static string DesDecrypt(string str, string key, string IV)
        {
            byte[] data = Convert.FromBase64String(str);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = Encoding.UTF8.GetBytes(key);
            DES.IV = Encoding.UTF8.GetBytes(IV);
            ICryptoTransform desencrypt = DES.CreateDecryptor();
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return Encoding.UTF8.GetString(result);
        }

        #endregion DES加密

        #region 3Des加密

        /// <summary>
        /// 3Des加密
        /// </summary>
        /// <param name="str">要进行加密的字符串(内部对字符串采用utf8编码)</param>
        /// <param name="key">加密key(24位字符串)</param>
        /// <param name="IV">偏移向量(8位字符串)</param>
        /// <returns>base64编码的字符串</returns>
        public static string TripleDesEncrypt(string str, string key, string IV)
        {
            TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();

            //设置偏移向量
            tdsp.IV = Encoding.UTF8.GetBytes(IV);
            //设置加密密匙
            tdsp.Key = System.Text.Encoding.UTF8.GetBytes(key);
            //设置加密算法运算模式为ECB(保持和java兼容)
            tdsp.Mode = CipherMode.CBC;
            //设置加密算法的填充模式为PKCS7(保持和java兼容)
            tdsp.Padding = PaddingMode.PKCS7;

            //对输入字符串采用utf8编码获取字节
            byte[] data = Encoding.UTF8.GetBytes(str);

            //加密后采用base64编码生成加密串
            ICryptoTransform ct = tdsp.CreateEncryptor();
            string result = Convert.ToBase64String(ct.TransformFinalBlock(data, 0, data.Length));
            return result;
        }

        /// <summary>
        /// 3Des解密
        /// </summary>
        /// <param name="str">要进行解密base64字符串</param>
        /// <param name="key">解密key(24位字符串)</param>
        /// <param name="IV">偏移向量(8位字符串)</param>
        /// <returns>原始字符串(内部对字符串采用utf8进行解码)</returns>
        public static string TripleDesDecrypt(string str, string key, string IV)
        {
            TripleDESCryptoServiceProvider tdsp = new TripleDESCryptoServiceProvider();
            tdsp.IV = Encoding.UTF8.GetBytes(IV);
            //设置偏移向量
            tdsp.IV = Encoding.UTF8.GetBytes(IV);
            //设置加密密匙
            tdsp.Key = System.Text.Encoding.UTF8.GetBytes(key);
            //设置加密算法运算模式为ECB(保持和java兼容)
            tdsp.Mode = CipherMode.CBC;
            //设置加密算法的填充模式为PKCS7(保持和java兼容)
            tdsp.Padding = PaddingMode.PKCS7;

            //加密串用base64编码,需要采用base64方式解析为字节
            byte[] data = Convert.FromBase64String(str);
            ICryptoTransform ct = tdsp.CreateDecryptor();
            //用utf8编码还原原始字符串
            string result = Encoding.UTF8.GetString(ct.TransformFinalBlock(data, 0, data.Length));
            return result;
        }

        #endregion 3Des加密

        #region AES加解密

        /// <summary>
        ///  AES 加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AesEncrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = System.Security.Cryptography.CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };

            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        ///  AES 解密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AesDecrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Convert.FromBase64String(str);

            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = System.Security.Cryptography.CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };

            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }

        #endregion AES加解密
    }
}