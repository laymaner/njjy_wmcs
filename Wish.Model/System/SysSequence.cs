using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;

namespace Wish.System.Model
{
    [Table("SYS_SEQUENCE")]
    public class SysSequence : BasePoco
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int32 ID { get; set; }

        ///<summary>
        ///序列名称
        ///</summary>
        [Column("SEQ_CODE")]
		[StringLength(200, ErrorMessage = "序列名称不能超出200字符")]
        public string SeqCode { get; set; }

        ///<summary>
        ///序列说明
        ///</summary>
        [Column("SEQ_DESC")]
        [StringLength(500, ErrorMessage = "序列说明长度不能超出500字符")]
        public string SeqDesc { get; set; }

        ///<summary>
        ///序列类型。1、是普通ID每次累加1且不循环2、为循环ID到达最大值后会从新重1开始3、为固定长度id每次生成id会按照ID长度生成不足就左边补04、为带前缀固定长度ID与类型3相同,只是会增加一个前缀字段5、为带日期的固定长度ID，每天从1开始更新6、在最大值与最小值之间取序列
        ///</summary>
        [Column("SEQ_TYPE")]
        public int? SeqType { get; set; }

        ///<summary>
        ///当前序列号
        ///</summary>
        [Column("NOW_SN")]
        public int? NowSn { get; set; }

        ///<summary>
        ///最小序列号
        ///</summary>
        [Column("MIN_SN")]
        public int? MinSn { get; set; }

        ///<summary>
        ///最大序列号
        ///</summary>
        [Column("MAX_SN")]
        public int? MaxSn { get; set; }

        ///<summary>
        ///序列号长度（不包含前缀、日期）
        ///</summary>
        [Column("SEQ_SN_LEN")]
        public int? SeqSnLen { get; set; }

        ///<summary>
        ///序列前缀
        ///</summary>
        [Column("SEQ_PREFIX")]
        [StringLength(500, ErrorMessage = "联系人长度不能超出500字符")]
        public string SeqPrefix { get; set; }

        ///<summary>
        ///序列日期：序列由日期组成时，最后获取序列的日期
        ///</summary>
        [Column("SEQ_DATE")]
        [StringLength(500, ErrorMessage = "联系人长度不能超出500字符")]
        public string SeqDate { get; set; }

        /////<summary>
        /////创建人：（账号）姓名
        /////</summary>
        //[Column("CREATE_BY")]
        //public string CreateBy { get; set; }

        /////<summary>
        /////创建时间
        /////</summary>
        //[Column("CREATE_TIME")]
        //public DateTime CreateTime { get; set; }

        /////<summary>
        /////更新人：（账号）姓名
        /////</summary>
        //[Column("UPDATE_BY")]
        //public string UpdateBy { get; set; }

        /////<summary>
        /////更新时间
        /////</summary>
        //[Column("UPDATE_TIME")]
        //public DateTime? UpdateTime { get; set; }
    }
}
