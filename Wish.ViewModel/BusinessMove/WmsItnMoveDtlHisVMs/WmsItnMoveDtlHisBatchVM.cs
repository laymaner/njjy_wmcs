using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessMove.WmsItnMoveDtlHisVMs
{
    public partial class WmsItnMoveDtlHisBatchVM : BaseBatchVM<WmsItnMoveDtlHis, WmsItnMoveDtlHis_BatchEdit>
    {
        public WmsItnMoveDtlHisBatchVM()
        {
            ListVM = new WmsItnMoveDtlHisListVM();
            LinkedVM = new WmsItnMoveDtlHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsItnMoveDtlHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
