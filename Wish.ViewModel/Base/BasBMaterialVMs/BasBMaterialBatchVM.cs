using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBMaterialVMs
{
    public partial class BasBMaterialBatchVM : BaseBatchVM<BasBMaterial, BasBMaterial_BatchEdit>
    {
        public BasBMaterialBatchVM()
        {
            ListVM = new BasBMaterialListVM();
            LinkedVM = new BasBMaterial_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasBMaterial_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
