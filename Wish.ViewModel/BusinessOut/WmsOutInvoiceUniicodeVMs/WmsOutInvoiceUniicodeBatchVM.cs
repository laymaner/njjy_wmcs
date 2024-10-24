using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceUniicodeVMs
{
    public partial class WmsOutInvoiceUniicodeBatchVM : BaseBatchVM<WmsOutInvoiceUniicode, WmsOutInvoiceUniicode_BatchEdit>
    {
        public WmsOutInvoiceUniicodeBatchVM()
        {
            ListVM = new WmsOutInvoiceUniicodeListVM();
            LinkedVM = new WmsOutInvoiceUniicode_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsOutInvoiceUniicode_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
