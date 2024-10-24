using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInOrderVMs
{
    public partial class WmsInOrderBatchVM : BaseBatchVM<WmsInOrder, WmsInOrder_BatchEdit>
    {
        public WmsInOrderBatchVM()
        {
            ListVM = new WmsInOrderListVM();
            LinkedVM = new WmsInOrder_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsInOrder_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
