//=============================================================================
//                                 A220101
//=============================================================================
//
// 设备状态变更日志。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/1/28
//      创建
//
//-----------------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace Wish.HWConfig.Models
{
    [Table("WCS_DEV_STATUS_LOG")]
    public class DeviceStatusLog : PersistPoco
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        [Display(Name = "DeviceInfo.DeviceNo")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(20, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Device_Code { get; set; }

        [Display(Name = "StatusLog.ChangeStatus")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Attribute { get; set; }

        [Display(Name = "StatusLog.StatusInfo")]
        [StringLength(100, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Content { get; set; }

        [Display(Name = "StatusLog.FeedBack")]
        [StringLength(5000, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Message { get; set; }
    }
}
