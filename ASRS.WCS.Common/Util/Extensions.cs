using System;
using System.Collections;
using System.Text;

namespace ASRS.WCS.Common.Util
{


    /// <summary>
    /// 自定义扩展函数
    /// </summary>
    public static class MyExtensions
    {
        /// <summary>
        /// 字符串转short
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static Int16 StringToInt16(this string _value)
        {
            if (_value == "" || string.IsNullOrEmpty(_value) || string.IsNullOrWhiteSpace(_value))
            {
                return 0;
            }
            return Convert.ToInt16(_value);
        }

        /// <summary>
        /// 字符串转Int32
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static Int32 StringToInt32(this string _value)
        {
            if (_value == "" || string.IsNullOrEmpty(_value) || string.IsNullOrWhiteSpace(_value))
            {
                return 0;
            }
            return Convert.ToInt32(_value);
        }

        /// <summary>
        /// 比特转Int16
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static int ConvertBytesToInt16(this byte[] bytes)
        {
            return BitConverter.ToInt16(bytes, 0);
        }

        /// <summary>
        /// 比特转Int32
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static int ConvertBytesToInt32(this byte[] bytes)
        {
            return BitConverter.ToInt32(bytes, 0);
        }
        /// <summary>
        /// 比特转字符
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string  ConvertBytesToString(this byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes); 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static bool ConvertBytesToBool(this byte[] bytes)
        {
            bool result = false;
            if (BitConverter.ToInt16(bytes, 0)==1)
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 转换为Siemens的字符，第1个字节为字符串定义长度，第2个字节为本字符串的长度。
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static string ToSiemensString(this string _value)
        {
            if (_value == "" || string.IsNullOrEmpty(_value) || string.IsNullOrWhiteSpace(_value))
            {
                return "";
            }

            string s = _value;
            if (_value != null)
            {
                byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(_value);
                if (byteArray.Length > 0)
                {
                    int length = byteArray[1];
                    if (length == 0)
                    {
                        s = "";
                    }
                    else
                    {
                        if (length <= byteArray.Length - 2)
                        {
                            byte[] newBytes = new byte[length];
                            Array.Copy(byteArray, 2, newBytes, 0, length);
                            s = System.Text.Encoding.ASCII.GetString(newBytes);
                        }
                        else
                        {
                            //s = _value;
                            s=string.Empty;
                            foreach (char c in _value)
                            {
                                int asciiValue = (int)c;
                                if (asciiValue >= 32 && asciiValue <= 126) // 只保留可打印的 ASCII 字符
                                {
                                    s += c;
                                }
                                //else
                                //{
                                //    s += " "; // 将不可打印的字符替换为空格
                                //}
                            }
                        }
                    }
                }



            }
            return s;
        }
    }
}
