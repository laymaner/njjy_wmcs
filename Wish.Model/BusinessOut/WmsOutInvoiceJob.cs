using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Business
{
    [Table("WMS_OUT_INVOICE_JOB")]
    public class WmsOutInvoiceJob : BasePoco
	{
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 发货单号
        /// </summary>
        [Column("INVOICE_NO")]
        [Display(Name = "发货单号")]
        [Required]
		[StringLength(100, ErrorMessage = "发货单号长度不能超出100字符")]
		public string invoiceNo { get; set; }

        /// <summary>
        /// 项目号
        /// </summary>
        [Column("PROJECT_NO")]
        [Display(Name = "项目号")]
        [StringLength(100, ErrorMessage = "项目号长度不能超出100字符")]
        public string projectNo { get; set; }

        /// <summary>
        /// 处理标识;0：待处理 1：已处理
        /// </summary>
        [Column("DEAL_FLAG")]
        [Display(Name = "处理标识")]
        public int dealFlag { get; set; }

        /// <summary>
        /// 单据创建时间
        /// </summary>
        [Column("ORDER_CREATE_TIME")]
        [Display(Name = "单据创建时间")]
        public DateTime? orderCreateTime { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [Column("REMARKS")]
        [Display(Name = "备注信息")]
        [StringLength(500, ErrorMessage = "备注信息长度不能超出500字符")]
        public string remarks { get; set; }
    }
}
