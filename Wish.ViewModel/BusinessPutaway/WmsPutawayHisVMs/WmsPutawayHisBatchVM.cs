using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessPutaway.WmsPutawayHisVMs
{
    public partial class WmsPutawayHisBatchVM : BaseBatchVM<WmsPutawayHis, WmsPutawayHis_BatchEdit>
    {
        public WmsPutawayHisBatchVM()
        {
            ListVM = new WmsPutawayHisListVM();
            LinkedVM = new WmsPutawayHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsPutawayHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
