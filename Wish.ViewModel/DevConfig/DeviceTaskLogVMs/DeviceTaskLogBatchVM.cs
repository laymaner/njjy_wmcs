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
    public partial class DeviceTaskLogBatchVM : BaseBatchVM<DeviceTaskLog, DeviceTaskLog_BatchEdit>
    {
        public DeviceTaskLogBatchVM()
        {
            ListVM = new DeviceTaskLogListVM();
            LinkedVM = new DeviceTaskLog_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class DeviceTaskLog_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
