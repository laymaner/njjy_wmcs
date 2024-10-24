using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessMove.WmsItnMoveVMs
{
    public partial class WmsItnMoveBatchVM : BaseBatchVM<WmsItnMove, WmsItnMove_BatchEdit>
    {
        public WmsItnMoveBatchVM()
        {
            ListVM = new WmsItnMoveListVM();
            LinkedVM = new WmsItnMove_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsItnMove_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
