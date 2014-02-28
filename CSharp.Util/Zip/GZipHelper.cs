using System.IO;
using System.IO.Compression;

namespace Helpers
{
    /// <summary>
    /// Gzip压缩辅助类(不依赖第三方类库)
    /// </summary>
    public class GZipHelper
    {
        /// <summary>
        /// 使用gzip压缩二进制数据并返回压缩后的数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (GZipStream gZipStream = new GZipStream(stream, CompressionMode.Compress))
                {
                    gZipStream.Write(data, 0, data.Length);
                }
                return stream.ToArray();
            }
        }

        /// <summary>
        /// 使用gzip压缩指定文件(默认编码为utf-8)
        /// </summary>
        /// <param name="srcFile">待压缩的文件路径</param>
        /// <param name="dstFile">压缩后的文件路径</param>
        public static void Compress(string srcFile, string dstFile)
        {
            using (FileStream dest = new FileStream(dstFile, FileMode.Create))
            {
                using (FileStream source = File.OpenRead(srcFile))
                {
                    using (GZipStream gzip = new GZipStream(dest, CompressionMode.Compress))
                    {
                        Pump(source, gzip);
                    }
                }
            }
        }

        /// <summary>
        /// 使用gzip解压缩指定文件(默认编码为utf-8)
        /// </summary>
        /// <param name="srcFile">待解压缩的文件路径</param>
        /// <param name="dstFile">解压缩后的文件路径</param>
        public static void Decompress(string srcFile, string dstFile)
        {
            using (FileStream dest = new FileStream(dstFile, FileMode.Create))
            {
                using (FileStream source = File.OpenRead(srcFile))
                {
                    using (GZipStream gzip = new GZipStream(source, CompressionMode.Decompress))
                    {
                        Pump(gzip, dest);
                    }
                }
            }
        }

        private static void Pump(Stream input, Stream output)
        {
            byte[] bytes = new byte[4096];
            int n;
            while ((n = input.Read(bytes, 0, bytes.Length)) != 0)
            {
                output.Write(bytes, 0, n);
            }
        }
    }
}