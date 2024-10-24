using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBSupplierVMs
{
    public partial class BasBSupplierBatchVM : BaseBatchVM<BasBSupplier, BasBSupplier_BatchEdit>
    {
        public BasBSupplierBatchVM()
        {
            ListVM = new BasBSupplierListVM();
            LinkedVM = new BasBSupplier_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasBSupplier_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
