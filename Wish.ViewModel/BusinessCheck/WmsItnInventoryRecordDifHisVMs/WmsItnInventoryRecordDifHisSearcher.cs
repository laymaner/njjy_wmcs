using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryRecordDifHisVMs
{
    public partial class WmsItnInventoryRecordDifHisSearcher : BaseSearcher
    {
        public String batchNo { get; set; }
        public String dataCode { get; set; }
        public Int32? delayFrozenFlag { get; set; }
        public Int32? delayTimes { get; set; }
        public Int32? driedScrapFlag { get; set; }
        public Int32? exposeFrozenFlag { get; set; }
        public Int32? inspectionResult { get; set; }
        public String inventoryNo { get; set; }
        public String materialName { get; set; }
        public String materialCode { get; set; }
        public String mslGradeCode { get; set; }
        public String palletBarcode { get; set; }
        public String positionNo { get; set; }
        public String projectNo { get; set; }
        public String proprietorCode { get; set; }
        public String skuCode { get; set; }
        public String stockCode { get; set; }
        public String supplierCode { get; set; }
        public String uniicode { get; set; }
        public String whouseNo { get; set; }
        public String areaNo { get; set; }
        public String delFlag { get; set; }
        public String erpWhouseNo { get; set; }
        public String unitCode { get; set; }
        [Display(Name = "备用项目号")]
        public String projectNoBak { get; set; }
        [Display(Name = "拆封状态")]
        public Int32? unpackStatus { get; set; }

        protected override void InitVM()
        {
        }

    }
}
