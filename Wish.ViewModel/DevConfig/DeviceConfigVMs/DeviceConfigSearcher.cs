using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;


namespace Wish.ViewModel.DevConfig.DeviceConfigVMs
{
    public partial class DeviceConfigSearcher : BaseSearcher
    {
        [Display(Name = "DeviceInfo.DeviceNo")]
        public String Device_Code { get; set; }
        [Display(Name = "DeviceInfo.DeviceName")]
        public String Device_Name { get; set; }
        [Display(Name = "DeviceInfo.HouseNo")]
        public Int64? WarehouseId { get; set; }
        [Display(Name = "DeviceInfo.PLCStep")]
        public Int32? Plc2WcsStep { get; set; }
        [Display(Name = "DeviceInfo.WCSStep")]
        public Int32? Wcs2PlcStep { get; set; }
        [Display(Name = "设备模式")]
        public Int32? Mode { get; set; }
        [Display(Name = "DeviceInfo.Config")]
        public String Config { get; set; }

        protected override void InitVM()
        {
        }

    }
}
