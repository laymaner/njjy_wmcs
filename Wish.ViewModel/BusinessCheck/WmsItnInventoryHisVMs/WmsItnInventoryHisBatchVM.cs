using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryHisVMs
{
    public partial class WmsItnInventoryHisBatchVM : BaseBatchVM<WmsItnInventoryHis, WmsItnInventoryHis_BatchEdit>
    {
        public WmsItnInventoryHisBatchVM()
        {
            ListVM = new WmsItnInventoryHisListVM();
            LinkedVM = new WmsItnInventoryHis_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsItnInventoryHis_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
