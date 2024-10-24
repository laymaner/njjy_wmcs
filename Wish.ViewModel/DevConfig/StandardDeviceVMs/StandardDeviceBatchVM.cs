using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;


namespace Wish.ViewModel.DevConfig.StandardDeviceVMs
{
    public partial class StandardDeviceBatchVM : BaseBatchVM<StandardDevice, StandardDevice_BatchEdit>
    {
        public StandardDeviceBatchVM()
        {
            ListVM = new StandardDeviceListVM();
            LinkedVM = new StandardDevice_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class StandardDevice_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
