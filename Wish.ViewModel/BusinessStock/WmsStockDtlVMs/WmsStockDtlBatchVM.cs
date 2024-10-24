using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockDtlVMs
{
    public partial class WmsStockDtlBatchVM : BaseBatchVM<WmsStockDtl, WmsStockDtl_BatchEdit>
    {
        public WmsStockDtlBatchVM()
        {
            ListVM = new WmsStockDtlListVM();
            LinkedVM = new WmsStockDtl_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsStockDtl_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
