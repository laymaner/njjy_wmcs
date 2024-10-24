using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInOrderDtlHisVMs
{
    public partial class WmsInOrderDtlHisBatchVM : BaseBatchVM<WmsInOrderDtlHis, WmsInOrderDtlHis_BatchEdit>
    {
        public WmsInOrderDtlHisBatchVM()
        {
            ListVM = new WmsInOrderDtlHisListVM();
            LinkedVM = new WmsInOrderDtlHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsInOrderDtlHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
