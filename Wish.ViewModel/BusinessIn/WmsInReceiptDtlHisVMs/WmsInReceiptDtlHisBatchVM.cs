using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptDtlHisVMs
{
    public partial class WmsInReceiptDtlHisBatchVM : BaseBatchVM<WmsInReceiptDtlHis, WmsInReceiptDtlHis_BatchEdit>
    {
        public WmsInReceiptDtlHisBatchVM()
        {
            ListVM = new WmsInReceiptDtlHisListVM();
            LinkedVM = new WmsInReceiptDtlHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsInReceiptDtlHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
