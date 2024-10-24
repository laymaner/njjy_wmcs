using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockHisVMs
{
    public partial class WmsStockHisSearcher : BaseSearcher
    {
        public String areaNo { get; set; }
        public String binNo { get; set; }
        public Int32? errFlag { get; set; }
        public String invoiceNo { get; set; }
        public String locNo { get; set; }
        public String palletBarcode { get; set; }
        public String proprietorCode { get; set; }
        public String regionNo { get; set; }
        public String roadwayNo { get; set; }
        public String stockCode { get; set; }
        public Int32? stockStatus { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
