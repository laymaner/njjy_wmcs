using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASRS.WCS.PLC
{
    public interface IPlcDevice
    {
        public void init(Plc plc, string deviceCode, string jsonConfigString, bool isEnable, IPlcMsgService msgService);


        /// <summary>
        /// 通知新客户端
        /// </summary>
        public void notifyNewClient();



        /// <summary>
        /// 确认报警
        /// </summary>
        public void CwAck();

        /// <summary>
        /// 删除任务
        /// </summary>
        public void CwClearTask();


        /// <summary>
        /// 真表示设备自动
        /// </summary>

        [Display(Name = "自动模式")]
        public bool SwIsAuto { get; }

        /// <summary>
        /// 任务编号
        /// </summary>
        [Display(Name = "任务号")]
        public string SwTaskNo { get;  }

        /// <summary>
        /// Plc反馈的Ack信息
        /// </summary>
        /// </summary>
        [Display(Name = "Plc.Ack")]
        public bool Plc2WcsAck { get;  }

        /// <summary>
        /// Plc写给Wcs的STB信号
        /// </summary>
        [Display(Name = "Plc.Stb")]
        public bool Plc2WcsStb { get;  }

        /// <summary>
        /// Wcs反馈的Ack信息
        /// </summary>
        [Display(Name = "Wcs.Ack")]
        public bool Wcs2PlcAck { get; set; }

        /// <summary>
        /// wcs写给plc的STB信号
        /// </summary>
        [Display(Name = "Wcs.Stb")]
        public bool Wcs2PlcStb { get; set; }

        /// <summary>
        /// 真表示报警
        /// </summary>
        [Display(Name = "故障")]
        public bool SwIsAlarm { get; }

        /// <summary>
        /// 真表示设备连接正常
        /// </summary>
        [Display(Name = "在线")]
        public bool SwIsConnect { get; }

        /// <summary>
        /// 报警代码 List
        /// </summary>
        [Display(Name = "故障信息")]
        public List<string> SwAlarmCode { get; }

        /// <summary>
        /// 报警信息总览 如 代码1 : 描述1 , 代码2 : 描述2...
        /// </summary>
        [Display(Name = "报警总览")]
        public string SwAlarmMessage { get; }
    }
}
