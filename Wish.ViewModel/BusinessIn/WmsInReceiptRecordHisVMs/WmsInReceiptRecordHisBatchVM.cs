using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptRecordHisVMs
{
    public partial class WmsInReceiptRecordHisBatchVM : BaseBatchVM<WmsInReceiptRecordHis, WmsInReceiptRecordHis_BatchEdit>
    {
        public WmsInReceiptRecordHisBatchVM()
        {
            ListVM = new WmsInReceiptRecordHisListVM();
            LinkedVM = new WmsInReceiptRecordHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsInReceiptRecordHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
