using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBMaterialTypeVMs
{
    public partial class BasBMaterialTypeBatchVM : BaseBatchVM<BasBMaterialType, BasBMaterialType_BatchEdit>
    {
        public BasBMaterialTypeBatchVM()
        {
            ListVM = new BasBMaterialTypeListVM();
            LinkedVM = new BasBMaterialType_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasBMaterialType_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
