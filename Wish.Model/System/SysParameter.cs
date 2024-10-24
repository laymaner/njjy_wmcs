using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Model.System
{
    [Table("SYS_PARAMETER")]
    public class SysParameter : BasePoco
	{
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int32 ID { get; set; }

        /// <summary>
        /// 仅限开发人员标志，默认1，只有开发人员才能看见。0：所有人都能看见；1：只有开发人员能看见；
        /// </summary>
        [Column("DEVELOP_FLAG")]
		public int developFlag { get; set; }

		/// <summary>
		/// 参数编码
		/// </summary>
		[Column("PAR_CODE")]
		[Required]
		[StringLength(100, ErrorMessage = "参数编码长度不能超出100字符")]
		public string parCode { get; set; }

		/// <summary>
		/// 参数描述
		/// </summary>
		[Column("PAR_DESC")]
		[StringLength(500, ErrorMessage = "参数描述长度不能超出500字符")]
		public string parDesc { get; set; }

		/// <summary>
		/// 参数描述-其他
		/// </summary>
		[Column("PAR_DESC_ALIAS")]
		[StringLength(500, ErrorMessage = "参数描述-其他长度不能超出500字符")]
		public string parDescAlias { get; set; }

		/// <summary>
		/// 参数描述-英文
		/// </summary>
		[Column("PAR_DESC_EN")]
		[StringLength(500, ErrorMessage = "参数描述-英文长度不能超出500字符")]
		public string parDescEn { get; set; }

		/// <summary>
		/// 参数值
		/// </summary>
		[Column("PAR_VALUE")]
		[StringLength(100, ErrorMessage = "参数值长度不能超出100字符")]
		public string parValue { get; set; }

		/// <summary>
		/// 参数类型。1：下拉框选值；2：文本框填充；3：数字类型(添加录入校验)
		/// </summary>
		[Column("PAR_VALUE_TYPE")]
        [Required]
        public int? parValueType { get; set; }

	}
}
