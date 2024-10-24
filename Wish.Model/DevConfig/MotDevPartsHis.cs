using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace Wish.Model.DevConfig
{
    [Table("MOT_DEV_PARTS_HIS")]
    public  class MotDevPartsHis : PersistPoco
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Int64 ID { get; set; }
        ///<summary>
        ///仓库编码
        ///</summary>
        public string AreaNo { get; set; }

        ///<summary>
        ///设备编号
        ///</summary>
        public string DevNo { get; set; }

        ///<summary>
        ///部件号
        ///</summary>
        public string PartNo { get; set; }

        ///<summary>
        ///对应节点编码(对应调度系统站点码)
        ///</summary>
        public string PartLocNo { get; set; }

        ///<summary>
        ///运行模式
        ///</summary>
        public int? DevRunMode { get; set; }

        ///<summary>
        ///堆垛机所在巷道
        ///</summary>
        public string SrmRoadway { get; set; }

        ///<summary>
        ///货叉类型
        ///</summary>
        public int? SrmForkType { get; set; }

        ///<summary>
        ///执行步骤
        ///</summary>
        public int? SrmExecStep { get; set; }

        ///<summary>
        ///是否原点
        ///</summary>
        public int? IsInSitu { get; set; }

        ///<summary>
        ///是否报警
        ///</summary>
        public int? IsAlarming { get; set; }

        ///<summary>
        ///报警代码
        ///</summary>
        public string AlarmCode { get; set; }

        ///<summary>
        ///是否空闲
        ///</summary>
        public int? IsFree { get; set; }

        ///<summary>
        ///是否有载
        ///</summary>
        public int? IsHasGoods { get; set; }

        ///<summary>
        ///指令号
        ///</summary>
        public string CmdNo { get; set; }

        ///<summary>
        ///托盘号
        ///</summary>
        public string PalletNo { get; set; }

        ///<summary>
        ///上一个托盘号
        ///</summary>
        public string OldPalletNo { get; set; }

        ///<summary>
        ///读码器读取的托盘条码
        ///</summary>
        public string ReadPalletNo { get; set; }

        ///<summary>
        ///RGV站台编号
        ///</summary>
        public string StationNo { get; set; }

        ///<summary>
        ///当前X坐标；堆垛机对应排，RGV对应位置
        ///</summary>
        public int? CurrentX { get; set; }

        ///<summary>
        ///当前Y坐标；堆垛机所在的列
        ///</summary>
        public int? CurrentY { get; set; }

        ///<summary>
        ///当前Z坐标：堆垛机所在的层
        ///</summary>
        public int? CurrentZ { get; set; }

    }
}
