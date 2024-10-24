using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockAdjustVMs
{
    public partial class WmsStockAdjustBatchVM : BaseBatchVM<WmsStockAdjust, WmsStockAdjust_BatchEdit>
    {
        public WmsStockAdjustBatchVM()
        {
            ListVM = new WmsStockAdjustListVM();
            LinkedVM = new WmsStockAdjust_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsStockAdjust_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
