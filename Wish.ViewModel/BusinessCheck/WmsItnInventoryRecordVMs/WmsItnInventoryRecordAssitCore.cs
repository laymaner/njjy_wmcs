using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using log4net;
using Wish.Areas.BasWhouse.Model;
using Wish.TaskConfig.Model;
using Wish.ViewModel.Common.Dtos;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryRecordVMs
{
    public partial class WmsItnInventoryRecordVM
    {
        /// <summary>
        /// 创建盘点记录
        /// </summary>
        /// <param name="uniiCode"></param>
        /// <param name="wmsStock"></param>
        /// <returns></returns>
        public WmsItnInventoryRecord BuildWmsItnInventoryRecordByUniicode(WmsStockUniicode uniiCode, WmsStock wmsStock, CreateInventoryTaskDto input, string locNo)
        {
            WmsItnInventoryRecord wmsItnInventoryRecord = new WmsItnInventoryRecord();
            wmsItnInventoryRecord.blindFlag = 0;
            wmsItnInventoryRecord.inOutTypeNo = "CHECK_OUT";
            wmsItnInventoryRecord.confirmBy = "";//
            wmsItnInventoryRecord.confirmQty = null;//
            wmsItnInventoryRecord.confirmReason = "";//
            wmsItnInventoryRecord.differenceFlag = 0;//
            wmsItnInventoryRecord.docTypeCode = "1004";
            wmsItnInventoryRecord.erpWhouseNo = uniiCode.erpWhouseNo;
            wmsItnInventoryRecord.extend1 = uniiCode.extend1;
            wmsItnInventoryRecord.extend2 = uniiCode.extend2;
            wmsItnInventoryRecord.extend3 = uniiCode.extend3;
            wmsItnInventoryRecord.extend4 = uniiCode.extend4;
            wmsItnInventoryRecord.extend5 = uniiCode.extend5;
            wmsItnInventoryRecord.extend6 = uniiCode.extend6;
            wmsItnInventoryRecord.extend7 = uniiCode.extend7;
            wmsItnInventoryRecord.extend8 = uniiCode.extend8;
            wmsItnInventoryRecord.extend9 = uniiCode.extend9;
            wmsItnInventoryRecord.extend10 = uniiCode.extend10;
            wmsItnInventoryRecord.extend11 = uniiCode.extend11;
            wmsItnInventoryRecord.extend12 = null;
            wmsItnInventoryRecord.extend13 = null;
            wmsItnInventoryRecord.extend14 = null;
            wmsItnInventoryRecord.extend15 = null;
            wmsItnInventoryRecord.inspectionResult = uniiCode.inspectionResult;
            wmsItnInventoryRecord.inventoryBy = uniiCode.UpdateBy;
            wmsItnInventoryRecord.inventoryDtlId = 0;
            wmsItnInventoryRecord.inventoryNo = "";///获取序列号
            wmsItnInventoryRecord.inventoryQty = uniiCode.occupyQty;
            wmsItnInventoryRecord.inventoryReason = input.inventoryReason;
            wmsItnInventoryRecord.inventoryRecordStatus = 31;//下架中
            wmsItnInventoryRecord.materialName = uniiCode.materialName;
            wmsItnInventoryRecord.materialCode = uniiCode.materialCode;
            wmsItnInventoryRecord.materialSpec = uniiCode.materialSpec;
            wmsItnInventoryRecord.occupyQty = uniiCode.occupyQty;
            wmsItnInventoryRecord.palletBarcode = uniiCode.palletBarcode;
            wmsItnInventoryRecord.projectNo = uniiCode.projectNo;
            wmsItnInventoryRecord.proprietorCode = uniiCode.proprietorCode;
            wmsItnInventoryRecord.putdownLocNo = locNo;
            wmsItnInventoryRecord.skuCode = "";
            wmsItnInventoryRecord.stockCode = wmsStock.stockCode;
            wmsItnInventoryRecord.stockDtlId = uniiCode.stockDtlId;
            wmsItnInventoryRecord.supplierCode = uniiCode.supplierCode;
            wmsItnInventoryRecord.supplierName = uniiCode.supplierName;
            wmsItnInventoryRecord.supplierNameAlias = uniiCode.supplierNameAlias;
            wmsItnInventoryRecord.supplierNameEn = uniiCode.supplierNameEn;
            wmsItnInventoryRecord.whouseNo = uniiCode.whouseNo;
            wmsItnInventoryRecord.CreateBy = uniiCode.UpdateBy;
            wmsItnInventoryRecord.CreateTime = DateTime.Now;
            wmsItnInventoryRecord.UpdateBy = uniiCode.UpdateBy;
            wmsItnInventoryRecord.UpdateTime = DateTime.Now;
            return wmsItnInventoryRecord;
        }
        
        /// <summary>
        /// 创建盘点记录
        /// </summary>
        /// <param name="uniiCode"></param>
        /// <param name="wmsStock"></param>
        /// <returns></returns>
        public WmsItnInventoryRecord BuildWmsItnInventoryRecordInByUniicode(WmsStockUniicode uniiCode, WmsStock wmsStock, ConfirmInventoryTaskDto input)
        {
            WmsItnInventoryRecord wmsItnInventoryRecord = new WmsItnInventoryRecord();
            wmsItnInventoryRecord.blindFlag = 0;
            wmsItnInventoryRecord.inOutTypeNo = "CHECK_IN";
            wmsItnInventoryRecord.confirmBy = "";//
            wmsItnInventoryRecord.confirmQty = null;//
            wmsItnInventoryRecord.confirmReason = "";//
            wmsItnInventoryRecord.differenceFlag = 0;//
            wmsItnInventoryRecord.docTypeCode = "1004";
            wmsItnInventoryRecord.erpWhouseNo = uniiCode.erpWhouseNo;
            wmsItnInventoryRecord.extend1 = uniiCode.extend1;
            wmsItnInventoryRecord.extend2 = uniiCode.extend2;
            wmsItnInventoryRecord.extend3 = uniiCode.extend3;
            wmsItnInventoryRecord.extend4 = uniiCode.extend4;
            wmsItnInventoryRecord.extend5 = uniiCode.extend5;
            wmsItnInventoryRecord.extend6 = uniiCode.extend6;
            wmsItnInventoryRecord.extend7 = uniiCode.extend7;
            wmsItnInventoryRecord.extend8 = uniiCode.extend8;
            wmsItnInventoryRecord.extend9 = uniiCode.extend9;
            wmsItnInventoryRecord.extend10 = uniiCode.extend10;
            wmsItnInventoryRecord.extend11 = uniiCode.extend11;
            wmsItnInventoryRecord.extend12 = null;
            wmsItnInventoryRecord.extend13 = null;
            wmsItnInventoryRecord.extend14 = null;
            wmsItnInventoryRecord.extend15 = null;
            wmsItnInventoryRecord.inspectionResult = uniiCode.inspectionResult;
            wmsItnInventoryRecord.inventoryBy = uniiCode.UpdateBy;
            wmsItnInventoryRecord.inventoryDtlId = 0;
            wmsItnInventoryRecord.inventoryNo = "";///获取序列号
            wmsItnInventoryRecord.inventoryQty = uniiCode.occupyQty;
            wmsItnInventoryRecord.inventoryReason = input.confirmReason;
            wmsItnInventoryRecord.inventoryRecordStatus = 31;//下架中
            wmsItnInventoryRecord.materialName = uniiCode.materialName;
            wmsItnInventoryRecord.materialCode = uniiCode.materialCode;
            wmsItnInventoryRecord.materialSpec = uniiCode.materialSpec;
            wmsItnInventoryRecord.occupyQty = uniiCode.occupyQty;
            wmsItnInventoryRecord.palletBarcode = uniiCode.palletBarcode;
            wmsItnInventoryRecord.projectNo = uniiCode.projectNo;
            wmsItnInventoryRecord.proprietorCode = uniiCode.proprietorCode;
            wmsItnInventoryRecord.putdownLocNo = "";
            wmsItnInventoryRecord.skuCode = "";
            wmsItnInventoryRecord.stockCode = wmsStock.stockCode;
            wmsItnInventoryRecord.stockDtlId = uniiCode.stockDtlId;
            wmsItnInventoryRecord.supplierCode = uniiCode.supplierCode;
            wmsItnInventoryRecord.supplierName = uniiCode.supplierName;
            wmsItnInventoryRecord.supplierNameAlias = uniiCode.supplierNameAlias;
            wmsItnInventoryRecord.supplierNameEn = uniiCode.supplierNameEn;
            wmsItnInventoryRecord.whouseNo = uniiCode.whouseNo;
            wmsItnInventoryRecord.CreateBy = uniiCode.UpdateBy;
            wmsItnInventoryRecord.CreateTime = DateTime.Now;
            wmsItnInventoryRecord.UpdateBy = uniiCode.UpdateBy;
            wmsItnInventoryRecord.UpdateTime = DateTime.Now;
            return wmsItnInventoryRecord;
        }



        /// <summary>
        /// 根据盘点记录生成任务
        /// </summary>
        /// <param name="wmsItnInventoryRecord"></param>
        /// <returns></returns>
        public WmsTask BuildWmsTaskByItnInventoryRecord(WmsItnInventoryRecord wmsItnInventoryRecord, WmsStock wmsStock)
        {
            WmsTask wmsTaskEntity = new WmsTask();
            wmsTaskEntity.feedbackDesc = String.Empty;
            wmsTaskEntity.feedbackStatus = 0;
            wmsTaskEntity.frLocationNo = wmsStock.binNo;
            wmsTaskEntity.frLocationType = 0;
            wmsTaskEntity.loadedType = wmsStock.loadedType;
            wmsTaskEntity.matHeight = wmsStock.height;//高
            wmsTaskEntity.matLength = null;//长
            wmsTaskEntity.matQty = null;
            wmsTaskEntity.matWeight = null;//重量
            wmsTaskEntity.matWidth = null;//宽
            wmsTaskEntity.orderNo = wmsItnInventoryRecord.inventoryNo;
            wmsTaskEntity.palletBarcode = wmsItnInventoryRecord.palletBarcode;
            wmsTaskEntity.proprietorCode = wmsItnInventoryRecord.proprietorCode;
            wmsTaskEntity.regionNo = wmsStock.regionNo;
            wmsTaskEntity.roadwayNo = wmsStock.roadwayNo;
            wmsTaskEntity.stockCode = wmsStock.stockCode;
            wmsTaskEntity.taskDesc = "初始创建";
            wmsTaskEntity.taskPriority = 100;
            wmsTaskEntity.taskStatus = 10;
            wmsTaskEntity.taskTypeNo = "OUT";
            wmsTaskEntity.toLocationNo = wmsItnInventoryRecord.putdownLocNo;
            wmsTaskEntity.toLocationType = 0;
            wmsTaskEntity.whouseNo = wmsItnInventoryRecord.whouseNo;
            wmsTaskEntity.wmsTaskNo = "";//获取序列号
            wmsTaskEntity.wmsTaskType = "CHECK_OUT";
            wmsTaskEntity.CreateBy = wmsStock.UpdateBy;
            wmsTaskEntity.CreateTime = DateTime.Now;
            wmsTaskEntity.UpdateBy = wmsStock.UpdateBy;
            wmsTaskEntity.UpdateTime = DateTime.Now;

            return wmsTaskEntity;
        }

        /// <summary>
        /// 根据盘点记录生成任务
        /// </summary>
        /// <param name="wmsItnInventoryRecord"></param>
        /// <returns></returns>
        public WmsTask BuildWmsInTaskByItnInventoryRecord(WmsItnInventoryRecord wmsItnInventoryRecord, WmsStock wmsStock)
        {
            WmsTask wmsTaskEntity = new WmsTask();
            wmsTaskEntity.feedbackDesc = String.Empty;
            wmsTaskEntity.feedbackStatus = 0;
            wmsTaskEntity.frLocationNo = wmsItnInventoryRecord.putdownLocNo;
            wmsTaskEntity.frLocationType = 0;
            wmsTaskEntity.loadedType = wmsStock.loadedType;
            wmsTaskEntity.matHeight = wmsStock.height;//高
            wmsTaskEntity.matLength = null;//长
            wmsTaskEntity.matQty = null;
            wmsTaskEntity.matWeight = null;//重量
            wmsTaskEntity.matWidth = null;//宽
            wmsTaskEntity.orderNo = wmsItnInventoryRecord.inventoryNo;
            wmsTaskEntity.palletBarcode = wmsItnInventoryRecord.palletBarcode;
            wmsTaskEntity.proprietorCode = wmsItnInventoryRecord.proprietorCode;
            wmsTaskEntity.regionNo = wmsStock.regionNo;
            wmsTaskEntity.roadwayNo = wmsStock.roadwayNo;
            wmsTaskEntity.stockCode = wmsStock.stockCode;
            wmsTaskEntity.taskDesc = "初始创建";
            wmsTaskEntity.taskPriority = 100;
            wmsTaskEntity.taskStatus = 10;
            wmsTaskEntity.taskTypeNo = "IN";
            wmsTaskEntity.toLocationNo = wmsStock.binNo;
            wmsTaskEntity.toLocationType = 0;
            wmsTaskEntity.whouseNo = wmsItnInventoryRecord.whouseNo;
            wmsTaskEntity.wmsTaskNo = "";//获取序列号
            wmsTaskEntity.wmsTaskType = "CHECK_IN";
            wmsTaskEntity.CreateBy = wmsStock.UpdateBy;
            wmsTaskEntity.CreateTime = DateTime.Now;
            wmsTaskEntity.UpdateBy = wmsStock.UpdateBy;
            wmsTaskEntity.UpdateTime = DateTime.Now;

            return wmsTaskEntity;
        }



        /// <summary>
        /// 根据WMS任务创建堆垛机指令
        /// </summary>
        /// <param name="wmsTask"></param>
        /// <returns></returns>
        public SrmCmd BuildSrmCmdByWmsTask(WmsTask wmsTask, BasWBin basWBin)
        {
            SrmCmd srmCmd = new SrmCmd();
            srmCmd.SubTask_No = "";//获取序列号
            srmCmd.Task_No = wmsTask.wmsTaskNo;
            srmCmd.Serial_No = 0;//获取序列号
            srmCmd.Device_No = "SRM" + wmsTask.roadwayNo.Substring(wmsTask.roadwayNo.Length - 2); ;
            srmCmd.Fork_No = "1";
            srmCmd.Station_Type = "0";
            srmCmd.Check_Point = 0;//计算
            srmCmd.From_Station = 0;
            srmCmd.To_Station = 1;
            srmCmd.Task_Type = "OUT";
            srmCmd.To_Column = 0;
            srmCmd.To_ForkDirection = 0;
            srmCmd.To_Layer = 0;
            srmCmd.To_Deep = 0;
            srmCmd.From_Column = (short)basWBin.binCol;//
            srmCmd.From_ForkDirection = (short)basWBin.binRow;//
            srmCmd.From_Layer = (short)basWBin.binLayer;//
            srmCmd.From_Deep = 0;//
            srmCmd.Task_Cmd = 4;
            srmCmd.Pallet_Barcode = Convert.ToInt32(wmsTask.palletBarcode);
            //srmCmd.Pallet_Barcode = wmsTask.palletBarcode;
            srmCmd.Exec_Status = 3;
            srmCmd.Recive_Date = DateTime.Now;
            srmCmd.CreateBy = wmsTask.UpdateBy;
            srmCmd.CreateTime = DateTime.Now;
            return srmCmd;
        }

        

        /// <summary>
        /// 根据WMS任务创建堆垛机指令
        /// </summary>
        /// <param name="wmsTask"></param>
        /// <returns></returns>
        public SrmCmd BuildSrmInCmdByWmsTask(WmsTask wmsTask)
        {
            SrmCmd srmCmd = new SrmCmd();
            srmCmd.SubTask_No = "";//获取序列号
            srmCmd.Task_No = wmsTask.wmsTaskNo;
            srmCmd.Serial_No = 0;//获取序列号
            srmCmd.Device_No = "SRM" + wmsTask.roadwayNo.Substring(wmsTask.roadwayNo.Length - 2); ;
            srmCmd.Fork_No = "1";
            srmCmd.Station_Type = "0";
            srmCmd.Check_Point = 0;//计算
            srmCmd.From_Station = Convert.ToInt16(wmsTask.toLocationNo);
            srmCmd.To_Station = 0;
            srmCmd.Task_Type = "IN";
            srmCmd.To_Column = 0;
            srmCmd.To_ForkDirection = 0;
            srmCmd.To_Layer = 0;
            srmCmd.To_Deep = 0;
            srmCmd.From_Column = 0;//
            srmCmd.From_ForkDirection = 0;//
            srmCmd.From_Layer = 0;//
            srmCmd.From_Deep = 0;//
            srmCmd.Task_Cmd = 4;
            srmCmd.Pallet_Barcode = Convert.ToInt32(wmsTask.palletBarcode);
            //srmCmd.Pallet_Barcode = wmsTask.palletBarcode;
            srmCmd.Exec_Status = 3;
            srmCmd.Recive_Date = DateTime.Now;
            srmCmd.CreateBy = wmsTask.UpdateBy;
            srmCmd.CreateTime = DateTime.Now;
            return srmCmd;
        }




        /// <summary>
        /// 创建盘点差异记录
        /// </summary>
        /// <param name="wmsItnInventoryRecord"></param>
        /// <param name="wmsStockUniicode"></param>
        /// <param name="difQty"></param>
        /// <returns></returns>
        public WmsItnInventoryRecordDif BuildCheckDifByRecord (WmsItnInventoryRecord wmsItnInventoryRecord, WmsStockUniicode wmsStockUniicode,decimal difQty)
        {
            WmsItnInventoryRecordDif wmsItnInventoryRecordDif = new WmsItnInventoryRecordDif();
            wmsItnInventoryRecordDif.batchNo = wmsStockUniicode.batchNo;
            wmsItnInventoryRecordDif.dataCode = wmsStockUniicode.dataCode;
            wmsItnInventoryRecordDif.delayFrozenFlag=wmsStockUniicode.delayFrozenFlag;
            wmsItnInventoryRecordDif.delayFrozenReason=wmsStockUniicode.delayFrozenReason;
            wmsItnInventoryRecordDif.delayReason=wmsStockUniicode.delayReason;
            wmsItnInventoryRecordDif.delayTimes = wmsStockUniicode.delayTimes;
            wmsItnInventoryRecordDif.delayToEndDate=wmsStockUniicode.delayToEndDate;
            wmsItnInventoryRecordDif.difQty= difQty;
            wmsItnInventoryRecordDif.driedScrapFlag= wmsStockUniicode.driedScrapFlag;
            wmsItnInventoryRecordDif.expDate= wmsStockUniicode.expDate;
            wmsItnInventoryRecordDif.exposeFrozenFlag= wmsStockUniicode.exposeFrozenFlag;
            wmsItnInventoryRecordDif.exposeFrozenReason= wmsStockUniicode.exposeFrozenReason;
            wmsItnInventoryRecordDif.extend1=wmsStockUniicode.extend1;
            wmsItnInventoryRecordDif.extend2=wmsStockUniicode.extend2;
            wmsItnInventoryRecordDif.extend3=wmsStockUniicode.extend3;
            wmsItnInventoryRecordDif.extend4=wmsStockUniicode.extend4;
            wmsItnInventoryRecordDif.extend5=wmsStockUniicode.extend5;
            wmsItnInventoryRecordDif.extend6=wmsStockUniicode.extend6;
            wmsItnInventoryRecordDif.extend7=wmsStockUniicode.extend7;
            wmsItnInventoryRecordDif.extend8=wmsStockUniicode.extend8;
            wmsItnInventoryRecordDif.extend9=wmsStockUniicode.extend9;
            wmsItnInventoryRecordDif.extend10=wmsStockUniicode.extend10;
            wmsItnInventoryRecordDif.extend11=wmsStockUniicode.extend11;
            wmsItnInventoryRecordDif.extend12 = null;
            wmsItnInventoryRecordDif.extend13 = null;
            wmsItnInventoryRecordDif.extend14 = null;
            wmsItnInventoryRecordDif.extend15 = null;
            wmsItnInventoryRecordDif.inspectionResult = wmsStockUniicode.inspectionResult;
            wmsItnInventoryRecordDif.inventoryDtlId = "0";
            wmsItnInventoryRecordDif.inventoryNo = wmsItnInventoryRecord.inventoryNo;
            wmsItnInventoryRecordDif.inventoryQty = wmsItnInventoryRecord.inventoryQty;
            wmsItnInventoryRecordDif.leftMslTimes = wmsStockUniicode.leftMslTimes;
            wmsItnInventoryRecordDif.materialName = wmsStockUniicode.materialName;
            wmsItnInventoryRecordDif.materialCode = wmsStockUniicode.materialCode;
            wmsItnInventoryRecordDif.materialSpec = wmsStockUniicode.materialSpec;
            wmsItnInventoryRecordDif.mslGradeCode = wmsStockUniicode.mslGradeCode;
            wmsItnInventoryRecordDif.packageTime = wmsStockUniicode.packageTime;
            wmsItnInventoryRecordDif.palletBarcode = wmsStockUniicode.palletBarcode;
            wmsItnInventoryRecordDif.positionNo = wmsStockUniicode.positionNo;
            wmsItnInventoryRecordDif.productDate = wmsStockUniicode.productDate;
            wmsItnInventoryRecordDif.projectNo = wmsStockUniicode.projectNo;
            wmsItnInventoryRecordDif.proprietorCode = wmsStockUniicode.proprietorCode;
            wmsItnInventoryRecordDif.realExposeTimes = wmsStockUniicode.realExposeTimes;
            wmsItnInventoryRecordDif.skuCode = wmsStockUniicode.skuCode;
            wmsItnInventoryRecordDif.stockCode = wmsStockUniicode.stockCode;
            wmsItnInventoryRecordDif.stockDtlId = wmsStockUniicode.stockDtlId;
            wmsItnInventoryRecordDif.supplierCode = wmsStockUniicode.supplierCode;
            wmsItnInventoryRecordDif.supplierName = wmsStockUniicode.supplierName;
            wmsItnInventoryRecordDif.supplierExposeTimes = wmsStockUniicode.supplierExposeTimes;
            wmsItnInventoryRecordDif.supplierNameAlias = wmsStockUniicode.supplierNameAlias;
            wmsItnInventoryRecordDif.supplierNameEn = wmsStockUniicode.supplierNameEn;
            wmsItnInventoryRecordDif.uniicode = wmsStockUniicode.uniicode;
            wmsItnInventoryRecordDif.unpackStatus = wmsStockUniicode.unpackStatus;
            wmsItnInventoryRecordDif.unpackTime = wmsStockUniicode.unpackTime;
            wmsItnInventoryRecordDif.whouseNo = wmsStockUniicode.whouseNo;
            wmsItnInventoryRecordDif.areaNo = wmsStockUniicode.areaNo;
            wmsItnInventoryRecordDif.delFlag = "";
            wmsItnInventoryRecordDif.delFlag = wmsStockUniicode.erpWhouseNo;
            wmsItnInventoryRecordDif.inwhTime = wmsStockUniicode.inwhTime;
            wmsItnInventoryRecordDif.occupyQty = wmsStockUniicode.occupyQty;
            wmsItnInventoryRecordDif.qty = wmsStockUniicode.qty;
            wmsItnInventoryRecordDif.unitCode = wmsStockUniicode.unitCode;
            wmsItnInventoryRecordDif.fileedId = wmsStockUniicode.fileedId;
            wmsItnInventoryRecordDif.fileedName = wmsStockUniicode.fileedName;
            wmsItnInventoryRecordDif.oldStockDtlId = "";
            wmsItnInventoryRecordDif.projectNoBak = "";

            return wmsItnInventoryRecordDif;
        }



    }
}
