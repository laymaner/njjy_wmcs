using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Base
{
    [Table("BAS_B_SUPPLIER_BIN")]
	public class BasBSupplierBin : BasePoco
	{
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        [Column("AREA_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "区域编码长度不能超出100字符")]
		public string areaNo { get; set; }

		/// <summary>
		/// 库位编码
		/// </summary>
		[Column("BIN_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "库位编码长度不能超出100字符")]
		public string binNo { get; set; }

		/// <summary>
		/// 库区编码
		/// </summary>
		[Column("REGION_NO")]
		[Required]
		[StringLength(100, ErrorMessage = "库区编码长度不能超出100字符")]
		public string regionNo { get; set; }

		/// <summary>
		/// 供应商编码
		/// </summary>
		[Column("SUPPLIER_CODE")]
		[Required]
		[StringLength(100, ErrorMessage = "供应商编码长度不能超出100字符")]
		public string supplierCode { get; set; }

		/// <summary>
		/// 仓库编码
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库编码长度不能超出100字符")]
		public string whouseNo { get; set; }

        /// <summary>
		/// ERP仓库编码
		/// </summary>
		[Column("ERP_WHOUSE_NO")]
        [StringLength(200, ErrorMessage = "ERP仓库编码长度不能超出100字符")]
        public string erpWhouseNo { get; set; }
    }
}
