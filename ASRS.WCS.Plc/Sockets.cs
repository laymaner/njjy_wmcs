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

using System.Net.Sockets;
using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Collections;
using System.Runtime.InteropServices;
using log4net.Util;

namespace ASRS.WCS.PLC
{

    struct SocketHeartBeatTimstamp
    {
        public object value;
        public DateTime timeStamp;

    }

    /// <summary>
    /// Socket 配置信息。
    /// </summary>
    public class Sockets
    {
        private HeartBeatTimstamp OldHeartBeatInfo;

        /// <summary>
        /// Socket编号。
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Socket名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Socket地址。
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// Socket端口
        /// </summary>
        public int IPPort { get; set; }

        /// <summary>
        /// 真表示启用。
        /// </summary>
        public bool IsEnabled { get; set; }

        public List<PlcDataBlock> DBs { get; set; }

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
                foreach (var item in DBs)
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
        public SocketHeartbeat Heartbeat { get; set; }

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


        /// <summary>
        /// 客户端对象
        /// </summary>
        //private TcpClient tcpClient = null;

        /// <summary>
        /// 为网络访问提供数据的基础流
        /// </summary>
        //private NetworkStream networkStream;
        /// <summary>
        /// 创建socket实例
        /// </summary>
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //Socket clientSocket = null;

        /// <summary>
        /// 与物理Plc连接通讯对象。
        /// </summary>
        public Socket PlcDevice()
        {
            try
            {
                if (clientSocket.Connected == false)
                {
                    IPAddress ip = IPAddress.Parse(IP);
                    var remoteEndPoint = new IPEndPoint(ip, IPPort);

                    // 将Socket设置为不实际连接，只是暂存连接信息
                    //clientSocket.Blocking = false;

                    // 将Socket置于非阻塞模式
                    //clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                    // 准备连接，但不实际连接
                    //clientSocket.BeginConnect(remoteEndPoint, ConnectCallback, null);
                    // 准备连接

                    //clientSocket.BeginConnect(remoteEndPoint, null, null);
                    //设置超时时间
                    clientSocket.ReceiveTimeout = 100;
                    //尝试连接到服务端
                    clientSocket.Connect(remoteEndPoint);
                }

                return clientSocket;
            }
            catch (Exception ex)
            {
                Log.Warn($"Plc [{Code},{Name},{IP},{IPPort}] ,异常：【{ex.Message}】。 ");
                //throw;
                //clientSocket.Close();
                clientSocket.Dispose();
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                Thread.Sleep(2000);
                return clientSocket;
            }

        }
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // 尝试完成连接，但不会实际建立连接
                clientSocket.EndConnect(ar);
                Log.Warn($"Plc [{Code},{Name}] 连接准备就绪，建立连接。 ");
            }
            catch (SocketException ex)
            {
                // 处理可能发生的异常，例如由于网络问题导致的无法完成连接
                Log.Warn($"Exception--  Plc [{Code},{Name}],连接准备过程中出现错误: {ex.Message}");
            }
        }
        //private ILog Log;
        private  ILog Log = LogManager.GetLogger(typeof(Sockets));
        public Sockets(ILog log)
        {
            this.Log = log;
        }

        private ConcurrentQueue<BaseSignal> writeSocketSignalQueue = new ConcurrentQueue<BaseSignal>();
        /// <summary>
        /// 写信号队列。
        /// </summary>
        public ConcurrentQueue<BaseSignal> WriteSocketSignalQueue
        {
            get
            {
                return writeSocketSignalQueue;
            }
        }

        private Dictionary<SocketBaseSignal, Action> witeSocketSignalQueueAction = new Dictionary<SocketBaseSignal, Action>();
        private Dictionary<BaseSignal, Action> witeSignalQueueAction = new Dictionary<BaseSignal, Action>();
        public Dictionary<SocketBaseSignal, Action> WriteSocketSignalQueueAction
        {
            get
            {
                return witeSocketSignalQueueAction;
            }
        }
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
                            //PlcDevice.ConnectTimeOut = 2000;
                            //OperateResult rs = PlcDevice.ConnectServer();

                            if (clientSocket.Connected == false)
                            {
                                clientSocket = PlcDevice();
                                if (clientSocket.Connected == true)
                                {
                                    IsConnect = true;
                                }
                            }
                            IPAddress ip = IPAddress.Parse(IP);
                            var remoteEndPoint = new IPEndPoint(ip, IPPort);
                            if (IsConnect == false)
                            {

                                clientSocket.Connect(remoteEndPoint);
                                // ***********************************************
                                // add by ... 2022-11/1  监控设备连接状态
                                Log.Warn($"Plc [{Code},{Name}]  IsConnect {clientSocket.Connected}");
                                //***********************************************
                                if (clientSocket.Connected == false)
                                {
                                    IsConnect = false;
                                    continue;
                                }
                            }
                            //IsConnect = CheckIsConnected();
                            //if (IsConnect == true)
                            //{
                                var sw = DateTime.Now;
                                UpdateDBData();
                            //HandleWriteSignal();
                            Log.Info($"Plc [{Code},{Name}] 开始发送，时间：{DateTime.Now}");
                            // 创建定时器实例，设置定时任务、初始延迟时间（0表示立即开始）、间隔时间（1000毫秒）和状态对象（这里为null）
                            TimerCallback timerCallback = new TimerCallback(HandleWriteSignalTimer);
                            Timer timer = new Timer(timerCallback, null, 0, 1000);
                            Log.Info($"Plc [{Code},{Name}] 结束发送，时间：{DateTime.Now}");
                            if (IsVerbose)
                                {
                                    Log.Info($"Plc [{Code},{Name}] spent {(DateTime.Now - sw).Milliseconds}ms to sync signals");
                                }
                            //}else
                            //{
                            //    Log.Debug($"Plc [{Code},{Name}] 已断开连接");
                            //}
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
                        catch (SocketException ex)
                        {
                            // ***********************************************
                            // add by ... 2022-11/1  监控异常异常情况
                            Log.Error($"Exception--  Plc [{Code},{Name}] {ex.Message}");
                            //***********************************************
                            Log.Error(ex);
                            IsConnect = false;
                            //clientSocket.Close();
                            continue;
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

        private bool CheckIsConnected()
        {
            bool isConnected = true;
            try
            {
                foreach (PlcDataBlock db in DBs)
                {
                    byte[] buffer = new byte[db.Length];
                    int receivedBytes = clientSocket.Receive(buffer);
                    if (receivedBytes > 0)
                    {

                    }
                    else
                    {
                        isConnected=false;
                        Log.Warn($"Plc UpdateDBData [{Code},{Name},{db.Address},{(ushort)db.Length}] error");
                        //判断是否断开连接，尝试重连
                        //IsConnect = false;
                        //clientSocket.Close();
                        clientSocket.Dispose();
                        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                        Thread.Sleep(2000);
                    }
                }
            }
            catch (Exception ex)
            {
                return isConnected;
            }
            finally
            {

            }
            return isConnected;
        }

        /// <summary>
        /// 更新内存DB块数据
        /// </summary>
        private void UpdateDBData()
        {
            try
            {
                if (clientSocket.Poll(800, SelectMode.SelectRead))
                {
                    foreach (PlcDataBlock db in DBs)
                    {

                        try
                        {
                            //todo:比特长度配置在数据库中
                            byte[] buffer = new byte[db.Length];


                            int receivedBytes = clientSocket.Receive(buffer);
                            //clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), clientSocket);
                            //string receivedMessage = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
                            if (receivedBytes > 0)
                            {
                                db.Data = buffer;
                                Log.Warn($"读取{Code}DB块【{db.Name}】信息为：{string.Join("/", db.Data)}");
                                if (!db.Name.Equals("S"))
                                {
                                    Console.WriteLine($"{DateTime.Now}--->读取{Code}DB块【{db.Name}】信息为：{string.Join("/", db.Data)}");
                                }
                            }
                            else
                            {
                                Log.Error($"Plc UpdateDBData [{Code},{Name},{db.Address},{(ushort)db.Length}] error");
                                //判断是否断开连接，尝试重连
                                IsConnect = false;
                                //clientSocket.Close();
                                clientSocket.Dispose();
                                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                                Thread.Sleep(2000);
                            }
                        }

                        catch (SocketException ex)
                        {
                            Log.Warn($"Plc UpdateDBData [{Code},{Name},{db.Address},{(ushort)db.Length}] errorInfo({ex.Message})");
                            continue;
                        }

                    }
                    for (int i = 0; i < AutoReadSignals.Values.Count; i++)
                    {
                        if (AutoReadSignals.Values.ElementAt(i).IsEnabled)
                        {
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
                else if (clientSocket.Poll(800, SelectMode.SelectError))
                {
                    //判断是否断开连接，尝试重连
                    IsConnect = false;
                    //clientSocket.Close();
                    clientSocket.Dispose();
                    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    Thread.Sleep(2000);
                }
            }
            catch (Exception)
            {

                Log.Error($"Plc UpdateDBData [{Code},{Name},error");
                //判断是否断开连接，尝试重连
                IsConnect = false;
                //clientSocket.Close();
                clientSocket.Dispose();
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                Thread.Sleep(2000);
            }
            
        }
        public byte[] ReceiveDataAsync(int dblength)
        {
            byte[] buffer = new byte[dblength];
            clientSocket.BeginReceive(buffer, 0, dblength, SocketFlags.None, new AsyncCallback(ReceiveCallback), buffer);
            return buffer;
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                byte[] buffer = (byte[])ar.AsyncState;
                int bytesRead = clientSocket.EndReceive(ar);
                string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received: {receivedData}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving data: {ex.Message}");
            }
        }
        private void UpdateSinals(List<object> signals)
        {
            {
                foreach (object signal in signals)
                {
                    if (signal is BaseSignal)
                    {
                        BaseSignal s = (BaseSignal)signal;
                        SocketDataUtil.ReadValue(this, s);
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
                WriteSocketSignalQueue.Enqueue(Heartbeat.Signal);
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
                if (WriteSocketSignalQueue.TryDequeue(out signal))
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
        /// 从队列里取10条数据，写给PLc。
        /// </summary>
        private void HandleWriteSignalTimer(object state)
        {
            Log.Info($"Plc [{Code},{Name}] 开始发送HandleWriteSignalTimer，时间：{DateTime.Now}");
            int count = 0;
            while (count < 10)
            {
                // 一次最多处理10个消息，以免影响信号同步
                BaseSignal signal;
                if (WriteSocketSignalQueue.TryDequeue(out signal))
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
                bool res = SocketDataUtil.WriteValue(clientSocket, signal);
                //Log.Debug($"write siganl [address:{signal.Address},value={signal.ToJsonString()}]");
                if (WriteSignalQueueAction.ContainsKey(signal))
                {
                    Action action = WriteSignalQueueAction[signal];
                    if (action != null)
                    {
                        action();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Warn(e.Message);
                signal.WriteTimeToLive++;
                if (signal.WriteTimeToLive <= 5)
                {
                    //22-11-01 添加进入异常清零
                    signal.WriteTimeToLive = 0;
                    // 失败次数小于5次继续
                    WriteSocketSignalQueue.Enqueue(signal);
                }
            }
        }


    }
}
