using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Base
{
    [Table("BAS_B_MATERIAL_CATEGORY")]
    [Index(nameof(materialCategoryCode), IsUnique = true)]
    public class BasBMaterialCategory : BasePoco
	{

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 延期天数
        /// </summary>
        [Column("DELAY_DAYS")]
		[Required]
		public int? delayDays { get; set; } = 0;

		/// <summary>
		/// 电子料标记：0不是，1是(不使用)
		/// </summary>
		//[Column("ELECTRONIC_MATERIAL_FLAG")]
		//[Required]
		//public int? electronicMaterialFlag { get; set; } = 0;

		/// <summary>
		///物料标记：1=成品/2=电子料/3=其他
		/// </summary>
		[Column("MATERIAL_FLAG")]
		[Required]
		[StringLength(50, ErrorMessage = "物料标记长度不能超出20字符")]
		public string? materialFlag { get; set; } = "1";

		/// <summary>
		/// 是否自动延期
		/// </summary>
		[Column("IS_AUTO_DELAY")]
		[Required]
		public int? isAutoDelay { get; set; } = 0;

		/// <summary>
		/// 物料大类描述
		/// </summary>
		[Column("MATERIAL_CATEGORY_DESC")]
		[StringLength(200, ErrorMessage = "物料大类描述长度不能超出200字符")]
		public string materialCategoryDesc { get; set; }

		/// <summary>
		/// 物料大类名称
		/// </summary>
		[Column("MATERIAL_CATEGORY_NAME")]
		[Required]
		[StringLength(500, ErrorMessage = "物料大类名称长度不能超出500字符")]
		public string materialCategoryName { get; set; }

		/// <summary>
		/// 物料大类名称-其他
		/// </summary>
		[Column("MATERIAL_CATEGORY_NAME_ALIAS")]
		[StringLength(500, ErrorMessage = "物料大类名称-其他长度不能超出500字符")]
		public string materialCategoryNameAlias { get; set; }

		/// <summary>
		/// 物料大类名称-英文
		/// </summary>
		[Column("MATERIAL_CATEGORY_NAME_EN")]
		[StringLength(500, ErrorMessage = "物料大类名称-英文长度不能超出500字符")]
		public string materialCategoryNameEn { get; set; }

		/// <summary>
		/// 物料大类编码
		/// </summary>
		[Column("MATERIAL_CATEGORY_CODE")]
		[Required]
		[StringLength(100, ErrorMessage = "物料大类编码长度不能超出100字符")]
		public string materialCategoryCode { get; set; }

		/// <summary>
		/// 货主编码
		/// </summary>
		[Column("PROPRIETOR_CODE")]
		[StringLength(100, ErrorMessage = "货主编码长度不能超出100字符")]
		public string proprietorCode { get; set; }

		/// <summary>
		/// 使用标识  0：禁用；1：启用
		/// </summary>
		[Column("USED_FLAG")]
		[Required]
		public int usedFlag { get; set; } = 1;

		/// <summary>
		/// 虚拟标记：0不是，1是
		/// </summary>
		[Column("VIRTUAL_FLAG")]
		[Required]
		public int? virtualFlag { get; set; } = 0;

		/// <summary>
		/// 仓库编码
		/// </summary>
		[Column("WHOUSE_NO")]
		[StringLength(100, ErrorMessage = "仓库编码长度不能超出100字符")]
		public string whouseNo { get; set; }


	}
}
