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
    public partial class DeviceAlarmLogBatchVM : BaseBatchVM<DeviceAlarmLog, DeviceAlarmLog_BatchEdit>
    {
        public DeviceAlarmLogBatchVM()
        {
            ListVM = new DeviceAlarmLogListVM();
            LinkedVM = new DeviceAlarmLog_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class DeviceAlarmLog_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
