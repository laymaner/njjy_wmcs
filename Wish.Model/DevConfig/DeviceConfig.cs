//=============================================================================
//                                 A220101
//=============================================================================
//
// 设备配置信息实体类。
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

namespace Wish.HWConfig.Models

{

    [Table("WCS_DEV_INFO")]
    public class DeviceConfig : PersistPoco
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        [Display(Name = "DeviceInfo.DeviceNo")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(20, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Device_Code { get; set; }

        [Display(Name = "DeviceInfo.DeviceName")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Device_Name { get; set; }

        [Display(Name = "DeviceInfo.HouseNo")]
        [Column("Warehouse_Id")]
        public Int64 WarehouseId { get; set; } = 0;

        [Display(Name = "DeviceInfo.VersionNo")]
        [Required()]
        [Column("StandardDevice_ID")]
        public Int64 ? StandardDeviceId { get; set; }
        public StandardDevice StandardDevice { get; set; }

        [Display(Name = "DeviceInfo.Effective")]
        [Required(ErrorMessage = "{0}是必填项")]
        [Column("Valid_Sign")]
        public bool IsEnabled { get; set; }


        [Display(Name = "DeviceInfo.Mutually")]
        [Required(ErrorMessage = "{0}是必填项")]
        public bool Exec_Flag { get; set; } = false;

        [Display(Name = "DeviceInfo.DeviceGroup")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Device_Group { get; set; } = "Default";


        [Display(Name = "DBInfo.PLCName")]
        [Required(ErrorMessage = "{0}是必填项")]
        [Column("Plc_Id")]
        public Int64? PlcConfigId { get; set; }
        public PlcConfig PlcConfig { get; set; }//socket版本不需要外键约束


        [Display(Name = "DeviceInfo.PLCStep")]
        public Int32 Plc2WcsStep { get; set; } = 0;

        [Display(Name = "DeviceInfo.WCSStep")]
        public Int32 Wcs2PlcStep { get; set; } = 0;

        /// <summary>
        /// 设备模式 10待机，20运行，0未连接
        // 设备模式 0=脱机,1=自动,2=手动,3=点动,4=连动,5=半自动
        /// </summary>
        [Display(Name = "设备模式")]
        [Required(ErrorMessage = "{0}是必填项")]
        public Int32 Mode { get; set; } = 0;

        [Display(Name = "在线状态")]
        [Required(ErrorMessage = "{0}是必填项")]
        public bool IsOnline { get; set; } = false;

        [Display(Name = "DeviceInfo.Config")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(500, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Config { get; set; }

        [Display(Name = "DeviceInfo.Remark")]
        [StringLength(500, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Describe { get; set; }

    }
}
