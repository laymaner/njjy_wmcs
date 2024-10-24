using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.Config.Model
{
    [Table("CFG_STRATEGY_DTL")]
    public class CfgStrategyDtl : BasePoco
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 策略项顺序
        /// </summary>
        [Column("STRATEGY_ITEM_IDX")]
        //[Required]
        [StringLength(20, ErrorMessage = "策略项顺序长度不能超出20字符")]
        public string strategyItemIdx { get; set; }

        /// <summary>
        /// 策略项编码
        /// </summary>
        [Column("STRATEGY_ITEM_NO")]
        //[Required]
        [StringLength(100, ErrorMessage = "策略项编码长度不能超出100字符")]
        public string strategyItemNo { get; set; }

        /// <summary>
        /// 策略项值1
        /// </summary>
        [Column("STRATEGY_ITEM_VALUE1")]
        [StringLength(100, ErrorMessage = "策略项值1长度不能超出100字符")]
        public string strategyItemValue1 { get; set; }

        /// <summary>
        /// 策略项值2
        /// </summary>
        [Column("STRATEGY_ITEM_VALUE2")]
        [StringLength(100, ErrorMessage = "策略项值2长度不能超出100字符")]
        public string strategyItemValue2 { get; set; }

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
        /// 使用标识  0：禁用；1：启用
        /// </summary>
        [Column("USED_FLAG")]
        //[Required]
        public int usedFlag { get; set; } = 1;


    }
}
