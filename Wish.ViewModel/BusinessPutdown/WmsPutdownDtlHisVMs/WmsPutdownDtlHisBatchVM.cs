using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessPutdown.WmsPutdownDtlHisVMs
{
    public partial class WmsPutdownDtlHisBatchVM : BaseBatchVM<WmsPutdownDtlHis, WmsPutdownDtlHis_BatchEdit>
    {
        public WmsPutdownDtlHisBatchVM()
        {
            ListVM = new WmsPutdownDtlHisListVM();
            LinkedVM = new WmsPutdownDtlHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsPutdownDtlHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
