//=============================================================================
//                                 A220101
//=============================================================================
//
// WCS 主流程。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/1/25
//      创建
//
//-----------------------------------------------------------------------------


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

using Wish.HWConfig.Models;
using Wish.Service;
using ASRS.WCS.PLC;
using ASRS.WCS.Common.Util;
using WISH.WCS.Device.SrmSocket.WishSrmV10Udt;
using ASRS.WCS.Common.Enum;
using WISH.WCS.Device.SrmSocket;
using MySqlConnector;
using NPOI.SS.Formula.Functions;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using HslCommunication.Enthernet;
using Quartz.Impl.Triggers;
using Wish.Areas.TaskConfig.Model;

namespace WISH.Custome
{
    public class P260_OB1
    {
        private bool isRunning = false;
        public void Start()
        {
            isRunning = true;
            Task.Factory.StartNew(async () =>
            {
                while (isRunning)
                {
                    Run();
                    //Thread.Sleep(300);
                    await Task.Delay(TimeSpan.FromMilliseconds(300));
                }
            }
            );
        }

        public void Stop()
        {
            isRunning = false;
        }

        public P260_OB1()
        {
        }

        /// <summary>
        /// 业务逻辑实现
        /// </summary>
        private async void Run()
        {
            try
            {
                DoSrmWork();


            }
            catch (Exception ex)
            {
                ContextService.Log.Error(ex.Message);
            }
            //try
            //{
            //    //await WcsToWmsDeviceInfo();//设备信息
            //    //WcsToWmsDeviceInfo();//设备信息
            //}
            //catch (Exception ex)
            //{

            //    ContextService.Log.Error(ex.Message);
            //}
        }
        //private List<DeviceConfig> srmDevices = null;

        /// <summary>
        /// 处理堆垛机业务流程
        /// </summary>
        private void DoSrmWork()
        {
            #region 1 堆垛机正常任务
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///         业务逻辑
            ///
            // if (Srm2Wcs.STB = true)
            //     处理任务 PRO_Srm_Set_Task -> 如果有反馈（删除任务) wcs2plc下任务交互stb ack-> 回写Ack
            // else
            //     判断是否空闲
            //        if (空闲)
            //     找任务 PRO_Srm_Get_Task   -> 有任务 wcs2plc下任务 交互stb ack -> PRO_Srm_Set_Task->回Ack           
            //   
            //    异常任务
            //   1、空取
            //   调用存储过程[PRO_Srm_Set_Task] Datatable(x)->Wcs2Srm(x)
            //   完成stb ack交互。
            //    
            //   2、满入
            //   调用存储过程[PRO_Srm_Set_Task]  -> Wcs2Srm(x)
            //   完成stb ack交互 
            //   
            //   找货叉任务对应的修改后的任务（Exec_Status = 10），查堆垛机任务表 Datatable -> Wcs2Srm(x)
            //   完成stb ack交互
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
            List<DeviceConfig> srmDevices = null;
            if (srmDevices == null)
                srmDevices = DCService.GetInstance().GetDC().Set<DeviceConfig>()
                    .Include(x => x.PlcConfig)
                    .Include(x => x.StandardDevice).Where(x => x.StandardDevice.DeviceType == EDeviceType.S1Srm && "WISH.SRM.V10".Equals(x.StandardDevice.Device_Code) && x.IsEnabled == true)
                    .ToList();
            foreach (var item in srmDevices)
            {
                WishSrmV10 srm = (WishSrmV10)PlcService.GetInstance().GetRegisterAutoReadObject(item.PlcConfig.Plc_Code, item.Device_Code);

                if (srm == null) continue;
                /// 修改设备在线状态
                if (srm.Plc.IsConnect != item.IsOnline || srm.RunMode() != item.Mode)
                {
                    item.IsOnline = srm.Plc.IsConnect;
                    item.Mode = srm.RunMode();
                    DCService.GetInstance().UpdateEntity(item);
                }
                string srm2WcsTaskNo = "";
                int taskStatus = 0;
                int errorCode = 0;
                #region Plc2WcsStep = 0 srm2wcs 无交互信息
                if (item.Plc2WcsStep == 0)
                {
                    srm2WcsTaskNo = srm.Srm2wcsTaskno();
                    if (srm.srm2Wcs.stb.Value == true)
                    {
                        // 记录通讯报文
                        MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, srm2WcsTaskNo, srm.srm2Wcs.ToJson());
                        // todo:协议特有设备反馈验证，可有可无
                        //if (srm.srm2Wcs.Receiver.Value.ToSiemensString() == item.Device_Code)
                        //{
                        item.Plc2WcsStep = 1;
                        DCService.GetInstance().GetDC().UpdateEntity(item);
                        DCService.GetInstance().GetDC().SaveChanges();
                        //}
                    }
                    else
                    {
                        //HslCommunication.OperateResult ISOK = srm.Plc.PlcDevice.ReadInt16("DB552.0", 3);
                        //HslCommunication.OperateResult ISOK1 = srm.Plc.PlcDevice.ReadBool("DB552.6", 2);
                        //报警，且报警信息为满入和空出，对其作出删除处理
                        errorCode = srm.ErrorCode();
                        taskStatus = srm.TaskStatus();
                        if (srm.srmStatus.FaultState.Value.ToSiemensString() == "FL" && taskStatus == 9)
                        {
                            // 记录通讯报文
                            MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, srm2WcsTaskNo, srm.srm2Wcs.ToJson());
                            if (errorCode == 5 || errorCode == 6 || errorCode == 7 || errorCode == 8)
                            {
                                // 记录通讯报文
                                MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, srm2WcsTaskNo, srm.srm2Wcs.ToJson());
                                // todo:协议特有设备反馈验证，可有可无
                                //if (srm.srm2Wcs.Receiver.Value.ToSiemensString() == item.Device_Code)
                                //{
                                //    item.Plc2WcsStep = 1;
                                //    DCService.GetInstance().GetDC().UpdateEntity(item);
                                //    DCService.GetInstance().GetDC().SaveChanges();
                                //}
                            }
                        }
                        if (srm.IsReady())
                        {
                            //货叉1反馈的标志
                            string forkOne = srm.IsFork1();
                            //货叉2反馈的标志
                            string forkTwo = srm.IsFork2();
                            // @IV_SRM_NO 设备编号
                            //@IV_FORK1_FLAG货叉1的标志
                            //@IV_FORK2_FLAG货叉2的标志
                            // PRO_Srm_Get_Task
                            MySqlParameter[] param = new MySqlParameter[5];
                            param[0] = new MySqlParameter("@IV_SRM_NO", item.Device_Code);
                            param[1] = new MySqlParameter("@IV_FORK1_FLAG", forkOne);
                            param[2] = new MySqlParameter("@IV_FORK2_FLAG", forkTwo);
                            param[3] = new MySqlParameter("@OV_RESULT_CODE", MySqlDbType.Int32);
                            param[4] = new MySqlParameter("@OV_MSG", MySqlDbType.VarChar, 100);
                            param[0].Direction = ParameterDirection.Input;
                            param[1].Direction = ParameterDirection.Input;
                            param[2].Direction = ParameterDirection.Input;
                            param[3].Direction = ParameterDirection.Output;
                            param[4].Direction = ParameterDirection.Output;
                            DataTable dt = null;
                            IDataContext dc = DCService.GetInstance().GetDC();
                            try
                            {
                                dc.Database.BeginTransaction();
                                dt = dc.RunSP("PRO_Srm_Get_Task", param);
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    bool res = DoWcs2SrmTask(dt.Rows[0], item, srm);
                                    if (res == true)
                                    {
                                        item.Plc2WcsStep = 2;
                                        item.Wcs2PlcStep = 1;
                                        dc.UpdateEntity(item);
                                        dc.SaveChanges();
                                    }
                                    else
                                    {
                                        throw new Exception("任务下发失败" + dt.Rows[0]["Task_Id"].ToString());
                                    }
                                }
                                dc.Database.CommitTransaction();
                            }
                            catch (Exception ex)
                            {
                                dc.Database.RollbackTransaction();
                                MsgService.GetInstance().NewDeviceMsg(item.Device_Code, ETaskExecMsgType.WARN, ex.Message);
                            }
                        }
                    }
                }
                #endregion

                #region Plc2WcsStep = 1 收到srm2wcs信息 更新状态
                if (item.Plc2WcsStep == 1)
                {
                    errorCode = srm.ErrorCode();
                    taskStatus = srm.TaskStatus();
                    srm2WcsTaskNo = srm.Srm2wcsTaskno();
                    MySqlParameter[] param = new MySqlParameter[11];
                    param[0] = new MySqlParameter("@IV_DEVICE_NO", item.Device_Code);
                    param[1] = new MySqlParameter("@IV_TASK_NO", srm2WcsTaskNo);
                    param[2] = new MySqlParameter("@IV_LHDID", "1");
                    param[3] = new MySqlParameter("@IV_TASK_STATE", taskStatus);
                    param[4] = new MySqlParameter("@IV_TASK_ERROR", errorCode);
                    param[5] = new MySqlParameter("@IV_TASK_NO2", "");
                    param[6] = new MySqlParameter("@IV_LHDID2", "2");
                    param[7] = new MySqlParameter("@IV_TASK_STATE2", 0);
                    param[8] = new MySqlParameter("@IV_TASK_ERROR2", 0);
                    param[9] = new MySqlParameter("@OV_RESULT_CODE", MySqlDbType.Int32);
                    param[10] = new MySqlParameter("@OV_MSG", MySqlDbType.VarChar, 100);
                    param[0].Direction = ParameterDirection.Input;
                    param[1].Direction = ParameterDirection.Input;
                    param[2].Direction = ParameterDirection.Input;
                    param[3].Direction = ParameterDirection.Input;
                    param[4].Direction = ParameterDirection.Input;
                    param[5].Direction = ParameterDirection.Input;
                    param[6].Direction = ParameterDirection.Input;
                    param[7].Direction = ParameterDirection.Input;
                    param[8].Direction = ParameterDirection.Input;
                    param[9].Direction = ParameterDirection.Output;
                    param[10].Direction = ParameterDirection.Output;
                    DataTable dt = null;
                    IDataContext dc = DCService.GetInstance().GetDC();
                    try
                    {
                        // 处理堆垛机任务反馈异常
                        DoSrmTaskError(item, errorCode, taskStatus);//

                        // 反馈的DataTable有那些数据, 如果DT > 0 ，回数据，负责为0或者Null回ACK
                        // dt.Rows[0]["has_send"].ToString(); "0"/"1", 1表示已经调用过 PRO_COV_TaskStart

                        dc.Database.BeginTransaction();
                        dt = dc.RunSP("PRO_Task_Srm", param);

                        // 如果是空出/满入 发送删除任务
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            // 删除任务
                            //if (dt.Rows[0]["COMMAND_TYPE"].ToString() == "3") { }
                            bool res = DoWcs2SrmTask(dt.Rows[0], item, srm);
                            if (res == true)
                            {
                                item.Plc2WcsStep = 20;
                                item.Wcs2PlcStep = 1;
                                //item.Plc2WcsStep = 7;//P475
                                dc.UpdateEntity(item);
                                dc.SaveChanges();
                            }
                            else
                            {
                                throw new Exception("任务下发失败" + dt.Rows[0]["Task_Id"].ToString());
                            }
                        }
                        else
                        {
                            // 直接 完成 srm2wcs stb ack交互 
                            item.Plc2WcsStep = 4;
                            dc.UpdateEntity(item);
                            dc.SaveChanges();
                        }

                        dc.Database.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        dc.Database.RollbackTransaction();
                        MsgService.GetInstance().NewDeviceMsg(item.Device_Code, ETaskExecMsgType.WARN, ex.Message);
                        //ContextService.Log.Error(ex);
                    }
                }
                #endregion

                #region  Plc2WcsStep = 2 / 20 完成 wcs下发任务的  stb ack交互  /   Plc2WcsStep = 20 下一步 Plc2WcsStep = 4
                if (item.Plc2WcsStep == 2 || item.Plc2WcsStep == 20)
                {
                    // 写 wcs2plc stb = 1
                    if (item.Wcs2PlcStep == 1)
                    {
                        // 写交互信号
                        if (srm.wcs2Srm.stb.Value == true)
                        {
                            // 记录通讯报文
                            MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Wcs2Plc, srm.Srm2wcsTaskno(), srm.wcs2Srm.ToJson());

                            item.Wcs2PlcStep = 2;
                            DCService.GetInstance().UpdateEntity(item);
                        }
                        else
                        {
                            Signal<bool> stb = (Signal<bool>)srm.wcs2Srm.stb.Clone();
                            stb.Value = true;
                            PlcService.GetInstance().WriteSignal(item.PlcConfig.Plc_Code, stb, null);
                        }
                    }

                    // 等待 wcs2plc ack = 1
                    if (item.Wcs2PlcStep == 2)
                    {
                        if (srm.wcs2Srm.stb.Value == true && srm.wcs2Srm.ack.Value == true)
                        {

                            // 记录通讯报文
                            MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Wcs2Plc, srm.Srm2wcsTaskno(), srm.wcs2Srm.ToJson());

                            item.Wcs2PlcStep = 3;
                            DCService.GetInstance().UpdateEntity(item);
                        }
                    }

                    // 写 wcs2plc stb = 0
                    if (item.Wcs2PlcStep == 3)
                    {
                        if (srm.wcs2Srm.stb.Value == false && (srm.wcs2Srm.ack.Value == true || srm.wcs2Srm.ack.Value == false))//6-20更改
                        {
                            // 记录通讯报文
                            MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Wcs2Plc, srm.Srm2wcsTaskno(), srm.wcs2Srm.ToJson());

                            item.Wcs2PlcStep = 4;
                            DCService.GetInstance().UpdateEntity(item);
                        }
                        else
                        {
                            Signal<bool> stb = (Signal<bool>)srm.wcs2Srm.stb.Clone();
                            stb.Value = false;
                            PlcService.GetInstance().WriteSignal(item.PlcConfig.Plc_Code, stb, null);
                        }
                    }

                    // 等待 wcs2plc ack = 0
                    if (item.Wcs2PlcStep == 4)
                    {
                        if (srm.wcs2Srm.stb.Value == false && srm.wcs2Srm.ack.Value == false)
                        {
                            // 记录通讯报文
                            MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Wcs2Plc, srm.Srm2wcsTaskno(), srm.wcs2Srm.ToJson());

                            item.Wcs2PlcStep = 0;

                            // 删除任务发送，不需要调用Pro_Srm_Set_Task
                            if (item.Plc2WcsStep == 20)
                            {
                                item.Plc2WcsStep = 4;
                            }
                            else
                            {
                                item.Plc2WcsStep = 3;
                            }
                            DCService.GetInstance().UpdateEntity(item);
                        }
                    }
                }
                #endregion

                #region Plc2WcsStep = 3 调用 PRO_Srm_Set_Task 更新任务状态
                if (item.Plc2WcsStep == 3)
                {
                    srm2WcsTaskNo = srm.Srm2wcsTaskno();
                    MySqlParameter[] param = new MySqlParameter[9];
                    // VARCHAR(10),任务编号
                    param[0] = new MySqlParameter("@IV_TASK_ID", srm2WcsTaskNo);
                    // VARCHAR(6),设备号
                    param[1] = new MySqlParameter("@IV_SRM_NO", item.Device_Code);
                    // VARCHAR(2) 任务状态
                    param[2] = new MySqlParameter("@IV_STATUS", srm2WcsTaskNo == "0" ? "" : "10");
                    // VARCHAR(2) 货叉
                    param[3] = new MySqlParameter("@IV_LHDID", "1");
                    // VARCHAR(10),任务编号
                    param[4] = new MySqlParameter("@IV_TASK_ID2", "");
                    // VARCHAR(2) 任务状态
                    param[5] = new MySqlParameter("@IV_STATUS2", "");
                    // VARCHAR(2) 货叉
                    param[6] = new MySqlParameter("@IV_LHDID2", "2");
                    param[7] = new MySqlParameter("@OV_RESULT_CODE", MySqlDbType.Int32);
                    param[8] = new MySqlParameter("@OV_MSG", MySqlDbType.VarChar, 100);
                    param[0].Direction = ParameterDirection.Input;
                    param[1].Direction = ParameterDirection.Input;
                    param[2].Direction = ParameterDirection.Input;
                    param[3].Direction = ParameterDirection.Input;
                    param[4].Direction = ParameterDirection.Input;
                    param[5].Direction = ParameterDirection.Input;
                    param[6].Direction = ParameterDirection.Input;
                    param[7].Direction = ParameterDirection.Output;
                    param[8].Direction = ParameterDirection.Output;

                    DataTable dt = null;
                    IDataContext dc = DCService.GetInstance().GetDC();
                    try
                    {
                        // 反馈的DataTable有那些数据, 如果DT > 0 ，回数据，负责为0或者Null回ACK
                        // dt.Rows[0]["has_send"].ToString(); "0"/"1", 1表示已经调用过 PRO_COV_TaskStart                        
                        dc.Database.BeginTransaction();
                        dt = dc.RunSP("PRO_Srm_Set_Task", param);
                        item.Plc2WcsStep = 7;
                        dc.UpdateEntity(item);
                        dc.SaveChanges();
                        dc.Database.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        dc.Database.RollbackTransaction();
                        MsgService.GetInstance().NewDeviceMsg(item.Device_Code, ETaskExecMsgType.WARN, ex.Message);
                        //ContextService.Log.Error(ex);
                    }
                }
                #endregion

                #region Plc2WcsStep = 4 回 srm2wcs ack=1
                if (item.Plc2WcsStep == 4)
                {
                    if (srm.srm2Wcs.ack.Value == true)
                    {

                        // 记录通讯报文
                        MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, srm.Srm2wcsTaskno(), srm.srm2Wcs.ToJson());

                        item.Plc2WcsStep = 5;
                        DCService.GetInstance().UpdateEntity(item);
                    }
                    else
                    {
                        Signal<bool> ack = (Signal<bool>)srm.srm2Wcs.ack.Clone();
                        ack.Value = true;
                        PlcService.GetInstance().WriteSignal(item.PlcConfig.Plc_Code, ack, null);
                    }
                }
                #endregion

                #region Plc2WcsStep = 5 等待 srm2wcs 的 stb = 0
                if (item.Plc2WcsStep == 5)
                {
                    if (srm.srm2Wcs.stb.Value == false)//&& srm.srm2Wcs.ack.Value == true
                    {

                        // 记录通讯报文
                        MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, srm.Srm2wcsTaskno(), srm.srm2Wcs.ToJson());

                        item.Plc2WcsStep = 6;
                        DCService.GetInstance().UpdateEntity(item);
                    }
                }
                #endregion

                #region Plc2WcsStep = 6 复位 srm2wcs ack=0
                if (item.Plc2WcsStep == 6)
                {
                    if (srm.srm2Wcs.stb.Value == false && srm.srm2Wcs.ack.Value == false)
                    {

                        // 记录通讯报文
                        MsgService.GetInstance().NewTaskCommunication(item.Device_Code, EPlcCommDirect.Plc2Wcs, srm.Srm2wcsTaskno(), srm.srm2Wcs.ToJson());

                        item.Plc2WcsStep = 7;
                        DCService.GetInstance().UpdateEntity(item);
                    }
                    else
                    {
                        Signal<bool> ack = (Signal<bool>)srm.srm2Wcs.ack.Clone();
                        ack.Value = false;
                        PlcService.GetInstance().WriteSignal(item.PlcConfig.Plc_Code, ack, null);
                    }
                }
                #endregion

                #region Plc2WcsStep = 7 完成 srm2wcs的交互
                if (item.Plc2WcsStep == 7)
                {
                    item.Plc2WcsStep = 0;
                    DCService.GetInstance().UpdateEntity(item);
                }
                #endregion


                #endregion
            }
        }

        #region 记录设备信息
        /// <summary>
        /// 记录设备信息
        /// </summary>
        //private List<SrmStatus> devInfo = null;
        //private List<DeviceConfig> deviceInfo = null;x.StandardDevice.DeviceType == EDeviceType.S1Srm && "WISH.SRM.V10".Equals(x.StandardDevice.Device_Code) &&
        /// 
        //private async Task WcsToWmsDeviceInfo()
        ////private void WcsToWmsDeviceInfo()
        //{
        //    List<SrmStatus> devInfo = null;
        //    //查询指令反馈
        //    if (devInfo == null)
        //    {
        //        devInfo = await DCService.GetInstance().GetDC().Set<SrmStatus>()
        //           //devInfo = DCService.GetInstance().GetDC().Set<SrmStatus>()
        //           .Include(x => x.PlcConfig)
        //           .Include(x => x.StandardDevice)
        //           .Include(x => x.DeviceConfig)
        //           .Where(x => x.IsValid == true && "WISH.SRM.V10"
        //           .Equals(x.StandardDevice.Device_Code))// && x.DeviceConfig.Exec_Flag == true&& x.DeviceConfig.IsEnabled == true  && x.PlcConfig.IsEnabled==true)//
        //                                                                                                //.ToList();
        //           .ToListAsync();
        //    }
        //    var dictSrm = await DCService.GetInstance().GetDC().Set<Dictonary>().Where(y => y.Type == "Device_AlarmInfo~WISH.SRM.V10").ToListAsync();
        //    var srmTaskStatus = await DCService.GetInstance().GetDC().Set<SrmTaskstatus>().ToListAsync();
        //    var srmSpare = await DCService.GetInstance().GetDC().Set<SrmSpare>().ToListAsync();

        //    IDataContext dc = DCService.GetInstance().GetDC();
        //    #region 记录设备状态
        //    foreach (var item in devInfo)
        //    {
        //        #region 堆垛机
        //        if (item.StandardDevice.DeviceType == EDeviceType.S1Srm)
        //        {
        //            WishSrmV10 srm = (WishSrmV10)PlcService.GetInstance().GetRegisterAutoReadObject(item.PlcConfig.Plc_Code, item.Srm_No);
        //            //WishSrmV10 srm = (WishSrmV10)PlcService.GetInstance().GetRegisterAutoReadObject("", item.Srm_No);

        //            if (srm == null || !srm.Plc.IsConnect) continue;

        //            #region 报警信息，2023-2-20添加
        //            string alarmCode = "";
        //            string alarmInfo = "";
        //            string forkCenter = "";
        //            string hasGoods = "";
        //            string stationHasGoods = "";
        //            int execStep = 0;
        //            int currentZ = 0;
        //            int currentRank = 0;
        //            int currentColumn = 0;
        //            int currentLayer = 0;
        //            bool alarmSame = true;//报警是否相同,true相同，false不同
        //            if (srm.srmStatus.FaultState.Value.ToSiemensString() == "FL")
        //            {
        //                List<string> msg = new List<string>();
        //                for (int i = 0; i < srm.srmStatus.FaultInfomation.Value.Length; i++)
        //                {
        //                    if (!srm.srmStatus.FaultInfomation.Value[i].ToSiemensString().Equals(""))
        //                    {
        //                        msg.Add(srm.srmStatus.FaultInfomation.Value[i].ToSiemensString());
        //                    }
        //                }
        //                alarmCode = string.Join(",", msg);
        //                if (!alarmCode.Equals(item.Alarm_Code))
        //                {
        //                    string[] alarmCodes = alarmCode.Split(',');
        //                    //Console.WriteLine("报警代码S" + alarmCodes);
        //                    string[] alarmMsg = new string[alarmCodes.Length];
        //                    for (int i = 0; i < alarmCodes.Length; i++)
        //                    {
        //                        foreach (Dictonary d in dictSrm)
        //                        {
        //                            //Console.WriteLine("报警代码" + alarmCodes[i]);
        //                            if (d.Value.Equals(alarmCodes[i]))
        //                            {
        //                                alarmMsg[i] = d.Label;
        //                            }
        //                            //Console.WriteLine("报警信息" + alarmMsg[i]);
        //                        }
        //                    }
        //                    alarmInfo = string.Join(",", alarmMsg);
        //                    alarmSame = false;
        //                }
        //            }
        //            #endregion

        //            #region 货叉1
        //            if (item.Fork_No.Equals("1"))
        //            {
        //                execStep = Convert.ToInt32(srm.ExecStep());
        //                stationHasGoods = srm.StationHasGoods();
        //                #region 记录设备任务执行状态
        //                foreach (SrmTaskstatus srmTaskExec in srmTaskStatus)
        //                {
        //                    if (srmTaskExec.Srm_No.Equals(item.Srm_No) && srmTaskExec.Fork_No.Equals(item.Fork_No))
        //                    {
        //                        if ((execStep == 0 || execStep == 2 || execStep == 4) && srmTaskExec.Exe_Step != execStep && srmTaskExec.Exe_Step != 5 && srmTaskExec.Exe_Step != 6)
        //                        {
        //                            srmTaskExec.Exe_Step = execStep;
        //                            srmTaskExec.UpdateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //                            srmTaskExec.UpdateBy = "PLC";
        //                            dc.UpdateEntity(srmTaskExec);
        //                            dc.SaveChanges();
        //                        }
                                
        //                    }
        //                }
        //                foreach (SrmSpare srmSpare1 in srmSpare)
        //                {
        //                    if (srmSpare1.Srm_No.Equals(item.Srm_No) && srmSpare1.Fork_No.Equals(item.Fork_No))
        //                    {
        //                        if (!srmSpare1.Spare1.Equals(stationHasGoods))
        //                        {
        //                            srmSpare1.Spare1 = stationHasGoods;
        //                            srmSpare1.UpdateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //                            srmSpare1.UpdateBy = "PLC";
        //                            dc.UpdateEntity(srmSpare1);
        //                            dc.SaveChanges();
        //                        }
                                
        //                    }
        //                }
        //                #endregion

        //                #region 待命（堆垛机自动状态时，上位机下发堆垛机指令）
        //                //货叉1
        //                if (srm.srmStatus.OperationMode.Value.ToSiemensString() == "AU" && srm.srmStatus.FaultState.Value.ToSiemensString() == "OK" && srm.ExecStep() == "00")
        //                {
        //                    currentZ = srm.CurrentZ();
        //                    currentRank = Convert.ToInt32(srm.ForkCenter());
        //                    currentColumn = srm.CurrentColumn();
        //                    currentLayer = srm.CurrentLayer();
        //                    forkCenter = srm.ForkCenter();
        //                    hasGoods = srm.HasGoods();
        //                    try
        //                    {
        //                        item.ForkCenter = forkCenter;
        //                        item.HasGood = hasGoods;
        //                        item.Device_Status = 2;//待命
        //                        item.Run_Mode = 2;
        //                        item.Current_Rank = currentRank;
        //                        item.Current_Column = currentColumn;
        //                        item.Current_Layer = currentLayer;
        //                        item.Current_X = srm.srmStatus.PositionMmX.Value;
        //                        item.Current_Y = srm.srmStatus.PositionMmY.Value;
        //                        item.Current_Z = currentZ;
        //                        item.Alarm_Code = alarmCode;
        //                        item.Alarm_Info = alarmInfo;
        //                        item.Update_Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //                        dc.UpdateEntity(item);
        //                        dc.SaveChanges();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        ContextService.Log.Error("记录货叉1空闲" + ex.Message);
        //                        //throw;
        //                    }

        //                }
        //                #endregion
        //                #region 运行中
        //                //货叉1
        //                if (srm.srmStatus.OperationMode.Value.ToSiemensString() == "AU" && srm.srmStatus.FaultState.Value.ToSiemensString() == "OK" && srm.ExecStep() != "00")//srm.srmStatus.Fork_One.Has_Goods.Value == true
        //                {
        //                    currentZ = srm.CurrentZ();
        //                    currentRank = Convert.ToInt32(srm.ForkCenter());
        //                    currentColumn = srm.CurrentColumn();
        //                    currentLayer = srm.CurrentLayer();
        //                    forkCenter = srm.ForkCenter();
        //                    hasGoods = srm.HasGoods();
        //                    try
        //                    {
        //                        item.ForkCenter = forkCenter;
        //                        item.HasGood = hasGoods;
        //                        item.Device_Status = 1;//运行
        //                        item.Run_Mode = 2;//自动
        //                        item.Current_Rank = currentRank;
        //                        item.Current_Column = currentColumn;
        //                        item.Current_Layer = currentLayer;
        //                        item.Current_X = srm.srmStatus.PositionMmX.Value;
        //                        item.Current_Y = srm.srmStatus.PositionMmY.Value;
        //                        item.Current_Z = currentZ;
        //                        item.Alarm_Code = alarmCode;
        //                        item.Alarm_Info = alarmInfo;
        //                        item.Update_Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //                        dc.UpdateEntity(item);
        //                        dc.SaveChanges();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        ContextService.Log.Error("记录货叉1运行中" + ex.Message);
        //                        //throw;
        //                    }

        //                }
        //                #endregion
        //                #region 手动
        //                //货叉1
        //                if (srm.srmStatus.OperationMode.Value.ToSiemensString() == "MU" && srm.srmStatus.FaultState.Value.ToSiemensString() == "OK")
        //                {
        //                    currentZ = srm.CurrentZ();
        //                    currentRank = Convert.ToInt32(srm.ForkCenter());
        //                    currentColumn = srm.CurrentColumn();
        //                    currentLayer = srm.CurrentLayer();
        //                    forkCenter = srm.ForkCenter();
        //                    hasGoods = srm.HasGoods();
        //                    try
        //                    {
        //                        item.ForkCenter = forkCenter;
        //                        item.HasGood = hasGoods;
        //                        item.Device_Status = 0;//空闲
        //                        item.Run_Mode = 1;//手动
        //                        item.Current_Rank = currentRank;
        //                        item.Current_Column = currentColumn;
        //                        item.Current_Layer = currentLayer;
        //                        item.Current_X = srm.srmStatus.PositionMmX.Value;
        //                        item.Current_Y = srm.srmStatus.PositionMmY.Value;
        //                        item.Current_Z = currentZ;
        //                        item.Alarm_Code = alarmCode;
        //                        item.Alarm_Info = alarmInfo;
        //                        item.Update_Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //                        dc.UpdateEntity(item);
        //                        dc.SaveChanges();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        ContextService.Log.Error("记录货叉1手动" + ex.Message);
        //                        //throw;
        //                    }

        //                }
        //                #endregion
        //                #region 脱机
        //                //货叉1
        //                if (srm.srmStatus.OperationMode.Value.ToSiemensString() == "OF")
        //                {
        //                    currentZ = srm.CurrentZ();
        //                    currentRank = Convert.ToInt32(srm.ForkCenter());
        //                    currentColumn = srm.CurrentColumn();
        //                    currentLayer = srm.CurrentLayer();
        //                    forkCenter = srm.ForkCenter();
        //                    hasGoods = srm.HasGoods();
        //                    try
        //                    {
        //                        item.ForkCenter = forkCenter;
        //                        item.HasGood = hasGoods;
        //                        item.Device_Status = 3;//故障
        //                        item.Run_Mode = 3;//脱机
        //                        item.Current_Rank = currentRank;
        //                        item.Current_Column = currentColumn;
        //                        item.Current_Layer = currentLayer;
        //                        item.Current_X = srm.srmStatus.PositionMmX.Value;
        //                        item.Current_Y = srm.srmStatus.PositionMmY.Value;
        //                        item.Current_Z = currentZ;
        //                        item.Alarm_Code = alarmCode;
        //                        item.Alarm_Info = alarmInfo;
        //                        item.Update_Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //                        dc.UpdateEntity(item);
        //                        dc.SaveChanges();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        ContextService.Log.Error("记录货叉1脱机" + ex.Message);
        //                        //throw;
        //                    }

        //                }
        //                #endregion
        //                #region 报警
        //                //货叉1
        //                if (srm.srmStatus.FaultState.Value.ToSiemensString() == "FL" && alarmSame == false)
        //                {
        //                    currentZ = srm.CurrentZ();
        //                    currentRank = Convert.ToInt32(srm.ForkCenter());
        //                    currentColumn = srm.CurrentColumn();
        //                    currentLayer = srm.CurrentLayer();
        //                    forkCenter = srm.ForkCenter();
        //                    hasGoods = srm.HasGoods();
        //                    try
        //                    {
        //                        item.ForkCenter = forkCenter;
        //                        item.HasGood = hasGoods;
        //                        item.Device_Status = 3;//故障
        //                        item.Run_Mode = srm.srmStatus.OperationMode.Value.ToSiemensString() != "OF" ? (srm.srmStatus.OperationMode.Value.ToSiemensString() == "AU" ? 2 : 1) : 3; //
        //                        item.Current_Rank = currentRank;
        //                        item.Current_Column = currentColumn;
        //                        item.Current_Layer = currentLayer;
        //                        item.Current_X = srm.srmStatus.PositionMmX.Value;
        //                        item.Current_Y = srm.srmStatus.PositionMmY.Value;
        //                        item.Current_Z = currentZ;
        //                        item.Alarm_Code = alarmCode;
        //                        item.Alarm_Info = alarmInfo;//dc.Set<Dictonary>().Where(y => y.Value == srm.srmStatus.Alarm_Code.Value.ToString()).Where(y => y.Type == "Device_AlarmInfo~WISH.SRM.V10").FirstOrDefault().Label
        //                        item.Update_Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //                        dc.UpdateEntity(item);
        //                        dc.SaveChanges();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        ContextService.Log.Error("记录货叉1报警" + ex.Message);
        //                        //throw;
        //                    }
        //                }
        //                #endregion
        //            }
        //            #endregion

        //            #region 货叉2
        //            //if (item.Fork_No.Equals("2"))
        //            //{
        //            //    #region 记录设备任务执行状态
        //            //    foreach (SrmSpare srmTaskExec in srmTaskStatus)
        //            //    {
        //            //        if (srmTaskExec.Srm_No.Equals(item.Srm_No) && srmTaskExec.Fork_No.Equals(item.Fork_No))
        //            //        {
        //            //            if (srmTaskExec.Spare1 != srm.srmStatus.Fork_Two.Exec_Step.Value.ToString())
        //            //            {
        //            //                srmTaskExec.Spare1 = srm.srmStatus.Fork_Two.Exec_Step.Value.ToString();
        //            //                srmTaskExec.UpdateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //            //                srmTaskExec.UpdateBy = "PLC";
        //            //                dc.UpdateEntity(srmTaskExec);
        //            //                dc.SaveChanges();
        //            //            }
        //            //        }
        //            //    }
        //            //    #endregion

        //            //    #region 待命（堆垛机自动状态时，上位机下发堆垛机指令）
        //            //    //货叉2
        //            //    if (srm.srmStatus.Run_Mode.Value == (Int16)SrmMode.Auto && srm.srmStatus.Alarming.Value == false && srm.srmStatus.State.Value == 2)
        //            //    {
        //            //        try
        //            //        {
        //            //            item.Device_Status = 2;//待命
        //            //            item.Run_Mode = 2;//自动
        //            //            item.Current_Rank = 0;
        //            //            item.Current_Column = 0;
        //            //            item.Current_Layer = 0;
        //            //            item.Current_X = srm.srmStatus.Current_X.Value;
        //            //            item.Current_Y = srm.srmStatus.Current_Y.Value;
        //            //            item.Current_Z = srm.srmStatus.Fork_Two.Current_Z1.Value;
        //            //            item.Alarm_Code = alarmCode;
        //            //            item.Alarm_Info = alarmInfo;
        //            //            item.Update_Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //            //            dc.UpdateEntity(item);
        //            //            dc.SaveChanges();
        //            //        }
        //            //        catch (Exception ex)
        //            //        {
        //            //            ContextService.Log.Error("记录货叉2空闲" + ex.Message);
        //            //            //throw;
        //            //        }

        //            //    }
        //            //    #endregion

        //            //    #region 运行中

        //            //    //货叉2
        //            //    if (srm.srmStatus.Run_Mode.Value == (Int16)SrmMode.Auto && srm.srmStatus.Alarming.Value == false && srm.srmStatus.State.Value == 1)// srm.srmStatus.Fork_Two.Has_Goods.Value == true
        //            //    {
        //            //        try
        //            //        {
        //            //            item.Device_Status = 1;//运行
        //            //            item.Run_Mode = 2;//自动
        //            //            item.Current_Rank = 0;
        //            //            item.Current_Column = 0;
        //            //            item.Current_Layer = 0;
        //            //            item.Current_X = srm.srmStatus.Current_X.Value;
        //            //            item.Current_Y = srm.srmStatus.Current_Y.Value;
        //            //            item.Current_Z = srm.srmStatus.Fork_Two.Current_Z1.Value;
        //            //            item.Alarm_Code = alarmCode;
        //            //            item.Alarm_Info = alarmInfo;
        //            //            item.Update_Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //            //            dc.UpdateEntity(item);
        //            //            dc.SaveChanges();
        //            //        }
        //            //        catch (Exception ex)
        //            //        {
        //            //            ContextService.Log.Error("记录货叉2运行中" + ex.Message);
        //            //            //throw;
        //            //        }


        //            //    }
        //            //    #endregion

        //            //    #region 手动

        //            //    //货叉2
        //            //    if (srm.srmStatus.Run_Mode.Value == (Int16)SrmMode.Man && srm.srmStatus.Alarming.Value == false)
        //            //    {
        //            //        try
        //            //        {
        //            //            item.Device_Status = 0;//运行
        //            //            item.Run_Mode = 1;//手动
        //            //            item.Current_Rank = 0;
        //            //            item.Current_Column = 0;
        //            //            item.Current_Layer = 0;
        //            //            item.Current_X = srm.srmStatus.Current_X.Value;
        //            //            item.Current_Y = srm.srmStatus.Current_Y.Value;
        //            //            item.Current_Z = srm.srmStatus.Fork_Two.Current_Z1.Value;
        //            //            item.Alarm_Code = alarmCode;
        //            //            item.Alarm_Info = alarmInfo;
        //            //            item.Update_Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //            //            dc.UpdateEntity(item);
        //            //            dc.SaveChanges();
        //            //        }
        //            //        catch (Exception ex)
        //            //        {
        //            //            ContextService.Log.Error("记录货叉2手动" + ex.Message);
        //            //            //throw;
        //            //        }

        //            //    }
        //            //    #endregion

        //            //    #region 脱机

        //            //    //货叉2
        //            //    if (srm.srmStatus.Run_Mode.Value == (Int16)SrmMode.Off)
        //            //    {
        //            //        try
        //            //        {
        //            //            item.Device_Status = 3;//故障
        //            //            item.Run_Mode = 3;//脱机
        //            //            item.Current_Rank = 0;
        //            //            item.Current_Column = 0;
        //            //            item.Current_Layer = 0;
        //            //            item.Current_X = srm.srmStatus.Current_X.Value;
        //            //            item.Current_Y = srm.srmStatus.Current_Y.Value;
        //            //            item.Current_Z = srm.srmStatus.Fork_Two.Current_Z1.Value;
        //            //            item.Alarm_Code = alarmCode;
        //            //            item.Alarm_Info = alarmInfo;
        //            //            item.Update_Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //            //            dc.UpdateEntity(item);
        //            //            dc.SaveChanges();
        //            //        }
        //            //        catch (Exception ex)
        //            //        {
        //            //            ContextService.Log.Error("记录货叉2脱机" + ex.Message);
        //            //            //throw;
        //            //        }

        //            //    }
        //            //    #endregion

        //            //    #region 报警

        //            //    //货叉2
        //            //    if (srm.srmStatus.Alarming.Value == true && alarmSame == false)
        //            //    {
        //            //        try
        //            //        {
        //            //            item.Device_Status = 3;//故障
        //            //            item.Run_Mode = srm.srmStatus.Run_Mode.Value != 0 ? (srm.srmStatus.Run_Mode.Value == 1 ? 2 : 1) : 3;//
        //            //            item.Current_Rank = 0;
        //            //            item.Current_Column = 0;
        //            //            item.Current_Layer = 0;
        //            //            item.Current_X = srm.srmStatus.Current_X.Value;
        //            //            item.Current_Y = srm.srmStatus.Current_Y.Value;
        //            //            item.Current_Z = srm.srmStatus.Fork_Two.Current_Z1.Value;
        //            //            item.Alarm_Code = alarmCode;
        //            //            item.Alarm_Info = alarmInfo;
        //            //            item.Update_Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //            //            dc.UpdateEntity(item);
        //            //            dc.SaveChanges();
        //            //        }
        //            //        catch (Exception ex)
        //            //        {
        //            //            ContextService.Log.Error("记录货叉2报警" + ex.Message);
        //            //            //throw;
        //            //        }

        //            //    }
        //            //    #endregion
        //            //}
        //            #endregion
        //        }
        //        #endregion
        //    }
        //    #endregion


        //}

        #endregion


        /// <summary>
        /// wcs2srm写任务，如果写入数据成功，返回true，接下来完成stb, ack交互
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="device"></param>
        /// <param name="srm"></param>
        /// <returns></returns>
        private bool DoWcs2SrmTask(DataRow row, DeviceConfig device, WishSrmV10 srm)
        {
            bool res = false;
            #region 任务字段定义
            string SUBTASK_NO = row["Task_Id"].ToString();
            string Source_Id = row["Source_Id"].ToString();
            string TASK_TYPE = row["TASK_TYPE"].ToString();
            string Device_NO = row["Srm_No"].ToString();
            string Fork_NO = row["Lhd_Id"].ToString();
            string Task_Flag = row["Task_Flag"].ToString();
            string Station_Type = row["Station_Type"].ToString();
            string Cmd_Type = row["Cmd_Type"].ToString();
            string Task_Cmd = row["Task_Cmd"].ToString();
            string Task_Status = row["Task_Status"].ToString();
            string BEGIN_STATION = row["From_Station"].ToString();
            string BEGIN_RANK = row["From_ForkDirection"].ToString();
            string BEGIN_COLUMN = row["From_Column"].ToString();
            string BEGIN_LAYER = row["From_Layer"].ToString();
            string BEGIN_DEEP = row["From_Deep"].ToString();
            string END_STATION = row["To_Station"].ToString();
            string END_RANK = row["To_ForkDirection"].ToString();
            string END_COLUMN = row["To_Column"].ToString();
            string END_LAYER = row["To_Layer"].ToString();
            string END_DEEP = row["To_Deep"].ToString();
            string HAS_SEND = row["HAS_SEND"].ToString();
            #endregion
            //转换指令
            string operanID = OperationID(Cmd_Type, Task_Cmd);
            //转换数据体
            string operantion = CreateOperation(row, "01", device.Device_Code);



            #region 判断是否写入成功

            res = DoForkTask(srm, device, operanID, operantion);
            #endregion


            return res;

        }
        #region 指令信息转换
        /// <summary>
        /// 指令转换，cmdType=1新任务，2=修改任务，3=删除任务，4=重置，5=报警
        /// taskType=1整套取放货动作，2=行走，3=单取，4=单放
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="taskType"></param>
        /// <returns></returns>
        private string OperationID(string cmdType, string taskType)
        {
            string operaID = "";
            if (cmdType == "1")
            {
                if (taskType == "1" || taskType == "3")
                {
                    //taskType == "3"整套取放货动作，盘点载货台类型任务，目的位置到货叉不放货，并且还要生成到库口的移动指令。
                    operaID = "SRMA_TM";//整套取放货动作
                }
                else if (taskType == "2")
                {
                    operaID = "SRMA_PM";//单独行走
                }
                else if (taskType == "4")
                {
                    operaID = "SRMA_MM";//单放
                }
            }
            else if (cmdType == "3")
            {
                operaID = "SRMA_MC";//取消
            }
            else
            {
                ContextService.Log.Error("无对应指令类型");
            }
            return operaID;
        }

        private string CreateOperation(DataRow row, string lhd, string device)
        {
            List<string> list_address = BuilderAddressInfos(row, lhd, device);
            string containerCode = "";
            int pickID = 1;
            int dropID = 2;
            OperationBlock operationBlock = new OperationBlock
            {
                TID = row["Source_Id"].ToString().PadLeft(10, '0'),
                CID = row["Task_Id"].ToString().PadLeft(10),
                TUID = containerCode.ToString().PadLeft(10),
                Source = list_address[0].PadLeft(20),
                Destination = list_address[1].PadLeft(20),
                Returncode = "OK",
                TourNumber = "0000",
                Pick_ID = pickID.ToString().PadLeft(2),
                Drop_ID = dropID.ToString().PadLeft(2),
                LHD_ID = lhd
            };
            return operationBlock.ToString();
        }
        private List<string> BuilderAddressInfos(DataRow dr, string lhd, string device)
        {
            int aisleNo = 0;
            int pattern = 1;
            int location = 1;
            int retain = 0;

            string TaskId = dr["Task_Id"].ToString();
            string taskType = dr["TASK_TYPE"].ToString();
            string stationType = dr["Station_Type"].ToString();
            string taskFlag = dr["Task_Flag"].ToString();
            string taskState = dr["Task_Status"].ToString();
            string cmdType = dr["Cmd_Type"].ToString();

            string lhdId = "G" + device + lhd + "1";//货叉位置

            string sourceStation = dr["From_Station"].ToString();//起始站台

            string sourceCell = aisleNo.ToString().PadLeft(2, '0')
                + dr["From_ForkDirection"].ToString().PadLeft(2, '0')
                + dr["From_Deep"].ToString().PadLeft(2, '0')
                + dr["From_Column"].ToString().PadLeft(4, '0')
                + dr["From_Layer"].ToString().PadLeft(4, '0')
                + pattern.ToString().PadLeft(2, '0')
                + location.ToString().PadLeft(2, '0')
                + retain.ToString().PadLeft(2, '0');//起始货位

            string targetStation = dr["To_Station"].ToString();//目标站台

            string targetCell = aisleNo.ToString().PadLeft(2, '0')
                + dr["To_ForkDirection"].ToString().PadLeft(2, '0')
                + dr["TO_DEEP"].ToString().PadLeft(2, '0')
                + dr["To_Column"].ToString().PadLeft(4, '0')
                + dr["To_Layer"].ToString().PadLeft(4, '0')
                + pattern.ToString().PadLeft(2, '0')
                + location.ToString().PadLeft(2, '0')
                + retain.ToString().PadLeft(2, '0');//目标货位

            List<string> listReturn = new List<string>();
            //取消任务
            if (cmdType == "3")
            {
                listReturn.Add("");
                listReturn.Add("");
                return listReturn;
            }
            //载货台--只有盘点
            if (stationType == "1")
            {
                //盘出 货位到货叉
                if (taskType == "30")
                {
                    listReturn.Add(sourceCell);
                    listReturn.Add(lhdId);
                }
                //盘入--货叉到货位
                else if (taskType == "31")
                {
                    listReturn.Add(lhdId);
                    listReturn.Add(targetCell);
                }
                else
                {
                    ContextService.Log.Error("类型为载货台只能是盘点任务");
                }
            }
            //库口 月台
            else
            {
                //入库 盘入
                if (taskType == "10" || taskType == "31")
                {
                    if (taskFlag == "20")//满入处理单放
                    {
                        listReturn.Add(lhdId);
                        listReturn.Add(targetCell);
                    }
                    else
                    {
                        listReturn.Add(sourceStation);
                        listReturn.Add(targetCell);
                    }
                }
                //出库 盘出
                else if (taskType == "20" || taskType == "30")
                {
                    listReturn.Add(sourceCell);
                    listReturn.Add(targetStation);
                }
                //移库
                else if (taskType == "40")
                {
                    if (taskFlag == "20")//满入处理单放
                    {
                        listReturn.Add(lhdId);
                        listReturn.Add(targetCell);
                    }
                    else
                    {
                        listReturn.Add(sourceCell);
                        listReturn.Add(targetCell);
                    }
                }
                //站台搬运
                else if (taskType == "50")
                {
                    listReturn.Add(sourceStation);
                    listReturn.Add(targetStation);
                }
                //回站台 盘点确认出库--货叉到库口
                else if (taskType == "60" || taskType == "32")
                {
                    listReturn.Add(lhdId);
                    listReturn.Add(targetStation);
                }
                else
                {
                    ContextService.Log.Error("任务类型不对");
                }
            }
            //取消任务
            if (cmdType == "3")
            {
                listReturn.Add("");
                listReturn.Add("");
            }
            return listReturn;

        }
        #endregion

        #region 写任务
        private bool DoForkTask(WishSrmV10 srm, DeviceConfig device, string operationID, string operationBlock)
        {
            bool res = false;
            #region 货叉数据体

            //开始字符
            Signal<string> StartChar = (Signal<string>)srm.wcs2Srm.StartChar.Clone();
            StartChar.Value = "RT";

            //版本
            Signal<string> Version = (Signal<string>)srm.wcs2Srm.Version.Clone();
            Version.Value = "01";

            //消息序列，1-9999，未使用
            //Signal<Int16> DatagramCounter = (Signal<Int16>)srm.wcs2Srm.DatagramCounter.Clone();
            //DatagramCounter.Value = 0;

            //消息反馈值，未使用
            //Signal<string> ReturnValue = (Signal<string>)srm.wcs2Srm.ReturnValue.Clone();
            //ReturnValue.Value = "";

            //消息长度，未使用
            //Signal<Int16> DatagramLength = (Signal<Int16>)srm.wcs2Srm.DatagramLength.Clone();
            //DatagramLength.Value = 0;

            //发送方节点编号
            Signal<string> Sender = (Signal<string>)srm.wcs2Srm.Sender.Clone();
            Sender.Value = "LDMS01";

            //接收方节点编号
            Signal<string> Receiver = (Signal<string>)srm.wcs2Srm.Receiver.Clone();
            Receiver.Value = device.Device_Code;

            //应答标志，未使用
            //Signal<string> FlowControl = (Signal<string>)srm.wcs2Srm.FlowControl.Clone();
            //Sender.Value = "";

            //消息类型代码
            Signal<string> OperationID = (Signal<string>)srm.wcs2Srm.OperationID.Clone();
            OperationID.Value = operationID;

            //数据体区数量，默认1
            Signal<Int16> OperationBlockCount = (Signal<Int16>)srm.wcs2Srm.OperationBlockCount.Clone();
            OperationBlockCount.Value = 1;

            //数据体区长度，未使用
            //Signal<Int16> OperationBlockLength = (Signal<Int16>)srm.wcs2Srm.OperationBlockLength.Clone();
            //OperationBlockLength.Value = 0;

            //数据体1
            Signal<char[]> OperationBlock1 = (Signal<char[]>)srm.wcs2Srm.OperationBlock1.Clone();
            OperationBlock1.Value = operationBlock.ToCharArray();

            //数据体1
            //Signal<char[]> OperationBlock2 = (Signal<char[]>)srm.wcs2Srm.OperationBlock2.Clone();
            //OperationBlock2.Value = Convert.ToInt16(Task_Cmd);

            #endregion
            Thread.Sleep(200);//停顿200毫秒
            List<BaseSignal> signals = new List<BaseSignal>();
            // 检查WCS2Conv的反馈数据是否和发送的数据一致

            //开始字符
            bool compRes = srm.wcs2Srm.StartChar.Value.ToSiemensString().Equals(StartChar.Value);
            if (compRes == false) signals.Add(StartChar);
            //版本
            compRes = srm.wcs2Srm.Version.Value.ToSiemensString().Equals(Version.Value);
            if (compRes == false) signals.Add(Version);
            //消息序列，1-9999，未使用
            //compRes = srm.wcs2Srm.DatagramCounter.Value.Equals(DatagramCounter.Value);
            //if (compRes == false) signals.Add(DatagramCounter);
            //消息反馈值，未使用
            //compRes = srm.wcs2Srm.ReturnValue.Value.Equals(ReturnValue.Value);
            //if (compRes == false) signals.Add(ReturnValue);
            //消息长度，未使用
            //compRes = srm.wcs2Srm.DatagramLength.Value.Equals(DatagramLength.Value);
            //if (compRes == false) signals.Add(DatagramLength);
            //发送方节点编号
            compRes = srm.wcs2Srm.Sender.Value.ToSiemensString().Equals(Sender.Value);
            if (compRes == false) signals.Add(Sender);
            //接收方节点编号
            compRes = srm.wcs2Srm.Receiver.Value.ToSiemensString().Equals(Receiver.Value);
            if (compRes == false) signals.Add(Receiver);
            //应答标志，未使用
            //compRes = srm.wcs2Srm.FlowControl.Value.Equals(FlowControl.Value);
            //if (compRes == false) signals.Add(FlowControl);
            //消息类型代码
            compRes = srm.wcs2Srm.OperationID.Value.ToSiemensString().Equals(OperationID.Value);
            if (compRes == false) signals.Add(OperationID);
            //数据体区数量，默认1
            compRes = srm.wcs2Srm.OperationBlockCount.Value.Equals(OperationBlockCount.Value);
            if (compRes == false) signals.Add(OperationBlockCount);
            //数据体区长度，未使用
            //compRes = srm.wcs2Srm.OperationBlockLength.Value.Equals(OperationBlockLength.Value);
            //if (compRes == false) signals.Add(OperationBlockLength);
            //数据体1
            for (int i = 0; i < OperationBlock1.Value.Length; i++)
            {
                compRes = srm.wcs2Srm.OperationBlock1.Value[i].Equals(OperationBlock1.Value[i]);
            }
            if (compRes == false) signals.Add(OperationBlock1);
            //数据体2
            //compRes = srm.wcs2Srm.OperationBlock2.Value.Equals(OperationBlock2.Value);
            //if (compRes == false) signals.Add(OperationBlock2);

            if (signals.Count > 0)
            {
                foreach (var signal in signals)
                {
                    PlcService.GetInstance().WriteSignal(device.PlcConfig.Plc_Code, signal, null);
                }
            }
            else
            {
                res = true;
            }
            return res;
        }
        #endregion

        /// <summary>
        /// 处理堆垛机反馈任务异常
        /// </summary>
        /// <param name="device"></param>
        /// <param name="jobError"></param>
        /// <param name="jobState"></param>
        private void DoSrmTaskError(DeviceConfig device, int jobError1, int jobState1)
        {
            // 6 放货时有货
            // 8 放货后载货台有货
            // 11 放货阻塞
            // 7 取货后载货台无货
            // 9 取货时无货
            // 12 取货阻塞
            if (jobState1 == 9)
            {
                string msg = "";
                switch (jobError1)
                {
                    case 6:
                        msg = "货叉1满入: 放货时有货";
                        break;
                    case 8:
                        msg = "货叉1满入: 放货后载货台有货";
                        break;
                    case 11:
                        msg = "货叉1满入: 放货阻塞";
                        break;
                    case 7:
                        msg = "货叉1空出: 取货后载货台无货";
                        break;
                    case 9:
                        msg = "货叉1空出: 取货时无货";
                        break;
                    case 12:
                        msg = "货叉1空出: 取货阻塞";
                        break;
                    default:
                        msg = $"货叉1未知异常，错误代码 {jobError1}";
                        break;
                }
                MsgService.GetInstance().NewDeviceMsg(device.Device_Code, ETaskExecMsgType.WARN, msg);
            }
        }



    }



}