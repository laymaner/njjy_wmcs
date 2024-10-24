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
using WISH.WCS.Device.SrmSocket.WishSrmSocketUdt;
using ASRS.WCS.Common.Util;
using System.Security.Policy;
using System.Linq;

namespace WISH.WCS.Device.SrmSocket.WishSrmSocketUdt
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

    public class UdtSrmSocketStatus : BaseUdt
    {
        /// <summary>
        /// 重报警01
        /// </summary>
        public SocketSignal<Int16> zr45000 = new SocketSignal<Int16>("DB6", "0");
        /// <summary>
        /// 重报警02
        /// </summary>
        public SocketSignal<Int16> zr45001 = new SocketSignal<Int16>("DB6", "2");
        /// <summary>
        /// 重报警03
        /// </summary>
        public SocketSignal<Int16> zr45002 = new SocketSignal<Int16>("DB6", "4");
        /// <summary>
        /// 重报警04
        /// </summary>
        public SocketSignal<Int16> zr45003 = new SocketSignal<Int16>("DB6","6");
        /// <summary>
        /// 重报警05
        /// </summary>
        public SocketSignal<Int16> zr45004 = new SocketSignal<Int16>("DB6", "8");
        /// <summary>
        /// 重报警06
        /// </summary>
        public SocketSignal<Int16> zr45005 = new SocketSignal<Int16>("DB6", "10");
        /// <summary>
        /// 重报警07
        /// </summary>
        public SocketSignal<Int16> zr45006 = new SocketSignal<Int16>("DB6", "12");
        /// <summary>
        /// 重报警08
        /// </summary>
        public SocketSignal<Int16> zr45007 = new SocketSignal<Int16>("DB6", "14");
        /// <summary>
        /// 重报警09
        /// </summary>
        public SocketSignal<Int16> zr45008 = new SocketSignal<Int16>("DB6", "16");
        /// <summary>
        /// 重报警10
        /// </summary>
        public SocketSignal<Int16> zr45009 = new SocketSignal<Int16>("DB6", "18");
        /// <summary>
        /// 重报警11
        /// </summary>
        public SocketSignal<Int16> zr45010 = new SocketSignal<Int16>("DB6","20");
        /// <summary>
        /// 重报警12
        /// </summary>
        public SocketSignal<Int16> zr45011 = new SocketSignal<Int16>("DB6","22");
        /// <summary>
        /// 重报警13
        /// </summary>
        public SocketSignal<Int16> zr45012 = new SocketSignal<Int16>("DB6","24");
        /// <summary>
        /// 重报警14
        /// </summary>
        public SocketSignal<Int16> zr45013 = new SocketSignal<Int16>("DB6","26");
        /// <summary>
        /// 重报警15
        /// </summary>
        public SocketSignal<Int16> zr45014 = new SocketSignal<Int16>("DB6","28");
        /// <summary>
        /// 重报警16
        /// </summary>
        public SocketSignal<Int16> zr45015 = new SocketSignal<Int16>("DB6","30");
        /// <summary>
        /// 重报警17
        /// </summary>
        public SocketSignal<Int16> zr45016 = new SocketSignal<Int16>("DB6","32");
        /// <summary>
        /// 重报警18
        /// </summary>
        public SocketSignal<Int16> zr45017 = new SocketSignal<Int16>("DB6","34");
        /// <summary>
        /// 重报警19
        /// </summary>
        public SocketSignal<Int16> zr45018 = new SocketSignal<Int16>("DB6","36");
        /// <summary>
        /// 重报警20
        /// </summary>
        public SocketSignal<Int16> zr45019 = new SocketSignal<Int16>("DB6","38");
        /// <summary>
        /// 轻报警01
        /// </summary>
        public SocketSignal<Int16> zr45020 = new SocketSignal<Int16>("DB6","40");
        /// <summary>
        /// 轻报警02
        /// </summary>
        public SocketSignal<Int16> zr45021 = new SocketSignal<Int16>("DB6","42");
        /// <summary>
        /// 轻报警03
        /// </summary>
        public SocketSignal<Int16> zr45022 = new SocketSignal<Int16>("DB6","44");
        /// <summary>
        /// 轻报警04
        /// </summary>
        public SocketSignal<Int16> zr45023 = new SocketSignal<Int16>("DB6","46");
        /// <summary>
        /// 轻报警05
        /// </summary>
        public SocketSignal<Int16> zr45024 = new SocketSignal<Int16>("DB6","48");
        /// <summary>
        /// 轻报警06
        /// </summary>
        public SocketSignal<Int16> zr45025 = new SocketSignal<Int16>("DB6","50");
        /// <summary>
        /// 轻报警07
        /// </summary>
        public SocketSignal<Int16> zr45026 = new SocketSignal<Int16>("DB6","52");
        /// <summary>
        /// 轻报警08
        /// </summary>
        public SocketSignal<Int16> zr45027 = new SocketSignal<Int16>("DB6","54");
        /// <summary>
        /// 轻报警09
        /// </summary>
        public SocketSignal<Int16> zr45028 = new SocketSignal<Int16>("DB6","56");
        /// <summary>
        /// 轻报警10
        /// </summary>
        public SocketSignal<Int16> zr45029 = new SocketSignal<Int16>("DB6","58");
        /// <summary>
        /// 轻报警11
        /// </summary>
        public SocketSignal<Int16> zr45030 = new SocketSignal<Int16>("DB6","60");
        /// <summary>
        /// 轻报警12
        /// </summary>
        public SocketSignal<Int16> zr45031 = new SocketSignal<Int16>("DB6","62");
        /// <summary>
        /// 轻报警13
        /// </summary>
        public SocketSignal<Int16> zr45032 = new SocketSignal<Int16>("DB6","64");
        /// <summary>
        /// 轻报警14
        /// </summary>
        public SocketSignal<Int16> zr45033 = new SocketSignal<Int16>("DB6","66");
        /// <summary>
        /// 轻报警15
        /// </summary>
        public SocketSignal<Int16> zr45034 = new SocketSignal<Int16>("DB6","68");
        /// <summary>
        /// 轻报警16
        /// </summary>
        public SocketSignal<Int16> zr45035 = new SocketSignal<Int16>("DB6","70");
        /// <summary>
        /// 轻报警17
        /// </summary>
        public SocketSignal<Int16> zr45036 = new SocketSignal<Int16>("DB6","72");
        /// <summary>
        /// 轻报警18
        /// </summary>
        public SocketSignal<Int16> zr45037 = new SocketSignal<Int16>("DB6","74");
        /// <summary>
        /// 轻报警19
        /// </summary>
        public SocketSignal<Int16> zr45038 = new SocketSignal<Int16>("DB6","76");
        /// <summary>
        /// 轻报警20
        /// </summary>
        public SocketSignal<Int16> zr45039 = new SocketSignal<Int16>("DB6","78");


        private List<object> signals;
        public override List<object> GetSignals()
        {
            if (signals == null)
            {
                signals = new List<object>();
                signals.Add(zr45000);
                signals.Add(zr45001);
                signals.Add(zr45002);
                signals.Add(zr45003);
                signals.Add(zr45004);
                signals.Add(zr45005);
                signals.Add(zr45006);
                signals.Add(zr45007);
                signals.Add(zr45008);
                signals.Add(zr45009);
                signals.Add(zr45010);
                signals.Add(zr45011);
                signals.Add(zr45012);
                signals.Add(zr45013);
                signals.Add(zr45014);
                signals.Add(zr45015);
                signals.Add(zr45016);
                signals.Add(zr45017);
                signals.Add(zr45018);
                signals.Add(zr45019);
                signals.Add(zr45021);
                signals.Add(zr45022);
                signals.Add(zr45023);
                signals.Add(zr45024);
                signals.Add(zr45025);
                signals.Add(zr45026);
                signals.Add(zr45027);
                signals.Add(zr45028);
                signals.Add(zr45029);
                signals.Add(zr45030);
                signals.Add(zr45031);
                signals.Add(zr45032);
                signals.Add(zr45033);
                signals.Add(zr45034);
                signals.Add(zr45035);
                signals.Add(zr45036);
                signals.Add(zr45037);
                signals.Add(zr45038);
                signals.Add(zr45039);
                zr45039.DataUpdatedAction = fireDataUpdate;
                //PlcDataUtil.UpdateUdtAddress(signals, db, offset);
            }
            return signals;
        }


        public UdtSrmSocketStatus(string db, int offset)
        {
            DB = db;
            Offset = offset;
        }

        public override string ToJson()
        {
            Dictionary<string, object> jsonDic = new Dictionary<string, object>();
            jsonDic.Add("zr45000", zr45000.Value.ToString());
            jsonDic.Add("zr45001", zr45001.Value.ToString());
            jsonDic.Add("zr45002", zr45002.Value.ToString());
            jsonDic.Add("zr45003", zr45003.Value.ToString());
            jsonDic.Add("zr45004", zr45004.Value.ToString());
            jsonDic.Add("zr45005", zr45005.Value.ToString());
            jsonDic.Add("zr45006", zr45006.Value.ToString());
            jsonDic.Add("zr45007", zr45007.Value.ToString());
            jsonDic.Add("zr45008", zr45008.Value.ToString());
            jsonDic.Add("zr45009", zr45009.Value.ToString());
            jsonDic.Add("zr45010", zr45010.Value.ToString());
            jsonDic.Add("zr45011", zr45011.Value.ToString());
            jsonDic.Add("zr45012", zr45012.Value.ToString());
            jsonDic.Add("zr45013", zr45013.Value.ToString());
            jsonDic.Add("zr45014", zr45014.Value.ToString());
            jsonDic.Add("zr45015", zr45015.Value.ToString());
            jsonDic.Add("zr45016", zr45016.Value.ToString());
            jsonDic.Add("zr45017", zr45017.Value.ToString());
            jsonDic.Add("zr45018", zr45018.Value.ToString());
            jsonDic.Add("zr45019", zr45019.Value.ToString());
            jsonDic.Add("zr45020", zr45020.Value.ToString());
            jsonDic.Add("zr45021", zr45021.Value.ToString());
            jsonDic.Add("zr45022", zr45022.Value.ToString());
            jsonDic.Add("zr45023", zr45023.Value.ToString());
            jsonDic.Add("zr45024", zr45024.Value.ToString());
            jsonDic.Add("zr45025", zr45025.Value.ToString());
            jsonDic.Add("zr45026", zr45026.Value.ToString());
            jsonDic.Add("zr45027", zr45027.Value.ToString());
            jsonDic.Add("zr45028", zr45028.Value.ToString());
            jsonDic.Add("zr45029", zr45029.Value.ToString());
            jsonDic.Add("zr45030", zr45030.Value.ToString());
            jsonDic.Add("zr45031", zr45031.Value.ToString());
            jsonDic.Add("zr45032", zr45032.Value.ToString());
            jsonDic.Add("zr45033", zr45033.Value.ToString());
            jsonDic.Add("zr45034", zr45034.Value.ToString());
            jsonDic.Add("zr45035", zr45035.Value.ToString());
            jsonDic.Add("zr45036", zr45036.Value.ToString());
            jsonDic.Add("zr45037", zr45037.Value.ToString());
            jsonDic.Add("zr45038", zr45038.Value.ToString());
            jsonDic.Add("zr45039", zr45039.Value.ToString());
            string json = JsonConvert.SerializeObject(jsonDic);
            return json;
        }
    }
}
