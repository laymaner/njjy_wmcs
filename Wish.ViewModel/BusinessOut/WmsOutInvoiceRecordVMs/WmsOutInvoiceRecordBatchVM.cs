using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceRecordVMs
{
    public partial class WmsOutInvoiceRecordBatchVM : BaseBatchVM<WmsOutInvoiceRecord, WmsOutInvoiceRecord_BatchEdit>
    {
        public WmsOutInvoiceRecordBatchVM()
        {
            ListVM = new WmsOutInvoiceRecordListVM();
            LinkedVM = new WmsOutInvoiceRecord_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsOutInvoiceRecord_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
