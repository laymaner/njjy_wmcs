using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgBusinessVMs
{
    public partial class CfgBusinessBatchVM : BaseBatchVM<CfgBusiness, CfgBusiness_BatchEdit>
    {
        public CfgBusinessBatchVM()
        {
            ListVM = new CfgBusinessListVM();
            LinkedVM = new CfgBusiness_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class CfgBusiness_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
