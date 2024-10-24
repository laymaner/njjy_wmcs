using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryDtlVMs
{
    public partial class WmsItnInventoryDtlBatchVM : BaseBatchVM<WmsItnInventoryDtl, WmsItnInventoryDtl_BatchEdit>
    {
        public WmsItnInventoryDtlBatchVM()
        {
            ListVM = new WmsItnInventoryDtlListVM();
            LinkedVM = new WmsItnInventoryDtl_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsItnInventoryDtl_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
