using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWLocVMs
{
    public partial class BasWLocBatchVM : BaseBatchVM<BasWLoc, BasWLoc_BatchEdit>
    {
        public BasWLocBatchVM()
        {
            ListVM = new BasWLocListVM();
            LinkedVM = new BasWLoc_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasWLoc_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
