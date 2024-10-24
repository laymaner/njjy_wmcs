using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptIqcResultHisVMs
{
    public partial class WmsInReceiptIqcResultHisBatchVM : BaseBatchVM<WmsInReceiptIqcResultHis, WmsInReceiptIqcResultHis_BatchEdit>
    {
        public WmsInReceiptIqcResultHisBatchVM()
        {
            ListVM = new WmsInReceiptIqcResultHisListVM();
            LinkedVM = new WmsInReceiptIqcResultHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsInReceiptIqcResultHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
