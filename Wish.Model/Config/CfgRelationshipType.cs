using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.Areas.Config.Model
{
    [Table("CFG_RELATIONSHIP_TYPE")]
    [Index(nameof(relationshipTypeCode), IsUnique = true)]
    public class CfgRelationshipType : BasePoco
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }

        /// <summary>
        /// 仅限开发人员标志。 0：所有人都可见； 1：超级管理员账号可见；
        /// </summary>
        [Column("DEVELOP_FLAG")]
        [Required]
        public int developFlag { get; set; }

        /// <summary>
        /// 自定义SQL语句：and no = ""
        /// </summary>
        [Column("LEFT_ATTRIBUTES")]
        [StringLength(2000, ErrorMessage = "自定义SQL语句：and no = 长度不能超出2000字符")]
        public string leftAttributes { get; set; }

        /// <summary>
        /// 左表表名称
        /// </summary>
        [Column("LEFT_TABLE")]
        [StringLength(100, ErrorMessage = "左表表名称长度不能超出100字符")]
        public string leftTable { get; set; }

        /// <summary>
        /// 左表数据编码
        /// </summary>
        [Column("LEFT_TABLE_CODE")]
        [StringLength(100, ErrorMessage = "左表数据编码长度不能超出100字符")]
        public string leftTableCode { get; set; }

        /// <summary>
        /// 左表数据编码前台显示名称
        /// </summary>
        [Column("LEFT_TABLE_CODE_LABEL")]
        [StringLength(100, ErrorMessage = "左表数据编码前台显示名称长度不能超出100字符")]
        public string leftTableCodeLabel { get; set; }

        /// <summary>
        /// 左表数据编码前台显示名称-其他
        /// </summary>
        [Column("LEFT_TABLE_CODE_LABEL_ALIAS")]
        [StringLength(100, ErrorMessage = "左表数据编码前台显示名称-其他长度不能超出100字符")]
        public string leftTableCodeLabelAlias { get; set; }

        /// <summary>
        /// 左表数据编码前台显示名称-英文
        /// </summary>
        [Column("LEFT_TABLE_CODE_LABEL_EN")]
        [StringLength(100, ErrorMessage = "左表数据编码前台显示名称-英文长度不能超出100字符")]
        public string leftTableCodeLabelEn { get; set; }

        /// <summary>
        /// 左表数据名称
        /// </summary>
        [Column("LEFT_TABLE_NAME")]
        [StringLength(100, ErrorMessage = "左表数据名称长度不能超出100字符")]
        public string leftTableName { get; set; }

        /// <summary>
        /// 左表数据名称其他语言
        /// </summary>
        [Column("LEFT_TABLE_NAME_ALIAS")]
        [StringLength(100, ErrorMessage = "左表数据名称其他语言长度不能超出100字符")]
        public string leftTableNameAlias { get; set; }

        /// <summary>
        /// 左表数据名称英文
        /// </summary>
        [Column("LEFT_TABLE_NAME_EN")]
        [StringLength(100, ErrorMessage = "左表数据名称英文长度不能超出100字符")]
        public string leftTableNameEn { get; set; }

        /// <summary>
        /// 左表数据名称前台显示名称
        /// </summary>
        [Column("LEFT_TABLE_NAME_LABEL")]
        [StringLength(100, ErrorMessage = "左表数据名称前台显示名称长度不能超出100字符")]
        public string leftTableNameLabel { get; set; }

        /// <summary>
        /// 左表数据名称前台显示名称-其他
        /// </summary>
        [Column("LEFT_TABLE_NAME_LABEL_ALIAS")]
        [StringLength(100, ErrorMessage = "左表数据名称前台显示名称-其他长度不能超出100字符")]
        public string leftTableNameLabelAlias { get; set; }

        /// <summary>
        /// 左表数据名称前台显示名称-英文
        /// </summary>
        [Column("LEFT_TABLE_NAME_LABEL_EN")]
        [StringLength(100, ErrorMessage = "左表数据名称前台显示名称-英文长度不能超出100字符")]
        public string leftTableNameLabelEn { get; set; }

        /// <summary>
        /// 是否需要仓库：0不需要，1需要
        /// </summary>
        [Column("LEFT_WHOUSE")]
        public int? leftWhouse { get; set; }

        /// <summary>
        /// 对应关系类型编号
        /// </summary>
        [Column("RELATIONSHIP_TYPE_CODE")]
        [Required]
        [StringLength(100, ErrorMessage = "对应关系类型编号长度不能超出100字符")]
        public string relationshipTypeCode { get; set; }

        /// <summary>
        /// 对应关系类型说明
        /// </summary>
        [Column("RELATIONSHIP_TYPE_DESC")]
        [StringLength(200, ErrorMessage = "对应关系类型说明长度不能超出200字符")]
        public string relationshipTypeDesc { get; set; }

        /// <summary>
        /// 对应关系类型名称
        /// </summary>
        [Column("RELATIONSHIP_TYPE_NAME")]
        [Required]
        [StringLength(100, ErrorMessage = "对应关系类型名称长度不能超出100字符")]
        public string relationshipTypeName { get; set; }

        /// <summary>
        /// 对应关系类型名称-其他
        /// </summary>
        [Column("RELATIONSHIP_TYPE_NAME_ALIAS")]
        [StringLength(100, ErrorMessage = "对应关系类型名称-其他长度不能超出100字符")]
        public string relationshipTypeNameAlias { get; set; }

        /// <summary>
        /// 对应关系类型名称-英文
        /// </summary>
        [Column("RELATIONSHIP_TYPE_NAME_EN")]
        [StringLength(100, ErrorMessage = "对应关系类型名称-英文长度不能超出100字符")]
        public string relationshipTypeNameEn { get; set; }

        /// <summary>
        /// 自定义SQL语句：
        /// </summary>
        [Column("RIGHT_ATTRIBUTES")]
        [StringLength(2000, ErrorMessage = "自定义SQL语句：长度不能超出2000字符")]
        public string rightAttributes { get; set; }

        /// <summary>
        /// 右表表名称
        /// </summary>
        [Column("RIGHT_TABLE")]
        [StringLength(100, ErrorMessage = "右表表名称长度不能超出100字符")]
        public string rightTable { get; set; }

        /// <summary>
        /// 右表数据编码
        /// </summary>
        [Column("RIGHT_TABLE_CODE")]
        [StringLength(100, ErrorMessage = "右表数据编码长度不能超出100字符")]
        public string rightTableCode { get; set; }

        /// <summary>
        /// 右表数据编码前台显示名称
        /// </summary>
        [Column("RIGHT_TABLE_CODE_LABEL")]
        [StringLength(100, ErrorMessage = "右表数据编码前台显示名称长度不能超出100字符")]
        public string rightTableCodeLabel { get; set; }

        /// <summary>
        /// 右表数据编码前台显示名称-其他
        /// </summary>
        [Column("RIGHT_TABLE_CODE_LABEL_ALIAS")]
        [StringLength(100, ErrorMessage = "右表数据编码前台显示名称-其他长度不能超出100字符")]
        public string rightTableCodeLabelAlias { get; set; }

        /// <summary>
        /// 右表数据编码前台显示名称-英文
        /// </summary>
        [Column("RIGHT_TABLE_CODE_LABEL_EN")]
        [StringLength(100, ErrorMessage = "右表数据编码前台显示名称-英文长度不能超出100字符")]
        public string rightTableCodeLabelEn { get; set; }

        /// <summary>
        /// 右表数据名称
        /// </summary>
        [Column("RIGHT_TABLE_NAME")]
        [StringLength(100, ErrorMessage = "右表数据名称长度不能超出100字符")]
        public string rightTableName { get; set; }

        /// <summary>
        /// 右表数据名称其他语言
        /// </summary>
        [Column("RIGHT_TABLE_NAME_ALIAS")]
        [StringLength(100, ErrorMessage = "右表数据名称其他语言长度不能超出100字符")]
        public string rightTableNameAlias { get; set; }

        /// <summary>
        /// 右表数据名称英文
        /// </summary>
        [Column("RIGHT_TABLE_NAME_EN")]
        [StringLength(100, ErrorMessage = "右表数据名称英文长度不能超出100字符")]
        public string rightTableNameEn { get; set; }

        /// <summary>
        /// 右表数据名称前台显示名称
        /// </summary>
        [Column("RIGHT_TABLE_NAME_LABEL")]
        [StringLength(100, ErrorMessage = "右表数据名称前台显示名称长度不能超出100字符")]
        public string rightTableNameLabel { get; set; }

        /// <summary>
        /// 右表数据名称前台显示名称-其他
        /// </summary>
        [Column("RIGHT_TABLE_NAME_LABEL_ALIAS")]
        [StringLength(200, ErrorMessage = "右表数据名称前台显示名称-其他长度不能超出200字符")]
        public string rightTableNameLabelAlias { get; set; }

        /// <summary>
        /// 右表数据名称前台显示名称-英文
        /// </summary>
        [Column("RIGHT_TABLE_NAME_LABEL_EN")]
        [StringLength(100, ErrorMessage = "右表数据名称前台显示名称-英文长度不能超出100字符")]
        public string rightTableNameLabelEn { get; set; }

        /// <summary>
        /// 右表需要录入优先级（0：不需要，1：需要）
        /// </summary>
        [Column("RIGHT_TABLE_PRIORITY")]
        public int rightTablePriority { get; set; }

        /// <summary>
        /// 是否需要仓库：0不需要，1需要
        /// </summary>
        [Column("RIGHT_WHOUSE")]
        public int? rightWhouse { get; set; }

        /// <summary>
        /// 使用标志(0：停用；1：启用)
        /// </summary>
        [Column("USED_FLAG")]
        [Required]
        public int usedFlag { get; set; } = 1;


    }
}
