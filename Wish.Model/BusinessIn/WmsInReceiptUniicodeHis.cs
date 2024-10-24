using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_IN_RECEIPT_UNIICODE_HIS")]
    [Index(nameof(uniicode), IsUnique = true)]
    public class WmsInReceiptUniicodeHis : BasePoco
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
        [Display(Name = "批次")]
        [StringLength(100, ErrorMessage = "批次长度不能超出100字符")]
		public string batchNo { get; set; }
		
		/// <summary>
		/// 当前托盘号
		/// </summary>
		[Column("CUR_PALLET_BARCODE")]
        [Display(Name = "当前托盘号")]
        [StringLength(100, ErrorMessage = "当前托盘号长度不能超出100字符")]
		public string curPalletBarcode { get; set; }
		
		/// <summary>
		/// 当前位置编号：在容器中的位置
		/// </summary>
		[Column("CUR_POSITION_NO")]
        [Display(Name = "当前位置编号")]
        [StringLength(100, ErrorMessage = "当前位置编号长度不能超出100字符")]
		public string curPositionNo { get; set; }
		
		/// <summary>
		/// 当前库存编码
		/// </summary>
		[Column("CUR_STOCK_CODE")]
        [Display(Name = "当前库存编码")]
        [StringLength(100, ErrorMessage = "当前库存编码长度不能超出100字符")]
		public string curStockCode { get; set; }
		
		/// <summary>
		/// 当前库存明细ID
		/// </summary>
		[Column("CUR_STOCK_DTL_ID")]
        [Display(Name = "当前库存明细ID")]
        [StringLength(100, ErrorMessage = "当前库存明细ID长度不能超出100字符")]
		public string curStockDtlId { get; set; }

		/// <summary>
		/// DATACODE
		/// </summary>
		[Column("DATA_CODE")]
        [Display(Name = "DC")]
        [StringLength(100, ErrorMessage = "DATACODE长度不能超出100字符")]
		public string dataCode { get; set; }

		///// <summary>
		///// 删除标志;0-有效,1-已删除
		///// </summary>
		//[Column("DEL_FLAG")]
		//[StringLength(50, ErrorMessage = "删除标志;0-有效,1-已删除长度不能超出50字符")]
		//public string delFlag { get; set; } = "0";

		///// <summary>
		///// 区域
		///// </summary>
		//[Column("AREA_NO")]
  //      [Display(Name = "楼号")]
  //      //[Required]
  //      [StringLength(100, ErrorMessage = "区域长度不能超出100字符")]
		//public string areaNo { get; set; }

		/// <summary>
		/// ERP仓库
		/// </summary>
		[Column("ERP_WHOUSE_NO")]
        [Display(Name = "ERP仓库")]
        //[Required]
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
		/// 扩展字段15------作为备注说明
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
		/// 外部入库单行号
		/// </summary>
		[Column("EXTERNAL_IN_DTL_ID")]
        [Display(Name = "外部入库单行号")]
        [StringLength(100, ErrorMessage = "外部入库单行号长度不能超出100字符")]
		public string externalInDtlId { get; set; }

		/// <summary>
		/// 外部入库单号
		/// </summary>
		[Column("EXTERNAL_IN_NO")]
        [Display(Name = "外部入库单号")]
        [StringLength(100, ErrorMessage = "外部入库单号长度不能超出100字符")]
		public string externalInNo { get; set; }

		/// <summary>
		/// 入库单明细ID
		/// </summary>
		[Column("IN_DTL_ID")]
		//[Required]
		[StringLength(36, ErrorMessage = "入库单明细ID长度不能超出36字符")]
		public string inDtlId { get; set; }

		/// <summary>
		/// 入库单号
		/// </summary>
		[Column("IN_NO")]
        [Display(Name = "WMS入库单号")]
        //[Required]
        [StringLength(100, ErrorMessage = "入库单号长度不能超出100字符")]
		public string inNo { get; set; }

		/// <summary>
		/// 检验单号
		/// </summary>
		[Column("IQC_RESULT_NO")]
        [Display(Name = "WMS检验单号")]
        [StringLength(100, ErrorMessage = "检验单号长度不能超出100字符")]
		public string iqcResultNo { get; set; }

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
		///// 载体条码
		///// </summary>
		//[Column("PALLET_BARCODE")]
  //      [Display(Name = "载体条码")]
  //      [StringLength(100, ErrorMessage = "载体条码长度不能超出100字符")]
		//public string palletBarcode { get; set; }

		///// <summary>
		///// 所在载体位置
		///// </summary>
		//[Column("POSITION_NO")]
		//[StringLength(100, ErrorMessage = "所在载体位置长度不能超出100字符")]
		//public string positionNo { get; set; }

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
		/// 收货单明细ID
		/// </summary>
		[Column("RECEIPT_DTL_ID")]
		[StringLength(36, ErrorMessage = "收货单明细ID长度不能超出36字符")]
		public string receiptDtlId { get; set; }

		/// <summary>
		/// 收货单编号
		/// </summary>
		[Column("RECEIPT_NO")]
        [Display(Name = "WMS收货单号")]
        [StringLength(100, ErrorMessage = "收货单编号长度不能超出100字符")]
		public string receiptNo { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		//[Column("RECEIPT_QTY", TypeName = "decimal(18,3)")]
		[Column("QTY", TypeName = "decimal(18,3)")]
        [Display(Name = "数量")]
        [Required]
		//public decimal?  receiptQty { get; set; }
		public decimal?  qty { get; set; }

        /// <summary>
        /// 实际上架数量
        /// </summary>
        [Column("RECORD_QTY", TypeName = "decimal(18,3)")]
        [Display(Name = "实际上架数量")]
        //[Required]
        public decimal? recordQty { get; set; }
        /// <summary>
        /// 入库记录ID
        /// </summary>
        [Column("RECEIPT_RECORD_ID")]
		[StringLength(36, ErrorMessage = "入库记录ID长度不能超出36字符")]
		public string receiptRecordId { get; set; }

		///// <summary>
		///// 库存编码
		///// </summary>
		//[Column("STOCK_CODE")]
		////[Required]//入库单创建时无库存，收货操作后有暂存记录保存库存编码
		//[StringLength(100, ErrorMessage = "库存编码长度不能超出100字符")]
		//public string stockCode { get; set; }

		///// <summary>
		///// 库存明细ID
		///// </summary>
		//[Column("STOCK_DTL_ID")]
  //      //[Required]//入库单创建时无库存，收货操作后有暂存记录保存库存明细ID
  //      [StringLength(36, ErrorMessage = "库存明细ID长度不能超出36字符")]
		//public string stockDtlId { get; set; }

		/// <summary>
		/// 供应商暴露时长
		/// </summary>
		[Column("SUPPLIER_EXPOSE_TIME_DURATION", TypeName = "decimal(18,3)")]
        [Display(Name = "供应商暴露时长")]
        public decimal?  supplierExposeTimeDuration { get; set; }

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
		/// 供应商编码
		/// </summary>
		[Column("SUPPLIER_CODE")]
        [Display(Name = "供应商编码")]
        [StringLength(100, ErrorMessage = "供应商编码长度不能超出100字符")]
		public string supplierCode { get; set; }
		
		/// <summary>
		/// SKU
		/// </summary>
		[Column("SKU_CODE")]
        [Display(Name = "SKU")]
        [StringLength(100, ErrorMessage = "SKU长度不能超出100字符")]
		public string skuCode { get; set; }

		/// <summary>
		/// 唯一码
		/// </summary>
		[Column("UNIICODE")]
        [Display(Name = "包装条码/SN号")]
        [Required]
		[StringLength(100, ErrorMessage = "唯一码长度不能超出100字符")]
		public string uniicode { get; set; }
		
		/// <summary>
		/// 单位
		/// </summary>
		[Column("UNIT_CODE")]
        [Display(Name = "单位")]
        [Required]
		[StringLength(100, ErrorMessage = "单位长度不能超出100字符")]
		public string unitCode { get; set; }

		/// <summary>
		/// 仓库号
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
		public string whouseNo { get; set; }


		/// <summary>
		/// 入库唯一码状态 0:初始创建; 10:已收货；20：已组盘；90：已入库；92：删除
		/// </summary>
		[Column("RUNII_STATUS")]
        [Display(Name = "入库唯一码状态")]
        [Required]
		//[StringLength(50, ErrorMessage = "仓库号长度不能超出100字符")]
		public int runiiStatus { get; set; } = 0;


	}
}
