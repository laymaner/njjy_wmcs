//=============================================================================
//                                 A220101
//=============================================================================
//
// 堆垛机状态信息。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/1/18
//      创建
//
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using WISH.WCS;
using ASRS.WCS.PLC;
//using WISH.WCS.PLC;
using System;
using System.Collections.Generic;
using WISH.WCS.Device.SrmSocket.WishSrmV10Udt;
using ASRS.WCS.Common.Util;
using System.Security.Policy;
using System.Linq;

namespace WISH.WCS.Device.SrmSocket.WishSrmV10Udt
{
    /// <summary>
    /// 堆垛机模式
    /// </summary>
    //public enum SrmMode
    //{
    ///// <summary>
    //    /// 脱机
    //    /// </summary>
    //    Off =0,

    //    /// <summary>
    //    /// 自动
    //    /// </summary>
    //    Auto = 1,

    //    /// <summary>
    //    /// 手动
    //    /// </summary>
    //    Man = 2

    //    /// <summary>
    //    /// 点动
    //    /// </summary>
    //    //ManJog = 3,

    //    /// <summary>
    //    /// 连动
    //    /// </summary>
    //    //ManCon = 4,

    //    /// <summary>
    //    /// 半自动
    //    /// </summary>
    //    //Semi = 5
    //}

    public class UdtSrmStatus : BaseUdt
    {

        /// <summary>
        /// 操作模式 OF=脱机  AU=自动 MU=手动 
        /// </summary>
        public Signal<string> OperationMode = new Signal<string>("DB39", "0.4");

        /// <summary>
        /// 故障状态 FL=有故障，OK=无故障
        /// </summary>
        public Signal<string> FaultState = new Signal<string>("DB39", "4.4");

        /// <summary>
        /// 故障信息
        /// </summary>
        public Signal<string[]> FaultInfomation = new Signal<string[]>("DB39", "8.60");

        /// <summary>
        /// 堆垛机当前位置
        /// </summary>
        public Signal<string> PositionXY = new Signal<string>("DB39", "68.12");

        /// <summary>
        /// 堆垛机当前行走位置，单位mm
        /// </summary>
        public Signal<Int32> PositionMmX = new Signal<Int32>("DB39", "80");

        /// <summary>
        /// 堆垛机当前升降位置，单位mm
        /// </summary>
        public Signal<Int32> PositionMmY = new Signal<Int32>("DB39", "84");

        /// <summary>
        /// 设备编号（出厂编号）
        /// </summary>
        public Signal<string> SRMID = new Signal<string>("DB39", "88.12");

        /// <summary>
        /// 工位数
        /// </summary>
        public Signal<Int16> LHD_Number = new Signal<Int16>("DB39", "100");

        /// <summary>
        /// 工位1
        /// </summary>
        public Signal<char[]> LHD1State = new Signal<char[]>("DB39", "102.100");

        /// <summary>
        /// 工位2
        /// </summary>
        public Signal<char[]> LHD2State = new Signal<char[]>("DB39", "102.200");


        private List<object> signals;
        public override List<object> GetSignals()
        {
            if (signals == null)
            {
                signals = new List<object>();
                signals.Add(OperationMode);
                signals.Add(FaultState);
                signals.Add(FaultInfomation);
                signals.Add(PositionXY);
                signals.Add(PositionMmX);
                signals.Add(PositionMmY);
                signals.Add(SRMID);
                signals.Add(LHD_Number);
                signals.Add(LHD1State);
                signals.Add(LHD2State);
                LHD2State.DataUpdatedAction = fireDataUpdate;
                //PlcDataUtil.UpdateUdtAddress(signals, db, offset);
            }
            return signals;
        }


        public UdtSrmStatus(string db, int offset)
        {
            DB = db;
            Offset = offset;
        }

        public override string ToJson()
        {
            Dictionary<string, object> jsonDic = new Dictionary<string, object>();
            string[] msgList = FaultInfomation.Value==null ? null : FaultInfomation.Value;
            string alarmInfo = "";
            if (msgList!=null )
            {
                alarmInfo = string.Join(",", msgList).ToSiemensString();
            }
            char[] lhd1 = LHD1State.Value == null ? null : LHD1State.Value;//货叉1
            string forkType = lhd1[0].ToString();//货叉类型A=单伸，B=双伸
            string execStep = "";//任务步骤
            string forkCenter="";//货叉原位,0=原位，1=左，2=右
            string hasgood = "";//有货状态
            string fork1="";
            string tID = "";//任务号
            string cID = "";//子任务号
            if (lhd1!=null )
            {
                forkType = lhd1[0].ToString();//货叉类型A=单伸，B=双伸
                execStep = new string(lhd1, 27, 2);//任务步骤
                forkCenter = new string(lhd1, 20, 1);//货叉原位,0=原位，1=左，2=右
                hasgood = new string(lhd1, 21, 1);//有货状态
                fork1 = new string(lhd1);
                tID = new string(lhd1, 29, 10).Replace('\0', ' ');//任务号
                cID = new string(lhd1, 39, 10);//子任务号
                if (forkType != "A")
                {
                    execStep = new string(lhd1, 32, 2);//任务步骤
                    tID = new string(lhd1, 34, 10).Replace('\0', ' ');//任务号
                    cID = new string(lhd1, 44, 10);//子任务号
                }
            }
            jsonDic.Add("mode", OperationMode.Value.ToSiemensString());
            jsonDic.Add("alarm", FaultState.Value.ToSiemensString());
            jsonDic.Add("alarminfo", alarmInfo);
            jsonDic.Add("location", PositionXY.Value.ToSiemensString());
            jsonDic.Add("X", PositionMmX.Value.ToString());
            jsonDic.Add("Y", PositionMmY.Value.ToString());
            jsonDic.Add("device", SRMID.Value.ToSiemensString());
            jsonDic.Add("forkType", forkType);
            jsonDic.Add("Task", tID);
            jsonDic.Add("cID", cID);
            jsonDic.Add("execStep", execStep);
            jsonDic.Add("forkCenter", forkCenter);
            jsonDic.Add("hasgood", hasgood);
            jsonDic.Add("LHDNo.", LHD_Number.Value.ToString());
            jsonDic.Add("forkOne", fork1);
            //jsonDic.Add("forkTwo", LHD2State.ToDictionary());
            string json = JsonConvert.SerializeObject(jsonDic);
            return json;
        }
    }
}
