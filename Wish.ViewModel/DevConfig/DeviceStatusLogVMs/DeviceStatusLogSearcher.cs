using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;


namespace Wish.ViewModel.DevConfig.DeviceStatusLogVMs
{
    public partial class DeviceStatusLogSearcher : BaseSearcher
    {
        [Display(Name = "DeviceInfo.DeviceNo")]
        public String Device_Code { get; set; }
        [Display(Name = "StatusLog.ChangeStatus")]
        public String Attribute { get; set; }
        [Display(Name = "StatusLog.StatusInfo")]
        public String Content { get; set; }
        [Display(Name = "StatusLog.FeedBack")]
        public String Message { get; set; }

        protected override void InitVM()
        {
        }

    }
}
