using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBMaterialVMs
{
    public partial class BasBMaterialSearcher : BaseSearcher
    {
        public Int32? BarcodeFlag { get; set; }
        public String Brand { get; set; }
        public String BuyerCode { get; set; }
        public String BuyerName { get; set; }
        public String ErpBinNo { get; set; }
        public String MaterialCategoryCode { get; set; }
        public String MaterialCode { get; set; }
        public String MaterialTypeCode { get; set; }
        public String UnitCode { get; set; }
        public String WhouseNo { get; set; }
        public String CompanyCode { get; set; }

        protected override void InitVM()
        {
        }

    }
}
