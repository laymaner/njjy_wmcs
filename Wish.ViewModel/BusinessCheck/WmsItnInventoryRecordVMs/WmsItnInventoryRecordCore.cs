using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using WISH.Helper.Common;
using Wish.ViewModel.System.SysSequenceVMs;
using Newtonsoft.Json;
using Wish.ViewModel.Common.Dtos;
using Microsoft.EntityFrameworkCore;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using Wish.Areas.BasWhouse.Model;
using Wish.Areas.Config.Model;
using Wish.TaskConfig.Model;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Wish.ViewModel.BasWhouse.BasWBinVMs;
using Wish.ViewModel.Common;
using Wish.ViewModel.BusinessStock.WmsStockVMs;
using Wish.ViewModel.Config.CfgRelationshipVMs;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryRecordVMs
{
    public partial class WmsItnInventoryRecordVM
    {
        /// <summary>
        /// 创建盘点单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> CreateInventoryTask(CreateInventoryTaskDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "创建盘点任务:";
            var hasParentTransaction = false;
            SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
            if (DC.Database.CurrentTransaction != null)
            {
                hasParentTransaction = true;
            }
            try
            {
                if (input == null)
                {
                    msg = $"{desc}入参为空";
                    return result.Error(msg);
                }
                logger.Warn($"----->Warn----->{desc}--入参:{JsonConvert.SerializeObject(input)} ");
                if (input.uniiCodes.Count==0 || input.uniiCodes==null)
                {
                    msg = $"{desc}入参的唯一码为空";
                    return result.Error(msg);
                }
                var stockUniicodes = await DC.Set<WmsStockUniicode>().Where(x => input.uniiCodes.Contains(x.uniicode)).ToListAsync();
                if (stockUniicodes.Count == 0 || stockUniicodes.Count != input.uniiCodes.Count)
                {
                    msg = $"{desc}根据返回的数据中的晶圆ID查询不到库存唯一码，或查询到的库存唯一码行数：{stockUniicodes.Count}与晶圆ID行数：{input.uniiCodes.Count}不一致";
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
                var outInvoiceRecords = await DC.Set<WmsOutInvoiceRecord>().Where(x => input.uniiCodes.Contains(x.palletBarcode)).ToListAsync();
                if (outInvoiceRecords.Count > 0)
                {
                    msg = $"{desc}根据晶圆ID查询到已存在的出库记录";
                    return result.Error(msg);
                }
                var itnInventoryRecords = await DC.Set<WmsItnInventoryRecord>().Where(x => input.uniiCodes.Contains(x.palletBarcode)).ToListAsync();
                if (itnInventoryRecords.Any())
                {
                    msg = $"{desc}根据晶圆ID查询到已存在的盘点记录";
                    return result.Error(msg);
                }
                if (hasParentTransaction == false)
                {
                    await DC.Database.BeginTransactionAsync();
                }
                foreach (var stockUniicode in stockUniicodes)
                {
                    if (stockUniicode != null)
                    {
                        var stockDtl = stockDtls.Where(x => x.ID == stockUniicode.stockDtlId).FirstOrDefault();
                        if (stockDtl != null)
                        {
                            stockDtl.stockDtlStatus = 70;
                            stockDtl.occupyQty = stockDtl.qty;
                            stockDtl.UpdateBy = input.invoker;
                            stockDtl.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).Set<WmsStockDtl>().SingleUpdateAsync(stockDtl);
                        }
                        var wmsStock = wmsStocks.Where(x => x.stockCode == stockUniicode.stockCode).FirstOrDefault();
                        if (wmsStock != null)
                        {
                            wmsStock.stockStatus = 70;
                            wmsStock.UpdateBy = input.invoker;
                            wmsStock.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(wmsStock);
                        }
                        stockUniicode.occupyQty = stockUniicode.qty;
                        stockUniicode.UpdateBy = input.invoker;
                        stockUniicode.UpdateTime = DateTime.Now;
                        await ((DbContext)DC).Set<WmsStockUniicode>().SingleUpdateAsync(stockUniicode);
                    }
                }
                await ((DbContext)DC).BulkSaveChangesAsync();
                #region 根据库存唯一码创建盘点记录
                foreach (var item in stockUniicodes)
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
                        throw new Exception($"{desc}根据唯一码的库存主表【{item.stockCode}】的库位号【{wmsStock.binNo}】,查不到库位");
                    }
                    var cfgRelationShip = await DC.Set<CfgRelationship>().Where(x => x.relationshipTypeCode == "Roadway&Station" && x.leftCode == wmsStock.roadwayNo).FirstOrDefaultAsync();
                    if (cfgRelationShip == null)
                    {
                        throw new Exception($"{desc}根据唯一码的库存主表【{item.stockCode}】的巷道号【{wmsStock.roadwayNo}】,查不到巷道与站台的对应关系");
                    }
                    //创建盘点记录，先判断是否已盘点记录
                    WmsItnInventoryRecord wmsItnInventoryRecord = new WmsItnInventoryRecord();
                    wmsItnInventoryRecord = await DC.Set<WmsItnInventoryRecord>().Where(x =>x.palletBarcode == item.uniicode).FirstOrDefaultAsync();

                    if (wmsItnInventoryRecord == null)
                    {
                        //创建盘点记录
                        wmsItnInventoryRecord = BuildWmsItnInventoryRecordByUniicode(item, wmsStock, input,cfgRelationShip.rightCode);
                        wmsItnInventoryRecord.inventoryNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.inventoryNo.GetCode()); // 盘点记录单号
                        await ((DbContext)DC).Set<WmsItnInventoryRecord>().SingleInsertAsync(wmsItnInventoryRecord);
                        //创建任务
                        WmsTask wmsTask = new WmsTask();
                        wmsTask = BuildWmsTaskByItnInventoryRecord(wmsItnInventoryRecord, wmsStock);
                        wmsTask.wmsTaskNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.wmsTaskNo.GetCode());
                        await ((DbContext)DC).Set<WmsTask>().SingleInsertAsync(wmsTask);
                        //创建指令
                        SrmCmd srmCmd = new SrmCmd();
                        srmCmd = BuildSrmCmdByWmsTask(wmsTask, basWBin);
                        srmCmd.SubTask_No = await sysSequenceVM.GetSequenceAsync(SequenceCode.srmCmdNo.GetCode());
                        srmCmd.Serial_No = Convert.ToInt16(await sysSequenceVM.GetSequenceAsync(srmCmd.Device_No));
                        srmCmd.Check_Point = (short)(srmCmd.Serial_No + srmCmd.To_ForkDirection + srmCmd.To_Column + srmCmd.To_Layer + srmCmd.From_Station);//计算
                        await ((DbContext)DC).Set<SrmCmd>().SingleInsertAsync(srmCmd);
                    }
                }
                await ((DbContext)DC).BulkSaveChangesAsync();
                #endregion

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
        /// 盘点确认回库
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> ConfirmInventoryBack(ConfirmInventoryTaskDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "盘点确认回库:";
            var hasParentTransaction = false;
            SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
            BasWBinVM basWBinVM = Wtm.CreateVM<BasWBinVM>();
            if (DC.Database.CurrentTransaction != null)
            {
                hasParentTransaction = true;
            }
            try
            {
                var wmsItnInventoryRecords = await DC.Set<WmsItnInventoryRecord>().Where(x => x.ID == input.ID && x.inventoryRecordStatus < 90).ToListAsync();
                if (wmsItnInventoryRecords.Count == 0)
                {
                    msg = $"{desc}根据ID【{input.ID}】查询不到盘点记录";
                    return result.Error(msg);
                }
                if (wmsItnInventoryRecords.Count > 1)
                {
                    msg = $"{desc}根据ID【{input.ID}】查询到多条盘点记录";
                    return result.Error(msg);
                }
                WmsItnInventoryRecord wmsItnInventoryRecord = new WmsItnInventoryRecord();
                wmsItnInventoryRecord = wmsItnInventoryRecords.FirstOrDefault();
                if (wmsItnInventoryRecord == null)
                {
                    msg = $"{desc}根据ID【{input.ID}】查询不到盘点记录";
                    return result.Error(msg);
                }
                decimal difQty = (decimal)(input.confirmQty ?? 0 - wmsItnInventoryRecord.inventoryQty ?? 0);
                if (difQty == 0)
                {
                    if (input.differenceFlag != 0)
                    {
                        msg = $"{desc}根据ID【{input.ID}】查询到盘点记录，盘点确认数量为【{input.confirmQty}】,盘点数量为【{wmsItnInventoryRecord.inventoryQty}】,差值为【{difQty}】，与差异标识不符，差异标识【{input.differenceFlag}】";
                        return result.Error(msg);
                    }
                }
                else if (difQty > 0)
                {
                    if (input.differenceFlag != 1)
                    {
                        msg = $"{desc}根据ID【{input.ID}】查询到盘点记录，盘点确认数量为【{input.confirmQty}】,盘点数量为【{wmsItnInventoryRecord.inventoryQty}】,差值为【{difQty}】，与差异标识不符，差异标识【{input.differenceFlag}】";
                        return result.Error(msg);
                    }
                }
                else
                {
                    if (input.differenceFlag != 2)
                    {
                        msg = $"{desc}根据ID【{input.ID}】查询到盘点记录，盘点确认数量为【{input.confirmQty}】,盘点数量为【{wmsItnInventoryRecord.inventoryQty}】,差值为【{difQty}】，与差异标识不符，差异标识【{input.differenceFlag}】";
                        return result.Error(msg);
                    }
                }
                // 库存信息
                WmsStock stock = await ((DbContext)DC).Set<WmsStock>().Where(x => x.stockCode == wmsItnInventoryRecord.stockCode && x.palletBarcode == wmsItnInventoryRecord.palletBarcode).FirstOrDefaultAsync();
                if (stock == null)
                {
                    return result.Error($"{desc}库存编码【{wmsItnInventoryRecord.stockCode}】未找到对应的库存信息");
                }
                // 库存明细信息
                WmsStockDtl stockDtl = await ((DbContext)DC).Set<WmsStockDtl>().Where(x => x.stockCode == stock.stockCode).FirstOrDefaultAsync();
                if (stockDtl == null)
                {
                    return result.Error($"{desc}库存编码【{stock.stockCode}】未找到对应的库存明细信息");
                }
                //库存唯一码信息
                WmsStockUniicode stockUniicode = await ((DbContext)DC).Set<WmsStockUniicode>().Where(x => x.stockCode == stock.stockCode && x.stockDtlId == stockDtl.ID).FirstOrDefaultAsync();
                if (stockUniicode == null)
                {
                    return result.Error($"{desc}托盘【{stock.palletBarcode}】,库存编码【{stock.stockCode}】未找到对应的库存唯一码信息");
                }
                var wmsItnInventoryRecordIns = await DC.Set<WmsItnInventoryRecord>().Where(x => x.palletBarcode == stockUniicode.palletBarcode && x.inOutTypeNo=="CHECK_IN").ToListAsync();
                if (wmsItnInventoryRecordIns.Any())
                {
                    return result.Error($"{desc}托盘【{stock.palletBarcode}】,找到回库的盘点记录");
                }
                if (hasParentTransaction == false)
                {
                    await DC.Database.BeginTransactionAsync();
                }

                //更新盘点记录，盘点出库记录转历史
                wmsItnInventoryRecord.confirmBy = input.confirmBy;
                wmsItnInventoryRecord.confirmQty = input.confirmQty;
                wmsItnInventoryRecord.confirmReason = input.confirmReason;
                wmsItnInventoryRecord.differenceFlag = (int)input.differenceFlag;
                wmsItnInventoryRecord.inventoryRecordStatus = 90;
                wmsItnInventoryRecord.UpdateBy = input.confirmBy;
                wmsItnInventoryRecord.UpdateTime = DateTime.Now;
                await ((DbContext)DC).Set<WmsItnInventoryRecord>().SingleUpdateAsync(wmsItnInventoryRecord);
                WmsItnInventoryRecordHis his = CommonHelper.Map<WmsItnInventoryRecord, WmsItnInventoryRecordHis>(wmsItnInventoryRecord, "ID");
                await ((DbContext)DC).Set<WmsItnInventoryRecordHis>().SingleInsertAsync(his);
                await ((DbContext)DC).Set<WmsItnInventoryRecord>().SingleDeleteAsync(wmsItnInventoryRecord);
                await ((DbContext)DC).BulkSaveChangesAsync();

                //有差异生成盘点差异记录
                if (input.differenceFlag != 0)
                {
                    //生成差异记录
                    WmsItnInventoryRecordDif wmsItnInventoryRecordDif = BuildCheckDifByRecord(wmsItnInventoryRecord, stockUniicode, difQty);
                    await ((DbContext)DC).Set<WmsItnInventoryRecordDif>().SingleInsertAsync(wmsItnInventoryRecordDif);
                    await ((DbContext)DC).BulkSaveChangesAsync();
                }

                #region 回库逻辑，注意数量是原有数量还是确认数量

                #endregion
                //生成盘点回库记录
                wmsItnInventoryRecord = BuildWmsItnInventoryRecordInByUniicode(stockUniicode, stock, input);
                wmsItnInventoryRecord.inventoryNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.inventoryNo.GetCode()); // 盘点记录单号
                await ((DbContext)DC).Set<WmsItnInventoryRecord>().SingleInsertAsync(wmsItnInventoryRecord);


                //更新库存状态，清空库位信息
                #region 库存处理
                //库存主表更新状态
                stock.stockStatus = 20;
                stock.binNo = "";
                stock.UpdateBy = input.confirmBy;
                stock.UpdateTime = DateTime.Now;
                await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(stock);

                //库存明细表更新状态
                stockDtl.stockDtlStatus = 20;
                //stockDtl.qty = input.confirmQty;//todo:数量问题
                stockDtl.occupyQty = 0;
                stockDtl.UpdateBy = input.confirmBy;
                stockDtl.UpdateTime = DateTime.Now;
                await ((DbContext)DC).Set<WmsStockDtl>().SingleUpdateAsync(stockDtl);

                //库存唯一码更新数量
                //stockUniicode.qty = input.confirmQty;//todo:数量问题
                stockUniicode.occupyQty = 0;
                stockUniicode.UpdateBy = input.confirmBy;
                stockUniicode.UpdateTime = DateTime.Now;
                await ((DbContext)DC).Set<WmsStockUniicode>().SingleUpdateAsync(stockUniicode);
                await ((DbContext)DC).BulkSaveChangesAsync();
                #endregion

                //生成任务
                WmsTask wmsTask = new WmsTask();
                wmsTask = BuildWmsInTaskByItnInventoryRecord(wmsItnInventoryRecord, stock);
                wmsTask.wmsTaskNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.wmsTaskNo.GetCode());
                await ((DbContext)DC).Set<WmsTask>().SingleInsertAsync(wmsTask);


                //生成指令
                SrmCmd srmCmd = new SrmCmd();
                srmCmd = BuildSrmInCmdByWmsTask(wmsTask);
                srmCmd.SubTask_No = await sysSequenceVM.GetSequenceAsync(SequenceCode.srmCmdNo.GetCode());
                srmCmd.Serial_No = Convert.ToInt16(await sysSequenceVM.GetSequenceAsync(srmCmd.Device_No));
                srmCmd.Check_Point = (short)(srmCmd.Serial_No + srmCmd.To_ForkDirection + srmCmd.To_Column + srmCmd.To_Layer + srmCmd.From_Station);//计算
                await ((DbContext)DC).Set<SrmCmd>().SingleInsertAsync(srmCmd);

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
        /// 盘点确认出库
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> ConfirmInventoryOut(ConfirmInventoryTaskDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "盘点确认出库:";
            var hasParentTransaction = false;
            SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
            BasWBinVM basWBinVM = Wtm.CreateVM<BasWBinVM>();
            if (DC.Database.CurrentTransaction != null)
            {
                hasParentTransaction = true;
            }
            try
            {
                var wmsItnInventoryRecords = await DC.Set<WmsItnInventoryRecord>().Where(x => x.ID == input.ID && x.inventoryRecordStatus<90).ToListAsync();
                if (wmsItnInventoryRecords.Count==0)
                {
                    msg = $"{desc}根据ID【{input.ID}】查询不到盘点记录";
                    return result.Error(msg);
                }
                if (wmsItnInventoryRecords.Count > 1)
                {
                    msg = $"{desc}根据ID【{input.ID}】查询到多条盘点记录";
                    return result.Error(msg);
                }
                WmsItnInventoryRecord wmsItnInventoryRecord = new WmsItnInventoryRecord();
                wmsItnInventoryRecord = wmsItnInventoryRecords.FirstOrDefault();
                if (wmsItnInventoryRecord == null)
                {
                    msg = $"{desc}根据ID【{input.ID}】查询不到盘点记录";
                    return result.Error(msg);
                }
                decimal difQty =(decimal)( input.confirmQty??0 - wmsItnInventoryRecord.inventoryQty??0);
                if (difQty == 0)
                {
                    if (input.differenceFlag!=0)
                    {
                        msg = $"{desc}根据ID【{input.ID}】查询到盘点记录，盘点确认数量为【{input.confirmQty}】,盘点数量为【{wmsItnInventoryRecord.inventoryQty}】,差值为【{difQty}】，与差异标识不符，差异标识【{input.differenceFlag}】";
                        return result.Error(msg);
                    }
                }
                else if (difQty > 0)
                {
                    if (input.differenceFlag != 1)
                    {
                        msg = $"{desc}根据ID【{input.ID}】查询到盘点记录，盘点确认数量为【{input.confirmQty}】,盘点数量为【{wmsItnInventoryRecord.inventoryQty}】,差值为【{difQty}】，与差异标识不符，差异标识【{input.differenceFlag}】";
                        return result.Error(msg);
                    }
                }
                else
                {
                    if (input.differenceFlag != 2)
                    {
                        msg = $"{desc}根据ID【{input.ID}】查询到盘点记录，盘点确认数量为【{input.confirmQty}】,盘点数量为【{wmsItnInventoryRecord.inventoryQty}】,差值为【{difQty}】，与差异标识不符，差异标识【{input.differenceFlag}】";
                        return result.Error(msg);
                    }
                }
                // 库存信息
                WmsStock stock = await ((DbContext)DC).Set<WmsStock>().Where(x => x.stockCode == wmsItnInventoryRecord.stockCode && x.palletBarcode == wmsItnInventoryRecord.palletBarcode).FirstOrDefaultAsync();
                if (stock == null)
                {
                    return result.Error($"{desc} 库存编码【{wmsItnInventoryRecord.stockCode}】未找到对应的库存信息");
                }
                // 库存明细信息
                WmsStockDtl stockDtl = await ((DbContext)DC).Set<WmsStockDtl>().Where(x => x.stockCode == stock.stockCode).FirstOrDefaultAsync();
                if (stockDtl == null)
                {
                    return result.Error($"{desc} 库存编码【{stock.stockCode}】未找到对应的库存明细信息");
                }
                //库存唯一码信息
                WmsStockUniicode stockUniicode = await ((DbContext)DC).Set<WmsStockUniicode>().Where(x => x.stockCode == stock.stockCode && x.stockDtlId == stockDtl.ID).FirstOrDefaultAsync();
                if (stockUniicode == null)
                {
                    return result.Error($"{desc}托盘【{stock.palletBarcode}】,库存编码【{stock.stockCode}】未找到对应的库存唯一码信息");
                }

                // 单据类型
                CfgDocType docType = await ((DbContext)DC).Set<CfgDocType>().Where(x => x.docTypeCode == wmsItnInventoryRecord.docTypeCode).FirstOrDefaultAsync();
                if (docType == null)
                {
                    return result.Error($"{desc}单据类型【{wmsItnInventoryRecord.docTypeCode}】信息未配置");
                }

                if (hasParentTransaction == false)
                {
                    await DC.Database.BeginTransactionAsync();
                }
                #region 有差异
                if (input.differenceFlag != 0)
                {
                    //生成差异记录
                    WmsItnInventoryRecordDif wmsItnInventoryRecordDif = BuildCheckDifByRecord(wmsItnInventoryRecord, stockUniicode, difQty);
                    await ((DbContext)DC).Set<WmsItnInventoryRecordDif>().SingleInsertAsync(wmsItnInventoryRecordDif);
                    await ((DbContext)DC).BulkSaveChangesAsync();
                    if (hasParentTransaction == false)
                    {
                        await DC.Database.CommitTransactionAsync();
                    }
                    msg = $"{desc}时间【{DateTime.Now}】,入参【{JsonConvert.SerializeObject(input)}】,生成盘点差异记录成功";
                    logger.Warn($"----->Warn----->{desc}:{msg} ");
                    return result.Success(msg);
                }
                #endregion
                #region 库存
                //库存主表更新状态
                stock.stockStatus = 90;
                stock.UpdateBy = input.confirmBy;
                stock.UpdateTime = DateTime.Now;
                await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(stock);

                //库存明细表更新状态
                stockDtl.stockDtlStatus = 90;
                stockDtl.UpdateBy = input.confirmBy;
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
                stockUniicodeHis.UpdateBy = input.confirmBy;
                stockUniicodeHis.UpdateTime = DateTime.Now;

                await ((DbContext)DC).Set<WmsStockUniicodeHis>().SingleInsertAsync(stockUniicodeHis);
                await ((DbContext)DC).Set<WmsStockUniicode>().SingleDeleteAsync(stockUniicode);
                await ((DbContext)DC).BulkSaveChangesAsync();


                WmsStockAdjust stockAdjust = new WmsStockAdjust();
                stockAdjust.whouseNo = wmsItnInventoryRecord.whouseNo;
                stockAdjust.proprietorCode = wmsItnInventoryRecord.proprietorCode;
                stockAdjust.stockCode = stock.stockCode;
                stockAdjust.palletBarcode = stock.palletBarcode;
                stockAdjust.packageBarcode = "";
                stockAdjust.adjustOperate = "下架";
                stockAdjust.adjustType = "UDP";
                stockAdjust.adjustDesc = $"盘点下架操作：盘点单记录ID【{wmsItnInventoryRecord.ID}】；【{stock.stockCode}】库存主表，明细表，唯一码表转历史";
                stockAdjust.CreateBy = input.confirmBy;
                stockAdjust.CreateTime = DateTime.Now;
                await ((DbContext)DC).Set<WmsStockAdjust>().AddAsync(stockAdjust);
                #region 业务类型
                if ("INVENTORY".Equals(docType.businessCode))
                {
                    wmsItnInventoryRecord.confirmBy = input.confirmBy;
                    wmsItnInventoryRecord.confirmQty = input.confirmQty;
                    wmsItnInventoryRecord.confirmReason = input.confirmReason;
                    wmsItnInventoryRecord.differenceFlag = (int)input.differenceFlag;
                    wmsItnInventoryRecord.inventoryRecordStatus = 90;
                    wmsItnInventoryRecord.UpdateBy = input.confirmBy;
                    wmsItnInventoryRecord.UpdateTime = DateTime.Now;
                    await ((DbContext)DC).Set<WmsItnInventoryRecord>().SingleUpdateAsync(wmsItnInventoryRecord);
                    WmsItnInventoryRecordHis his = CommonHelper.Map<WmsItnInventoryRecord, WmsItnInventoryRecordHis>(wmsItnInventoryRecord, "ID");
                    await ((DbContext)DC).Set<WmsItnInventoryRecordHis>().SingleInsertAsync(his);
                    await ((DbContext)DC).Set<WmsItnInventoryRecord>().SingleDeleteAsync(wmsItnInventoryRecord);
                    await ((DbContext)DC).BulkSaveChangesAsync();
                }
                

                #endregion
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
    }
}
