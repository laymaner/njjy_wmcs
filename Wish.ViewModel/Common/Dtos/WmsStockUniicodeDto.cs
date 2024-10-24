using Com.Wish.Model.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common.Dtos
{
    public class WmsStockUniicodeDto : WmsStockUniicode
    {

        public string fileName { get; set; }
        [Display(Name = "库存状态")]
        public int stockDtlStatus { get; set; }

        public string stockDtlStatusDesc { get; set; }

        public string delayFrozenFlagDesc { get; set; }

        public string driedScrapFlagDesc { get; set; }

        public string exposeFrozenFlagDesc { get; set; }

        public string unpackStatusDesc { get; set; }

        public string inspectionResultDesc { get; set; }

        public string stockStatusDesc { get; set; }

        public string lockFlagDesc { get; set; }

        public string specialFlagDesc { get; set; }

        public string whouseName { get; set; }

        public string areaName { get; set; }


        public string proprietorName { get; set; }

        public string erpWhouseName { get; set; }
        [Display(Name = "单位")]
        public string unitCode { get; set; }
        public string unitName { get; set; }
        [Display(Name = "备用项目号")]
        public string projectNoBak { get; set; }


        #region 明细表
        public decimal? qty { get; set; }
        [Display(Name = "是否锁定")]
        public int? lockFlag { get; set; }
        [Display(Name = "锁定原因")]
        public String lockReason { get; set; }
        #endregion

        #region 主表
        [Display(Name = "库区")]
        public string regionNo { get; set; }

        public string regionName { get; set; }
        [Display(Name = "巷道")]
        public string roadwayNo { get; set; }

        public string roadwayName { get; set; }
        [Display(Name = "库位")]
        public string binNo { get; set; }

        public string binName { get; set; }

        public int? loadedType { get; set; }

        public string loadedTypeDesc { get; set; }

        public string locAllotGroup { get; set; }

        public int? errFlag { get; set; }

        public string errFlagDesc { get; set; }

        public string invoiceNo { get; set; }

        public decimal? specialFlag { get; set; }

        public string stockDelFlag { get; set; }
        public string locNo { get; set; }

        public decimal? weight { get; set; }

        public string errMsg { get; set; }

        public string specialMsg { get; set; }

        public decimal? stockStatus { get; set; }
        #endregion

        public int stockDeadType { get; set; }

        public string alertDateType { get; set; }
        public string stockDeadTypeDesc { get; set; }


        public string defaultValidityDate { get; set; }

        public decimal? warnOverdueLen { get; set; }

        public decimal? sluggishTime { get; set; }
        /// <summary>
        /// 库存已呆滞天数
        /// </summary>
        public double? stockSluggishedTime { get; set; }

        public string materialTypeCode { get; set; }
        public int? maxDelayTimes { get; set; }


        public string materialTypeName { get; set; }
        [Display(Name = "物料大类")]
        public string materialCategoryCode { get; set; }
        [Display(Name = "工程图号")]
        public string projectDrawingNo { get; set; }

        public string materialCategoryName { get; set; }

        [Display(Name = "电子料标识")]
        public string materialFlagDesc { get; set; }

        public string materialFlag { get; set; }

        public decimal? exposeTimeWarn { get; set; }

        public string ematerialVtime { get; set; }

        public decimal? maxExposeTimes { get; set; }

        public decimal? canQty { get; set; }

        public decimal? dtlQty { get; set; }

        public decimal? dtlOccupyQty { get; set; }

        public decimal? dtlCanQty { get; set; }

        public string createByDesc { set; get; }

        public string updateByDesc { set; get; }

        public string stockType { set; get; }

        [Display(Name = "是否易动")]
        public string stockTypeDesc { set; get; }

        public int changeTime { get; set; }
    }
}
