using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_STOCK_UNIICODE")]
    [Index(nameof(uniicode), IsUnique = true)]
    public class WmsStockUniicode : BasePoco
	{
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 区域编码(楼号)--生产部门--SourceBy
        /// </summary>
        [Column("AREA_NO")]
        [Display(Name = "生产部门")]
        [Required]
		[StringLength(100, ErrorMessage = "区域编码(楼号)长度不能超出100字符")]
		public string areaNo { get; set; }

		/// <summary>
		/// 批次
		/// </summary>
		[Column("BATCH_NO")]
        [Display(Name = "批次")]
        [StringLength(100, ErrorMessage = "批次长度不能超出100字符")]
		public string batchNo { get; set; }

		/// <summary>
		/// DATACODE
		/// </summary>
		[Column("DATA_CODE")]
        [Display(Name = "DC")]
        [StringLength(100, ErrorMessage = "DATACODE长度不能超出100字符")]
		public string dataCode { get; set; }

		/// <summary>
		/// 有效期冻结
		/// </summary>
		[Column("DELAY_FROZEN_FLAG")]
        [Display(Name = "有效期冻结")]
        public int?  delayFrozenFlag { get; set; }

		/// <summary>
		/// 有效期冻结原因
		/// </summary>
		[Column("DELAY_FROZEN_REASON")]
        [Display(Name = "有效期冻结原因")]
        [StringLength(500, ErrorMessage = "有效期冻结原因长度不能超出500字符")]
		public string delayFrozenReason { get; set; }

		/// <summary>
		/// 延长有效期原因
		/// </summary>
		[Column("DELAY_REASON")]
        [Display(Name = "延长有效期原因")]
        [StringLength(500, ErrorMessage = "延长有效期原因长度不能超出500字符")]
		public string delayReason { get; set; }

		/// <summary>
		/// 延期次数
		/// </summary>
		[Column("DELAY_TIMES")]
        [Display(Name = "延期次数")]
        public int?  delayTimes { get; set; }

        /// <summary>
        /// 延长有效期至日期---DAF 贴膜时间
        /// </summary>
        [Column("DELAY_TO_END_DATE")]
        [Display(Name = "DAF 贴膜时间")]
        public DateTime? delayToEndDate { get; set; }

		///// <summary>
		///// 删除标志;0-有效,1-已删除
		///// </summary>
		//[Column("DEL_FLAG")]
		//[StringLength(50, ErrorMessage = "删除标志;0-有效,1-已删除长度不能超出50字符")]
		//public string delFlag { get; set; } = "0";

		/// <summary>
		/// 是否烘干报废;0-有效,1-已报废
		/// </summary>
		[Column("DRIED_SCRAP_FLAG")]
        [Display(Name = "是否烘干报废")]
        //[StringLength(50, ErrorMessage = "是否烘干报废;0-有效,1-已报废长度不能超出50字符")]
		public int? driedScrapFlag { get; set; }

		/// <summary>
		/// 已烘干次数
		/// </summary>
		[Column("DRIED_TIMES")]
        [Display(Name = "已烘干次数")]
        public int?  driedTimes { get; set; }

		/// <summary>
		/// ERP仓库
		/// </summary>
		[Column("ERP_WHOUSE_NO")]
        [Display(Name = "ERP仓库")]
        [StringLength(100, ErrorMessage = "ERP仓库长度不能超出100字符")]
		public string erpWhouseNo { get; set; }

        /// <summary>
        /// 失效日期---DAF 过期时间
        /// </summary>
        [Column("EXP_DATE")]
        [Display(Name = "DAF 过期时间")]
        public DateTime? expDate { get; set; }

		/// <summary>
		/// 暴露冻结
		/// </summary>
		[Column("EXPOSE_FROZEN_FLAG")]
        [Display(Name = "暴露冻结标志")]
        public int?  exposeFrozenFlag { get; set; }

		/// <summary>
		/// 暴露冻结原因
		/// </summary>
		[Column("EXPOSE_FROZEN_REASON")]
        [Display(Name = "暴露冻结原因")]
        [StringLength(500, ErrorMessage = "暴露冻结原因长度不能超出500字符")]
		public string exposeFrozenReason { get; set; }

		/// <summary>
		/// 扩展字段1
		/// </summary>
		[Column("EXTEND1")]
		[StringLength(200, ErrorMessage = "扩展字段1长度不能超出200字符")]
		public string extend1 { get; set; }

		/// <summary>
		/// 扩展字段10
		/// </summary>
		[Column("EXTEND10")]
		[StringLength(200, ErrorMessage = "扩展字段10长度不能超出200字符")]
		public string extend10 { get; set; }

		/// <summary>
		/// 扩展字段11
		/// </summary>
		[Column("EXTEND11")]
		[StringLength(200, ErrorMessage = "扩展字段11长度不能超出200字符")]
		public string extend11 { get; set; }

		/// <summary>
		/// 芯片尺寸
		/// </summary>
		[Column("CHIP_SIZE")]
		[StringLength(200, ErrorMessage = "芯片尺寸长度不能超出200字符")]
        [Display(Name = "芯片尺寸")]
        public string chipSize { get; set; }

		/// <summary>
		/// 芯片厚度
		/// </summary>
		[Column("CHIP_THICKNESS")]
		[StringLength(200, ErrorMessage = "芯片厚度长度不能超出200字符")]
        [Display(Name = "芯片厚度")]
        public string chipThickness { get; set; }

		/// <summary>
		/// 芯片型号
		/// </summary>
		[Column("CHIP_MODEL")]
		[StringLength(200, ErrorMessage = "芯片型号长度不能超出200字符")]
        [Display(Name = "芯片型号")]
        public string chipModel { get; set; }

		/// <summary>
		/// DAF型号
		/// </summary>
		[Column("DAF_TYPE")]
		[StringLength(200, ErrorMessage = "DAF型号长度不能超出200字符")]
        [Display(Name = "DAF型号")]
        public string dafType { get; set; }

		/// <summary>
		/// 扩展字段2
		/// </summary>
		[Column("EXTEND2")]
		[StringLength(200, ErrorMessage = "扩展字段2长度不能超出200字符")]
		public string extend2 { get; set; }

		/// <summary>
		/// 扩展字段3
		/// </summary>
		[Column("EXTEND3")]
		[StringLength(200, ErrorMessage = "扩展字段3长度不能超出200字符")]
		public string extend3 { get; set; }

		/// <summary>
		/// 扩展字段4
		/// </summary>
		[Column("EXTEND4")]
		[StringLength(200, ErrorMessage = "扩展字段4长度不能超出200字符")]
		public string extend4 { get; set; }

		/// <summary>
		/// 扩展字段5
		/// </summary>
		[Column("EXTEND5")]
		[StringLength(200, ErrorMessage = "扩展字段5长度不能超出200字符")]
		public string extend5 { get; set; }

		/// <summary>
		/// 扩展字段6
		/// </summary>
		[Column("EXTEND6")]
		[StringLength(200, ErrorMessage = "扩展字段6长度不能超出200字符")]
		public string extend6 { get; set; }

		/// <summary>
		/// 扩展字段7
		/// </summary>
		[Column("EXTEND7")]
		[StringLength(200, ErrorMessage = "扩展字段7长度不能超出200字符")]
		public string extend7 { get; set; }

		/// <summary>
		/// 扩展字段8
		/// </summary>
		[Column("EXTEND8")]
		[StringLength(200, ErrorMessage = "扩展字段8长度不能超出200字符")]
		public string extend8 { get; set; }

		/// <summary>
		/// 扩展字段9
		/// </summary>
		[Column("EXTEND9")]
		[StringLength(200, ErrorMessage = "扩展字段9长度不能超出200字符")]
		public string extend9 { get; set; }

		/// <summary>
		/// 质量标记;0：待检；1：合格；2：不合格；3：免检；4：冻结；
		/// </summary>
		[Column("INSPECTION_RESULT")]
		//[StringLength(50, ErrorMessage = "质量标记;0：待检；1：合格；2：不合格；3：免检；4：冻结；长度不能超出50字符")]
		public int? inspectionResult { get; set; }

		/// <summary>
		/// 入库时间
		/// </summary>
		[Column("INWH_TIME")]
        [Display(Name = "入库时间")]
        public DateTime inwhTime { get; set; }

		/// <summary>
		/// 剩余湿敏时长
		/// </summary>
		[Column("LEFT_MSL_TIMES", TypeName = "decimal(18,3)")]
        [Display(Name = "剩余湿敏时长")]
        public decimal?  leftMslTimes { get; set; }

		/// <summary>
		/// 物料名称
		/// </summary>
		[Column("MATERIAL_NAME")]
        [Display(Name = "物料名称")]
        [StringLength(500, ErrorMessage = "物料名称长度不能超出500字符")]
		public string materialName { get; set; }

		/// <summary>
		/// 物料编码
		/// </summary>
		[Column("MATERIAL_CODE")]
        [Display(Name = "物料编码")]
        [Required]
		[StringLength(100, ErrorMessage = "物料编码长度不能超出100字符")]
		public string materialCode { get; set; }

		/// <summary>
		/// 规格型号
		/// </summary>
		[Column("MATERIAL_SPEC")]
        [Display(Name = "规格")]
        [StringLength(4000, ErrorMessage = "规格型号长度不能超出4000字符")]
		public string materialSpec { get; set; }

		/// <summary>
		/// MSL等级编码
		/// </summary>
		[Column("MSL_GRADE_CODE")]
        [Display(Name = "MSL等级")]
        [StringLength(100, ErrorMessage = "MSL等级编码长度不能超出100字符")]
		public string mslGradeCode { get; set; }

        ///// <summary>
        ///// 数量
        ///// </summary>
        //[Column("QTY")]
        //public decimal? qty { get; set; }

        /// <summary>
        /// 占用数量
        /// </summary>
        [Column("OCCUPY_QTY", TypeName = "decimal(18,3)")]
        [Display(Name = "占用数量")]
        [Required]
		public decimal?  occupyQty { get; set; }

		/// <summary>
		/// 封包时间;如果需要封包日期从这格式化取
		/// </summary>
		[Column("PACKAGE_TIME")]
        [Display(Name = "封包时间")]
        public DateTime? packageTime { get; set; }

		/// <summary>
		/// 载体条码
		/// </summary>
		[Column("PALLET_BARCODE")]
        [Display(Name = "载体条码")]
        [Required]
		[StringLength(100, ErrorMessage = "载体条码长度不能超出100字符")]
		public string palletBarcode { get; set; }

		/// <summary>
		/// 所在载体位置
		/// </summary>
		[Column("POSITION_NO")]
		[StringLength(100, ErrorMessage = "所在载体位置长度不能超出100字符")]
		public string positionNo { get; set; }

		/// <summary>
		/// 生产日期
		/// </summary>
		[Column("PRODUCT_DATE")]
        [Display(Name = "生产日期")]
        public DateTime? productDate { get; set; }

		/// <summary>
		/// 项目号--工单号
		/// </summary>
		[Column("PROJECT_NO")]
        [Display(Name = "工单号")]
        [StringLength(100, ErrorMessage = "项目号长度不能超出100字符")]
		public string projectNo { get; set; }

		/// <summary>
		/// 货主
		/// </summary>
		[Column("PROPRIETOR_CODE")]
		[StringLength(100, ErrorMessage = "货主长度不能超出100字符")]
		public string proprietorCode { get; set; }

		/// <summary>
		/// 实际暴露时长
		/// </summary>
		[Column("REAL_EXPOSE_TIMES", TypeName = "decimal(18,3)")]
        [Display(Name = "实际暴露时长")]
        public decimal?  realExposeTimes { get; set; }

		/// <summary>
		/// SKU编码
		/// </summary>
		[Column("SKU_CODE")]
		[StringLength(500, ErrorMessage = "SKU编码长度不能超出500字符")]
		public string skuCode { get; set; }

		/// <summary>
		/// 库存编码
		/// </summary>
		[Column("STOCK_CODE")]
		[Required]
		[StringLength(100, ErrorMessage = "库存编码长度不能超出100字符")]
        [Display(Name = "库存编码")]
        public string stockCode { get; set; }

		/// <summary>
		/// 库存明细ID
		/// </summary>
		[Column("STOCK_DTL_ID")]
		[Required]
        //[StringLength(36, ErrorMessage = "库存明细ID长度不能超出36字符")]
        //public string stockDtlId { get; set; }
        [Display(Name = "库存明细ID")]
        public Int64 stockDtlId { get; set; }

		/// <summary>
		/// 库存数量
		/// </summary>
		//[Column("STOCK_QTY", TypeName = "decimal(18,3)")]
		[Column("QTY", TypeName = "decimal(18,3)")]
        [Display(Name = "库存数量")]
        [Required]
		//public decimal?  stockQty { get; set; }
		public decimal?  qty { get; set; }

		/// <summary>
		/// 供应商编码
		/// </summary>
		[Column("SUPPLIER_CODE")]
        [Display(Name = "客户代码")]
        [StringLength(100, ErrorMessage = "供应商编码长度不能超出100字符")]
		public string supplierCode { get; set; }

		/// <summary>
		/// 供应商暴露时长
		/// </summary>
		[Column("SUPPLIER_EXPOSE_TIMES", TypeName = "decimal(18,3)")]
        [Display(Name = "供应商暴露时长")]
        public decimal?  supplierExposeTimes { get; set; }

		/// <summary>
		/// 供应商名称
		/// </summary>
		[Column("SUPPLIER_NAME")]
        [Display(Name = "供应商名称")]
        [StringLength(500, ErrorMessage = "供应商名称长度不能超出500字符")]
		public string supplierName { get; set; }

		/// <summary>
		/// 供应商名称-其他
		/// </summary>
		[Column("SUPPLIER_NAME_ALIAS")]
		[StringLength(500, ErrorMessage = "供应商名称-其他长度不能超出500字符")]
		public string supplierNameAlias { get; set; }

		/// <summary>
		/// 供应商名称-英文
		/// </summary>
		[Column("SUPPLIER_NAME_EN")]
		[StringLength(500, ErrorMessage = "供应商名称-英文长度不能超出500字符")]
		public string supplierNameEn { get; set; }

		/// <summary>
		/// 库存唯一码
		/// </summary>
		[Column("UNIICODE")]
        [Display(Name = "包装条码/SN码")]
        [Required]
		[StringLength(100, ErrorMessage = "库存唯一码长度不能超出100字符")]
		public string uniicode { get; set; }

		/// <summary>
		/// 拆封状态;0:已封包，1:已开包--是否下单：0否,1是
		/// </summary>
		[Column("UNPACK_STATUS")]
        [Display(Name = "是否下单")]
        //[StringLength(50, ErrorMessage = "拆封状态;0:已封包，1:已开包长度不能超出50字符")]
		public int? unpackStatus { get; set; }

		/// <summary>
		/// 开封时间;如果需要开封日期从这格式化取
		/// </summary>
		[Column("UNPACK_TIME")]
        [Display(Name = "开封时间")]
        public DateTime? unpackTime { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [Column("UNIT_CODE")]
        [Display(Name = "单位")]
        [StringLength(50, ErrorMessage = "单位长度不能超出50字符")]
        public string unitCode { get; set; }

        /// <summary>
        /// 仓库号
        /// </summary>
        [Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
		public string whouseNo { get; set; }

		/// <summary>
		/// 附件ID
		/// </summary>
		[Column("FILEED_ID")]
		[StringLength(200, ErrorMessage = "附件ID长度不能超出200字符")]
		public string fileedId { get; set; }

		/// <summary>
		/// 附件名称
		/// </summary>
		[Column("FILEED_NAME")]
		[Display(Name = "附件名称")]
		[StringLength(200, ErrorMessage = "附件名称长度不能超出200字符")]
		public string fileedName { get; set; }

        /// <summary>
        /// 备用项目号
        /// </summary>
        [Column("PROJECT_NO_BAK")]
        [Display(Name = "备用项目号")]
        [StringLength(100, ErrorMessage = "备用项目号长度不能超出100字符")]
        public string projectNoBak { get; set; }
    }
}
