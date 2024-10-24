//=============================================================================
//                                 A220101
//=============================================================================
//
// 单工位标准Wish堆垛机 DB540/DB541/DB542协议。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/1/18
//      创建
//
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WISH.WCS;

using System;
using System.Collections.Generic;
using WISH.WCS.Device.SrmSocket.WishSrmV10Udt;
using System.Linq;
using ASRS.WCS.PLC;
//using WISH.WCS.PLC;
using ASRS.WCS.Common.Util;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace WISH.WCS.Device.SrmSocket
{

    #region 实现 IS1Srm 接口
    public partial class WishSrmV10 : IS2Srm
    {

        /// <summary>
        /// 设备产品唯一版本号
        /// </summary>
        public static readonly string Version = "WISH.SRM.V10";

        //[Display(Name = "叉1有货")]
        //public bool SwHasPallet { get { return this.srmStatus.Fork_One.Has_Goods.Value; } }

        //[Display(Name = "叉2有货")]
        //public bool SwHasPallet2 { get { return this.srmStatus.Fork_Two.Has_Goods.Value; } }

        //[Display(Name = "叉1中位")]
        //public bool SwForkCenter { get { return this.srmStatus.Fork_One.Fork_Center.Value; } }

        //[Display(Name = "叉2中位")]
        //public bool SwForkCenter2 { get { return this.srmStatus.Fork_Two.Fork_Center.Value; } }

        //public bool SwIsAxisZCenter { get { return this.srmStatus.zCenter.Value; } }

        //public int SwAxisXPos { get { return this.srmStatus.actPos.storeX.Value; } }

        //public int SwAxisXPosMm { get { return this.srmStatus.actPosMm.x.Value; } }

        //public int SwAxisYPos { get { return this.srmStatus.actPos.storeY.Value; } }

        //public int SwAxisYPosMm { get { return this.srmStatus.actPosMm.y.Value; } }

        //public int SwAxisZ1PosMm { get { return this.srmStatus.actPosMm.z1.Value; } }

        //public int SwAxisZ2PosMm { get { return this.srmStatus.actPosMm.z2.Value; } }

        //public string SwTaskType { get { return this.srmStatus.jobType.Value.ToString(); } }

        [Display(Name = "是否自动")]
        public bool SwIsAuto { get { return this.srmStatus.OperationMode.Value.ToSiemensString() == "AU"; } }

        [Display(Name = "叉1任务号")]
        public string SwTaskNo { get { return SubTask(); } }

        //[Display(Name = "叉2任务号")]
        //public string SwTaskNo2 { get { return this.srmStatus.Fork_Two.Task_No.Value.ToSiemensString(); } }
        //[Display(Name = "货叉1")]
        //public string ForkTaskFlag { get { return Fork1(); } }
        //[Display(Name = "货叉2")]
        //public string ForkTaskFlag2 { get { return Fork2(); } }

        [Display(Name = "是否报警")]
        public bool SwIsAlarm { get { return this.srmStatus.FaultState.Value.ToSiemensString() == "FL"; } }

        public bool Plc2WcsAck { get { return this.srm2Wcs.ack.Value; } }

        public bool Plc2WcsStb { get { return this.srm2Wcs.stb.Value; } }

        public bool Wcs2PlcAck { get { return this.wcs2Srm.ack.Value; } set { this.wcs2Srm.ack.Value = value; } }
        public bool Wcs2PlcStb { get { return this.wcs2Srm.stb.Value; } set { this.wcs2Srm.stb.Value = value; } }


        [Display(Name = "是否连接")]
        public bool SwIsConnect { get { return this.plc.IsConnect; } }

        public List<string> SwAlarmCode
        {
            get
            {
                List<string> alarmCode = new List<string>();
                if (this.srmStatus.FaultState.Value.ToSiemensString() == "FL")
                {
                    //string[] msg = this.srmStatus.FaultInfomation.Value;//2023-2-20修改
                    //for (int i = 0; i < msg.Length; i++)
                    //{
                    //    if (!msg[i].Equals(""))
                    //    {
                    //        alarmCode.Add(msg.ToString());
                    //    }
                    //}
                    //TODO: 补充报警编号，报警说明
                    for (int i = 0; i < srmStatus.FaultInfomation.Value.Length; i++)
                    {
                        if (!srmStatus.FaultInfomation.Value[i].ToSiemensString().Equals(""))
                        {
                            alarmCode.Add(srmStatus.FaultInfomation.Value[i].ToSiemensString());
                        }
                    }
                }
                return alarmCode;
            }
        }

        public string SwAlarmMessage
        {
            get
            {
                string alarmInfo = "";
                foreach (var item in SwAlarmCode)
                {
                    if (AlarmTable.Keys.Contains(item))
                    {
                        alarmInfo += "," + item + " : " + AlarmTable[item];
                    }
                    else
                    {
                        alarmInfo += "," + item + " : 未知报警";
                    }
                }
                if (alarmInfo.StartsWith(","))
                    alarmInfo = alarmInfo.Substring(1);
                return alarmInfo;
            }

        }

        private Dictionary<string, string> alarmTable = null;

        /// <summary>
        /// 报警信息对照表
        /// </summary>
        public Dictionary<string, string> AlarmTable
        {
            get
            {
                if (alarmTable == null)
                {
                    alarmTable = new Dictionary<string, string>();
                }
                return alarmTable;
            }
            set { this.alarmTable = value; }
        }


        public void CwAck()
        {
            throw new NotImplementedException();
        }

        public void CwClearTask()
        {
            throw new NotImplementedException();
        }

        private IPlcMsgService msgService;
        /// <summary>
        /// 初始化设备信息
        /// </summary>
        /// <param name="plc"></param>
        /// <param name="deviceConfig"></param>
        public void init(Plc plc, string deviceCode, string jsonConfigString, bool isEnable, IPlcMsgService msgService)
        {
            //DeviceConfig deviceConfig = DCService.GetInstance().GetDC().Set<DeviceConfig>().Include(x=>x.PlcConfig).Include(x=>x.StandardDevice).Where(x => x.ID == deviceConfigID).SingleOrDefault();
            //this.deviceConfig = deviceConfig;
            //deviceConfig.Config = deviceConfig.Config.Replace("&quot;", "\"");
            this.deviceCode = deviceCode;
            this.msgService = msgService;
            jsonConfigString = jsonConfigString.Replace("&quot;", "\"");
            //JObject jsonConfig = (JObject)JsonConvert.DeserializeObject(deviceConfig.Config);
            JObject jsonConfig = (JObject)JsonConvert.DeserializeObject(jsonConfigString);
            //deviceConfig.Config = deviceConfig.Config.Replace("&quot;", "\"");
            this.plc = plc;
            this.wcs2SrmDB = jsonConfig["wcs2SrmDB"].ToString();
            this.srm2WcsDB = jsonConfig["srm2WcsDB"].ToString();
            this.srmStatusDB = jsonConfig["srmStatusDB"].ToString();
            this.wcs2SrmOffset = jsonConfig["wcs2SrmOffset"].ToString().StringToInt32();
            this.srmStatusOffset = jsonConfig["srmStatusOffset"].ToString().StringToInt32();
            this.srm2WcsOffset = jsonConfig["srm2WcsOffset"].ToString().StringToInt32();
            wcs2Srm = new WishSrmV10Udt.UdtWcs2Srm(wcs2SrmDB, wcs2SrmOffset);
            srm2Wcs = new UdtSrm2Wcs(srm2WcsDB, srm2WcsOffset);
            srmStatus = new WishSrmV10Udt.UdtSrmStatus(srmStatusDB, srmStatusOffset);

            IsEnabled = isEnable;
            srmStatus.FaultState.DataChangedAction = new SignalDataChangeEvent(deviceCode, "fault", srmStatus, msgService).fireDataChange;
            srmStatus.OperationMode.DataChangedAction = new SignalDataChangeEvent(deviceCode, "mode", srmStatus, msgService).fireDataChange;
            srmStatus.FaultInfomation.DataChangedAction = AlarmChangeAction;//2023-2-20修改
            srmStatus.DataChangedAction = FireStatusDataChange;
        }

        /// <summary>
        /// 设备状态变化，通知展示客户端
        /// </summary>
        public void notifyNewClient()
        {
            FireStatusDataChange(this.srmStatus);
        }

        /// <summary>
        /// 设备状态变化，通知展示客户端
        /// </summary>
        /// <param name="obj"></param>
        public void FireStatusDataChange(object obj)
        {
            //"{\"srms\":[{\"code\":\"SRM02\",\"taskno\":\"T101\",\"tray\":true,\"alarm\":false,\"connect\":true,\"column\":" + x + "},";
            UdtSrmStatus status = (UdtSrmStatus)obj;
            //string str = "{\"srms\":[{\"code\":\"" + (deviceCode) 
            //    + "\",\"taskno1\":\"" + status.Fork_One.Task_No.Value.ToSiemensString()
            //    + "\",\"taskno2\":\"" + status.Fork_Two.Task_No.Value.ToSiemensString()
            //    + "\",\"tray1\":" + (status.Fork_One.Has_Goods.Value ? 1 : 0)
            //    + ",\"tray2\":" + (status.Fork_Two.Has_Goods.Value ? 1 : 0)
            //    + ",\"alarm\":" + (status.Alarming.Value ? 1 : 0)
            //    + ",\"connect\":" + (this.plc.IsConnect ? 1 : 0) 
            //    + ",\"column\":" + status.Current_X.Value + "}]}";]
            int xLength = status.PositionMmX.Value == 0 ? 0 : -(status.PositionMmX.Value / 2391 - 46);
            string str = "{\"srms\":[{\"code\":\"" + (deviceCode) + "\",\"taskno\":\"" + SubTask()
                + "\",\"tray\":" + (ForkCenter()=="0" ? 1 : 0)
                + ",\"alarm\":" + (status.FaultState.Value.ToSiemensString() == "FL" ? 1 : 0) + ",\"connect\":" + (this.plc.IsConnect ? 1 : 0) + ",\"column\":" + xLength + "}]}";
            //string str1 = "{\"srms\":[{\"code\":\"" + (deviceCode) 
            //    + "\",\"taskno\":\"" + status.Fork_One.Task_No.Value.ToSiemensString()
            //   + "\",\"tray\":" + (status.Fork_One.Fork_Center.Value ? 1 : 0)
            //   + ",\"alarm\":" + (status.Alarming.Value ? 1 : 0) 
            //   + ",\"connect\":" + (this.plc.IsConnect ? 1 : 0) 
            //   + ",\"column\":" + status.Current_X.Value + "}]}";
            if (msgService != null)
            {
                msgService.NewPageClientMsg(str);
            }
            //WcsViewSocketServer.GetInstanceAsync().SendWcsWebJsonMssage(str);
        }

        private void fireDataUpdate(object obj)
        {
            isDataChanged = false;
            foreach (object sig in GetAutoReadSignals())
            {
                if (sig is BaseSignal)
                {
                    BaseSignal signal = (BaseSignal)obj;
                    if (signal.IsDataChanged())
                    {
                        isDataChanged = true;
                        break;
                    }
                }
                else if (sig is BaseUdt)
                {
                    BaseUdt baseUdt = (BaseUdt)sig;
                    if (baseUdt.IsDataChanged())
                    {
                        isDataChanged = true;
                        break;
                    }
                }
            }
            if (isDataChanged)
            {
                DoDataChangedAction();
            }
        }

        /// <summary>
        /// 通知报警信息有变化
        /// </summary>
        /// <param name="obj"></param>
        public void AlarmChangeAction(object obj)
        {
            if (srmStatus.FaultState.Value.ToSiemensString() == "FL" && srmStatus.FaultInfomation.IsDataChanged())
            {
                List<string> list = new List<string>();
                for (int i = 0; i < srmStatus.FaultInfomation.Value.Length; i++)
                {
                    if (!srmStatus.FaultInfomation.Value[i].ToSiemensString().Equals(""))
                    {
                        list.Add(srmStatus.FaultInfomation.Value[i].ToSiemensString());
                    }
                }

                if (msgService != null)
                {
                    msgService.NewDeviceAlarm(deviceCode, string.Join(",", list), srmStatus);
                }

                //DeviceAlarmLog alarm = new DeviceAlarmLog();
                //alarm.Device_Code = deviceConfig.Device_Code;

                //alarm.Message = string.Join(",", list);
                //alarm.OriginTime = global::System.DateTime.Now;
                //DCService.GetInstance().NewEntity(alarm);
            }

        }


        /// <summary>
        /// 当前堆垛机运行步骤
        /// </summary>
        public string TaskStep { get { return ""; } }

        /// <summary>
        /// 任务取货完成
        /// </summary>
        public bool TaskPickFinish { get { return false; } }

        /// <summary>
        /// 任务放货完成
        /// </summary>
        public bool TaskDeliveryFinish { get { return false; } }


        public WishSrmV10()
        {

        }


        /// <summary>
        /// 真表示堆垛机空闲  自动 & 非报警 & 没有任务交互 & 货叉在中间 & 静止
        /// </summary>
        /// <returns></returns>
        public bool IsReady()
        {
            bool ready = false;
            if (ExecStep().Equals("00"))
            {
                // 自动    空闲     静止     无故障     原位&& srmStatus.ready.Value == true && srmStatus.motionless.Value == true 
                if (srmStatus.OperationMode.Value.ToSiemensString() == "AU" && srmStatus.FaultState.Value.ToSiemensString() == "OK" && ForkCenter().Equals("0"))
                {
                   ready = true;
                }
            }
            return ready;
        }
        #region 状态块数组转换
        //堆垛机模式转换
        public int RunMode()
        {
            int runMode = 0;
            if (srmStatus.OperationMode.Value.ToSiemensString() =="AU")
            {
                runMode = 1;
            }
            else if (srmStatus.OperationMode.Value.ToSiemensString() == "MU")
            {
                runMode = 2;
            }
            else
            {

            }
            return runMode;
        }
        //获取任务步骤
        public string ExecStep()
        {
            char[] lhd1 = srmStatus.LHD1State.Value==null ? null: srmStatus.LHD1State.Value;//货叉1
            string execStep = "";
            if (lhd1!=null)
            {
                string forkType = lhd1[0].ToString();//货叉类型A=单伸，B=双伸
                execStep = new string(lhd1, 27, 2).Replace('\0', ' ');//任务步骤
                if (forkType != "A")
                {
                    execStep = new string(lhd1, 32, 2).Replace('\0', ' ');//任务步骤
                }
            }
            return execStep;
        }

        //399-621项目特殊标记，记录站台有载状态
        public string StationHasGoods()
        {
            char[] lhd1 = srmStatus.LHD1State.Value == null ? null : srmStatus.LHD1State.Value;//货叉1
            string stationHasGoods = "";
            if (lhd1!=null)
            {
                stationHasGoods = new string(lhd1, 7, 2);//新增站台提示
            }
            return stationHasGoods;
        }

        //获取任务号
        private string SubTask()
        {
            char[] lhd1 = srmStatus.LHD1State.Value==null ? null: srmStatus.LHD1State.Value;//货叉1
            string cID = "";
            if (lhd1 != null)
            {
                cID = new string(lhd1, 39, 10);//子任务号
                string forkType = lhd1[0].ToString();//货叉类型A=单伸，B=双伸
                if (forkType != "A")
                {
                    cID = new string(lhd1, 44, 10);//子任务号
                }
            }
            return cID;
        }
        //获取货叉原位
        public string ForkCenter()
        {
            char[] lhd1 = srmStatus.LHD1State.Value==null ? null: srmStatus.LHD1State.Value;//货叉1
            string forkCenter = "";
            if (lhd1 != null)
            {
                forkCenter = new string(lhd1, 20, 1);//货叉原位,0=原位，1=左，2=右
            }
            return forkCenter;
        }
        //获取有货标志
        public string HasGoods()
        {
            char[] lhd1 = srmStatus.LHD1State.Value==null ? null: srmStatus.LHD1State.Value;//货叉1
            string hasgood = "";
            if (lhd1 != null)
            {
                hasgood = new string(lhd1, 21, 1);//有货状态 Y=有，N=无
            }
            return hasgood;
        }
        /// <summary>
        /// 货叉位置
        /// </summary>
        /// <returns></returns>
        public int CurrentZ()
        {
            char[] lhd1 = srmStatus.LHD1State.Value==null ? null: srmStatus.LHD1State.Value;//货叉1
            int currentZ=0;
            if (lhd1 != null)
            {
                if (lhd1[22] == '+')
                {
                    currentZ = Convert.ToInt32(new string(lhd1, 23, 4).Trim()) * 1;//货叉位置
                }
                else
                {
                    currentZ = Convert.ToInt32(new string(lhd1, 23, 4).Trim()) * -1;//货叉位置
                }
            }
            return currentZ;
        }
        /// <summary>
        /// 当前列
        /// </summary>
        /// <returns></returns>
        public int CurrentColumn()
        {
            int currentColumn = 0;
            string pointXY = srmStatus.PositionXY.Value.ToSiemensString();
            if (pointXY != null)
            {
                if (pointXY.StartsWith("SL"))
                {
                    //货位
                    currentColumn = Convert.ToInt32(pointXY.Substring(5, 3));
                }
            }
            return currentColumn;
        }
        /// <summary>
        /// 当前层
        /// </summary>
        /// <returns></returns>
        public int CurrentLayer()
        {
            int currentLayer = 0;
            string pointXY = srmStatus.PositionXY.Value.ToSiemensString();
            if (pointXY != null)
            {
                if (pointXY.StartsWith("SL"))
                {
                    //货位
                    currentLayer = Convert.ToInt32(pointXY.Substring(8, 2));
                }
            }
            return currentLayer;
        }
        #endregion 状态块数组转换


        #region Srm2wcs数组转换
        /// <summary>
        /// 任务号
        /// </summary>
        /// <returns></returns>
        public string Srm2wcsTaskno()
        {
            char[] dbOne = srm2Wcs.OperationBlock1.Value==null ? null: srm2Wcs.OperationBlock1.Value;
            string taskNo = "";
            if (dbOne!=null)
            {
                taskNo = new string(dbOne, 10, 10).Replace('\0', ' ');//子任务编号
            }
            return taskNo;
        }
        /// <summary>
        /// 堆垛机执行状态反馈
        /// </summary>
        /// <returns></returns>
        public int TaskStatus()
        {
            int status = 0;
            char[] dbOne = srm2Wcs.OperationBlock1.Value==null ? null: srm2Wcs.OperationBlock1.Value;//取第一个数据体
            string code = "";
            if (dbOne != null)
            {
                code = new string(dbOne, 70, 2);//返回值
            }
            //取货完成
            if (srm2Wcs.OperationID.Value.ToSiemensString()== "SRMB_LF")
            {
                if (code.Equals("OK"))
                {
                    status = 1;//取货完成
                }
            }
            //放货完成
            else if (srm2Wcs.OperationID.Value.ToSiemensString() == "SRMB_DF")
            {
                if (code.Equals("OK"))
                {
                    status = 2;//放货完成
                }
                else
                {
                    status = 9;//异常状态
                }
            }
            //取消反馈
            else if (srm2Wcs.OperationID.Value.ToSiemensString() == "SRMB_CR")
            {
                if (code.Equals("OK"))
                {
                    status = 3;//取消反馈
                }
            }
            //定位完成
            else if (srm2Wcs.OperationID.Value.ToSiemensString() == "SRMB_PF")
            {
                if (code.Equals("OK"))
                {
                    status = 4;//定位完成
                }
            }
            //拒绝任务
            else if (srm2Wcs.OperationID.Value.ToSiemensString() == "SRMB_FD")
            {

            }
            return status;
        }
        /// <summary>
        /// 堆垛机任务异常代码反馈
        /// </summary>
        /// <returns></returns>
        public int ErrorCode()
        {
            int returCode = 0;
            char[] dbOne = srm2Wcs.OperationBlock1.Value==null ? null: srm2Wcs.OperationBlock1.Value;//取第一个数据体
            string code = "";
            if (dbOne != null)
            {
                code = new string(dbOne, 70, 2);//返回值
            }
            if (srm2Wcs.OperationID.Value.ToSiemensString() == "SRMB_DF")
            {
                if (code.Equals("OK"))
                {
                    returCode = 0;//放货完成
                }
                else if (code.Equals("SN")) 
                {
                    returCode = 8;//取远货位近端货位阻塞
                }
                else if (code.Equals("FB") || code.Equals("DN"))
                {
                    returCode = 5;//满入、放远货近货位有货阻塞
                }
                else if (code.Equals("FF"))
                {
                    returCode = 6;//空取
                }
                else if (code.Equals("YH"))
                {
                    returCode = 7;//满入流程，货架低位无法放货
                }
            }
            return returCode;
        }
        #endregion Srm2wcs数组转换
        /// <summary>
        /// 货叉接收任务标志10：正常任务，11取消任务，12满入任务，13载货台盘点任务，00不接受任务
        /// </summary>
        /// <returns></returns>
        public string IsFork1()
        {
            string Fork1Flag = "00";
            //正常任务    自动                                                                                       无报警                                              货叉中位                                                                               货叉无货
            if (srmStatus.OperationMode.Value.ToSiemensString() == "AU" && srmStatus.FaultState.Value.ToSiemensString() == "OK" && ForkCenter().Equals("0") && HasGoods().Equals("N"))
            {
                Fork1Flag = "10";
            }
            //取消任务    自动                                                                                       报警                                              货叉中位                                                                               货叉无货
            //if (srmStatus.Run_Mode.Value == (Int16)SrmMode.Auto && srmStatus.Alarming.Value == true && srmStatus.Fork_One.Fork_Center.Value == true && srmStatus.Fork_One.Has_Goods.Value == false)
            //{
            //    Fork1Flag = "11";
            //}
            //满入任务    自动                                                                                       无报警                                              货叉中位                                                                               货叉有货
            //if (srmStatus.Run_Mode.Value == (Int16)SrmMode.Auto && srmStatus.Alarming.Value == false && srmStatus.Fork_One.Fork_Center.Value == true && srmStatus.Fork_One.Has_Goods.Value == true)
            //{
            //    Fork1Flag = "12";
            //}
            //满入任务 载货台盘点任务    自动                                                                                       无报警                                              货叉中位                                                                               货叉有货
            if (srmStatus.OperationMode.Value.ToSiemensString() == "AU" && srmStatus.FaultState.Value.ToSiemensString() == "OK" && ForkCenter().Equals("0") && HasGoods().Equals("Y"))
            {
                Fork1Flag = "13";
            }
            return Fork1Flag;
        }

        public string IsFork2()
        {
            string Fork2Flag = "00";
            //正常任务    自动                                                                                       无报警                                              货叉中位                                                                               货叉无货
            //if (srmStatus.Run_Mode.Value == (Int16)SrmMode.Auto && srmStatus.Alarming.Value == false && srmStatus.Fork_Two.Fork_Center.Value == true && srmStatus.Fork_Two.Has_Goods.Value == false)
            //{
            //    Fork2Flag = "10";
            //}
            ////取消任务    自动                                                                                       报警                                              货叉中位                                                                               货叉无货
            //if (srmStatus.Run_Mode.Value == (Int16)SrmMode.Auto && srmStatus.Alarming.Value == true && srmStatus.Fork_Two.Fork_Center.Value == true && srmStatus.Fork_Two.Has_Goods.Value == false)
            //{
            //    Fork2Flag = "11";
            //}
            ////满入任务    自动                                                                                       报警                                              货叉中位                                                                               货叉有货
            //if (srmStatus.Run_Mode.Value == (Int16)SrmMode.Auto && srmStatus.Alarming.Value == true && srmStatus.Fork_Two.Fork_Center.Value == true && srmStatus.Fork_Two.Has_Goods.Value == true)
            //{
            //    Fork2Flag = "12";
            //}
            //满入任务 载货台盘点任务    自动                                                                                       无报警                                              货叉中位                                                                               货叉有货
            //if (srmStatus.Run_Mode.Value == (Int16)SrmMode.Auto && srmStatus.Alarming.Value == false && srmStatus.Fork_Two.Fork_Center.Value == true && srmStatus.Fork_Two.Has_Goods.Value == true)
            //{
            //    Fork2Flag = "13";
            //}
            return Fork2Flag;
        }

        public string Fork1()
        {
            string Flagmsg = "";
            if (IsFork1().Equals("10"))
            {
                Flagmsg = "可以接收取放任务";
            }
            else if (IsFork1().Equals("13"))
            {
                Flagmsg = "可以接收单放任务";
            }
            else
            {
                Flagmsg = "不接受任务/执行步骤不是0";
            }
            return Flagmsg;
        }

        public string Fork2()
        {
            string Flagmsg = "";
            if (IsFork2().Equals("10"))
            {
                Flagmsg = "可以接收取放任务";
            }
            else if (IsFork2().Equals("13"))
            {
                Flagmsg = "可以接收单放任务";
            }
            else
            {
                Flagmsg = "不接受任务/执行步骤不是0";
            }
            return Flagmsg;
        }
    }

    #endregion

    #region 实现 IAutoRead 抽象类
    public partial class WishSrmV10 : IAutoRead
    {
        public string wcs2SrmDB { get; set; } = "DB540";
        public int wcs2SrmOffset { get; set; } = 0;

        public WishSrmV10Udt.UdtWcs2Srm wcs2Srm;
        //public WishSrmV10Udt.UdtWcs2Srm wcs2Srm = new WishSrmV10Udt.UdtWcs2Srm("DB550", 0);

        public Plc Plc { get { return plc; } }
        public string srm2WcsDB { get; set; } = "DB541";
        public int srm2WcsOffset { get; set; } = 0;
        public WishSrmV10Udt.UdtSrm2Wcs srm2Wcs = new WishSrmV10Udt.UdtSrm2Wcs("DB541", 0);

        public string srmStatusDB { get; set; } = "DB39";
        public int srmStatusOffset { get; set; } = 0;
        public UdtSrmStatus srmStatus = new UdtSrmStatus("DB39", 0);

        private Plc plc;

        private string deviceCode;

        /// <summary>
        /// 真表示设备有效。
        /// </summary>
        public bool IsEnabled { get; set; } = true;


        private List<object> signals;
        public List<object> GetAutoReadSignals()
        {
            {
                if (signals == null)
                {
                    signals = new List<object>();
                    signals.Add(wcs2Srm);
                    signals.Add(srm2Wcs);
                    signals.Add(srmStatus);
                    PlcDataUtil.UpdateUdtAddress(wcs2Srm.GetSignals(), wcs2SrmDB, wcs2SrmOffset);
                    PlcDataUtil.UpdateUdtAddress(srm2Wcs.GetSignals(), srm2WcsDB, srm2WcsOffset);
                    PlcDataUtil.UpdateUdtAddress(srmStatus.GetSignals(), srmStatusDB, srmStatusOffset);
                    srmStatus.DataUpdatedAction = fireDataUpdate;
                }
                return signals;
            }
        }

    }
    #endregion

    #region 实现 BaseMonitor 抽象类

    public partial class WishSrmV10 : BaseMonitor
    {


        private bool isDataChanged;

        public override bool IsDataChanged()
        {
            return isDataChanged;
        }

    }
    #endregion


}
