using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgBusinessModuleVMs
{
    public partial class CfgBusinessModuleBatchVM : BaseBatchVM<CfgBusinessModule, CfgBusinessModule_BatchEdit>
    {
        public CfgBusinessModuleBatchVM()
        {
            ListVM = new CfgBusinessModuleListVM();
            LinkedVM = new CfgBusinessModule_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class CfgBusinessModule_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
