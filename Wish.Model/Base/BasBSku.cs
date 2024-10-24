using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Base
{
    [Table("BAS_B_SKU")]
    [Index(nameof(skuCode), IsUnique = true)]
    public class BasBSku : BasePoco
	{
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }


        /// <summary>
        /// 拣选规则编码
        /// </summary>
        [Column("PICK_RULE_NO")]
		[StringLength(100, ErrorMessage = "拣选规则编码长度不能超出100字符")]
		public string pickRuleNo { get; set; }

		/// <summary>
		/// 业主编码
		/// </summary>
		[Column("PROPRIETOR_CODE")]
		[StringLength(100, ErrorMessage = "业主编码长度不能超出100字符")]
		public string proprietorCode { get; set; }

		/// <summary>
		/// SKU编码
		/// </summary>
		[Column("SKU_CODE")]
		[Required]
		[StringLength(500, ErrorMessage = "SKU编码长度不能超出500字符")]
		public string skuCode { get; set; }

		/// <summary>
		/// 拣选策略编码
		/// </summary>
		[Column("SKU_RULES_NO")]
		[StringLength(100, ErrorMessage = "拣选策略编码长度不能超出100字符")]
		public string skuRulesNo { get; set; }

		/// <summary>
		/// 存储规则编码
		/// </summary>
		[Column("STORAGE_RULE_NO")]
		[StringLength(100, ErrorMessage = "存储规则编码长度不能超出100字符")]
		public string storageRuleNo { get; set; }

		/// <summary>
		/// 仓库编码
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库编码长度不能超出100字符")]
		public string whouseNo { get; set; }


	}
}
