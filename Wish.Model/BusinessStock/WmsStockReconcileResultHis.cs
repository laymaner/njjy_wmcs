using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_STOCK_RECONCILE_RESULT_HIS")]
    [Index(nameof(reconcileNo), IsUnique = true)]
    public class WmsStockReconcileResultHis : BasePoco
	{
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 差异数量
        /// </summary>
        [Column("DIFFER_QTY", TypeName = "decimal(18,3)")]
		[Required]
		public decimal?  differQty { get; set; }

		/// <summary>
		/// ERP存货代码
		/// </summary>
		[Column("ERP_STOCK_NO")]
		[StringLength(100, ErrorMessage = "ERP存货代码长度不能超出100字符")]
		public string erpStockNo { get; set; }

		/// <summary>
		/// ERP库存数量
		/// </summary>
		[Column("ERP_STOCK_QTY", TypeName = "decimal(18,3)")]
		[Required]
		public decimal?  erpStockQty { get; set; }

		/// <summary>
		/// ERP仓库
		/// </summary>
		[Column("ERP_WHOUSE_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "ERP仓库长度不能超出100字符")]
		public string erpWhouseNo { get; set; }

		/// <summary>
		/// 物料大类编码
		/// </summary>
		[Column("MATERIAL_CATEGORY_NO")]
		[StringLength(100, ErrorMessage = "物料大类编码长度不能超出100字符")]
		public string materialCategoryCode { get; set; }

		/// <summary>
		/// 物料编码
		/// </summary>
		[Column("MATERIAL_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "物料编码长度不能超出100字符")]
		public string materialCode { get; set; }

		/// <summary>
		/// 物料类型编码
		/// </summary>
		[Column("MATERIAL_TYPE_NO")]
		[StringLength(100, ErrorMessage = "物料类型编码长度不能超出100字符")]
		public string materialTypeCode { get; set; }

		/// <summary>
		/// 货主
		/// </summary>
		[Column("PROPRIETOR_CODE")]
		[StringLength(100, ErrorMessage = "货主长度不能超出100字符")]
		public string proprietorCode { get; set; }

		/// <summary>
		/// 对账单号
		/// </summary>
		[Column("RECONCILE_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "对账单号长度不能超出100字符")]
		public string reconcileNo { get; set; }

		/// <summary>
		/// 对账人
		/// </summary>
		[Column("RECONCILE_OPERATOR")]
		[StringLength(100, ErrorMessage = "对账人长度不能超出100字符")]
		public string reconcileOperator { get; set; }

		/// <summary>
		/// 对账时间
		/// </summary>
		[Column("RECONCILE_TIME")]
		public DateTime? reconcileTime { get; set; }

		/// <summary>
		/// 仓库号
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
		public string whouseNo { get; set; }

		/// <summary>
		/// WMS库存数量
		/// </summary>
		[Column("WMS_STOCK_QTY", TypeName = "decimal(18,3)")]
		[Required]
		public decimal?  wmsStockQty { get; set; }


	}
}
