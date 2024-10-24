using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;


namespace Wish.ViewModel.DevConfig.DeviceTaskLogVMs
{
    public partial class DeviceTaskLogSearcher : BaseSearcher
    {
        [Display(Name = "DeviceInfo.DeviceNo")]
        public String Device_Code { get; set; }
        [Display(Name = "TaskLog.Direction")]
        public String Direct { get; set; }
        [Display(Name = "TaskLog.TaskNo")]
        public String Task_No { get; set; }
        [Display(Name = "TaskLog.Massage")]
        public String Message { get; set; }

        protected override void InitVM()
        {
        }

    }
}
