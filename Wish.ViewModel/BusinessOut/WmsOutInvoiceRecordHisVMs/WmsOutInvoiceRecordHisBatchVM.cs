using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceRecordHisVMs
{
    public partial class WmsOutInvoiceRecordHisBatchVM : BaseBatchVM<WmsOutInvoiceRecordHis, WmsOutInvoiceRecordHis_BatchEdit>
    {
        public WmsOutInvoiceRecordHisBatchVM()
        {
            ListVM = new WmsOutInvoiceRecordHisListVM();
            LinkedVM = new WmsOutInvoiceRecordHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsOutInvoiceRecordHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
