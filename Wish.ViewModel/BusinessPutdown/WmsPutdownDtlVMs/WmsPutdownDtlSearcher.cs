using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessPutdown.WmsPutdownDtlVMs
{
    public partial class WmsPutdownDtlSearcher : BaseSearcher
    {
        public String areaNo { get; set; }
        public String erpWhouseNo { get; set; }
        [Display(Name = "质量标记")]
        public Int32? inspectionResult { get; set; }
        [Display(Name = "物料编码")]
        public String materialCode { get; set; }
        [Display(Name = "规格型号")]
        public String materialSpec { get; set; }
        [Display(Name = "载体条码")]
        public String palletBarcode { get; set; }
        public String projectNo { get; set; }
        public String proprietorCode { get; set; }
        public String putdownNo { get; set; }
        [Display(Name = "库存编码")]
        public String stockCode { get; set; }
        public String stockDtlId { get; set; }
        public String supplierCode { get; set; }
        public String whouseNo { get; set; }
        public Int32? putdownDtlStatus { get; set; }
        public String unitCode { get; set; }

        protected override void InitVM()
        {
        }

    }
}
