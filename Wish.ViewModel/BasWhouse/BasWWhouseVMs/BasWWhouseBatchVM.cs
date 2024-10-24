using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWWhouseVMs
{
    public partial class BasWWhouseBatchVM : BaseBatchVM<BasWWhouse, BasWWhouse_BatchEdit>
    {
        public BasWWhouseBatchVM()
        {
            ListVM = new BasWWhouseListVM();
            LinkedVM = new BasWWhouse_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasWWhouse_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
