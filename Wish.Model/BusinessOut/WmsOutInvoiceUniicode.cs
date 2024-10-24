using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_OUT_INVOICE_UNIICODE")]
	public class WmsOutInvoiceUniicode : BasePoco
	{
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 区域编码(楼号)
        /// </summary>
        [Column("AREA_NO")]
		[Required]
        [Display(Name = "楼号")]
        [StringLength(100, ErrorMessage = "区域编码(楼号)长度不能超出100字符")]
		public string areaNo { get; set; }

		/// <summary>
		/// 批次号
		/// </summary>
		[Column("BATCH_NO")]
        [Display(Name = "批次")]
        [StringLength(100, ErrorMessage = "批次号长度不能超出100字符")]
		public string batchNo { get; set; }

		/// <summary>
		/// DATACODE
		/// </summary>
		[Column("DATA_CODE")]
        [Display(Name = "DC")]
        [StringLength(100, ErrorMessage = "DATACODE长度不能超出100字符")]
		public string dataCode { get; set; }

		/// <summary>
		/// 延长有效期原因
		/// </summary>
		[Column("DELAY_REASON")]
        [Display(Name = "延期原因")]
        [StringLength(500, ErrorMessage = "延长有效期原因长度不能超出500字符")]
		public string delayReason { get; set; }

		/// <summary>
		/// 延期次数
		/// </summary>
		[Column("DELAY_TIMES")]
        [Display(Name = "延期次数")]
        public int?  delayTimes { get; set; }

		/// <summary>
		/// 延长有效期至日期
		/// </summary>
		[Column("DELAY_TO_END_DATE")]
        [Display(Name = "延长有效期至日期")]
        public DateTime? delayToEndDate { get; set; }

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
		/// ERP货位
		/// </summary>
		[Column("ERP_BIN_NO")]
		[StringLength(100, ErrorMessage = "ERP货位长度不能超出100字符")]
		public string erpBinNo { get; set; }

		/// <summary>
		/// ERP仓库
		/// </summary>
		[Column("ERP_WHOUSE_NO")]
		[Required]
        [Display(Name = "ERP仓库")]
        [StringLength(100, ErrorMessage = "ERP仓库长度不能超出100字符")]
		public string erpWhouseNo { get; set; }

		/// <summary>
		/// 失效日期
		/// </summary>
		[Column("EXP_DATE")]
        [Display(Name = "失效日期")]
        public DateTime? expDate { get; set; }

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
		/// 扩展字段12
		/// </summary>
		[Column("EXTEND12")]
		[StringLength(200, ErrorMessage = "扩展字段12长度不能超出200字符")]
		public string extend12 { get; set; }

		/// <summary>
		/// 扩展字段13
		/// </summary>
		[Column("EXTEND13")]
		[StringLength(200, ErrorMessage = "扩展字段13长度不能超出200字符")]
		public string extend13 { get; set; }

		/// <summary>
		/// 扩展字段14
		/// </summary>
		[Column("EXTEND14")]
		[StringLength(200, ErrorMessage = "扩展字段14长度不能超出200字符")]
		public string extend14 { get; set; }

		/// <summary>
		/// 扩展字段15
		/// </summary>
		[Column("EXTEND15")]
		[StringLength(200, ErrorMessage = "扩展字段15长度不能超出200字符")]
		public string extend15 { get; set; }

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
		/// 质检结果;待检、合格、不合格
		/// </summary>
		[Column("INSPECTION_RESULT")]
        [Display(Name = "质检结果")]
        //[StringLength(50, ErrorMessage = "质检结果;待检、合格、不合格长度不能超出50字符")]
		public int? inspectionResult { get; set; }

		/// <summary>
		/// 发货单明细ID
		/// </summary>
		[Column("INVOICE_DTL_ID")]
		//[StringLength(36, ErrorMessage = "发货单明细ID长度不能超出36字符")]
		public Int64? invoiceDtlId { get; set; }

		/// <summary>
		/// 发货单号
		/// </summary>
		[Column("INVOICE_NO")]
		[Required]
        [Display(Name = "WMS发货单号")]
        [StringLength(100, ErrorMessage = "发货单号长度不能超出100字符")]
		public string invoiceNo { get; set; }

		/// <summary>
		/// 出库记录ID（用于关联出库唯一码表）
		/// </summary>
		[Column("INVOICE_RECORD_ID")]
		//[StringLength(36, ErrorMessage = "出库记录ID长度不能超出36字符")]
		public Int64? invoiceRecordId { get; set; }

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
		[Column("MATERIAL_NO")]
		[Required]
        [Display(Name = "物料编码")]
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
		[Required]
        [Display(Name = "载体条码")]
        [StringLength(100, ErrorMessage = "载体条码长度不能超出100字符")]
		public string palletBarcode { get; set; }

        /// <summary>
        /// 分配数量
        /// </summary>
        [Column("ALLOT_QTY", TypeName = "decimal(18,3)")]
        [Display(Name = "分配数量")]
        [Required]
        public decimal? allotQty { get; set; }

        /// <summary>
        /// 拣选数量
        /// </summary>
        [Column("PICK_QTY", TypeName = "decimal(18,3)")]
        [Display(Name = "拣选数量")]
        [Required]
		public decimal?  pickQty { get; set; }

		/// <summary>
		/// 拣选任务编号;就是自动生成序列号
		/// </summary>
		[Column("PICK_TASK_NO")]
        [Display(Name = "拣选任务号")]
        [StringLength(100, ErrorMessage = "拣选任务编号;就是自动生成序列号长度不能超出100字符")]
		public string pickTaskNo { get; set; }

		/// <summary>
		/// 生产日期
		/// </summary>
		[Column("PRODUCT_DATE")]
        [Display(Name = "生产日期")]
        public DateTime? productDate { get; set; }

		/// <summary>
		/// 项目号
		/// </summary>
		[Column("PROJECT_NO")]
        [Display(Name = "项目号")]
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
      //  [Display(Name = "SKU编码")]
        [StringLength(255, ErrorMessage = "SKU编码长度不能超出255字符")]
		public string skuCode { get; set; }

		/// <summary>
		/// 库存编码
		/// </summary>
		[Column("STOCK_CODE")]
		[Required]
        [Display(Name = "库存编码")]
        [StringLength(100, ErrorMessage = "库存编码长度不能超出100字符")]
		public string stockCode { get; set; }

		/// <summary>
		/// 库存明细ID
		/// </summary>
		[Column("STOCK_DTL_ID")]
		//[Required]
		//[StringLength(36, ErrorMessage = "库存明细ID长度不能超出36字符")]
		//public string stockDtlId { get; set; }
		public Int64 stockDtlId { get; set; }

		/// <summary>
		/// 供应商编码
		/// </summary>
		[Column("SUPPLIER_CODE")]
        [Display(Name = "供应商编码")]
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
		/// 唯一码：包装条码/SN码
		/// </summary>
		[Column("UNIICODE")]
		[Required]
        [Display(Name = "包装条码/SN码")]
        [StringLength(100, ErrorMessage = "唯一码：包装条码/SN码长度不能超出100字符")]
		public string uniicode { get; set; }

		/// <summary>
		/// 开封时间;如果需要开封日期从这格式化取
		/// </summary>
		[Column("UNPACK_TIME")]
        [Display(Name = "开封时间")]
        public DateTime? unpackTime { get; set; }

		/// <summary>
		/// 波次号
		/// </summary>
		[Column("WAVE_NO")]
        [Display(Name = "WMS波次号")]
        [StringLength(100, ErrorMessage = "波次号长度不能超出100字符")]
		public string waveNo { get; set; }

		/// <summary>
		/// 仓库号
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
		public string whouseNo { get; set; }

        /// <summary>
        /// 出库条码
        /// </summary>
        [Column("OUT_BARCODE")]
        [Display(Name = "出库条码")]
        [StringLength(100, ErrorMessage = "出库条码长度不能超出100字符")]
        public string outBarcode { get; set; }

        /// <summary>
        /// 出库唯一码状态
        /// </summary>
        [Column("OUNII_STATUS")]
        [Display(Name = "包装条码状态")]
        [StringLength(100, ErrorMessage = "出库唯一码状态不能超出100字符")]
        public int? ouniiStatus { get; set; }
    }
}
