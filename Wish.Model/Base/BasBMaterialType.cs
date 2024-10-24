using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Base
{
    [Table("BAS_B_MATERIAL_TYPE")]
    [Index(nameof(materialTypeCode), IsUnique = true)]
    public class BasBMaterialType : BasePoco
	{
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        ///// <summary>
        ///// 物料大类编码
        ///// </summary>
        //[Column("MATERIAL_CATEGORY_NO")]
        //[StringLength(100, ErrorMessage = "物料大类编码长度不能超出100字符")]
        //public string materialCategoryCode { get; set; }

        /// <summary>
        /// 物料小类描述
        /// </summary>
        [Column("MATERIAL_TYPE_DESC")]
		[StringLength(500, ErrorMessage = "物料小类描述长度不能超出500字符")]
		public string materialTypeDesc { get; set; }

		/// <summary>
		/// 物料小类名称
		/// </summary>
		[Column("MATERIAL_TYPE_NAME")]
		[Required]
		[StringLength(500, ErrorMessage = "物料小类名称长度不能超出500字符")]
		public string materialTypeName { get; set; }

		/// <summary>
		/// 物料小类名称-其他
		/// </summary>
		[Column("MATERIAL_TYPE_NAME_ALIAS")]
		[StringLength(500, ErrorMessage = "物料小类名称-其他长度不能超出500字符")]
		public string materialTypeNameAlias { get; set; }

		/// <summary>
		/// 物料小类名称-英文
		/// </summary>
		[Column("MATERIAL_TYPE_NAME_EN")]
		[StringLength(500, ErrorMessage = "物料小类名称-英文长度不能超出500字符")]
		public string materialTypeNameEn { get; set; }

		/// <summary>
		/// 物料小类编码
		/// </summary>
		//[Column("MATERIAL_TYPE_NO")]
		[Column("MATERIAL_TYPE_CODE")]
		[Required]
		[StringLength(100, ErrorMessage = "物料小类编码长度不能超出100字符")]
		public string materialTypeCode { get; set; }

		/// <summary>
		/// 货主编码
		/// </summary>
		[Column("PROPRIETOR_CODE")]
		[StringLength(100, ErrorMessage = "货主编码长度不能超出100字符")]
		public string proprietorCode { get; set; }

        /// <summary>
        /// 默认拣选规则编码
        /// </summary>
  //      [Column("PICK_RULE_NO")]
		//[StringLength(100, ErrorMessage = "默认拣选规则编码长度不能超出100字符")]
		//public string pickRuleNo { get; set; }

        /// <summary>
        /// 默认存储规则编码
        /// </summary>
        //[Column("STORAGE_RULE_NO")]
        //[StringLength(100, ErrorMessage = "默认存储规则编码长度不能超出100字符")]
        //public string storageRuleNo { get; set; }

        /// <summary>
        /// 供方类型：''无供应商管理，S：供应商，P：产线
        /// </summary>
        //[Column("SUPPLIER_TYPE")]
        //[StringLength(100, ErrorMessage = "供方类型长度不能超出100字符")]
        //public string supplierType { get; set; }

        /// <summary>
        /// 整出拆包类型
        /// </summary>
  //      [Column("UNPACK_TYPE")]
  //      [StringLength(100, ErrorMessage = "整出拆包类型长度不能超出100字符")]
		//public string unpackType { get; set; }

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

        ///// <summary>
        ///// 是否自动延期
        ///// </summary>
        //[Column("IS_AUTO_DELAY")]
        //[Required]
        //public int? isAutoDelay { get; set; } = 0;

        ///// <summary>
        ///// 延期天数
        ///// </summary>
        //[Column("DELAY_DAYS")]
        //[Required]
        //public int? delayDays { get; set; } = 0;
    }
}
