using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Method
    {
        #region 数据转换方法
        /// <summary>
        /// 转换为字符串类型，如果参数为null，返回空字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToStr(object obj)
        {
            if (obj == null) return "";
            else return obj.ToString();
        }
        /// <summary>
        /// 转换字符串类型，如果参数为null，返回null值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToStrNullable(object obj)
        {
            if (obj == null) return null;
            else return obj.ToString();
        }
        /// <summary>
        /// 转换整数类型，如果参数为null，返回0值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(object obj)
        {
            int result;
            if (obj != null && int.TryParse(obj.ToString(), out result))
                return result;
            else
                return 0;
        }
        /// <summary>
        /// 转换整数类型，如果参数为null，返回null值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int? ToIntNullable(object obj)
        {
            int result;
            if (obj != null && int.TryParse(obj.ToString(), out result))
                return result;
            else
                return null;
        }
        /// <summary>
        /// 转换无符号整数类型，如果参数为null，返回0值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static uint ToUInt(object obj)
        {
            uint result;
            if (obj != null && uint.TryParse(obj.ToString(), out result))
                return result;
            else
                return 0U;
        }
        /// <summary>
        /// 转换无符号整数类型，如果参数为null，返回null值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static uint? ToUIntNullable(object obj)
        {
            uint result;
            if (obj != null && uint.TryParse(obj.ToString(), out result))
                return result;
            else
                return null;
        }
        /// <summary>
        /// 转换64位整数类型，如果参数为null，返回0值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ToLng(object obj)
        {
            long result;
            if (obj != null && long.TryParse(obj.ToString(), out result))
                return result;
            else
                return 0;
        }
        /// <summary>
        /// 转换64位整数类型，如果参数为null，返回null值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long? ToLngNullable(object obj)
        {
            long result;
            if (obj != null && long.TryParse(obj.ToString(), out result))
                return result;
            else
                return null;
        }
        /// <summary>
        /// 转换无符号64位整数类型，如果参数为null，返回0值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ulong ToULng(object obj)
        {
            ulong result;
            if (obj != null && ulong.TryParse(obj.ToString(), out result))
                return result;
            else
                return 0UL;
        }
        /// <summary>
        /// 转换无符号64位整数类型，如果参数为null，返回null值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ulong? ToULngNullable(object obj)
        {
            ulong result;
            if (obj != null && ulong.TryParse(obj.ToString(), out result))
                return result;
            else
                return null;
        }
        /// <summary>
        /// 转换双精度浮点类型，如果参数为null，返回0值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ToDbl(object obj)
        {
            double result;
            if (obj != null && double.TryParse(obj.ToString(), out result))
                return result;
            else
                return 0;
        }
        /// <summary>
        /// 转换双精度浮点类型，如果参数为null，返回null值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double? ToDblNullable(object obj)
        {
            double result;
            if (obj != null && double.TryParse(obj.ToString(), out result))
                return result;
            else
                return null;
        }
        /// <summary>
        /// 转换十进制类型，如果参数为null，返回0值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ToDec(object obj)
        {
            decimal result;
            if (obj != null && decimal.TryParse(obj.ToString(), out result))
                return result;
            else
                return 0M;
        }
        /// <summary>
        /// 转换十进制类型，如果参数为null，返回null值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal? ToDecNullable(object obj)
        {
            decimal result;
            if (obj != null && decimal.TryParse(obj.ToString(), out result))
                return result;
            else
                return null;
        }
        /// <summary>
        /// 将字符串转换为字节数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] ToBytes(string s)
        {
            try { return Encoding.Default.GetBytes(s); }
            catch { return null; }
        }
        /// <summary>
        /// 将字节内容还原成字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToStr(byte[] bytes)
        {
            try { return Encoding.Default.GetString(bytes); }
            catch { return ""; }
        }
        /// <summary>
        /// 将字节数组内容以十六进制字符串显示
        /// </summary>
        /// <param name="bts"></param>
        /// <returns></returns>
        public static string ToHexStr(byte[] bts)
        {
            try
            {
                StringBuilder result = new StringBuilder(bts.Length * 2);
                for (int i = 0; i < bts.Length; i++)
                {
                    result.Append(bts[i].ToString("X2"));
                }
                return result.ToString();
            }
            catch { return ""; }
        }
        /// <summary>
        /// 获取字符串的MD5编码字符串（不可逆）
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetMd5(string s)
        {
            byte[] bytes = ToBytes(s);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] md5Hash = md5.ComputeHash(bytes);
                return ToHexStr(md5Hash);
            }
        }
        /// <summary>
        /// 获取字符串的SHA1编码字符串（不可逆）
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetSha1(string s)
        {
            byte[] bytes = ToBytes(s);
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
            {
                byte[] sha1Hash = sha1.ComputeHash(bytes);
                return ToHexStr(sha1Hash);
            }
        }
        /// <summary>
        /// 返回GUID值
        /// </summary>
        public static string GuidString
        {
            get { return Guid.NewGuid().ToString("N"); }
        }
        /// <summary>
        /// 将对象序列化成字节数组
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ToBytes(object obj)
        {
            if (obj == null) return null;
            using (MemoryStream s = new MemoryStream())
            {
                IFormatter f = new BinaryFormatter();
                f.Serialize(s, obj);
                return s.GetBuffer();
            }
        }
        /// <summary>
        /// 将字节数组反序列化成对象
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static object ToObject(byte[] bytes)
        {
            using (MemoryStream s = new MemoryStream(bytes))
            {
                IFormatter f = new BinaryFormatter();
                return f.Deserialize(s);
            }
        }
        /// <summary>
        /// 克隆对象
        /// </summary>
        /// <param name="obj">要克隆的对象</param>
        /// <returns></returns>
        public static object Clone(object obj)
        {
            if (obj == null) return null;
            byte[] bytes = ToBytes(obj);
            return ToObject(bytes);
        }
        /// <summary>
        /// 交换
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="x">要交换的第一个数</param>
        /// <param name="y">要交换的第二个数</param>
        public static void Swap<T>(ref T x, ref T y)
        {
            T temp = x;
            x = y;
            y = temp;
        }
        #endregion
    }
}
