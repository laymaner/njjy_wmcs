using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessPutaway.WmsPutawayDtlVMs
{
    public partial class WmsPutawayDtlBatchVM : BaseBatchVM<WmsPutawayDtl, WmsPutawayDtl_BatchEdit>
    {
        public WmsPutawayDtlBatchVM()
        {
            ListVM = new WmsPutawayDtlListVM();
            LinkedVM = new WmsPutawayDtl_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsPutawayDtl_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
