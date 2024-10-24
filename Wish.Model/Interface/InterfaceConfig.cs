using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace Wish.Model.Interface
{
    [Table("INTERFACE_CONFIG")]
    public class InterfaceConfig : BasePoco
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 接口编码
        /// </summary>
        [Column("INTERFACE_CODE")]
        [StringLength(100, ErrorMessage = "接口编码长度不能超出100字符")]
        public string interfaceCode { get; set; }

        /// <summary>
        /// 接口名称
        /// </summary>
        [Column("INTERFACE_NAME")]
        [StringLength(255, ErrorMessage = "接口名称长度不能超出255字符")]
        public string interfaceName { get; set; }

        /// <summary>
        /// 接口地址
        /// </summary>
        [Column("INTERFACE_URL")]
        [StringLength(500, ErrorMessage = "接口地址长度不能超出500字符")]
        public string interfaceUrl { get; set; }

        /// <summary>
        /// 最大回传尝试次数
        /// </summary>
        [Column("RETRY_MAX_TIMES")]
        public int retryMaxTimes { get; set; }

        /// <summary>
        /// 失败回调尝试间隔(单位秒)
        /// </summary>
        [Column("RETRY_INTERVAL")]
        public int retryInterval { get; set; }

    }
}
