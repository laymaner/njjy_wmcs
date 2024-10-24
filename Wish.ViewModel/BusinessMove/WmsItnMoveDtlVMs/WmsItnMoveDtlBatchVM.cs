using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessMove.WmsItnMoveDtlVMs
{
    public partial class WmsItnMoveDtlBatchVM : BaseBatchVM<WmsItnMoveDtl, WmsItnMoveDtl_BatchEdit>
    {
        public WmsItnMoveDtlBatchVM()
        {
            ListVM = new WmsItnMoveDtlListVM();
            LinkedVM = new WmsItnMoveDtl_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsItnMoveDtl_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
