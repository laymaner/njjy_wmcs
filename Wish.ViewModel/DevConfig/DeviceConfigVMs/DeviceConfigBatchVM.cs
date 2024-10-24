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
    public partial class DeviceConfigBatchVM : BaseBatchVM<DeviceConfig, DeviceConfig_BatchEdit>
    {
        public DeviceConfigBatchVM()
        {
            ListVM = new DeviceConfigListVM();
            LinkedVM = new DeviceConfig_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class DeviceConfig_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
