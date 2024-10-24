using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryRecordHisVMs
{
    public partial class WmsItnInventoryRecordHisBatchVM : BaseBatchVM<WmsItnInventoryRecordHis, WmsItnInventoryRecordHis_BatchEdit>
    {
        public WmsItnInventoryRecordHisBatchVM()
        {
            ListVM = new WmsItnInventoryRecordHisListVM();
            LinkedVM = new WmsItnInventoryRecordHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsItnInventoryRecordHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
