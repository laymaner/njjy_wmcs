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
using Microsoft.EntityFrameworkCore;
using Wish.Areas.BasWhouse.Model;
using Wish.Areas.Config.Model;
using WISH.Helper.Common;
using Wish.ViewModel.BusinessStock.WmsStockAdjustVMs;
using Wish.ViewModel.Common.Dtos;
using Wish.ViewModel.BusinessIn.WmsInReceiptRecordVMs;
using Wish.ViewModel.Common;
using Newtonsoft.Json;
using static Aliyun.OSS.Model.SelectObjectRequestModel.InputFormatModel;

namespace Wish.ViewModel.BusinessPutdown.WmsPutdownVMs
{
    public partial class WmsPutdownVM
    {
        /// <summary>
        /// 下架操作
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> Putdown(PutdownDto input)
        {
            BusinessResult result = new BusinessResult();

            WmsStockAdjustVM wmsStockAdjustApiVM = Wtm.CreateVM<WmsStockAdjustVM>();

            using (var tran = await ((DbContext)DC).Database.BeginTransactionAsync())
            {
                try
                {
                    #region 参数校验

                    if (input == null)
                    {
                        return result.Error($"下架提交信息为空");
                    }
                    if (string.IsNullOrWhiteSpace(input.palletBarcode))
                    {
                        return result.Error($"下架载体条码信息为空");
                    }
                    if (string.IsNullOrWhiteSpace(input.locNo))
                    {
                        return result.Error($"下架站台编号信息为空");
                    }

                    // 下架单
                    WmsPutdown putdown = await ((DbContext)DC).Set<WmsPutdown>().Where(x => x.palletBarcode == input.palletBarcode && x.putdownStatus < 90).FirstOrDefaultAsync();
                    if (putdown == null)
                    {
                        return result.Error($"托盘编号【{input.palletBarcode}】找不到对应的下架单信息");
                    }
                    if (string.IsNullOrWhiteSpace(putdown.docTypeCode))
                    {
                        return result.Error($"下架单单据类型为空");
                    }
                    // 下架单明细信息
                    List<WmsPutdownDtl> putdownDtls = await ((DbContext)DC).Set<WmsPutdownDtl>().Where(x => x.putdownNo == putdown.putdownNo && x.stockCode == putdown.stockCode).ToListAsync();
                    if (putdownDtls.Count <= 0)
                    {
                        return result.Error($"下架单号【{putdown.putdownNo}】找不到对应的下架单明细信息");
                    }

                    // 库存信息
                    WmsStock stock = await ((DbContext)DC).Set<WmsStock>().Where(x => x.stockCode == putdown.stockCode).FirstOrDefaultAsync();
                    if (stock == null)
                    {
                        return result.Error($"库存编码【{putdown.stockCode}】未找到对应的库存信息");
                    }
                    // 库存明细信息
                    List<WmsStockDtl> stockDtls = await ((DbContext)DC).Set<WmsStockDtl>().Where(x => x.stockCode == stock.stockCode).ToListAsync();
                    if (stockDtls.Count <= 0)
                    {
                        return result.Error($"库存编码【{putdown.stockCode}】未找到对应的库存明细信息");
                    }
                    // 库存唯一码信息
                    var stockDtlIds = stockDtls.Select(x => x.ID).ToList();
                    List<WmsStockUniicode> stockUniicodes = new List<WmsStockUniicode>();
                    if (stock.loadedType == 1)
                    {
                        stockUniicodes = await ((DbContext)DC).Set<WmsStockUniicode>().Where(x => x.stockCode == stock.stockCode && stockDtlIds.Contains(x.stockDtlId)).ToListAsync();
                        if (stockUniicodes.Count <= 0)
                        {
                            return result.Error($"托盘【{stock.palletBarcode}】,库存编码【{stock.stockCode}】未找到对应的库存唯一码信息");
                        }
                    }


                    // 单据类型
                    CfgDocType docType = await ((DbContext)DC).Set<CfgDocType>().Where(x => x.docTypeCode == putdown.docTypeCode).FirstOrDefaultAsync();
                    if (docType == null)
                    {
                        return result.Error($"单据类型【{putdown.docTypeCode}】信息未配置");
                    }

                    BasWRegion region = await ((DbContext)DC).Set<BasWRegion>().Where(x => x.regionTypeCode == "WP").FirstOrDefaultAsync();
                    if (region == null)
                    {
                        return result.Error($"拣选库区信息未配置");
                    }

                    BasWBin bin = await ((DbContext)DC).Set<BasWBin>().Where(x => x.regionNo == region.regionNo).FirstOrDefaultAsync();
                    if (bin == null)
                    {
                        return result.Error($"拣选库位信息未配置");
                    }
                    string locNo = string.Empty;
                    BasWLoc loc = await ((DbContext)DC).Set<BasWLoc>().Where(x => x.locNo == input.locNo).FirstOrDefaultAsync();
                    if (loc == null)
                    {
                        var locGroupInfo = await ((DbContext)DC).Set<BasWLocGroup>().Where(x => x.locGroupNo == input.locNo).FirstOrDefaultAsync();
                        if (locGroupInfo != null)
                        {
                            locNo = locGroupInfo.locGroupNo;
                        }
                    }
                    else
                    {
                        locNo = loc.locNo;
                    }

                    #endregion


                    #region 逻辑处理

                    #region 下架单

                    putdown.putdownStatus = 90;
                    putdown.UpdateBy = input.invoker;
                    putdown.UpdateTime = DateTime.Now;
                    await ((DbContext)DC).Set<WmsPutdown>().SingleUpdateAsync(putdown);

                    foreach (var putdownDtl in putdownDtls)
                    {
                        putdownDtl.putdownDtlStatus = 90;
                        putdownDtl.UpdateBy = input.invoker;
                        putdownDtl.UpdateTime = DateTime.Now;
                    }
                    await ((DbContext)DC).BulkUpdateAsync(putdownDtls);
                    #endregion

                    #region 库存

                    stock.stockStatus = 70;
                    stock.regionNo = region.regionNo;
                    stock.binNo = bin.binNo;
                    stock.roadwayNo = "00";
                    stock.locNo = input.locNo;
                    stock.UpdateBy = input.invoker;
                    stock.UpdateTime = DateTime.Now;
                    await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(stock);

                    foreach (var stockDtl in stockDtls)
                    {
                        stockDtl.stockDtlStatus = 70;
                        stockDtl.UpdateBy = input.invoker;
                        stockDtl.UpdateTime = DateTime.Now;
                    }
                    await ((DbContext)DC).BulkUpdateAsync(stockDtls);

                    WmsStockAdjust stockAdjust = new WmsStockAdjust();
                    stockAdjust.whouseNo = putdown.whouseNo;
                    stockAdjust.proprietorCode = putdown.proprietorCode;
                    stockAdjust.stockCode = putdown.stockCode;
                    stockAdjust.palletBarcode = putdown.palletBarcode;
                    stockAdjust.packageBarcode = "";
                    stockAdjust.adjustOperate = "下架";
                    stockAdjust.adjustType = "UDP";
                    stockAdjust.adjustDesc = $"下架操作：下架单ID【{putdown.ID}】；更新库存库区为【{region.regionNo}】；更新库存库位为【{bin.binNo}】";
                    stockAdjust.CreateBy = input.invoker;
                    stockAdjust.CreateTime = DateTime.Now;
                    await ((DbContext)DC).Set<WmsStockAdjust>().AddAsync(stockAdjust);

                    #endregion

                    #region 业务类型

                    // 出库
                    if ("OUT".Equals(docType.businessCode))
                    {
                        List<WmsOutInvoiceRecord> outInvoiceRecords = await ((DbContext)DC).Set<WmsOutInvoiceRecord>().Where(x => x.stockCode == putdown.stockCode && x.deliveryLocNo == locNo && x.outRecordStatus < 41).ToListAsync();
                        if (outInvoiceRecords.Count <= 0)
                        {
                            return result.Error($"库存编码【{putdown.stockCode}】未找到对应的出库记录");
                        }

                        foreach (var outRecord in outInvoiceRecords)
                        {
                            outRecord.outRecordStatus = 40;
                            outRecord.deliveryLocNo = input.locNo;
                            outRecord.pickLocNo = input.locNo;
                            outRecord.UpdateBy = input.invoker;
                            outRecord.UpdateTime = DateTime.Now;
                        }
                        await ((DbContext)DC).BulkUpdateAsync(outInvoiceRecords);
                    }

                    // 空托出库
                    else if ("EMPTY_OUT".Equals(docType.businessCode))
                    {
                        //List<WmsOutInvoiceRecord> outInvoiceRecords =await ((DbContext)DC).Set<WmsOutInvoiceRecord>().Where(x => x.stockCode == putdown.stockCode && x.deliveryLocNo == locNo && x.outRecordStatus < 41).ToListAsync();
                        List<WmsOutInvoiceRecord> outInvoiceRecords = await ((DbContext)DC).Set<WmsOutInvoiceRecord>().Where(x => x.stockCode == putdown.stockCode && x.deliveryLocNo == input.locNo && x.outRecordStatus < 41).ToListAsync();
                        if (outInvoiceRecords.Count <= 0)
                        {
                            return result.Error($"库存编码【{putdown.stockCode}】未找到对应的出库记录");
                        }

                        foreach (var outRecord in outInvoiceRecords)
                        {
                            outRecord.deliveryLocNo = input.locNo;
                            outRecord.outRecordStatus = 90;
                            outRecord.UpdateBy = input.invoker;
                            outRecord.UpdateTime = DateTime.Now;
                        }
                        await ((DbContext)DC).BulkUpdateAsync(outInvoiceRecords);

                        //foreach (var stockUniicode in stockUniicodes)
                        //{
                        //    var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStockUniicode, WmsStockUniicodeHis>());
                        //    var mapper = config.CreateMapper();
                        //    WmsStockUniicodeHis uniicodeHis = mapper.Map<WmsStockUniicodeHis>(stockUniicode);
                        //    wmsStockAdjustApiVM.AddAdjustOperate(stockDtls[0], stockUniicode.uniicode, "下架", "DEL", $"空托出库完成，删除库存", input.invoker);
                        //    ((DbContext)DC).Set<WmsStockUniicodeHis>().Add(uniicodeHis);
                        //    ((DbContext)DC).Set<WmsStockUniicode>().Remove(stockUniicode);
                        //    ((DbContext)DC).SaveChanges();
                        //}

                        foreach (var stockdtl in stockDtls)
                        {
                            var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStockDtl, WmsStockDtlHis>());
                            var mapper = config.CreateMapper();
                            WmsStockDtlHis wmsStockDtlHis = mapper.Map<WmsStockDtlHis>(stockdtl);
                            await ((DbContext)DC).Set<WmsStockDtlHis>().AddAsync(wmsStockDtlHis);
                            await ((DbContext)DC).Set<WmsStockDtl>().SingleDeleteAsync(stockdtl);
                            await ((DbContext)DC).BulkSaveChangesAsync();
                        }

                        if (stock != null)
                        {
                            var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStock, WmsStockHis>());
                            var mapper = config.CreateMapper();
                            WmsStockHis wmsStockDtlHis = mapper.Map<WmsStockHis>(stock);
                            await ((DbContext)DC).Set<WmsStockHis>().AddAsync(wmsStockDtlHis);
                            await ((DbContext)DC).Set<WmsStock>().SingleDeleteAsync(stock);
                            await ((DbContext)DC).BulkSaveChangesAsync();
                        }

                    }

                    // 移库
                    else if ("MOVE".Equals(docType.businessCode))
                    {

                        WmsItnMove moveInfo = await ((DbContext)DC).Set<WmsItnMove>().Where(x => x.moveNo == putdown.orderNo).FirstOrDefaultAsync();
                        if (moveInfo == null)
                        {
                            return result.Error($"单号【{putdown.orderNo}】未找到对应的移库单据信息");
                        }

                        List<WmsItnMoveDtl> moveDtls = await ((DbContext)DC).Set<WmsItnMoveDtl>().Where(x => x.moveNo == moveInfo.moveNo).ToListAsync();
                        if (moveDtls.Count <= 0)
                        {
                            return result.Error($"库存编码【{putdown.orderNo}】未找到对应的移库明细信息");
                        }

                        //foreach ( var moveRecord in moveRecords )
                        //{
                        //    moveRecord.moveRecordStatus = 20;
                        //    moveRecord.curLocationNo = input.locNo;
                        //    moveRecord.updateBy = input.invoker;
                        //    moveRecord.updateTime = DateTime.Now;
                        //}
                        var moveDtl = moveDtls.FirstOrDefault(t => t.palletBarcode == input.palletBarcode);
                        if (moveDtl != null)
                        {
                            moveDtl.moveDtlStatus = 90;
                            moveDtl.updateBy = input.invoker;
                            moveDtl.updateTime = DateTime.Now;
                        }
                        if (moveDtls.FirstOrDefault(t => t.moveDtlStatus < 90) == null)
                        {
                            moveInfo.moveStatus = 90;
                            moveInfo.UpdateBy = input.invoker;
                            moveInfo.UpdateTime = DateTime.Now;
                        }
                        await ((DbContext)DC).Set<WmsItnMoveDtl>().BulkUpdateAsync(moveDtls);
                        await ((DbContext)DC).Set<WmsItnMove>().SingleUpdateAsync(moveInfo);
                        stock.stockStatus = 0;
                        stock.regionNo = "MOVE01";
                        stock.binNo = "MOVE01_010101";
                        stock.roadwayNo = "00";
                        await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(stock);

                        foreach (var stockDtl in stockDtls)
                        {
                            stockDtl.stockDtlStatus = 0;
                            stockDtl.occupyQty = 0;
                            stockDtl.UpdateBy = input.invoker;
                            stockDtl.UpdateTime = DateTime.Now;
                        }
                        await ((DbContext)DC).BulkUpdateAsync(stockDtls);

                    }

                    // 盘点
                    else if ("INVENTORY".Equals(docType.businessCode))
                    {
                        var inventoryRecordList = await ((DbContext)DC).Set<WmsItnInventoryRecord>().Where(x => x.stockCode == putdown.stockCode && x.inventoryRecordStatus < 90).ToListAsync();
                        if (inventoryRecordList.Count == 0)
                        {
                            return result.Error($"库存编码【{putdown.stockCode}】未找到对应的盘点记录信息");
                        }
                        // TODO:修改状态
                        //inventoryRecord.inventoryRecordStatus = 0;
                        inventoryRecordList.ForEach(t =>
                        {
                            t.putdownLocNo = input.locNo;
                            t.inventoryRecordStatus = 41;
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });

                        await ((DbContext)DC).Set<WmsItnInventoryRecord>().BulkUpdateAsync(inventoryRecordList);
                    }

                    // 抽检
                    else if ("QC".Equals(docType.businessCode))
                    {
                        var itnQcRecordList = await ((DbContext)DC).Set<WmsItnQcRecord>().Where(x => x.stockCode == putdown.stockCode && x.itnQcStatus < 90).ToListAsync();
                        if (itnQcRecordList.Count == 0)
                        {
                            return result.Error($"库存编码【{putdown.stockCode}】未找到对应的抽检记录信息");
                        }
                        // TODO:修改状态
                        //itnQcRecord.itnQcStatus = 0;
                        itnQcRecordList.ForEach(t =>
                        {
                            t.itnQcLocNo = input.locNo;
                            t.itnQcStatus = 39;
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        await ((DbContext)DC).Set<WmsItnQcRecord>().BulkUpdateAsync(itnQcRecordList);
                    }

                    #endregion

                    #endregion

                    await tran.CommitAsync();

                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();

                    result = result.Error(e.Message);
                }
            }
            return result;
        }

        /// <summary>
        /// 指令完成下架操作
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> PutdownByWCS(PutdownDto input)
        {
            BusinessResult result = new BusinessResult();

            WmsStockAdjustVM wmsStockAdjustApiVM = Wtm.CreateVM<WmsStockAdjustVM>();
            var hasParentTransaction = false;

            var inputJson = JsonConvert.SerializeObject(input);

            if (DC.Database.CurrentTransaction != null)
            {
                hasParentTransaction = true;
            }
            try
            {
                #region 参数校验

                if (input == null)
                {
                    return result.Error($"下架提交信息为空");
                }
                if (string.IsNullOrWhiteSpace(input.palletBarcode))
                {
                    return result.Error($"下架载体条码信息为空");
                }
                if (string.IsNullOrWhiteSpace(input.locNo))
                {
                    return result.Error($"下架站台编号信息为空");
                }
                WmsOutInvoiceRecord outInvoiceRecord = await ((DbContext)DC).Set<WmsOutInvoiceRecord>().Where(x => x.palletBarcode == input.palletBarcode && x.deliveryLocNo == input.locNo && x.outRecordStatus < 41).FirstOrDefaultAsync();
                if (outInvoiceRecord == null)
                {
                    return result.Error($"托盘编号【{input.palletBarcode}】找不到对应的出库记录单信息");
                }
                if (string.IsNullOrWhiteSpace(outInvoiceRecord.docTypeCode))
                {
                    return result.Error($"下架单单据类型为空");
                }

                // 库存信息
                WmsStock stock = await ((DbContext)DC).Set<WmsStock>().Where(x => x.stockCode == outInvoiceRecord.stockCode).FirstOrDefaultAsync();
                if (stock == null)
                {
                    return result.Error($"库存编码【{outInvoiceRecord.stockCode}】未找到对应的库存信息");
                }
                // 库存明细信息
                WmsStockDtl stockDtl = await ((DbContext)DC).Set<WmsStockDtl>().Where(x => x.stockCode == stock.stockCode).FirstOrDefaultAsync();
                if (stockDtl == null)
                {
                    return result.Error($"库存编码【{stock.stockCode}】未找到对应的库存明细信息");
                }
                //库存唯一码信息
                WmsStockUniicode stockUniicode = await ((DbContext)DC).Set<WmsStockUniicode>().Where(x => x.stockCode == stock.stockCode && x.stockDtlId == stockDtl.ID).FirstOrDefaultAsync();
                if (stockUniicode == null)
                {
                    return result.Error($"托盘【{stock.palletBarcode}】,库存编码【{stock.stockCode}】未找到对应的库存唯一码信息");
                }



                // 单据类型
                CfgDocType docType = await ((DbContext)DC).Set<CfgDocType>().Where(x => x.docTypeCode == outInvoiceRecord.docTypeCode).FirstOrDefaultAsync();
                if (docType == null)
                {
                    return result.Error($"单据类型【{outInvoiceRecord.docTypeCode}】信息未配置");
                }

                //string locNo = string.Empty;
                //BasWLoc loc = await ((DbContext)DC).Set<BasWLoc>().Where(x => x.locNo == input.locNo).FirstOrDefaultAsync();
                //if (loc == null)
                //{
                //    var locGroupInfo = await ((DbContext)DC).Set<BasWLocGroup>().Where(x => x.locGroupNo == input.locNo).FirstOrDefaultAsync();
                //    if (locGroupInfo != null)
                //    {
                //        locNo = locGroupInfo.locGroupNo;
                //    }
                //}
                //else
                //{
                //    locNo = loc.locNo;
                //}

                #endregion

                if (hasParentTransaction == false)
                {
                    await DC.Database.BeginTransactionAsync();
                }
                #region 逻辑处理

                #region 库存
                //库存主表更新状态
                stock.stockStatus = 90;
                stock.UpdateBy = input.invoker;
                stock.UpdateTime = DateTime.Now;
                await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(stock);

                //库存明细表更新状态
                stockDtl.stockDtlStatus = 90;
                stockDtl.UpdateBy = input.invoker;
                stockDtl.UpdateTime = DateTime.Now;
                await ((DbContext)DC).Set<WmsStockDtl>().SingleUpdateAsync(stockDtl);
                await ((DbContext)DC).BulkSaveChangesAsync();

                //库存主表转历史
                WmsStockHis stockHis = CommonHelper.Map<WmsStock, WmsStockHis>(stock, "ID");
                await ((DbContext)DC).Set<WmsStockHis>().SingleInsertAsync(stockHis);
                await ((DbContext)DC).Set<WmsStock>().SingleDeleteAsync(stock);

                //库存明细表转历史
                WmsStockDtlHis stockDtlHis = CommonHelper.Map<WmsStockDtl, WmsStockDtlHis>(stockDtl, "ID");
                await ((DbContext)DC).Set<WmsStockDtlHis>().SingleInsertAsync(stockDtlHis);
                await ((DbContext)DC).Set<WmsStockDtl>().SingleDeleteAsync(stockDtl);
                await ((DbContext)DC).BulkSaveChangesAsync();

                //库存唯一码转历史
                WmsStockUniicodeHis stockUniicodeHis = CommonHelper.Map<WmsStockUniicode, WmsStockUniicodeHis>(stockUniicode, "ID");
                stockUniicodeHis.extend1 = stockDtlHis.ID.ToString();
                stockUniicodeHis.UpdateBy = input.invoker;
                stockUniicodeHis.UpdateTime = DateTime.Now;

                await ((DbContext)DC).Set<WmsStockUniicodeHis>().SingleInsertAsync(stockUniicodeHis);
                await ((DbContext)DC).Set<WmsStockUniicode>().SingleDeleteAsync(stockUniicode);
                await ((DbContext)DC).BulkSaveChangesAsync();


                WmsStockAdjust stockAdjust = new WmsStockAdjust();
                stockAdjust.whouseNo = outInvoiceRecord.whouseNo;
                stockAdjust.proprietorCode = outInvoiceRecord.proprietorCode;
                stockAdjust.stockCode = stock.stockCode;
                stockAdjust.palletBarcode = stock.palletBarcode;
                stockAdjust.packageBarcode = "";
                stockAdjust.adjustOperate = "下架";
                stockAdjust.adjustType = "UDP";
                stockAdjust.adjustDesc = $"下架操作：发货单记录ID【{outInvoiceRecord.ID}】；【{stock.stockCode}】库存主表，明细表，唯一码表转历史";
                stockAdjust.CreateBy = input.invoker;
                stockAdjust.CreateTime = DateTime.Now;
                await ((DbContext)DC).Set<WmsStockAdjust>().AddAsync(stockAdjust);

                #endregion

                #region 业务类型

                // 出库
                if ("OUT".Equals(docType.businessCode))
                {
                    outInvoiceRecord.outRecordStatus = 90;
                    outInvoiceRecord.UpdateBy = input.invoker;
                    outInvoiceRecord.UpdateTime = DateTime.Now;
                    await ((DbContext)DC).Set<WmsOutInvoiceRecord>().SingleUpdateAsync(outInvoiceRecord);

                    WmsOutInvoiceRecordHis outInvoiceRecordHis = CommonHelper.Map<WmsOutInvoiceRecord, WmsOutInvoiceRecordHis>(outInvoiceRecord, "ID");
                    await ((DbContext)DC).Set<WmsOutInvoiceRecordHis>().SingleInsertAsync(outInvoiceRecordHis);
                    await ((DbContext)DC).Set<WmsOutInvoiceRecord>().SingleDeleteAsync(outInvoiceRecord);
                    await ((DbContext)DC).BulkSaveChangesAsync();
                }
                #endregion

                #endregion

                if (hasParentTransaction == false)
                {
                    await DC.Database.CommitTransactionAsync();
                }

            }
            catch (Exception e)
            {
                if (hasParentTransaction == false)
                {
                    await DC.Database.RollbackTransactionAsync();
                }

                result = result.Error(e.Message);
            }
            string outJson = JsonConvert.SerializeObject(result);
            logger.Warn($"---->下架操作，操作人：{input.invoker},托盘号：【{input.palletBarcode}】,入参：【{inputJson}】,出参：【{outJson}】");
            return result;
        }

        /// <summary>
        /// 处理下架单、下架单明细历史数据
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void DealPutdownHis()
        {
            var hasParentTransaction = false;
            try
            {
                if (((DbContext)DC).Database.CurrentTransaction != null)
                {
                    hasParentTransaction = true;
                }

                if (!hasParentTransaction)
                {
                    ((DbContext)DC).Database.BeginTransaction();
                }

                DateTime date = DateTime.Now.AddMonths(-3);
                var wmsPutdowns = DC.Set<WmsPutdown>().Where(x => x.putdownStatus >= 90 && x.UpdateTime < date).ToList();
                if (wmsPutdowns.Any())
                {
                    List<WmsPutdownHis> putdownHisList = new List<WmsPutdownHis>();
                    foreach (var item in wmsPutdowns)
                    {
                        var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsPutdown, WmsPutdownHis>());
                        var mapper = config.CreateMapper();
                        WmsPutdownHis wmsPutdownHis = mapper.Map<WmsPutdownHis>(item);
                        putdownHisList.Add(wmsPutdownHis);
                    }
                    ((DbContext)DC).BulkInsert(putdownHisList);
                    ((DbContext)DC).BulkDelete(wmsPutdowns);
                    ((DbContext)DC).BulkSaveChanges();

                    var putdownNos = wmsPutdowns.Select(x => x.putdownNo).ToList();
                    var wmsPutdownDtls = DC.Set<WmsPutdownDtl>().Where(x => putdownNos.Contains(x.putdownNo) && x.putdownDtlStatus >= 90).ToList();
                    if (wmsPutdownDtls.Any())
                    {
                        List<WmsPutdownDtlHis> putdownDtlHisList = new List<WmsPutdownDtlHis>();
                        foreach (var item in wmsPutdownDtls)
                        {
                            var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsPutdownDtl, WmsPutdownDtlHis>());
                            var mapper = config.CreateMapper();
                            WmsPutdownDtlHis wmsPutdownDtlHis = mapper.Map<WmsPutdownDtlHis>(item);
                            putdownDtlHisList.Add(wmsPutdownDtlHis);
                        }
                        ((DbContext)DC).BulkInsert(putdownDtlHisList);
                        ((DbContext)DC).BulkDelete(wmsPutdownDtls);
                        ((DbContext)DC).BulkSaveChanges();
                    }
                }

                if (!hasParentTransaction)
                {
                    ((DbContext)DC).Database.CommitTransaction();
                }
            }
            catch (Exception e)
            {
                if (!hasParentTransaction)
                {
                    ((DbContext)DC).Database.RollbackTransaction();
                }
                throw new Exception(e.Message);
            }
        }
    }
}
