using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockReconcileResultHisVMs
{
    public partial class WmsStockReconcileResultHisBatchVM : BaseBatchVM<WmsStockReconcileResultHis, WmsStockReconcileResultHis_BatchEdit>
    {
        public WmsStockReconcileResultHisBatchVM()
        {
            ListVM = new WmsStockReconcileResultHisListVM();
            LinkedVM = new WmsStockReconcileResultHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsStockReconcileResultHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
