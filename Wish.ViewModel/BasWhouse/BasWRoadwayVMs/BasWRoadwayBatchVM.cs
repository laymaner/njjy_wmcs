using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWRoadwayVMs
{
    public partial class BasWRoadwayBatchVM : BaseBatchVM<BasWRoadway, BasWRoadway_BatchEdit>
    {
        public BasWRoadwayBatchVM()
        {
            ListVM = new BasWRoadwayListVM();
            LinkedVM = new BasWRoadway_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasWRoadway_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
