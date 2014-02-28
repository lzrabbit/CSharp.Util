using System;

namespace CSharp.Util
{
    /// <summary>
    /// 系统类型安全转换处理类
    /// </summary>
    public static class SafeConvert
    {
        public static bool IsDBNull(object obj)
        {
            return Convert.IsDBNull(obj);
        }

        #region Conver To Methods

        public static Guid ToGuid(object obj)
        {
            if (obj != null && obj != DBNull.Value)
            {
                try
                {
                    return new Guid(obj.ToString());
                }
                catch
                {
                    return Guid.Empty;
                }
            }

            return Guid.Empty;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(object obj)
        {
            return ToTimeSpan(obj, TimeSpan.Zero);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(object obj, TimeSpan defaultValue)
        {
            if (obj != null)
                return ToTimeSpan(obj.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(string s, TimeSpan defaultValue)
        {
            TimeSpan result;
            bool success = TimeSpan.TryParse(s, out result);

            return success ? result : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(string s)
        {
            return ToTimeSpan(s, TimeSpan.Zero);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToString(object obj)
        {
            if (obj != null) return obj.ToString();

            return string.Empty;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToString(string s)
        {
            return ToString(s, string.Empty);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultString"></param>
        /// <returns></returns>
        public static string ToString(string s, string defaultString)
        {
            if (s == null) return defaultString;

            return s.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultString"></param>
        /// <returns></returns>
        public static string ToString(object s, string defaultString)
        {
            if (s == null) return defaultString;

            return s.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToDouble(string s, double defaultValue)
        {
            double result;
            bool success = double.TryParse(s, out result);

            return success ? result : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double ToDouble(string s)
        {
            return ToDouble(s, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ToDouble(object obj)
        {
            return ToDouble(obj, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToDouble(object obj, double defaultValue)
        {
            if (obj != null)
                return ToDouble(obj.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float ToSingle(string s, float defaultValue)
        {
            float result;
            bool success = float.TryParse(s, out result);

            return success ? result : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static float ToSingle(string s)
        {
            return ToSingle(s, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static float ToSingle(object obj)
        {
            return ToSingle(obj, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float ToSingle(object obj, float defaultValue)
        {
            if (obj != null)
                return ToSingle(obj.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(string s, decimal defaultValue)
        {
            decimal result;
            bool success = decimal.TryParse(s, out result);

            return success ? result : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static decimal ToDecimal(string s)
        {
            return ToDecimal(s, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object obj)
        {
            return ToDecimal(obj, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object obj, decimal defaultValue)
        {
            if (obj != null)
                return ToDecimal(obj.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ToBoolean(string s, bool defaultValue)
        {
            if (s.TrimStart('0') == "1")
                return true;
            bool result;
            bool success = bool.TryParse(s, out result);

            return success ? result : defaultValue;
        }

        public static bool ToBoolean(string s)
        {
            return ToBoolean(s, false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBoolean(object obj)
        {
            return ToBoolean(obj, false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ToBoolean(object obj, bool defaultValue)
        {
            if (obj != null)
                return ToBoolean(obj.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static char ToChar(string s, char defaultValue)
        {
            char result;
            bool success = char.TryParse(s, out result);

            return success ? result : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static char ToChar(string s)
        {
            return ToChar(s, '\0');
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static char ToChar(object obj)
        {
            return ToChar(obj, '\0');
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static char ToChar(object obj, char defaultValue)
        {
            if (obj != null)
                return ToChar(obj.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static byte ToByte(string s, byte defaultValue)
        {
            byte result;
            bool success = byte.TryParse(s, out result);

            return success ? result : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte ToByte(string s)
        {
            return ToByte(s, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte ToByte(object obj)
        {
            return ToByte(obj, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static byte ToByte(object obj, byte defaultValue)
        {
            if (obj != null)
                return ToByte(obj.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static sbyte ToSByte(string s, sbyte defaultValue)
        {
            sbyte result;
            bool success = sbyte.TryParse(s, out result);

            return success ? result : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static sbyte ToSByte(string s)
        {
            return ToSByte(s, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static sbyte ToSByte(object obj)
        {
            return ToSByte(obj, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static sbyte ToSByte(object obj, sbyte defaultValue)
        {
            if (obj != null)
                return ToSByte(obj.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static short ToInt16(string s, short defaultValue)
        {
            short result;
            bool success = short.TryParse(s, out result);

            return success ? result : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static short ToInt16(string s)
        {
            return ToInt16(s, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static short ToInt16(object obj)
        {
            return ToInt16(obj, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static short ToInt16(object obj, short defaultValue)
        {
            if (obj != null)
                return ToInt16(obj.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ushort ToUInt16(string s, ushort defaultValue)
        {
            ushort result;
            bool success = ushort.TryParse(s, out result);

            return success ? result : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static ushort ToUInt16(string s)
        {
            return ToUInt16(s, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ushort ToUInt16(object obj)
        {
            return ToUInt16(obj, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ushort ToUInt16(object obj, ushort defaultValue)
        {
            if (obj != null)
                return ToUInt16(obj.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt32(string s, int defaultValue)
        {
            int result;
            bool success = int.TryParse(s, out result);

            return success ? result : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt32(string s)
        {
            return ToInt32(s, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt32(object obj)
        {
            return ToInt32(obj, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt32(object obj, int defaultValue)
        {
            if (obj != null)
                return ToInt32(obj.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static uint ToUInt32(string s, uint defaultValue)
        {
            uint result;
            bool success = uint.TryParse(s, out result);

            return success ? result : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static uint ToUInt32(string s)
        {
            return ToUInt32(s, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static uint ToUInt32(object obj)
        {
            return ToUInt32(obj, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static uint ToUInt32(object obj, uint defaultValue)
        {
            if (obj != null)
                return ToUInt32(obj.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToInt64(string s, long defaultValue)
        {
            long result;
            bool success = long.TryParse(s, out result);

            return success ? result : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long ToInt64(string s)
        {
            return ToInt64(s, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ToInt64(object obj)
        {
            return ToInt64(obj, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToInt64(object obj, long defaultValue)
        {
            if (obj != null)
                return ToInt64(obj.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ulong ToUInt64(string s, ulong defaultValue)
        {
            ulong result;
            bool success = ulong.TryParse(s, out result);

            return success ? result : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static ulong ToUInt64(string s)
        {
            return ToUInt64(s, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ulong ToUInt64(object obj)
        {
            return ToUInt64(obj, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ulong ToUInt64(object obj, ulong defaultValue)
        {
            if (obj != null)
                return ToUInt64(obj.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(string s, DateTime defaultValue)
        {
            DateTime result;
            bool success = DateTime.TryParse(s, out result);

            return success ? result : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(string s)
        {
            return ToDateTime(s, new DateTime(1900, 1, 1));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(object obj)
        {
            return ToDateTime(obj, new DateTime(1900, 1, 1));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(object obj, DateTime defaultValue)
        {
            if (obj != null)
                return ToDateTime(obj.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="text"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static object ToEnum(Type enumType, string text, object defaultValue)
        {
            if (Enum.IsDefined(enumType, text))
            {
                return Enum.Parse(enumType, text, false);
            }

            return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static object ToEnum(Type enumType, object obj, object defaultValue)
        {
            if (obj != null)
                return ToEnum(enumType, obj.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static object ToEnum(Type enumType, int index)
        {
            return Enum.ToObject(enumType, index);
        }

        #endregion Conver To Methods
    }
}