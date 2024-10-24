using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWRegionVMs
{
    public partial class BasWRegionBatchVM : BaseBatchVM<BasWRegion, BasWRegion_BatchEdit>
    {
        public BasWRegionBatchVM()
        {
            ListVM = new BasWRegionListVM();
            LinkedVM = new BasWRegion_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasWRegion_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
