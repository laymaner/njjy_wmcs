//=============================================================================
//                                 A220101
//=============================================================================
//
// 设备消息服务。用于记录设备的报警、状态变更、交互报文等消息记录。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/1/30
//      创建
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ASRS.WCS.Common.Enum;
using WISH.WCS.Device.SrmSocket.WishSrmV10Udt;
using ASRS.WCS.PLC;
using Wish.HWConfig.Models;
using WISH.WCS.Device.SrmSocket.WishSrmSocketUdt;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Aliyun.OSS;
using log4net;
using System.Collections;

namespace Wish.Service
{

    public interface DeviceMsgListener
    {
        /// <summary>
        /// 通知设备有新消息
        /// </summary>
        /// <param name="device"></param>
        /// <param name="msg"></param>
        public void NoticeMsg(string device, string msg, ETaskExecMsgType msgType);
    }


    public class MsgService : IPlcMsgService
    {

        private ILog logger = LogManager.GetLogger(typeof(MsgService));
        public static List<DeviceMsgListener> DeviceMsgListeners = new List<DeviceMsgListener>();

        private static Dictionary<string, string> DeviceAlarmHistory = new Dictionary<string, string>();

        /// <summary>
        /// 需要保存数据库的消息对象
        /// </summary>
        private static ConcurrentQueue<object> DBMsgQueue = new ConcurrentQueue<object>();
        private static ConcurrentQueue<Dictionary<string, object>> AlarmMsgQueue = new ConcurrentQueue<Dictionary<string, object>>();

        private bool isRunning = false;

        
        private MsgService()
        {
        }

        private static MsgService instance;
        public static MsgService GetInstance()
        {
            if (instance == null)
            {
                instance = new MsgService();
            }
            return instance;
        }

        public void Start()
        {
            isRunning = true;

            Task.Factory.StartNew(() =>
            {
                while (isRunning)
                {
                    int count = 0;
                    // 一次最多处理10个消息，以免影响信号同步
                    object obj;
                    Dictionary<string, object> strObj = new Dictionary<string, object>();
                    while (count < 10)
                    {
                        try
                        {
                            bool hasGetQueueObj = DBMsgQueue.TryDequeue(out obj);
                            //ContextService.Log.Error($"记录日志返回值:{hasGetQueueObj}");
                            if (hasGetQueueObj)
                            {
                                if (obj.GetType() == typeof(DeviceTaskLog))
                                {
                                    DeviceTaskLog dtl = (DeviceTaskLog)obj;
                                    //ContextService.Log.Error($"记录任务日志队列:{dtl}");
                                    DCService.GetInstance().NewEntity(dtl);
                                }
                                if (obj.GetType() == typeof(DeviceStatusLog))
                                {
                                    DeviceStatusLog dtl = (DeviceStatusLog)obj;
                                    //ContextService.Log.Error($"记录状态队列:{dtl}");
                                    DCService.GetInstance().NewEntity(dtl);
                                }
                            }
                            bool hasAarmMsgObj = AlarmMsgQueue.TryDequeue(out strObj);
                            if (hasAarmMsgObj)
                            {
                                if (strObj != null)
                                {
                                    foreach (var pair in strObj)
                                    {
                                        Console.WriteLine($"Key: {pair.Key}, Value: {pair.Value}");
                                        if(pair.Key.Equals("Add"))
                                        {
                                            if (pair.Value.GetType()==typeof(DeviceAlarmLog))
                                            {
                                                DeviceAlarmLog dtl = (DeviceAlarmLog)pair.Value;
                                                //ContextService.Log.Error($"记录状态队列:{dtl}");
                                                DCService.GetInstance().NewEntity(dtl);
                                            }
                                        }
                                        else if (pair.Key.Equals("Update"))
                                        {
                                            if (pair.Value.GetType() == typeof(DeviceAlarmLog))
                                            {
                                                DeviceAlarmLog dtl = (DeviceAlarmLog)pair.Value;
                                                //ContextService.Log.Error($"记录状态队列:{dtl}");
                                                DCService.GetInstance().UpdateEntity(dtl);
                                            }
                                        }
                                        
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Warn($"==================================记录日志的异常:{ex.Message}===================================");
                            //throw;
                        }
                        
                        count++;
                        ;
                    }


                    Thread.Sleep(100);
                }
            });
        }

        public void Stop()
        {
            isRunning = false;
        }


        /// <summary>
        /// 添加消息处理监听
        /// </summary>
        /// <param name="listener"></param>
        public void AddListener(DeviceMsgListener listener)
        {
            DeviceMsgListeners.Add(listener);
        }


        /// <summary>
        /// 设备新消息处理，主要负责任务执行过程中的信息记录
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <param name="type">            DEBUG = 1, INFO = 2, WARN = 3,</param>
        /// <param name="msg"></param>
        public void NewDeviceMsg(string deviceNo, ETaskExecMsgType type, string msg)
        {
            bool newMsg = false;
            if (DeviceAlarmHistory.ContainsKey(deviceNo))
            {
                string hisMsg = DeviceAlarmHistory[deviceNo];
                if (hisMsg != null && msg.Equals(msg))
                {
                    return;
                }
                else
                {
                    DeviceAlarmHistory.Remove(deviceNo);
                    DeviceAlarmHistory.Add(deviceNo, msg);
                    newMsg = true;
                }
            }
            else
            {
                DeviceAlarmHistory.Add(deviceNo, msg);
                newMsg = true;
            }

            if (newMsg)
            {
                foreach (var item in DeviceMsgListeners)
                {
                    item.NoticeMsg(deviceNo, msg, (ETaskExecMsgType)type);
                }
                if (type == ETaskExecMsgType.DEBUG)
                    logger.Debug($"{deviceNo}:{msg}");
                if (type == ETaskExecMsgType.INFO)
                    logger.Info($"{deviceNo}:{msg}");
                if (type == ETaskExecMsgType.WARN)
                    logger.Warn($"{deviceNo}:{msg}");
            }
        }




        /// <summary>
        /// 设备状态变更
        /// </summary>
        /// <param name="device"></param>
        /// <param name="status"></param>
        public static void NewDeviceStatusChange(DeviceConfig device, string attribute, string changeContent , object status)
        {
            DeviceStatusLog deviceStatusLog = new DeviceStatusLog();
            deviceStatusLog.Device_Code = device.Device_Code;
            deviceStatusLog.Content = changeContent;
            deviceStatusLog.Attribute = attribute;
            deviceStatusLog.CreateTime = global::System.DateTime.Now;
            bool newMsg = true;
            if (status.GetType() == typeof(UdtSrmSocketStatus))
            {
                deviceStatusLog.Message = (status as UdtSrmSocketStatus).ToJson();
            } 
            //else if (status.GetType() == typeof(UdtConvStatus))
            //{
            //    deviceStatusLog.Message = (status as UdtConvStatus).ToJson();
            //}
            else
            {
                newMsg = false;
            }
            if (DBMsgQueue.Count < 1000 && newMsg)
            {
                DBMsgQueue.Enqueue(deviceStatusLog);
            }
        }

        public void NewDeviceStatusChange(string deviceCode, string attribute, string changeMsg, object status)
        {
            DeviceStatusLog deviceStatusLog = new DeviceStatusLog();
            deviceStatusLog.Device_Code = deviceCode;
            deviceStatusLog.Content = changeMsg;
            deviceStatusLog.Attribute = attribute;
            deviceStatusLog.CreateTime = global::System.DateTime.Now;
            bool newMsg = true;


            if (status.GetType().IsSubclassOf(typeof(BaseUdt)))
            {
                deviceStatusLog.Message = (status as BaseUdt).ToJson();
            }
            else
            {
                newMsg = false;
            }

            //if (status.GetType() == typeof(UdtSrmStatus))
            //{
            //    deviceStatusLog.Message = (status as UdtSrmStatus).ToJson();
            //}
            //else if (status.GetType() == typeof(UdtConvStatus))
            //{
            //    deviceStatusLog.Message = (status as UdtConvStatus).ToJson();
            //}
            //else
            //{
            //    newMsg = false;
            //}
            //ContextService.Log.Error($"记录状态日志队列数量:{DBMsgQueue.Count}：{newMsg}");
            if (DBMsgQueue.Count < 1000 && newMsg)
            {
                DBMsgQueue.Enqueue(deviceStatusLog);
            }
        }

        public void NewDeviceAlarm(string deviceCode, string msg, object statusDB)
        {

            var alarms =  DCService.GetInstance().GetDC().Set<DeviceAlarmLog>().Where(x => x.Device_Code == deviceCode && x.HandleFlag == 0).ToList();
            if (alarms.Count==0 && !string.IsNullOrWhiteSpace(msg))
            {
                DeviceAlarmLog alarm = new DeviceAlarmLog();
                alarm.Device_Code = deviceCode;

                alarm.Message = msg;
                alarm.OriginTime = global::System.DateTime.Now;
                alarm.CreateTime = global::System.DateTime.Now;
                alarm.CreateBy = deviceCode;
                //DCService.GetInstance().NewEntity(alarm);
                // 向队列中添加一个字典
                var dict = new Dictionary<string, object>
                {
                    { "Add", alarm }
                };
                AlarmMsgQueue.Enqueue(dict);
            }
            else if(alarms.Count>0)
            {
                foreach (var alarm in alarms)
                {
                    var alarmLogs = AlarmMsgQueue.Where(item => item.ContainsKey("Add")).Select(x => (DeviceAlarmLog)x["Add"]).ToList();
                    List<string> alarmOlds = alarm.Message.Split(',').ToList();
                    List<string> alarmNews = msg.Split(',').ToList();
                    bool areEqual = alarmOlds.SequenceEqual(alarmNews);
                    foreach (var item in alarmLogs)
                    {
                        List<string> alarmOldLogs= item.Message.Split(',').ToList();
                        areEqual = alarmOldLogs.SequenceEqual(alarmNews);
                    }
                    if (!string.IsNullOrWhiteSpace(msg) && !areEqual)
                    {
                        DeviceAlarmLog alarmEntity = new DeviceAlarmLog();
                        alarmEntity.Device_Code = deviceCode;

                        alarmEntity.Message = msg;
                        alarmEntity.OriginTime = global::System.DateTime.Now;
                        alarmEntity.CreateTime = global::System.DateTime.Now;
                        alarmEntity.CreateBy = deviceCode;
                        //DCService.GetInstance().NewEntity(alarm);
                        //DCService.GetInstance().GetDC().SaveChanges();
                        // 向队列中添加一个字典
                        var dict = new Dictionary<string, object>
                        {
                            { "Add", alarmEntity }
                        };
                        AlarmMsgQueue.Enqueue(dict);
                    }
                    else
                    {
                        alarm.HandleFlag = 1;
                        alarm.UpdateBy = deviceCode;
                        alarm.UpdateTime = global::System.DateTime.Now;
                        alarm.EndTime = global::System.DateTime.Now;
                        //DCService.GetInstance().UpdateEntity(alarms);
                        //DCService.GetInstance().GetDC().SaveChanges();
                        // 向队列中添加一个字典
                        var dict = new Dictionary<string, object>
                        {
                            { "Update", alarm }
                        };
                        AlarmMsgQueue.Enqueue(dict);
                    }
                }
            }
            
        }

        public void NewPageClientMsg(string msg)
        {
            WcsViewSocketServer.GetInstanceAsync().SendWcsWebJsonMssage(msg);
        }

        public void NewTaskCommunication(string deviceNo, EPlcCommDirect direct, string taskNo, object commInfo)
        {
            //string msg = JsonConvert.SerializeObject(commInfo, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            //string msg = JsonConvert.SerializeObject(commInfo);
            DeviceTaskLog dtl = new DeviceTaskLog();
            dtl.Device_Code = deviceNo;
            dtl.Task_No = taskNo;
            dtl.Message = commInfo.ToString();

            dtl.Direct = direct == EPlcCommDirect.Plc2Wcs ? "Plc2Wcs" : "Wcs2Plc";
            dtl.CreateTime = global::System.DateTime.Now;
            dtl.CreateBy = deviceNo;
            //ContextService.Log.Error($"记录任务日志队列数量:{DBMsgQueue.Count}");
            if (DBMsgQueue.Count < 1000)
            {
                DBMsgQueue.Enqueue(dtl);
            }
        }
    }

}
