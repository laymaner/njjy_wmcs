using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessPutaway.WmsPutawayVMs
{
    public partial class WmsPutawayBatchVM : BaseBatchVM<WmsPutaway, WmsPutaway_BatchEdit>
    {
        public WmsPutawayBatchVM()
        {
            ListVM = new WmsPutawayListVM();
            LinkedVM = new WmsPutaway_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsPutaway_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
