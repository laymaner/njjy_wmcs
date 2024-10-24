//=============================================================================
//                                 A220101
//=============================================================================
//
// Plc配置类。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/1/9
//      创建
//
//-----------------------------------------------------------------------------
using HslCommunication;
using HslCommunication.Core.Net;
using HslCommunication.Profinet.Siemens;
using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASRS.WCS.PLC
{

    struct HeartBeatTimstamp
    {
        public object value;
        public DateTime timeStamp;

    }
    /// <summary>
    /// Plc 配置信息。
    /// </summary>
    public class Plc
    {
        private HeartBeatTimstamp OldHeartBeatInfo;

        /// <summary>
        /// Plc编号。
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Plc名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Plc地址。
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 真表示启用。
        /// </summary>
        public bool IsEnabled { get; set; }

        public List<PlcDataBlock> DBs {get;set;}

        private Dictionary<string, PlcDataBlock> dbDict = new Dictionary<string, PlcDataBlock>();

        /// <summary>
        /// 根据DB块名称获取DB对象。
        /// </summary>
        /// <param name="datablockId">数据块编号，如"DB10"。</param>
        /// <returns></returns>
        public PlcDataBlock GetDataBlock(string datablockId)
        {
            PlcDataBlock res = null;
            if (dbDict.ContainsKey(datablockId))
            {
                res = dbDict[datablockId];
            } 
            else
            {
                foreach(var item in DBs)
                {
                    if (item.Id.Equals(datablockId))
                    {
                        dbDict[datablockId] = item;
                        res = item;
                        break;
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// 心跳检测。
        /// </summary>
        public Heartbeat Heartbeat { get; set; }

        /// <summary>
        /// 真表示忽略心跳检测。
        /// </summary>
        public bool IsIgnoreHearbeat { get; set; } = false;

        public bool IsVerbose { get; set; } = false;

        public bool IsRunning { get; set; } = false;

        /// <summary>
        /// Plc刷新数据周期间隔，单位[毫秒]。
        /// </summary>
        public int ScanCycle { get; set; } = 100;

        private NetworkDeviceBase plcDevice;

        /// <summary>
        /// 与物理Plc连接通讯对象。
        /// </summary>
        public NetworkDeviceBase PlcDevice { 
            get 
            { 
                if(plcDevice == null)
                {
                    
                    plcDevice = new SiemensS7Net(SiemensPLCS.S1500, IP);
                }
                return plcDevice;
             }
            set
            {
                plcDevice = value;
            }
        }

        private ILog Log;

        public Plc(ILog log)
        {
            this.Log = log;
        }

        private ConcurrentQueue<BaseSignal> writeSignalQueue = new ConcurrentQueue<BaseSignal>();
        /// <summary>
        /// 写信号队列。
        /// </summary>
        public ConcurrentQueue<BaseSignal> WriteSignalQueue
        {
            get
            {
                return writeSignalQueue;
            }
        }


        private Dictionary<BaseSignal, Action> witeSignalQueueAction = new Dictionary<BaseSignal, Action>();
        public Dictionary<BaseSignal, Action> WriteSignalQueueAction
        {
            get
            {
                return witeSignalQueueAction;
            }
        }

        /// <summary>
        /// 自动读取数据对象。
        /// </summary>
        private Dictionary<string, IAutoRead> autoReadSignals = new Dictionary<string, IAutoRead>();
        public Dictionary<string, IAutoRead> AutoReadSignals
        {
            get
            {
                return autoReadSignals;
            }
        }

        /// <summary>
        /// 注册自动刷新数据对象。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="autoReadObj"></param>
        public void RegisterAutoReadObject(string key, IAutoRead autoReadObj)
        {
            autoReadSignals[key] = autoReadObj;
        }

        public IAutoRead GetRegisterAutoReadObject(string key)
        {
            if (autoReadSignals.ContainsKey(key))
                return autoReadSignals[key];
            else return null;
        }

        public bool IsConnect { get; set; } = false;


        public void Start()
        {
            IsRunning = true;
            {
                OldHeartBeatInfo.value = lastBeat;
                OldHeartBeatInfo.timeStamp = DateTime.Now;
                Task.Factory.StartNew(() =>
                {
                    Log.Info($"Plc [{Code},{Name}] start");
                    while (IsRunning)
                    {
                        try
                        {
                            // 通讯连接检测。
                            PlcDevice.ConnectTimeOut = 2000;
                            OperateResult rs = PlcDevice.ConnectServer();
                            if (rs.IsSuccess == false)
                            {
                                // ***********************************************
                                // add by ... 2022-11/1  监控设备连接状态
                                Log.Info($"Plc [{Code},{Name}]  IsConnect {IsConnect}，error:{rs.ErrorCode}:{rs.Message}");
                                //***********************************************
                                IsConnect = false;
                                continue;
                            }
                            IsConnect = true;

                            var sw = DateTime.Now;
                            UpdateDBData();
                            HandleWriteSignal();
                            if (IsVerbose)
                            {
                                Log.Debug($"Plc [{Code},{Name}] spent {(DateTime.Now - sw).Milliseconds}ms to sync signals");
                            }

                            //// 心跳检测。
                            //if (IsIgnoreHearbeat == false)
                            //{
                            //    HeartBeatTimstamp newHeartBeat;
                            //    newHeartBeat.timeStamp = sw;
                            //    newHeartBeat.value = plcDevice.ReadBool(Heartbeat.Signal.Address).Content;
                            //    if (newHeartBeat.value.Equals(OldHeartBeatInfo.value) == false)
                            //    {
                            //        OldHeartBeatInfo = newHeartBeat;
                            //        IsConnect = true;
                            //    }
                            //    else
                            //    {
                            //        TimeSpan t1 = new TimeSpan(newHeartBeat.timeStamp.Ticks);
                            //        TimeSpan t2 = new TimeSpan(OldHeartBeatInfo.timeStamp.Ticks);
                            //        TimeSpan t3 = t1.Subtract(t2);
                            //        // 检查和上一周期的时间差
                            //        if (t3.TotalMilliseconds > Heartbeat.MonitorCycle)
                            //        {
                            //            IsConnect = false;
                            //        }
                            //    }
                            //}
                        }
                        catch (Exception ex)
                        {
                            // ***********************************************
                            // add by ... 2022-11/1  监控异常异常情况
                            Log.Info($"Exception--  Plc [{Code},{Name}] {ex.Message}");
                            //***********************************************
                            Log.Error(ex);
                        }
                        Thread.Sleep(ScanCycle);
                    }
                    Log.Info($"Plc [{Code},{Name}] stop");
                });

                if (IsIgnoreHearbeat == true)
                {
                    Task.Factory.StartNew(() =>
                    {
                        Log.Info($"Plc Heartbeat [{Code},{Name}] start");
                        while (IsRunning)
                        {
                            if (IsConnect)
                            {
                                UpdateHeartbeat();
                            }
                            Thread.Sleep(Heartbeat.WriteInterval);
                        }
                        Log.Info($"Plc Heartbeat [{Code},{Name}] stop");
                    });
                }
            }
        }

        public void Stop()
        {
            IsRunning = false;
        }



        /// <summary>
        /// 更新内存DB块数据
        /// </summary>
        private void UpdateDBData()
        {
            foreach(PlcDataBlock db in DBs)
            {
               OperateResult<byte[]> res = plcDevice.Read(db.Address, (ushort)db.Length);
               if (res.IsSuccess == true)
               { 
                    db.Data = res.Content;
                } 
                else
                {
                    Log.Info($"Plc UpdateDBData [{Code},{Name},{db.Address},{(ushort)db.Length}] error");
                }
            }
            for (int i = 0; i < AutoReadSignals.Values.Count; i++)
            {
                if (AutoReadSignals.Values.ElementAt(i).IsEnabled) { 
                    List<object> signals = AutoReadSignals.Values.ElementAt(i).GetAutoReadSignals();
                    UpdateSinals(signals);
                    //lock (signals)
                    //{
                    //    UpdateSinals(signals);
                    //    //CurrentThread = Thread.CurrentThread.ManagedThreadId.ToString();
                    //}
                }
            }
        }

        private void UpdateSinals(List<object> signals) {
            {
                foreach (object signal in signals)
                {
                    if (signal is BaseSignal)
                    {
                        BaseSignal s = (BaseSignal)signal;
                        PlcDataUtil.ReadValue(this, s);
                    }
                    if (signal is BaseUdt)
                    {
                        BaseUdt s = (BaseUdt)signal;
                        UpdateSinals(s.GetSignals());
                    }
                }
            }
        }

        private bool lastBeat = false;

        /// <summary>
        /// 更新心跳状态。
        /// </summary>
        private void UpdateHeartbeat()
        {
            try
            {
                Heartbeat.Signal.Value = lastBeat;
                WriteSignalQueue.Enqueue(Heartbeat.Signal);
                lastBeat = !lastBeat;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        /// <summary>
        /// 从队列里取10条数据，写给PLc。
        /// </summary>
        private void HandleWriteSignal()
        {
            int count = 0;
            while (count < 10)
            {
                // 一次最多处理10个消息，以免影响信号同步
                BaseSignal signal;
                if (WriteSignalQueue.TryDequeue(out signal))
                {
                    writeSignal(signal);
                    count++;
                }
                else
                {
                    // 队列为空
                    break;
                }
            }
        }

        /// <summary>
        /// 写入Plc变量信号值。
        /// </summary>
        /// <param name="signal"></param>
        private void writeSignal(BaseSignal signal)
        {
            try
            {
                bool res = PlcDataUtil.WriteValue(PlcDevice, signal);
                //Log.Debug($"write siganl [address:{signal.Address},value={signal.ToJsonString()}]");
                if (WriteSignalQueueAction.ContainsKey(signal)) { 
                    Action action = WriteSignalQueueAction[signal];
                    if (action != null) 
                    { 
                        action(); 
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                signal.WriteTimeToLive++;
                if (signal.WriteTimeToLive <= 5)
                {
                    //22-11-01 添加进入异常清零
                    signal.WriteTimeToLive = 0;
                    // 失败次数小于5次继续
                    WriteSignalQueue.Enqueue(signal);
                }
            }
        }
    }
}
