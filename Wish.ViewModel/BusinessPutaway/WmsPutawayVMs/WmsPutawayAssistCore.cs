using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Wish.ViewModel.Base.BasBMaterialVMs;

namespace Wish.ViewModel.BusinessPutaway.WmsPutawayVMs
{
    public partial class WmsPutawayVM
    {
        /// <summary>
        /// 构建库存明细表
        /// </summary>
        /// <param name="wmsStock"></param>
        /// <param name="item"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        private WmsStockDtl BuildWmsStockDtl(WmsStock wmsStock, WmsInReceiptRecord item, string invoker)
        {
            WmsStockDtl wmsStockDtl = new WmsStockDtl();
            wmsStockDtl.whouseNo = item.whouseNo; // 仓库号
            //wmsStockDtl.areaNo = item.areaNo; // 区域编码(楼号)
            wmsStockDtl.areaNo = wmsStock.areaNo; // 区域编码(楼号)
            //wmsStockDtl.batchNo = item.batchNo; // 批次
            //wmsStockDtl.delFlag = 0; // 删除标志;0-有效,1-已删除
            wmsStockDtl.erpWhouseNo = item.erpWhouseNo; // ERP仓库
            wmsStockDtl.inspectionResult = item.inspectionResult; // 质量标记;0：待检；1：合格；2：不合格；3：免检；4：冻结；
            wmsStockDtl.lockFlag = 0; // 锁定状态;0：未锁定库存；1：已锁定库存。
            wmsStockDtl.lockReason = ""; // 锁定原因
            wmsStockDtl.materialName = item.materialName; // 物料名称
            wmsStockDtl.materialCode = item.materialCode; // 物料编码
            wmsStockDtl.materialSpec = item.materialSpec; // 规格型号
            wmsStockDtl.occupyQty = 0; // 占用数量
            wmsStockDtl.palletBarcode = wmsStock.palletBarcode; // 载体条码
            wmsStockDtl.projectNo = item.projectNo; // 项目号
            wmsStockDtl.projectNoBak = item.projectNo; // 项目号
            wmsStockDtl.proprietorCode = item.proprietorCode; // 货主
            wmsStockDtl.qty = item.recordQty; // 库存数量
            wmsStockDtl.skuCode = item.skuCode; // SKU编码
            wmsStockDtl.stockCode = wmsStock.stockCode; // 库存编码
            wmsStockDtl.stockDtlStatus = Convert.ToInt32(StockStatus.InitCreate.GetCode()); // 库存明细状态;0：初始创建；20：入库中；50：在库；70：出库中；90：托盘出库完成(生命周期结束)；92删除（撤销）；93强制完成
            wmsStockDtl.supplierCode = item.supplierCode; // 供应商编码
            wmsStockDtl.supplierName = item.supplierName; // 供方名称
            wmsStockDtl.supplierNameAlias = item.supplierNameAlias; // 供方名称-其他
            wmsStockDtl.supplierNameEn = item.supplierNameEn; // 供方名称-英文
            wmsStockDtl.extend1 = null; // 扩展字段1
            wmsStockDtl.extend2 = null; // 扩展字段2
            wmsStockDtl.extend3 = null; // 扩展字段3
            wmsStockDtl.extend4 = null; // 扩展字段4
            wmsStockDtl.extend5 = null; // 扩展字段5
            wmsStockDtl.extend6 = null; // 扩展字段6
            wmsStockDtl.extend7 = null; // 扩展字段7
            wmsStockDtl.extend8 = null; // 扩展字段8
            wmsStockDtl.extend9 = null; // 扩展字段9
            wmsStockDtl.extend10 = null; // 扩展字段10
            wmsStockDtl.extend11 = null; // 扩展字段11
            wmsStockDtl.chipSize = null; // 扩展字段12
            wmsStockDtl.chipThickness = null; // 扩展字段13
            wmsStockDtl.chipModel = null; // 扩展字段14
            wmsStockDtl.dafType = null; // 扩展字段15
            wmsStockDtl.unitCode = item.unitCode; // 急料标记
            //wmsStockDtl.urgentFlag = item.urgentFlag; // 急料标记
            wmsStockDtl.CreateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
            wmsStockDtl.UpdateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
            wmsStockDtl.CreateTime = DateTime.Now;
            wmsStockDtl.UpdateTime = DateTime.Now;
            //wmsStockDtl.inwhTime = DateTime.Now;
            return wmsStockDtl;
        }

        private WmsStockUniicode BuildWmsStockUniicode(decimal qty, WmsInReceiptUniicode inReceiptUniicode, WmsStockUniicode stockUniicode, WmsInReceiptIqcResult iqcResult, WmsStockDtl wmsStockDtl, string invoker, string palletCode)
        //private WmsStockUniicode BuildWmsStockUniicode(decimal qty, WmsInReceiptUniicode inReceiptUniicode, WmsInReceiptIqcResult iqcResult,WmsStockDtl wmsStockDtl, string invoker)
        {
            WmsStockUniicode wmsStockUniicode = new WmsStockUniicode();
            wmsStockUniicode.whouseNo = iqcResult.whouseNo; // 仓库号
            //wmsStockUniicode.areaNo = iqcResult.areaNo; // 区域编码(楼号)
            wmsStockUniicode.areaNo = wmsStockDtl?.areaNo ?? iqcResult.areaNo; // 区域编码(楼号)
            //wmsStockUniicode.batchNo = iqcResult.batchNo; // 批次
            wmsStockUniicode.batchNo = inReceiptUniicode.batchNo; // 批次
            wmsStockUniicode.dataCode = stockUniicode?.dataCode ?? inReceiptUniicode.dataCode; // DATACODE
            wmsStockUniicode.delayFrozenFlag = stockUniicode?.delayFrozenFlag ?? 0; // null; // 有效期冻结
            wmsStockUniicode.delayFrozenReason = stockUniicode?.delayFrozenReason; //null; // 有效期冻结原因
            wmsStockUniicode.delayReason = stockUniicode?.delayReason; // null; // 延长有效期原因
            wmsStockUniicode.delayTimes = stockUniicode?.delayTimes ?? 0; //null; // 延期次数
            wmsStockUniicode.delayToEndDate = stockUniicode?.delayToEndDate; //null; // 延长有效期至日期
            //wmsStockUniicode.delFlag = "0"; // 删除标志;0-有效,1-已删除
            wmsStockUniicode.driedScrapFlag = stockUniicode?.driedScrapFlag ?? 0; //null; // 是否烘干报废;0-有效,1-已报废
            wmsStockUniicode.driedTimes = stockUniicode?.driedTimes ?? 0; //null; // 已烘干次数
            wmsStockUniicode.erpWhouseNo = iqcResult.erpWhouseNo; // ERP仓库
            wmsStockUniicode.expDate = stockUniicode?.expDate ?? inReceiptUniicode.expDate; // 失效日期
            wmsStockUniicode.exposeFrozenFlag = stockUniicode?.exposeFrozenFlag ?? 0; //null; // 暴露冻结
            wmsStockUniicode.exposeFrozenReason = stockUniicode?.exposeFrozenReason; //null; // 暴露冻结原因
            wmsStockUniicode.inspectionResult = iqcResult.inspectionResult; // 质量标记;0：待检；1：合格；2：不合格；3：免检；4：冻结；
            wmsStockUniicode.inwhTime = DateTime.Now; // 入库时间
            wmsStockUniicode.leftMslTimes = stockUniicode?.leftMslTimes; //null; // 剩余湿敏时长
            wmsStockUniicode.materialName = iqcResult.materialName; // 物料名称
            wmsStockUniicode.materialCode = iqcResult.materialCode; // 物料编码
            wmsStockUniicode.materialSpec = iqcResult.materialSpec; // 规格型号
            wmsStockUniicode.mslGradeCode = inReceiptUniicode.mslGradeCode; // MSL等级编码
            wmsStockUniicode.occupyQty = 0; // 占用数量
            wmsStockUniicode.packageTime = stockUniicode?.packageTime; //
            //null; // 封包时间;如果需要封包日期从这格式化取                                                                                                
            wmsStockUniicode.palletBarcode = wmsStockDtl?.palletBarcode ?? palletCode; // 载体条码
            wmsStockUniicode.positionNo = inReceiptUniicode.curPositionNo; // 所在载体位置
            wmsStockUniicode.productDate = stockUniicode?.productDate; // 生产日期
            wmsStockUniicode.projectNo = iqcResult.projectNo; // 项目号
            wmsStockUniicode.proprietorCode = iqcResult.proprietorCode; // 货主
            wmsStockUniicode.realExposeTimes = stockUniicode?.realExposeTimes; //null; // 实际暴露时长
            wmsStockUniicode.skuCode = iqcResult.skuCode; // SKU编码
            wmsStockUniicode.stockCode = wmsStockDtl?.stockCode; // 库存编码
            wmsStockUniicode.stockDtlId = (long)wmsStockDtl?.ID; // 库存明细ID
            wmsStockUniicode.qty = qty; // 库存数量
            wmsStockUniicode.supplierCode = inReceiptUniicode.supplierCode; // 供应商编码
            wmsStockUniicode.supplierExposeTimes = inReceiptUniicode.supplierExposeTimeDuration; // 供应商暴露时长
            wmsStockUniicode.supplierNameAlias = iqcResult.supplierNameAlias; // 供应商名称-其他
            wmsStockUniicode.supplierNameEn = iqcResult.supplierNameEn; // 供应商名称-英文
            wmsStockUniicode.supplierName = iqcResult.supplierName; // 供应商名称
            wmsStockUniicode.uniicode = inReceiptUniicode.uniicode; // 库存唯一码
            wmsStockUniicode.unitCode = inReceiptUniicode.unitCode; // 单位
            wmsStockUniicode.unpackStatus = stockUniicode?.unpackStatus; //null; // 拆封状态;0:已封包，1:已开包
            wmsStockUniicode.unpackTime = stockUniicode?.unpackTime; //null; // 开封时间;如果需要开封日期从这格式化取
            wmsStockUniicode.extend1 = stockUniicode?.extend1; //null; // 扩展字段1
            wmsStockUniicode.extend2 = null; // 扩展字段2
            wmsStockUniicode.extend3 = null; // 扩展字段3
            wmsStockUniicode.extend4 = null; // 扩展字段4
            wmsStockUniicode.extend5 = null; // 扩展字段5
            wmsStockUniicode.extend6 = null; // 扩展字段6
            wmsStockUniicode.extend7 = null; // 扩展字段7
            wmsStockUniicode.extend8 = null; // 扩展字段8
            wmsStockUniicode.extend9 = null; // 扩展字段9
            wmsStockUniicode.extend10 = null; // 扩展字段10
            wmsStockUniicode.extend11 = null; // 扩展字段11
            wmsStockUniicode.chipSize = null; // 扩展字段12
            wmsStockUniicode.chipThickness = null; // 扩展字段13
            wmsStockUniicode.chipModel = null; // 扩展字段14
            wmsStockUniicode.dafType = null; // 扩展字段15
            wmsStockUniicode.CreateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
            wmsStockUniicode.CreateTime = DateTime.Now;
            wmsStockUniicode.UpdateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
            wmsStockUniicode.UpdateTime = DateTime.Now;
            return wmsStockUniicode;
        }

        private WmsStockUniicode BuildWmsReceiptStockUniicode(decimal qty, WmsInReceiptUniicode inReceiptUniicode, WmsInReceiptIqcResult iqcResult,
            string invoker)
        {
            BasBMaterialVM basBMaterialApiVM = Wtm.CreateVM<BasBMaterialVM>();
            var _basBMaterialView = basBMaterialApiVM.GetBasBMaterial(inReceiptUniicode.materialCode);
            if (_basBMaterialView == null || _basBMaterialView.basBMaterialCategory == null ||
                _basBMaterialView.basBMaterial == null)
            {
                throw new Exception($"物料信息 {inReceiptUniicode.materialCode} 维护不完整!");
            }
            int? unpackStatus = null;
            if (_basBMaterialView.basBMaterialCategory.materialFlag != null && _basBMaterialView.basBMaterialCategory.materialFlag == MaterialFlag.Electronic.GetCode())
            {
                unpackStatus = 0;
            }
            WmsStockUniicode wmsStockUniicode = new WmsStockUniicode();
            wmsStockUniicode.whouseNo = iqcResult.whouseNo; // 仓库号
            wmsStockUniicode.areaNo = iqcResult.areaNo; // 区域编码(楼号)
            wmsStockUniicode.batchNo = inReceiptUniicode.batchNo; // 批次
            wmsStockUniicode.dataCode = inReceiptUniicode.dataCode; // DATACODE
            wmsStockUniicode.delayFrozenFlag = 0; // null; // 有效期冻结
            wmsStockUniicode.delayFrozenReason = null; //null; // 有效期冻结原因
            wmsStockUniicode.delayReason = null; // null; // 延长有效期原因
            wmsStockUniicode.delayTimes = 0; //null; // 延期次数
            wmsStockUniicode.delayToEndDate = null; //null; // 延长有效期至日期
            //wmsStockUniicode.delFlag = "0"; // 删除标志;0-有效,1-已删除
            wmsStockUniicode.driedScrapFlag = 0; //null; // 是否烘干报废;0-有效,1-已报废
            wmsStockUniicode.driedTimes = 0; //null; // 已烘干次数
            wmsStockUniicode.erpWhouseNo = iqcResult.erpWhouseNo; // ERP仓库
            wmsStockUniicode.expDate = inReceiptUniicode.expDate; // 失效日期
            wmsStockUniicode.exposeFrozenFlag = 0; //null; // 暴露冻结
            wmsStockUniicode.exposeFrozenReason = null; //null; // 暴露冻结原因
            wmsStockUniicode.inspectionResult = iqcResult.inspectionResult; // 质量标记;0：待检；1：合格；2：不合格；3：免检；4：冻结；
            wmsStockUniicode.inwhTime = DateTime.Now; // 入库时间
            wmsStockUniicode.leftMslTimes = 0; //null; // 剩余湿敏时长
            wmsStockUniicode.materialName = iqcResult.materialName; // 物料名称
            wmsStockUniicode.materialCode = iqcResult.materialCode; // 物料编码
            wmsStockUniicode.materialSpec = iqcResult.materialSpec; // 规格型号
            wmsStockUniicode.mslGradeCode = inReceiptUniicode.mslGradeCode; // MSL等级编码
            wmsStockUniicode.occupyQty = 0; // 占用数量
            wmsStockUniicode.packageTime = null; //
            //null; // 封包时间;如果需要封包日期从这格式化取                                                                                                
            wmsStockUniicode.palletBarcode = iqcResult.receiptNo; // 载体条码
            wmsStockUniicode.positionNo = inReceiptUniicode.curPositionNo; // 所在载体位置
            wmsStockUniicode.productDate = inReceiptUniicode.productDate; // 生产日期
            wmsStockUniicode.projectNo = iqcResult.projectNo; // 项目号
            wmsStockUniicode.proprietorCode = iqcResult.proprietorCode; // 货主
            wmsStockUniicode.realExposeTimes = inReceiptUniicode.supplierExposeTimeDuration; //null; // 实际暴露时长
            wmsStockUniicode.skuCode = iqcResult.skuCode; // SKU编码
            //wmsStockUniicode.stockCode = inReceiptUniicode.stockCode; // 库存编码
            //wmsStockUniicode.stockDtlId = inReceiptUniicode.stockDtlId; // 库存明细ID
            wmsStockUniicode.qty = qty; // 库存数量
            wmsStockUniicode.supplierCode = inReceiptUniicode.supplierCode; // 供应商编码
            wmsStockUniicode.supplierExposeTimes = inReceiptUniicode.supplierExposeTimeDuration; // 供应商暴露时长
            wmsStockUniicode.supplierNameAlias = iqcResult.supplierNameAlias; // 供应商名称-其他
            wmsStockUniicode.supplierNameEn = iqcResult.supplierNameEn; // 供应商名称-英文
            wmsStockUniicode.supplierName = iqcResult.supplierName; // 供应商名称
            wmsStockUniicode.uniicode = inReceiptUniicode.uniicode; // 库存唯一码
            wmsStockUniicode.unpackStatus = unpackStatus; //null; // 拆封状态;0:已封包，1:已开包
            wmsStockUniicode.unpackTime = null; //null; // 开封时间;如果需要开封日期从这格式化取
            wmsStockUniicode.extend1 = null; //null; // 扩展字段1
            wmsStockUniicode.extend2 = null; // 扩展字段2
            wmsStockUniicode.extend3 = null; // 扩展字段3
            wmsStockUniicode.extend4 = null; // 扩展字段4
            wmsStockUniicode.extend5 = null; // 扩展字段5
            wmsStockUniicode.extend6 = null; // 扩展字段6
            wmsStockUniicode.extend7 = null; // 扩展字段7
            wmsStockUniicode.extend8 = null; // 扩展字段8
            wmsStockUniicode.extend9 = null; // 扩展字段9
            wmsStockUniicode.extend10 = null; // 扩展字段10
            wmsStockUniicode.extend11 = null; // 扩展字段11
            wmsStockUniicode.chipSize = null; // 扩展字段12
            wmsStockUniicode.chipThickness = null; // 扩展字段13
            wmsStockUniicode.chipModel = null; // 扩展字段14
            wmsStockUniicode.dafType = null; // 扩展字段15
            wmsStockUniicode.CreateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
            wmsStockUniicode.CreateTime = DateTime.Now;
            wmsStockUniicode.UpdateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
            wmsStockUniicode.UpdateTime = DateTime.Now;
            return wmsStockUniicode;
        }

        /// <summary>
        /// 生成入库记录数据
        /// </summary>
        /// <returns></returns>
        private WmsInReceiptRecord BuildWmsInReceiptRecord(
            WmsInReceiptIqcResult wmsInReceiptIqcResult,
            WmsStock wmsStock,
            bool isPK,
            int returnFlag,
            decimal recordQty,
            string invoker
        )
        {
            string stockCode = wmsStock.stockCode;

            string loadedType = "1";
            string palletBarcode = wmsStock.palletBarcode;
            string binNo = wmsStock.binNo;
            string ptaBinNo = "";
            string ptaPalletBarcode = "";
            string iqcStatus = PutAwayOrDtlStatus.Init.GetCode();
            if (isPK)
            {
                ptaBinNo = wmsStock.binNo;
                ptaPalletBarcode = wmsStock.palletBarcode;
                iqcStatus = PutAwayOrDtlStatus.StoreIned.GetCode();
            }

            WmsInReceiptRecord wmsInReceiptRecord = new WmsInReceiptRecord();
            //wmsInReceiptRecord.areaNo = wmsInReceiptIqcResult.areaNo; // 区域
            wmsInReceiptRecord.areaNo = wmsStock.areaNo; // 区域
            wmsInReceiptRecord.erpWhouseNo = wmsInReceiptIqcResult.erpWhouseNo; // ERP仓库号
            wmsInReceiptRecord.whouseNo = wmsInReceiptIqcResult.whouseNo; // ERP仓库号
            wmsInReceiptRecord.regionNo = wmsInReceiptIqcResult.regionNo; // 库区
            wmsInReceiptRecord.binNo = binNo; // 库位
            wmsInReceiptRecord.proprietorCode = wmsInReceiptIqcResult.proprietorCode; // 货主
            wmsInReceiptRecord.iqcResultNo = wmsInReceiptIqcResult.iqcRecordNo; // 检验单号
            wmsInReceiptRecord.receiptNo = wmsInReceiptIqcResult.receiptNo; // 收货单号
            wmsInReceiptRecord.receiptDtlId = wmsInReceiptIqcResult.receiptDtlId; // 收货明细ID
            wmsInReceiptRecord.inNo = wmsInReceiptIqcResult.inNo; // 入库单号
            wmsInReceiptRecord.inDtlId = wmsInReceiptIqcResult.inDtlId; // 入库单明细ID
            wmsInReceiptRecord.externalInNo = wmsInReceiptIqcResult.externalInNo; // 外部入库单号
            wmsInReceiptRecord.externalInDtlId = wmsInReceiptIqcResult.externalInDtlId; // 外部入库单行号
            wmsInReceiptRecord.orderNo = wmsInReceiptIqcResult.orderNo; // 关联单号
            wmsInReceiptRecord.orderDtlId = wmsInReceiptIqcResult.orderDtlId; // 关联单行号
            wmsInReceiptRecord.docTypeCode = wmsInReceiptIqcResult.docTypeCode; // 单据类型
            wmsInReceiptRecord.sourceBy = wmsInReceiptIqcResult.sourceBy; // 数据来源
            wmsInReceiptRecord.inOutTypeNo = wmsInReceiptIqcResult.inOutTypeNo; // 出入库类别代码
            wmsInReceiptRecord.inOutTypeNo = wmsInReceiptIqcResult.inOutTypeNo; // 出入库类别代码
            wmsInReceiptRecord.inOutName = wmsInReceiptIqcResult.inOutName; // 出入库名称
            wmsInReceiptRecord.stockCode = stockCode; // 库存编码
            wmsInReceiptRecord.palletBarcode = palletBarcode; // 载体条码
            wmsInReceiptRecord.loadedType = Convert.ToInt32(loadedType); // 装载类型 : 1:实盘 ；2:工装；0：空盘；
            wmsInReceiptRecord.ptaBinNo = ptaBinNo; // 实际上架库位号
            wmsInReceiptRecord.ptaPalletBarcode = ptaPalletBarcode; // 实际上架后的托盘号
            wmsInReceiptRecord.returnFlag = returnFlag; // 回传状态：0默认，1可回传，2回传失败，3回传成功，4无需回传
            wmsInReceiptRecord.returnTime = DateTime.Now; // 回传时间
            //wmsInReceiptRecord.returnResult = returnResult;                                                // 回传结果
            wmsInReceiptRecord.materialCode = wmsInReceiptIqcResult.materialCode; // 物料代码
            wmsInReceiptRecord.materialName = wmsInReceiptIqcResult.materialName; // 物料名称
            wmsInReceiptRecord.supplierCode = wmsInReceiptIqcResult.supplierCode; // 供应商编码
            wmsInReceiptRecord.supplierName = wmsInReceiptIqcResult.supplierName; // 供应商名称
            wmsInReceiptRecord.supplierNameEn = wmsInReceiptIqcResult.supplierNameEn; // 供应商名称-英文
            wmsInReceiptRecord.supplierNameAlias = wmsInReceiptIqcResult.supplierNameAlias; // 供应商名称-其他
            wmsInReceiptRecord.batchNo = wmsInReceiptIqcResult.batchNo; // 批次
            wmsInReceiptRecord.materialSpec = wmsInReceiptIqcResult.materialSpec; // 规格型号
            wmsInReceiptRecord.recordQty = recordQty; // 组盘数量
            wmsInReceiptRecord.inspectionResult = wmsInReceiptIqcResult.inspectionResult; // 质检结果：待检、合格、特采、不合格
            wmsInReceiptRecord.inRecordStatus = Convert.ToInt32(iqcStatus); // 状态：0：初始创建（组盘完成）；41：入库中；90入库完成；92删除（撤销）；93强制完成
            wmsInReceiptRecord.skuCode = wmsInReceiptIqcResult.skuCode; // SKU 编码
            wmsInReceiptRecord.departmentName = wmsInReceiptIqcResult.departmentName; // 部门名称
            wmsInReceiptRecord.projectNo = wmsInReceiptIqcResult.projectNo; // 项目号
            wmsInReceiptRecord.ticketNo = wmsInReceiptIqcResult.ticketNo; // 工单号
            wmsInReceiptRecord.inspector = wmsInReceiptIqcResult.inspector; // 质检员
            wmsInReceiptRecord.minPkgQty = wmsInReceiptIqcResult.minPkgQty; // 包装数量
            wmsInReceiptRecord.urgentFlag = wmsInReceiptIqcResult.urgentFlag; // 急料标记
            wmsInReceiptRecord.CreateTime = DateTime.Now;
            wmsInReceiptRecord.CreateBy = invoker; // LoginUserInfo?.Name == null ? "EBS" : LoginUserInfo?.Name;
            return wmsInReceiptRecord;
        }
        /// <summary>
        /// 生成入库记录数据(无批次情况下)
        /// </summary>
        /// <param name="wmsInReceiptIqcResult"></param>
        /// <param name="wmsStock"></param>
        /// <param name="isPK"></param>
        /// <param name="returnFlag"></param>
        /// <param name="recordQty"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        private WmsInReceiptRecord BuildWmsInReceiptRecordByUniicode(
            WmsInReceiptIqcResult wmsInReceiptIqcResult,
            WmsStock wmsStock,
            string batchNo,
            bool isPK,
            int returnFlag,
            decimal recordQty,
            string invoker
        )
        {
            string stockCode = wmsStock.stockCode;

            //string loadedType = "1";
            string palletBarcode = wmsStock.palletBarcode;
            string binNo = wmsStock.binNo;
            string ptaBinNo = "";
            string ptaPalletBarcode = "";
            string iqcStatus = PutAwayOrDtlStatus.Init.GetCode();
            if (isPK)
            {
                ptaBinNo = wmsStock.binNo;
                ptaPalletBarcode = wmsStock.palletBarcode;
                iqcStatus = PutAwayOrDtlStatus.StoreIned.GetCode();
            }

            WmsInReceiptRecord wmsInReceiptRecord = new WmsInReceiptRecord();
            //wmsInReceiptRecord.areaNo = wmsInReceiptIqcResult.areaNo; // 区域
            wmsInReceiptRecord.areaNo = wmsStock.areaNo; // 区域
            wmsInReceiptRecord.erpWhouseNo = wmsInReceiptIqcResult.erpWhouseNo; // ERP仓库号
            wmsInReceiptRecord.whouseNo = wmsInReceiptIqcResult.whouseNo; // ERP仓库号
            wmsInReceiptRecord.regionNo = wmsInReceiptIqcResult.regionNo; // 库区
            wmsInReceiptRecord.binNo = binNo; // 库位
            wmsInReceiptRecord.proprietorCode = wmsInReceiptIqcResult.proprietorCode; // 货主
            wmsInReceiptRecord.iqcResultNo = wmsInReceiptIqcResult.iqcRecordNo; // 检验单号
            wmsInReceiptRecord.receiptNo = wmsInReceiptIqcResult.receiptNo; // 收货单号
            wmsInReceiptRecord.receiptDtlId = wmsInReceiptIqcResult.receiptDtlId; // 收货明细ID
            wmsInReceiptRecord.inNo = wmsInReceiptIqcResult.inNo; // 入库单号
            wmsInReceiptRecord.inDtlId = wmsInReceiptIqcResult.inDtlId; // 入库单明细ID
            wmsInReceiptRecord.externalInNo = wmsInReceiptIqcResult.externalInNo; // 外部入库单号
            wmsInReceiptRecord.externalInDtlId = wmsInReceiptIqcResult.externalInDtlId; // 外部入库单行号
            wmsInReceiptRecord.orderNo = wmsInReceiptIqcResult.orderNo; // 关联单号
            wmsInReceiptRecord.orderDtlId = wmsInReceiptIqcResult.orderDtlId; // 关联单行号
            wmsInReceiptRecord.docTypeCode = wmsInReceiptIqcResult.docTypeCode; // 单据类型
            wmsInReceiptRecord.sourceBy = wmsInReceiptIqcResult.sourceBy; // 数据来源
            wmsInReceiptRecord.inOutTypeNo = wmsInReceiptIqcResult.inOutTypeNo; // 出入库类别代码
            wmsInReceiptRecord.inOutTypeNo = wmsInReceiptIqcResult.inOutTypeNo; // 出入库类别代码
            wmsInReceiptRecord.inOutName = wmsInReceiptIqcResult.inOutName; // 出入库名称
            wmsInReceiptRecord.stockCode = stockCode; // 库存编码
            wmsInReceiptRecord.palletBarcode = palletBarcode; // 载体条码
            //wmsInReceiptRecord.loadedType = Convert.ToInt32(loadedType); // 装载类型 : 1:实盘 ；2:工装；0：空盘；
            wmsInReceiptRecord.loadedType = wmsStock.loadedType; // 装载类型 : 1:实盘 ；2:工装；0：空盘；
            wmsInReceiptRecord.ptaBinNo = ptaBinNo; // 实际上架库位号
            wmsInReceiptRecord.ptaPalletBarcode = ptaPalletBarcode; // 实际上架后的托盘号
            wmsInReceiptRecord.returnFlag = returnFlag; // 回传状态：0默认，1可回传，2回传失败，3回传成功，4无需回传
            wmsInReceiptRecord.returnTime = DateTime.Now; // 回传时间
            //wmsInReceiptRecord.returnResult = returnResult;                                                // 回传结果
            wmsInReceiptRecord.materialCode = wmsInReceiptIqcResult.materialCode; // 物料代码
            wmsInReceiptRecord.materialName = wmsInReceiptIqcResult.materialName; // 物料名称
            wmsInReceiptRecord.supplierCode = wmsInReceiptIqcResult.supplierCode; // 供应商编码
            wmsInReceiptRecord.supplierName = wmsInReceiptIqcResult.supplierName; // 供应商名称
            wmsInReceiptRecord.supplierNameEn = wmsInReceiptIqcResult.supplierNameEn; // 供应商名称-英文
            wmsInReceiptRecord.supplierNameAlias = wmsInReceiptIqcResult.supplierNameAlias; // 供应商名称-其他
            wmsInReceiptRecord.batchNo = batchNo; // 批次
            wmsInReceiptRecord.materialSpec = wmsInReceiptIqcResult.materialSpec; // 规格型号
            wmsInReceiptRecord.recordQty = recordQty; // 组盘数量
            wmsInReceiptRecord.inspectionResult = wmsInReceiptIqcResult.inspectionResult; // 质检结果：待检、合格、特采、不合格
            wmsInReceiptRecord.inRecordStatus = Convert.ToInt32(iqcStatus); // 状态：0：初始创建（组盘完成）；41：入库中；90入库完成；92删除（撤销）；93强制完成
            wmsInReceiptRecord.skuCode = wmsInReceiptIqcResult.skuCode; // SKU 编码
            wmsInReceiptRecord.departmentName = wmsInReceiptIqcResult.departmentName; // 部门名称
            wmsInReceiptRecord.projectNo = wmsInReceiptIqcResult.projectNo; // 项目号
            wmsInReceiptRecord.ticketNo = wmsInReceiptIqcResult.ticketNo; // 工单号
            wmsInReceiptRecord.inspector = wmsInReceiptIqcResult.inspector; // 质检员
            wmsInReceiptRecord.minPkgQty = wmsInReceiptIqcResult.minPkgQty; // 包装数量
            wmsInReceiptRecord.urgentFlag = wmsInReceiptIqcResult.urgentFlag; // 急料标记
            wmsInReceiptRecord.unitCode = wmsInReceiptIqcResult.unitCode; // 单位
            //wmsInReceiptRecord.replenishFlag = wmsInReceiptIqcResult.replenishFlag;                        // 补料标记
            //wmsInReceiptRecord.extend1 = wmsInReceiptIqcResult.extend1; // 备用字段1
            //wmsInReceiptRecord.extend2 = wmsInReceiptIqcResult.extend2; // 备用字段2
            //wmsInReceiptRecord.extend3 = wmsInReceiptIqcResult.extend3; // 备用字段3
            //wmsInReceiptRecord.extend4 = wmsInReceiptIqcResult.extend4; // 备用字段4
            //wmsInReceiptRecord.extend5 = wmsInReceiptIqcResult.extend5; // 备用字段5
            //wmsInReceiptRecord.extend6 = wmsInReceiptIqcResult.extend6; // 备用字段6
            //wmsInReceiptRecord.extend7 = wmsInReceiptIqcResult.extend7; // 备用字段7
            //wmsInReceiptRecord.extend8 = wmsInReceiptIqcResult.extend8; // 备用字段8
            //wmsInReceiptRecord.extend9 = wmsInReceiptIqcResult.extend9; // 备用字段9
            //wmsInReceiptRecord.extend10 = wmsInReceiptIqcResult.iqcResultNo; // 备用字段10 因组盘撤销需用到质检结果单号，入库记录中备用字段10
            //wmsInReceiptRecord.extend11 = wmsInReceiptIqcResult.extend11; // 备用字段11
            //wmsInReceiptRecord.extend12 = wmsInReceiptIqcResult.extend12; // 备用字段12
            //wmsInReceiptRecord.extend13 = wmsInReceiptIqcResult.extend13; // 备用字段13
            //wmsInReceiptRecord.extend14 = wmsInReceiptIqcResult.extend14; // 备用字段14
            //wmsInReceiptRecord.extend15 = wmsInReceiptIqcResult.extend15; // 备用字段15
            wmsInReceiptRecord.CreateTime = DateTime.Now;
            wmsInReceiptRecord.CreateBy = invoker; // LoginUserInfo?.Name == null ? "EBS" : LoginUserInfo?.Name;
            return wmsInReceiptRecord;
        }

    }
}
