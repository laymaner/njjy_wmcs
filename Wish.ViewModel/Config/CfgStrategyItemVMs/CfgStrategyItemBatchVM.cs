using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgStrategyItemVMs
{
    public partial class CfgStrategyItemBatchVM : BaseBatchVM<CfgStrategyItem, CfgStrategyItem_BatchEdit>
    {
        public CfgStrategyItemBatchVM()
        {
            ListVM = new CfgStrategyItemListVM();
            LinkedVM = new CfgStrategyItem_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class CfgStrategyItem_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
