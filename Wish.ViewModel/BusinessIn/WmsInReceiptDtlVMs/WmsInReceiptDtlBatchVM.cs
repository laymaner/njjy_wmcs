using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptDtlVMs
{
    public partial class WmsInReceiptDtlBatchVM : BaseBatchVM<WmsInReceiptDtl, WmsInReceiptDtl_BatchEdit>
    {
        public WmsInReceiptDtlBatchVM()
        {
            ListVM = new WmsInReceiptDtlListVM();
            LinkedVM = new WmsInReceiptDtl_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsInReceiptDtl_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
