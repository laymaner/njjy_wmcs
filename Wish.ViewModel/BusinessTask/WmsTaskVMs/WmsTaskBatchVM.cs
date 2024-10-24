﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessTask.WmsTaskVMs
{
    public partial class WmsTaskBatchVM : BaseBatchVM<WmsTask, WmsTask_BatchEdit>
    {
        public WmsTaskBatchVM()
        {
            ListVM = new WmsTaskListVM();
            LinkedVM = new WmsTask_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsTask_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
