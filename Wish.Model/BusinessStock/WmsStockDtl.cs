using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_STOCK_DTL")]
	public partial class WmsStockDtl : BasePoco
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

		///// <summary>
		///// 批次
		///// </summary>
		//[Column("BATCH_NO")]
  //      [Display(Name = "批次")]
  //      [StringLength(100, ErrorMessage = "批次长度不能超出100字符")]
		//public string batchNo { get; set; }

		///// <summary>
		///// 删除标志;0-有效,1-已删除
		///// </summary>
		//[Column("DEL_FLAG")]
		//[StringLength(50, ErrorMessage = "删除标志;0-有效,1-已删除长度不能超出50字符")]
		//public string delFlag { get; set; } = "0";

		/// <summary>
		/// ERP仓库
		/// </summary>
		[Column("ERP_WHOUSE_NO")]
        [Display(Name = "ERP仓库")]
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
        /// 芯片尺寸
        /// </summary>
        [Column("CHIP_SIZE")]
        [StringLength(200, ErrorMessage = "芯片尺寸长度不能超出200字符")]
        public string chipSize { get; set; }

        /// <summary>
        /// 芯片厚度
        /// </summary>
        [Column("CHIP_THICKNESS")]
        [StringLength(200, ErrorMessage = "芯片厚度长度不能超出200字符")]
        public string chipThickness { get; set; }

        /// <summary>
        /// 芯片型号
        /// </summary>
        [Column("CHIP_MODEL")]
        [StringLength(200, ErrorMessage = "芯片型号长度不能超出200字符")]
        public string chipModel { get; set; }

        /// <summary>
        /// DAF型号
        /// </summary>
        [Column("DAF_TYPE")]
        [StringLength(200, ErrorMessage = "DAF型号长度不能超出200字符")]
        public string dafType { get; set; }


        /// <summary>
        /// 扩展字段2
        /// </summary>
        [Column("EXTEND2")]
		[StringLength(200, ErrorMessage = "扩展字段2长度不能超出200字符")]
		public string extend2 { get; set; }

		/// <summary>
		/// 扩展字段3---使用为收货明细ID
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
        [Display(Name = "质量标记")]
        //[StringLength(50, ErrorMessage = "质量标记;0：待检；1：合格；2：不合格；3：免检；4：冻结；长度不能超出50字符")]
		public int? inspectionResult { get; set; }
		
		/// <summary>
		/// 入库时间
		/// </summary>
		//[Column("INWH_TIME")]
        //[Display(Name = "入库时间")]
        //public DateTime? inwhTime { get; set; }

		/// <summary>
		/// 锁定状态;0：未锁定库存；1：已锁定库存。
		/// </summary>
		[Column("LOCK_FLAG")]
        [Display(Name = "锁定状态")]
        //[StringLength(50, ErrorMessage = "锁定状态;0：未锁定库存；1：已锁定库存。长度不能超出50字符")]
		public int? lockFlag { get; set; }

		/// <summary>
		/// 锁定原因
		/// </summary>
		[Column("LOCK_REASON")]
        [Display(Name = "锁定原因")]
        [StringLength(500, ErrorMessage = "锁定原因长度不能超出500字符")]
		public string lockReason { get; set; }

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
        [Display(Name = "规格型号")]
        [StringLength(4000, ErrorMessage = "规格型号长度不能超出4000字符")]
		public string materialSpec { get; set; }

		/// <summary>
		/// 占用数量
		/// </summary>
		[Column("OCCUPY_QTY", TypeName = "decimal(18,3)")]
        [Display(Name = "占用数量")]
        [Required]
		public decimal?  occupyQty { get; set; }

		/// <summary>
		/// 载体条码
		/// </summary>
		[Column("PALLET_BARCODE")]
        [Display(Name = "载体条码")]
        [Required]
		[StringLength(100, ErrorMessage = "载体条码长度不能超出100字符")]
		public string palletBarcode { get; set; }

		/// <summary>
		/// 项目号
		/// </summary>
		[Column("PROJECT_NO")]
        [Display(Name = "项目号")]
        [StringLength(100, ErrorMessage = "项目号长度不能超出100字符")]
		public string projectNo { get; set; }

        /// <summary>
        /// 备用项目号
        /// </summary>
        [Column("PROJECT_NO_BAK")]
        [Display(Name = "备用项目号")]
        [StringLength(100, ErrorMessage = "备用项目号长度不能超出100字符")]
        public string projectNoBak { get; set; }

        /// <summary>
        /// 货主
        /// </summary>
        [Column("PROPRIETOR_CODE")]
		[StringLength(100, ErrorMessage = "货主长度不能超出100字符")]
		public string proprietorCode { get; set; }

		/// <summary>
		/// 库存数量
		/// </summary>
		[Column("QTY", TypeName = "decimal(18,3)")]
        [Display(Name = "库存数量")]
        [Required]
		public decimal?  qty { get; set; }

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
		/// 库存明细状态;0：初始创建；20：入库中；50：在库；70：出库中；90：托盘出库完成(生命周期结束)；92删除（撤销）；93强制完成
		/// </summary>
		[Column("STOCK_DTL_STATUS")]
        [Display(Name = "库存明细状态")]
        [Required]
		//[StringLength(50, ErrorMessage = "库存明细状态;0：初始创建；20：入库中；50：在库；70：出库中；90：托盘出库完成(生命周期结束)；92删除（撤销）；93强制完成长度不能超出50字符")]
		public int stockDtlStatus { get; set; } = 0;

		/// <summary>
		/// 供应商编码
		/// </summary>
		[Column("SUPPLIER_CODE")]
        [Display(Name = "供应商编码")]
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
		/// 仓库号
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
		public string whouseNo { get; set; }

        ///// <summary>
        ///// 急料标记
        ///// </summary>
        //[Column("URGENT_FLAG")]
        //[Display(Name = "急料标记")]
        //[StringLength(50, ErrorMessage = "急料标记长度不能超出50字符")]
        //public string urgentFlag { get; set; }
		
		/// <summary>
        /// 单位
        /// </summary>
        [Column("UNIT_CODE")]
        [Display(Name = "单位")]
        [StringLength(50, ErrorMessage = "单位长度不能超出50字符")]
        public string unitCode { get; set; }
		
		///// <summary>
		///// 版本更新计数(当并发脏写时，该字段自动+1)
		///// </summary>
		//[Column("VERSION")]
		//[Display(Name = "版本计数")]
		////public int version { get; set; }
		
		///// <summary>
		///// 脏写计数(当并发脏写时，该字段自动+1)
		///// </summary>
		//[Column("VERSION_DIRTY")]
		//[Display(Name = "脏写计数")]
		//public int versionDirty { get; set; }
		
		///// <summary>
		///// 预留计数
		///// </summary>
		//[Column("VERSION_EXTEND1")]
		//[Display(Name = "预留计数")]
		//public int versionExtend1 { get; set; }
		
		///// <summary>
		///// 业务编码
		///// </summary>
		//[Column("BUSINESS_CODE")]
		//[Display(Name = "版本计数")]
		//[StringLength(100, ErrorMessage = "业务编码长度不能超出100字符")]
		//public string businessCode { get; set; }
    }
}
