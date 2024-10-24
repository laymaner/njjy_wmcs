using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWPalletTypeVMs
{
    public partial class BasWPalletTypeBatchVM : BaseBatchVM<BasWPalletType, BasWPalletType_BatchEdit>
    {
        public BasWPalletTypeBatchVM()
        {
            ListVM = new BasWPalletTypeListVM();
            LinkedVM = new BasWPalletType_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasWPalletType_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
