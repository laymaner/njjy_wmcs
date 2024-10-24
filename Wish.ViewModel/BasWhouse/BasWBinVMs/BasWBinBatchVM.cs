using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWBinVMs
{
    public partial class BasWBinBatchVM : BaseBatchVM<BasWBin, BasWBin_BatchEdit>
    {
        public BasWBinBatchVM()
        {
            ListVM = new BasWBinListVM();
            LinkedVM = new BasWBin_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasWBin_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
