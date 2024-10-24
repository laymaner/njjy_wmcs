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

namespace WISH.WCS.Device.SrmSocket.WishSrmSocketUdt
{
    public class UdtSrmSocketWcs : BaseUdt
    {
        /// <summary>
        /// 任务号（流水号）
        /// </summary>
        public SocketSignal<Int16> TaskNo = new SocketSignal<Int16>("DB5","0");
        /// <summary>
        /// 动作类型：联机申请1，联机2，脱机3，取货4，入库申请5，放货6，出库申请7，双重改址8，倒库9，召回10，待机11，故障12，工作13，入库完成14，出库完成15，倒库完成16，申请卸货17，允许卸货18，取货完毕19，放货完毕20，人工取货完成21，清洗卡夹mask信息22.
        /// </summary>
        public SocketSignal<Int16> ActionType = new SocketSignal<Int16>("DB5", "2");

        /// <summary>
        /// 托盘号
        /// </summary>
        public SocketSignal<Int32> PalletBarcode = new SocketSignal<Int32>("DB4", "4");
        //public SocketSignal<string> PalletBarcode = new SocketSignal<string>("DB4", 4);

        /// <summary>
        /// 源排
        /// </summary>
        public SocketSignal<Int16> SourceRow = new SocketSignal<Int16>("DB4", "8");

        /// <summary>
        /// 源列
        /// </summary>
        public SocketSignal<Int16> SourceColumn = new SocketSignal<Int16>("DB4", "10");

        /// <summary>
        /// 源层
        /// </summary>
        public SocketSignal<Int16> SourceLayer = new SocketSignal<Int16>("DB4", "12");
        /// <summary>
        /// 库台号（目的排）
        /// </summary>
        public SocketSignal<Int16> StationNo = new SocketSignal<Int16>("DB4", "14");
        /// <summary>
        /// 目的列
        /// </summary>
        public SocketSignal<Int16> TargetColumn = new SocketSignal<Int16>("DB4", "16");
        /// <summary>
        /// 目的层
        /// </summary>
        public SocketSignal<Int16> TargetLayer = new SocketSignal<Int16>("DB4", "18");

        /// <summary>
        /// 故障号
        /// </summary>
        public SocketSignal<Int16> AlarmCode = new SocketSignal<Int16>("DB4", "20");
        /// <summary>
        /// 站
        /// </summary>
        public SocketSignal<Int16> Station = new SocketSignal<Int16>("DB4", "22");
        /// <summary>
        /// 标志
        /// </summary>
        public SocketSignal<Int16> Sign = new SocketSignal<Int16>("DB4", "24");

        /// <summary>
        /// 校验位
        /// </summary>
        public SocketSignal<Int16> CheckPoint = new SocketSignal<Int16>("DB4", "26");
        /// 写STB
        /// </summary>
        public SocketSignal<Int16> stb = new SocketSignal<Int16>("DB4", "28");
        /// <summary>
        /// 读ACK
        /// </summary>
        public SocketSignal<Int16> ack = new SocketSignal<Int16>("DB4", "30");
        /// <summary>
        /// 晶圆ID
        /// </summary>
        public SocketSignal<string> WaferID = new SocketSignal<string>("DB4", "32.20");
        /// <summary>
        /// 发送的byte[]信息
        /// </summary>
        public SocketSignal<byte[]> SocketByte = new SocketSignal<byte[]>("DB4", "54");


        private List<object> signals;
        public override List<object> GetSignals()
        {
            if (signals == null)
            {
                signals = new List<object>();
                signals.Add(TaskNo);
                signals.Add(ActionType);
                signals.Add(PalletBarcode);
                signals.Add(SourceRow);
                signals.Add(SourceColumn);
                signals.Add(SourceLayer);
                signals.Add(StationNo);
                signals.Add(TargetColumn);
                signals.Add(TargetLayer);
                signals.Add(AlarmCode);
                signals.Add(Station);
                signals.Add(Sign);
                signals.Add(CheckPoint);
                signals.Add(stb);
                signals.Add(ack);
                signals.Add(WaferID);
                signals.Add(SocketByte);
                ack.DataUpdatedAction = fireDataUpdate;
                //PlcDataUtil.UpdateUdtAddress(signals, db, offset);
            }
            return signals;
        }


        public UdtSrmSocketWcs(string db, int offset)
        {
            DB = db;
            Offset = offset;
        }

        public override string ToJson()
        {
            Dictionary<string, object> jsonDic = new Dictionary<string, object>();
            jsonDic.Add("TaskNo", TaskNo.Value);
            jsonDic.Add("ActionType", ActionType.Value);
            jsonDic.Add("PalletBarcode", PalletBarcode.Value);
            jsonDic.Add("SourceRow", SourceRow.Value);
            jsonDic.Add("SourceColumn", SourceColumn.Value);
            jsonDic.Add("SourceLayer", SourceLayer.Value);
            jsonDic.Add("StationNo", StationNo.Value);
            jsonDic.Add("TargetColumn", TargetColumn.Value);
            jsonDic.Add("TargetLayer", TargetLayer.Value);
            jsonDic.Add("AlarmCode", AlarmCode.Value);
            jsonDic.Add("Station", Station.Value);
            jsonDic.Add("Sign", Sign.Value);
            jsonDic.Add("CheckPoint", CheckPoint.Value);
            jsonDic.Add("stb", stb.Value);
            jsonDic.Add("ack", ack.Value);
            jsonDic.Add("WaferID", WaferID.Value.ToSiemensString());
            jsonDic.Add("SocketByte", SocketByte.Value);
            string json = JsonConvert.SerializeObject(jsonDic);
            return json;
        }
    }
}
