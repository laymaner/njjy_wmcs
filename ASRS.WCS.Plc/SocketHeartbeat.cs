//=============================================================================
//                                 A220101
//=============================================================================
//
// Plc心跳检测。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/1/9 
//      创建
//
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASRS.WCS.PLC
{
    public class SocketHeartbeat
    {
        /// <summary>
        /// 心跳信号信息。
        /// </summary>
        public SocketSignal<bool> Signal { get; set; }  

        /// <summary>
        ///  真表示心跳检测有效。
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 心跳写周期。
        /// </summary>
        public int WriteInterval { get; set; }  

        /// <summary>
        /// 监控周期，在监控周期内容如果没有心跳，则表示连接断开。
        /// </summary>
        public int MonitorCycle { get; set; }

    }
}
