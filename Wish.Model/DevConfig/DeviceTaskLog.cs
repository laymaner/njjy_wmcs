//=============================================================================
//                                 A220101
//=============================================================================
//
// 设备任务执行日志。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/1/28
//      创建
//
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace Wish.HWConfig.Models
{
    [Table("WCS_DEV_TASK_LOG")]
    public class DeviceTaskLog : PersistPoco
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        [Display(Name = "DeviceInfo.DeviceNo")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(20, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Device_Code { get; set; }

        [Display(Name = "TaskLog.Direction")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Direct { get; set; }


        [Display(Name = "TaskLog.TaskNo")]
        [StringLength(500, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Task_No { get; set; }

        [Display(Name = "TaskLog.Massage")]
        [StringLength(5000, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Message { get; set; }
    }
}
