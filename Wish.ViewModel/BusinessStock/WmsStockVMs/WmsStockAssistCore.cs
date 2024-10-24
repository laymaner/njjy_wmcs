using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using log4net;
using AutoMapper;


namespace Wish.ViewModel.BusinessStock.WmsStockVMs
{
    public partial class WmsStockVM : BaseCRUDVM<WmsStock>
    {
        private WmsOutInvoiceRecord BuildOutInvoiceRecord(
           WmsStock wmsStock,
           WmsStockDtl wmsStockDtl,
           string pickTaskNo,
           int allotType,
           int pickType,
           int palletPickType,
           string invoker,
           decimal allotQty,
           string docTypeCode,
           bool isBatch = false,
           bool isPk = true,
           string deliveryLocNo = "")
        {
            WmsOutInvoiceRecord wmsOutInvoiceRecord = new WmsOutInvoiceRecord();
            wmsOutInvoiceRecord.whouseNo = wmsStockDtl.whouseNo;
            wmsOutInvoiceRecord.erpWhouseNo = wmsStockDtl.erpWhouseNo;
            wmsOutInvoiceRecord.areaNo = wmsStock.areaNo;
            wmsOutInvoiceRecord.regionNo = wmsStock.regionNo;
            wmsOutInvoiceRecord.loadedTtype = wmsStock.loadedType;
            wmsOutInvoiceRecord.deliveryLocNo = deliveryLocNo;
            wmsOutInvoiceRecord.binNo = wmsStock.binNo;
            wmsOutInvoiceRecord.proprietorCode = wmsStock.proprietorCode;
            wmsOutInvoiceRecord.pickTaskNo = pickTaskNo;
            wmsOutInvoiceRecord.invoiceNo = "";
            wmsOutInvoiceRecord.invoiceDtlId = null;
            wmsOutInvoiceRecord.sourceBy = 0;
            wmsOutInvoiceRecord.waveNo = "";
            wmsOutInvoiceRecord.stockCode = wmsStock.stockCode;
            wmsOutInvoiceRecord.palletBarcode = wmsStock.palletBarcode;
            wmsOutInvoiceRecord.preStockDtlId = wmsStockDtl.ID;
            wmsOutInvoiceRecord.stockDtlId = wmsStockDtl.ID;
            wmsOutInvoiceRecord.docTypeCode = docTypeCode;
            wmsOutInvoiceRecord.pickType = pickType;
            wmsOutInvoiceRecord.palletPickType = palletPickType;
            wmsOutInvoiceRecord.allotType = allotType;
            //wmsOutInvoiceRecord.outBarcode = obj.outBarcode;                                  
            wmsOutInvoiceRecord.isBatch = isBatch ? 1 : 0;
            //wmsOutInvoiceRecord.pickLocNo = obj.pickLocNo;
            //wmsOutInvoiceRecord.deliveryLocNo = obj.deliveryLocNo;
            wmsOutInvoiceRecord.reversePickFlag = 0;
            wmsOutInvoiceRecord.skuCode = wmsStockDtl.skuCode;
            wmsOutInvoiceRecord.materialCode = wmsStockDtl.materialCode;
            wmsOutInvoiceRecord.materialName = wmsStockDtl.materialName;
            wmsOutInvoiceRecord.materialSpec = wmsStockDtl.materialSpec;
            wmsOutInvoiceRecord.batchNo = "";
            wmsOutInvoiceRecord.supplierCode = wmsStockDtl.supplierCode;
            wmsOutInvoiceRecord.supplierName = wmsStockDtl.supplierName;
            wmsOutInvoiceRecord.supplierNameEn = wmsStockDtl.supplierNameEn;
            wmsOutInvoiceRecord.supplierNameAlias = wmsStockDtl.supplierNameAlias;
            wmsOutInvoiceRecord.inspectionResult = wmsStockDtl.inspectionResult;
            //wmsOutInvoiceRecord.erpBinNo = wmsStockDtl.erpWhouseNo;
            //wmsOutInvoiceRecord.urgentFlag = wmsStockDtl.urgentFlag == "Y" ? "Y" : "N";
            //TODO:出库记录急料标记
            wmsOutInvoiceRecord.allotQty = allotQty;
            wmsOutInvoiceRecord.pickQty = 0;
            //wmsOutInvoiceRecord.issuedResult = obj.issuedResult;
            wmsOutInvoiceRecord.outRecordStatus = isPk ? 40 : 31;
            wmsOutInvoiceRecord.allocatResult = "";
            wmsOutInvoiceRecord.ticketPlanBeginTime = null;
            wmsOutInvoiceRecord.supplyType = null;
            wmsOutInvoiceRecord.belongDepartment = null;
            wmsOutInvoiceRecord.ticketType = null;
            wmsOutInvoiceRecord.assemblyIdx = null;
            wmsOutInvoiceRecord.productLocation = null;
            wmsOutInvoiceRecord.orderNo = null;
            wmsOutInvoiceRecord.orderDtlId = null;
            wmsOutInvoiceRecord.projectNo = null;
            //wmsOutInvoiceRecord.productDeptCode = null;
            wmsOutInvoiceRecord.productDeptName = null;
            wmsOutInvoiceRecord.fpNo = null;
            wmsOutInvoiceRecord.fpName = null;
            wmsOutInvoiceRecord.fpQty = null;
            wmsOutInvoiceRecord.ticketNo = null;
            wmsOutInvoiceRecord.externalOutNo = null;
            wmsOutInvoiceRecord.externalOutDtlId = null;
            wmsOutInvoiceRecord.inOutTypeNo = null;
            wmsOutInvoiceRecord.inOutName = null;
            wmsOutInvoiceRecord.CreateBy = invoker;
            wmsOutInvoiceRecord.CreateTime = DateTime.Now;
            wmsOutInvoiceRecord.extend1 = null;
            wmsOutInvoiceRecord.extend2 = null;
            wmsOutInvoiceRecord.extend3 = null;
            wmsOutInvoiceRecord.extend4 = null;
            wmsOutInvoiceRecord.extend5 = null;
            wmsOutInvoiceRecord.extend6 = null;
            wmsOutInvoiceRecord.extend7 = null;
            wmsOutInvoiceRecord.extend8 = null;
            wmsOutInvoiceRecord.extend9 = null;
            wmsOutInvoiceRecord.extend10 = null;
            wmsOutInvoiceRecord.extend11 = null;
            wmsOutInvoiceRecord.extend12 = null;
            wmsOutInvoiceRecord.extend13 = null;
            wmsOutInvoiceRecord.extend14 = null;
            wmsOutInvoiceRecord.extend15 = null;
            wmsOutInvoiceRecord.UpdateBy = invoker;
            wmsOutInvoiceRecord.UpdateTime = DateTime.Now;
            return wmsOutInvoiceRecord;
        }

        public WmsTask BuildEmptyWmsTask(string wmsTaskNo, WmsStock wmsStock, string locNo, string wmsTaskType, string taskType, string invoker, int docPriority = 99)
        {
            WmsTask wmsTask = new WmsTask();
            wmsTask.wmsTaskNo = wmsTaskNo;
            wmsTask.taskTypeNo = taskType;
            wmsTask.wmsTaskType = wmsTaskType;
            wmsTask.stockCode = wmsStock.stockCode;
            wmsTask.whouseNo = wmsStock.whouseNo;
            wmsTask.palletBarcode = wmsStock.palletBarcode;
            wmsTask.proprietorCode = wmsStock.proprietorCode;
            wmsTask.loadedType = wmsStock.loadedType;
            wmsTask.regionNo = wmsStock.regionNo;
            wmsTask.roadwayNo = wmsStock.roadwayNo;
            wmsTask.frLocationNo = wmsStock.binNo;
            wmsTask.frLocationType = 1;
            wmsTask.toLocationNo = locNo;
            wmsTask.toLocationType = 0;
            wmsTask.feedbackStatus = 0;
            wmsTask.taskStatus = 0;
            wmsTask.matHeight = wmsStock.height;
            wmsTask.matWeight = null;
            wmsTask.taskPriority = docPriority;
            wmsTask.UpdateBy = invoker;
            wmsTask.CreateBy = invoker;
            wmsTask.CreateTime = DateTime.Now;
            wmsTask.UpdateTime = DateTime.Now;
            return wmsTask;

        }


        public WmsTask BuildOutWmsTask(string wmsTaskNo, WmsStock wmsStock, string locNo, string wmsTaskType, string taskType, string invoker, int taskPriority = 99)
        {
            WmsTask wmsTask = new WmsTask();
            wmsTask.wmsTaskNo = wmsTaskNo;
            wmsTask.taskTypeNo = taskType;
            wmsTask.wmsTaskType = wmsTaskType;
            wmsTask.stockCode = wmsStock.stockCode;
            wmsTask.whouseNo = wmsStock.whouseNo;
            wmsTask.palletBarcode = wmsStock.palletBarcode;
            wmsTask.proprietorCode = wmsStock.proprietorCode;
            wmsTask.loadedType = wmsStock.loadedType;
            wmsTask.regionNo = wmsStock.regionNo;
            wmsTask.roadwayNo = wmsStock.roadwayNo;
            wmsTask.frLocationNo = wmsStock.binNo;
            wmsTask.frLocationType = 1;
            wmsTask.toLocationNo = locNo;
            wmsTask.toLocationType = 0;
            wmsTask.feedbackStatus = 0;
            wmsTask.taskStatus = 0;
            wmsTask.matHeight = wmsStock.height;
            wmsTask.matWeight = null;
            wmsTask.taskPriority = taskPriority;
            wmsTask.UpdateBy = invoker;
            wmsTask.CreateBy = invoker;
            wmsTask.CreateTime = DateTime.Now;
            wmsTask.UpdateTime = DateTime.Now;
            return wmsTask;

        }

        public WmsTask BuildMoveWmsTask(string wmsTaskNo, WmsStock wmsStockSrc, WmsStock wmsStocktarget, string wmsTaskType, string taskType, string invoker, int taskPriority = 99)
        {
            WmsTask wmsTask = new WmsTask();
            wmsTask.wmsTaskNo = wmsTaskNo;
            wmsTask.taskTypeNo = taskType;
            wmsTask.wmsTaskType = wmsTaskType;
            wmsTask.stockCode = wmsStocktarget.stockCode;
            wmsTask.whouseNo = wmsStocktarget.whouseNo;
            wmsTask.palletBarcode = wmsStocktarget.palletBarcode;
            wmsTask.proprietorCode = wmsStocktarget.proprietorCode;
            wmsTask.loadedType = wmsStocktarget.loadedType;
            wmsTask.regionNo = wmsStocktarget.regionNo;
            wmsTask.roadwayNo = wmsStocktarget.roadwayNo;
            wmsTask.frLocationNo = wmsStockSrc.binNo;
            wmsTask.frLocationType = 1;
            wmsTask.toLocationNo = wmsStocktarget.binNo;
            wmsTask.toLocationType = 1;
            wmsTask.feedbackStatus = 0;
            wmsTask.taskStatus = 0;
            wmsTask.matHeight = wmsStockSrc.height;
            wmsTask.matWeight = null;
            wmsTask.taskPriority = taskPriority;
            wmsTask.UpdateBy = invoker;
            wmsTask.CreateBy = invoker;
            wmsTask.CreateTime = DateTime.Now;
            wmsTask.UpdateTime = DateTime.Now;
            return wmsTask;

        }
        private WmsItnMoveRecord CreateMoveRecord(string docTypeCode, WmsStockUniicode wmsStockSrcUniicode, WmsStockUniicode wmsStockTarUniicode, WmsStock wmsStockSrc, WmsStock wmsStockTarget)
        {
            WmsItnMoveRecord wmsItnMoveRecord = new WmsItnMoveRecord();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStockUniicode, WmsItnMoveRecord>());
            var mapper = config.CreateMapper();
            wmsItnMoveRecord = mapper.Map<WmsItnMoveRecord>(wmsStockSrcUniicode);
            wmsItnMoveRecord.moveRecordStatus = 31;
            wmsItnMoveRecord.docTypeCode = docTypeCode;
            wmsItnMoveRecord.whouseNo = wmsStockTarget.whouseNo;                           // 仓库号
            wmsItnMoveRecord.proprietorCode = wmsStockTarget.proprietorCode;               // 货主
            wmsItnMoveRecord.frStockCode = wmsStockSrc.stockCode;
            wmsItnMoveRecord.frRegionNo = wmsStockSrc.regionNo;
            wmsItnMoveRecord.frStockDtlId = wmsStockSrcUniicode.stockDtlId;
            wmsItnMoveRecord.frLocationNo = wmsStockSrc.binNo;                                  // 移库前库位号
            wmsItnMoveRecord.frPalletBarcode = wmsStockSrc.palletBarcode;                       // 移库前托盘号
            wmsItnMoveRecord.toRegionNo = wmsStockTarget.regionNo;
            wmsItnMoveRecord.toLocationNo = wmsStockTarget.binNo;                               // 移库后库位号
            wmsItnMoveRecord.toPalletBarcode = wmsStockTarget.palletBarcode;                    // 移库后托盘号
            wmsItnMoveRecord.toStockCode = wmsStockTarget.stockCode;                            // 库存编码
            wmsItnMoveRecord.toStockDtlId = wmsStockTarUniicode.stockDtlId;                                       // 库存明细ID
            wmsItnMoveRecord.curPalletbarCode = wmsStockTarget.palletBarcode;                    // 移库后托盘号
            wmsItnMoveRecord.curStockCode = wmsStockTarget.stockCode;                            // 库存编码
            wmsItnMoveRecord.curStockDtlId = wmsStockTarUniicode.stockDtlId;
            wmsItnMoveRecord.CreateBy = wmsStockTarget.CreateBy;                                                // 创建人
            wmsItnMoveRecord.CreateTime = wmsStockTarget.CreateTime;
            wmsItnMoveRecord.UpdateBy = wmsStockTarget.UpdateBy;                                                // 创建人
            wmsItnMoveRecord.UpdateTime = wmsStockTarget.UpdateTime;  // 创建时间
            return wmsItnMoveRecord;
        }

        private WmsItnMoveRecord CreateMoveRecord(string docTypeCode, WmsStockUniicode wmsStockSrcUniicode, WmsStock wmsStockSrc, WmsStock wmsStockTarget, string invoker)
        {
            WmsItnMoveRecord wmsItnMoveRecord = new WmsItnMoveRecord();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStockUniicode, WmsItnMoveRecord>());
            var mapper = config.CreateMapper();
            wmsItnMoveRecord = mapper.Map<WmsItnMoveRecord>(wmsStockSrcUniicode);
            wmsItnMoveRecord.moveRecordStatus = 31;
            wmsItnMoveRecord.docTypeCode = docTypeCode;
            wmsItnMoveRecord.whouseNo = wmsStockSrcUniicode.whouseNo;                           // 仓库号
            wmsItnMoveRecord.proprietorCode = wmsStockSrcUniicode.proprietorCode;               // 货主
            wmsItnMoveRecord.frStockCode = wmsStockSrc.stockCode;
            wmsItnMoveRecord.frRegionNo = wmsStockSrc.regionNo;
            wmsItnMoveRecord.frStockDtlId = wmsStockSrcUniicode.stockDtlId;
            wmsItnMoveRecord.frLocationNo = wmsStockSrc.binNo;                                  // 移库前库位号
            wmsItnMoveRecord.frPalletBarcode = wmsStockSrc.palletBarcode;                       // 移库前托盘号
            wmsItnMoveRecord.toRegionNo = "";
            wmsItnMoveRecord.toLocationNo = "";                               // 移库后库位号
            wmsItnMoveRecord.toPalletBarcode = wmsStockTarget.palletBarcode;                    // 移库后托盘号
            wmsItnMoveRecord.toStockCode = wmsStockTarget.stockCode;                            // 库存编码
            wmsItnMoveRecord.toStockDtlId = wmsStockSrcUniicode.stockDtlId;                                       // 库存明细ID
            wmsItnMoveRecord.curPalletbarCode = wmsStockTarget.palletBarcode;                    // 移库后托盘号
            wmsItnMoveRecord.curStockCode = "";                           // 库存编码
            wmsItnMoveRecord.curStockDtlId = wmsStockSrcUniicode.stockDtlId;
            wmsItnMoveRecord.CreateBy = invoker;                                                // 创建人
            wmsItnMoveRecord.CreateTime = DateTime.Now;
            wmsItnMoveRecord.UpdateBy = invoker;                                                // 创建人
            wmsItnMoveRecord.UpdateTime = DateTime.Now;  // 创建时间
            return wmsItnMoveRecord;
        }

        private WmsItnMoveRecord CreateMoveRecord(string docTypeCode, WmsStockDtl wmsStockDtlSrc, WmsStockDtl wmsStockDtlTarget, WmsStock wmsStockSrc, WmsStock wmsStockTarget)
        {
            WmsItnMoveRecord wmsItnMoveRecord = new WmsItnMoveRecord();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStockUniicode, WmsItnMoveRecord>());
            var mapper = config.CreateMapper();
            //wmsItnMoveRecord = mapper.Map<WmsItnMoveRecord>(wmsStockSrcUniicode);
            wmsItnMoveRecord.moveRecordStatus = 31;
            wmsItnMoveRecord.docTypeCode = docTypeCode;
            wmsItnMoveRecord.confirmQty = wmsStockDtlTarget.qty;
            wmsItnMoveRecord.moveQty = wmsStockDtlTarget.qty;
            wmsItnMoveRecord.stockQty = wmsStockDtlTarget.qty;
            wmsItnMoveRecord.whouseNo = wmsStockTarget.whouseNo;                           // 仓库号
            wmsItnMoveRecord.proprietorCode = wmsStockTarget.proprietorCode;               // 货主
            wmsItnMoveRecord.frStockCode = wmsStockSrc.stockCode;
            wmsItnMoveRecord.frRegionNo = wmsStockSrc.regionNo;
            wmsItnMoveRecord.frStockDtlId = wmsStockDtlSrc.ID;
            wmsItnMoveRecord.frLocationNo = wmsStockSrc.binNo;                                  // 移库前库位号
            wmsItnMoveRecord.frPalletBarcode = wmsStockSrc.palletBarcode;                       // 移库前托盘号
            wmsItnMoveRecord.toRegionNo = wmsStockTarget.regionNo;
            wmsItnMoveRecord.toLocationNo = wmsStockTarget.binNo;                               // 移库后库位号
            wmsItnMoveRecord.toPalletBarcode = wmsStockTarget.palletBarcode;                    // 移库后托盘号
            wmsItnMoveRecord.toStockCode = wmsStockTarget.stockCode;                            // 库存编码
            wmsItnMoveRecord.toStockDtlId = wmsStockDtlTarget.ID;                                       // 库存明细ID
            wmsItnMoveRecord.CreateBy = wmsStockTarget.CreateBy;                                                // 创建人
            wmsItnMoveRecord.CreateTime = wmsStockTarget.CreateTime;
            wmsItnMoveRecord.UpdateBy = wmsStockTarget.UpdateBy;                                                // 创建人
            wmsItnMoveRecord.UpdateTime = wmsStockTarget.UpdateTime;  // 创建时间
            return wmsItnMoveRecord;
        }

        private WmsItnMoveRecord CreateMoveRecord(string docTypeCode, WmsStockDtl wmsStockDtlSrc, WmsStock wmsStockSrc, WmsStock wmsStockTarget, string invoker)
        {
            WmsItnMoveRecord wmsItnMoveRecord = new WmsItnMoveRecord();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStockUniicode, WmsItnMoveRecord>());
            var mapper = config.CreateMapper();
            //wmsItnMoveRecord = mapper.Map<WmsItnMoveRecord>(wmsStockSrcUniicode);
            wmsItnMoveRecord.moveRecordStatus = 31;
            wmsItnMoveRecord.docTypeCode = docTypeCode;
            wmsItnMoveRecord.confirmQty = wmsStockDtlSrc.qty;
            wmsItnMoveRecord.moveQty = wmsStockDtlSrc.qty;
            wmsItnMoveRecord.stockQty = wmsStockDtlSrc.qty;
            wmsItnMoveRecord.whouseNo = wmsStockTarget.whouseNo;                           // 仓库号
            wmsItnMoveRecord.proprietorCode = wmsStockTarget.proprietorCode;               // 货主
            wmsItnMoveRecord.frStockCode = wmsStockSrc.stockCode;
            wmsItnMoveRecord.frRegionNo = wmsStockSrc.regionNo;
            wmsItnMoveRecord.frStockDtlId = wmsStockDtlSrc.ID;
            wmsItnMoveRecord.frLocationNo = wmsStockSrc.binNo;                                  // 移库前库位号
            wmsItnMoveRecord.frPalletBarcode = wmsStockSrc.palletBarcode;                       // 移库前托盘号
            wmsItnMoveRecord.toRegionNo = "";
            wmsItnMoveRecord.toLocationNo = "";                               // 移库后库位号
            wmsItnMoveRecord.toPalletBarcode = wmsStockTarget.palletBarcode;                    // 移库后托盘号
            wmsItnMoveRecord.toStockCode = wmsStockTarget.stockCode;                            // 库存编码
            wmsItnMoveRecord.toStockDtlId = wmsStockDtlSrc.ID;                                       // 库存明细ID
            wmsItnMoveRecord.curPalletbarCode = wmsStockTarget.palletBarcode;                    // 移库后托盘号
            wmsItnMoveRecord.curStockCode = wmsStockTarget.stockCode;                            // 库存编码
            wmsItnMoveRecord.curStockDtlId = wmsStockDtlSrc.ID;
            wmsItnMoveRecord.CreateBy = invoker;                                                // 创建人
            wmsItnMoveRecord.CreateTime = DateTime.Now;
            wmsItnMoveRecord.CreateBy = invoker;                                                // 创建人
            wmsItnMoveRecord.UpdateTime = DateTime.Now;  // 创建时间
            return wmsItnMoveRecord;
        }

    }
}
