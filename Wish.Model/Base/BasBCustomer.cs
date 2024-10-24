using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Base
{
    [Table("BAS_B_CUSTOMER")]
    [Index(nameof(CustomerCode), IsUnique = true)]
    public class BasBCustomer : BasePoco
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
		public string Address { get; set; }

		/// <summary>
		/// 联系人
		/// </summary>
		[Column("CONTACTS")]
		[StringLength(500, ErrorMessage = "联系人长度不能超出500字符")]
		public string Contacts { get; set; }

		/// <summary>
		/// 客户全称
		/// </summary>
		[Column("CUSTOMER_FULLNAME")]
		[StringLength(200, ErrorMessage = "客户全称长度不能超出200字符")]
		public string CustomerFullname { get; set; }

		/// <summary>
		/// 客户全称-其他
		/// </summary>
		[Column("CUSTOMER_FULLNAME_ALIAS")]
		[StringLength(200, ErrorMessage = "客户全称-其他长度不能超出200字符")]
		public string CustomerFullnameAlias { get; set; }

		/// <summary>
		/// 客户全称-英文
		/// </summary>
		[Column("CUSTOMER_FULLNAME_EN")]
		[StringLength(200, ErrorMessage = "客户全称-英文长度不能超出200字符")]
		public string CustomerFullnameEn { get; set; }

		/// <summary>
		/// 客户名称
		/// </summary>
		[Column("CUSTOMER_NAME")]
		[Required]
		[StringLength(500, ErrorMessage = "客户名称长度不能超出500字符")]
		public string CustomerName { get; set; }

		/// <summary>
		/// 客户简称-其他
		/// </summary>
		[Column("CUSTOMER_NAME_ALIAS")]
		[StringLength(500, ErrorMessage = "客户简称-其他长度不能超出500字符")]
		public string CustomerNameAlias { get; set; }

		/// <summary>
		/// 客户简称-英文
		/// </summary>
		[Column("CUSTOMER_NAME_EN")]
		[StringLength(500, ErrorMessage = "客户简称-英文长度不能超出500字符")]
		public string CustomerNameEn { get; set; }

		/// <summary>
		/// 客户编码
		/// </summary>
		[Column("CUSTOMER_CODE")]
		[Required]
		[StringLength(100, ErrorMessage = "客户编码长度不能超出100字符")]
		public string CustomerCode { get; set; }

		/// <summary>
		/// 描述
		/// </summary>
		[Column("DESCRIPTION")]
		[StringLength(200, ErrorMessage = "描述长度不能超出200字符")]
		public string Description { get; set; }

		/// <summary>
		/// 传真
		/// </summary>
		[Column("FAX")]
		[StringLength(100, ErrorMessage = "传真长度不能超出100字符")]
		public string Fax { get; set; }

		/// <summary>
		/// 邮箱
		/// </summary>
		[Column("MAIL")]
		[StringLength(100, ErrorMessage = "邮箱长度不能超出100字符")]
		public string Mail { get; set; }

		/// <summary>
		/// 电话
		/// </summary>
		[Column("MOBILE")]
		[StringLength(100, ErrorMessage = "电话长度不能超出100字符")]
		public string Mobile { get; set; }

		/// <summary>
		/// 手机
		/// </summary>
		[Column("PHONE")]
		[StringLength(100, ErrorMessage = "手机长度不能超出100字符")]
		public string Phone { get; set; }

		/// <summary>
		/// 货主编码
		/// </summary>
		[Column("PROPRIETOR_CODE")]
		[StringLength(100, ErrorMessage = "货主编码长度不能超出100字符")]
		public string ProprietorCode { get; set; }

		/// <summary>
		/// 使用标识  0：禁用；1：启用
		/// </summary>
		[Column("USED_FLAG")]
		[Required]
		public int UsedFlag { get; set; } = 1;

		/// <summary>
		/// 仓库编码
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库编码长度不能超出100字符")]
		public string WhouseNo { get; set; }

		/// <summary>
		/// 邮编
		/// </summary>
		[Column("ZIP")]
		[StringLength(100, ErrorMessage = "邮编长度不能超出100字符")]
		public string Zip { get; set; }


	}
}
