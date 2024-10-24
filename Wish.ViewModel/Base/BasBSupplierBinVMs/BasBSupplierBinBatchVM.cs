using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBSupplierBinVMs
{
    public partial class BasBSupplierBinBatchVM : BaseBatchVM<BasBSupplierBin, BasBSupplierBin_BatchEdit>
    {
        public BasBSupplierBinBatchVM()
        {
            ListVM = new BasBSupplierBinListVM();
            LinkedVM = new BasBSupplierBin_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasBSupplierBin_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
