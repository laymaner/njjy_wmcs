using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using log4net;
using WISH.Helper.Common;
using Newtonsoft.Json;
using Wish.ViewModel.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using Microsoft.AspNetCore.Http;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using Wish.Areas.Config.Model;
using Wish.ViewModel.System.SysSequenceVMs;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Wish.Areas.BasWhouse.Model;
using Wish.TaskConfig.Model;
using NPOI.SS.Formula.Functions;


namespace Wish.ViewModel.BusinessStock.WmsStockVMs
{
    public partial class WmsStockVM
    {
        /// <summary>
        /// 根据入库反馈后外部接口回传信息更新库存晶圆ID属性
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> UpdateStockInfoByInter(List<FeedbackDto> input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "入库完成调用MES接口反馈:";
            var hasParentTransaction = false;
            if (DC.Database.CurrentTransaction != null)
            {
                hasParentTransaction = true;
            }

            try
            {
                if (input == null || input.Count == 0)
                {
                    msg = $"{desc}入参为空";
                    return result.Error(msg);
                }
                logger.Warn($"----->Warn----->{desc}--入参:{JsonConvert.SerializeObject(input)} ");
                var waferIDs = input.Select(x => x.waferID).ToList();
                if (waferIDs.Where(x => string.IsNullOrWhiteSpace(x)).Any())
                {
                    msg = $"{desc}返回的数据中有空的晶圆ID，晶圆ID集合{JsonConvert.SerializeObject(waferIDs)}";
                    return result.Error(msg);
                }
                bool sameWaferIDs = waferIDs.SelectMany(s => waferIDs.Where(s2 => s2 == s), (s, s2) => s).Count() != waferIDs.Count;
                if (sameWaferIDs)
                {
                    msg = $"{desc}返回的数据中有重复的晶圆ID，晶圆ID集合{JsonConvert.SerializeObject(waferIDs)}";
                    return result.Error(msg);
                }
                var stockUniicodes = await DC.Set<WmsStockUniicode>().Where(x => waferIDs.Contains(x.uniicode)).ToListAsync();
                var waferIDCounts = waferIDs.ToList().Count();
                if (stockUniicodes.Count == 0 || stockUniicodes.Count != waferIDCounts)
                {
                    msg = $"{desc}根据返回的数据中的晶圆ID查询不到库存唯一码，或查询到的库存唯一码行数：{stockUniicodes.Count}与晶圆ID行数：{waferIDCounts}不一致";
                    return result.Error(msg);
                }
                var stockDtlIds = stockUniicodes.Select(x => x.stockDtlId).Distinct().ToList();
                var stockDtls = await DC.Set<WmsStockDtl>().Where(x => stockDtlIds.Contains(x.ID)).ToListAsync();
                if (stockDtls.Count != stockDtlIds.Count)
                {
                    msg = $"{desc}根据查询到的库存唯一码与库存明细做对比，库存唯一码中库存明细ID的行数：{stockUniicodes.Count}与库存明细行数：{waferIDCounts}不一致";
                    return result.Error(msg);
                }
                bool isInStock = stockDtls.Where(x => x.stockDtlStatus != 50).ToList().Any();
                if (isInStock)
                {
                    msg = $"{desc}根据查询到的库存明细，存在不是在库的记录";
                    return result.Error(msg);
                }
                if (hasParentTransaction == false)
                {
                    await DC.Database.BeginTransactionAsync();
                }
                foreach (var item in input)
                {
                    DateTime dafFileAppTime = StringTransferDatetime(item.dafFilmAppTime.ToString());
                    DateTime dafExpiryTime = StringTransferDatetime(item.dafExpiryTime.ToString());
                    var stockUniicode = await DC.Set<WmsStockUniicode>().Where(x => x.uniicode == item.waferID).FirstOrDefaultAsync();
                    if (stockUniicode != null)
                    {
                        var stockDtl = await DC.Set<WmsStockDtl>().Where(x => x.ID == stockUniicode.stockDtlId).FirstOrDefaultAsync();
                        if (stockDtl != null)
                        {
                            stockDtl.areaNo = item.sourceBy;
                            //stockDtl.batchNo = item.batchNo;
                            stockDtl.dafType = item.dafType;
                            stockDtl.chipModel = item.chipModel;
                            stockDtl.qty = item.qty;
                            //stockDtl.delayToEndDate = dafFileAppTime;
                            //stockDtl.expDate = dafExpiryTime;
                            stockDtl.chipThickness = item.chipThickness;
                            stockDtl.chipSize = item.chipSize;
                            //stockDtl.unpackStatus = Convert.ToInt32(item.isOrder);
                            stockDtl.projectNo = item.invoiceNo;
                            stockDtl.supplierCode = item.customerNo;
                            stockDtl.UpdateBy = "MES_IN";
                            stockDtl.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).Set<WmsStockDtl>().SingleUpdateAsync(stockDtl);
                        }
                        var wmsStock = await DC.Set<WmsStock>().Where(x => x.stockCode == stockUniicode.stockCode).FirstOrDefaultAsync();
                        if (wmsStock != null)
                        {
                            wmsStock.areaNo = item.sourceBy;
                            wmsStock.UpdateBy = "MES_IN";
                            wmsStock.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(wmsStock);
                        }
                        stockUniicode.areaNo = item.sourceBy;
                        stockUniicode.batchNo = item.batchNo;
                        stockUniicode.dafType = item.dafType;
                        stockUniicode.chipModel = item.chipModel;
                        stockUniicode.qty = item.qty;
                        stockUniicode.delayToEndDate = dafFileAppTime;
                        stockUniicode.expDate = dafExpiryTime;
                        stockUniicode.chipThickness = item.chipThickness;
                        stockUniicode.chipSize = item.chipSize;
                        //stockUniicode.unpackStatus = Convert.ToInt32(item.isOrder);
                        stockUniicode.unpackStatus = item.isOrder == false ? 0 : 1;
                        stockUniicode.projectNo = item.invoiceNo;
                        stockUniicode.supplierCode = item.customerNo;
                        stockUniicode.UpdateBy = "MES_IN";
                        stockUniicode.UpdateTime = DateTime.Now;
                        await ((DbContext)DC).Set<WmsStockUniicode>().SingleUpdateAsync(stockUniicode);
                    }
                }
                await ((DbContext)DC).BulkSaveChangesAsync();
                if (hasParentTransaction == false)
                {
                    await DC.Database.CommitTransactionAsync();
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
        /// string转时间类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public DateTime StringTransferDatetime(string input)
        {
            DateTime dateTime;
            bool success = DateTime.TryParse(input, out dateTime);
            if (success)
            {
                return dateTime;
            }
            return dateTime;
        }

        /// <summary>
        /// 根据接口更新库存发货单信息,生成任务与指令
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> UpdateStockProjectByInter(List<RequestDto> input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "根据接口更新库存发货单信息,生成任务与指令:";
            var hasParentTransaction = false;
            SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
            if (DC.Database.CurrentTransaction != null)
            {
                hasParentTransaction = true;
            }
            try
            {
                if (input == null || input.Count == 0)
                {
                    msg = $"{desc}入参为空";
                    return result.Error(msg);
                }
                logger.Warn($"----->Warn----->{desc}--入参:{JsonConvert.SerializeObject(input)} ");
                var invoiceNos = input.Select(x => x.invoiceNo).Distinct().ToList();
                if (invoiceNos.Where(x => string.IsNullOrWhiteSpace(x)).Any())
                {
                    msg = $"{desc}返回的数据中有空的工单号，工单号集合{JsonConvert.SerializeObject(invoiceNos)}";
                    return result.Error(msg);
                }
                List<string> invoiceNoLists = new List<string>();
                List<string> waferIDLists = new List<string>();
                input.ForEach(async t =>
                {
                    if (t.waferIDs.Where(x => string.IsNullOrWhiteSpace(x)).Any())
                    {
                        msg = $"{desc}返回的数据中有空的晶圆ID，晶圆ID集合{JsonConvert.SerializeObject(t.waferIDs)}";
                        result.Error(msg);
                        return;
                    }
                    bool sameWaferIDs = t.waferIDs.SelectMany(s => t.waferIDs.Where(s2 => s2 == s), (s, s2) => s).Count() != t.waferIDs.Count;
                    if (sameWaferIDs)
                    {
                        msg = $"{desc}返回的数据中有重复的晶圆ID，晶圆ID集合{JsonConvert.SerializeObject(t.waferIDs)}";
                        result.Error(msg);
                        return;
                    }
                    invoiceNoLists.Add(t.invoiceNo);
                    t.waferIDs.ForEach(s =>
                    {
                        waferIDLists.Add(s);
                    });
                });
                if (result.code == ResCode.Error)
                {
                    return result.Error(msg);
                }

                var stockUniicodes = await DC.Set<WmsStockUniicode>().Where(x => waferIDLists.Contains(x.uniicode)).ToListAsync();
                if (stockUniicodes.Count == 0 || stockUniicodes.Count != waferIDLists.Count)
                {
                    msg = $"{desc}根据返回的数据中的晶圆ID查询不到库存唯一码，或查询到的库存唯一码行数：{stockUniicodes.Count}与晶圆ID行数：{waferIDLists.Count}不一致";
                    return result.Error(msg);
                }
                var stockDtlIds = stockUniicodes.Select(x => x.stockDtlId).Distinct().ToList();
                var stockDtls = await DC.Set<WmsStockDtl>().Where(x => stockDtlIds.Contains(x.ID)).ToListAsync();
                if (stockDtls.Count != stockDtlIds.Count)
                {
                    msg = $"{desc}根据查询到的库存唯一码与库存明细做对比，库存唯一码中库存明细ID的行数：{stockDtlIds.Count}与库存明细行数：{stockDtls.Count}不一致";
                    return result.Error(msg);
                }
                bool isInStockDtl = stockDtls.Where(x => x.stockDtlStatus != 50).ToList().Any();
                if (isInStockDtl)
                {
                    msg = $"{desc}根据查询到的库存明细，存在不是在库的记录";
                    return result.Error(msg);
                }
                var stockCodes = stockUniicodes.Select(x => x.stockCode).Distinct().ToList();
                var wmsStocks = await DC.Set<WmsStock>().Where(x => stockCodes.Contains(x.stockCode)).ToListAsync();
                if (wmsStocks.Count != stockCodes.Count)
                {
                    msg = $"{desc}根据查询到的库存唯一码与库存主表做对比，库存唯一码中库存编码的行数：{stockCodes.Count}与库存主表行数：{wmsStocks.Count}不一致";
                    return result.Error(msg);
                }
                bool isInStock = wmsStocks.Where(x => x.stockStatus != 50).ToList().Any();
                if (isInStock)
                {
                    msg = $"{desc}根据查询到的库存主表，存在不是在库的记录";
                    return result.Error(msg);
                }
                var outInvoiceRecords = await DC.Set<WmsOutInvoiceRecord>().Where(x => waferIDLists.Contains(x.palletBarcode)).ToListAsync();
                if (outInvoiceRecords.Count > 0)
                {
                    msg = $"{desc}根据晶圆ID查询到已存在的出库记录";
                    return result.Error(msg);
                }
                if (hasParentTransaction == false)
                {
                    await DC.Database.BeginTransactionAsync();
                }
                #region 更新工单号
                List<WmsStockUniicode> stockUniicodeList = new List<WmsStockUniicode>();
                foreach (var item in input)
                {
                    foreach (var itemUnii in item.waferIDs)
                    {
                        var stockUniicode = stockUniicodes.Where(x => x.uniicode == itemUnii).FirstOrDefault();
                        if (stockUniicode != null)
                        {
                            var stockDtl = stockDtls.Where(x => x.ID == stockUniicode.stockDtlId).FirstOrDefault();
                            if (stockDtl != null)
                            {
                                stockDtl.projectNo = item.invoiceNo;
                                stockDtl.stockDtlStatus = 70;
                                stockDtl.occupyQty = stockDtl.qty;
                                stockDtl.UpdateBy = "MES_OUT";
                                stockDtl.UpdateTime = DateTime.Now;
                                await ((DbContext)DC).Set<WmsStockDtl>().SingleUpdateAsync(stockDtl);
                            }
                            var wmsStock = wmsStocks.Where(x => x.stockCode == stockUniicode.stockCode).FirstOrDefault();
                            if (wmsStock != null)
                            {
                                wmsStock.stockStatus = 70;
                                wmsStock.UpdateBy = "MES_OUT";
                                wmsStock.UpdateTime = DateTime.Now;
                                await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(wmsStock);
                            }
                            stockUniicode.projectNo = item.invoiceNo;
                            stockUniicode.occupyQty = stockUniicode.qty;
                            stockUniicode.UpdateBy = "MES_OUT";
                            stockUniicode.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).Set<WmsStockUniicode>().SingleUpdateAsync(stockUniicode);
                            stockUniicodeList.Add(stockUniicode);
                        }
                    }
                }
                await ((DbContext)DC).BulkSaveChangesAsync();

                #endregion

                #region 根据工单、库存唯一码创建发货记录
                foreach (var item in stockUniicodeList)
                {
                    WmsStock wmsStock = new WmsStock();
                    wmsStock = wmsStocks.Where(x => x.stockCode == item.stockCode).FirstOrDefault();
                    if (wmsStock == null)
                    {
                        throw new Exception($"{desc}根据唯一码的库存编码【{item.stockCode}】,查不到库存主表");
                    }
                    BasWBin basWBin = await DC.Set<BasWBin>().Where(x => x.binNo == wmsStock.binNo).FirstOrDefaultAsync();
                    if (basWBin == null)
                    {
                        throw new Exception($"{desc}根据唯一码的库存主表的库位号【{wmsStock.binNo}】,查不到库位");
                    }
                    var cfgRelationShip = await DC.Set<CfgRelationship>().Where(x => x.relationshipTypeCode == "Roadway&Station" && x.leftCode == wmsStock.roadwayNo).FirstOrDefaultAsync();
                    if (cfgRelationShip == null)
                    {
                        throw new Exception($"{desc}根据唯一码的库存主表的巷道号【{wmsStock.roadwayNo}】,查不到巷道与站台的对应关系");
                    }
                    //创建发货记录，先判断是否已发货记录
                    WmsOutInvoiceRecord wmsOutInvoiceRecord = new WmsOutInvoiceRecord();
                    wmsOutInvoiceRecord = await DC.Set<WmsOutInvoiceRecord>().Where(x => x.externalOutNo == item.projectNo && x.palletBarcode == item.uniicode).FirstOrDefaultAsync();

                    if (wmsOutInvoiceRecord == null)
                    {
                        //创建发货记录
                        wmsOutInvoiceRecord = BuildWmsOutInvoiceRecordByProject(item, wmsStock, cfgRelationShip.rightCode);
                        wmsOutInvoiceRecord.pickTaskNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.PickNo.GetCode()); // 出库记录单号
                        await ((DbContext)DC).Set<WmsOutInvoiceRecord>().SingleInsertAsync(wmsOutInvoiceRecord);
                        //创建任务
                        WmsTask wmsTask = new WmsTask();
                        wmsTask = BuildWmsTaskByOutInvoiceRecord(wmsOutInvoiceRecord, wmsStock);
                        wmsTask.wmsTaskNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.wmsTaskNo.GetCode());
                        await ((DbContext)DC).Set<WmsTask>().SingleInsertAsync(wmsTask);
                        //创建指令
                        SrmCmd srmCmd = new SrmCmd();
                        srmCmd = BuildSrmCmdByWmsTask(item, wmsTask, basWBin);
                        srmCmd.SubTask_No = await sysSequenceVM.GetSequenceAsync(SequenceCode.srmCmdNo.GetCode());
                        srmCmd.Serial_No = Convert.ToInt16(await sysSequenceVM.GetSequenceAsync(srmCmd.Device_No));
                        srmCmd.Check_Point = (short)(srmCmd.Serial_No + srmCmd.From_ForkDirection + srmCmd.From_Column + srmCmd.From_Layer + srmCmd.To_Station);//计算
                        await ((DbContext)DC).Set<SrmCmd>().SingleInsertAsync(srmCmd);
                    }
                }
                await ((DbContext)DC).BulkSaveChangesAsync();
                #endregion
                if (hasParentTransaction == false)
                {
                    await DC.Database.CommitTransactionAsync();
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
        /// 根据接口更新库存发货单信息,生成任务与指令
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> UpdateStockProjectByChooseIds(List<long> ids, string invoker)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "根据所选库存明细信息,生成任务与指令:";
            var hasParentTransaction = false;
            SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
            if (DC.Database.CurrentTransaction != null)
            {
                hasParentTransaction = true;
            }
            try
            {
                if (ids == null || ids.Count <= 0)
                {
                    msg = $"{desc}入参为空";
                    return result.Error(msg);
                }
                logger.Warn($"----->Warn----->{desc}--入参:{JsonConvert.SerializeObject(ids)} ");
                var wmsStockUniicodes = await DC.Set<WmsStockUniicode>().Where(x => ids.Contains(x.ID)).ToListAsync();
                if (wmsStockUniicodes == null)
                {
                    throw new Exception($"{desc}根据所选库存明细信息【{ids}】,查不到库存主表");
                }
                var stockCodes = wmsStockUniicodes.Select(x => x.stockCode).ToList();
                var stock_Dtl_ids = wmsStockUniicodes.Select(x => x.stockDtlId).ToList();
                var wmsStockDtls = await DC.Set<WmsStockDtl>().Where(x => stock_Dtl_ids.Contains(x.ID)).ToListAsync();

                var wmsStocks = await DC.Set<WmsStock>().Where(x => stockCodes.Contains(x.stockCode)).ToListAsync();
                if (wmsStocks.Count != stockCodes.Count)
                {
                    msg = $"{desc}根据查询到的库存唯一码与库存主表做对比，库存唯一码中库存编码的行数：{stockCodes.Count}与库存主表行数：{wmsStocks.Count}不一致";
                    return result.Error(msg);
                }
                if (stock_Dtl_ids.Count != wmsStockDtls.Count)
                {
                    msg = $"{desc}根据查询到的库存唯一码与库存明细做对比，库存唯一码中库存明细ID的行数：{stock_Dtl_ids.Count}与库存明细行数：{wmsStockDtls.Count}不一致";
                    return result.Error(msg);
                }
                if (wmsStockDtls.Any(x => x.stockDtlStatus != 50))
                {
                    msg = $"{desc}根据查询到的库存明细，存在库存明细状态不符合条件";
                    return result.Error(msg);
                }
                if (hasParentTransaction == false)
                {
                    await DC.Database.BeginTransactionAsync();
                }
                #region 根据工单、库存唯一码创建发货记录
                foreach (var item in wmsStockUniicodes)
                {
                    WmsStock wmsStock = new WmsStock();
                    wmsStock = wmsStocks.Where(x => x.stockCode == item.stockCode).FirstOrDefault();
                    if (wmsStock == null)
                    {
                        throw new Exception($"{desc}根据唯一码的库存编码【{item.stockCode}】,查不到库存主表");
                    }
                    BasWBin basWBin = await DC.Set<BasWBin>().Where(x => x.binNo == wmsStock.binNo).FirstOrDefaultAsync();
                    if (basWBin == null)
                    {
                        throw new Exception($"{desc}根据唯一码的库存主表的库位号【{wmsStock.binNo}】,查不到库位");
                    }
                    var cfgRelationShip = await DC.Set<CfgRelationship>().Where(x => x.relationshipTypeCode == "Roadway&Station" && x.leftCode == wmsStock.roadwayNo).FirstOrDefaultAsync();
                    if (cfgRelationShip == null)
                    {
                        throw new Exception($"{desc}根据唯一码的库存主表的巷道号【{wmsStock.roadwayNo}】,查不到巷道与站台的对应关系");
                    }
                    //创建发货记录，先判断是否已发货记录
                    WmsOutInvoiceRecord wmsOutInvoiceRecord = new WmsOutInvoiceRecord();
                    wmsOutInvoiceRecord = await DC.Set<WmsOutInvoiceRecord>().Where(x => x.externalOutNo == item.projectNo && x.palletBarcode == item.uniicode).FirstOrDefaultAsync();

                    #region 更新工单号

                    var stockDtl = wmsStockDtls.Where(x => x.ID == item.stockDtlId).FirstOrDefault();
                    if (stockDtl != null)
                    {
                        stockDtl.stockDtlStatus = 70;
                        stockDtl.occupyQty = stockDtl.qty;
                        stockDtl.UpdateBy = invoker;
                        stockDtl.UpdateTime = DateTime.Now;
                        await ((DbContext)DC).Set<WmsStockDtl>().SingleUpdateAsync(stockDtl);
                    }
                    if (wmsStock != null)
                    {
                        wmsStock.stockStatus = 70;
                        wmsStock.UpdateBy = invoker;
                        wmsStock.UpdateTime = DateTime.Now;
                        await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(wmsStock);
                    }
                    item.occupyQty = item.qty;
                    item.UpdateBy = invoker;
                    item.UpdateTime = DateTime.Now;
                    await ((DbContext)DC).Set<WmsStockUniicode>().SingleUpdateAsync(item);



                    await ((DbContext)DC).BulkSaveChangesAsync();

                    #endregion
                    if (wmsOutInvoiceRecord == null)
                    {
                        //创建发货记录
                        wmsOutInvoiceRecord = BuildWmsOutInvoiceRecordByProject(item, wmsStock, cfgRelationShip.rightCode);
                        wmsOutInvoiceRecord.pickTaskNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.PickNo.GetCode()); // 出库记录单号
                        wmsOutInvoiceRecord.CreateBy = invoker;
                        await ((DbContext)DC).Set<WmsOutInvoiceRecord>().SingleInsertAsync(wmsOutInvoiceRecord);
                        //创建任务
                        WmsTask wmsTask = new WmsTask();
                        wmsTask = BuildWmsTaskByOutInvoiceRecord(wmsOutInvoiceRecord, wmsStock);
                        wmsTask.wmsTaskNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.wmsTaskNo.GetCode());
                        wmsTask.CreateBy = invoker;
                        await ((DbContext)DC).Set<WmsTask>().SingleInsertAsync(wmsTask);
                        //创建指令
                        SrmCmd srmCmd = new SrmCmd();
                        srmCmd = BuildSrmCmdByWmsTask(item, wmsTask, basWBin);
                        srmCmd.SubTask_No = await sysSequenceVM.GetSequenceAsync(SequenceCode.srmCmdNo.GetCode());
                        srmCmd.Serial_No = Convert.ToInt16(await sysSequenceVM.GetSequenceAsync(srmCmd.Device_No));
                        srmCmd.Check_Point = (short)(srmCmd.Serial_No + srmCmd.From_ForkDirection + srmCmd.From_Column + srmCmd.From_Layer + srmCmd.To_Station);//计算
                        srmCmd.CreateBy = invoker;
                        await ((DbContext)DC).Set<SrmCmd>().SingleInsertAsync(srmCmd);
                    }
                }
                await ((DbContext)DC).BulkSaveChangesAsync();
                #endregion
                if (hasParentTransaction == false)
                {
                    await DC.Database.CommitTransactionAsync();
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
            msg = $"{desc}时间【{DateTime.Now}】,入参【{JsonConvert.SerializeObject(ids)}】";
            logger.Warn($"----->Warn----->{desc}:{msg} ");
            return result.Success(msg);
        }

        /// <summary>
        /// 创建发货记录
        /// </summary>
        /// <param name="uniiCode"></param>
        /// <param name="wmsStock"></param>
        /// <returns></returns>
        public WmsOutInvoiceRecord BuildWmsOutInvoiceRecordByProject(WmsStockUniicode uniiCode, WmsStock wmsStock, string locNo)
        {
            WmsOutInvoiceRecord wmsOutInvoiceRecord = new WmsOutInvoiceRecord();
            wmsOutInvoiceRecord.allocatResult = $"分配完成：晶圆ID【{uniiCode.uniicode}】";
            wmsOutInvoiceRecord.allotQty = uniiCode.qty;
            wmsOutInvoiceRecord.allotType = 0;//MES分配
            wmsOutInvoiceRecord.docTypeCode = "3001";
            wmsOutInvoiceRecord.areaNo = uniiCode.areaNo;
            wmsOutInvoiceRecord.assemblyIdx = "";
            wmsOutInvoiceRecord.batchNo = uniiCode.batchNo;
            wmsOutInvoiceRecord.belongDepartment = "";
            wmsOutInvoiceRecord.binNo = wmsStock.binNo;
            wmsOutInvoiceRecord.deliveryLocNo = locNo;
            wmsOutInvoiceRecord.erpWhouseNo = uniiCode.erpWhouseNo;
            wmsOutInvoiceRecord.extend1 = uniiCode.extend1;
            wmsOutInvoiceRecord.extend2 = uniiCode.extend2;
            wmsOutInvoiceRecord.extend3 = uniiCode.extend3;
            wmsOutInvoiceRecord.extend4 = uniiCode.extend4;
            wmsOutInvoiceRecord.extend5 = uniiCode.extend5;
            wmsOutInvoiceRecord.extend6 = uniiCode.extend6;
            wmsOutInvoiceRecord.extend7 = uniiCode.extend7;
            wmsOutInvoiceRecord.extend8 = uniiCode.extend8;
            wmsOutInvoiceRecord.extend9 = uniiCode.extend9;
            wmsOutInvoiceRecord.extend10 = uniiCode.extend10;
            wmsOutInvoiceRecord.extend11 = uniiCode.extend11;
            wmsOutInvoiceRecord.extend12 = null;
            wmsOutInvoiceRecord.extend13 = null;
            wmsOutInvoiceRecord.extend14 = null;
            wmsOutInvoiceRecord.extend15 = null;
            wmsOutInvoiceRecord.externalOutDtlId = "";
            wmsOutInvoiceRecord.externalOutNo = uniiCode.projectNo;
            wmsOutInvoiceRecord.fpName = "";
            wmsOutInvoiceRecord.fpNo = "";
            wmsOutInvoiceRecord.fpQty = null;
            wmsOutInvoiceRecord.inOutName = "OUT";
            wmsOutInvoiceRecord.inOutTypeNo = "OUT";
            wmsOutInvoiceRecord.inspectionResult = uniiCode.inspectionResult;
            wmsOutInvoiceRecord.invoiceDtlId = null;
            wmsOutInvoiceRecord.invoiceNo = null;
            wmsOutInvoiceRecord.isBatch = 0;
            wmsOutInvoiceRecord.issuedResult = "";
            wmsOutInvoiceRecord.materialName = uniiCode.materialName;
            wmsOutInvoiceRecord.materialCode = uniiCode.materialCode;
            wmsOutInvoiceRecord.materialSpec = uniiCode.materialSpec;
            wmsOutInvoiceRecord.orderDtlId = null;
            wmsOutInvoiceRecord.orderNo = "";
            wmsOutInvoiceRecord.outRecordStatus = 31;
            wmsOutInvoiceRecord.palletBarcode = uniiCode.palletBarcode;
            wmsOutInvoiceRecord.palletPickType = 0;
            wmsOutInvoiceRecord.pickLocNo = locNo;
            wmsOutInvoiceRecord.pickQty = uniiCode.qty;
            wmsOutInvoiceRecord.pickTaskNo = "";//获取序列号
            wmsOutInvoiceRecord.pickType = 0;
            wmsOutInvoiceRecord.preStockDtlId = uniiCode.stockDtlId;
            wmsOutInvoiceRecord.productDeptName = "";
            wmsOutInvoiceRecord.productDeptCode = "";
            wmsOutInvoiceRecord.productLocation = "";
            wmsOutInvoiceRecord.projectNo = uniiCode.projectNo;
            wmsOutInvoiceRecord.proprietorCode = uniiCode.proprietorCode;
            wmsOutInvoiceRecord.regionNo = wmsStock.regionNo;
            wmsOutInvoiceRecord.reversePickFlag = 0;
            wmsOutInvoiceRecord.skuCode = "";
            wmsOutInvoiceRecord.sourceBy = 1;
            wmsOutInvoiceRecord.stockCode = wmsStock.stockCode;
            wmsOutInvoiceRecord.stockDtlId = uniiCode.stockDtlId;
            wmsOutInvoiceRecord.supplierCode = uniiCode.supplierCode;
            wmsOutInvoiceRecord.supplierName = uniiCode.supplierName;
            wmsOutInvoiceRecord.supplierNameAlias = uniiCode.supplierNameAlias;
            wmsOutInvoiceRecord.supplierNameEn = uniiCode.supplierNameEn;
            wmsOutInvoiceRecord.supplyType = "";
            wmsOutInvoiceRecord.ticketNo = uniiCode.projectNo;
            wmsOutInvoiceRecord.ticketPlanBeginTime = null;
            wmsOutInvoiceRecord.ticketType = "";
            wmsOutInvoiceRecord.unitCode = uniiCode.unitCode;
            wmsOutInvoiceRecord.waveNo = "";
            wmsOutInvoiceRecord.whouseNo = uniiCode.whouseNo;
            wmsOutInvoiceRecord.operationMode = "";
            wmsOutInvoiceRecord.outBarCode = uniiCode.uniicode;
            wmsOutInvoiceRecord.urgentFlag = null;
            wmsOutInvoiceRecord.loadedTtype = wmsStock.loadedType;
            wmsOutInvoiceRecord.productSn = uniiCode.uniicode;
            wmsOutInvoiceRecord.lightColor = "";
            wmsOutInvoiceRecord.CreateBy = "MES_OUT";
            wmsOutInvoiceRecord.CreateTime = DateTime.Now;
            wmsOutInvoiceRecord.UpdateBy = "MES_OUT";
            wmsOutInvoiceRecord.UpdateTime = DateTime.Now;
            return wmsOutInvoiceRecord;
        }

        /// <summary>
        /// 根据出库记录生成任务
        /// </summary>
        /// <param name="wmsOutInvoiceRecord"></param>
        /// <returns></returns>
        public WmsTask BuildWmsTaskByOutInvoiceRecord(WmsOutInvoiceRecord wmsOutInvoiceRecord, WmsStock wmsStock)
        {
            WmsTask wmsTaskEntity = new WmsTask();
            wmsTaskEntity.feedbackDesc = String.Empty;
            wmsTaskEntity.feedbackStatus = 0;
            wmsTaskEntity.frLocationNo = wmsStock.binNo;
            wmsTaskEntity.frLocationType = 0;
            wmsTaskEntity.loadedType = wmsOutInvoiceRecord.loadedTtype;
            wmsTaskEntity.matHeight = wmsStock.height;//高
            wmsTaskEntity.matLength = null;//长
            wmsTaskEntity.matQty = null;
            wmsTaskEntity.matWeight = null;//重量
            wmsTaskEntity.matWidth = null;//宽
            wmsTaskEntity.orderNo = wmsOutInvoiceRecord.pickTaskNo;
            wmsTaskEntity.palletBarcode = wmsOutInvoiceRecord.palletBarcode;
            wmsTaskEntity.proprietorCode = wmsOutInvoiceRecord.proprietorCode;
            wmsTaskEntity.regionNo = wmsStock.regionNo;
            wmsTaskEntity.roadwayNo = wmsStock.roadwayNo;
            wmsTaskEntity.stockCode = wmsStock.stockCode;
            wmsTaskEntity.taskDesc = "初始创建";
            wmsTaskEntity.taskPriority = 100;
            wmsTaskEntity.taskStatus = 10;
            wmsTaskEntity.taskTypeNo = "OUT";
            wmsTaskEntity.toLocationNo = wmsOutInvoiceRecord.deliveryLocNo;
            wmsTaskEntity.toLocationType = 0;
            wmsTaskEntity.whouseNo = wmsOutInvoiceRecord.whouseNo;
            wmsTaskEntity.wmsTaskNo = "";//获取序列号
            wmsTaskEntity.wmsTaskType = "OUT";
            wmsTaskEntity.CreateBy = "MES_OUT";
            wmsTaskEntity.CreateTime = DateTime.Now;
            wmsTaskEntity.UpdateBy = "MES_OUT";
            wmsTaskEntity.UpdateTime = DateTime.Now;

            return wmsTaskEntity;
        }
        /// <summary>
        /// 根据WMS任务创建堆垛机指令
        /// </summary>
        /// <param name="wmsTask"></param>
        /// <returns></returns>
        public SrmCmd BuildSrmCmdByWmsTask(WmsStockUniicode uniiCode, WmsTask wmsTask, BasWBin basWBin)
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
            srmCmd.WaferID = uniiCode.uniicode;
            srmCmd.Exec_Status = 3;
            srmCmd.Recive_Date = DateTime.Now;
            srmCmd.CreateBy = "MES_OUT";
            srmCmd.CreateTime = DateTime.Now;
            return srmCmd;
        }
    }
}
