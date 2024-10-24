using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgRelationshipVMs
{
    public partial class CfgRelationshipBatchVM : BaseBatchVM<CfgRelationship, CfgRelationship_BatchEdit>
    {
        public CfgRelationshipBatchVM()
        {
            ListVM = new CfgRelationshipListVM();
            LinkedVM = new CfgRelationship_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class CfgRelationship_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
