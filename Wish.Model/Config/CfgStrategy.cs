using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.Config.Model
{
    [Table("CFG_STRATEGY")]
    [Index(nameof(strategyNo), IsUnique = true)]
    public class CfgStrategy : BasePoco
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 策略描述
        /// </summary>
        [Column("STRATEGY_DESC")]
        [StringLength(200, ErrorMessage = "策略描述长度不能超出200字符")]
        public string strategyDesc { get; set; }

        /// <summary>
        /// 策略名称
        /// </summary>
        [Column("STRATEGY_NAME")]
        //[Required]
        [StringLength(500, ErrorMessage = "策略名称长度不能超出500字符")]
        public string strategyName { get; set; }

        /// <summary>
        /// 策略名称-其他
        /// </summary>
        [Column("STRATEGY_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "策略名称-其他长度不能超出500字符")]
        public string strategyNameAlias { get; set; }

        /// <summary>
        /// 策略名称-英文
        /// </summary>
        [Column("STRATEGY_NAME_EN")]
        [StringLength(500, ErrorMessage = "策略名称-英文长度不能超出500字符")]
        public string strategyNameEn { get; set; }

        /// <summary>
        /// 策略编码
        /// </summary>
        [Column("STRATEGY_NO")]
        //[Required]
        [StringLength(100, ErrorMessage = "策略编码长度不能超出100字符")]
        public string strategyNo { get; set; }

        /// <summary>
        /// 策略类型编码
        /// </summary>
        [Column("STRATEGY_TYPE_CODE")]
        //[Required]
        [StringLength(100, ErrorMessage = "策略类型编码长度不能超出100字符")]
        public string strategyTypeCode { get; set; }

        /// <summary>
        /// 使用标志(0：停用；1：启用)
        /// </summary>
        [Column("USED_FLAG")]
        //[Required]
        public int usedFlag { get; set; } = 1;


    }
}
