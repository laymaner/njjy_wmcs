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
    public partial class CfgRelationshipSearcher : BaseSearcher
    {
        public String leftCode { get; set; }
        public Int32? priority { get; set; }
        public String relationshipTypeCode { get; set; }
        public String rightCode { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
