using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace Wish.Model.System
{
    [Table("SYS_EMAIL")]
    public class SysEmail : BasePoco
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int32 ID { get; set; }

        /// <summary>
        /// 预警类型：超期预警、暴露预警等
        /// </summary>
        [Column("ALERT_TYPE")]
        [Required]
        [StringLength(100, ErrorMessage = "预警类型：超期预警、暴露预警等长度不能超出100字符")]
        public string alertType { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        [Column("EMAIL")]
        [Required]
        [StringLength(100, ErrorMessage = "邮箱地址长度不能超出100字符")]
        public string email { get; set; }

        /// <summary>
        /// 使用标识  0：禁用；1：启用
        /// </summary>
        [Column("USED_FLAG")]
        [Required]
        public int usedFlag { get; set; } = 1;

        /// <summary>
        /// 用户名
        /// </summary>
        [Column("USER_NAME")]
        [StringLength(100, ErrorMessage = "用户名长度不能超出100字符")]
        public string userName { get; set; }


    }
}
