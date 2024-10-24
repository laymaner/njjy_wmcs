using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_ITN_MOVE_RECORD")]
	public class WmsItnMoveRecord : BasePoco
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
        //[Column("AREA_NO")]
        //      [Display(Name = "楼号")]
        //      [Required]
        //[StringLength(100, ErrorMessage = "区域编码(楼号)长度不能超出100字符")]
        //public string areaNo { get; set; }

        /// <summary>
        /// 码级管理
        /// </summary>
        [Column("BARCODE_FLAG")]
        [Display(Name = "码级管理")]
		public int barcodeFlag { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        [Column("BATCH_NO")]
        [Display(Name = "批次")]
        [StringLength(100, ErrorMessage = "批次长度不能超出100字符")]
        public string batchNo { get; set; }

        /// <summary>
        /// 移库确认数量
        /// </summary>
        [Column("CONFIRM_QTY")]
        [Display(Name = "CONFIRM_QTY")]
		public decimal? confirmQty { get; set; }

        /// <summary>
        /// 当前站台号/库位号
        /// </summary>
        [Column("CUR_LOCATION_NO")]
        [StringLength(100, ErrorMessage = "当前站台号/库位号不能超出100字符")]
        public string curLocationNo { get; set; }

        /// <summary>
        /// 当前位置类型
        /// </summary>
        [Column("CUR_LOCATION_TYPE")]
		[StringLength(500, ErrorMessage = "当前位置类型长度不能超出500字符")]
		public string curLocationType { get; set; }

        /// <summary>
        /// 当前托盘条码
        /// </summary>
        [Column("CUR_PALLET_BARCODE")]
		[StringLength(500, ErrorMessage = "当前托盘条码长度不能超出500字符")]
		public string curPalletbarCode { get; set; }

        /// <summary>
        /// 当前库存编码
        /// </summary>
        [Column("CUR_STOCK_CODE")]
        [Display(Name = "当前库存编码")]
        public string curStockCode { get; set; }

        /// <summary>
        /// 当前库存明细ID
        /// </summary>
        [Column("CUR_STOCK_DTL_ID")]
        [Display(Name = "当前库存明细ID")]
        //public string curStockDtlId { get; set; }
        public Int64? curStockDtlId { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        [Column("DOC_TYPE_CODE")]
		[StringLength(50, ErrorMessage = "单据类型长度不能超出50字符")]
		public string docTypeCode { get; set; }

        /// <summary>
        /// 来源站台/库位号
        /// </summary>
        [Column("FR_LOCATION_NO")]
        [Display(Name = "来源站台/库位号")]
        public string frLocationNo { get; set; }

        /// <summary>
        /// 来源位置类型
        /// </summary>
        [Column("FR_LOCATION_TYPE")]
        [Display(Name = "来源位置类型")]
        [StringLength(100, ErrorMessage = "来源位置类型长度不能超出100字符")]
		public string frLocationType { get; set; }

        /// <summary>
        /// 来源托盘条码
        /// </summary>
        [Column("FR_PALLET_BARCODE")]
        [Display(Name = "来源托盘条码")]
        public string frPalletBarcode { get; set; }

        /// <summary>
        /// 移库来源库区
        /// </summary>
        [Column("FR_REGION_NO")]
		public string frRegionNo { get; set; }

        /// <summary>
        /// 来源库存编码
        /// </summary>
        [Column("FR_STOCK_CODE")]
		[StringLength(500, ErrorMessage = "来源库存编码长度不能超出500字符")]
		public string frStockCode { get; set; }

        /// <summary>
        /// 来源库存明细ID
        /// </summary>
        [Column("FR_STOCK_DTL_ID")]
		//[StringLength(200, ErrorMessage = "来源库存明细ID长度不能超出200字符")]
		//public string frStockDtlId { get; set; }
		public Int64? frStockDtlId { get; set; }

        /// <summary>
        /// 质量标记
        /// </summary>
        [Column("INSPECTION_RESULT")]
		//[StringLength(200, ErrorMessage = "质量标记长度不能超出200字符")]
		public int inspectionResult { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("MATERIAL_CODE")]
		[StringLength(200, ErrorMessage = "物料编码长度不能超出200字符")]
		public string materialCode { get; set; }

        /// <summary>
        /// 物料规格
        /// </summary>
        [Column("MATERIAL_SPEC")]
		[StringLength(200, ErrorMessage = "物料规格长度不能超出200字符")]
		public string materialSpec { get; set; }

        /// <summary>
        /// 移库单号
        /// </summary>
        [Column("MOVE_NO")]
        [StringLength(200, ErrorMessage = "移库单号长度不能超出200字符")]
        public string moveNo { get; set; }

        /// <summary>
        /// 移动单明细ID
        /// </summary>
        [Column("MOVE_DTL_ID")]
        //[StringLength(200, ErrorMessage = "移动单明细ID长度不能超出200字符")]
        //public string moveDtlId { get; set; }
        public Int64 moveDtlId { get; set; }
        /// <summary>
        /// 移库计划数量
        /// </summary>
        [Column("MOVE_QTY")]
		//[StringLength(200, ErrorMessage = "移库计划数量长度不能超出200字符")]
		public decimal? moveQty { get; set; }

        /// <summary>
        /// 移库记录状态：0	初始创建；31	下架中；39	下架完成；51	上架中；90	移库完成；92	删除；93	强制关闭
        /// </summary>
        [Column("MOVE_RECORD_STATUS")]
		//[StringLength(200, ErrorMessage = "移库记录状态长度不能超出200字符")]
		public int? moveRecordStatus { get; set; }

        /// <summary>
        /// 拣选方式
        /// </summary>
        [Column("PICK_METHOD")]
		[StringLength(200, ErrorMessage = "拣选方式长度不能超出200字符")]
		public string pickMethod { get; set; }

        /// <summary>
        /// 拣选类型
        /// </summary>
        [Column("PICK_TYPE")]
		[StringLength(200, ErrorMessage = "拣选类型长度不能超出200字符")]
		public string pickType { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        [Column("PRODUCT_DATE")]
		public DateTime? productDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        [Column("EXP_DATE")]
		public DateTime? expDate { get; set; }

        /// <summary>
        /// 移库下架站台
        /// </summary>
        [Column("PUTDOWN_LOC_NO")]
		[StringLength(200, ErrorMessage = "移库下架站台长度不能超出200字符")]
		public string putDownLocNo { get; set; }

        /// <summary>
        /// SKU编码
        /// </summary>
        [Column("SKU_CODE")]
		[StringLength(200, ErrorMessage = "SKU编码长度不能超出200字符")]
		public string skuCode { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        [Column("STOCK_QTY")]
		public decimal? stockQty { get; set; }

        /// <summary>
        /// 供应商批次
        /// </summary>
        [Column("SUPPLIER_BATCH_NO")]
		[StringLength(200, ErrorMessage = "供应商批次长度不能超出200字符")]
		public string supplierBatchNo { get; set; }

        /// <summary>
        /// 供应商编码
        /// </summary>
        [Column("SUPPLIER_CODE")]
		[StringLength(200, ErrorMessage = "供应商编码长度不能超出200字符")]
		public string supplierCode { get; set; }

		/// <summary>
		/// 移库前库位号
		/// </summary>
		[Column("SUPPLIER_NAME")]
        [Display(Name = "供应商名称")]
		[StringLength(100, ErrorMessage = "供应商名称长度不能超出100字符")]
		public string supplierName { get; set; }

        /// <summary>
        /// 供应商名称-EN
        /// </summary>
        [Column("SUPPLIER_NAME_EN")]
        [Display(Name = "供应商名称-EN")]
		[StringLength(100, ErrorMessage = "供应商名称-EN长度不能超出100字符")]
		public string supplierNameEn { get; set; }

        /// <summary>
        /// 供应商名称-ALIAS
        /// </summary>
        [Column("SUPPLIER_NAME_ALIAS")]
		[StringLength(50, ErrorMessage = "供应商名称-ALIAS长度不能超出50字符")]
		public string supplierNameAlias { get; set; }

        /// <summary>
        /// 供货方类型：供应商、产线
        /// </summary>
        [Column("SUPPLIER_TYPE")]
        [Display(Name = "供货方类型：供应商、产线")]
        public string supplierType { get; set; }

        /// <summary>
        /// 目标库位/站台号
        /// </summary>
        [Column("TO_LOCATION_NO")]
        [Display(Name = "目标库位/站台号")]
        public string toLocationNo { get; set; }

        /// <summary>
        /// 目标位置类型
        /// </summary>
        [Column("TO_LOCATION_TYPE")]
		[StringLength(50, ErrorMessage = "目标位置类型长度不能超出50字符")]
		public string toLocationType { get; set; }

        /// <summary>
        /// 目标托盘条码
        /// </summary>
        [Column("TO_PALLET_BARCODE")]
		[StringLength(500, ErrorMessage = "目标托盘条码长度不能超出500字符")]
		public string toPalletBarcode { get; set; }

        /// <summary>
        /// 目标库区
        /// </summary>
        [Column("TO_REGION_NO")]
        [Display(Name = "目标库区")]
        [StringLength(500, ErrorMessage = "目标库区长度不能超出500字符")]
		public string toRegionNo { get; set; }

        /// <summary>
        /// 目标库存编码
        /// </summary>
        [Column("TO_STOCK_CODE")]
        [Display(Name = "目标库存编码")]
        [Required]
		[StringLength(100, ErrorMessage = "目标库存编码长度不能超出100字符")]
		public string toStockCode { get; set; }

        /// <summary>
        /// 目标库存明细ID
        /// </summary>
        [Column("TO_STOCK_DTL_ID")]
        [Display(Name = "目标库存明细ID")]
        //[StringLength(4000, ErrorMessage = "目标库存明细ID长度不能超出4000字符")]
		//public string toStockDtlId { get; set; }
		public Int64? toStockDtlId { get; set; }

        /// <summary>
        /// 单位编码
        /// </summary>
        [Column("UNIT_CODE")]
		//[Required]
		[StringLength(36, ErrorMessage = "单位编码长度不能超出36字符")]
		public string unitCode { get; set; }

        /// <summary>
        /// 货主
        /// </summary>
        [Column("PROPRIETOR_CODE")]
        [Display(Name = "货主")]
        //[Required]
        [StringLength(100, ErrorMessage = "货主长度不能超出100字符")]
		public string proprietorCode { get; set; }

        /// <summary>
        /// 仓库号
        /// </summary>
        [Column("WHOUSE_NO")]
        [Display(Name = "仓库号")]
        [StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
		public string whouseNo { get; set; }


	}
}
