using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceUniicodeHisVMs
{
    public partial class WmsOutInvoiceUniicodeHisBatchVM : BaseBatchVM<WmsOutInvoiceUniicodeHis, WmsOutInvoiceUniicodeHis_BatchEdit>
    {
        public WmsOutInvoiceUniicodeHisBatchVM()
        {
            ListVM = new WmsOutInvoiceUniicodeHisListVM();
            LinkedVM = new WmsOutInvoiceUniicodeHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsOutInvoiceUniicodeHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
