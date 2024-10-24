using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;


namespace Wish.ViewModel.DevConfig.DeviceAlarmLogVMs
{
    public partial class DeviceAlarmLogSearcher : BaseSearcher
    {
        [Display(Name = "DeviceInfo.DeviceNo")]
        public String Device_Code { get; set; }
        [Display(Name = "AlarmLog.AlarmInfo")]
        public String Message { get; set; }
        [Display(Name = "AlarmLog.StartTime")]
        public DateRange OriginTime { get; set; }

        protected override void InitVM()
        {
        }

    }
}
