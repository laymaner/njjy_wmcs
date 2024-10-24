using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptVMs
{
    public partial class WmsInReceiptBatchVM : BaseBatchVM<WmsInReceipt, WmsInReceipt_BatchEdit>
    {
        public WmsInReceiptBatchVM()
        {
            ListVM = new WmsInReceiptListVM();
            LinkedVM = new WmsInReceipt_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsInReceipt_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
