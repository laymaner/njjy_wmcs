using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgDocTypeDtlVMs
{
    public partial class CfgDocTypeDtlBatchVM : BaseBatchVM<CfgDocTypeDtl, CfgDocTypeDtl_BatchEdit>
    {
        public CfgDocTypeDtlBatchVM()
        {
            ListVM = new CfgDocTypeDtlListVM();
            LinkedVM = new CfgDocTypeDtl_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class CfgDocTypeDtl_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
