using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryRecordVMs
{
    public partial class WmsItnInventoryRecordBatchVM : BaseBatchVM<WmsItnInventoryRecord, WmsItnInventoryRecord_BatchEdit>
    {
        public WmsItnInventoryRecordBatchVM()
        {
            ListVM = new WmsItnInventoryRecordListVM();
            LinkedVM = new WmsItnInventoryRecord_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsItnInventoryRecord_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
