using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWPalletVMs
{
    public partial class BasWPalletBatchVM : BaseBatchVM<BasWPallet, BasWPallet_BatchEdit>
    {
        public BasWPalletBatchVM()
        {
            ListVM = new BasWPalletListVM();
            LinkedVM = new BasWPallet_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasWPallet_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
