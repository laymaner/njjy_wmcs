using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Base
{
    [Table("BAS_B_MSL_MAINTAIN")]
    [Index(nameof(mslGradeCode), IsUnique = true)]
    public class BasBMslMaintain : BasePoco
	{
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Column("DESCRIPTION")]
		[StringLength(200, ErrorMessage = "描述长度不能超出200字符")]
		public string description { get; set; }

		/// <summary>
		/// 暴露时长预警(默认8小时)
		/// </summary>
		[Column("EXPOSE_TIME_WARN", TypeName = "decimal(18,3)")]
		public decimal? exposeTimeWarn { get; set; }

		/// <summary>
		/// 暴露时长上限
		/// </summary>
		[Column("MAX_EXPOSE_TIMES", TypeName = "decimal(18,3)")]
		public decimal? maxExposeTimes { get; set; }

		/// <summary>
		/// MSL等级编码
		/// </summary>
		[Column("MSL_GRADE_CODE")]
		[Required]
		[StringLength(100, ErrorMessage = "MSL等级编码长度不能超出100字符")]
		public string mslGradeCode { get; set; }

		/// <summary>
		/// MSL等级名称
		/// </summary>
		[Column("MSL_GRADE_NAME")]
		[Required]
		[StringLength(500, ErrorMessage = "MSL等级名称长度不能超出500字符")]
		public string mslGradeName { get; set; }

		/// <summary>
		/// MSL等级名称-其他
		/// </summary>
		[Column("MSL_GRADE_NAME_ALIAS")]
		[StringLength(500, ErrorMessage = "MSL等级名称-其他长度不能超出500字符")]
		public string mslGradeNameAlias { get; set; }

		/// <summary>
		/// MSL等级名称-英文
		/// </summary>
		[Column("MSL_GRADE_NAME_EN")]
		[StringLength(500, ErrorMessage = "MSL等级名称-英文长度不能超出500字符")]
		public string mslGradeNameEn { get; set; }

		/// <summary>
		/// 使用标识  0：禁用；1：启用
		/// </summary>
		[Column("USED_FLAG")]
		[Required]
		public int usedFlag { get; set; } = 1;


	}
}
