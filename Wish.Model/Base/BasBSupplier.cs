using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Com.Wish.Model.Base
{
    [Table("BAS_B_SUPPLIER")]
    [Index(nameof(supplierCode), IsUnique = true)]
    public class BasBSupplier : BasePoco
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 供应商地址
        /// </summary>
        [Column("ADDRESS")]
        [StringLength(200, ErrorMessage = "供应商地址长度不能超出200字符")]
        public string address { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Column("CONTACTS")]
        [StringLength(100, ErrorMessage = "联系人长度不能超出100字符")]
        public string contacts { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Column("DESCRIPTION")]
        [StringLength(200, ErrorMessage = "描述长度不能超出200字符")]
        public string description { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        [Column("FAX")]
        [StringLength(100, ErrorMessage = "传真长度不能超出100字符")]
        public string fax { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Column("MAIL")]
        [StringLength(100, ErrorMessage = "邮箱长度不能超出100字符")]
        public string mail { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [Column("MOBILE")]
        [StringLength(30, ErrorMessage = "电话长度不能超出30字符")]
        public string mobile { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [Column("PHONE")]
        [StringLength(2000, ErrorMessage = "手机长度不能超出2000字符")]
        public string phone { get; set; }

        /// <summary>
        /// 货主编码
        /// </summary>
        [Column("PROPRIETOR_CODE")]
        [StringLength(100, ErrorMessage = "货主编码长度不能超出100字符")]
        public string proprietorCode { get; set; }

        /// <summary>
        /// 供应商全称
        /// </summary>
        [Column("SUPPILER_FULLNAME")]
        [StringLength(200, ErrorMessage = "供应商全称长度不能超出200字符")]
        public string suppilerFullname { get; set; }

        /// <summary>
        /// 供应商全称-其他
        /// </summary>
        [Column("SUPPILER_FULLNAME_ALIAS")]
        [StringLength(200, ErrorMessage = "供应商全称-其他长度不能超出200字符")]
        public string suppilerFullnameAlias { get; set; }

        /// <summary>
        /// 供应商全称-英文
        /// </summary>
        [Column("SUPPILER_FULLNAME_EN")]
        [StringLength(200, ErrorMessage = "供应商全称-英文长度不能超出200字符")]
        public string suppilerFullnameEn { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        [Column("SUPPLIER_NAME")]
        [Required]
        [StringLength(500, ErrorMessage = "供应商名称长度不能超出500字符")]
        public string supplierName { get; set; }

        /// <summary>
        /// 供应商名称-其他
        /// </summary>
        [Column("SUPPLIER_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "供应商名称-其他长度不能超出500字符")]
        public string supplierNameAlias { get; set; }

        /// <summary>
        /// 供应商名称-英文
        /// </summary>
        [Column("SUPPLIER_NAME_EN")]
        [StringLength(500, ErrorMessage = "供应商名称-英文长度不能超出500字符")]
        public string supplierNameEn { get; set; }

        /// <summary>
        /// 供应商编码
        /// </summary>
        [Column("SUPPLIER_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "供应商编码长度不能超出100字符")]
        public string supplierCode { get; set; }

        /// <summary>
        /// 使用标识  0：禁用；1：启用
        /// </summary>
        [Column("USED_FLAG")]
        [Required]
        public int usedFlag { get; set; } = 1;

        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("WHOUSE_NO")]
        [StringLength(100, ErrorMessage = "仓库编码长度不能超出100字符")]
        public string whouseNo { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        [Column("ZIP")]
        [StringLength(100, ErrorMessage = "邮编长度不能超出100字符")]
        public string zip { get; set; }

        /// <summary>
        /// 公司代码，固定值：A01，WMS不做任何处理，只做记录
        /// </summary>
        [Column("COMPANY_CODE")]
        [StringLength(100, ErrorMessage = "公司代码长度不能超出100字符")]
        public string companyCode { get; set; }

        ///// <summary>
        ///// 扩展字段1
        ///// </summary>
        //[Column("EXTEND1")]
        //[StringLength(200, ErrorMessage = "扩展字段1长度不能超出200字符")]
        //public string extend1 { get; set; }

        ///// <summary>
        ///// 扩展字段2
        ///// </summary>
        //[Column("EXTEND2")]
        //[StringLength(200, ErrorMessage = "扩展字段2长度不能超出200字符")]
        //public string extend2 { get; set; }

        ///// <summary>
        ///// 扩展字段3
        ///// </summary>
        //[Column("EXTEND3")]
        //[StringLength(200, ErrorMessage = "扩展字段3长度不能超出200字符")]
        //public string extend3 { get; set; }

        ///// <summary>
        ///// 扩展字段4
        ///// </summary>
        //[Column("EXTEND4")]
        //[StringLength(200, ErrorMessage = "扩展字段4长度不能超出200字符")]
        //public string extend4 { get; set; }

        ///// <summary>
        ///// 扩展字段5
        ///// </summary>
        //[Column("EXTEND5")]
        //[StringLength(200, ErrorMessage = "扩展字段5长度不能超出200字符")]
        //public string extend5 { get; set; }

        ///// <summary>
        ///// 扩展字段6
        ///// </summary>
        //[Column("EXTEND6")]
        //[StringLength(200, ErrorMessage = "扩展字段6长度不能超出200字符")]
        //public string extend6 { get; set; }

        ///// <summary>
        ///// 扩展字段7
        ///// </summary>
        //[Column("EXTEND7")]
        //[StringLength(200, ErrorMessage = "扩展字段7长度不能超出200字符")]
        //public string extend7 { get; set; }

        ///// <summary>
        ///// 扩展字段8
        ///// </summary>
        //[Column("EXTEND8")]
        //[StringLength(200, ErrorMessage = "扩展字段8长度不能超出200字符")]
        //public string extend8 { get; set; }

        ///// <summary>
        ///// 扩展字段9
        ///// </summary>
        //[Column("EXTEND9")]
        //[StringLength(200, ErrorMessage = "扩展字段9长度不能超出200字符")]
        //public string extend9 { get; set; }

        ///// <summary>
        ///// 扩展字段10
        ///// </summary>
        //[Column("EXTEND10")]
        //[StringLength(200, ErrorMessage = "扩展字段10长度不能超出200字符")]
        //public string extend10 { get; set; }

        ///// <summary>
        ///// 扩展字段11
        ///// </summary>
        //[Column("EXTEND11")]
        //[StringLength(200, ErrorMessage = "扩展字段11长度不能超出200字符")]
        //public string extend11 { get; set; }

        ///// <summary>
        ///// 扩展字段12
        ///// </summary>
        //[Column("EXTEND12")]
        //[StringLength(200, ErrorMessage = "扩展字段12长度不能超出200字符")]
        //public string extend12 { get; set; }

        ///// <summary>
        ///// 扩展字段13
        ///// </summary>
        //[Column("EXTEND13")]
        //[StringLength(200, ErrorMessage = "扩展字段13长度不能超出200字符")]
        //public string extend13 { get; set; }

        ///// <summary>
        ///// 扩展字段14
        ///// </summary>
        //[Column("EXTEND14")]
        //[StringLength(200, ErrorMessage = "扩展字段14长度不能超出200字符")]
        //public string extend14 { get; set; }

        ///// <summary>
        ///// 扩展字段15
        ///// </summary>
        //[Column("EXTEND15")]
        //[StringLength(200, ErrorMessage = "扩展字段15长度不能超出200字符")]
        //public string extend15 { get; set; }
    }
}