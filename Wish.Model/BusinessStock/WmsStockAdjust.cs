using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_STOCK_ADJUST")]
	public class WmsStockAdjust : BasePoco
	{
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 调整说明
        /// </summary>
        [Column("ADJUST_DESC")]
        [Display(Name = "调整说明")]
        [StringLength(1000, ErrorMessage = "调整说明长度不能超出1000字符")]
		public string adjustDesc { get; set; }

		/// <summary>
		/// 调整操作
		/// </summary>
		[Column("ADJUST_OPERATE")]
		[Required]
        [Display(Name = "调整操作")]
        [StringLength(100, ErrorMessage = "调整操作长度不能超出100字符")]
		public string adjustOperate { get; set; }

		/// <summary>
		/// 调整类型;新增、修改、删除
		/// </summary>
		[Column("ADJUST_TYPE")]
		[Required]
        [Display(Name = "调整类型")]
        [StringLength(50, ErrorMessage = "调整类型;新增、修改、删除长度不能超出50字符")]
		public string adjustType { get; set; }

        /// <summary>
        /// 包装条码
        /// </summary>
        [Display(Name = "包装条码")]
        [Column("PACKAGE_BARCODE")]
		[StringLength(100, ErrorMessage = "包装条码长度不能超出100字符")]
		public string packageBarcode { get; set; }

		/// <summary>
		/// 载体条码
		/// </summary>
		[Column("PALLET_BARCODE")]
        [Display(Name = "载体条码")]
        [Required]
		[StringLength(100, ErrorMessage = "载体条码长度不能超出100字符")]
		public string palletBarcode { get; set; }

		/// <summary>
		/// 货主
		/// </summary>
		[Column("PROPRIETOR_CODE")]
		[StringLength(100, ErrorMessage = "货主长度不能超出100字符")]
		public string proprietorCode { get; set; }

		/// <summary>
		/// 库存编码
		/// </summary>
		[Column("STOCK_CODE")]
        [Display(Name = "库存编码")]
        [StringLength(100, ErrorMessage = "库存编码长度不能超出100字符")]
		public string stockCode { get; set; }

		/// <summary>
		/// 仓库号
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库号长度不能超出100字符")]
		public string whouseNo { get; set; }


	}
}
