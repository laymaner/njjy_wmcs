using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIqc.WmsItnQcRecordHisVMs
{
    public partial class WmsItnQcRecordHisBatchVM : BaseBatchVM<WmsItnQcRecordHis, WmsItnQcRecordHis_BatchEdit>
    {
        public WmsItnQcRecordHisBatchVM()
        {
            ListVM = new WmsItnQcRecordHisListVM();
            LinkedVM = new WmsItnQcRecordHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsItnQcRecordHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
