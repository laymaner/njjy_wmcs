using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIqc.WmsItnQcDtlVMs
{
    public partial class WmsItnQcDtlBatchVM : BaseBatchVM<WmsItnQcDtl, WmsItnQcDtl_BatchEdit>
    {
        public WmsItnQcDtlBatchVM()
        {
            ListVM = new WmsItnQcDtlListVM();
            LinkedVM = new WmsItnQcDtl_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsItnQcDtl_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
