using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInOrderDtlVMs
{
    public partial class WmsInOrderDtlBatchVM : BaseBatchVM<WmsInOrderDtl, WmsInOrderDtl_BatchEdit>
    {
        public WmsInOrderDtlBatchVM()
        {
            ListVM = new WmsInOrderDtlListVM();
            LinkedVM = new WmsInOrderDtl_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsInOrderDtl_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
