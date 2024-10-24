using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;


namespace Wish.ViewModel.DevConfig.PlcConfigVMs
{
    public partial class PlcConfigBatchVM : BaseBatchVM<PlcConfig, PlcConfig_BatchEdit>
    {
        public PlcConfigBatchVM()
        {
            ListVM = new PlcConfigListVM();
            LinkedVM = new PlcConfig_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class PlcConfig_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
