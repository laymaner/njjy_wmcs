using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryRecordDifHisVMs
{
    public partial class WmsItnInventoryRecordDifHisBatchVM : BaseBatchVM<WmsItnInventoryRecordDifHis, WmsItnInventoryRecordDifHis_BatchEdit>
    {
        public WmsItnInventoryRecordDifHisBatchVM()
        {
            ListVM = new WmsItnInventoryRecordDifHisListVM();
            LinkedVM = new WmsItnInventoryRecordDifHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsItnInventoryRecordDifHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
