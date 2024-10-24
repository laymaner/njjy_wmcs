using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgStrategyDtlVMs
{
    public partial class CfgStrategyDtlBatchVM : BaseBatchVM<CfgStrategyDtl, CfgStrategyDtl_BatchEdit>
    {
        public CfgStrategyDtlBatchVM()
        {
            ListVM = new CfgStrategyDtlListVM();
            LinkedVM = new CfgStrategyDtl_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class CfgStrategyDtl_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
