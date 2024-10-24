﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessPutaway.WmsPutawayDtlVMs
{
    public partial class WmsPutawayDtlSearcher : BaseSearcher
    {
        public String areaNo { get; set; }
        public String binNo { get; set; }
        public String docTypeCode { get; set; }
        public String erpWhouseNo { get; set; }
        public Int32? inspectionResult { get; set; }
        [Display(Name = "物料编码")]
        public String materialCode { get; set; }
        [Display(Name = "载体条码")]
        public String palletBarcode { get; set; }
        [Display(Name = "项目号")]
        public String projectNo { get; set; }
        [Display(Name = "上架库位")]
        public String ptaBinNo { get; set; }
        [Display(Name = "状态")]
        public Int32? putawayDtlStatus { get; set; }
        public String putawayNo { get; set; }
        [Display(Name = "库区编号")]
        public String regionNo { get; set; }
        [Display(Name = "巷道")]
        public String roadwayNo { get; set; }
        public String stockCode { get; set; }
        public String supplierCode { get; set; }
        public String whouseNo { get; set; }
        public String unitCode { get; set; }

        protected override void InitVM()
        {
        }

    }
}
