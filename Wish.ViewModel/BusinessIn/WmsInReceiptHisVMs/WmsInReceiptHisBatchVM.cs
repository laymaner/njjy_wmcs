using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptHisVMs
{
    public partial class WmsInReceiptHisBatchVM : BaseBatchVM<WmsInReceiptHis, WmsInReceiptHis_BatchEdit>
    {
        public WmsInReceiptHisBatchVM()
        {
            ListVM = new WmsInReceiptHisListVM();
            LinkedVM = new WmsInReceiptHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsInReceiptHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
