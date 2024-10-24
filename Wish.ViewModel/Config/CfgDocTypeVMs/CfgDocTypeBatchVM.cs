using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgDocTypeVMs
{
    public partial class CfgDocTypeBatchVM : BaseBatchVM<CfgDocType, CfgDocType_BatchEdit>
    {
        public CfgDocTypeBatchVM()
        {
            ListVM = new CfgDocTypeListVM();
            LinkedVM = new CfgDocType_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class CfgDocType_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
