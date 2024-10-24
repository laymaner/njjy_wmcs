using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWErpWhouseBinVMs
{
    public partial class BasWErpWhouseBinBatchVM : BaseBatchVM<BasWErpWhouseBin, BasWErpWhouseBin_BatchEdit>
    {
        public BasWErpWhouseBinBatchVM()
        {
            ListVM = new BasWErpWhouseBinListVM();
            LinkedVM = new BasWErpWhouseBin_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasWErpWhouseBin_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
