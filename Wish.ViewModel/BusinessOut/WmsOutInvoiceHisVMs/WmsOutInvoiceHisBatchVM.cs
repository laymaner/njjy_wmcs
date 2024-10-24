using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceHisVMs
{
    public partial class WmsOutInvoiceHisBatchVM : BaseBatchVM<WmsOutInvoiceHis, WmsOutInvoiceHis_BatchEdit>
    {
        public WmsOutInvoiceHisBatchVM()
        {
            ListVM = new WmsOutInvoiceHisListVM();
            LinkedVM = new WmsOutInvoiceHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsOutInvoiceHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
