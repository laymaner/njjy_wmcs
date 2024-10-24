using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockUniicodeVMs
{
    public partial class WmsStockUniicodeBatchVM : BaseBatchVM<WmsStockUniicode, WmsStockUniicode_BatchEdit>
    {
        public WmsStockUniicodeBatchVM()
        {
            ListVM = new WmsStockUniicodeListVM();
            LinkedVM = new WmsStockUniicode_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsStockUniicode_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
