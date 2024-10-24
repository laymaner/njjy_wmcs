using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockReconcileResultVMs
{
    public partial class WmsStockReconcileResultBatchVM : BaseBatchVM<WmsStockReconcileResult, WmsStockReconcileResult_BatchEdit>
    {
        public WmsStockReconcileResultBatchVM()
        {
            ListVM = new WmsStockReconcileResultListVM();
            LinkedVM = new WmsStockReconcileResult_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsStockReconcileResult_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
