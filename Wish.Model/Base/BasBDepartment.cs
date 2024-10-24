using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace WMS.Model.Base
{
    [Table("BAS_B_DEPARTMENT")]
    [Index(nameof(DepartmentCode), IsUnique = true)]
    public class BasBDepartment: BasePoco
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 事业部编码
        /// </summary>
        [Column("DEPARTMENT_CODE")]
        [Required]
        [StringLength(500, ErrorMessage = "单位编码长度不能超出255字符")]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 事业部名称
        /// </summary>
        [Column("DEPARTMENT_NAME")]
        [Required]
        [StringLength(500, ErrorMessage = "单位编码长度不能超出255字符")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 事业部名称
        /// </summary>
        [Column("DEPARTMENT_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "单位编码长度不能超出255字符")]
        public string DepartmentNameAlias { get; set; }

        /// <summary>
        /// 事业部名称
        /// </summary>
        [Column("DEPARTMENT_NAME_EN")]
        [StringLength(500, ErrorMessage = "单位编码长度不能超出255字符")]
        public string DepartmentNameEn { get; set; }


        /// <summary>
        /// 使用标识  0：禁用；1：启用
        /// </summary>
        [Column("USED_FLAG")]
        [Required]
        public int UsedFlag { get; set; } = 1;
      
    }
}
