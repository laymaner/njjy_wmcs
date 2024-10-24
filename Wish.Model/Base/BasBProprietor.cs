using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Base
{
    [Table("BAS_B_PROPRIETOR")]
    [Index(nameof(proprietorCode), IsUnique = true)]
    public class BasBProprietor : BasePoco
	{
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }


        /// <summary>
        /// 地址
        /// </summary>
        [Column("ADDRESS")]
		[StringLength(200, ErrorMessage = "地址长度不能超出200字符")]
		public string address { get; set; }

		/// <summary>
		/// 联系人
		/// </summary>
		[Column("CONTACTS")]
		[StringLength(100, ErrorMessage = "联系人长度不能超出100字符")]
		public string contacts { get; set; }

		/// <summary>
		/// 描述
		/// </summary>
		[Column("DESCRIPTION")]
		[StringLength(200, ErrorMessage = "描述长度不能超出200字符")]
		public string description { get; set; }

		/// <summary>
		/// 传真
		/// </summary>
		[Column("FAX")]
		[StringLength(100, ErrorMessage = "传真长度不能超出100字符")]
		public string fax { get; set; }

		/// <summary>
		/// 邮箱
		/// </summary>
		[Column("MAIL")]
		[StringLength(100, ErrorMessage = "邮箱长度不能超出100字符")]
		public string mail { get; set; }

		/// <summary>
		/// 电话
		/// </summary>
		[Column("MOBILE")]
		[StringLength(100, ErrorMessage = "电话长度不能超出100字符")]
		public string mobile { get; set; }

		/// <summary>
		/// 手机
		/// </summary>
		[Column("PHONE")]
		[StringLength(100, ErrorMessage = "手机长度不能超出100字符")]
		public string phone { get; set; }

		/// <summary>
		/// 货主编码
		/// </summary>
		[Column("PROPRIETOR_CODE")]
		[Required]
		[StringLength(100, ErrorMessage = "货主编码长度不能超出100字符")]
		public string proprietorCode { get; set; }

		/// <summary>
		/// 货主全称
		/// </summary>
		[Column("PROPRIETOR_FULLNAME")]
		[StringLength(200, ErrorMessage = "货主全称长度不能超出200字符")]
		public string proprietorFullname { get; set; }

		/// <summary>
		/// 货主全称-其他
		/// </summary>
		[Column("PROPRIETOR_FULLNAME_ALIAS")]
		[StringLength(200, ErrorMessage = "货主全称-其他长度不能超出200字符")]
		public string proprietorFullnameAlias { get; set; }

		/// <summary>
		/// 货主全称-英文
		/// </summary>
		[Column("PROPRIETOR_FULLNAME_EN")]
		[StringLength(200, ErrorMessage = "货主全称-英文长度不能超出200字符")]
		public string proprietorFullnameEn { get; set; }

		/// <summary>
		/// 货主简称
		/// </summary>
		[Column("PROPRIETOR_NAME")]
		[Required]
		[StringLength(500, ErrorMessage = "货主简称长度不能超出500字符")]
		public string proprietorName { get; set; }

		/// <summary>
		/// 货主简称-其他
		/// </summary>
		[Column("PROPRIETOR_NAME_ALIAS")]
		[StringLength(500, ErrorMessage = "货主简称-其他长度不能超出500字符")]
		public string proprietorNameAlias { get; set; }

		/// <summary>
		/// 货主简称-英文
		/// </summary>
		[Column("PROPRIETOR_NAME_EN")]
		[StringLength(500, ErrorMessage = "货主简称-英文长度不能超出500字符")]
		public string proprietorNameEn { get; set; }

		/// <summary>
		/// 使用标识  0：禁用；1：启用
		/// </summary>
		[Column("USED_FLAG")]
		[Required]
		public int? usedFlag { get; set; } = 1;

		/// <summary>
		/// 邮编
		/// </summary>
		[Column("ZIP")]
		[StringLength(100, ErrorMessage = "邮编长度不能超出100字符")]
		public string zip { get; set; }


	}
}
