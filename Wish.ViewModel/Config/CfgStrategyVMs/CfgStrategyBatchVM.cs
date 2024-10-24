using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgStrategyVMs
{
    public partial class CfgStrategyBatchVM : BaseBatchVM<CfgStrategy, CfgStrategy_BatchEdit>
    {
        public CfgStrategyBatchVM()
        {
            ListVM = new CfgStrategyListVM();
            LinkedVM = new CfgStrategy_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class CfgStrategy_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
