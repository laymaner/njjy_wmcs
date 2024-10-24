using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIqc.WmsItnQcRecordVMs
{
    public partial class WmsItnQcRecordBatchVM : BaseBatchVM<WmsItnQcRecord, WmsItnQcRecord_BatchEdit>
    {
        public WmsItnQcRecordBatchVM()
        {
            ListVM = new WmsItnQcRecordListVM();
            LinkedVM = new WmsItnQcRecord_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsItnQcRecord_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
