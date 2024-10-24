using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgStrategyTypeVMs
{
    public partial class CfgStrategyTypeBatchVM : BaseBatchVM<CfgStrategyType, CfgStrategyType_BatchEdit>
    {
        public CfgStrategyTypeBatchVM()
        {
            ListVM = new CfgStrategyTypeListVM();
            LinkedVM = new CfgStrategyType_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class CfgStrategyType_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
