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
    [Table("INTERFACE_SEND_BACK_HIS")]
    public class InterfaceSendBackHis : BasePoco
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
        /// 接口内容
        /// </summary>
        [Column("INTERFACE_SEND_INFO")]
        [StringLength(255, ErrorMessage = "接口名称长度不能超出255字符")]
        public string interfaceSendInfo { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        [Column("INTERFACE_RESULT")]
        [StringLength(255, ErrorMessage = "接口名称长度不能超出255字符")]
        public string interfaceResult { get; set; }

        /// <summary>
        /// 回传标记，0初始等待回传，1回传失败，2回传成功，3无需回传
        /// </summary>
        [Column("RETURN_FLAG")]
        [Required]
        [StringLength(50, ErrorMessage = "WMS任务类型。详见字典表。长度不能超出50字符")]
        public int returnFlag { get; set; } = 0;

        /// <summary>
        /// 回传次数
        /// </summary>
        [Column("RETURN_TIMES")]
        [Required]
        public int returnTimes { get; set; } = 0;

    }
}
