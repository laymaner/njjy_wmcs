using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using WISH.Helper.Common;
using Wish.Models.ImportDto;
using Wish.ViewModel.System.SysSequenceVMs;
using Newtonsoft.Json;
using Wish.Areas.BasWhouse.Model;
using Wish.Areas.Config.Model;
using Microsoft.EntityFrameworkCore;
using Wish.ViewModel.Common.Dtos;
using Wish.ViewModel.BasWhouse.BasWBinVMs;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using System.Diagnostics;
using System.Net.NetworkInformation;
using Wish.ViewModel.BusinessIn.WmsInReceiptIqcResultVMs;
using Wish.ViewModel.BusinessStock.WmsStockVMs;
using Com.Wish.Model.Base;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Wish.ViewModel.BusinessIn.WmsInOrderVMs;
using MediatR;
using Wish.ViewModel.BasWhouse.BasWRegionVMs;
using Wish.TaskConfig.Model;
using Wish.ViewModel.WcsCmd.SrmCmdVMs;
using Wish.ViewModel.BusinessTask.WmsTaskVMs;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptRecordVMs
{
    public partial class WmsInReceiptRecordVM
    {
        /// <summary>
        /// 根据堆垛机入库请求生成入库记录、库存主表、库存明细表、库存唯一码表、任务表、指令表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> CreateInforBySRM(DealSrmTaskDto input, string invoker)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "根据堆垛机入库请求生成任务信息:";
            var hasParentTransaction = false;
            SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
            BasWBinVM basWBinVM = Wtm.CreateVM<BasWBinVM>();
            if (DC.Database.CurrentTransaction != null)
            {
                hasParentTransaction = true;
            }
            try
            {
                var basRoadways = await DC.Set<BasWRoadway>().Where(x => x.areaNo == "LM").ToListAsync();
                if (basRoadways.Count == 0)
                {
                    msg = $"{desc}:不存在LM区域的巷道";
                    return result.Error(msg);
                }
                string stockNo = input.deviceNo.Substring(input.deviceNo.Length - 2);
                var basRoadway = basRoadways.Where(x => x.roadwayNo.Contains(stockNo)).FirstOrDefault();
                if (basRoadway == null)
                {
                    msg = $"{desc}:不存在{input.deviceNo}设备的巷道";
                    return result.Error(msg);
                }
                List<BasWRoadway> basWRoadwayLists = new List<BasWRoadway>();
                basWRoadwayLists.Add(basRoadway);
                List<string> roadwayNos = new List<string>();
                roadwayNos.Add(basRoadway.roadwayNo);
                var basWWhouse = await DC.Set<BasWWhouse>().FirstOrDefaultAsync();
                if (basWWhouse == null)
                {
                    msg = $"{desc}:不存在{input.deviceNo}设备的仓库";
                    return result.Error(msg);
                }
                var basBMaterial = await DC.Set<BasBMaterial>().Where(x => x.WhouseNo == basWWhouse.whouseNo).FirstOrDefaultAsync();
                if (basBMaterial == null)
                {
                    msg = $"{desc}:不存在{input.deviceNo}设备/仓库{basWWhouse.whouseNo}的物料";
                    return result.Error(msg);
                }
                //查询设备站台对应关系
                var cfgRelationship = await DC.Set<CfgRelationship>().Where(x => x.relationshipTypeCode == "Device&Station" && x.leftCode == input.deviceNo).FirstOrDefaultAsync();
                if (cfgRelationship == null)
                {
                    msg = $"{desc}:不存在{input.deviceNo}设备对应站台关系";
                    return result.Error(msg);
                }
                //判断托盘信息
                var wmsInRecord = await DC.Set<WmsInReceiptRecord>().Where(x => x.ptaPalletBarcode == input.waferID.ToString() && x.inRecordStatus < 90).FirstOrDefaultAsync();
                if (wmsInRecord != null)
                {
                    msg = $"{desc}:存在{input.palletBarcode}的入库记录";
                    return result.Error(msg);
                }
                //判断是否存在任务
                var wmsTaskentity = await DC.Set<WmsTask>().Where(x => x.palletBarcode == input.palletBarcode.ToString()).FirstOrDefaultAsync();
                if (wmsTaskentity != null)
                {
                    if (!wmsTaskentity.wmsTaskType.Contains("CHECK_IN"))
                    {
                        msg = $"{desc}:存在{input.palletBarcode}的任务";
                        return result.Error(msg);
                    }
                }
                //判断是否存在库存
                var wmsStockEntity = await DC.Set<WmsStock>().Where(x => x.palletBarcode == input.palletBarcode.ToString()).FirstOrDefaultAsync();
                if (wmsStockEntity != null)
                {
                    if (wmsTaskentity != null)
                    {
                        if (!wmsTaskentity.wmsTaskType.Contains("CHECK_IN"))
                        {
                            msg = $"{desc}:入库任务已存在{input.palletBarcode}的库存";
                            return result.Error(msg);
                        }
                    }
                }
                else
                {
                    if (wmsTaskentity != null)
                    {
                        if (wmsTaskentity.wmsTaskType.Contains("CHECK_IN"))
                        {
                            msg = $"{desc}:盘点回库任务不存在{input.palletBarcode}的库存";
                            return result.Error(msg);
                        }
                    }
                }
                //判断库存唯一码是否存在
                var stockUniicodeEntity = await DC.Set<WmsStockUniicode>().Where(x => x.uniicode == input.waferID).FirstOrDefaultAsync();
                if (stockUniicodeEntity !=null)
                {
                    try
                    {
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.BeginTransactionAsync();
                        }
                        var wmsStockAlarm = await DC.Set<WmsStock>().Where(x => x.palletBarcode == stockUniicodeEntity.palletBarcode && x.stockCode == stockUniicodeEntity.stockCode).FirstOrDefaultAsync();
                        BasWBin basWBin = new BasWBin();
                        if (wmsStockAlarm != null)
                        {
                            basWBin= await DC.Set<BasWBin>().Where(x => x.binNo == wmsStockAlarm.binNo).FirstOrDefaultAsync();
                        }
                        msg = $"{desc}:库内已存在{input.waferID}的库存唯一码记录";
                        string palletBarcode1 = await sysSequenceVM.GetSequenceAsync(SequenceCode.palletBarCode.GetCode());
                        input.palletBarcode = Convert.ToInt32(palletBarcode1.Substring(4, 8));
                        //生成指令表
                        SrmCmd srmCmd = new SrmCmd();
                        srmCmd = CreateSrmCmdByAlarm(input, basWBin);
                        srmCmd.SubTask_No = await sysSequenceVM.GetSequenceAsync(SequenceCode.srmCmdNo.GetCode());
                        srmCmd.Serial_No = Convert.ToInt16(await sysSequenceVM.GetSequenceAsync(input.deviceNo));
                        srmCmd.Check_Point = (short)(srmCmd.Serial_No + srmCmd.To_ForkDirection + srmCmd.To_Column + srmCmd.To_Layer + srmCmd.From_Station);
                        if (!string.IsNullOrWhiteSpace(invoker))
                        {
                            srmCmd.CreateBy = invoker;
                        }
                        srmCmd.Remark_Desc = $"库内已存在{input.waferID}的库存唯一码记录";
                        await ((DbContext)DC).Set<SrmCmd>().SingleInsertAsync(srmCmd);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.BeginTransactionAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (hasParentTransaction == false)
                        {
                            await DC.Database.RollbackTransactionAsync();
                        }
                        msg = msg+ $"{desc}{ex.Message}";
                        return result.Error(msg);
                    }
                    return result.Success(msg);
                }
                //判断是否存在指令
                var srmCmdEntity= await DC.Set<SrmCmd>().Where(x=>x.Pallet_Barcode==input.palletBarcode || x.WaferID == input.waferID).FirstOrDefaultAsync();
                if (srmCmdEntity != null)
                {
                    if (wmsTaskentity != null)
                    {
                        if (!wmsTaskentity.wmsTaskType.Contains("CHECK_IN"))
                        {
                            msg = $"{desc}:入库任务已存在{input.palletBarcode}的指令";
                            return result.Error(msg);
                        }
                    }
                }
                else
                {
                    if (wmsTaskentity != null)
                    {
                        if (wmsTaskentity.wmsTaskType.Contains("CHECK_IN"))
                        {
                            msg = $"{desc}:盘点回库任务不存在{input.palletBarcode}的指令";
                            return result.Error(msg);
                        }
                    }
                }
                //分配库位
                AllotBinForMatDto allotBinForMatDto = new AllotBinForMatDto()
                {
                    loadedType = 1,
                    palletBarcode = input.palletBarcode.ToString(),
                    erpWhouseNo = "",
                    sdType = "SS",
                    roadwayList = roadwayNos,
                    regionNo = basRoadway.regionNo,
                    height = 500,
                };

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                result = await basWBinVM.QueryBinForSS(allotBinForMatDto, basWRoadwayLists);
                stopwatch.Stop();
                TimeSpan elapsedTime = stopwatch.Elapsed;

                logger.Warn($"----->库位分配----->{desc}{input.deviceNo}库位分配用时【{elapsedTime.TotalMilliseconds}】毫秒");
                Console.WriteLine($"{DateTime.Now}--->----->Warn----->{desc}:{input.deviceNo}库位分配用时【{elapsedTime.TotalMilliseconds}】毫秒");
                if (result.code == ResCode.Error)
                {
                    result.msg = $"{desc}" + result.msg;
                    return result;
                }
                else
                {
                    AllotBinResultDto allotBinResult = (AllotBinResultDto)result.outParams;
                    logger.Warn($"----->库位分配----->{desc}:{result.msg} --》库位：【{allotBinResult.binNo}】，返回结果：{JsonConvert.SerializeObject(allotBinResult)}");
                    BasWBin basWBin = await DC.Set<BasWBin>().Where(x => x.binNo == allotBinResult.binNo).FirstOrDefaultAsync();
                    if (basWBin == null)
                    {
                        msg = $"{desc}:库存表中不存在该库位【{allotBinResult.binNo}】";
                        return result.Error(msg);
                    }
                    if (hasParentTransaction == false)
                    {
                        await DC.Database.BeginTransactionAsync();
                    }
                    if (wmsTaskentity != null && wmsTaskentity.wmsTaskType.Contains("CHECK_IN"))
                    {
                        //更新库存主表货位信息
                        wmsStockEntity.binNo = allotBinResult.binNo;
                        wmsStockEntity.roadwayNo = allotBinResult.roadwayNo;
                        wmsStockEntity.locNo = cfgRelationship.rightCode;
                        if (!string.IsNullOrWhiteSpace(invoker))
                        {
                            wmsStockEntity.UpdateBy = invoker;
                        }
                        wmsStockEntity.UpdateTime = DateTime.Now;
                        await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(wmsStockEntity);
                        //更新任务
                        wmsTaskentity.frLocationNo = wmsStockEntity.locNo;
                        wmsTaskentity.toLocationNo = wmsStockEntity.binNo;
                        wmsTaskentity.roadwayNo = wmsStockEntity.roadwayNo;
                        if (!string.IsNullOrWhiteSpace(invoker))
                        {
                            wmsTaskentity.UpdateBy = invoker;
                        }
                        wmsTaskentity.UpdateTime = DateTime.Now;
                        await ((DbContext)DC).Set<WmsTask>().SingleUpdateAsync(wmsTaskentity);
                        //更新指令
                        srmCmdEntity.To_Column = (short)basWBin.binCol;
                        srmCmdEntity.To_ForkDirection = (short)basWBin.binRow;
                        srmCmdEntity.To_Layer = (short)basWBin.binLayer;
                        srmCmdEntity.From_Station = Convert.ToInt16(wmsTaskentity.frLocationNo);
                        srmCmdEntity.Check_Point = (short)(srmCmdEntity.Serial_No + srmCmdEntity.To_ForkDirection + srmCmdEntity.To_Column + srmCmdEntity.To_Layer+ srmCmdEntity.From_Station);
                        if (!string.IsNullOrWhiteSpace(invoker))
                        {
                            srmCmdEntity.CreateBy = invoker;
                        }
                        srmCmdEntity.CreateTime = DateTime.Now;
                        await ((DbContext)DC).Set<SrmCmd>().SingleUpdateAsync(srmCmdEntity);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                    }
                    else
                    {
                        string palletBarcode1= await sysSequenceVM.GetSequenceAsync(SequenceCode.palletBarCode.GetCode());
                        input.palletBarcode = Convert.ToInt32(palletBarcode1.Substring(4,8));
                        //生成入库记录
                        WmsInReceiptRecord wmsInReceiptRecord = new WmsInReceiptRecord();
                        wmsInReceiptRecord = CreateWmsInReceiptRecord(input, allotBinResult, basWWhouse, basBMaterial);
                        if (!string.IsNullOrWhiteSpace(invoker))
                        {
                            wmsInReceiptRecord.CreateBy = invoker;
                        }
                        wmsInReceiptRecord.inNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.InOrderNo.GetCode());

                        //生成库存主表
                        WmsStock wmsStock = new WmsStock();
                        wmsStock = CreateWmsSockByInRecord(wmsInReceiptRecord, allotBinResult, cfgRelationship.rightCode);
                        wmsStock.stockCode = await sysSequenceVM.GetSequenceAsync(SequenceCode.StockCode.GetCode()); // 库存编码
                        if (!string.IsNullOrWhiteSpace(invoker))
                        {
                            wmsStock.CreateBy = invoker;
                        }
                        await ((DbContext)DC).Set<WmsStock>().SingleInsertAsync(wmsStock);

                        //生成库存明细表
                        WmsStockDtl wmsStockDtl = new WmsStockDtl();
                        wmsStockDtl = CreateWmsStockDtlByStock(wmsInReceiptRecord, wmsStock);
                        if (!string.IsNullOrWhiteSpace(invoker))
                        {
                            wmsStockDtl.CreateBy = invoker;
                        }
                        await ((DbContext)DC).Set<WmsStockDtl>().SingleInsertAsync(wmsStockDtl);
                        await ((DbContext)DC).BulkSaveChangesAsync();

                        //入库记录更新库存关联内容
                        wmsInReceiptRecord.stockCode = wmsStock.stockCode;
                        wmsInReceiptRecord.ptaStockCode = wmsStock.stockCode;
                        wmsInReceiptRecord.stockDtlId = wmsStockDtl.ID;
                        wmsInReceiptRecord.ptaStockDtlId = wmsStockDtl.ID;
                        await ((DbContext)DC).Set<WmsInReceiptRecord>().SingleInsertAsync(wmsInReceiptRecord);

                        //生成库存唯一码表
                        WmsStockUniicode wmsStockUniicode = new WmsStockUniicode();
                        wmsStockUniicode = CreateWmsStockUniicodeByStockDtl(input,wmsStockDtl, wmsInReceiptRecord);
                        if (!string.IsNullOrWhiteSpace(invoker))
                        {
                            wmsStockUniicode.CreateBy = invoker;
                        }
                        await ((DbContext)DC).Set<WmsStockUniicode>().SingleInsertAsync(wmsStockUniicode);
                        await ((DbContext)DC).BulkSaveChangesAsync();

                        //生成任务表
                        WmsTask wmsTask = new WmsTask();
                        wmsTask = CreateWmsTaskByInfo(wmsInReceiptRecord, wmsStock);
                        wmsTask.wmsTaskNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.wmsTaskNo.GetCode());
                        if (!string.IsNullOrWhiteSpace(invoker))
                        {
                            wmsTask.CreateBy = invoker;
                        }
                        await ((DbContext)DC).Set<WmsTask>().SingleInsertAsync(wmsTask);

                        //生成指令表
                        SrmCmd srmCmd = new SrmCmd();
                        srmCmd = CreateSrmCmdByWmsTask(input, wmsTask, basWBin);
                        srmCmd.SubTask_No = await sysSequenceVM.GetSequenceAsync(SequenceCode.srmCmdNo.GetCode());
                        srmCmd.Serial_No = Convert.ToInt16(await sysSequenceVM.GetSequenceAsync(input.deviceNo));
                        srmCmd.Check_Point = (short)(srmCmd.Serial_No + srmCmd.To_ForkDirection + srmCmd.To_Column + srmCmd.To_Layer+ srmCmd.From_Station);
                        if (!string.IsNullOrWhiteSpace(invoker))
                        {
                            srmCmd.CreateBy = invoker;
                        }
                        await ((DbContext)DC).Set<SrmCmd>().SingleInsertAsync(srmCmd);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                    }

                    if (hasParentTransaction == false)
                    {
                        await DC.Database.CommitTransactionAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                if (hasParentTransaction == false)
                {
                    await DC.Database.RollbackTransactionAsync();
                }
                msg = $"{desc}{ex.Message}";
                return result.Error(msg);
            }
            msg = $"{desc}时间【{DateTime.Now}】,入参【{JsonConvert.SerializeObject(input)}】";
            logger.Warn($"----->Warn----->{desc}:{msg} ");
            return result.Success(msg);
        }
        /// <summary>
        /// 创建入库记录
        /// </summary>
        /// <param name="input"></param>
        /// <param name="binInfo"></param>
        /// <param name="basWWhouse"></param>
        /// <param name="basBMaterial"></param>
        /// <returns></returns>
        public WmsInReceiptRecord CreateWmsInReceiptRecord(DealSrmTaskDto input, AllotBinResultDto binInfo, BasWWhouse basWWhouse, BasBMaterial basBMaterial)
        {
            WmsInReceiptRecord wmsInReceiptRecord = new WmsInReceiptRecord();
            wmsInReceiptRecord.areaNo = "LM"; // 区域
            wmsInReceiptRecord.erpWhouseNo = ""; // ERP仓库号
            wmsInReceiptRecord.whouseNo = basWWhouse.whouseNo;
            wmsInReceiptRecord.regionNo = binInfo.regionNo; // 库区
            wmsInReceiptRecord.ptaRegionNo = binInfo.regionNo; // 库区
            wmsInReceiptRecord.binNo = binInfo.binNo; // 库位
            wmsInReceiptRecord.proprietorCode = basWWhouse.whouseNo; // 货主
            wmsInReceiptRecord.iqcResultNo = ""; // 检验单号
            wmsInReceiptRecord.receiptNo = ""; // 收货单号
            wmsInReceiptRecord.receiptDtlId = null; // 收货明细ID
            wmsInReceiptRecord.inNo = ""; // 入库单号---------------
            wmsInReceiptRecord.inDtlId = null; // 入库单明细ID
            wmsInReceiptRecord.externalInNo = ""; // 外部入库单号
            wmsInReceiptRecord.externalInDtlId = ""; // 外部入库单行号
            wmsInReceiptRecord.orderNo = ""; // 关联单号
            wmsInReceiptRecord.orderDtlId = null; // 关联单行号
            wmsInReceiptRecord.docTypeCode = "2001"; // 单据类型
            wmsInReceiptRecord.sourceBy = 0; // 数据来源
            wmsInReceiptRecord.inOutTypeNo = "IN"; // 出入库类别代码
            wmsInReceiptRecord.inOutName = "IN"; // 出入库名称
            wmsInReceiptRecord.stockCode = ""; // 库存编码---------------
            wmsInReceiptRecord.stockDtlId = 0; // 库存编码---------------
            wmsInReceiptRecord.ptaStockCode = ""; // 库存编码---------------
            wmsInReceiptRecord.ptaStockDtlId = 0; // 库存编码---------------
            wmsInReceiptRecord.palletBarcode = input.palletBarcode.ToString(); // 载体条码
            //wmsInReceiptRecord.loadedType = Convert.ToInt32(loadedType); // 装载类型 : 1:实盘 ；2:工装；0：空盘；
            wmsInReceiptRecord.loadedType = 1; // 装载类型 : 1:实盘 ；2:工装；0：空盘；
            wmsInReceiptRecord.ptaBinNo = binInfo.binNo; // 实际上架库位号
            wmsInReceiptRecord.ptaPalletBarcode = input.waferID.ToString(); // 实际上架后的托盘号--晶圆ID
            wmsInReceiptRecord.returnFlag = 0; // 回传状态：0默认，1可回传，2回传失败，3回传成功，4无需回传
            wmsInReceiptRecord.returnTime = DateTime.Now; // 回传时间
            //wmsInReceiptRecord.returnResult = returnResult;                                                // 回传结果
            wmsInReceiptRecord.materialCode = basBMaterial.MaterialCode; // 物料代码
            wmsInReceiptRecord.materialName = basBMaterial.MaterialName; // 物料名称
            wmsInReceiptRecord.supplierCode = ""; // 供应商编码
            wmsInReceiptRecord.supplierName = ""; // 供应商名称
            wmsInReceiptRecord.supplierNameEn = ""; // 供应商名称-英文
            wmsInReceiptRecord.supplierNameAlias = ""; // 供应商名称-其他
            wmsInReceiptRecord.batchNo = ""; // 批次
            wmsInReceiptRecord.materialSpec = basBMaterial.MaterialSpec; // 规格型号
            wmsInReceiptRecord.recordQty = 1; // 组盘数量
            wmsInReceiptRecord.inspectionResult = Convert.ToInt32(InspectionResult.Qualitified.GetCode()); // 质检结果：待检、合格、特采、不合格
            wmsInReceiptRecord.inRecordStatus = 41; // 状态：0：初始创建（组盘完成）；41：入库中；90入库完成；92删除（撤销）；93强制完成
            wmsInReceiptRecord.skuCode = ""; // SKU 编码
            wmsInReceiptRecord.departmentName = ""; // 部门名称
            wmsInReceiptRecord.projectNo = ""; // 项目号
            wmsInReceiptRecord.ticketNo = ""; // 工单号
            wmsInReceiptRecord.inspector = ""; // 质检员
            wmsInReceiptRecord.minPkgQty = 1; // 包装数量
            wmsInReceiptRecord.urgentFlag = null; // 急料标记
            wmsInReceiptRecord.unitCode = basBMaterial.UnitCode; // 单位
            //wmsInReceiptRecord.extend1 = ""; // 备用字段1
            //wmsInReceiptRecord.extend2 = ""; // 备用字段2
            //wmsInReceiptRecord.extend3 = ""; // 备用字段3
            //wmsInReceiptRecord.extend4 = ""; // 备用字段4
            //wmsInReceiptRecord.extend5 = ""; // 备用字段5
            //wmsInReceiptRecord.extend6 = ""; // 备用字段6
            //wmsInReceiptRecord.extend7 = ""; // 备用字段7
            //wmsInReceiptRecord.extend8 = ""; // 备用字段8
            //wmsInReceiptRecord.extend9 = ""; // 备用字段9
            //wmsInReceiptRecord.extend10 = ""; // 备用字段10 
            //wmsInReceiptRecord.extend11 = ""; // 备用字段11
            //wmsInReceiptRecord.extend12 = ""; // 备用字段12
            //wmsInReceiptRecord.extend13 = ""; // 备用字段13
            //wmsInReceiptRecord.extend14 = ""; // 备用字段14
            //wmsInReceiptRecord.extend15 = ""; // 备用字段15
            wmsInReceiptRecord.CreateTime = DateTime.Now;
            wmsInReceiptRecord.UpdateTime = DateTime.Now;
            wmsInReceiptRecord.CreateBy = input.deviceNo;
            wmsInReceiptRecord.UpdateBy = input.deviceNo;
            return wmsInReceiptRecord;
        }

        /// <summary>
        /// 根据入库记录创建库存主表
        /// </summary>
        /// <param name="wmsInReceiptRecord"></param>
        /// <param name="binInfo"></param>
        /// <param name="locNo"></param>
        /// <returns></returns>
        public WmsStock CreateWmsSockByInRecord(WmsInReceiptRecord wmsInReceiptRecord, AllotBinResultDto binInfo, string locNo)
        {
            WmsStock wmsStock = new WmsStock()
            {
                areaNo = wmsInReceiptRecord.areaNo,
                binNo = wmsInReceiptRecord.ptaBinNo,
                errFlag = 0,
                errMsg = "",
                height = 500,
                invoiceNo = "",
                loadedType = wmsInReceiptRecord.loadedType,
                locAllotGroup = "",
                locNo = locNo,
                palletBarcode = wmsInReceiptRecord.palletBarcode,
                proprietorCode = wmsInReceiptRecord.proprietorCode,
                regionNo = wmsInReceiptRecord.regionNo,
                roadwayNo = binInfo.roadwayNo,
                specialFlag = 0,
                specialMsg = "",
                stockCode = "",//序列号
                stockStatus = Convert.ToInt32(StockStatus.InStoreing.GetCode()),
                weight = 0,
                whouseNo = wmsInReceiptRecord.whouseNo,
                CreateBy = wmsInReceiptRecord.CreateBy,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                UpdateBy = wmsInReceiptRecord.UpdateBy,
            };
            return wmsStock;
        }

        /// <summary>
        /// 根据入库记录库存生成库存明细
        /// </summary>
        /// <param name="wmsInReceiptRecord"></param>
        /// <param name="wmsStock"></param>
        /// <returns></returns>
        public WmsStockDtl CreateWmsStockDtlByStock(WmsInReceiptRecord wmsInReceiptRecord, WmsStock wmsStock)
        {
            WmsStockDtl wmsStockDtl = new WmsStockDtl()
            {
                areaNo = wmsStock.areaNo,
                erpWhouseNo = wmsInReceiptRecord.erpWhouseNo,
                inspectionResult = Convert.ToInt32(InspectionResult.Qualitified.GetCode()),
                lockFlag = 0,
                lockReason = "",
                materialCode = wmsInReceiptRecord.materialCode,
                materialName = wmsInReceiptRecord.materialName,
                materialSpec = wmsInReceiptRecord.materialSpec,
                occupyQty = 0,
                palletBarcode = wmsInReceiptRecord.palletBarcode,
                projectNo = wmsInReceiptRecord.projectNo,
                proprietorCode = wmsInReceiptRecord.proprietorCode,
                qty = wmsInReceiptRecord.recordQty,
                skuCode = wmsInReceiptRecord.skuCode,
                stockDtlStatus = Convert.ToInt32(StockStatus.InStoreing.GetCode()),
                stockCode = wmsStock.stockCode,
                supplierCode = wmsInReceiptRecord.supplierCode,
                supplierName = wmsInReceiptRecord.supplierName,
                supplierNameAlias = wmsInReceiptRecord.supplierNameAlias,
                supplierNameEn = wmsInReceiptRecord.supplierNameEn,
                whouseNo = wmsInReceiptRecord.whouseNo,
                unitCode = wmsInReceiptRecord.unitCode,
                CreateBy = wmsInReceiptRecord.CreateBy,
                CreateTime = DateTime.Now,
                UpdateBy = wmsInReceiptRecord.UpdateBy,
                UpdateTime = DateTime.Now,
            };
            return wmsStockDtl;
        }

        /// <summary>
        /// 根据库存明细生成库存唯一码
        /// </summary>
        /// <param name="wmsStockDtl"></param>
        /// <param name="wmsInReceiptRecord"></param>
        /// <returns></returns>
        public WmsStockUniicode CreateWmsStockUniicodeByStockDtl(DealSrmTaskDto input, WmsStockDtl wmsStockDtl, WmsInReceiptRecord wmsInReceiptRecord)
        {
            WmsStockUniicode wmsStockUniicode = new WmsStockUniicode()
            {
                areaNo = wmsStockDtl.areaNo,
                batchNo = wmsInReceiptRecord.batchNo,
                dataCode = "",
                delayFrozenFlag = 0,
                delayTimes = 0,
                driedScrapFlag = 0,
                driedTimes = 0,
                erpWhouseNo = wmsInReceiptRecord.erpWhouseNo,
                exposeFrozenFlag = 0,
                inspectionResult = Convert.ToInt32(InspectionResult.Qualitified.GetCode()),
                inwhTime = DateTime.Now,
                leftMslTimes = 0,
                materialCode = wmsStockDtl.materialCode,
                materialName = wmsStockDtl.materialName,
                materialSpec = wmsStockDtl.materialSpec,
                occupyQty = 0,
                palletBarcode = wmsStockDtl.palletBarcode,
                projectNo = wmsStockDtl.projectNo,
                proprietorCode = wmsStockDtl.proprietorCode,
                skuCode = wmsStockDtl.skuCode,
                stockCode = wmsStockDtl.stockCode,
                stockDtlId = wmsStockDtl.ID,
                qty = wmsStockDtl.qty,
                supplierCode = wmsStockDtl.supplierCode,
                supplierExposeTimes = 0,
                supplierName = wmsStockDtl.supplierName,
                supplierNameAlias = wmsStockDtl.supplierNameAlias,
                supplierNameEn = wmsStockDtl.supplierNameEn,
                uniicode = input.waferID,
                unitCode = wmsStockDtl.unitCode,
                whouseNo = wmsStockDtl.whouseNo,
                CreateBy = wmsStockDtl.CreateBy,
                CreateTime = DateTime.Now,
                UpdateBy = wmsStockDtl.UpdateBy,
                UpdateTime = DateTime.Now,
            };
            return wmsStockUniicode;
        }

        /// <summary>
        /// 根据入库记录创建WMS任务
        /// </summary>
        /// <param name="wmsInReceiptRecord"></param>
        /// <param name="wmsStock"></param>
        /// <returns></returns>
        public WmsTask CreateWmsTaskByInfo(WmsInReceiptRecord wmsInReceiptRecord, WmsStock wmsStock)
        {
            WmsTask wmsTaskEntity = new WmsTask();
            wmsTaskEntity.feedbackDesc = String.Empty;
            wmsTaskEntity.feedbackStatus = 0;
            wmsTaskEntity.frLocationNo = wmsStock.locNo;
            wmsTaskEntity.frLocationType = 0;
            wmsTaskEntity.loadedType = wmsStock.loadedType;
            wmsTaskEntity.matHeight = wmsStock.height;//高
            wmsTaskEntity.matLength = null;//长
            wmsTaskEntity.matQty = null;
            wmsTaskEntity.matWeight = null;//重量
            wmsTaskEntity.matWidth = null;//宽
            wmsTaskEntity.orderNo = wmsInReceiptRecord.inNo;
            wmsTaskEntity.palletBarcode = wmsStock.palletBarcode;
            wmsTaskEntity.proprietorCode = wmsStock.proprietorCode;
            wmsTaskEntity.regionNo = wmsStock.regionNo;
            wmsTaskEntity.roadwayNo = wmsStock.roadwayNo;
            wmsTaskEntity.stockCode = wmsStock.stockCode;
            wmsTaskEntity.taskDesc = "初始创建";
            wmsTaskEntity.taskPriority = 100;
            wmsTaskEntity.taskStatus = 10;
            wmsTaskEntity.taskTypeNo = "IN";
            wmsTaskEntity.toLocationNo = wmsStock.binNo;
            wmsTaskEntity.toLocationType = 0;
            wmsTaskEntity.whouseNo = wmsStock.whouseNo;
            wmsTaskEntity.wmsTaskNo = "";//序列号
            wmsTaskEntity.wmsTaskType = wmsStock.loadedType == 1 ? "IN" : "EMPTY_IN";
            wmsTaskEntity.CreateBy = wmsStock.CreateBy;
            wmsTaskEntity.CreateTime = DateTime.Now;
            wmsTaskEntity.UpdateBy = wmsStock.UpdateBy;
            wmsTaskEntity.UpdateTime = DateTime.Now;
            return wmsTaskEntity;
        }

        /// <summary>
        /// 根据WMS任务创建堆垛机指令
        /// </summary>
        /// <param name="input"></param>
        /// <param name="wmsTask"></param>
        /// <param name="basWBin"></param>
        /// <returns></returns>
        public SrmCmd CreateSrmCmdByWmsTask(DealSrmTaskDto input, WmsTask wmsTask, BasWBin basWBin)
        {
            SrmCmd srmCmd = new SrmCmd();
            srmCmd.SubTask_No = "";
            srmCmd.Task_No = wmsTask.wmsTaskNo;
            srmCmd.Serial_No = 0;
            srmCmd.Device_No = input.deviceNo;
            srmCmd.Fork_No = "1";
            srmCmd.Station_Type = "0";
            srmCmd.Check_Point = 0;
            srmCmd.From_Station = Convert.ToInt16(wmsTask.frLocationNo);
            srmCmd.Task_Type = "IN";
            srmCmd.To_Column = (short)basWBin.binCol;
            srmCmd.To_ForkDirection = (short)basWBin.binRow;
            srmCmd.To_Layer = (short)basWBin.binLayer;
            srmCmd.To_Deep = 0;
            srmCmd.From_Column = 0;
            srmCmd.From_ForkDirection = 0;
            srmCmd.From_Layer = 0;
            srmCmd.From_Deep = 0;
            srmCmd.Task_Cmd = 4;
            srmCmd.Pallet_Barcode = Convert.ToInt32(input.palletBarcode);
            //srmCmd.Pallet_Barcode = input.palletBarcode;
            srmCmd.WaferID = input.waferID;
            srmCmd.Exec_Status = 0;
            srmCmd.Recive_Date = DateTime.Now;
            srmCmd.CreateBy = wmsTask.CreateBy;
            srmCmd.CreateTime = DateTime.Now;
            return srmCmd;
        }
        /// <summary>
        /// 根据WMS任务创建堆垛机指令
        /// </summary>
        /// <param name="input"></param>
        /// <param name="wmsTask"></param>
        /// <param name="basWBin"></param>
        /// <returns></returns>
        public SrmCmd CreateSrmCmdByAlarm(DealSrmTaskDto input,BasWBin basWBin)
        {
            SrmCmd srmCmd = new SrmCmd();
            srmCmd.SubTask_No = "";
            srmCmd.Task_No = "WMS99999999";
            srmCmd.Serial_No = 0;
            srmCmd.Device_No = input.deviceNo;
            srmCmd.Fork_No = input.deviceNo;
            srmCmd.Station_Type = "0";
            srmCmd.Check_Point = 0;
            srmCmd.From_Station = Convert.ToInt16(1);
            srmCmd.Task_Type = "IN";
            srmCmd.To_Column = (short)(basWBin?.binCol != null ? basWBin.binCol : 1);
            srmCmd.To_ForkDirection = (short)(basWBin?.binRow != null ? basWBin.binRow : 1);
            srmCmd.To_Layer = (short)(basWBin?.binLayer != null ? basWBin.binLayer : 1);
            srmCmd.To_Deep = 0;
            srmCmd.From_Column = 0;
            srmCmd.From_ForkDirection = 0;
            srmCmd.From_Layer = 0;
            srmCmd.From_Deep = 0;
            srmCmd.Task_Cmd = 27;//库内有相同晶圆ID报警
            srmCmd.Pallet_Barcode = Convert.ToInt32(input.palletBarcode);
            //srmCmd.Pallet_Barcode = input.palletBarcode;
            srmCmd.WaferID = input.waferID;
            srmCmd.Exec_Status = 0;
            srmCmd.Recive_Date = DateTime.Now;
            srmCmd.CreateBy = input.deviceNo;
            srmCmd.CreateTime = DateTime.Now;
            return srmCmd;
        }
    }
}
