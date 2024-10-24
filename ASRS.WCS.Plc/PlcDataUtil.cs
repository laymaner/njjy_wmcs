//=============================================================================
//                                 A220101
//=============================================================================
//
// Plc基础数据类型处理工具类。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/1/9
//      创建
//
//-----------------------------------------------------------------------------

using HslCommunication;
using HslCommunication.Core.Net;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASRS.WCS.Common.Util;

namespace ASRS.WCS.PLC
{
    public class PlcDataUtil
    {
        public static ILog Log { get; set; }


        public static object ReadValue(Plc plc, BaseSignal signal)
        {
            object res = null;
            Type sType = signal.GetType();
            // [0]: DB, [1]: byte, [2]:bit 
            string[] address = signal.Address.Split(".");
            string dbName = address[0];
            int byteOffset = Convert.ToInt16(address[1]);
            int bitOrLength = 0;
            if (address.Length > 2)
                bitOrLength = Convert.ToInt16(address[2]);
            int appendLenth = 0;
            if (address.Length > 3)
                appendLenth = Convert.ToInt16(address[3]);

            PlcDataBlock datablock = plc.GetDataBlock(dbName);
            if (datablock != null && datablock.Data != null)
            {
                try
                {
                    if (typeof(Signal<Int16>) == (sType))
                    {
                        byte[] data = new byte[2];
                        Array.Copy(datablock.Data, byteOffset, data, 0, 2);
                        Array.Reverse(data);
                        // 2 byte 组合
                        ((Signal<Int16>)signal).Value = BitConverter.ToInt16(data);
                    }
                    else if (typeof(Signal<Int32>) == (sType))
                    {
                        byte[] data = new byte[4];
                        Array.Copy(datablock.Data, byteOffset, data, 0, 4);
                        Array.Reverse(data);
                        ((Signal<Int32>)signal).Value = BitConverter.ToInt32(data);
                    }
                    else if (sType == (typeof(Signal<bool>)))
                    {
                        byte byteData = datablock.Data[byteOffset];
                        bool b = (byteData & (byte)Math.Pow(2, bitOrLength)) > 0 ? true : false;
                        ((Signal<bool>)signal).Value = b;
                    }
                    else if (typeof(Signal<byte>) == (sType))
                    {
                        ((Signal<byte>)signal).Value = datablock.Data[byteOffset];
                    }
                    else if (typeof(Signal<string>) == (sType))
                    {
                        byte[] data = new byte[bitOrLength];
                        Array.Copy(datablock.Data, byteOffset, data, 0, bitOrLength);
                        ((Signal<string>)signal).Value =System.Text.Encoding.Default.GetString(data);
                        
                    }
                    else if (typeof(Signal<byte[]>) == (sType))
                    {
                        byte[] data = new byte[bitOrLength];
                        Array.Copy(datablock.Data, byteOffset, data, 0, bitOrLength);
                        ((Signal<byte[]>)signal).Value = data;
                    }
                    else if (typeof(Signal<Int16[]>) == (sType))
                    {
                        byte[] data = new byte[bitOrLength * 2];
                        Array.Copy(datablock.Data, byteOffset, data, 0, bitOrLength * 2);
                        short[] _temp = new short[bitOrLength];
                        ByteToInt16(data, bitOrLength * 2, ref _temp);
                        ((Signal<Int16[]>)signal).Value = _temp;
                    }
                    else if (typeof(Signal<char[]>) == (sType))
                    {
                        byte[] data = new byte[bitOrLength];
                        Array.Copy(datablock.Data, byteOffset, data, 0, bitOrLength);
                        ((Signal<char[]>)signal).Value = (data).Select(x => Convert.ToChar(x)).ToArray();
                    }
                    else if (typeof(Signal<string[]>) == (sType))
                    {
                        string[] strs = new string[bitOrLength];

                        for (int i = 0; i < bitOrLength; i++)
                        {
                            byte[] data = new byte[appendLenth];

                            Array.Copy(datablock.Data, byteOffset + i * appendLenth, data, 0, appendLenth);
                            strs[i] = System.Text.Encoding.Default.GetString(data);
                        }
                        ((Signal<string[]>)signal).Value = strs;
                    }
                    else if (typeof(Signal<UInt16>) == (sType))
                    {
                        byte[] data = new byte[2];
                        Array.Copy(datablock.Data, byteOffset, data, 0, 2);
                        Array.Reverse(data);
                        // 2 byte 组合
                        ((Signal< UInt16>)signal).Value = BitConverter.ToUInt16(data);
                    }
                    else if (typeof(Signal<float>) == (sType))
                    {
                        byte[] data = new byte[4];
                        Array.Copy(datablock.Data, byteOffset, data, 0, 4);
                        Array.Reverse(data);
                        ((Signal<float>)signal).Value = BitConverter.ToSingle(data);
                    }
                    else if (typeof(Signal<char>) == (sType))
                    {
                        ((Signal<char>)signal).Value = (char)datablock.Data[byteOffset];
                    }
                    else if (typeof(Signal<Int32[]>) == (sType))
                    {
                        int[] intVal = new int[bitOrLength];
                        for (int i = 0; i < intVal.Length; i++)
                        {
                            byte[] data = new byte[4];
                            Array.Copy(datablock.Data, byteOffset + 4 * i, data, 0, 4);
                            Array.Reverse(data);
                            intVal[i] = BitConverter.ToInt32(data);
                        }
                        ((Signal<Int32[]>)signal).Value = intVal;
                    }
                    else if (typeof(Signal<DateTime>) == (sType))
                    {
                        byte[] data = new byte[8];
                        Array.Copy(datablock.Data, byteOffset, data, 0, 8);
                        string[] strs = (data).Select(x => x.ToString("X")).ToArray();
                        int i_year = Convert.ToInt32(strs[0]);
                        strs[0] = (i_year <= 89 ? 2000 + i_year : 1900 + i_year).ToString();//年
                        strs[1] = "-" + strs[1].PadLeft(2, '0');//月
                        strs[2] = "-" + strs[2].PadLeft(2, '0');//日
                        strs[3] = " " + strs[3].PadLeft(2, '0');//时
                        strs[4] = ":" + strs[4].PadLeft(2, '0');//分
                        strs[5] = ":" + strs[5].PadLeft(2, '0');//秒
                        strs[6] = "." + strs[6];//秒
                        ((Signal<DateTime>)signal).Value = Convert.ToDateTime(string.Join("", strs, 0, 7));//将前7个byte组合成string并转换成DateTime
                    }
                    //else if (typeof(Signal<TimeSpan>) == (sType))
                    //{
                    //    byte[] data = new byte[4];
                    //    Array.Copy(datablock.Data, byteOffset, data, 0, 4);
                    //    Array.Reverse(data);
                    //    // 2 byte 组合
                    //    int temp = BitConverter.ToInt32(data);
                    //    //换算成秒
                    //    res = Convert.ToSingle(temp);
                    //    TimeSpan ts = new TimeSpan();                        
                    //}                   
                }
                catch (Exception e)
                {
                    Log.Error($"faild to read [addres:{signal.Address} ,datatype:{sType}]!{e.Message} ");
                }
            }
            return res;

        }

        /// <summary>
        /// 写入Plc数据，返回是否写成功。
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="signal"></param>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static bool WriteValue(NetworkDeviceBase plc, BaseSignal signal)
        {
            OperateResult result = null;
            string address = signal?.Address;
            object _value ;
            Type sType = signal.GetType();

            string[] addressx = signal.Address.Split(".");
            string dbName = addressx[0];
            int byteOffset = Convert.ToInt16(addressx[1]);
            int bitOrLength = 0;
            if (addressx.Length > 2)
                bitOrLength = Convert.ToInt16(addressx[2]);
            int appendLenth = 0;
            if (addressx.Length > 3 && typeof(Signal<bool>) == (sType))
                appendLenth = Convert.ToInt16(addressx[3]);

            address = dbName + "." + byteOffset;
            try
            {
                if (typeof(Signal<Int16>) == (sType))
                {
                    _value = ((Signal<Int16>)signal).Value;
                    result = plc.Write(address, Convert.ToInt16(_value));
                }
                else if (typeof(Signal<Int32>) == (sType))
                {
                    _value = ((Signal<Int32>)signal).Value;
                    result = plc.Write(address, Convert.ToUInt32(_value));
                }
                else if (typeof(Signal<bool>) == (sType))
                {
                    _value = ((Signal<bool>)signal).Value;
                    result = plc.Write(address+"."+bitOrLength, Convert.ToBoolean(_value));
                }
                else if (typeof(Signal<byte>) == (sType))
                {
                    _value = ((Signal<byte>)signal).Value;
                    byte[] data = new byte[1];
                    data[0] = (byte)_value;
                    result = plc.Write(address, data);
                }
                else if (typeof(Signal<string>) == (sType))
                {
                    _value = ((Signal<string>)signal).Value;
                    result = plc.Write(address, Convert.ToString(_value));
                }
                else if (typeof(Signal<byte[]>) == (sType))
                {
                    _value = ((Signal<byte[]>)signal).Value;
                    result = plc.Write(address, _value as byte[]);
                }
                else if (typeof(Signal<Int16[]>) == (sType))
                {
                    _value = ((Signal<Int16[]>)signal).Value;
                    result = plc.Write(address, _value as Int16[]);
                }
                else if (typeof(Signal<char[]>) == (sType))
                {
                    _value = ((Signal<char[]>)signal).Value;
                    char[] chars = _value as char[];
                    byte[] bytes = new byte[chars.Length];
                    for (int i = 0; i < chars.Length; i++)
                    {
                        bytes[i] = (byte)chars[i];
                    }
                    result = plc.Write(address, bytes);
                }
                else if (typeof(Signal<string[]>) == (sType))
                {
                    _value = ((Signal<string[]>)signal).Value;
                    string[] strings = _value as string[];
                    for (int i = 0; i < strings.Length; i++)
                    {
                        if (strings[i] == null)
                        {
                            strings[i] = "";
                        }
                        result = plc.Write($"{dbName}.{byteOffset + i * appendLenth}", strings[i]);
                    }
                }
                else if (typeof(Signal<UInt16>) == (sType))
                {
                    _value = ((Signal<UInt16>)signal).Value;
                    result = plc.Write(address, Convert.ToUInt16(_value));
                }
                else if (typeof(Signal<float>) == (sType))
                {
                    _value = ((Signal<float>)signal).Value;
                    result = plc.Write(address, Convert.ToSingle(_value));
                }
                else if (typeof(Signal<char>) == (sType))
                {
                    _value = ((Signal<char>)signal).Value;
                    char c = (char)_value;
                    byte[] bytes = BitConverter.GetBytes(c);
                    byte[] data = new byte[1];
                    data[0] = bytes[0];
                    result = plc.Write(address, data);
                }
                else if (typeof(Signal<Int32[]>) == (sType))
                {
                    _value = ((Signal<Int32[]>)signal).Value;
                    result = plc.Write(address, _value as Int32[]);
                }
                else if (typeof(Signal<DateTime>) == (sType))
                {
                    _value = ((Signal<DateTime>)signal).Value;
                    if (!DateTime.TryParse(_value.ToString(), out DateTime dateTime))
                    {
                        throw new Exception(_value.ToString() + " error converting to DateTime");
                    }
                    byte[] tmp = new byte[8];
                    tmp[0] = Convert.ToByte(dateTime.Year.ToString().Substring(2, 2), 16);//年
                    tmp[1] = Convert.ToByte(dateTime.Month.ToString(), 16);//月
                    tmp[2] = Convert.ToByte(dateTime.Day.ToString(), 16);//日
                    tmp[3] = Convert.ToByte(dateTime.Hour.ToString(), 16);//时
                    tmp[4] = Convert.ToByte(dateTime.Minute.ToString(), 16);//分
                    tmp[5] = Convert.ToByte(dateTime.Second.ToString(), 16);//秒
                    if (dateTime.Millisecond > 0)
                    {
                        tmp[6] = Convert.ToByte(dateTime.Millisecond.ToString().Substring(0, 2), 16);//毫秒前2位
                    }
                    tmp[7] = Convert.ToByte(((int)dateTime.DayOfWeek).ToString(), 16);//星期几
                    result = plc.Write(address, tmp);
                }
                //else if (typeof(Signal<TimeSpan>) == (sType))
                //{
                //    _value = ((Signal<TimeSpan>)signal).Value;
                //    result = plc.Write(address, Convert.ToInt32(_value));
                //}

                if (result?.IsSuccess == false)
                {
                    throw new Exception($"{result.Message}");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"faild to write [address:[{address}],datatype:{sType}, msg:{e.Message}] ");
            }
            return (bool)result?.IsSuccess;
        }

        private static void ByteToInt16(Byte[] arrByte, int nByteCount, ref Int16[] destInt16Arr)
        {
            //按两个字节一个整数解析，前一字节当做整数高位，后一字节当做整数低位
            for (int i = 0; i < nByteCount / 2; i++)
            {
                byte[] data = new byte[2] { arrByte[2 * i + 0], arrByte[2 * i + 1] };
                Array.Reverse(data);
                // 2 byte 组合
                destInt16Arr[i] = BitConverter.ToInt16(data);
            }
        }

        //public static PlcDataType DataTypeTrans(string datatype)
        //{
        //    if (datatype == null)
        //    {
        //        throw new AccessViolationException("datatype is null");
        //    }
        //    datatype = datatype.ToLower();
        //    if (datatype.Equals("Int16".ToLower()))
        //    {
        //        return Model.PlcDataType.Int16;
        //    }
        //    if (datatype.Equals("Int32".ToLower()))
        //    {
        //        return Model.PlcDataType.Int32;
        //    }
        //    if (datatype.Equals("UInt32".ToLower()))
        //    {
        //        return Model.PlcDataType.UInt32;
        //    }
        //    if (datatype.Equals("Bool".ToLower()))
        //    {
        //        return Model.PlcDataType.Bool;
        //    }
        //    if (datatype.Equals("Byte".ToLower()))
        //    {
        //        return Model.PlcDataType.Byte;
        //    }
        //    if (datatype.Equals("String".ToLower()))
        //    {
        //        return Model.PlcDataType.String;
        //    }
        //    if (datatype.Equals("ArrayOfByte".ToLower()))
        //    {
        //        return Model.PlcDataType.ArrayOfByte;
        //    }
        //    if (datatype.Equals("ArrayOfInt16".ToLower()))
        //    {
        //        return Model.PlcDataType.ArrayOfInt16;
        //    }
        //    if (datatype.Equals("DateAndTime".ToLower()))
        //    {
        //        return Model.PlcDataType.DateAndTime;
        //    }
        //    if (datatype.Equals("ArrayOfChar".ToLower()))
        //    {
        //        return Model.PlcDataType.ArrayOfChar;
        //    }
        //    if (datatype.Equals("ArrayOfString".ToLower()))
        //    {
        //        return Model.PlcDataType.ArrayOfString;
        //    }
        //    if (datatype.Equals("UInt16".ToLower()))
        //    {
        //        return Model.PlcDataType.UInt16;
        //    }
        //    if (datatype.Equals("Float".ToLower()))
        //    {
        //        return Model.PlcDataType.Float;
        //    }
        //    if (datatype.Equals("Time".ToLower()))
        //    {
        //        return Model.PlcDataType.Time;
        //    }
        //    if (datatype.Equals("Char".ToLower()))
        //    {
        //        return Model.PlcDataType.Char;
        //    }
        //    if (datatype.Equals("ArrayOfDInt".ToLower()))
        //    {
        //        return Model.PlcDataType.ArrayOfDInt;
        //    }
        //    return Model.PlcDataType.Undefined;
        //}

        /// <summary>
        /// 用于UDT对象更新地址。
        /// </summary>
        /// <param name="signals"></param>
        /// <param name="db"></param>
        /// <param name="offset"></param>
        public static void UpdateUdtAddress(List<object> signals, string db, int offset)
        {
            foreach (object obj in signals)
            {
                if (obj is BaseSignal)
                {
                    BaseSignal signal = (BaseSignal)obj;
                    string[] address = signal.Address.Split(".");
                    string newAddress = "";
                    address[0] = db;
                    int addressOffset = 0;
                    int.TryParse(address[1], out addressOffset);
                    address[1] = Convert.ToString(addressOffset + offset);
                    for (int i = 0; i < address.Length; i++)
                    {
                        if (i == 0)
                            newAddress = address[i];
                        else
                            newAddress = newAddress + "." + address[i];
                    }
                    signal.Address = newAddress;
                }
                else if (obj is BaseUdt)
                {
                    BaseUdt baseUdt = (BaseUdt)obj;
                    baseUdt.DB = db;
                    UpdateUdtAddress(baseUdt.GetSignals(), db, offset + baseUdt.Offset); 
                }

            }
        }
    }
}
