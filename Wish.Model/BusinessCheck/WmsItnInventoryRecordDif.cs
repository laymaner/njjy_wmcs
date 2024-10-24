using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_ITN_INVENTORY_RECORD_DIF")]
	public class WmsItnInventoryRecordDif : BasePoco
	{
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        [Column("BATCH_NO")]
		[StringLength(100, ErrorMessage = "批次长度不能超出100字符")]
		public string batchNo { get; set; }

		/// <summary>
		/// DATACODE
		/// </summary>
		[Column("DATA_CODE")]
		[StringLength(100, ErrorMessage = "DATACODE长度不能超出100字符")]
		public string dataCode { get; set; }

		/// <summary>
		/// 有效期冻结
		/// </summary>
		[Column("DELAY_FROZEN_FLAG")]
		//[StringLength(50, ErrorMessage = "有效期冻结长度不能超出50字符")]
		//public string delayFrozenFlag { get; set; }
		public int? delayFrozenFlag { get; set; }

		/// <summary>
		/// 有效期冻结原因
		/// </summary>
		[Column("DELAY_FROZEN_REASON")]
		[StringLength(500, ErrorMessage = "有效期冻结原因长度不能超出500字符")]
		public string delayFrozenReason { get; set; }

		/// <summary>
		/// 延长有效期原因
		/// </summary>
		[Column("DELAY_REASON")]
		[StringLength(500, ErrorMessage = "延长有效期原因长度不能超出500字符")]
		public string delayReason { get; set; }

		/// <summary>
		/// 延期次数
		/// </summary>
		[Column("DELAY_TIMES")]
		public int?  delayTimes { get; set; }

		/// <summary>
		/// 延长有效期至日期
		/// </summary>
		[Column("DELAY_TO_END_DATE")]
		public DateTime? delayToEndDate { get; set; }

		///// <summary>
		///// 差异标识;0无差异，1盘盈，2盘亏
		///// </summary>
		//[Column("DIFFERENCE_FLAG")]
		//[Required]
		//[StringLength(50, ErrorMessage = "差异标识;0无差异，1盘盈，2盘亏长度不能超出50字符")]
		//public string differenceFlag { get; set; }

		/// <summary>
		/// 差异数量
		/// </summary>
		[Column("DIF_QTY", TypeName = "decimal(18,3)")]
		[Required]
		public decimal  difQty { get; set; }

		/// <summary>
		/// 是否烘干报废;0-有效,1-已报废
		/// </summary>
		[Column("DRIED_SCRAP_FLAG")]
		//[StringLength(50, ErrorMessage = "是否烘干报废;0-有效,1-已报废长度不能超出50字符")]
		//public string driedScrapFlag { get; set; }
		public int? driedScrapFlag { get; set; }

		/// <summary>
		/// 已烘干次数
		/// </summary>
		[Column("DRIED_TIMES")]
		public int?  driedTimes { get; set; }

		/// <summary>
		/// 失效日期
		/// </summary>
		[Column("EXP_DATE")]
		public DateTime? expDate { get; set; }

		/// <summary>
		/// 暴露冻结
		/// </summary>
		[Column("EXPOSE_FROZEN_FLAG")]
		//[StringLength(50, ErrorMessage = "暴露冻结长度不能超出50字符")]
		//public string exposeFrozenFlag { get; set; }
		public int? exposeFrozenFlag { get; set; }

		/// <summary>
		/// 暴露冻结原因
		/// </summary>
		[Column("EXPOSE_FROZEN_REASON")]
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
		/// 质量结果标记;0：待检；1：合格；2：不合格；3：免检；4：冻结；
		/// </summary>
		[Column("INSPECTION_RESULT")]
		//[StringLength(50, ErrorMessage = "质量结果标记;0：待检；1：合格；2：不合格；3：免检；4：冻结；长度不能超出50字符")]
		//public string inspectionResult { get; set; }
		public int? inspectionResult { get; set; }

		/// <summary>
		/// 盘点明细ID
		/// </summary>
		[Column("INVENTORY_DTL_ID")]
		[Required]
		[StringLength(36, ErrorMessage = "盘点明细ID长度不能超出36字符")]
		public string inventoryDtlId { get; set; }

		/// <summary>
		/// 盘点单号
		/// </summary>
		[Column("INVENTORY_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "盘点单号长度不能超出100字符")]
		public string inventoryNo { get; set; }

		/// <summary>
		/// 盘点数量
		/// </summary>
		[Column("INVENTORY_QTY", TypeName = "decimal(18,3)")]
		[Required]
		public decimal?  inventoryQty { get; set; }

		/// <summary>
		/// 盘点记录ID
		/// </summary>
		[Column("INVENTORY_RECORD_ID")]
		[Required]
		//[StringLength(36, ErrorMessage = "盘点记录ID长度不能超出36字符")]
		//public string inventoryRecordId { get; set; }
		public Int64 inventoryRecordId { get; set; }

		/// <summary>
		/// 剩余湿敏时长
		/// </summary>
		[Column("LEFT_MSL_TIMES", TypeName = "decimal(18,3)")]
		public decimal?  leftMslTimes { get; set; }

		///// <summary>
		///// 锁定状态;0：未锁定库存；1：已锁定库存。
		///// </summary>
		//[Column("LOCK_FLAG")]
		//[StringLength(50, ErrorMessage = "锁定状态;0：未锁定库存；1：已锁定库存。长度不能超出50字符")]
		//public string lockFlag { get; set; }

		///// <summary>
		///// 锁定原因
		///// </summary>
		//[Column("LOCK_REASON")]
		//[StringLength(500, ErrorMessage = "锁定原因长度不能超出500字符")]
		//public string lockReason { get; set; }

		/// <summary>
		/// 物料名称
		/// </summary>
		[Column("MATERIAL_NAME")]
		[StringLength(500, ErrorMessage = "物料名称长度不能超出500字符")]
		public string materialName { get; set; }

		/// <summary>
		/// 物料编码
		/// </summary>
		[Column("MATERIAL_CODE")]
		[Required]
		[StringLength(100, ErrorMessage = "物料编码长度不能超出100字符")]
		public string materialCode { get; set; }

		/// <summary>
		/// 规格型号
		/// </summary>
		[Column("MATERIAL_SPEC")]
		[StringLength(4000, ErrorMessage = "规格型号长度不能超出4000字符")]
		public string materialSpec { get; set; }

		/// <summary>
		/// MSL等级编码
		/// </summary>
		[Column("MSL_GRADE_CODE")]
		[StringLength(100, ErrorMessage = "MSL等级编码长度不能超出100字符")]
		public string mslGradeCode { get; set; }

		/// <summary>
		/// 封包时间;如果需要封包日期从这格式化取
		/// </summary>
		[Column("PACKAGE_TIME")]
		public DateTime? packageTime { get; set; }

		/// <summary>
		/// 载体条码
		/// </summary>
		[Column("PALLET_BARCODE")]
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
		public DateTime? productDate { get; set; }

		/// <summary>
		/// 项目号
		/// </summary>
		[Column("PROJECT_NO")]
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
		public string stockCode { get; set; }

		/// <summary>
		/// 库存明细ID
		/// </summary>
		[Column("STOCK_DTL_ID")]
		[Required]
        public Int64 stockDtlId { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        //[Column("STOCK_QTY", TypeName = "decimal(18,3)")]
        //[Required]
        //public decimal?  stockQty { get; set; }

        /// <summary>
        /// 供应商编码
        /// </summary>
        [Column("SUPPLIER_CODE")]
		[StringLength(100, ErrorMessage = "供应商编码长度不能超出100字符")]
		public string supplierCode { get; set; }

		/// <summary>
		/// 供应商暴露时长
		/// </summary>
		[Column("SUPPLIER_EXPOSE_TIMES", TypeName = "decimal(18,3)")]
		public decimal?  supplierExposeTimes { get; set; }

		/// <summary>
		/// 供应商名称
		/// </summary>
		[Column("SUPPLIER_NAME")]
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
		/// 唯一码
		/// </summary>
		[Column("UNIICODE")]
		[Required]
		[StringLength(100, ErrorMessage = "唯一码长度不能超出100字符")]
		public string uniicode { get; set; }

		/// <summary>
		/// 开封时间;如果需要开封日期从这格式化取
		/// </summary>
		[Column("UNPACK_TIME")]
		public DateTime? unpackTime { get; set; }

		/// <summary>
		/// 仓库号
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
		public string whouseNo { get; set; }
		
		/// <summary>
		/// 楼号
		/// </summary>
		[Column("AREA_NO")]
		[StringLength(100, ErrorMessage = "楼号长度不能超出100字符")]
		public string areaNo { get; set; }
		
		/// <summary>
		/// 删除标记
		/// </summary>
		[Column("DEL_FLAG")]
		[StringLength(100, ErrorMessage = "删除标记长度不能超出100字符")]
		public string delFlag { get; set; }

        /// <summary>
        /// ERP仓库
        /// </summary>
        [Column("ERP_WHOUSE_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "ERP仓库长度不能超出100字符")]
        public string erpWhouseNo { get; set; }

		/// <summary>
		/// 入库时间
		/// </summary>
		[Column("INWH_TIME")]
		[Display(Name = "入库时间")]
		public DateTime? inwhTime { get; set; }


		/// <summary>
		/// 占用数量
		/// </summary>
		[Column("OCCUPY_QTY", TypeName = "decimal(18,3)")]
		//[Required]
		public decimal? occupyQty { get; set; }
		
		/// <summary>
		/// 数量
		/// </summary>
		[Column("QTY", TypeName = "decimal(18,3)")]
		//[Required]
		public decimal? qty { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [Column("UNIT_CODE")]
        //[Required]
        [StringLength(50, ErrorMessage = "单位长度不能超出100字符")]
        public string unitCode { get; set; }

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
        /// 原库存明细ID
        /// </summary>
        [Column("OLD_STOCK_DTL_ID")]
        [Required]
        [StringLength(36, ErrorMessage = "原库存明细ID长度不能超出36字符")]
        public string oldStockDtlId { get; set; }

        /// <summary>
        /// 备用项目号
        /// </summary>
        [Column("PROJECT_NO_BAK")]
        [Display(Name = "备用项目号")]
        [StringLength(100, ErrorMessage = "备用项目号长度不能超出100字符")]
        public string projectNoBak { get; set; }

        /// <summary>
        /// 拆封状态;0:已封包，1:已开包
        /// </summary>
        [Column("UNPACK_STATUS")]
        [Display(Name = "拆封状态")]
        //[StringLength(50, ErrorMessage = "拆封状态;0:已封包，1:已开包长度不能超出50字符")]
        public int? unpackStatus { get; set; }
    }
}
