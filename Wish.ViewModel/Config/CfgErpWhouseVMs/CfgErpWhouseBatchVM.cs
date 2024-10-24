using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgErpWhouseVMs
{
    public partial class CfgErpWhouseBatchVM : BaseBatchVM<CfgErpWhouse, CfgErpWhouse_BatchEdit>
    {
        public CfgErpWhouseBatchVM()
        {
            ListVM = new CfgErpWhouseListVM();
            LinkedVM = new CfgErpWhouse_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class CfgErpWhouse_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
