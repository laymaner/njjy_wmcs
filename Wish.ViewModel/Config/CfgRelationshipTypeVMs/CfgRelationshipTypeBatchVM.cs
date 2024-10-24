using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.Config.Model;


namespace Wish.ViewModel.Config.CfgRelationshipTypeVMs
{
    public partial class CfgRelationshipTypeBatchVM : BaseBatchVM<CfgRelationshipType, CfgRelationshipType_BatchEdit>
    {
        public CfgRelationshipTypeBatchVM()
        {
            ListVM = new CfgRelationshipTypeListVM();
            LinkedVM = new CfgRelationshipType_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class CfgRelationshipType_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
