using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWAreaVMs
{
    public partial class BasWAreaBatchVM : BaseBatchVM<BasWArea, BasWArea_BatchEdit>
    {
        public BasWAreaBatchVM()
        {
            ListVM = new BasWAreaListVM();
            LinkedVM = new BasWArea_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasWArea_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
