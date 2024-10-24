using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Model.System
{
    [Table("SYS_PARAMETER_VALUE")]
	public class SysParameterValue : BasePoco
	{
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int32 ID { get; set; }

        /// <summary>
        /// 参数编码
        /// </summary>
        [Column("PAR_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "参数编码长度不能超出100字符")]
		public string parCode { get; set; }

		/// <summary>
		/// 参数值
		/// </summary>
		[Column("PAR_VALUE_DESC")]
		[StringLength(100, ErrorMessage = "参数值长度不能超出100字符")]
		public string parValueDesc { get; set; }

		/// <summary>
		/// 参数值-其他
		/// </summary>
		[Column("PAR_VALUE_DESC_ALIAS")]
		[StringLength(100, ErrorMessage = "参数值-其他长度不能超出100字符")]
		public string parValueDescAlias { get; set; }

		/// <summary>
		/// 参数值-英文
		/// </summary>
		[Column("PAR_VALUE_DESC_EN")]
		[StringLength(100, ErrorMessage = "参数值-英文长度不能超出100字符")]
		public string parValueDescEn { get; set; }

		/// <summary>
		/// 对应参数
		/// </summary>
		[Column("PAR_VALUE_NO")]
		[Required]
		public int parValueNo { get; set; }

	}
}
