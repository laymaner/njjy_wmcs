using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessMove.WmsItnMoveRecordVMs
{
    public partial class WmsItnMoveRecordBatchVM : BaseBatchVM<WmsItnMoveRecord, WmsItnMoveRecord_BatchEdit>
    {
        public WmsItnMoveRecordBatchVM()
        {
            ListVM = new WmsItnMoveRecordListVM();
            LinkedVM = new WmsItnMoveRecord_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsItnMoveRecord_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
