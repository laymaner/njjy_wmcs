//=============================================================================
//                                 A220101
//=============================================================================
//
// Wcs->Srm命令通道。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/1/18
//      创建
//
//-----------------------------------------------------------------------------
using Newtonsoft.Json;
using ASRS.WCS.Common.Util;
//using WISH.WCS.PLC;
using ASRS.WCS.PLC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WISH.WCS.Device.SrmSocket.WishSrmV10Udt
{
    public class UdtWcs2Srm : BaseUdt
    {
        /// <summary>
        /// 开始字符
        /// </summary>
        public Signal<string> StartChar = new Signal<string>("DB540", "0.4");
        /// <summary>
        /// 版本
        /// </summary>
        public Signal<string> Version = new Signal<string>("DB540", "4.4");

        /// <summary>
        /// 消息序列，1-9999，未使用
        /// </summary>
        public Signal<Int16> DatagramCounter = new Signal<Int16>("DB540", "8");

        /// <summary>
        /// 消息反馈值，未使用
        /// </summary>
        public Signal<string> ReturnValue = new Signal<string>("DB540", "10.4");

        /// <summary>
        /// 消息长度，未使用
        /// </summary>
        public Signal<Int16> DatagramLength = new Signal<Int16>("DB540", "14");

        /// <summary>
        /// 发送方节点编号
        /// </summary>
        public Signal<string> Sender = new Signal<string>("DB540", "16.8");
        /// <summary>
        /// 接收方节点编号
        /// </summary>
        public Signal<string> Receiver = new Signal<string>("DB540", "24.8");
        /// <summary>
        /// 应答标志，未使用
        /// </summary>
        public Signal<string> FlowControl = new Signal<string>("DB540", "32.4");
        /// <summary>
        /// 消息类型代码
        /// </summary>
        public Signal<string> OperationID = new Signal<string>("DB540", "36.10");

        /// <summary>
        /// 数据体区数量，默认1
        /// </summary>
        public Signal<Int16> OperationBlockCount = new Signal<Int16>("DB540", "46");
        /// <summary>
        /// 数据体区长度，未使用
        /// </summary>
        public Signal<Int16> OperationBlockLength = new Signal<Int16>("DB540", "48");
        /// <summary>
        /// 数据体1
        /// </summary>
        public Signal<char[]> OperationBlock1 = new Signal<char[]>("DB540", "50.200");
        /// <summary>
        /// 数据体2
        /// </summary>
        public Signal<char[]> OperationBlock2 = new Signal<char[]>("DB540", "250.200");
        /// <summary>
        /// 写STB
        /// </summary>
        public Signal<bool> stb = new Signal<bool>("DB540", "450.0");
        /// <summary>
        /// 读ACK
        /// </summary>
        public Signal<bool> ack = new Signal<bool>("DB540", "450.1");

        private List<object> signals;
        public override List<object> GetSignals()
        {
            if (signals == null)
            {
                signals = new List<object>();
                signals.Add(StartChar);
                signals.Add(Version);
                signals.Add(DatagramCounter);
                signals.Add(ReturnValue);
                signals.Add(DatagramLength);
                signals.Add(Sender);
                signals.Add(Receiver);
                signals.Add(FlowControl);
                signals.Add(OperationID);
                signals.Add(OperationBlockCount);
                signals.Add(OperationBlockLength);
                signals.Add(OperationBlock1);
                signals.Add(OperationBlock2);
                signals.Add(stb);
                signals.Add(ack);
                ack.DataUpdatedAction = fireDataUpdate;
                //PlcDataUtil.UpdateUdtAddress(signals, db, offset);
            }
            return signals;
        }

        public UdtWcs2Srm(string db, int offset)
        {
            DB = db;
            Offset = offset;
        }

        public override string ToJson()
        {
            Dictionary<string, object> jsonDic = new Dictionary<string, object>();
            char[] dbOne = OperationBlock1.Value==null ? null : OperationBlock1.Value;
            string tID = "";
            string cID = "";
            string source = "";
            string target = "";
            string db1 = "";
            if (dbOne!=null)
            {
                tID = new string(dbOne, 0, 10).Replace('\0', ' ');//任务号
                cID = new string(dbOne, 10, 10);//子任务号
                source = new string(dbOne, 30, 20);//源地址
                target = new string(dbOne, 50, 20);//目标地址
                db1 = new string(dbOne);
            }
            jsonDic.Add("StartChar", StartChar.Value.ToSiemensString());
            jsonDic.Add("Version", Version.Value.ToSiemensString());
            jsonDic.Add("DataC", DatagramCounter.Value.ToString());
            jsonDic.Add("ReturnV", ReturnValue.Value.ToSiemensString());
            jsonDic.Add("DataL", DatagramLength.Value.ToString());
            jsonDic.Add("Sender", Sender.Value.ToSiemensString());
            jsonDic.Add("Receiver", Receiver.Value.ToSiemensString());
            jsonDic.Add("FlowC", FlowControl.Value.ToSiemensString());
            jsonDic.Add("Task", tID);
            jsonDic.Add("cID", cID);
            jsonDic.Add("source", source);
            jsonDic.Add("target", target);
            jsonDic.Add("OperaID", OperationID.Value.ToSiemensString());
            jsonDic.Add("OperaBC", OperationBlockCount.Value.ToString());
            jsonDic.Add("OperaBL", OperationBlockLength.Value.ToString());
            jsonDic.Add("dbOne", db1);
            //jsonDic.Add("dbTwo", OperationBlock2.Value.ToString());
            jsonDic.Add("stb", stb.Value.ToString());
            jsonDic.Add("ack", ack.Value.ToString());
            string json = JsonConvert.SerializeObject(jsonDic);
            return json;
        }
    }
}
