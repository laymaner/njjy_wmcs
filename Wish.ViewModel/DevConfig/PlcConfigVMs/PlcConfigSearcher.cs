using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;


namespace Wish.ViewModel.DevConfig.PlcConfigVMs
{
    public partial class PlcConfigSearcher : BaseSearcher
    {
        [Display(Name = "DeviceInfo.DeviceNo")]
        public String Plc_Code { get; set; }
        [Display(Name = "DeviceInfo.DeviceName")]
        public String Plc_Name { get; set; }
        [Display(Name = "PLCInfo.IP")]
        public String IP_Address { get; set; }
        [Display(Name = "PLCInfo.Port")]
        public Int32? IP_Port { get; set; }
        [Display(Name = "PLCInfo.Connect")]
        public PlcConnType? ConnType { get; set; }
        [Display(Name = "连接状态")]
        public Boolean? IsConnect { get; set; }
        [Display(Name = "DeviceInfo.Effective")]
        public Boolean? IsEnabled { get; set; }
        [Display(Name = "PLCInfo.CheckHeartDB")]
        public String Heartbeat_DB { get; set; }

        protected override void InitVM()
        {
        }

    }
}
