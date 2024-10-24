//=============================================================================
//                                 A220101
//=============================================================================
//
// Plc服务。为其他服务提供Plc数据交换的服务。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/1/9
//      创建
// V1.1 2022/1/30 
//      加载设备改为动态加载，在StandardDevice中添加Device_Class
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using WISH.WCS;
using Wish.HWConfig.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ASRS.WCS.PLC;

namespace Wish.Service
{
    public class PlcService
    {
        private static PlcService instance = null;
        private string code = "f562cc4c-4772-4b32-bdcd-f3e122c534e3";
        /// <summary>
        /// Plc列表。
        /// </summary>
        public List<Plc> PlcList { get; set; }

        private Dictionary<string,Plc> plcDict = new Dictionary<string,Plc>();
        /// <summary>
        /// Plc字典对象。
        /// </summary>
        public Dictionary<string, Plc> PlcDict { get { return plcDict; } }    

        public PlcService()
        {
            bool res=HslCommunication.Authorization.SetAuthorizationCode(code);
        }

        public static PlcService GetInstance()
        {
            if (instance == null)
            {
                instance = new PlcService();
                instance.init();
            }

            return instance;
        }

        private void init()
        {
            try
            {
                //obj.RegisterAutoReadObject("P127PLC", "SRM01", new S1SrmV540(obj.PlcDict["P127PLC"], "DB540", 0, "DB541", 0, "DB542", 0));
                
                // 加载PLC信息
                List<PlcConfig> plcs = DCService.GetInstance().GetDC().Set<PlcConfig>().Include(x => x.DBConfigs).Where(x=> x.Plc_Code != null).ToList();
                foreach (var plc in plcs)
                {
                    Plc temp = new Plc(ContextService.Log);
                    temp.Code = plc.Plc_Code;
                    temp.Name = plc.Plc_Name;
                    temp.IP = plc.IP_Address;
                    temp.ScanCycle = plc.Scan_Cycle;
                    temp.IsEnabled = plc.IsEnabled;
                    temp.IsIgnoreHearbeat = false;
                    // ToDO 完成 心跳的初始化
                    //temp.Heartbeat.Signal = new Signal<bool>(plc.HeartbeatAddress, plc.HeartbeatAddress);
                    temp.Heartbeat = new Heartbeat();
                    temp.Heartbeat.Signal = new Signal<bool>(plc.Heartbeat_DB, plc.Heartbeat_Address);
                    temp.Heartbeat.WriteInterval = plc.Heartbeat_WriteInterval;
                    temp.Heartbeat.IsEnabled = plc.Heartbeat_Enabled;

                    temp.IsVerbose = true;

                    List<PlcDataBlock> blocks = new List<PlcDataBlock>();

                    for (int i = 0; i < plc.DBConfigs.Count; i++)
                    {
                        PlcDataBlock block = new PlcDataBlock();
                        block.Id = plc.DBConfigs[i].Block_Code;
                        block.Name = plc.DBConfigs[i].Block_Name;
                        block.Length = plc.DBConfigs[i].Block_Length;
                        block.Offset = plc.DBConfigs[i].Block_Offset;

                        blocks.Add(block);
                    }
                    temp.DBs = blocks;
                    PlcDict.Add(plc.Plc_Code, temp);
                    if (temp.IsEnabled)
                        temp.Start();
                }

                /// 加载设备库
                Assembly assembly = Assembly.LoadFrom("WISH.WCS.Device.dll");


                // 加载设备信息
                List<DeviceConfig> devices = DCService.GetInstance().GetDC().Set<DeviceConfig>().Where(x=>x.IsValid).Include(x => x.PlcConfig).Include(x => x.StandardDevice).ToList();
                foreach (var item in devices)
                {
                    try
                    {
                        IPlcDevice obj = (IPlcDevice)assembly.CreateInstance(item.StandardDevice.Device_Class);

                        // 如果在动态库中找不到，则在本项目中找
                        if (obj == null)
                        {
                            Assembly assemblyx = Assembly.GetExecutingAssembly();
                            obj = (IPlcDevice)assemblyx.CreateInstance(item.StandardDevice.Device_Class);
                        }

                        obj.init(PlcDict[item.PlcConfig.Plc_Code], item.Device_Code, item.Config, item.IsEnabled, MsgService.GetInstance());
                        this.RegisterAutoReadObject(item.PlcConfig.Plc_Code, item.Device_Code, (IAutoRead)obj);
                    }
                    catch (Exception ex)
                    {
                        ContextService.Log.Error(ex);
                    }
                }
            }
            catch (Exception e)
            {
                ContextService.Log.Error(e);
            }
        }

        public void notifyNewClient()
        {
            List<DeviceConfig> devices = DCService.GetInstance().GetDC().Set<DeviceConfig>().Where(x=>x.IsValid).Include(x => x.PlcConfig).Include(x => x.StandardDevice).ToList();
            foreach (var item in devices)
            {
                var plcCode = item.PlcConfig.Plc_Code;
                var deviceCode = item.Device_Code;
                IPlcDevice dev = (IPlcDevice)PlcService.GetInstance().GetRegisterAutoReadObject(plcCode, deviceCode);
                if (dev != null) 
                { 
                    dev.notifyNewClient();
                }
            }

        }

        /// <summary>
        /// 其他服务调用写Plc信号。
        /// </summary>
        /// <param name="plcId"></param>
        /// <param name="signal"></param>
        /// <param name="actionEvent"></param>
        public void WriteSignal(string plcId, BaseSignal signal, Action actionEvent)
        {
            if (PlcDict[plcId] != null)
            {
                /// 增加地址和值都一致的检查，如果一致，不加入队列，防止stb, ack 重复写
                 lock (signal)
                //lock (PlcDict[plcId].WriteSignalQueue)//22-11-01注释
                {
                    /*22-11-01 注释
                   foreach (BaseSignal sig in PlcDict[plcId].WriteSignalQueue)
                   {
                       if (signal is Signal<bool> && sig is Signal<bool>)
                       {
                           Signal<bool> s1 = (Signal<bool>)signal;
                           Signal<bool> s2 = (Signal<bool>)sig;
                           if (s1.Address.Equals(s2.Address) && s1.Value.Equals(s2.Value))
                           {
                               return;
                           }
                       }
                   }*/
                    //22-11-01 添加
                    if (signal is Signal<bool>)
                    {
                        bool isOK = CheckSignalExists<bool>(plcId, signal);
                        if (isOK)
                        {
                            return;
                        }
                    }
                    else if (signal is Signal<string>)
                    {
                        bool isOK = CheckSignalExists<string>(plcId, signal);
                        if (isOK)
                        {
                            return;
                        }
                    }
                    else if (signal is Signal<Int16>)
                    {
                        bool isOK = CheckSignalExists<Int16>(plcId, signal);
                        if (isOK)
                        {
                            return;
                        }
                    }
                    else if (signal is Signal<Int32>)
                    {
                        bool isOK = CheckSignalExists<Int32>(plcId, signal);
                        if (isOK)
                        {
                            return;
                        }
                    }
                    //todo:2023-06-29 写char数组
                    else if (signal is Signal<char[]>)
                    {
                        bool isOK = CheckSignalExists<char[]>(plcId, signal);
                        if (isOK)
                        {
                            return;
                        }
                    }
                    PlcDict[plcId].WriteSignalQueue.Enqueue(signal);
                    if (actionEvent != null)
                    {
                        PlcDict[plcId].WriteSignalQueueAction.Add(signal, actionEvent);
                    }
                }
            }
        }


        //public object ReadSignal(string plcId, BaseSignal signal)
        //{
        //    object obj = PlcDataUtil.ReadValue(PlcDict[plcId], signal);
        //    return obj;
        //}

        /// <summary>
        /// 注册自动刷新数据对象。
        /// </summary>
        /// <param name="plcId"></param>
        /// <param name="autoReadSignalsId"></param>
        /// <param name="autoReadSignals"></param>
        public void RegisterAutoReadObject(string plcId, string autoReadSignalsId, IAutoRead autoReadSignals)
        {
            if(PlcDict.ContainsKey(plcId))
            { 
                PlcDict[plcId].RegisterAutoReadObject(autoReadSignalsId, autoReadSignals);
            }
        }

        /// <summary>
        /// 获取自动刷新数据对象。
        /// </summary>
        /// <param name="plcId"></param>
        /// <param name="autoReadSignalsId"></param>
        /// <returns></returns>
        public IAutoRead GetRegisterAutoReadObject(string plcId, string autoReadSignalsId)
        {
            IAutoRead obj = null;
            if (PlcDict.ContainsKey(plcId))
            {
                obj = PlcDict[plcId].GetRegisterAutoReadObject(autoReadSignalsId);
            }
            return obj;
        }
        //**************************
        //22-11-01 检查写的信号
        private bool CheckSignalExists<T>(string plcId, BaseSignal signal)
        {
            Signal<T> s1 = (Signal<T>)signal;
            var temp = from item in PlcDict[plcId].WriteSignalQueue where item is Signal<T> && s1.Address.Equals(item.Address) select item;
            foreach (BaseSignal sig in temp)
            {
                Signal<T> s2 = (Signal<T>)sig;
                if (s1.Value.Equals(s2.Value))
                {
                    return true;
                }
            }
            return false;
        }
    }



}
