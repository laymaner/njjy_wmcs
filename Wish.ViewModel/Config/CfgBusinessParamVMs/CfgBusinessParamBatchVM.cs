using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgBusinessParamVMs
{
    public partial class CfgBusinessParamBatchVM : BaseBatchVM<CfgBusinessParam, CfgBusinessParam_BatchEdit>
    {
        public CfgBusinessParamBatchVM()
        {
            ListVM = new CfgBusinessParamListVM();
            LinkedVM = new CfgBusinessParam_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class CfgBusinessParam_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
