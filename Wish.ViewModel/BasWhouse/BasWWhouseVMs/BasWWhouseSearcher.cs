using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWWhouseVMs
{
    public partial class BasWWhouseSearcher : BaseSearcher
    {
        public String contacts { get; set; }
        public String telephone { get; set; }
        public Int32? usedFlag { get; set; }
        public String whouseAddress { get; set; }
        public String whouseName { get; set; }
        public String whouseNo { get; set; }
        public String whouseType { get; set; }

        protected override void InitVM()
        {
        }

    }
}
