using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_ITN_QC_RECORD_HIS")]
	public class WmsItnQcRecordHis : BasePoco
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

		///// <summary>
		///// 批次
		///// </summary>
		//[Column("BATCH_NO")]
		//[StringLength(100, ErrorMessage = "批次长度不能超出100字符")]
		//public string batchNo { get; set; }

		/// <summary>
		/// ERP仓库
		/// </summary>
		[Column("ERP_WHOUSE_NO")]
		[Required]
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
		public int inspectionResult { get; set; }

		/// <summary>
		/// 抽检单明细ID
		/// </summary>
		[Column("ITN_QC_DTL_ID")]
		//[Required]
		//[StringLength(36, ErrorMessage = "抽检单明细ID长度不能超出36字符")]
		//public string itnQcDtlId { get; set; }
		public Int64 itnQcDtlId { get; set; }

		/// <summary>
		/// 抽检下架站台
		/// </summary>
		[Column("ITN_QC_LOC_NO")]
		//[Required]
		[StringLength(100, ErrorMessage = "抽检下架站台长度不能超出100字符")]
		public string itnQcLocNo { get; set; }

		/// <summary>
		/// 抽检单号
		/// </summary>
		[Column("ITN_QC_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "抽检单号长度不能超出100字符")]
		public string itnQcNo { get; set; }

		/// <summary>
		/// 抽检记录状态;0：初始创建；11：已下发；66：待确认中；77：已确认；99：抽检完成；111：已删除
		/// </summary>
		[Column("ITN_QC_STATUS")]
		[Required]
		//[StringLength(50, ErrorMessage = "抽检记录状态;0：初始创建；11：已下发；66：待确认中；77：已确认；99：抽检完成；111：已删除长度不能超出50字符")]
		public int? itnQcStatus { get; set; } = 0;

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

		///// <summary>
		///// 占用数量
		///// </summary>
		//[Column("OCCUPY_QTY", TypeName = "decimal(18,3)")]
		//public decimal?  occupyQty { get; set; }

		/// <summary>
		/// 载体条码
		/// </summary>
		[Column("PALLET_BARCODE")]
		[Required]
		[StringLength(100, ErrorMessage = "载体条码长度不能超出100字符")]
		public string palletBarcode { get; set; }

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

		///// <summary>
		///// SKU编码
		///// </summary>
		//[Column("SKU_CODE")]
		//[StringLength(500, ErrorMessage = "SKU编码长度不能超出500字符")]
		//public string skuCode { get; set; }

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

		/// <summary>
		/// 库存数量
		/// </summary>
		[Column("STOCK_QTY", TypeName = "decimal(18,3)")]
		[Required]
		public decimal?  stockQty { get; set; }

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
		/// 供应商编码
		/// </summary>
		[Column("SUPPLIER_CODE")]
		[StringLength(100, ErrorMessage = "供应商编码长度不能超出100字符")]
		public string supplierCode { get; set; }

		/// <summary>
		/// 仓库号
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
		public string whouseNo { get; set; }


	}
}
