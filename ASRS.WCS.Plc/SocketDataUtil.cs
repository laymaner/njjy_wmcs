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
using System.Net.Sockets;
using Newtonsoft.Json.Linq;

namespace ASRS.WCS.PLC
{
    public class SocketDataUtil
    {
        public static ILog Log { get; set; }
        public static int isSend { get; set; } = 0;

        public static object ReadValue(Sockets sockets, BaseSignal signal)
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

            PlcDataBlock datablock = sockets.GetDataBlock(dbName);
            if (datablock != null && datablock.Data != null)
            {
                try
                {
                    if (typeof(SocketSignal<Int16>) == (sType))
                    {
                        byte[] data = new byte[2];
                        Array.Copy(datablock.Data, byteOffset, data, 0, 2);
                        //Array.Reverse(data);
                        // 2 byte 组合
                        ((SocketSignal<Int16>)signal).Value = BitConverter.ToInt16(data, 0);
                    }
                    else if (typeof(SocketSignal<Int32>) == (sType))
                    {
                        byte[] data = new byte[4];
                        Array.Copy(datablock.Data, byteOffset, data, 0, 4);
                        //Array.Reverse(data);
                        ((SocketSignal<Int32>)signal).Value = BitConverter.ToInt32(data);
                    }
                    else if (sType == (typeof(SocketSignal<bool>)))
                    {
                        byte byteData = datablock.Data[byteOffset];
                        bool b = (byteData & (byte)Math.Pow(2, bitOrLength)) > 0 ? true : false;
                        ((SocketSignal<bool>)signal).Value = b;
                    }
                    else if (typeof(SocketSignal<byte>) == (sType))
                    {
                        ((SocketSignal<byte>)signal).Value = datablock.Data[byteOffset];
                    }
                    else if (typeof(SocketSignal<string>) == (sType))
                    {
                        byte[] data = new byte[bitOrLength];
                        Array.Copy(datablock.Data, byteOffset, data, 0, bitOrLength);
                        ((SocketSignal<string>)signal).Value = System.Text.Encoding.Default.GetString(data);
                        //((SocketSignal<string>)signal).Value = System.Text.Encoding.ASCII.GetString(data); ;
                        
                    }
                    else if (typeof(SocketSignal<byte[]>) == (sType))
                    {
                        byte[] data = new byte[bitOrLength];
                        Array.Copy(datablock.Data, byteOffset, data, 0, bitOrLength);
                        ((SocketSignal<byte[]>)signal).Value = data;
                    }
                    else if (typeof(SocketSignal<Int16[]>) == (sType))
                    {
                        byte[] data = new byte[bitOrLength * 2];
                        Array.Copy(datablock.Data, byteOffset, data, 0, bitOrLength * 2);
                        short[] _temp = new short[bitOrLength];
                        ByteToInt16(data, bitOrLength * 2, ref _temp);
                        ((SocketSignal<Int16[]>)signal).Value = _temp;
                    }
                    else if (typeof(SocketSignal<char[]>) == (sType))
                    {
                        byte[] data = new byte[bitOrLength];
                        Array.Copy(datablock.Data, byteOffset, data, 0, bitOrLength);
                        ((SocketSignal<char[]>)signal).Value = (data).Select(x => Convert.ToChar(x)).ToArray();
                    }
                    else if (typeof(SocketSignal<string[]>) == (sType))
                    {
                        string[] strs = new string[bitOrLength];

                        for (int i = 0; i < bitOrLength; i++)
                        {
                            byte[] data = new byte[appendLenth];

                            Array.Copy(datablock.Data, byteOffset + i * appendLenth, data, 0, appendLenth);
                            strs[i] = System.Text.Encoding.Default.GetString(data);
                        }
                        ((SocketSignal<string[]>)signal).Value = strs;
                    }
                    else if (typeof(SocketSignal<UInt16>) == (sType))
                    {
                        byte[] data = new byte[2];
                        Array.Copy(datablock.Data, byteOffset, data, 0, 2);
                        Array.Reverse(data);
                        // 2 byte 组合
                        ((SocketSignal< UInt16>)signal).Value = BitConverter.ToUInt16(data);
                    }
                    else if (typeof(SocketSignal<float>) == (sType))
                    {
                        byte[] data = new byte[4];
                        Array.Copy(datablock.Data, byteOffset, data, 0, 4);
                        Array.Reverse(data);
                        ((SocketSignal<float>)signal).Value = BitConverter.ToSingle(data);
                    }
                    else if (typeof(SocketSignal<char>) == (sType))
                    {
                        ((SocketSignal<char>)signal).Value = (char)datablock.Data[byteOffset];
                    }
                    else if (typeof(SocketSignal<Int32[]>) == (sType))
                    {
                        int[] intVal = new int[bitOrLength];
                        for (int i = 0; i < intVal.Length; i++)
                        {
                            byte[] data = new byte[4];
                            Array.Copy(datablock.Data, byteOffset + 4 * i, data, 0, 4);
                            Array.Reverse(data);
                            intVal[i] = BitConverter.ToInt32(data);
                        }
                        ((SocketSignal<Int32[]>)signal).Value = intVal;
                    }
                    else if (typeof(SocketSignal<DateTime>) == (sType))
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
                        ((SocketSignal<DateTime>)signal).Value = Convert.ToDateTime(string.Join("", strs, 0, 7));//将前7个byte组合成string并转换成DateTime
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
        public static bool WriteValue(Socket clientSocket, BaseSignal signal)
        {
            int res = 0;
            bool result = false;
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
            if (addressx.Length > 3 && typeof(SocketSignal<bool>) == (sType))
                appendLenth = Convert.ToInt16(addressx[3]);

            address = dbName + "." + byteOffset;
            try
            {
                if (typeof(SocketSignal<Int16>) == (sType))
                {
                    _value = ((SocketSignal<Int16>)signal).Value;
                    byte[] bytes = Int16ToByteArray(Convert.ToInt16(_value));
                    //res = clientSocket.Send(bytes);
                    SendDataAsync(bytes, clientSocket);
                    res = isSend;
                }
                else if (typeof(SocketSignal<Int32>) == (sType))
                {
                    _value = ((SocketSignal<Int32>)signal).Value;
                    byte[] bytes = Int32ToByteArray(Convert.ToInt32(_value));
                    //res = clientSocket.Send(bytes);
                    SendDataAsync(bytes, clientSocket);
                    res = isSend;
                }
                else if (typeof(SocketSignal<bool>) == (sType))
                {
                    _value = ((SocketSignal<bool>)signal).Value;
                    byte[] bytes = BoolToByteArray(Convert.ToBoolean(_value));
                    //res = clientSocket.Send(bytes);
                    SendDataAsync(bytes, clientSocket);
                    res = isSend;
                }
                else if (typeof(SocketSignal<byte>) == (sType))
                {
                    _value = ((SocketSignal<byte>)signal).Value;
                    byte[] data = new byte[1];
                    //data[0] = (byte)_value;
                    //result = plc.Write(address, data);
                    //res = clientSocket.Send(data);
                    SendDataAsync(data, clientSocket);
                    res = isSend;
                }
                else if (typeof(SocketSignal<string>) == (sType))
                {
                    _value = ((SocketSignal<string>)signal).Value;
                    byte[] bytes = StringToByteArray(Convert.ToString(_value));
                    //res = clientSocket.Send(bytes);
                    SendDataAsync(bytes, clientSocket);
                    res = isSend;
                }
                else if (typeof(SocketSignal<byte[]>) == (sType))
                {
                    _value = ((SocketSignal<byte[]>)signal).Value;
                    byte[] bytes = _value as byte[];
                    //result = plc.Write(address, _value as byte[]);
                    //res = clientSocket.Send(bytes);
                    SendDataAsync(bytes, clientSocket);
                    res = isSend;
                }
                else if (typeof(SocketSignal<Int16[]>) == (sType))
                {
                    _value = ((SocketSignal<Int16[]>)signal).Value;
                    byte[] bytes = Int16ArrayToByteArray(_value as Int16[]);
                    res = clientSocket.Send(bytes);
                    SendDataAsync(bytes, clientSocket);
                    res = isSend;

                }
                else if (typeof(SocketSignal<char[]>) == (sType))
                {
                    _value = ((SocketSignal<char[]>)signal).Value;
                    char[] chars = _value as char[];
                    byte[] bytes = new byte[chars.Length];
                    for (int i = 0; i < chars.Length; i++)
                    {
                        bytes[i] = (byte)chars[i];
                    }
                    //res = clientSocket.Send(bytes);
                    SendDataAsync(bytes, clientSocket);
                    res = isSend;
                }
                else if (typeof(SocketSignal<string[]>) == (sType))
                {
                    _value = ((SocketSignal<string[]>)signal).Value;
                    string[] strings = _value as string[];
                    //for (int i = 0; i < strings.Length; i++)
                    //{
                    //    if (strings[i] == null)
                    //    {
                    //        strings[i] = "";
                    //    }
                    //    result = plc.Write($"{dbName}.{byteOffset + i * appendLenth}", strings[i]);
                    //}
                    byte[] bytes = StringArrayToByteArray(strings);
                    //res = clientSocket.Send(bytes);
                    SendDataAsync(bytes, clientSocket);
                    res = isSend;
                }
                else if (typeof(SocketSignal<UInt16>) == (sType))
                {
                    _value = ((SocketSignal<UInt16>)signal).Value;
                    byte[] bytes = UInt16ToByteArray(Convert.ToUInt16(_value));
                    //res = clientSocket.Send(bytes);
                    SendDataAsync(bytes, clientSocket);
                    res = isSend;
                }
                else if (typeof(SocketSignal<float>) == (sType))
                {
                    _value = ((SocketSignal<float>)signal).Value;
                    byte[] bytes = FloatToByteArray(Convert.ToSingle(_value));
                    //res = clientSocket.Send(bytes);
                    SendDataAsync(bytes, clientSocket);
                    res = isSend;
                }
                else if (typeof(SocketSignal<char>) == (sType))
                {
                    _value = ((SocketSignal<char>)signal).Value;
                    char c = (char)_value;
                    byte[] bytes = BitConverter.GetBytes(c);
                    byte[] data = new byte[1];
                    data[0] = bytes[0];
                    //res = clientSocket.Send(data);
                    SendDataAsync(bytes, clientSocket);
                    res = isSend;
                }
                else if (typeof(SocketSignal<UInt32>) == (sType))
                {
                    _value = ((SocketSignal<UInt32>)signal).Value;
                    byte[] bytes = UInt32ToByteArray(Convert.ToUInt32(_value));
                    //res = clientSocket.Send(bytes);
                    SendDataAsync(bytes, clientSocket);
                    res = isSend;
                }
                else if (typeof(SocketSignal<DateTime>) == (sType))
                {
                    _value = ((SocketSignal<DateTime>)signal).Value;
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
                    //res = clientSocket.Send(tmp);
                    SendDataAsync(tmp, clientSocket);
                    res = isSend;
                }
                //else if (typeof(Signal<TimeSpan>) == (sType))
                //{
                //    _value = ((Signal<TimeSpan>)signal).Value;
                //    result = plc.Write(address, Convert.ToInt32(_value));
                //}

                if (res > 0)
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                throw new Exception($"faild to write [address:[{address}],datatype:{sType}, msg:{e.Message}] ");
            }
            return result;
        }
        public static void SendDataAsync(byte[] buffer, Socket clientSocket)
        {
            clientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendCallback), clientSocket);
        }

        /// <summary>
        /// 结束发送
        /// </summary>
        /// <param name="ar"></param>
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
                if (bytesSent > 0)
                {
                    isSend= bytesSent;
                }
                else
                {
                    isSend= bytesSent;
                }
                Console.WriteLine($"Sent {bytesSent} bytes to the server.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending data: {ex.Message}");
            }
        }

        /// <summary>
        /// 写入Socket数据，返回是否写成功。
        /// </summary>
        /// <param name="clientSocket"></param>
        /// <param name="signal"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool WriteSocketValue(Socket clientSocket, SocketBaseSignal signal)
        {
            int res = 0;
            bool result = false;
            res = clientSocket.Send(signal.signSocket);
            if (res > 0)
            {
                result = true;
            }
            return result;
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
        /// <summary>
        /// Int16转Byte[]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static byte[] Int16ToByteArray(short value)
        {
            return BitConverter.GetBytes(value);
        }

        /// <summary>
        /// Int32转Byte[]
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static byte[] Int32ToByteArray(Int32 num)
        {
            return BitConverter.GetBytes(num);
        }

        /// <summary>
        /// Bool转Byte[]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static byte[] BoolToByteArray(bool value)
        {
            return BitConverter.GetBytes(value);
        }

        /// <summary>
        /// string转Byte[]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static byte[] StringToByteArray(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        /// Int16[]转Byte[]
        /// </summary>
        /// <param name="int16Array"></param>
        /// <returns></returns>
        private static byte[] Int16ArrayToByteArray(short[] int16Array)
        {
            byte[] byteArray = new byte[int16Array.Length * sizeof(short)];

            Buffer.BlockCopy(int16Array, 0, byteArray, 0, byteArray.Length);

            return byteArray;
        }

        /// <summary>
        /// string[]转byte[]
        /// </summary>
        /// <param name="strArray"></param>
        /// <returns></returns>
        private static byte[] StringArrayToByteArray(string[] strArray)
        {
            List<byte> result = new List<byte>();

            foreach (string str in strArray)
            {
                byte[] strBytes = Encoding.UTF8.GetBytes(str);
                result.AddRange(strBytes);
            }

            return result.ToArray();
        }

        /// <summary>
        /// UInt16转byte[]
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static byte[] UInt16ToByteArray(ushort num)
        {
            return BitConverter.GetBytes(num);
        }

        /// <summary>
        /// float转byte[]
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static byte[] FloatToByteArray(float num)
        {
            return BitConverter.GetBytes(num);
        }

        /// <summary>
        /// UInt32转byte[]
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static byte[] UInt32ToByteArray(uint num)
        {
            return BitConverter.GetBytes(num);
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
