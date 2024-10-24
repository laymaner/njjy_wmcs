using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptIqcResultVMs
{
    public partial class WmsInReceiptIqcResultBatchVM : BaseBatchVM<WmsInReceiptIqcResult, WmsInReceiptIqcResult_BatchEdit>
    {
        public WmsInReceiptIqcResultBatchVM()
        {
            ListVM = new WmsInReceiptIqcResultListVM();
            LinkedVM = new WmsInReceiptIqcResult_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsInReceiptIqcResult_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
