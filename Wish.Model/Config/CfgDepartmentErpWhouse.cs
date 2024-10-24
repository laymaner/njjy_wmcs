using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.Config.Model
{
    [Table("CFG_DEPARTMENT_ERP_WHOUSE")]
    [Index(nameof(departmentCode), IsUnique = true)]
    public class CfgDepartmentErpWhouse : BasePoco
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
        [Column("DEPARTMENT_NO")]
        [Required]
        [StringLength(500, ErrorMessage = "单据类型长度不能超出255字符")]
        public string departmentCode { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        [Column("DOC_TYPE_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "单据类型长度不能超出255字符")]
        public string docTypeCode { get; set; }

        /// <summary>
		/// ERP仓库编号
		/// </summary>
		[Column("ERP_WHOUSE_NO")]
        [Required]
        [StringLength(500, ErrorMessage = "ERP仓库编号长度不能超出100字符")]
        public string erpWhouseNo { get; set; }

        /// <summary>
		/// 优先级
		/// </summary>
		[Column("PRIORITY")]
        [Required]
        public int priority { get; set; }
    }
}
