//=============================================================================
//                                 A220101
//=============================================================================
//
// 堆垛机->Wcs反馈数据构体。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/1/18
//      创建
//
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using WISH.WCS;
using ASRS.WCS.Common.Util;
//using WISH.WCS.PLC;
using ASRS.WCS.PLC;
using System;
using System.Collections.Generic;

namespace WISH.WCS.Device.SrmSocket.WishSrmV10Udt
{
    public class UdtSrm2Wcs : BaseUdt
    {
        /// <summary>
        /// 开始字符
        /// </summary>
        public Signal<string> StartChar = new Signal<string>("DB541", "0.4");
        /// <summary>
        /// 版本
        /// </summary>
        public Signal<string> Version = new Signal<string>("DB541", "4.4");

        /// <summary>
        /// 消息序列，1-9999，未使用
        /// </summary>
        public Signal<Int16> DatagramCounter = new Signal<Int16>("DB541", "8");

        /// <summary>
        /// 消息反馈值，未使用
        /// </summary>
        public Signal<string> ReturnValue = new Signal<string>("DB541", "10.4");

        /// <summary>
        /// 消息长度，未使用
        /// </summary>
        public Signal<Int16> DatagramLength = new Signal<Int16>("DB541", "14");

        /// <summary>
        /// 发送方节点编号
        /// </summary>
        public Signal<string> Sender = new Signal<string>("DB541", "16.8");
        /// <summary>
        /// 接收方节点编号
        /// </summary>
        public Signal<string> Receiver = new Signal<string>("DB541", "24.8");
        /// <summary>
        /// 应答标志，未使用
        /// </summary>
        public Signal<string> FlowControl = new Signal<string>("DB541", "32.4");
        /// <summary>
        /// 消息类型代码
        /// </summary>
        public Signal<string> OperationID = new Signal<string>("DB541", "36.10");

        /// <summary>
        /// 数据体区数量，默认1
        /// </summary>
        public Signal<Int16> OperationBlockCount = new Signal<Int16>("DB541", "46");
        /// <summary>
        /// 数据体区长度，未使用
        /// </summary>
        public Signal<Int16> OperationBlockLength = new Signal<Int16>("DB541", "48");
        /// <summary>
        /// 数据体1
        /// </summary>
        public Signal<char[]> OperationBlock1 = new Signal<char[]>("DB541", "50.200");
        /// <summary>
        /// 数据体2
        /// </summary>
        public Signal<char[]> OperationBlock2 = new Signal<char[]>("DB541", "250.200");
        /// <summary>
        /// 读STB
        /// </summary>
        public Signal<bool> stb = new Signal<bool>("DB541", "450.0");
        /// <summary>
        /// 写ACK
        /// </summary>
        public Signal<bool> ack = new Signal<bool>("DB541", "450.1");

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
                signals.Add(Sender);
                signals.Add(stb);
                signals.Add(ack);
                ack.DataUpdatedAction = fireDataUpdate;
                //PlcDataUtil.UpdateUdtAddress(signals, db, offset);
            }
            return signals;
        }


        public UdtSrm2Wcs(string db, int offset)
        {
            DB = db;
            Offset = offset;
        }

        public override string ToJson()
        {
            Dictionary<string, object> jsonDic = new Dictionary<string, object>();
            char[] dbOne = OperationBlock1.Value == null ? null : OperationBlock1.Value;
            string taskNo= "";//子任务编号
            string code = "";//返回值
            string db1 = "";
            if (dbOne!=null)
            {
                taskNo = new string(dbOne, 10, 10).Replace('\0', ' ');//子任务编号
                code = new string(dbOne, 70, 2);//返回值
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
            jsonDic.Add("Task&Code", taskNo+"-"+ code);
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
