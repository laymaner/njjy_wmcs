using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptUniicodeVMs
{
    public partial class WmsInReceiptUniicodeBatchVM : BaseBatchVM<WmsInReceiptUniicode, WmsInReceiptUniicode_BatchEdit>
    {
        public WmsInReceiptUniicodeBatchVM()
        {
            ListVM = new WmsInReceiptUniicodeListVM();
            LinkedVM = new WmsInReceiptUniicode_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsInReceiptUniicode_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
