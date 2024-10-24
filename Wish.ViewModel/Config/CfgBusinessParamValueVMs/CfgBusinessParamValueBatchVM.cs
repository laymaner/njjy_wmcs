using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgBusinessParamValueVMs
{
    public partial class CfgBusinessParamValueBatchVM : BaseBatchVM<CfgBusinessParamValue, CfgBusinessParamValue_BatchEdit>
    {
        public CfgBusinessParamValueBatchVM()
        {
            ListVM = new CfgBusinessParamValueListVM();
            LinkedVM = new CfgBusinessParamValue_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class CfgBusinessParamValue_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
