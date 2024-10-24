//=============================================================================
//                                 A220101
//=============================================================================
//
// Plc配置信息实体类。
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

    public enum PlcConnType
    {
        [Display(Name = "HSL")]
        HSL = 1,
        [Display(Name = "Socket")]
        Socket=2,
        //[Display(Name = "OPC_Net")]
        //OPC_Net
    }

    [Table("WCS_DEV_PLC")]
    public class PlcConfig: PersistPoco
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        [Display(Name = "DeviceInfo.DeviceNo")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(20, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Plc_Code { get; set; }

        [Display(Name = "DeviceInfo.DeviceName")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Plc_Name { get; set; }

        [Display(Name = "PLCInfo.IP")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string IP_Address { get; set; }

        [Display(Name = "PLCInfo.Port")]
        [Required(ErrorMessage = "{0}是必填项")]
        public int IP_Port { get; set; } = 0;

        [Display(Name = "PLCInfo.Connect")]
        [Required(ErrorMessage = "{0}是必填项")]
        [Column("Comm_Type")]
        public PlcConnType ConnType { get; set; } = PlcConnType.Socket;

        [Display(Name = "连接状态")]
        [Required(ErrorMessage = "{0}是必填项")]
        [NotMapped]
        public bool IsConnect { get; set; } = false;

        [Display(Name = "DeviceInfo.Effective")]
        [Required(ErrorMessage = "{0}是必填项")]
        [Column("Valid_Sign")]
        public bool IsEnabled { get; set; } = true;

        [Display(Name = "PLCInfo.Scan")]
        [Required(ErrorMessage = "{0}是必填项")]
        public int Scan_Cycle { get; set; } = 100;

        [Display(Name = "PLCInfo.CheckHeartDB")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(20, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Heartbeat_DB { get; set; }

        [Display(Name = "PLCInfo.CheckHeartOffset")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(20, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Heartbeat_Address { get; set; }

        [Display(Name = "PLCInfo.CheckHeart")]
        [Required(ErrorMessage = "{0}是必填项")]
        public bool Heartbeat_Enabled { get; set; } = true;

        [Display(Name = "PLCInfo.WriteHeart")]
        [Required(ErrorMessage = "{0}是必填项")]
        public int Heartbeat_WriteInterval { get; set; } = 500;

        [Display(Name = "DeviceInfo.Remark")]
        [StringLength(500, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Describe { get; set; }

        public List<DBConfig> DBConfigs { get; set; }

    }
}
