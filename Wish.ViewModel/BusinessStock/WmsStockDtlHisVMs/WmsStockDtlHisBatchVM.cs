using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockDtlHisVMs
{
    public partial class WmsStockDtlHisBatchVM : BaseBatchVM<WmsStockDtlHis, WmsStockDtlHis_BatchEdit>
    {
        public WmsStockDtlHisBatchVM()
        {
            ListVM = new WmsStockDtlHisListVM();
            LinkedVM = new WmsStockDtlHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsStockDtlHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
