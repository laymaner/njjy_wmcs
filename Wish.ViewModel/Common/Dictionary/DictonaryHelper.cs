using System;
using System.ComponentModel;

namespace WISH.Helper.Common.Dictionary.DictionaryHelper
{
    public partial class DictonaryHelper
    {

        /// <summary>
        /// 盘点方式
        /// </summary>
        public enum InventoryType
        {
            /// <summary>
            /// 全盘
            /// </summary>
            [Description("7001")] AllInventory,

            /// <summary>
            /// 抽盘
            /// </summary>
            [Description("7002")] SpotInventory,

            /// <summary>
            /// 动盘
            /// </summary>
            [Description("7003")] MoveInventory,

            /// <summary>
            /// 随机盘点
            /// </summary>
            [Description("7004")] RandomInventory,
        }

            /// <summary>
            /// 库位分配类型
            /// </summary>
        public enum AllotBinType
        {
            /// <summary>
            /// 平库
            /// </summary>
            [Description("PK")] PK,

            /// <summary>
            /// 托盘库
            /// </summary>
            [Description("TPK")] TPK,

            /// <summary>
            /// 料箱库
            /// </summary>
            [Description("LXK")] LXK
        }
        /// <summary>
        /// 数据来源字典
        /// </summary>
        public enum QueryType
        {
            /// <summary>
            /// 当前
            /// </summary>
            [Description("CUR")] Cur,

            /// <summary>
            /// 历史
            /// </summary>
            [Description("HIS")] His,

            /// <summary>
            /// 所有
            /// </summary>
            [Description("ALL")] All
        }

        /// <summary>
        ///库存变化字典
        /// </summary>
        public enum StockType
        {
            /// <summary>
            /// 当前
            /// </summary>
            [Description("0")] No,

            /// <summary>
            /// 历史
            /// </summary>
            [Description("1")] Yes,

        }

        /// <summary>
        /// 物料标记字典
        /// </summary>
        public enum MaterialFlag
        {
            /// <summary>
            /// 成品
            /// </summary>
            [Description("1")] Product,

            /// <summary>
            /// 电子料
            /// </summary>
            [Description("2")] Electronic,

            // 其他
            /// <summary>
            /// 其他
            /// </summary>
            [Description("3")] Other
        }

        /// <summary>
        /// 电子料标记（不仅仅是电子料,用作物料类型标记）
        /// </summary>
        public enum ElectronicMaterialFlag
        {
            /// <summary>
            /// 1成品
            /// </summary>
            [Description("1")] Product,

            /// <summary>
            /// 2电子料
            /// </summary>
            [Description("2")] Electronic,

            /// <summary>
            /// 3其他(原材料、半成品)
            /// </summary>
            [Description("3")] Other,
        }

        /// <summary>
        /// 基础入库单类型
        /// </summary>
        public enum InOrderDocType
        {
            /// <summary>
            /// 采购入库
            /// </summary>
            [Description("1001")] PO,

            /// <summary>
            /// 委外加工
            /// </summary>
            [Description("1030")] OEM,
        }


        ///// <summary>
        ///// 库区标记
        ///// </summary>
        //public enum WRgionTypeFlagCode
        //{
        //    /// <summary>
        //    /// 暂存区
        //    /// </summary>
        //    [Description("ZC")] ZC,

        //    /// <summary>
        //    /// 拣选区
        //    /// </summary>
        //    [Description("PK")] PK,

        //    /// <summary>
        //    /// 存拣区
        //    /// </summary>
        //    [Description("SP")] SP,

        //    /// <summary>
        //    /// 存储区
        //    /// </summary>
        //    [Description("ST")] ST
        //}

        /// <summary>
        /// 库区设备标记
        /// </summary>
        public enum WRegionDeviceFlag
        {
            /// <summary>
            /// 托盘库
            /// </summary>
            [Description("1")] Pallet,

            /// <summary>
            /// 料箱库
            /// </summary>
            [Description("2")] Box,

            /// <summary>
            /// 平库
            /// </summary>
            [Description("3")] Flat
        }

        /// <summary>
        /// 入库单据类型基础参数
        /// </summary>
        public enum InOrderDocTypeParam
        {
            /// <summary>
            /// 自动收获
            /// </summary>
            [Description("AUTO_RECEIPT")] AutoReceipt,

            /// <summary>
            /// 自动打印
            /// </summary>
            [Description("Print")] Print,

            /// <summary>
            /// 自动生成唯一码
            /// </summary>
            [Description("IS_AUTO_CREATE_UNII")] GenerateUnnicode,

            /// <summary>
            /// 自动质检验收
            /// </summary>
            [Description("AUTO_CHECK")] AutoQC,

            /// <summary>
            /// 回传SRM
            /// </summary>
            [Description("FeedbackSRM")] ReturnFlag,

            /// <summary>
            /// 单据可以修改
            /// </summary>
            [Description("IS_EDIT")] Editable,
            /// <summary>
            /// 是否批次管理
            /// </summary>
            [Description("IS_BATCH")] IsBatch,
            /// <summary>
            /// 是否上架变更
            /// </summary>
            [Description("IS_CHANGE")] IsChange,
        }

        /// <summary>
        /// 出库单据类型基础参数
        /// </summary>
        public enum outOrderDocTypeParam
        {
            /// <summary>
            /// 创建单据
            /// </summary>
            [Description("IS_CREATE")] IsCreate,

            /// <summary>
            /// 检查库存
            /// </summary>
            [Description("IS_CHECK_STOCK")] IsCheckStock,

            /// <summary>
            /// 单据可以修改
            /// </summary>
            [Description("IS_EDIT")] IsEdit,

            /// <summary>
            /// 单据可以关闭
            /// </summary>
            [Description("IS_CLOSE")] IsClose,
        }

        /// <summary>
        /// 质量标记;0：待检；1：合格；2：不合格；3：免检；4：冻结
        /// </summary>
        public enum QcFlag
        {
            /// <summary>
            /// 待检
            /// </summary>
            [Description("0")] WaitQC,

            /// <summary>
            /// 合格
            /// </summary>
            [Description("1")] Pass,

            /// <summary>
            /// 不合格
            /// </summary>
            [Description("2")] NG,

            /// <summary>
            /// 免检
            /// </summary>
            [Description("3")] Exempt,

            /// <summary>
            /// 冻结
            /// </summary>
            [Description("4")] Freeze,

            /// <summary>
            /// 特采(ERP)
            /// </summary>
            [Description("5")] ErpAod,

            /// <summary>
            /// 不合格(ERP)
            /// </summary>
            [Description("6")] ErpNg,
        }


        /// <summary>
        /// 参数配置值Yes:1. No:0
        /// </summary>
        public enum YesNoCode
        {
            [Description("1")] Yes,
            [Description("0")] No
        }


        public enum SequenceCode
        {
            /// <summary>
            /// 批次编号
            /// </summary>
            [Description("SRM_BATCH_NO")] SrmBatchNo,

            /// <summary>
            /// 包装条码/序列号==唯一码
            /// </summary>
            [Description("UNIICODE")] PackageNo,

            /// <summary>
            /// 入库单号
            /// </summary>
            [Description("IN_NO")] InOrderNo,

            /// <summary>
            /// 收货单号
            /// </summary>
            [Description("IN_RECEIPT_NO")] InReceiptNo,

            /// <summary>
            /// 收货暂存库存单号
            /// </summary>
            [Description("STOCK_CODE")] InReceiptTStockNo,

            /// <summary>
            /// 质检录入
            /// </summary>
            [Description("IN_RECEIPT_QC_NO")] InReceiptQCNo,


            /// <summary>
            /// 质检结果操作
            /// </summary>
            [Description("IN_RECEIPT_QC_RECORD_NO")]
            InReceiptQCRecordNo,

            /// <summary>
            /// 发货单号
            /// </summary>
            [Description("INVOICE_NO")] InvoiceOrderNo,

            /// <summary>
            /// 波次号
            /// </summary>
            [Description("WAVE_NO")] WaveNo,

            /// <summary>
            /// 拣选单号
            /// </summary>
            [Description("OUT_NO")] PickNo,

            /// <summary>
            /// 下架单号
            /// </summary>
            [Description("PUT_DOWN_NO")] WmsPutdownNo,

            /// <summary>
            /// 出库条码
            /// </summary>
            [Description("OUT_BARCODE")] OutBarCode,

            /// <summary>
            /// 上架单号
            /// </summary>
            [Description("PUT_DOWN_NO")] WmsPutawayNo,

            /// <summary>
            /// 库存编号
            /// </summary>
            [Description("STOCK_CODE")] StockCode,

            /// <summary>
            /// 库存差异单号
            /// </summary>
            [Description("STOCK_DIF")] ReconcileNo,

            /// <summary>
            /// 移库单号
            /// </summary>
            [Description("MOVE_NO")] MoveNo,
            /// <summary>
            ///WMS任务号
            /// </summary>
            [Description("WMS_TASK_NO")] wmsTaskNo,


            /// <summary>
            ///抽检单号
            /// </summary>
            [Description("ITN_QC_NO")] itnQcNo,

            /// <summary>
            ///盘点单号
            /// </summary>
            [Description("INVENTORY_NO")] inventoryNo,

            /// <summary>
            ///SRM指令
            /// </summary>
            [Description("SRM")] srmCmdNo,
            /// <summary>
            /// 托盘号
            /// </summary>
            [Description("CT")] palletBarCode,
        }

        /// <summary>
        /// 反馈代码
        /// </summary>
        public enum ResCode
        {
            OK,
            Error
        }

        /// <summary>
        /// 业务类型(单据类型与这个业务类型一一对应)
        /// </summary>
        public enum BusinessCode
        {
            /// <summary>
            /// 入库业务
            /// </summary>
            [Description("IN")] In,

            /// <summary>
            /// 出库业务
            /// </summary>
            [Description("OUT")] Out,

            /// <summary>
            /// 质检业务
            /// </summary>
            [Description("QC")] Qc,

            // *********  下面的是入库业务 **********

            /// <summary>
            /// 采购入库
            /// </summary>
            [Description("1001")] InPurchase,
            /// <summary>
            /// 委外采购入库
            /// </summary>
            [Description("1030")] OEM,

            /// <summary>
            /// 客供品入库
            /// </summary>
            [Description("1002")] InCusSup,

            /// <summary>
            /// 其它入库单
            /// </summary>
            [Description("1003")] InOther,

            /// <summary>
            /// 免费服务调拨入
            /// </summary>
            [Description("1004")] InFreeSerTran,

            /// <summary>
            /// 收费服务入库单
            /// </summary>
            [Description("1005")] InBillSer,

            /// <summary>
            /// 免费服务入库单
            /// </summary>
            [Description("1006")] InFreeSer,

            /// <summary>
            /// 改造退料
            /// </summary>
            [Description("1007")] InRetrofitReturn,

            /// <summary>
            /// 借入借用单
            /// </summary>
            [Description("1008")] InBorrowingSlip,

            /// <summary>
            /// 销售退货
            /// </summary>
            [Description("1012")] InSalesReturn,

            /// <summary>
            /// 辅料入库单
            /// </summary>
            [Description("1014")] InAccessories,

            /// <summary>
            /// WMS手工创建入库
            /// </summary>
            [Description("1015")] InWmsCreate,

            /// <summary>
            /// 成品生产完工入库单
            /// </summary>
            [Description("1016")] InProFinish,

            /// <summary>
            /// 半成品生产完工入库单
            /// </summary>
            [Description("1017")] InSemiProFinish,

            /// <summary>
            /// 生产退料入库单
            /// </summary>
            [Description("1018")] InProduceReturn,

            /// <summary>
            /// 拆卸入库单
            /// </summary>
            [Description("1019")] InDismantle,

            /// <summary>
            /// 副产品入库单
            /// </summary>
            [Description("1020")] InSecondProReturn,

            /// <summary>
            /// 客户现场退料单
            /// </summary>
            [Description("1021")] InCustomerSiteReturn,

            /// <summary>
            /// 辅料退回入库单
            /// </summary>
            [Description("1022")] InExcipientReturn,

            /// <summary>
            /// 委外退料单
            /// </summary>
            [Description("1023")] InOutSourceReturn,

            /// <summary>
            /// 调拨申请单
            /// </summary>
            [Description("1024")] InTransferRequest,

            /// <summary>
            /// 形态转换申请
            /// </summary>
            [Description("1025")] InPatternConversion,
            
            /// <summary>
            /// 借出归还单   Loan Return Form 这个其实是入库业务
            /// </summary>
            [Description("2018")] InLoanReturn,

            /*
            // 下面几个未有匹配的，暂放
            IN	0000	test
            IN	1009	借入转借入单
            IN	1010	特采采购入库
            IN	1011	工单退料
            */

            // ********** 下面的是出库业务 **********
            /// <summary>
            /// 发货单(销售发货出库)
            /// </summary>
            [Description("2001")] OutInvoice,

            /// <summary>
            /// 收费服务出库单 Paid Service Outbound Slip  
            /// </summary>
            [Description("2002")] OutPaidService,

            /// <summary>
            /// 免费服务出库单 Free Service Shipping Slip 
            /// </summary>
            [Description("2003")] OutFreeService,

            /// <summary>
            /// 其它出库单 Other Outbound Orders
            /// </summary>
            [Description("2004")] OutOther,

            /// <summary>
            /// 免费服务调拨出 Free Service Transfer Out 
            /// </summary>
            [Description("2006")] OutFreeServiceTran,

            /// <summary>
            /// 借入归还单   Borrow Return Form 
            /// </summary>
            [Description("2007")] OutBorrowReturn,

            /// <summary>
            ///  辅料出库单   Excipient Outbound Order 
            /// </summary>
            [Description("2008")] OutExcipient,

            /// <summary>
            /// 客供品出库单  Customer Offering Shipping Slip 
            /// </summary>
            [Description("2009")] OutCustomerSup,

            /// <summary>
            /// 改造领料 Retrofit Picking 
            /// </summary>
            [Description("2010")] OutRetrofit,

            /// <summary>
            /// 委外领料单   Outsourcing Picking List
            /// </summary>
            [Description("2013")] OutOutSourcePick,

            /// <summary>
            /// 生产工单发料单 Production Work Order Issuing Order 
            /// </summary>
            [Description("2014")] OutProduceOrder,

            /// <summary>
            /// 生产补料领料单 Production Feed Picking List 
            /// </summary>
            [Description("2015")] OutProduceFeed,

            /// <summary>
            /// 生产辅料领料单 Production Accessories Picking List 
            /// </summary>
            [Description("2016")] OutProduceAccessories,

            /// <summary>
            /// 借出借用单   Loan Slip 
            /// </summary>
            [Description("2017")] OutLoanSlip,

            /// <summary>
            /// 调拨申请单   Transfer Request Form 
            /// </summary>
            [Description("2019")] OutTransferRequest,

            /// <summary>
            /// 形态转换申请  Pattern Shift Application 
            /// </summary>
            [Description("2020")] OutPatternShift,

            /// <summary>
            /// WMS手工创建出库单  WMS manually creates outbound orders
            /// </summary>
            [Description("2021")] OutWmsCreate,

            /// <summary>
            /// 采购退货单 这个其实是出库业务
            /// </summary>
            [Description("2022")] OutPurchaseReturn,

            /*
            OUT	2005	借出转消耗单  
            OUT	2012	测试  
            */
        }

        /// <summary>
        /// 业务模块
        /// </summary>
        public enum BusinessModuleCode
        {
            /// <summary>
            /// 入库单据
            /// </summary>
            [Description("IN_ORDER")] InOrder,

            /// <summary>
            /// 收获操作
            /// </summary>
            [Description("RECEIPT")] Receipt,

            /// <summary>
            /// 质检操作
            /// </summary>
            [Description("IQC")] IQC,

            // ********** 下面的是出库业务模块 **********

            /// <summary>
            /// 出库单据
            /// </summary>
            [Description("OUT_ORDER")] OUT_ORDER,

            /// <summary>
            /// 波次管理
            /// </summary>
            [Description("WAVE_MSG")] WAVE_MSG,

            /// <summary>
            /// 分配操作
            /// </summary>
            [Description("ALLOT")] ALLOT,

            /// <summary>
            /// 下发操作
            /// </summary>
            [Description("DOWN")] DOWN,

            /// <summary>
            /// 下架操作
            /// </summary>
            [Description("OUT_PUTDOWN")] OUT_PUTDOWN,

            /// <summary>
            /// 拣选操作
            /// </summary>
            [Description("PICK")] PICK,

            /// <summary>
            /// 发货操作
            /// </summary>
            [Description("DELIVERY")] DELIVERY,
        }

        /// <summary>
        /// 出库业务模块参数(对于是否这样的取值用这个 BusinessParamValueYesNoValueCode )
        /// </summary>
        public enum BusinessModuleParam
        {
            /// <summary>
            /// 是否供应商对应库位发料
            /// </summary>
            [Description("IS_SUPPLIER_BIN")] IS_SUPPLIER_BIN,

            /// <summary>
            /// 是否允许拣选撤消
            /// </summary>
            [Description("IS_PICK_UNDO")] IS_PICK_UNDO,
        }


        /// <summary>
        /// 托盘类型扩展,1=托盘,2=料箱
        /// </summary>
        public enum PalletTypeExtend
        {
            /// <summary>
            /// 托盘
            /// </summary>
            [Description("PL")] Pallet,

            /// <summary>
            /// 料箱
            /// </summary>
            [Description("BX")] Box,

            /// <summary>
            /// 钢托盘
            /// </summary>
            [Description("ST")] Steel,

            /// <summary>
            /// 无
            /// </summary>
            [Description("3")] UnKnown,
        }

        /// <summary>
        /// 区域类型
        /// </summary>
        //public enum RegionType
        //{
        //    /// <summary>
        //    /// 托盘库
        //    /// </summary>
        //    PalletArea,

        //    /// <summary>
        //    /// 料箱库
        //    /// </summary>
        //    BoxArea,

        //    /// <summary>
        //    /// 平库
        //    /// </summary>
        //    PlatArea,
        //}
        public enum BaseCfgRelationshipCode
        {
            /// <summary>
            /// 物料大类与托盘类型对应关系
            /// </summary>
            [Description("MaterialCatelogType&PalletType")]
            materialCatelogTypeVsPalletType
        }

        public enum SourceBy
        {
            /// <summary>
            /// 外部系统接口
            /// </summary>
            [Description("1")] ExtInterface,

            /// <summary>
            /// Wms系统
            /// </summary>
            [Description("0")] WMS,
        }

        /// <summary>
        /// 回传状态：0默认，1可回传，2回传失败，3回传成功，4无需回传;回传SRM用长度不能超出50字符
        /// </summary>
        public enum InOrderReturnFlag
        {
            /// <summary>
            /// 默认
            /// </summary>
            [Description("0")] InitCreate,

            /// <summary>
            /// 可回传
            /// </summary>
            [Description("1")] WaitReturn,

            /// <summary>
            /// 回传失败
            /// </summary>
            [Description("2")] ReturnFail,

            /// <summary>
            /// 回传成功
            /// </summary>
            [Description("3")] ReturnOk,

            /// <summary>
            /// 无需回传
            /// </summary>
            [Description("4")] NoReturn
        }


        public enum InReceiptReturnFlag
        {
            /// <summary>
            /// 默认
            /// </summary>
            [Description("0")] InitCreate,

            /// <summary>
            /// 可回传
            /// </summary>
            [Description("1")] WaitReturn,

            /// <summary>
            /// 回传失败
            /// </summary>
            [Description("2")] ReturnFail,

            /// <summary>
            /// 回传成功
            /// </summary>
            [Description("3")] ReturnOk,

            /// <summary>
            /// 无需回传
            /// </summary>
            [Description("4")] NoReturn
        }

        /// <summary>
        /// 删除标志;0-有效,1-已删除
        /// </summary>
        public enum DelFlag
        {
            /// <summary>
            /// 有效
            /// </summary>
            [Description("0")] NDelete,

            /// <summary>
            /// 已删除
            /// </summary>
            [Description("1")] Delete,
        }

        public enum WmsStockAdjustType
        {
            /// <summary>
            /// 新增
            /// </summary>
            [Description("1")] New,

            /// <summary>
            /// 修改
            /// </summary>
            [Description("2")] Modify,

            /// <summary>
            /// 删除
            /// </summary>
            [Description("3")] Delete,
        }

        public enum InReceiptIqcType
        {
            /// <summary>
            /// WMS质检
            /// </summary>
            //[Description("1")]
            [Description("ITN")] WMS,

            /// <summary>
            /// ERP质检
            /// </summary>
            //[Description("2")]
            [Description("QCS")] ERP
        }

        /// <summary>
        /// 出库手动分配类型
        /// </summary>
        public enum OutInvoiceManuAllocateType
        {
            /// <summary>
            /// 库存明细分配
            /// </summary>
            [Description("ByInvoiceDtlId")] ByStockDtlId,

            /// <summary>
            /// 唯一码分配
            /// </summary>
            [Description("ByUniicode")] ByUniicode,
        }

        /// <summary>
        /// 客供类型：S 供应商，P 产线，C 客户
        /// </summary>
        public enum CustomerSupplierType
        {
            /// <summary>
            /// 客户
            /// </summary>
            [Description("C")] Customer,

            /// <summary>
            /// 供应商
            /// </summary>
            [Description("S")] Supplier,

            /// <summary>
            /// 产线
            /// </summary>
            [Description("P")] ProductLine,
        }

        public enum ProgramConst
        {
            /// <summary>
            /// while死循环次数检查
            /// </summary>
            [Description("1000")] DeadWhileCheckTime = 1000,
        }

        ///// <summary>
        ///// 库存锁定状态
        ///// </summary>
        //public enum StockLockFlag
        //{
        //    /// <summary>
        //    /// 锁定
        //    /// </summary>
        //    [Description("1")]
        //    Lock,

        //    /// <summary>
        //    /// 未锁定
        //    /// </summary>
        //    [Description("0")]
        //    UnLock,
        //}

        ///// <summary>
        ///// 有效期冻结
        ///// </summary>
        //public enum StockDelayFrozenFlag
        //{

        //    /// <summary>
        //    /// 冻结
        //    /// </summary>
        //    [Description("1")]
        //    Lock,

        //    /// <summary>
        //    /// 非冻结
        //    /// </summary>
        //    [Description("0")]
        //    UnLock,
        //}

        ///// <summary>
        ///// 暴露冻结
        ///// </summary>
        //public enum StockExposeFrozenFlag
        //{
        //    /// <summary>
        //    /// 冻结
        //    /// </summary>
        //    [Description("1")]
        //    Lock,

        //    /// <summary>
        //    /// 非冻结
        //    /// </summary>
        //    [Description("0")]
        //    UnLock,
        //}

        ///// <summary>
        ///// 烘干报废标记
        ///// </summary>
        //public enum StockDriedScrapFlag
        //{
        //    /// <summary>
        //    /// 报废
        //    /// </summary>
        //    [Description("1")]
        //    Scra,

        //    /// <summary>
        //    /// 非报废
        //    /// </summary>
        //    [Description("0")]
        //    UnScra,

        //}

        /// <summary>
        /// 分配方式：自动分配；手动分配；波次分配
        /// </summary>
        public enum AllotType
        {
            /// <summary>
            /// 自动分配
            /// </summary>
            [Description("1")] Auto,

            /// <summary>
            /// 手动分配
            /// </summary>
            [Description("2")] Manu,

            /// <summary>
            /// 波次分配
            /// </summary>
            [Description("3")] Wave
        }

        //public enum AllocateBatchFlag
        //{
        //    /// <summary>
        //    /// 批次分配
        //    /// </summary>
        //    [Description("1")]
        //    Yes,

        //    /// <summary>
        //    /// 非批次分配
        //    /// </summary>
        //    [Description("0")]
        //    No
        //}

        ///// <summary>
        ///// 反拣标记：0不反拣，1反拣
        ///// </summary>
        //public enum ReversePickFlag
        //{

        //    /// <summary>
        //    /// 反拣
        //    /// </summary>
        //    [Description("1")]
        //    Yes,

        //    /// <summary>
        //    /// 不反拣
        //    /// </summary>
        //    [Description("0")]
        //    No
        //}

        /// <summary>
        /// 拣选类型
        /// </summary>
        public enum PickType
        {
            /// <summary>
            /// 拣选
            /// </summary>
            [Description("1")] Pick,

            /// <summary>
            /// 整出
            /// </summary>
            [Description("2")] All
        }

        /// <summary>
        /// 托盘拣选类型
        /// </summary>
        public enum PalletPickType
        {
            /// <summary>
            /// 拣选
            /// </summary>
            [Description("1")] Pick,

            /// <summary>
            /// 整出
            /// </summary>
            [Description("2")] All
        }


        /// <summary>
        /// 操作方式
        /// </summary>
        public enum OperationMode
        {
            /// <summary>
            /// 合并拣选
            /// </summary>
            [Description("1")] CombindPick,

            /// <summary>
            /// 单任务拣选
            /// </summary>
            [Description("0")] SinglePick
        }

        /// <summary>
        /// 质检结果
        /// </summary>
        public enum InspectionResult
        {
            /// <summary>
            /// 待检
            /// </summary>
            [Description("0")] WaitInspection,

            /// <summary>
            /// 合格
            /// </summary>
            [Description("10")] Qualitified,

            /// <summary>
            /// 不合格
            /// </summary>
            [Description("20")] UnQualitified,

            /// <summary>
            /// 特采
            /// </summary>
            [Description("30")] AcceptOnDeviation
        }


        public enum ExtInterface
        {
            /// <summary>
            /// 收货回传
            /// </summary>
            SRM001,
        }

        /// <summary>
        /// 电子料邮件类型
        /// </summary>
        public enum WarnType
        {
            /// <summary>
            /// 预警
            /// </summary>
            [Description("电子料预警")] ElecWarn,

            /// <summary>
            /// 冻结
            /// </summary>
            [Description("电子料冻结")] ElecFrozen,

        }
    }

    /// <summary>
    /// 状态标记
    /// </summary>
    public class IntValueAttribute : Attribute
    {
        public IntValueAttribute(int intVal)
        {
            this.intVal = intVal;
        }

        public int intVal { get; set; }
    }

    public static class EnumExtensions
    {
        public static string GetCode(this Enum val)
        {
            var field = val.GetType().GetField(val.ToString());
            var customAttribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return customAttribute == null ? val.ToString() : ((DescriptionAttribute)customAttribute).Description;
        }

        public static int IntVal(this Enum val)
        {
            var field = val.GetType().GetField(val.ToString());
            var customAttribute = Attribute.GetCustomAttribute(field, typeof(IntValueAttribute));
            return customAttribute == null ? 0 : ((IntValueAttribute)customAttribute).intVal;
        }
    }
}