using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.Config.Model
{
    [Table("CFG_STRATEGY_ITEM")]
    [Index(nameof(strategyItemNo), IsUnique = true)]
    public class CfgStrategyItem : BasePoco
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 策略项描述
        /// </summary>
        [Column("STRATEGY_ITEM_DESC")]
        [StringLength(200, ErrorMessage = "策略项描述长度不能超出200字符")]
        public string strategyItemDesc { get; set; }

        /// <summary>
        /// 策略项组内序号
        /// </summary>
        [Column("STRATEGY_ITEM_GROUP_IDX")]
        [Required]
        [StringLength(100, ErrorMessage = "策略项组内序号长度不能超出100字符")]
        public string strategyItemGroupIdx { get; set; }

        /// <summary>
        /// 策略项分组
        /// </summary>
        [Column("STRATEGY_ITEM_GROUP_NO")]
        [StringLength(100, ErrorMessage = "策略项分组长度不能超出100字符")]
        public string strategyItemGroupNo { get; set; }

        /// <summary>
        /// 策略项名称
        /// </summary>
        [Column("STRATEGY_ITEM_NAME")]
        [Required]
        [StringLength(500, ErrorMessage = "策略项名称长度不能超出500字符")]
        public string strategyItemName { get; set; }

        /// <summary>
        /// 策略项名称-其他
        /// </summary>
        [Column("STRATEGY_ITEM_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "策略项名称-其他长度不能超出500字符")]
        public string strategyItemNameAlias { get; set; }

        /// <summary>
        /// 策略项名称-英文
        /// </summary>
        [Column("STRATEGY_ITEM_NAME_EN")]
        [StringLength(500, ErrorMessage = "策略项名称-英文长度不能超出500字符")]
        public string strategyItemNameEn { get; set; }

        /// <summary>
        /// 策略项编码
        /// </summary>
        [Column("STRATEGY_ITEM_NO")]
        [Required]
        [StringLength(100, ErrorMessage = "策略项编码长度不能超出100字符")]
        public string strategyItemNo { get; set; }

        /// <summary>
        /// 策略类型编码
        /// </summary>
        [Column("STRATEGY_TYPE_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "策略类型编码长度不能超出100字符")]
        public string strategyTypeCode { get; set; }

        /// <summary>
        /// 使用标志(0：停用；1：启用)
        /// </summary>
        [Column("USED_FLAG")]
        [Required]
        public int usedFlag { get; set; } = 1;


    }
}
