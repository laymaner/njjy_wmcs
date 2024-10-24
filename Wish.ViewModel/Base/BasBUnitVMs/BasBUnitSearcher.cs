using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBUnitVMs
{
    public partial class BasBUnitSearcher : BaseSearcher
    {
        public String unitCode { get; set; }
        public String unitName { get; set; }
        public String unitType { get; set; }
        public Int32? usedFlag { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
