using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_ITN_INVENTORY_DTL")]
	public class WmsItnInventoryDtl : BasePoco
	{
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [Column("BATCH_NO")]
        [Display(Name = "批次号")]
        [StringLength(100, ErrorMessage = "批次号长度不能超出100字符")]
		public string batchNo { get; set; }

		/// <summary>
		/// 盘点确认数量
		/// </summary>
		[Column("CONFIRM_QTY", TypeName = "decimal(18,3)")]
        [Display(Name = "确认数量")]
        [Required]
		public decimal?  confirmQty { get; set; }

		/// <summary>
		/// 是否差异
		/// </summary>
		[Column("DIF_FLAG")]
        [Display(Name = "是否差异")]
        [Required]
		public int?  difFlag { get; set; }

		///// <summary>
		///// 失效日期
		///// </summary>
		//[Column("EXP_DATE")]
		//public DateTime? expDate { get; set; }

		/// <summary>
		/// 质检结果
		/// </summary>
		[Column("INSPECTION_RESULT")]
		//[StringLength(50, ErrorMessage = "质检结果长度不能超出50字符")]
		//public string inspectionResult { get; set; }
		public int? inspectionResult { get; set; }

        /// <summary>
        /// 盘点明细状态：0：初始创建；51：盘点中；90：盘点完成；92：已删除；93：强制结束盘点；
        /// </summary>
        [Column("INVENTORY_DTL_STATUS")]
        [Display(Name = "盘点明细状态")]
        //[Required]
        //[StringLength(50, ErrorMessage = "盘点明细状态：0：初始创建；22：开始盘点(任务下发)；55：盘点中；99：盘点完成；100：强制结束盘点；111：已删除；长度不能超出50字符")]
        public int inventoryDtlStatus { get; set; } = 0;

		/// <summary>
		/// 盘点单号
		/// </summary>
		[Column("INVENTORY_NO")]
        [Display(Name = "盘点单号")]
        [Required]
		[StringLength(100, ErrorMessage = "盘点单号长度不能超出100字符")]
		public string inventoryNo { get; set; }

		/// <summary>
		/// 盘点操作数量
		/// </summary>
		[Column("INVENTORY_QTY", TypeName = "decimal(18,3)")]
        [Display(Name = "盘点数量")]
        [Required]
		public decimal?  inventoryQty { get; set; }

		/// <summary>
		/// 物料编码
		/// </summary>
		[Column("MATERIAL_CODE")]
        [Display(Name = "物料编码")]
        [Required]
		[StringLength(100, ErrorMessage = "物料编码长度不能超出100字符")]
		public string materialCode { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("MATERIAL_NAME")]
        [Display(Name = "物料名称")]
        [StringLength(500, ErrorMessage = "物料名称长度不能超出500字符")]
        public string materialName { get; set; }

        /// <summary>
        /// 物料规格
        /// </summary>
        [Column("MATERIAL_SPEC")]
        [Display(Name = "物料规格")]
        [StringLength(4000, ErrorMessage = "物料规格长度不能超出4000字符")]
		public string materialSpec { get; set; }

		///// <summary>
		///// 生产日期
		///// </summary>
		//[Column("PRODUCT_DATE")]
		//public DateTime? productDate { get; set; }

		/// <summary>
		/// 货主
		/// </summary>
		[Column("PROPRIETOR_CODE")]
		[StringLength(100, ErrorMessage = "货主长度不能超出100字符")]
		public string proprietorCode { get; set; }

		/// <summary>
		/// 盘点总数数量
		/// </summary>
		[Column("QTY", TypeName = "decimal(18,3)")]
        [Display(Name = "计划数量")]
        [Required]
		public decimal?  qty { get; set; }

		///// <summary>
		///// SKU编码
		///// </summary>
		//[Column("SKU_CODE")]
		//[StringLength(500, ErrorMessage = "SKU编码长度不能超出500字符")]
		//public string skuCode { get; set; }

		/// <summary>
		/// 单位编码
		/// </summary>
		[Column("UNIT_CODE")]
        [Display(Name = "单位")]
        [StringLength(100, ErrorMessage = "单位编码长度不能超出100字符")]
		public string unitCode { get; set; }

		/// <summary>
		/// 仓库号
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
		public string whouseNo { get; set; }


	}
}
