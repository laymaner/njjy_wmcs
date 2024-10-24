using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBMaterialTypeVMs
{
    public partial class BasBMaterialTypeSearcher : BaseSearcher
    {
        public String materialTypeName { get; set; }
        public String materialTypeCode { get; set; }
        public String proprietorCode { get; set; }
        public Int32? usedFlag { get; set; }
        public Int32? virtualFlag { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
