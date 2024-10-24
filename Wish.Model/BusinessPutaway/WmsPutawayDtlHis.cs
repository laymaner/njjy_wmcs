using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_PUTAWAY_DTL_HIS")]
	public class WmsPutawayDtlHis : BasePoco
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
        [StringLength(100, ErrorMessage = "区域编码(楼号)长度不能超出100字符")]
        public string areaNo { get; set; }

  //      /// <summary>
  //      /// 码级管理：0无码，1一级码，2二级码
  //      /// </summary>
  //      [Column("BARCODE_FLAG")]
  //      [Display(Name = "码级管理")]
  //      [StringLength(100, ErrorMessage = "码级管理长度不能超出100字符")]
		//public string barcodeFlag { get; set; }
		
		///// <summary>
		///// 批次号
		///// </summary>
		////[Column("BATCH_NO")]
  //      [Display(Name = "批次号")]
  //      [StringLength(100, ErrorMessage = "批次号长度不能超出100字符")]
		//public string batchNo { get; set; }

		/// <summary>
		/// 系统推荐库位号
		/// </summary>
		[Column("BIN_NO")]
		[StringLength(100, ErrorMessage = "系统推荐库位号长度不能超出100字符")]
		public string binNo { get; set; }
		
		/// <summary>
		/// 容器号
		/// </summary>
		//[Column("CONTAINER_BARCODE")]
		//[StringLength(100, ErrorMessage = "容器号长度不能超出100字符")]
		//public string containerBarcode { get; set; }

		///// <summary>
		///// 删除标志;0-有效,1-已删除
		///// </summary>
		//[Column("DEL_FLAG")]
		//[StringLength(50, ErrorMessage = "删除标志;0-有效,1-已删除长度不能超出50字符")]
		//public string delFlag { get; set; } = "0";

		/// <summary>
		/// 单据类型
		/// </summary>
		[Column("DOC_TYPE_CODE")]
		//[Required]
		[StringLength(100, ErrorMessage = "单据类型长度不能超出100字符")]
		public string docTypeCode { get; set; }

		/// <summary>
		/// ERP仓库
		/// </summary>
		[Column("ERP_WHOUSE_NO")]
		//[Required]
		[StringLength(100, ErrorMessage = "ERP仓库长度不能超出100字符")]
		public string erpWhouseNo { get; set; }

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
		/// 质量标记;0：待检；1：合格；2：不合格；3：免检；4：冻结；
		/// </summary>
		[Column("INSPECTION_RESULT")]
		//[StringLength(50, ErrorMessage = "质量标记;0：待检；1：合格；2：不合格；3：免检；4：冻结；长度不能超出50字符")]
		public int? inspectionResult { get; set; }

		/// <summary>
		/// 物料编码
		/// </summary>
		[Column("MATERIAL_CODE")]
		[Required]
        [Display(Name = "物料编码")]
        [StringLength(100, ErrorMessage = "物料编码长度不能超出100字符")]
		public string materialCode { get; set; }
		
		/// <summary>
		/// 物料编码
		/// </summary>
		[Column("MATERIAL_NAME")]
		[Required]
        [Display(Name = "物料编码")]
        [StringLength(100, ErrorMessage = "物料编码长度不能超出100字符")]
		public string materialName { get; set; }

		/// <summary>
		/// 物料规格
		/// </summary>
		[Column("MATERIAL_SPEC")]
        [Display(Name = "物料规格")]
        [StringLength(4000, ErrorMessage = "物料规格长度不能超出4000字符")]
		public string materialSpec { get; set; }

        /// <summary>
        /// 最小包装量
        /// </summary>
        //[Column("MIN_PKG_QTY", TypeName = "decimal(9,3)")]
        //[Display(Name = "最小包装量")]
        //public decimal minPkgQty { get; set; }

        /// <summary>
        /// 关联单据明细ID
        /// </summary>
        [Column("ORDER_DTL_ID")]
		//[StringLength(100, ErrorMessage = "关联单据明细ID长度不能超出100字符")]
		public Int64? orderDtlId { get; set; }

		/// <summary>
		/// 关联单据编号
		/// </summary>
		[Column("ORDER_NO")]
        [Display(Name = "关联单据编号")]
        [StringLength(100, ErrorMessage = "关联单据编号长度不能超出100字符")]
		public string orderNo { get; set; }

		/// <summary>
		/// 载体条码
		/// </summary>
		[Column("PALLET_BARCODE")]
		[Required]
        [Display(Name = "载体条码")]
        [StringLength(100, ErrorMessage = "载体条码长度不能超出100字符")]
		public string palletBarcode { get; set; }
		
		/// <summary>
		/// 载体条码
		/// </summary>
		[Column("PROJECT_NO")]
		//[Required]
        [Display(Name = "项目号")]
        [StringLength(100, ErrorMessage = "项目号长度不能超出100字符")]
		public string projectNo { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        //[Column("PRODUCT_DATE")]
        //[Display(Name = "生产日期")]
        //public DateTime? productDate { get; set; }

        /// <summary>
        /// 生产计划单编号
        /// </summary>
  //      [Column("PRODUCTION_PLAN_NO")]
  //      [Display(Name = "生产计划单编号")]
  //      [StringLength(100, ErrorMessage = "生产计划单编号长度不能超出100字符")]
		//public string productionPlanNo { get; set; }

		/// <summary>
		/// 货主
		/// </summary>
		[Column("PROPRIETOR_CODE")]
		[StringLength(100, ErrorMessage = "货主长度不能超出100字符")]
		public string proprietorCode { get; set; }

		/// <summary>
		/// 上架库位
		/// </summary>
		[Column("PTA_BIN_NO")]
        [Display(Name = "上架库位")]
        [StringLength(100, ErrorMessage = "上架库位长度不能超出100字符")]
		public string ptaBinNo { get; set; }
		
		/// <summary>
		/// 实际上架后的托盘号
		/// </summary>
		//[Column("PTA_PALLET_BARCODE")]
  //      [Display(Name = "实际上架后的托盘号")]
  //      [StringLength(100, ErrorMessage = "实际上架后的托盘号长度不能超出100字符")]
		//public string ptaPalletBarcode { get; set; }
		
		/// <summary>
		/// 实际上架后的库区
		/// </summary>
		//[Column("PTA_REGION_NO")]
  //      [Display(Name = "实际上架后的库区")]
  //      [StringLength(100, ErrorMessage = "实际上架后的库区长度不能超出100字符")]
		//public string ptaRegionNo { get; set; }
		
		/// <summary>
		/// 实际上架后的库存编码
		/// </summary>
		//[Column("PTA_STOCK_CODE")]
  //      [Display(Name = "实际上架后的库存编码")]
  //      [StringLength(100, ErrorMessage = "实际上架后的库存编码长度不能超出100字符")]
		//public string ptaStockCode { get; set; }
		
		/// <summary>
		/// 实际上架后的库存明细ID
		/// </summary>
		//[Column("PTA_STOCK_DTL_ID")]
  //      [Display(Name = "实际上架后的库存库存明细ID")]
  //      [StringLength(100, ErrorMessage = "实际上架后的库存库存明细ID长度不能超出100字符")]
		//public string ptaStockDtlId { get; set; }

		/// <summary>
		/// 状态;0：初始创建（组盘完成）；11：入库中；90：上架完成；92删除；93强制完成
		/// </summary>
		[Column("PUTAWAY_DTL_STATUS")]
		[Required]
        [Display(Name = "状态")]
        //[StringLength(50, ErrorMessage = "状态;0：初始创建（组盘完成）；11：入库中；90：上架完成；92删除；93强制完成长度不能超出50字符")]
		public int putawayDtlStatus { get; set; } = 0;

		/// <summary>
		/// 上架单编号
		/// </summary>
		[Column("PUTAWAY_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "上架单编号长度不能超出100字符")]
		public string putawayNo { get; set; }

		/// <summary>
		/// 记录ID
		/// </summary>
		[Column("RECORD_ID")]
		//[StringLength(36, ErrorMessage = "记录ID长度不能超出36字符")]
		//public string recordId { get; set; }
		public Int64? recordId { get; set; }

		/// <summary>
		/// 数量(组盘数量)
		/// </summary>
		[Column("RECORD_QTY", TypeName = "decimal(18,3)")]
		//[Column("QTY", TypeName = "decimal(18,3)")]
		[Required]
        [Display(Name = "数量")]
		public decimal? recordQty { get; set; }
		//public decimal? qty { get; set; }

		/// <summary>
		/// 库区编号
		/// </summary>
		[Column("REGION_NO")]
		[Required]
        [Display(Name = "库区编号")]
        [StringLength(100, ErrorMessage = "库区编号长度不能超出100字符")]
		public string regionNo { get; set; }

		/// <summary>
		/// 巷道
		/// </summary>
		[Column("ROADWAY_NO")]
		[Required]
        [Display(Name = "巷道")]
        [StringLength(100, ErrorMessage = "巷道长度不能超出100字符")]
		public string roadwayNo { get; set; }

		/// <summary>
		/// SKU编码
		/// </summary>
		[Column("SKU_CODE")]
        [Display(Name = "SKU编码")]
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
		//[Required]
		//[StringLength(36, ErrorMessage = "库存明细ID长度不能超出36字符")]
		//public string stockDtlId { get; set; }
		public Int64 stockDtlId { get; set; }

		///// <summary>
		///// 供应商批次
		///// </summary>
		//[Column("SUPPLIER_BATCH_NO")]
		//[StringLength(100, ErrorMessage = "供应商批次长度不能超出100字符")]
		//public string supplierBatchNo { get; set; }

		/// <summary>
		/// 供应商编码
		/// </summary>
		[Column("SUPPLIER_CODE")]
		[StringLength(100, ErrorMessage = "供应商编码长度不能超出100字符")]
		public string supplierCode { get; set; }

		/// <summary>
		/// 供方名称
		/// </summary>
		[Column("SUPPLIER_NAME")]
        [Display(Name = "供方名称")]
        [StringLength(500, ErrorMessage = "供方名称长度不能超出500字符")]
		public string supplierName { get; set; }

		/// <summary>
		/// 供方名称-其他
		/// </summary>
		[Column("SUPPLIER_NAME_ALIAS")]
		[StringLength(500, ErrorMessage = "供方名称-其他长度不能超出500字符")]
		public string supplierNameAlias { get; set; }

		/// <summary>
		/// 供方名称-英文
		/// </summary>
		[Column("SUPPLIER_NAME_EN")]
		[StringLength(500, ErrorMessage = "供方名称-英文长度不能超出500字符")]
		public string supplierNameEn { get; set; }

		/// <summary>
		/// 供货方类型;供应商、产线
		/// </summary>
		//[Column("SUPPLIER_TYPE")]
		//[StringLength(50, ErrorMessage = "供货方类型;供应商、产线长度不能超出50字符")]
		//public string supplierType { get; set; }

		/// <summary>
		/// 仓库号
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
		public string whouseNo { get; set; }
		
		/// <summary>
		/// 单位编码
		/// </summary>
		[Column("UNIT_CODE")]
		[StringLength(100, ErrorMessage = "单位编码长度不能超出100字符")]
		public string unitCode { get; set; }


	}
}
