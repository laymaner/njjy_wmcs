using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessTask.WmsTaskHisVMs
{
    public partial class WmsTaskHisBatchVM : BaseBatchVM<WmsTaskHis, WmsTaskHis_BatchEdit>
    {
        public WmsTaskHisBatchVM()
        {
            ListVM = new WmsTaskHisListVM();
            LinkedVM = new WmsTaskHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsTaskHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
