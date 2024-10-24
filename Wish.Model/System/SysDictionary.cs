using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Model.System
{
    [Table("SYS_DICTIONARY")]
	[Index(nameof(dictionaryCode),nameof(dictionaryItemCode),IsUnique =true)]
	public class SysDictionary : BasePoco
	{
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int32 ID { get; set; }


        /// <summary>
        /// 仅限开发人员标志。 0：所有人都可见； 1：超级管理员账号可见；
        /// </summary>
        [Column("DEVELOP_FLAG")]
		public int developFlag { get; set; }

		/// <summary>
		/// 字典编码
		/// </summary>
		[Column("DICTIONARY_CODE")]
		[Required]
		[StringLength(100, ErrorMessage = "字典编码长度不能超出100字符")]
		public string dictionaryCode { get; set; }

		/// <summary>
		/// 字典项代码
		/// </summary>
		[Column("DICTIONARY_ITEM_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "字典项代码长度不能超出100字符")]
		public string dictionaryItemCode { get; set; }

		/// <summary>
		/// 字典项名称
		/// </summary>
		[Column("DICTIONARY_ITEM_NAME")]
        [Required]
        [StringLength(500, ErrorMessage = "字典项名称长度不能超出500字符")]
		public string dictionaryItemName { get; set; }

		/// <summary>
		/// 字典项名称-其他
		/// </summary>
		[Column("DICTIONARY_ITEM_NAME_ALIAS")]
		[StringLength(500, ErrorMessage = "字典项名称-其他长度不能超出500字符")]
		public string dictionaryItemNameAlias { get; set; }

		/// <summary>
		/// 字典项名称-英文
		/// </summary>
		[Column("DICTIONARY_ITEM_NAME_EN")]
		[StringLength(500, ErrorMessage = "字典项名称-英文长度不能超出500字符")]
		public string dictionaryItemNameEn { get; set; }

		/// <summary>
		/// 字典名称
		/// </summary>
		[Column("DICTIONARY_NAME")]
        [Required]
        [StringLength(500, ErrorMessage = "字典名称长度不能超出500字符")]
		public string dictionaryName { get; set; }

		/// <summary>
		/// 字典名称-其他
		/// </summary>
		[Column("DICTIONARY_NAME_ALIAS")]
		[StringLength(500, ErrorMessage = "字典名称-其他长度不能超出500字符")]
		public string dictionaryNameAlias { get; set; }

		/// <summary>
		/// 字典名称-英文
		/// </summary>
		[Column("DICTIONARY_NAME_EN")]
		[StringLength(500, ErrorMessage = "字典名称-英文长度不能超出500字符")]
		public string dictionaryNameEn { get; set; }

		/// <summary>
		/// 使用标志。0：停用；1：启用；
		/// </summary>
		[Column("USED_FLAG")]
		[Required]
		public int usedFlag { get; set; } = 1;


	}
}
