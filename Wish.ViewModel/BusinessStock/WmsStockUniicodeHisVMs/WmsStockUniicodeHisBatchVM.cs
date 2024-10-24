using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockUniicodeHisVMs
{
    public partial class WmsStockUniicodeHisBatchVM : BaseBatchVM<WmsStockUniicodeHis, WmsStockUniicodeHis_BatchEdit>
    {
        public WmsStockUniicodeHisBatchVM()
        {
            ListVM = new WmsStockUniicodeHisListVM();
            LinkedVM = new WmsStockUniicodeHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsStockUniicodeHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
