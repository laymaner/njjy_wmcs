//=============================================================================
//                                 A220101
//=============================================================================
//
// 系统内置设备。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/1/18
//      创建
//
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WalkingTec.Mvvm.Core;
using ASRS.WCS.Common.Enum;

namespace Wish.HWConfig.Models
{
    [Table("WCS_DEV_STANDARD")]
    public class StandardDevice : PersistPoco
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// WISH.SRM.V10
        /// </summary>
        [Display(Name = "DeviceInfo.DeviceNo")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(20, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Device_Code { get; set; }

        [Display(Name = "DeviceInfo.DeviceName")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Device_Name { get; set; }


        [Display(Name = "实现类名")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(100, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Device_Class { get; set; }

        [Display(Name = "VersionInfo.Type")]
        [Required(ErrorMessage = "{0}是必填项")]
        [Column("Device_Type")]
        public EDeviceType DeviceType { get; set; } = EDeviceType.S1Srm;

        //[Display(Name = "版本号")]
        //[Required(ErrorMessage = "{0}是必填项")]
        //[StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        //[MaxLength(50)]
        //public string Verson { get; set; }

        [Display(Name = "VersionInfo.Company")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Company { get; set; }

        [Display(Name = "DeviceInfo.Config")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(500, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Config { get; set; }

        [Display(Name = "DeviceInfo.Remark")]
        [StringLength(500, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Describe { get; set; }

    }
}
