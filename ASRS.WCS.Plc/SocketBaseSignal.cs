using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASRS.WCS.PLC
{
    //public abstract class SocketBaseSignal : BaseMonitor
    public abstract class SocketBaseSignal 
    {
        /// <summary>
        /// 写Socket信号
        /// </summary>
        public byte[] signSocket { get; set; }



        /// <summary>
        /// 当写入失败时TTL++，超过一定次数后丢弃。
        /// </summary>
        public int WriteTimeToLive { get; set; }

        

    }
}
