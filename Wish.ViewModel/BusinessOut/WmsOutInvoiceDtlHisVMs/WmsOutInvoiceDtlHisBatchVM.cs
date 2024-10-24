using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceDtlHisVMs
{
    public partial class WmsOutInvoiceDtlHisBatchVM : BaseBatchVM<WmsOutInvoiceDtlHis, WmsOutInvoiceDtlHis_BatchEdit>
    {
        public WmsOutInvoiceDtlHisBatchVM()
        {
            ListVM = new WmsOutInvoiceDtlHisListVM();
            LinkedVM = new WmsOutInvoiceDtlHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsOutInvoiceDtlHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
