using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace Wish.Model.System
{
    [Table("SYS_MAP_MONITOR")]
    public class SysMapMonitor : BasePoco
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int32 ID { get; set; }

        /// <summary>
        /// 配置
        /// </summary>
        [Column("MAP_CONFIG")]
        //[Required]
        [StringLength(5000, ErrorMessage = "预警类型：配置长度不能超出100字符")]
        public string mapConfig { get; set; }
    }
}
