using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using log4net;
using Wish.ViewModel.BusinessPutaway.WmsPutawayVMs;
using AutoMapper;
using Wish.TaskConfig.Model;
using Wish.Areas.BasWhouse.Model;
using System.Text.RegularExpressions;


namespace Wish.ViewModel.BusinessTask.WmsTaskVMs
{
    public partial class WmsTaskVM
    {
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
        /// <summary>
        /// 创建WCS出库指令
        /// </summary>
        /// <param name="cmdNo"></param>
        /// <param name="deviceNo"></param>
        /// <param name="wmsTask"></param>
        /// <param name="serialNo"></param>
        /// <param name="basWBin"></param>
        /// <param name="taskType"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public SrmCmd BuildOutSrmCmd(string cmdNo,string deviceNo,WmsTask wmsTask,Int16 serialNo,BasWBin basWBin,string taskType,string invoker)
        {
            SrmCmd srmCmd = new SrmCmd()
            {
                SubTask_No = cmdNo,
                Task_No = wmsTask.wmsTaskNo,
                Serial_No = serialNo,
                Device_No = deviceNo,
                Fork_No="1",
                Station_Type="0",
                Check_Point= (short)(serialNo +basWBin.binRow+ basWBin.binCol+ basWBin.binLayer+ Convert.ToInt16(wmsTask.toLocationNo)),
                Task_Cmd=4,
                Task_Type= taskType,
                Pallet_Barcode = Convert.ToInt32(Regex.Replace(wmsTask.palletBarcode, "[a-zA-Z]", "")),
                //Pallet_Barcode = wmsTask.palletBarcode,
                Exec_Status=3,
                From_Station=0,
                From_ForkDirection=(short)basWBin.binRow,
                From_Column=(short)basWBin.binCol,
                From_Layer=(short)basWBin.binLayer,
                From_Deep=0,
                To_Station= Convert.ToInt16(wmsTask.toLocationNo),
                To_ForkDirection=0,
                To_Column=0,
                To_Layer=0,
                To_Deep=0,
                Recive_Date=DateTime.Now,
                CreateTime= DateTime.Now,
                CreateBy=invoker
            };
            return srmCmd;
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
            wmsItnMoveRecord.UpdateBy = invoker;                                                // 创建人
            wmsItnMoveRecord.UpdateTime = DateTime.Now;  // 创建时间
            return wmsItnMoveRecord;
        }
    }
}
