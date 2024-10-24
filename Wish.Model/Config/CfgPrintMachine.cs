using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.Config.Model
{
    [Table("CFG_PRINT_MACHINE")]
    public class CfgPrintMachine : BasePoco
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 打印机编号
        /// </summary>
        [Column("PRINTER_NO")]
        [StringLength(100, ErrorMessage = "打印机编号长度不能超出100字符")]
        public string printerNo { get; set; }

        /// <summary>
        /// 打印机名称
        /// </summary>
        [Column("PRINTER_NAME")]
        [StringLength(500, ErrorMessage = "打印机名称长度不能超出500字符")]
        public string printerName { get; set; }

        /// <summary>
        /// 打印机类型
        /// </summary>
        [Column("PRINTER_TYPE")]
        [StringLength(100, ErrorMessage = "打印机类型长度不能超出100字符")]
        public string printerType { get; set; }

        /// <summary>
        /// 打印对应的功能编码
        /// </summary>
        [Column("PRINT_FUNC_CODE")]
        [StringLength(100, ErrorMessage = "打印对应的功能编码长度不能超出100字符")]
        public string printFuncCode { get; set; }

        /// <summary>
        /// 打印对应的功能名称
        /// </summary>
        [Column("PRINT_FUNC_NAME")]
        [StringLength(500, ErrorMessage = "打印对应的功能名称长度不能超出500字符")]
        public string printFuncName { get; set; }

        /// <summary>
        /// 打印对应的功能名称-英文
        /// </summary>
        [Column("PRINT_FUNC_NAME_EN")]
        [StringLength(500, ErrorMessage = "打印对应的功能名称-英文长度不能超出500字符")]
        public string printFuncNameEn { get; set; }

        /// <summary>
        /// 打印对应的功能名称-其他
        /// </summary>
        [Column("PRINT_FUNC_NAME_ALIAS")]
        [StringLength(500, ErrorMessage = "打印对应的功能名称-其他长度不能超出500字符")]
        public string printFuncNameAlias { get; set; }

        /// <summary>
        /// 打印机IP
        /// </summary>
        [Column("PRINTER_IP")]
        [StringLength(100, ErrorMessage = "打印机IP长度不能超出100字符")]
        public string printerIp { get; set; }

        /// <summary>
        /// 打印机端口
        /// </summary>
        [Column("PRINTER_PORT")]
        [StringLength(100, ErrorMessage = "打印机端口长度不能超出100字符")]
        public string printerPort { get; set; }

        /// <summary>
        /// 一次打印几份
        /// </summary>
        [Column("COPIES_NUM")]
        public int copiedNum { get; set; }

        /// <summary>
        /// 所在楼号(楼号+打印机模板编码 是为了要确定打印机唯一性)
        /// </summary>
        [Column("AREA_NO")]
        [StringLength(100, ErrorMessage = "所在楼号长度不能超出100字符")]
        public string areaNo { get; set; }

        /// <summary>
        /// 打印模板编码
        /// </summary>
        [Column("PRINT_TEMPLATE_CODE")]
        [StringLength(100, ErrorMessage = "打印模板编码长度不能超出100字符")]
        public string printTemplateCode { get; set; }

        /// <summary>
        /// 使用标志。0：停用；1：启用；
        /// </summary>
        [Column("USED_FLAG")]
        [Required]
        public int usedFlag { get; set; } = 1;
    }
}