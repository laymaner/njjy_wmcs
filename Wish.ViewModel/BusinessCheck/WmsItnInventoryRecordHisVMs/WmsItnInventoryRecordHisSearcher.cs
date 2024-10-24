using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryRecordHisVMs
{
    public partial class WmsItnInventoryRecordHisSearcher : BaseSearcher
    {
        public Int32? blindFlag { get; set; }
        public Int32? differenceFlag { get; set; }
        public String docTypeCode { get; set; }
        public String inOutTypeNo { get; set; }
        public String erpWhouseNo { get; set; }
        public Int32? inspectionResult { get; set; }
        public String inventoryBy { get; set; }
        public String inventoryNo { get; set; }
        public String inventoryReason { get; set; }
        public Int32? inventoryRecordStatus { get; set; }
        public String materialName { get; set; }
        public String materialCode { get; set; }
        public String palletBarcode { get; set; }
        public String projectNo { get; set; }
        public String proprietorCode { get; set; }
        public String putdownLocNo { get; set; }
        public String skuCode { get; set; }
        public String stockCode { get; set; }
        public String supplierCode { get; set; }
        public String whouseNo { get; set; }

        protected override void InitVM()
        {
        }

    }
}
