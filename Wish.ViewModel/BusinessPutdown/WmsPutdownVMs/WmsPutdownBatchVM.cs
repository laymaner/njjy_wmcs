using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessPutdown.WmsPutdownVMs
{
    public partial class WmsPutdownBatchVM : BaseBatchVM<WmsPutdown, WmsPutdown_BatchEdit>
    {
        public WmsPutdownBatchVM()
        {
            ListVM = new WmsPutdownListVM();
            LinkedVM = new WmsPutdown_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class WmsPutdown_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
