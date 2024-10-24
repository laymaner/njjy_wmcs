using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptIqcRecordHisVMs
{
    public partial class WmsInReceiptIqcRecordHisBatchVM : BaseBatchVM<WmsInReceiptIqcRecordHis, WmsInReceiptIqcRecordHis_BatchEdit>
    {
        public WmsInReceiptIqcRecordHisBatchVM()
        {
            ListVM = new WmsInReceiptIqcRecordHisListVM();
            LinkedVM = new WmsInReceiptIqcRecordHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsInReceiptIqcRecordHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
