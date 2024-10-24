using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIqc.WmsItnQcVMs
{
    public partial class WmsItnQcBatchVM : BaseBatchVM<WmsItnQc, WmsItnQc_BatchEdit>
    {
        public WmsItnQcBatchVM()
        {
            ListVM = new WmsItnQcListVM();
            LinkedVM = new WmsItnQc_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsItnQc_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
