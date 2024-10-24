//=============================================================================
//                                 A220101
//=============================================================================
//
// PLC的DB块配置信息实体类。
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
    [Table("WCS_DEV_DB")]
    public class DBConfig : PersistPoco
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }
        /// <summary>
        /// 数据块编号，如"DB540"
        /// </summary>
        [Display(Name = "DBInfo.DBNo")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(20, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Block_Code { get; set; }

        [Display(Name = "DBInfo.DBName")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Block_Name { get; set; }

        [Display(Name = "DBInfo.Offset")]
        [Required(ErrorMessage = "{0}是必填项")]
        public int Block_Offset { get; set; }

        [Display(Name = "DBInfo.TotalLength")]
        [Required(ErrorMessage = "{0}是必填项")]
        public int Block_Length { get; set; } = 0;

        [Display(Name = "所属Plc")]
        [Required()]
        public Int64  PlcConfigId { get; set; }
        public PlcConfig PlcConfig { get; set; }

        [Display(Name = "DeviceInfo.Remark")]
        [StringLength(500, ErrorMessage = "{0}最多输入{1}个字符")]
        public string Describe { get; set; }
    }
}
