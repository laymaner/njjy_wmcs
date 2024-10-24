using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWRegionTypeVMs
{
    public partial class BasWRegionTypeSearcher : BaseSearcher
    {
        public String regionTypeCode { get; set; }
        public String regionTypeFlag { get; set; }
        public String regionTypeName { get; set; }

        protected override void InitVM()
        {
        }

    }
}
