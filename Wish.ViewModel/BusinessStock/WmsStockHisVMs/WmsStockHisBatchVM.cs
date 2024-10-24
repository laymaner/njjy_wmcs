using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockHisVMs
{
    public partial class WmsStockHisBatchVM : BaseBatchVM<WmsStockHis, WmsStockHis_BatchEdit>
    {
        public WmsStockHisBatchVM()
        {
            ListVM = new WmsStockHisListVM();
            LinkedVM = new WmsStockHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsStockHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
