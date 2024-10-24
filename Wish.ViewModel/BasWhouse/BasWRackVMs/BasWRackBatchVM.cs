using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWRackVMs
{
    public partial class BasWRackBatchVM : BaseBatchVM<BasWRack, BasWRack_BatchEdit>
    {
        public BasWRackBatchVM()
        {
            ListVM = new BasWRackListVM();
            LinkedVM = new BasWRack_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasWRack_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
