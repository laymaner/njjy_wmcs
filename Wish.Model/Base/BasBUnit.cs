using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Base
{
    [Table("BAS_B_UNIT")]
    [Index(nameof(unitCode), IsUnique = true)]
    public class BasBUnit : BasePoco
	{

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 单位编码
        /// </summary>
        [Column("UNIT_CODE")]
		[Required]
		[StringLength(100, ErrorMessage = "单位编码长度不能超出100字符")]
		public string unitCode { get; set; }

		/// <summary>
		/// 单位名称
		/// </summary>
		[Column("UNIT_NAME")]
		[Required]
		[StringLength(100, ErrorMessage = "单位名称长度不能超出100字符")]
		public string unitName { get; set; }

		/// <summary>
		/// 单位名称-其他
		/// </summary>
		[Column("UNIT_NAME_ALIAS")]
		[StringLength(100, ErrorMessage = "单位名称-其他长度不能超出100字符")]
		public string unitNameAlias { get; set; }

		/// <summary>
		/// 单位名称-英文
		/// </summary>
		[Column("UNIT_NAME_EN")]
		[StringLength(100, ErrorMessage = "单位名称-英文长度不能超出100字符")]
		public string unitNameEn { get; set; }

		/// <summary>
		/// 单位类型
		/// </summary>
		[Column("UNIT_TYPE")]
		[StringLength(50, ErrorMessage = "单位类型长度不能超出50字符")]
		public string unitType { get; set; }

		/// <summary>
		/// 使用标识  0：禁用；1：启用
		/// </summary>
		[Column("USED_FLAG")]
		[Required]
		public int usedFlag { get; set; } = 1;

		/// <summary>
		/// 仓库编码
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库编码长度不能超出100字符")]
		public string whouseNo { get; set; }


	}
}
