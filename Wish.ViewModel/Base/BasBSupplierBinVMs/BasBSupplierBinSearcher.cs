using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBSupplierBinVMs
{
    public partial class BasBSupplierBinSearcher : BaseSearcher
    {
        public String areaNo { get; set; }
        public String binNo { get; set; }
        public String regionNo { get; set; }
        public String supplierCode { get; set; }
        public String whouseNo { get; set; }
        public String erpWhouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
