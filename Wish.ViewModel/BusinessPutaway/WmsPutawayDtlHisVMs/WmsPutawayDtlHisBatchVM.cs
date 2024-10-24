using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessPutaway.WmsPutawayDtlHisVMs
{
    public partial class WmsPutawayDtlHisBatchVM : BaseBatchVM<WmsPutawayDtlHis, WmsPutawayDtlHis_BatchEdit>
    {
        public WmsPutawayDtlHisBatchVM()
        {
            ListVM = new WmsPutawayDtlHisListVM();
            LinkedVM = new WmsPutawayDtlHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsPutawayDtlHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
