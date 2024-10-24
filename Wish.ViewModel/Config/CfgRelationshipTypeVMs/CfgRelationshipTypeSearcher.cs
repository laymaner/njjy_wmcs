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
    public partial class CfgRelationshipTypeSearcher : BaseSearcher
    {
        public Int32? developFlag { get; set; }
        public String leftTable { get; set; }
        public String leftTableCode { get; set; }
        public String leftTableCodeLabel { get; set; }
        public String leftTableName { get; set; }
        public Int32? leftWhouse { get; set; }
        public String relationshipTypeCode { get; set; }
        public String rightTable { get; set; }
        public String rightTableCode { get; set; }
        public Int32? rightWhouse { get; set; }
        public Int32? usedFlag { get; set; }

        protected override void InitVM()
        {
        }

    }
}
