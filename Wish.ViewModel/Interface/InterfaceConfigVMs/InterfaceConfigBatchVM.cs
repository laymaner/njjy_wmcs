using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.Interface;


namespace Wish.ViewModel.Interface.InterfaceConfigVMs
{
    public partial class InterfaceConfigBatchVM : BaseBatchVM<InterfaceConfig, InterfaceConfig_BatchEdit>
    {
        public InterfaceConfigBatchVM()
        {
            ListVM = new InterfaceConfigListVM();
            LinkedVM = new InterfaceConfig_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InterfaceConfig_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
