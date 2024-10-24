using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WISH.Helper.Common.Dictionary.DictionaryHelper
{
    public partial class DictonaryHelper
    {
        #region 入库模块枚举
        #region 入库单、入库明细单，字典枚举
        /// <summary>
        /// 入库单、入库明细单，状态（初始创建：0，收货中：11，收货完成：19，验收中：21，验收完成：29，组盘中：31，组盘完成：39，入库中：41，入库完成：90，删除：92，强制完成：93）
        /// </summary>
        public enum InOrDtlStatus
        {
            /// <summary>
            /// 初始创建
            /// </summary>
            [Description("0")]
            Init,

            /// <summary>
            /// 收货中
            /// </summary>
            [Description("11")]
            Receipting,

            /// <summary>
            /// 收货完成
            /// </summary>
            [Description("19")]
            Receipted,

            /// <summary>
            /// 验收中
            /// </summary>
            [Description("21")]
            IQCing,

            /// <summary>
            /// 验收完成
            /// </summary>
            [Description("29")]
            IQCFinished,

            /// <summary>
            /// 组盘中
            /// </summary>
            [Description("31")]
            PutAwaying,

            /// <summary>
            /// 组盘完成
            /// </summary>
            [Description("39")]
            PutAwayed,

            /// <summary>
            /// 入库中
            /// </summary>
            [Description("41")]
            StoreIning,

            /// <summary>
            /// 入库完成
            /// </summary>
            [Description("90")]
            StoreIned,

            /// <summary>
            /// 删除
            /// </summary>
            [Description("92")]
            Delete,

            /// <summary>
            /// 强制完成
            /// </summary>
            [Description("93")]
            ForceFinish
        }
        #endregion

        #region 收货单、收货明细单，枚举
        /// <summary>
        /// 收货单、收货明细单，状态（初始创建：0，质检中：21，质检完成：29，组盘中：31，组盘完成：39，入库中：41，入库完成：90，删除：92，强制完成：93）
        /// </summary>
        public enum ReceiptOrDtlStatus
        {
            /// <summary>
            /// 初始创建
            /// </summary>
            [Description("0")]
            Init,

            /// <summary>
            /// 质检中
            /// </summary>
            [Description("20")]
            QCRecord,

            /// <summary>
            /// 质检中
            /// </summary>
            [Description("21")]
            IQCing,


            /// <summary>
            /// 质检完成
            /// </summary>
            [Description("29")]
            IQCFinished,

            /// <summary>
            /// 组盘中
            /// </summary>
            [Description("31")]
            PutAwaying,

            /// <summary>
            /// 组盘完成
            /// </summary>
            [Description("39")]
            PutAwayed,

            /// <summary>
            /// 入库中
            /// </summary>
            [Description("41")]
            StoreIning,

            /// <summary>
            /// 入库完成
            /// </summary>
            [Description("90")]
            StoreIned,

            /// <summary>
            /// 删除
            /// </summary>
            [Description("92")]
            Delete,

            /// <summary>
            /// 强制完成
            /// </summary>
            [Description("93")]
            ForceFinish
        }
        public enum IQCTypeFlag
        {
            /// <summary>
            /// WMS质检
            /// </summary>
            [Description("10")]
            WMS,

            /// <summary>
            /// 外部质检
            /// </summary>
            [Description("20")]
            EBS,
        }
        #endregion

            #region 质检记录、质检结果，枚举
            /// <summary>
            /// 质检记录，状态(初始创建：0，质检中：21，质检完成：90，删除：92，强制完成：93)
            /// </summary>
        public enum IqcRecordStatus
        {
            /// <summary>
            /// 初始创建
            /// </summary>
            [Description("0")]
            Init,

            /// <summary>
            /// 质检中
            /// </summary>
            [Description("21")]
            IQCing,

            /// <summary>
            /// 质检完成
            /// </summary>
            [Description("90")]
            IQCFinished,

            /// <summary>
            /// 删除
            /// </summary>
            [Description("92")]
            Delete,

            /// <summary>
            /// 强制完成
            /// </summary>
            [Description("93")]
            ForceFinish
        }
        /// <summary>
        /// 质检结果，状态（初始创建：0，组盘中：31，组盘完成：39，入库中：41，入库完成：90，删除：92，强制完成：93）
        /// </summary>
        public enum IqcResultStatus
        {
            /// <summary>
            /// 初始创建
            /// </summary>
            [Description("0")]
            Init,
            /// <summary>
            /// 组盘中
            /// </summary>
            [Description("31")]
            PutAwaying,

            /// <summary>
            /// 组盘完成
            /// </summary>
            [Description("39")]
            PutAwayed,

            /// <summary>
            /// 入库中
            /// </summary>
            [Description("41")]
            StoreIning,

            /// <summary>
            /// 入库完成
            /// </summary>
            [Description("90")]
            StoreIned,

            /// <summary>
            /// 删除
            /// </summary>
            [Description("92")]
            Delete,

            /// <summary>
            /// 强制完成
            /// </summary>
            [Description("93")]
            ForceFinish
        }
        #endregion

        #region 入库记录，枚举
        /// <summary>
        /// 入库记录，状态（初始创建：0，入库中：41，入库完成：90，删除：92，强制完成：93）
        /// </summary>
        public enum InRecordStatus
        {
            /// <summary>
            /// 初始创建
            /// </summary>
            [Description("0")]
            Init,

            /// <summary>
            /// 入库中
            /// </summary>
            [Description("41")]
            StoreIning,

            /// <summary>
            /// 入库完成
            /// </summary>
            [Description("90")]
            StoreIned,

            /// <summary>
            /// 删除
            /// </summary>
            [Description("92")]
            Delete,

            /// <summary>
            /// 强制完成
            /// </summary>
            [Description("93")]
            ForceFinish
        }
        #endregion

        #region 入库唯一码，枚举
        /// <summary>
        /// 入库唯一码，状态（初始创建：0，收货完成：10，组盘完成：20，入库完成：90，删除：92，强制完成：93）无字典维护
        /// </summary>
        public enum InUniicodeStatus
        {
            /// <summary>
            /// 初始创建
            /// </summary>
            [Description("0")]
            InitCreate,

            /// <summary>
            /// 已收货
            /// </summary>
            [Description("10")]
            ReceiptFinished,

            /// <summary>
            /// 已组盘
            /// </summary>
            [Description("20")]
            PutwayFinished,

            /// <summary>
            /// 已入库
            /// </summary>
            [Description("90")]
            InStoreFinished,

            /// <summary>
            /// 删除
            /// </summary>
            [Description("92")]
            Deleted,

            /// <summary>
            /// 强制完成
            /// </summary>
            [Description("93")]
            ForceFinished,


        }
        #endregion

        #region 上架单、上架明细单，枚举
        /// <summary>
        /// 上架单、上架明细单，状态枚举
        /// </summary>
        public enum PutAwayOrDtlStatus
        {
            /// <summary>
            /// 初始创建
            /// </summary>
            [Description("0")]
            Init,

            /// <summary>
            /// 入库中 
            /// </summary>
            [Description("41")]
            StoreIning,

            /// <summary>
            /// 上架完成
            /// </summary>
            [Description("90")]
            StoreIned,

            /// <summary>
            /// 删除
            /// </summary>
            [Description("92")]
            Delete,

            /// <summary>
            /// 强制完成
            /// </summary>
            [Description("93")]
            ForceFinish
        }
        #endregion

        #region 入库模块通用枚举
        /// <summary>
        /// 质检方式
        /// </summary>
        public enum IQCType
        {

            /// <summary>
            /// 内部质检
            /// </summary>
            [Description("ITN")]
            ITN,

            /// <summary>
            /// 质检系统
            /// </summary>
            [Description("QCS")]
            QCS
        }

        /// <summary>
        /// 不良处理选项,字典未维护具体含义
        /// </summary>
        public enum BadOptions
        {

            /// <summary>
            /// 不良选项1
            /// </summary>
            [Description("1")]
            Option1,

            /// <summary>
            /// 不良选项2
            /// </summary>
            [Description("2")]
            Option2,

            /// <summary>
            /// 不良选项3
            /// </summary>
            [Description("3")]
            Option3,

            /// <summary>
            /// 不良选项4
            /// </summary>
            [Description("4")]
            Option4
        }
        /// <summary>
        /// 不良处理方式,字典未维护具体含义
        /// </summary>
        public enum BadHandleWay
        {

            /// <summary>
            /// 不良处理方式1
            /// </summary>
            [Description("1")]
            Deal1,

            /// <summary>
            /// 不良处理方式2
            /// </summary>
            [Description("2")]
            Deal2,

            /// <summary>
            /// 不良处理方式3
            /// </summary>
            [Description("3")]
            Deal3,

            /// <summary>
            /// 不良处理方式4
            /// </summary>
            [Description("4")]
            Deal4
        }


        #endregion

        #endregion


        #region 出库模块枚举
        #region 发货单，发货单明细，状态
        /// <summary>
        /// 发货单，状态（0：初始创建；41：出库中；51：发货中；90：出库完成；92删除；93强制完成）
        /// 发货单明细，状态（0：初始创建；21：分配中；29：分配完成；41：拣选中；49：拣选完成；51：发货中；90：出库完成；92删除；93强制完成）
        /// </summary>
        public enum OutInvoiceOrDtlStatus
        {
            /// <summary>
            /// 初始创建
            /// </summary>
            [Description("0")]
            Init,
            /// <summary>
            /// 分配中
            /// </summary>
            [Description("21")]
            Allocating,

            /// <summary>
            /// 分配完成
            /// </summary>
            [Description("29")]
            AllocateFinished,

            /// <summary>
            /// 出库中（明细单拣选中）
            /// </summary>
            [Description("41")]
            StoreOrPickOuting,

            /// <summary>
            /// 拣选完成
            /// </summary>
            [Description("49")]
            PickFinished,

            /// <summary>
            /// 发货中
            /// </summary>
            [Description("51")]
            Deliverying,

            /// <summary>
            /// 出库完成
            /// </summary>
            [Description("90")]
            StoreOuted,

            /// <summary>
            /// 删除
            /// </summary>
            [Description("92")]
            Delete,

            /// <summary>
            /// 强制完成
            /// </summary>
            [Description("93")]
            ForceFinish
        }
        #endregion

        #region 波次状态
        /// <summary>
        /// 波次单状态：0：初始创建；11：分配中；22：分配完成；99：确认完成；100: 强制完成；111：已删除。
        /// </summary>
        public enum WaveStatus
        {
            /// <summary>
            /// 初始创建
            /// </summary>
            [Description("0")]
            InitCreate,

            /// <summary>
            /// 分配中
            /// </summary>
            [Description("11")]
            Allocating,

            /// <summary>
            /// 分配完成
            /// </summary>
            [Description("22")]
            AllocateFinished,

            /// <summary>
            /// 确认完成
            /// </summary>
            [Description("99")]
            Confirmed,

            /// <summary>
            /// 强制完成
            /// </summary>
            [Description("100")]
            ForceFinished,

            /// <summary>
            /// 已删除
            /// </summary>
            [Description("111")]
            Deleted
        }
        #endregion

        #region 出库记录状态
        /// <summary>
        /// 出库记录状态：0：初始创建；31：下架中；39：下架完成；40：待拣选；41：拣选中；90：拣选完成；92删除；93强制完成
        /// </summary>
        public enum OutRecordStatus
        {
            /// <summary>
            /// 初始创建
            /// </summary>
            [Description("0")]
            InitCreate,

            /// <summary>
            /// 下架中
            /// </summary>
            [Description("31")]
            OutPutDownIng,

            /// <summary>
            /// 下架完成
            /// </summary>
            [Description("39")]
            OutPutDownEnd,

            /// <summary>
            /// 待拣选
            /// </summary>
            [Description("40")]
            ToPick,

            /// <summary>
            /// 拣选中
            /// </summary>
            [Description("41")]
            PickIng,

            /// <summary>
            /// 拣选完成
            /// </summary>
            [Description("90")]
            PickEnd,

            /// <summary>
            /// 92删除
            /// </summary>
            [Description("92")]
            Delete,

            /// <summary>
            /// 强制完成
            /// </summary>
            [Description("93")]
            ForceFinished
        }

        #endregion

        #region 下架单，下架明细单
        /// <summary>
        /// 下架单、下架明细单，状态：0：初始创建；31：下架中；90：下架完成；92删除；93强制完成
        /// </summary>
        public enum PutDownOrDtlStatus
        {
            /// <summary>
            /// 初始创建
            /// </summary>
            [Description("0")]
            InitCreate,

            /// <summary>
            /// 下架中
            /// </summary>
            [Description("31")]
            OutPutDownIng,

            /// <summary>
            /// 下架完成
            /// </summary>
            [Description("90")]
            OutPutDownEnd,

            /// <summary>
            /// 92删除
            /// </summary>
            [Description("92")]
            Delete,

            /// <summary>
            /// 强制完成
            /// </summary>
            [Description("93")]
            ForceFinished
        }


        #endregion
        #endregion

        #region 库存
        /// <summary>
        /// 库存，库存明细状态：0：初始创建；20：入库中；50：在库；70：出库中；90：托盘出库完成(生命周期结束)；92删除（撤销）；93强制完成
        /// </summary>
        public enum StockStatus
        {
            /// <summary>
            /// 初始创建
            /// </summary>
            [Description("0")]
            InitCreate,

            /// <summary>
            /// 入库中
            /// </summary>
            [Description("20")]
            InStoreing,

            /// <summary>
            /// 在库
            /// </summary>
            [Description("50")]
            InStore,

            /// <summary>
            /// 出库中
            /// </summary>
            [Description("70")]
            OutStoreing,

            /// <summary>
            /// 托盘出库完成(生命周期结束)
            /// </summary>
            [Description("90")]
            OutStoreFinished,

            /// <summary>
            /// 删除
            /// </summary>
            [Description("92")]
            Deleted,

            /// <summary>
            /// 强制完成
            /// </summary>
            [Description("93")]
            ForceFinished
        }
        #endregion

        #region 移库单，移库明细，状态
        /// <summary>
        /// 移库单，移库明细单：0：初始创建；20出库中；90出库完成；92删除；
        /// </summary>
        public enum MoveOrDtlStatus
        {
            /// <summary>
            /// 初始创建
            /// </summary>
            [Description("0")]
            InitCreate,

            /// <summary>
            /// 出库中
            /// </summary>
            [Description("20")]
            OutStoreing,

            /// <summary>
            /// 出库完成
            /// </summary>
            [Description("90")]
            OutStoreFinished,

            /// <summary>
            /// 删除
            /// </summary>
            [Description("92")]
            Deleted,

            /// <summary>
            /// 强制完成
            /// </summary>
            [Description("93")]
            ForceFinished
        }
        #endregion

        #region 抽检单，抽检明细单，抽检记录，状态
        /// <summary>
        /// 抽检单，抽检明细，单抽检记录：0初始创建；11：已下发；66：待确认中；77：已确认；99：抽检完成；111：已删除；
        /// </summary>
        public enum IQCOrDtlOrRecordStatus
        {
            /// <summary>
            /// 初始创建
            /// </summary>
            [Description("0")]
            InitCreate,

            /// <summary>
            /// 已下发
            /// </summary>
            [Description("11")]
            Sended,

            /// <summary>
            /// 待确认中
            /// </summary>
            [Description("66")]
            WaitConfirming,

            /// <summary>
            /// 已确认
            /// </summary>
            [Description("77")]
            Confirmed,

            /// <summary>
            /// 抽检完成
            /// </summary>
            [Description("99")]
            IQCFinished,

            /// <summary>
            /// 已删除
            /// </summary>
            [Description("111")]
            Deleted
        }
        #endregion

        #region 盘点单，盘点明细单，盘点记录，状态
        /// <summary>
        /// 盘点单，盘点明细单：0：初始创建；22：开始盘点(任务下发)；55：盘点中；99：盘点完成；100：强制结束盘点；111：已删除；
        /// 盘点记录单：66：托盘到位；77：盘点操作完成
        /// </summary>
        public enum InventoryOrDtlOrRecordStatus
        {
            /// <summary>
            /// 初始创建
            /// </summary>
            [Description("0")]
            InitCreate,

            /// <summary>
            /// 开始盘点(任务下发)
            /// </summary>
            [Description("22")]
            StartInventory,

            /// <summary>
            /// 盘点中
            /// </summary>
            [Description("55")]
            Inventorying,


            /// <summary>
            /// 托盘到位(盘点记录单)
            /// </summary>
            [Description("66")]
            PalletArrived,

            /// <summary>
            /// 盘点操作完成(盘点记录单)
            /// </summary>
            [Description("77")]
            InventoryOperated,

            /// <summary>
            /// 盘点完成
            /// </summary>
            [Description("99")]
            Inventoryed,

            /// <summary>
            /// 强制结束盘点
            /// </summary>
            [Description("100")]
            ForceFinished,

            /// <summary>
            /// 已删除
            /// </summary>
            [Description("111")]
            Deleted
        }
        #endregion

        #region 任务状态
        /// <summary>
        /// 任务状态：0：初始创建；10：已下发；40：已反馈；90：任务完成；91：异常完成；92：已删除；93：强制完成
        /// </summary>
        public enum TaskStatus
        {
            /// <summary>
            /// 初始创建
            /// </summary>
            [Description("0")]
            Init,

            /// <summary>
            /// 已下发
            /// </summary>
            [Description("10")]
            Receipting,

            /// <summary>
            /// 已反馈
            /// </summary>
            [Description("40")]
            Returned,
           
            /// <summary>
            /// 任务完成
            /// </summary>
            [Description("90")]
            Finished,

            /// <summary>
            /// 任务完成
            /// </summary>
            [Description("91")]
            Exfinished,

            /// <summary>
            /// 删除
            /// </summary>
            [Description("92")]
            Delete,

            /// <summary>
            /// 强制完成
            /// </summary>
            [Description("93")]
            ForceFinish
        }
        #endregion
    }
}