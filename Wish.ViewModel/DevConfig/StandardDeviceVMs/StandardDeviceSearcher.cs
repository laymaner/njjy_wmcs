using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;
using ASRS.WCS.Common.Enum;


namespace Wish.ViewModel.DevConfig.StandardDeviceVMs
{
    public partial class StandardDeviceSearcher : BaseSearcher
    {
        [Display(Name = "DeviceInfo.DeviceNo")]
        public String Device_Code { get; set; }
        [Display(Name = "DeviceInfo.DeviceName")]
        public String Device_Name { get; set; }
        [Display(Name = "实现类名")]
        public String Device_Class { get; set; }
        [Display(Name = "VersionInfo.Type")]
        public EDeviceType? DeviceType { get; set; }
        [Display(Name = "VersionInfo.Company")]
        public String Company { get; set; }
        [Display(Name = "DeviceInfo.Config")]
        public String Config { get; set; }

        protected override void InitVM()
        {
        }

    }
}
