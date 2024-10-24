using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWLocGroupVMs
{
    public partial class BasWLocGroupBatchVM : BaseBatchVM<BasWLocGroup, BasWLocGroup_BatchEdit>
    {
        public BasWLocGroupBatchVM()
        {
            ListVM = new BasWLocGroupListVM();
            LinkedVM = new BasWLocGroup_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasWLocGroup_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
