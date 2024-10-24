using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWLocGroupVMs
{
    public partial class BasWLocGroupSearcher : BaseSearcher
    {
        public String locGroupName { get; set; }
        public String locGroupNo { get; set; }
        public String locGroupType { get; set; }
        public Int32? usedFlag { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
