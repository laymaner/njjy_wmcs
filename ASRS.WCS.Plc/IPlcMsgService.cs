//=============================================================================
//                                 A220101
//=============================================================================
//
// 设备消息服务接口。用于记录设备的报警、状态变更、交互报文等消息记录。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/2/8 
//      创建
//-----------------------------------------------------------------------------
using ASRS.WCS.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASRS.WCS.PLC
{
    /// <summary>
    /// 设备消息服务接口。用于记录设备的报警、状态变更、交互报文等消息记录。
    /// </summary>
    public interface IPlcMsgService
    {
        /// <summary>
        /// 设备状态变化
        /// </summary>
        /// <param name="deviceCode">设备编号</param>
        /// <param name="attribute">变化字段</param>
        /// <param name="changeMsg">变化内容</param>
        /// <param name="statudDB">状态数据块</param>
        public void NewDeviceStatusChange(string deviceCode, string attribute, string changeMsg, object statudDB);

        /// <summary>
        /// 设备有新的报警信息
        /// </summary>
        /// <param name="deviceCode">设备编号</param>
        /// <param name="msg">报警信息</param>
        /// <param name="statusDB">状态块</param>
        public void NewDeviceAlarm(string deviceCode, string msg, object statusDB);

        /// <summary>
        /// 设备状态有变化，通知展示界面
        /// </summary>
        /// <param name="msg"></param>
        public void NewPageClientMsg(string msg);

        /// <summary>
        /// 任务报文交互记录
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <param name="direct">1=Wcs2Plc;2=Plc2Wcs</param>
        /// <param name="taskNo"></param>
        /// <param name="commInfo"></param>
        public void NewTaskCommunication(string deviceNo, EPlcCommDirect direct, string taskNo, object commInfo);

        /// <summary>
        /// 设备执行过程中的消息，如堆垛机满入报警，通知前台显示屏展示
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <param name="type"></param>
        /// <param name="msg"></param>
        public void NewDeviceMsg(string deviceNo, ETaskExecMsgType type, string msg);
    }
}
