using Com.Wish.Model.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common.Dtos
{
    public class StockLockDto
    {
        public List<string> ids { get; set; }

        public int lockFlag { get; set; }

        public string lockReason { get; set; }
    }

    public class WmsStockDtlDto : WmsStockDtl
    {
        public decimal? supplierExposeTimes { get; set; }
        [Display(Name = "单位")]
        public string unitCode { get; set; }

        public string unitName { get; set; }

        /// <summary>
        /// 唯一码
        /// </summary>
        public string uniicode { get; set; }
        public string stockDtlStatusDesc { get; set; }
        public string mslGradeCode { get; set; }

        public string dataCode { get; set; }

        public string materialTypeCode { get; set; }


        public string materialTypeName { get; set; }
        [Display(Name = "物料大类")]
        public string materialCategoryCode { get; set; }

        [Display(Name = "工程图号")]
        public string projectDrawingNo { get; set; }

        public string materialCategoryName { get; set; }

        public DateTime? expDate { get; set; }

        public DateTime? productDate { get; set; }

        public decimal? stockQty { get; set; }

        public string inspectionResultDesc { get; set; }

        public string lockFlagDesc { get; set; }

        public string whouseName { get; set; }

        public string areaName { get; set; }


        public string proprietorName { get; set; }
        public string erpWhouseName { get; set; }

        public int stockDeadType { get; set; }

        public string stockDeadTypeDesc { get; set; }

        public DateTime? inwhTime { get; set; }

        public string defaultValidityDate { get; set; }

        public decimal? warnOverdueLen { get; set; }

        // public decimal? sluggishTime { get; set; }

        #region 主表
        public string regionNo { get; set; }
        [Display(Name = "库区")]
        public string regionName { get; set; }

        public string roadwayNo { get; set; }
        [Display(Name = "巷道")]
        public string roadwayName { get; set; }
        [Display(Name = "库位")]
        public string binNo { get; set; }

        public string binName { get; set; }
        [Display(Name = "装载类型")]
        public int? loadedType { get; set; }

        public string loadedTypeDesc { get; set; }

        public string locAllotGroup { get; set; }

        public int? errFlag { get; set; }

        public string errFlagDesc { get; set; }

        public string invoiceNo { get; set; }

        public int? specialFlag { get; set; }

        public string specialFlagDesc { get; set; }

        public string stockDelFlag { get; set; }
        [Display(Name = "当前站台")]
        public string locNo { get; set; }

        public decimal? weight { get; set; }

        public string errMsg { get; set; }

        public string specialMsg { get; set; }

        public int? stockStatus { get; set; }

        public string stockStatusDesc { get; set; }

        #endregion

        [Display(Name = "电子料标识")]
        public string materialFlagDesc { get; set; }

        public string materialFlag { get; set; }

        #region 库位表
        public int? binPriority { get; set; }
        #endregion


        public decimal? canQty { get; set; }

        public string stockType { set; get; }
        [Display(Name = "是否易动")]
        public string stockTypeDesc { set; get; }

        public int changeTime { get; set; }

        public class StockEditView
        {
            /// <summary>
            /// ID主键
            /// </summary>
            public string ids { get; set; }

            /// <summary>
            /// DATACODE
            /// </summary>
            public string dataCode { get; set; }

            /// <summary>
            /// MSL等级
            /// </summary>
            public string mslGradeCode { get; set; }
        }
    }
}
