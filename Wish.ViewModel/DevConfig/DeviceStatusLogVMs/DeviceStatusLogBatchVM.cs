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
    public partial class DeviceStatusLogBatchVM : BaseBatchVM<DeviceStatusLog, DeviceStatusLog_BatchEdit>
    {
        public DeviceStatusLogBatchVM()
        {
            ListVM = new DeviceStatusLogListVM();
            LinkedVM = new DeviceStatusLog_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class DeviceStatusLog_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
