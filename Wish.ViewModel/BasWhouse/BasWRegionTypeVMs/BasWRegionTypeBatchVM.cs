using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWRegionTypeVMs
{
    public partial class BasWRegionTypeBatchVM : BaseBatchVM<BasWRegionType, BasWRegionType_BatchEdit>
    {
        public BasWRegionTypeBatchVM()
        {
            ListVM = new BasWRegionTypeListVM();
            LinkedVM = new BasWRegionType_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasWRegionType_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
