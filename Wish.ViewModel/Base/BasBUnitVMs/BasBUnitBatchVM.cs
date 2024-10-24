using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBUnitVMs
{
    public partial class BasBUnitBatchVM : BaseBatchVM<BasBUnit, BasBUnit_BatchEdit>
    {
        public BasBUnitBatchVM()
        {
            ListVM = new BasBUnitListVM();
            LinkedVM = new BasBUnit_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasBUnit_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
