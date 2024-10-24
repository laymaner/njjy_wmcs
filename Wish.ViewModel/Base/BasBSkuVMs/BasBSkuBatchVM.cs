using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBSkuVMs
{
    public partial class BasBSkuBatchVM : BaseBatchVM<BasBSku, BasBSku_BatchEdit>
    {
        public BasBSkuBatchVM()
        {
            ListVM = new BasBSkuListVM();
            LinkedVM = new BasBSku_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasBSku_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
