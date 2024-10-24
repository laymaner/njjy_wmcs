﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBMaterialCategoryVMs
{
    public partial class BasBMaterialCategoryBatchVM : BaseBatchVM<BasBMaterialCategory, BasBMaterialCategory_BatchEdit>
    {
        public BasBMaterialCategoryBatchVM()
        {
            ListVM = new BasBMaterialCategoryListVM();
            LinkedVM = new BasBMaterialCategory_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasBMaterialCategory_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
