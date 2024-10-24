using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.Config.Model
{
    [Table("CFG_STRATEGY_TYPE")]
    [Index(nameof(strategyTypeCode), IsUnique = true)]
    public class CfgStrategyType : BasePoco
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 策略类型分类：排序、选择
        /// </summary>
        [Column("STRATEGY_TYPE_CATEGORY")]
        [StringLength(100, ErrorMessage = "策略类型分类：排序、选择长度不能超出100字符")]
        public string strategyTypeCategory { get; set; }

        /// <summary>
        /// 策略类型编码
        /// </summary>
        [Column("STRATEGY_TYPE_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "策略类型编码长度不能超出100字符")]
        public string strategyTypeCode { get; set; }

        /// <summary>
        /// 策略类型说明
        /// </summary>
        [Column("STRATEGY_TYPE_DESRIPTION")]
        [StringLength(200, ErrorMessage = "策略类型说明长度不能超出200字符")]
        public string strategyTypeDesription { get; set; }

        /// <summary>
        /// 策略类型名称
        /// </summary>
        [Column("STRATEGY_TYPE_NAME")]
        [Required]
        [StringLength(500, ErrorMessage = "策略类型名称长度不能超出500字符")]
        public string strategyTypeName { get; set; }

        /// <summary>
        /// 策略类型名称-其他
        /// </summary>
        [Column("STRATEGY_TYPE_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "策略类型名称-其他长度不能超出500字符")]
        public string strategyTypeNameAlias { get; set; }

        /// <summary>
        /// 策略类型名称-英文
        /// </summary>
        [Column("STRATEGY_TYPE_NAME_EN")]
        [StringLength(500, ErrorMessage = "策略类型名称-英文长度不能超出500字符")]
        public string strategyTypeNameEn { get; set; }

        /// <summary>
        /// 使用标志(0：停用；1：启用)
        /// </summary>
        [Column("USED_FLAG")]
        [Required]
        public int usedFlag { get; set; } = 1;


    }
}
