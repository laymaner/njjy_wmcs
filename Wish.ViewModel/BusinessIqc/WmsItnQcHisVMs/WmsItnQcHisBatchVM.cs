using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIqc.WmsItnQcHisVMs
{
    public partial class WmsItnQcHisBatchVM : BaseBatchVM<WmsItnQcHis, WmsItnQcHis_BatchEdit>
    {
        public WmsItnQcHisBatchVM()
        {
            ListVM = new WmsItnQcHisListVM();
            LinkedVM = new WmsItnQcHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsItnQcHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
