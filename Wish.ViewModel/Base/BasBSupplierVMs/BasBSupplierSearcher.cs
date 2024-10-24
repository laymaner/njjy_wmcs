using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBSupplierVMs
{
    public partial class BasBSupplierSearcher : BaseSearcher
    {
        public String address { get; set; }
        public String contacts { get; set; }
        public String mail { get; set; }
        public String mobile { get; set; }
        public String phone { get; set; }
        public String proprietorCode { get; set; }
        public String supplierCode { get; set; }
        public Int32? usedFlag { get; set; }
        public String whouseNo { get; set; }
        public String companyCode { get; set; }

        protected override void InitVM()
        {
        }

    }
}
