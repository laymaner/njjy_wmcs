using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceVMs
{
    public partial class WmsOutInvoiceBatchVM : BaseBatchVM<WmsOutInvoice, WmsOutInvoice_BatchEdit>
    {
        public WmsOutInvoiceBatchVM()
        {
            ListVM = new WmsOutInvoiceListVM();
            LinkedVM = new WmsOutInvoice_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsOutInvoice_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
