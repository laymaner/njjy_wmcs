using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBCustomerVMs
{
    public partial class BasBCustomerBatchVM : BaseBatchVM<BasBCustomer, BasBCustomer_BatchEdit>
    {
        public BasBCustomerBatchVM()
        {
            ListVM = new BasBCustomerListVM();
            LinkedVM = new BasBCustomer_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasBCustomer_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
