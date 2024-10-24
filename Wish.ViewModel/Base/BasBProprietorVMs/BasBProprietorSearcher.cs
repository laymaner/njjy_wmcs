using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBProprietorVMs
{
    public partial class BasBProprietorSearcher : BaseSearcher
    {
        public String address { get; set; }
        public String contacts { get; set; }
        public String description { get; set; }
        public String mail { get; set; }
        public String mobile { get; set; }
        public String phone { get; set; }
        public String proprietorCode { get; set; }
        public String proprietorName { get; set; }
        public Int32? usedFlag { get; set; }

        protected override void InitVM()
        {
        }

    }
}
