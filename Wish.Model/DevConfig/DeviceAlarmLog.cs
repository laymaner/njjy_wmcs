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
    [Table("WCS_DEV_ALARM_LOG")]
    public class DeviceAlarmLog : PersistPoco
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        [Display(Name = "DeviceInfo.DeviceNo")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(20, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Device_Code { get; set; }

        [Display(Name = "AlarmLog.AlarmInfo")]
        [StringLength(5000, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Message { get; set; }


        [Display(Name = "AlarmLog.HandleFlag")]
        public int HandleFlag { get; set; }= 0;


        /// <summary>
        /// 报警发生时间
        /// </summary>
        [Display(Name = "AlarmLog.StartTime")]
        [Required(ErrorMessage = "{0}是必填项")]



        public DateTime OriginTime {get; set; }
        [Display(Name = "AlarmLog.EndTime")]
        public DateTime? EndTime { get; set; }    
    }
}
