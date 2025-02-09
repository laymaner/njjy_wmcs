﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgDocLocVMs
{
    public partial class CfgDocLocBatchVM : BaseBatchVM<CfgDocLoc, CfgDocLoc_BatchEdit>
    {
        public CfgDocLocBatchVM()
        {
            ListVM = new CfgDocLocListVM();
            LinkedVM = new CfgDocLoc_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class CfgDocLoc_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
