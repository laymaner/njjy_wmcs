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
using Microsoft.EntityFrameworkCore;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using System.DirectoryServices.Protocols;
using System.Text.RegularExpressions;
using System.Threading;
using Wish.Areas.BasWhouse.Model;
using Wish.Areas.Config.Model;
using WISH.Helper.Common;
using Z.BulkOperations;
using AutoMapper;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Wish.ViewModel.Common.Dtos;
using Wish.ViewModel.Common;
using Wish.ViewModel.System.SysSequenceVMs;
using Wish.ViewModel.BusinessStock.WmsStockVMs;
using Wish.ViewModel.BasWhouse.BasWBinVMs;
using Wish.TaskConfig.Model;


namespace Wish.ViewModel.BusinessTask.WmsTaskVMs
{
    public partial class WmsTaskVM
    {
        public string RemoveNumber(string key)
        {
            return Regex.Replace(key, @"\d", "");
        }

        private async Task<outRecordForPalletDto> HandleRegionAndLoc(outRecordForPalletDto outRecordForPallet)
        {
            #region 库区和出库站台不对应时
            if (!string.IsNullOrWhiteSpace(outRecordForPallet.targetLocNo))
            {
                var locGroupInfo = await DC.Set<BasWLocGroup>().Where(t => t.locGroupNo.Contains(outRecordForPallet.regionNo) && t.locGroupNo == outRecordForPallet.targetLocNo && t.usedFlag == 1).FirstOrDefaultAsync();
                if (locGroupInfo == null)
                {
                    var locInfo = await DC.Set<BasWLoc>().Where(t => t.locGroupNo.Contains(outRecordForPallet.regionNo) && t.locNo == outRecordForPallet.targetLocNo && t.usedFlag == 1).FirstOrDefaultAsync();
                    if (locInfo == null)
                    {
                        outRecordForPallet.targetLocNo = "";
                    }
                }
            }
            #endregion
            return outRecordForPallet;
        }





        #region 出库任务下发
        public async Task<List<outRecordForDocDto>> OutDownRecordForDoc()
        {
            BusinessResult result = new BusinessResult();
            List<outRecordForDocDto> recordForDocList = new List<outRecordForDocDto>();
            string msg = string.Empty;
            string desc = "物料出库:";
            try
            {

                #region 任务数量判断
                var docTypeInfos = await DC.Set<CfgDocType>().AsNoTracking().Where(t => !t.businessCode.Contains("IN")).ToListAsync();
                List<string> filterDocList = new List<string>();
                var taskInfos = await DC.Set<WmsTask>().Where(t => t.taskStatus < 90 && t.taskTypeNo == "OUT").ToListAsync();
                List<string> palletList = taskInfos.Select(t => t.palletBarcode).Distinct().ToList();
                var outRecordInfos = await DC.Set<WmsOutInvoiceRecord>().Where(t => t.outRecordStatus > 0 && t.outRecordStatus < 90 && palletList.Contains(t.palletBarcode)).OrderBy(t => t.CreateTime).ToListAsync();
                var groupRecordForDoc = outRecordInfos.GroupBy(t => new { t.docTypeCode });
                foreach (var doc in docTypeInfos)
                {
                    outRecordForDocDto recordForDocView = new outRecordForDocDto()
                    {
                        docTypeCode = doc.docTypeCode,
                        docTypeName = doc.docTypeName,
                        businessCode = doc.businessCode,
                        docPriority = doc.docPriority ?? 99

                    };
                    if (doc.taskMaxQty == null)
                    {
                        recordForDocView.isLimitTaskCount = false;
                        recordForDocView.canDownTaskCount = 999;
                    }
                    else
                    {
                        recordForDocView.isLimitTaskCount = true;
                        var outRecordForDocInfos = outRecordInfos.Where(t => t.docTypeCode == doc.docTypeCode).ToList();
                        int count = outRecordForDocInfos.Select(t => t.palletBarcode).Distinct().Count();
                        recordForDocView.canDownTaskCount = doc.taskMaxQty.Value - count;
                    }

                    if (recordForDocView.canDownTaskCount > 0 || recordForDocView.isLimitTaskCount == false)
                        recordForDocList.Add(recordForDocView);
                }
                #endregion


            }
            catch (Exception e)
            {

            }
            return recordForDocList;
        }
        public outRecordForDocDto OutDownRecordForDoc(string docTypeCode)
        {
            outRecordForDocDto result = new outRecordForDocDto();
            string msg = string.Empty;
            string desc = "物料出库:";
            try
            {
                #region 任务数量判断
                var docTypeInfos = DC.Set<CfgDocType>().AsNoTracking().Where(t => t.docTypeCode == docTypeCode && !t.businessCode.Contains("IN")).FirstOrDefault();
                List<string> filterDocList = new List<string>();
                var taskInfos = DC.Set<WmsTask>().Where(t => t.taskStatus < 90 && t.taskTypeNo == "OUT").ToList();
                List<string> palletList = taskInfos.Select(t => t.palletBarcode).Distinct().ToList();
                var outRecordInfos = DC.Set<WmsOutInvoiceRecord>().Where(t => t.outRecordStatus > 0 && t.outRecordStatus < 90 && palletList.Contains(t.palletBarcode)).OrderBy(t => t.CreateTime).ToList();
                var groupRecordForDoc = outRecordInfos.GroupBy(t => new { t.docTypeCode });
                result = new outRecordForDocDto()
                {
                    docTypeCode = docTypeInfos.docTypeCode,
                    docTypeName = docTypeInfos.docTypeName,
                    businessCode = docTypeInfos.businessCode,
                    docPriority = docTypeInfos.docPriority ?? 99

                };
                if (docTypeInfos.taskMaxQty == null)
                {
                    result.isLimitTaskCount = false;
                }
                else
                {
                    result.isLimitTaskCount = true;
                }
                var outRecordForDocInfos = outRecordInfos.Where(t => t.docTypeCode == docTypeInfos.docTypeCode).ToList();
                int count = outRecordForDocInfos.Select(t => t.palletBarcode).Distinct().Count();
                result.canDownTaskCount = docTypeInfos.taskMaxQty.Value - count;
                #endregion


            }
            catch (Exception e)
            {

            }
            return result;
        }

        /// <summary>
        /// 托盘是否整出处理
        /// </summary>
        /// <param name="regionList"></param>
        public async Task PalletWholeOutHandle(List<string> regionList)
        {
            var outRecordInfo = await DC.Set<WmsOutInvoiceRecord>().Where(t => t.outRecordStatus == 0 && t.loadedTtype == 1 && regionList.Contains(t.regionNo)).OrderBy(t => t.CreateTime).ToListAsync();
            if (outRecordInfo.Any())
            {
                var groupRecord = outRecordInfo.GroupBy(t => new { t.binNo, t.palletBarcode, t.stockCode });
                foreach (var item in groupRecord)
                {
                    #region 判断托盘是否整出
                    var recordlist = item.ToList();
                    var stockDtlInfos = await DC.Set<WmsStockDtl>().Where(t => t.palletBarcode == item.Key.palletBarcode && t.stockCode == item.Key.stockCode).ToListAsync();
                    if (stockDtlInfos.Sum(t => t.qty) <= stockDtlInfos.Sum(t => t.occupyQty))
                    {
                        recordlist.ForEach(t =>
                        {
                            t.palletPickType = 0;
                        });

                    }
                    else
                    {
                        recordlist.ForEach(t =>
                        {
                            t.palletPickType = 1;
                        });
                    }
                    await ((DbContext)DC).BulkUpdateAsync(recordlist);
                    BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                    await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                    #endregion
                }

            }
        }


        /// <summary>
        /// 托盘是否整出处理
        /// </summary>
        /// <param name="regionList"></param>
        public void PalletWholeOutHandle(string palletBarcode, string stockCode)
        {
            var outRecordInfo = DC.Set<WmsOutInvoiceRecord>().Where(t => t.outRecordStatus == 0 && t.loadedTtype == 1 && t.palletBarcode == palletBarcode).OrderBy(t => t.CreateTime).ToList();
            if (outRecordInfo.Any())
            {
                var stockDtlInfos = DC.Set<WmsStockDtl>().Where(t => t.palletBarcode == palletBarcode && t.stockCode == stockCode).ToList();
                if (stockDtlInfos.Sum(t => t.qty) <= stockDtlInfos.Sum(t => t.occupyQty))
                {
                    outRecordInfo.ForEach(t =>
                    {
                        t.palletPickType = 0;
                    });

                }
                else
                {
                    outRecordInfo.ForEach(t =>
                    {
                        t.palletPickType = 1;
                    });
                }
                ((DbContext)DC).BulkUpdate(outRecordInfo);
                BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                ((DbContext)DC).BulkSaveChanges(t => t = option);

            }
        }
        //任务下发
        //1.任务数量卡控
        //2. 移库校验，移库任务生成
        //3.任务生成
        /// <summary>
        /// 出库记录生成任务总入口
        /// </summary>
        /// <returns></returns>
        public async Task<BusinessResult> OutDownRecord(string docNo = "", string invoker = "WMS")
        {
            string key = "WmsTaskVMCore_OutDownRecord";
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "物料出库:";
            try
            {
                ReportLockProvider.WaitAsync(key).Wait();
                logger.Warn($"----->Warn----->WMS任务生成:1 ");

                Thread.Sleep(TimeSpan.FromSeconds(2)); // 暂停执行两秒


                logger.Warn($"----->Warn----->WMS任务生成:2 ");

                Thread.Sleep(TimeSpan.FromSeconds(2)); // 暂停执行两秒
                logger.Warn($"----->Warn----->WMS任务生成:3 ");

                Thread.Sleep(TimeSpan.FromSeconds(2)); // 暂停执行两秒

                List<outRecordForDocDto> recordForDocList = await OutDownRecordForDoc();
                if (recordForDocList.Count == 0)
                {
                    return result.Success("未可出库的单据类型");
                }

                List<string> list = new List<string>() { "MTV", "SRM" };
                var lkRegionInfo = await DC.Set<BasWRegion>().AsNoTracking().Where(t => list.Contains(t.pickupMethod)).ToListAsync();
                List<string> regionList = lkRegionInfo.Select(t => t.regionNo).Distinct().ToList();
                await PalletWholeOutHandle(regionList);

                var outRecordInfo = await DC.Set<WmsOutInvoiceRecord>().Where(t => t.outRecordStatus == 0 && t.loadedTtype == 1 && regionList.Contains(t.regionNo))
                     .WhereIf(!string.IsNullOrWhiteSpace(docNo), t => t.invoiceNo == docNo).OrderBy(t => t.CreateTime).ToListAsync();
                if (outRecordInfo.Count > 0)
                {

                    var groupRecord = outRecordInfo.GroupBy(t => new { t.binNo, t.docTypeCode, t.palletBarcode, t.stockCode });
                    foreach (var item in groupRecord)
                    {
                        var docInfo = recordForDocList.FirstOrDefault(t => t.docTypeCode == item.Key.docTypeCode);
                        if (docInfo.canDownTaskCount > 0 || docInfo.isLimitTaskCount == false)
                        {
                            var invoiceRecord = item.FirstOrDefault();
                            outRecordForPalletDto outRecordForPallet = new outRecordForPalletDto()
                            {
                                regionNo = invoiceRecord.regionNo,
                                binNo = invoiceRecord.binNo,
                                docTypeCode = invoiceRecord.docTypeCode,
                                palletBarcode = invoiceRecord.palletBarcode,
                                palletPickType = invoiceRecord.palletPickType ?? 1,
                                stockCode = invoiceRecord.stockCode,
                                docNo = invoiceRecord.invoiceNo,
                                waveNo = invoiceRecord.waveNo,
                                invoker = invoker,
                                oldLocNo = invoiceRecord.deliveryLocNo,
                                targetLocNo = invoiceRecord.deliveryLocNo,
                                businessCode = docInfo.businessCode,
                                docPriority = docInfo.docPriority,
                                palletPrefix = RemoveNumber(invoiceRecord.palletBarcode)
                            };
                            #region 库区和出库站台不对应时
                            outRecordForPallet = await HandleRegionAndLoc(outRecordForPallet);
                            #endregion

                            result = await OutDownRecordForSingle(outRecordForPallet);
                            if (result.code == ResCode.OK)
                            {
                                docInfo.canDownTaskCount--;
                                msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:托盘【{invoiceRecord.palletBarcode}】生成出库任务成功：{result.msg}";
                                logger.Warn($"----->Warn----->WMS任务生成:{msg} ");
                            }
                            else
                            {
                                msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:托盘【{invoiceRecord.palletBarcode}】生成出库任务异常：{result.msg}";
                                logger.Warn($"----->Warn----->WMS任务生成:{msg} ");
                            }
                        }
                        else if (docInfo.canDownTaskCount <= 0)
                        {
                            msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:单据类型【{docInfo.docTypeName}】任务已达上限：暂不出库";
                            logger.Warn($"----->Warn----->WMS任务生成:{msg} ");
                        }


                    }
                }
                else
                {
                    return result.Success("未待出库记录");
                }


            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                return result.Error(msg);
            }
            finally
            {
                ReportLockProvider.Release(key);
            }
            return result;
        }
        #endregion


        #region 移库下发
        public async Task<List<outRecordForDocDto>> OutDownMoveForDoc(string businessCode = "MOVE")
        {
            List<outRecordForDocDto> result = new List<outRecordForDocDto>();
            try
            {
                List<string> regionList = new List<string>() { "LXK", "TPK" };
                var docTypeInfos = await DC.Set<CfgDocType>().AsNoTracking().Where(t => t.businessCode == businessCode).ToListAsync();
                var moveDtlInfos = await DC.Set<WmsItnMoveDtl>().Where(t => t.moveDtlStatus > 0 && t.moveDtlStatus < 90 && regionList.Contains(t.regionNo)).ToListAsync();
                var moveNoList = moveDtlInfos.Select(t => t.moveNo).Distinct().ToList();
                var moveInfos = await DC.Set<WmsItnMove>().Where(t => t.moveStatus > 0 && t.moveStatus < 90 && moveNoList.Contains(t.moveNo)).ToListAsync();
                foreach (var doc in docTypeInfos)
                {
                    outRecordForDocDto recordForDocView = new outRecordForDocDto()
                    {
                        docTypeCode = doc.docTypeCode,
                        docTypeName = doc.docTypeName,
                        businessCode = doc.businessCode,
                        docPriority = doc.docPriority ?? 99

                    };
                    if (doc.taskMaxQty == null)
                    {
                        recordForDocView.isLimitTaskCount = false;
                        recordForDocView.canDownTaskCount = 999;
                    }
                    else
                    {
                        recordForDocView.isLimitTaskCount = true;
                        var moveNoForDocList = moveInfos.Where(t => t.docTypeCode == doc.docTypeCode).Select(t => t.moveNo).ToList();
                        int count = moveDtlInfos.Where(t => moveNoForDocList.Contains(t.moveNo)).Select(t => t.palletBarcode).Distinct().Count();
                        recordForDocView.canDownTaskCount = doc.taskMaxQty.Value - count;
                    }
                    if (recordForDocView.canDownTaskCount > 0 || recordForDocView.isLimitTaskCount == false)
                        result.Add(recordForDocView);
                }
                // var taskInfos = await DC.Set<WmsTask>().Where(t => t.taskStatus < 90 && t.taskTypeNo == "OUT").ToListAsync();

            }
            catch
            {

            }
            return result;
        }
        //任务下发
        //1.任务数量卡控
        //2. 移库校验，移库任务生成
        //3.任务生成
        /// <summary>
        /// 移库生成任务总入口
        /// </summary>
        /// <returns></returns>
        public async Task<BusinessResult> OutDownMove(string docNo = "", string invoker = "WMS")
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "物料移库出库:";
            try
            {

                List<outRecordForDocDto> moveForDocList = await OutDownMoveForDoc();
                if (moveForDocList.Count == 0)
                {
                    return result.Success("未可出库的移库单据类型");
                }

                List<string> list = new List<string>() { "MTV", "SRM" };
                var lkRegionInfo = await DC.Set<BasWRegion>().AsNoTracking().Where(t => list.Contains(t.pickupMethod)).ToListAsync();
                List<string> regionList = lkRegionInfo.Select(t => t.regionNo).Distinct().ToList();

                var moveDtlInfos = await DC.Set<WmsItnMoveDtl>().Where(t => t.moveDtlStatus == 0 && regionList.Contains(t.regionNo))
                                   .WhereIf(!string.IsNullOrWhiteSpace(docNo), t => t.moveNo == docNo).ToListAsync();
                var moveNoList = moveDtlInfos.Select(t => t.moveNo).Distinct().ToList();
                var moveInfos = await DC.Set<WmsItnMove>().Where(t => moveNoList.Contains(t.moveNo)).OrderBy(t => t.CreateTime).ToListAsync();
                if (moveDtlInfos.Any())
                {

                    var groupRecord = moveInfos.GroupBy(t => new { t.docTypeCode });
                    foreach (var item in groupRecord)
                    {
                        var docInfo = moveForDocList.FirstOrDefault(t => t.docTypeCode == item.Key.docTypeCode);
                        if (docInfo.canDownTaskCount > 0 || docInfo.isLimitTaskCount == false)
                        {
                            var moveForDocInfos = moveInfos.Where(t => t.docTypeCode == item.Key.docTypeCode).ToList();
                            foreach (var moveInfo in moveForDocInfos)
                            {
                                var moveDtlForDocInfos = moveDtlInfos.Where(t => t.moveNo == moveInfo.moveNo && t.moveDtlStatus == 0).ToList();
                                foreach (var moveDtlInfo in moveDtlInfos)
                                {
                                    outRecordForPalletDto outRecordForPallet = new outRecordForPalletDto()
                                    {
                                        regionNo = moveDtlInfo.regionNo,
                                        binNo = moveDtlInfo.binNo,
                                        docTypeCode = moveInfo.docTypeCode,
                                        palletBarcode = moveDtlInfo.palletBarcode,
                                        palletPickType = 0,
                                        stockCode = moveDtlInfo.stockCode,
                                        docNo = moveDtlInfo.moveNo,
                                        waveNo = "",
                                        invoker = invoker,
                                        businessCode = docInfo.businessCode,
                                        docPriority = docInfo.docPriority,
                                        targetLocNo = moveInfo.putdownLocNo,
                                        oldLocNo = moveInfo.putdownLocNo,
                                        palletPrefix = RemoveNumber(moveDtlInfo.palletBarcode)
                                    };
                                    #region 库区和出库站台不对应时
                                    outRecordForPallet = await HandleRegionAndLoc(outRecordForPallet);
                                    #endregion
                                    result = await OutDownRecordForSingle(outRecordForPallet);
                                    if (result.code == ResCode.OK)
                                    {
                                        docInfo.canDownTaskCount--;
                                        msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:托盘【{moveDtlInfo.palletBarcode}】生成移库出库任务成功：{result.msg}";
                                        logger.Warn($"----->Warn----->WMS任务生成:{msg} ");
                                        if (docInfo.canDownTaskCount <= 0)
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:托盘【{moveDtlInfo.palletBarcode}】生成移库出库任务异常：{result.msg}";
                                        logger.Warn($"----->Warn----->WMS任务生成:{msg} ");
                                    }
                                }
                                if (docInfo.canDownTaskCount <= 0)
                                {
                                    break;
                                }

                            }
                        }


                    }
                }
                else
                {
                    return result.Success("未待下发移库明细");
                }


            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                return result.Error(msg);
            }
            return result;
        }
        #endregion

        #region 抽检下发
        public async Task<List<outRecordForDocDto>> OutDownQcForDoc(string businessCode = "QC")
        {
            List<outRecordForDocDto> result = new List<outRecordForDocDto>();
            try
            {
                List<string> regionList = new List<string>() { "LXK", "TPK" };
                var docTypeInfos = await DC.Set<CfgDocType>().AsNoTracking().Where(t => t.businessCode == businessCode).ToListAsync();
                var qcRecordInfos = await DC.Set<WmsItnQcRecord>().Where(t => t.itnQcStatus > 0 && t.itnQcStatus < 90).ToListAsync();
                var qcNoList = qcRecordInfos.Select(t => t.itnQcNo).Distinct().ToList();
                var qcInfos = await DC.Set<WmsItnQc>().Where(t => t.itnQcStatus > 0 && t.itnQcStatus < 90 && qcNoList.Contains(t.itnQcNo)).ToListAsync();
                foreach (var doc in docTypeInfos)
                {
                    outRecordForDocDto recordForDocView = new outRecordForDocDto()
                    {
                        docTypeCode = doc.docTypeCode,
                        docTypeName = doc.docTypeName,
                        businessCode = doc.businessCode,
                        docPriority = doc.docPriority ?? 99

                    };
                    if (doc.taskMaxQty == null)
                    {
                        recordForDocView.isLimitTaskCount = false;
                        recordForDocView.canDownTaskCount = 999;
                    }
                    else
                    {
                        recordForDocView.isLimitTaskCount = true;
                        var qcNoForDocList = qcInfos.Where(t => t.docTypeCode == doc.docTypeCode).Select(t => t.itnQcNo).ToList();
                        int count = qcRecordInfos.Where(t => qcNoForDocList.Contains(t.itnQcNo)).Select(t => t.palletBarcode).Distinct().Count();
                        recordForDocView.canDownTaskCount = doc.taskMaxQty.Value - count;
                    }
                    if (recordForDocView.canDownTaskCount > 0 || recordForDocView.isLimitTaskCount == false)
                        result.Add(recordForDocView);
                }
                // var taskInfos = await DC.Set<WmsTask>().Where(t => t.taskStatus < 90 && t.taskTypeNo == "OUT").ToListAsync();

            }
            catch
            {

            }
            return result;
        }
        //任务下发
        //1.任务数量卡控
        //2. 移库校验，移库任务生成
        //3.任务生成
        /// <summary>
        /// 抽检生成任务总入口
        /// </summary>
        /// <returns></returns>
        public async Task<BusinessResult> OutDownQc(string docNo = "", string invoker = "WMS")
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "物料抽检出库:";
            try
            {

                List<outRecordForDocDto> qcForDocList = await OutDownQcForDoc();
                if (qcForDocList.Count == 0)
                {
                    return result.Success("未可出库的抽检单据类型");
                }

                List<string> list = new List<string>() { "MTV", "SRM" };
                var lkRegionInfo = await DC.Set<BasWRegion>().AsNoTracking().Where(t => list.Contains(t.pickupMethod)).ToListAsync();
                List<string> regionList = lkRegionInfo.Select(t => t.regionNo).Distinct().ToList();

                var qcRecordInfos = await DC.Set<WmsItnQcRecord>().Where(t => t.itnQcStatus == 0)
                                    .WhereIf(!string.IsNullOrWhiteSpace(docNo), t => t.itnQcNo == docNo).ToListAsync();
                var qcNoList = qcRecordInfos.Select(t => t.itnQcNo).Distinct().ToList();
                var qcInfos = await DC.Set<WmsItnQc>().Where(t => qcNoList.Contains(t.itnQcNo)).OrderBy(t => t.CreateTime).ToListAsync();
                if (qcRecordInfos.Any())
                {

                    var groupRecord = qcRecordInfos.GroupBy(t => new { t.palletBarcode, t.stockCode, t.itnQcNo });
                    foreach (var item in groupRecord)
                    {
                        var docInfo = qcForDocList.FirstOrDefault();
                        var qcInfo = qcInfos.FirstOrDefault(t => t.itnQcNo == item.Key.itnQcNo);
                        if (docInfo.canDownTaskCount > 0 || docInfo.isLimitTaskCount == false)
                        {
                            var stockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == item.Key.palletBarcode && t.stockCode == item.Key.stockCode).FirstOrDefaultAsync();
                            if (stockInfo != null)
                            {
                                outRecordForPalletDto outRecordForPallet = new outRecordForPalletDto()
                                {
                                    regionNo = stockInfo.regionNo,
                                    binNo = stockInfo.binNo,
                                    docTypeCode = docInfo.docTypeCode,
                                    palletBarcode = stockInfo.palletBarcode,
                                    palletPickType = 0,
                                    stockCode = stockInfo.stockCode,
                                    docNo = item.Key.itnQcNo,
                                    waveNo = "",
                                    invoker = invoker,
                                    oldLocNo = qcInfo.itnQcLocNo,
                                    targetLocNo = qcInfo.itnQcLocNo,
                                    businessCode = docInfo.businessCode,
                                    docPriority = docInfo.docPriority,
                                    palletPrefix = RemoveNumber(stockInfo.palletBarcode)
                                };
                                #region 库区和出库站台不对应时
                                outRecordForPallet = await HandleRegionAndLoc(outRecordForPallet);
                                #endregion
                                result = await OutDownRecordForSingle(outRecordForPallet);
                                if (result.code == ResCode.OK)
                                {
                                    docInfo.canDownTaskCount--;
                                    msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:托盘【{stockInfo.palletBarcode}】生成抽检出库任务成功：{result.msg}";
                                    logger.Warn($"----->Warn----->WMS任务生成:{msg} ");
                                }
                                else
                                {
                                    msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:托盘【{stockInfo.palletBarcode}】生成抽检出库任务异常：{result.msg}";
                                    logger.Warn($"----->Warn----->WMS任务生成:{msg} ");
                                }
                            }

                        }
                        else if (docInfo.canDownTaskCount <= 0)
                        {
                            msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:单据类型【{docInfo.docTypeName}】任务已达上限：暂不出库";
                            logger.Warn($"----->Warn----->WMS任务生成:{msg} ");
                        }


                    }
                }
                else
                {
                    return result.Success("未待下发抽检记录");
                }


            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                return result.Error(msg);
            }
            return result;
        }
        #endregion


        #region 盘点下发
        public async Task<List<outRecordForDocDto>> OutDownInventoryForDoc(string businessCode = "INVENTORY")
        {
            List<outRecordForDocDto> result = new List<outRecordForDocDto>();
            try
            {
                List<string> regionList = new List<string>() { "LXK", "TPK" };
                var docTypeInfos = await DC.Set<CfgDocType>().AsNoTracking().Where(t => t.businessCode == businessCode).ToListAsync();
                var inventoryRecordInfos = await DC.Set<WmsItnInventoryRecord>().Where(t => t.inventoryRecordStatus > 0 && t.inventoryRecordStatus < 90).ToListAsync();
                //var qcNoList = qcRecordInfos.Select(t => t.itnQcNo).Distinct().ToList();
                //var qcInfos = await DC.Set<WmsItnQc>().Where(t => t.itnQcStatus > 0 && t.itnQcStatus < 90 && qcNoList.Contains(t.itnQcNo)).ToListAsync();
                foreach (var doc in docTypeInfos)
                {
                    outRecordForDocDto recordForDocView = new outRecordForDocDto()
                    {
                        docTypeCode = doc.docTypeCode,
                        docTypeName = doc.docTypeName,
                        businessCode = doc.businessCode,
                        docPriority = doc.docPriority ?? 99

                    };
                    if (doc.taskMaxQty == null)
                    {
                        recordForDocView.isLimitTaskCount = false;
                        recordForDocView.canDownTaskCount = 999;
                    }
                    else
                    {
                        recordForDocView.isLimitTaskCount = true;
                        // var qcNoForDocList = qcInfos.Select(t => t.itnQcNo).ToList();
                        int count = inventoryRecordInfos.Where(t => t.docTypeCode == doc.docTypeCode).Select(t => t.palletBarcode).Distinct().Count();
                        recordForDocView.canDownTaskCount = doc.taskMaxQty.Value - count;
                    }
                    if (recordForDocView.canDownTaskCount > 0 || recordForDocView.isLimitTaskCount == false)
                        result.Add(recordForDocView);
                }
                // var taskInfos = await DC.Set<WmsTask>().Where(t => t.taskStatus < 90 && t.taskTypeNo == "OUT").ToListAsync();

            }
            catch
            {

            }
            return result;
        }

        //任务下发
        //1.任务数量卡控
        //2. 移库校验，移库任务生成
        //3.任务生成
        /// <summary>
        /// 盘点记录生成任务总入口
        /// </summary>
        /// <returns></returns>
        public async Task<BusinessResult> OutDownInventory(string docNo = "", string invoker = "WMS")
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "物料出库:";
            try
            {

                List<outRecordForDocDto> recordForDocList = await OutDownInventoryForDoc();
                if (recordForDocList.Count == 0)
                {
                    return result.Success("未可出库的单据类型");
                }

                List<string> list = new List<string>() { "MTV", "SRM" };
                var lkRegionInfo = await DC.Set<BasWRegion>().AsNoTracking().Where(t => list.Contains(t.pickupMethod)).ToListAsync();
                List<string> regionList = lkRegionInfo.Select(t => t.regionNo).Distinct().ToList();

                var inventoryRecordInfo = await DC.Set<WmsItnInventoryRecord>().Where(t => t.inventoryRecordStatus == 0)
                    .WhereIf(!string.IsNullOrWhiteSpace(docNo), t => t.inventoryNo == docNo).OrderBy(t => t.CreateTime).ToListAsync();
                if (inventoryRecordInfo.Count > 0)
                {

                    var groupRecord = inventoryRecordInfo.GroupBy(t => new { t.docTypeCode, t.palletBarcode, t.stockCode });
                    foreach (var item in groupRecord)
                    {
                        var docInfo = recordForDocList.FirstOrDefault(t => t.docTypeCode == item.Key.docTypeCode);
                        if (docInfo.canDownTaskCount > 0 || docInfo.isLimitTaskCount == false)
                        {
                            var invoiceRecord = item.FirstOrDefault();
                            var stockInfo = await DC.Set<WmsStock>().Where(t => t.palletBarcode == item.Key.palletBarcode && t.stockCode == item.Key.stockCode).FirstOrDefaultAsync();
                            if (stockInfo != null)
                            {
                                outRecordForPalletDto outRecordForPallet = new outRecordForPalletDto()
                                {
                                    regionNo = stockInfo.regionNo,
                                    binNo = stockInfo.binNo,
                                    docTypeCode = invoiceRecord.docTypeCode,
                                    palletBarcode = invoiceRecord.palletBarcode,
                                    palletPickType = 0,
                                    stockCode = invoiceRecord.stockCode,
                                    docNo = invoiceRecord.inventoryNo,
                                    waveNo = "",
                                    invoker = invoker,
                                    businessCode = docInfo.businessCode,
                                    docPriority = docInfo.docPriority,
                                    oldLocNo = invoiceRecord.putdownLocNo,
                                    targetLocNo = invoiceRecord.putdownLocNo,
                                    palletPrefix = RemoveNumber(invoiceRecord.palletBarcode)
                                };
                                #region 库区和出库站台不对应时
                                outRecordForPallet = await HandleRegionAndLoc(outRecordForPallet);
                                #endregion
                                result = await OutDownRecordForSingle(outRecordForPallet);
                                if (result.code == ResCode.OK)
                                {
                                    docInfo.canDownTaskCount--;
                                    msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:托盘【{invoiceRecord.palletBarcode}】生成出库任务成功：{result.msg}";
                                    logger.Warn($"----->Warn----->WMS任务生成:{msg} ");
                                }
                                else
                                {
                                    msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:托盘【{invoiceRecord.palletBarcode}】生成出库任务异常：{result.msg}";
                                    logger.Warn($"----->Warn----->WMS任务生成:{msg} ");
                                }
                            }

                        }
                        else if (docInfo.canDownTaskCount <= 0)
                        {
                            msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:单据类型【{docInfo.docTypeName}】任务已达上限：暂不出库";
                            logger.Warn($"----->Warn----->WMS任务生成:{msg} ");
                        }


                    }
                }
                else
                {
                    return result.Success("未待下发盘点记录");
                }


            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                return result.Error(msg);
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 出库、抽检、盘点、库区间移库记录生成任务
        /// </summary>
        /// <param name="invoiceRecord"></param>
        /// <param name="businessCode"></param>
        /// <param name="docPriority"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public async Task<BusinessResult> OutDownRecordForSingle(outRecordForPalletDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "物料出库:";
            string msgForCanDown = string.Empty;
            bool isNeedMove = false;
            bool isCanDown = false;
            try
            {


                ////判断是否存在任务
                var taskInfo = await DC.Set<WmsTask>().Where(t => t.palletBarcode == input.palletBarcode && t.taskStatus < 90).FirstOrDefaultAsync();
                if (taskInfo != null)
                {
                    msg = $"{desc}【{input.palletBarcode}】已存在任务【{taskInfo.wmsTaskNo}】";
                    return result.Error(msg);
                }

                var binInfo = await DC.Set<BasWBin>().Where(t => t.binNo == input.binNo).FirstOrDefaultAsync();
                if (binInfo == null)
                {
                    msg = $"{desc}库位【{input.binNo}】未查询到";
                    return result.Error(msg);
                }
                //远伸有无库存、有无占用，有无移库任务
                WmsStock nearStockInfo = null;
                if (binInfo.extensionIdx > 1)
                {
                    var nearBinInfo = await DC.Set<BasWBin>().Where(t => t.roadwayNo == binInfo.roadwayNo && t.regionNo == binInfo.regionNo && t.extensionGroupNo == binInfo.extensionGroupNo && t.extensionIdx == 1).FirstOrDefaultAsync();
                    if (nearBinInfo == null)
                    {
                        isCanDown = true;
                    }
                    else
                    {
                        nearStockInfo = await DC.Set<WmsStock>().Where(t => t.binNo == nearBinInfo.binNo).FirstOrDefaultAsync();

                        if (nearStockInfo == null)
                        {
                            isCanDown = true;
                        }
                        else
                        {
                            var nearTaskInfo = await DC.Set<WmsTask>().Where(t => ((t.palletBarcode == nearStockInfo.palletBarcode) || (t.frLocationNo == nearStockInfo.binNo && t.wmsTaskType == "EXCEPTION_OUT")) && t.taskStatus < 90).FirstOrDefaultAsync();
                            if (nearStockInfo.stockStatus > 50 && nearTaskInfo != null)
                            {
                                isCanDown = true;
                            }
                            else if (nearStockInfo.stockStatus == 50)
                            {
                                //占用数量考虑
                                var nearStockDtlInfos = await DC.Set<WmsStockDtl>().Where(t => t.palletBarcode == nearStockInfo.palletBarcode && t.stockCode == nearStockInfo.stockCode).ToListAsync();
                                var nearStockDtlInfo = nearStockDtlInfos.FirstOrDefault(t => t.occupyQty > 0);
                                if (nearStockDtlInfo != null)
                                {
                                    if (nearTaskInfo == null)
                                    {
                                        isCanDown = false;
                                        msgForCanDown = $"【{input.palletBarcode}】对应库位【{binInfo.binNo}】的近伸库位存在出库记录，但无任务，待近伸库位生成任务，暂时无法下发";
                                    }
                                    else
                                    {
                                        if (nearTaskInfo.wmsTaskType.Contains("OUT"))
                                        {
                                            isCanDown = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (nearTaskInfo == null)
                                    {
                                        isNeedMove = true;
                                    }
                                }
                            }
                            else
                            {
                                isCanDown = false;
                                msgForCanDown = $"【{input.palletBarcode}】对应库位【{binInfo.binNo}】的近伸库位存在入库中库存，暂时无法下发";
                            }
                        }
                    }
                }
                else
                {
                    isCanDown = true;
                }

                //移库
                if (isNeedMove && nearStockInfo != null)
                {
                    //todo:移库处理
                    result = await MoveHandle(nearStockInfo, input.invoker);
                    msgForCanDown = $"{desc}【{input.palletBarcode}】对应库位【{binInfo.binNo}】的近伸库位需要移库，暂时无法下发;{result.msg}";
                    msg = $"{desc}{msgForCanDown}";
                    return result.Error(msg);
                }
                else
                {
                    if (isCanDown)
                    {
                        result = await OutDownRecordForDatabase(input);
                    }
                    else
                    {
                        msg = $"{desc}{msgForCanDown}";
                        return result.Error(msg);
                    }
                }


            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                return result.Error(msg);
            }
            return result;
        }
        /// <summary>
        /// 出库、抽检、盘点、库区间移库记录生成任务事务处理
        /// </summary>
        /// <param name="input"></param>
        /// <param name="businessCode"></param>
        /// <param name="docPriority"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public async Task<BusinessResult> OutDownRecordForDatabase(outRecordForPalletDto input)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "物料出库:";
            using (var tran = await ((DbContext)DC).Database.BeginTransactionAsync())
            {
                try
                {

                    var stockInfo = await ((DbContext)DC).Set<WmsStock>().Where(t => t.stockCode == input.stockCode && t.palletBarcode == input.palletBarcode && t.stockStatus == 50).FirstOrDefaultAsync();
                    if (stockInfo == null)
                    {
                        return result.Error($"未找到托盘【{input.palletBarcode}】可用库存；");
                    }
                    var regionInfo = await ((DbContext)DC).Set<BasWRegion>().Where(t => t.regionNo == stockInfo.regionNo).FirstOrDefaultAsync();
                    var stockDtlInfos = await ((DbContext)DC).Set<WmsStockDtl>().Where(t => t.palletBarcode == input.palletBarcode && t.stockCode == stockInfo.stockCode).ToListAsync();
                    if (stockDtlInfos.Count == 0)
                    {
                        return result.Error($"未找到托盘【{input.palletBarcode}】可用库存明细；");
                    }

                    var taskInfo = await ((DbContext)DC).Set<WmsTask>().Where(t => t.palletBarcode == input.palletBarcode && t.taskStatus < 90).FirstOrDefaultAsync();
                    if (taskInfo != null)
                    {
                        return result.Error($"托盘【{input.palletBarcode}】存在未完成任务【{taskInfo.wmsTaskNo}】；");
                    }

                    var putdownInfo = await ((DbContext)DC).Set<WmsPutdown>().Where(t => t.palletBarcode == input.palletBarcode && t.stockCode == input.stockCode && t.putdownStatus == 0).FirstOrDefaultAsync();
                    if (putdownInfo == null)
                    {
                        //todo:是否生成下架单
                        return result.Error($"未找到托盘【{input.palletBarcode}】对应下架单");
                    }
                    var putdownDtlInfos = await ((DbContext)DC).Set<WmsPutdownDtl>().Where(t => t.putdownNo == putdownInfo.putdownNo && t.palletBarcode == input.palletBarcode && t.stockCode == input.stockCode).ToListAsync();
                    string locNo = input.targetLocNo;
                    if (string.IsNullOrEmpty(locNo))
                    {
                        result = await GetOutLoc(input, input.businessCode);
                        if (result.code == ResCode.Error)
                        {
                            return result;
                        }
                        locNo = result.outParams.ToString();
                    }
                    #region 库存占用
                    //占用库存
                    stockInfo.stockStatus = 70;
                    stockInfo.UpdateBy = input.invoker;
                    stockInfo.UpdateTime = DateTime.Now;

                    stockDtlInfos.ForEach(t =>
                    {
                        //t.occupyQty = t.qty;
                        t.stockDtlStatus = 70;
                        t.UpdateBy = input.invoker;
                        t.UpdateTime = DateTime.Now;
                    });
                    #endregion

                    #region 业务类型
                    if (input.businessCode == "OUT")
                    {
                        #region 记录更新
                        var recordInfos = await DC.Set<WmsOutInvoiceRecord>().Where(t => t.palletBarcode == input.palletBarcode && t.stockCode == input.stockCode && t.outRecordStatus == 0).ToListAsync();
                        recordInfos.ForEach(t =>
                        {
                            t.outRecordStatus = 31;
                            t.deliveryLocNo = locNo;
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        #endregion
                        #region 单据明细和单据
                        var docDtlIdList = recordInfos.Select(t => t.invoiceDtlId).ToList();
                        var docNoList = recordInfos.Select(t => t.invoiceNo).ToList();

                        var docDtlInfos = await DC.Set<WmsOutInvoiceDtl>().Where(t => docDtlIdList.Contains(t.ID)).ToListAsync();
                        docDtlInfos.ForEach(t =>
                        {
                            if (t.invoiceDtlStatus >= 29)
                            {
                                t.invoiceDtlStatus = 31;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            }

                        });
                        var docInfos = await DC.Set<WmsOutInvoice>().Where(t => docNoList.Contains(t.invoiceNo)).ToListAsync();
                        docInfos.ForEach(t =>
                        {
                            t.invoiceStatus = 41;
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        #endregion
                        await ((DbContext)DC).Set<WmsOutInvoiceRecord>().BulkUpdateAsync(recordInfos);
                        await ((DbContext)DC).Set<WmsOutInvoiceDtl>().BulkUpdateAsync(docDtlInfos);
                        await ((DbContext)DC).Set<WmsOutInvoice>().BulkUpdateAsync(docInfos);
                    }
                    else if (input.businessCode == "MOVE")
                    {
                        #region 记录更新
                        var recordInfos = await DC.Set<WmsItnMoveRecord>().Where(t => t.frPalletBarcode == input.palletBarcode && t.frStockCode == input.stockCode && t.moveRecordStatus == 0).ToListAsync();
                        if (recordInfos.Count > 0)
                        {
                            recordInfos.ForEach(t =>
                            {
                                t.putDownLocNo = locNo;
                                t.moveRecordStatus = 41;
                                t.UpdateBy = input.invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            await ((DbContext)DC).Set<WmsItnMoveRecord>().BulkUpdateAsync(recordInfos);
                        }
                        #endregion
                        #region 单据明细和单据
                        // var docDtlIdList = recordInfos.Select(t => t.moveDtlId).ToList();
                        var docDtlInfos = await DC.Set<WmsItnMoveDtl>().Where(t => t.palletBarcode == input.palletBarcode && t.stockCode == input.stockCode && t.moveDtlStatus < 90).ToListAsync();
                        docDtlInfos.ForEach(t =>
                        {
                            t.moveDtlStatus = 41;
                            t.updateBy = input.invoker;
                            t.updateTime = DateTime.Now;
                        });
                        var docNoList = docDtlInfos.Select(t => t.moveNo).ToList();
                        var docInfos = await DC.Set<WmsItnMove>().Where(t => docNoList.Contains(t.moveNo)).ToListAsync();
                        docInfos.ForEach(t =>
                        {
                            t.moveStatus = 41;
                            t.putdownLocNo = locNo;
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        #endregion

                        await ((DbContext)DC).Set<WmsItnMoveDtl>().BulkUpdateAsync(docDtlInfos);
                        await ((DbContext)DC).Set<WmsItnMove>().BulkUpdateAsync(docInfos);
                    }
                    else if (input.businessCode == "QC")
                    {
                        #region 记录更新
                        var recordInfos = await DC.Set<WmsItnQcRecord>().Where(t => t.palletBarcode == input.palletBarcode && t.stockCode == input.stockCode && t.itnQcStatus == 0).ToListAsync();
                        recordInfos.ForEach(t =>
                        {
                            t.itnQcStatus = 31;
                            t.itnQcLocNo = locNo;
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        #endregion
                        #region 单据明细和单据
                        var docDtlIdList = recordInfos.Select(t => t.itnQcDtlId).ToList();
                        var docNoList = recordInfos.Select(t => t.itnQcNo).ToList();

                        var docDtlInfos = await DC.Set<WmsItnQcDtl>().Where(t => docDtlIdList.Contains(t.ID)).ToListAsync();
                        docDtlInfos.ForEach(t =>
                        {
                            t.itnQcDtlStatus = 51;
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        var docInfos = await DC.Set<WmsItnQc>().Where(t => docNoList.Contains(t.itnQcNo)).ToListAsync();
                        docInfos.ForEach(t =>
                        {
                            t.itnQcStatus = 51;
                            t.itnQcLocNo = locNo;
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        #endregion
                        await ((DbContext)DC).Set<WmsItnQcRecord>().BulkUpdateAsync(recordInfos);
                        await ((DbContext)DC).Set<WmsItnQcDtl>().BulkUpdateAsync(docDtlInfos);
                        await ((DbContext)DC).Set<WmsItnQc>().BulkUpdateAsync(docInfos);
                    }
                    else if (input.businessCode == "INVENTORY")
                    {
                        #region 记录更新
                        var recordInfos = await DC.Set<WmsItnInventoryRecord>().Where(t => t.palletBarcode == input.palletBarcode && t.stockCode == input.stockCode && t.inventoryRecordStatus == 0).ToListAsync();
                        recordInfos.ForEach(t =>
                        {
                            t.inventoryRecordStatus = 31;
                            t.putdownLocNo = locNo;
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        #endregion
                        #region 单据明细和单据
                        var docDtlIdList = recordInfos.Select(t => t.inventoryDtlId).ToList();
                        var docNoList = recordInfos.Select(t => t.inventoryNo).ToList();

                        var docDtlInfos = await DC.Set<WmsItnInventoryDtl>().Where(t => docDtlIdList.Contains(t.ID)).ToListAsync();
                        docDtlInfos.ForEach(t =>
                        {
                            t.inventoryDtlStatus = 51;
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        var docInfos = await DC.Set<WmsItnInventory>().Where(t => docNoList.Contains(t.inventoryNo)).ToListAsync();
                        docInfos.ForEach(t =>
                        {
                            t.inventoryStatus = 51;
                            t.inventoryLocNo = locNo;
                            t.UpdateBy = input.invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        #endregion
                        await ((DbContext)DC).Set<WmsItnInventoryRecord>().BulkUpdateAsync(recordInfos);
                        await ((DbContext)DC).Set<WmsItnInventoryDtl>().BulkUpdateAsync(docDtlInfos);
                        await ((DbContext)DC).Set<WmsItnInventory>().BulkUpdateAsync(docInfos);
                    }


                    #endregion

                    #region 下架单
                    putdownInfo.putdownStatus = 31;
                    putdownInfo.targetLocNo = locNo;
                    putdownInfo.UpdateBy = input.invoker;
                    putdownInfo.UpdateTime = DateTime.Now;
                    putdownDtlInfos.ForEach(t =>
                    {
                        t.putdownDtlStatus = 31;
                        t.UpdateBy = input.invoker;
                        t.UpdateTime = DateTime.Now;
                    });

                    #endregion

                    #region 任务
                    #region 生成任务
                    SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
                    WmsStockVM wmsStockVM = Wtm.CreateVM<WmsStockVM>();
                    BasWBinVM basWBinVM = Wtm.CreateVM<BasWBinVM>();
                    string wmstaskNo = sysSequenceVM.GetSequence(SequenceCode.wmsTaskNo.GetCode());
                    string wmsTaskType = input.palletPickType == 0 ? "WHOLE_OUT" : "PICK_OUT";
                    WmsTask wmsTask = BuildOutWmsTask(wmstaskNo, stockInfo, locNo, wmsTaskType, "OUT", input.invoker, input.docPriority);
                    #endregion
                    #region 生成指令
                    string CmdNo= sysSequenceVM.GetSequence(SequenceCode.srmCmdNo.GetCode());
                    string SrmNo = "SRM" + stockInfo.roadwayNo.Substring(stockInfo.roadwayNo.Length - 2);
                    string SerialNo= sysSequenceVM.GetSequence(SrmNo);
                    BasWBin basWBin =await basWBinVM.GetBinByBinNoAsync(stockInfo.binNo);
                    SrmCmd srmCmd = BuildOutSrmCmd(CmdNo, SrmNo, wmsTask, Convert.ToInt16(SerialNo), basWBin, "OUT", input.invoker);
                    #endregion

                    #endregion

                    await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(stockInfo);
                    await ((DbContext)DC).Set<WmsStockDtl>().BulkUpdateAsync(stockDtlInfos);

                    await ((DbContext)DC).Set<WmsPutdown>().SingleUpdateAsync(putdownInfo);
                    await ((DbContext)DC).Set<WmsPutdownDtl>().BulkUpdateAsync(putdownDtlInfos);
                    await ((DbContext)DC).Set<WmsTask>().AddAsync(wmsTask);
                    //生成指令
                    await ((DbContext)DC).Set<SrmCmd>().AddAsync(srmCmd);
                    BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                    await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                    await tran.CommitAsync();
                    result.code = ResCode.OK;
                    result.msg = $"托盘【{input.palletBarcode}】任务生成成功；";
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    return result.Error($"{ex.Message};");
                }
            }
            return result;
        }
        public async Task<BusinessResult> GetOutLoc(outRecordForPalletDto input, string bussinessCode)
        {
            BusinessResult result = new BusinessResult();
            string locNo = string.Empty;
            string locGroupType = string.Empty;
            if (bussinessCode == "OUT")
            {
                if (input.palletPickType == 0)
                {
                    locGroupType = "WHOLE_OUT";
                }
                else
                {
                    locGroupType = "PICK_OUT";
                }
                WmsOutInvoiceRecord outRecordInfo = null;
                if (string.IsNullOrWhiteSpace(input.waveNo))
                {
                    outRecordInfo = await DC.Set<WmsOutInvoiceRecord>().Where(t => t.invoiceNo == input.docNo && t.palletBarcode != input.palletBarcode && t.palletBarcode.Contains(input.palletPrefix) && t.palletPickType == input.palletPickType && !string.IsNullOrWhiteSpace(t.deliveryLocNo) && t.outRecordStatus > 0 && t.outRecordStatus < 39).FirstOrDefaultAsync();

                }
                else
                {
                    outRecordInfo = await DC.Set<WmsOutInvoiceRecord>().Where(t => t.waveNo == input.waveNo && t.palletBarcode != input.palletBarcode && t.palletBarcode.Contains(input.palletPrefix) && t.palletPickType == input.palletPickType && !string.IsNullOrWhiteSpace(t.deliveryLocNo) && t.outRecordStatus > 0 && t.outRecordStatus < 39).FirstOrDefaultAsync();
                }
                if (outRecordInfo != null)
                {
                    locNo = outRecordInfo.deliveryLocNo;
                }
            }
            else if (bussinessCode == "MOVE")
            {
                locGroupType = bussinessCode;
                var recordInfo = await DC.Set<WmsItnMoveRecord>().Where(t => t.moveNo == input.docNo && t.frPalletBarcode != input.palletBarcode && t.frPalletBarcode.Contains(input.palletPrefix) && !string.IsNullOrWhiteSpace(t.putDownLocNo) && t.moveRecordStatus > 0 && t.moveRecordStatus <= 49).FirstOrDefaultAsync();
                if (recordInfo != null)
                {
                    locNo = recordInfo.putDownLocNo;
                }
            }
            else if (bussinessCode == "QC")
            {
                locGroupType = bussinessCode;
                var recordInfo = await DC.Set<WmsItnQcRecord>().Where(t => t.itnQcNo == input.docNo && t.palletBarcode != input.palletBarcode && t.palletBarcode.Contains(input.palletPrefix) && !string.IsNullOrWhiteSpace(t.itnQcLocNo) && t.itnQcStatus > 0 && t.itnQcStatus <= 39).FirstOrDefaultAsync();
                if (recordInfo != null)
                {
                    locNo = recordInfo.itnQcLocNo;
                }
            }
            else if (bussinessCode == "INVENTORY")
            {
                locGroupType = bussinessCode;
                var recordInfo = await DC.Set<WmsItnInventoryRecord>().Where(t => t.inventoryNo == input.docNo && t.palletBarcode != input.palletBarcode && t.palletBarcode.Contains(input.palletPrefix) && !string.IsNullOrWhiteSpace(t.putdownLocNo) && t.inventoryRecordStatus > 0 && t.inventoryRecordStatus <= 49).FirstOrDefaultAsync();
                if (recordInfo != null)
                {
                    locNo = recordInfo.putdownLocNo;
                }
            }

            if (string.IsNullOrWhiteSpace(locNo))
            {

                List<string> loclist = new List<string>();
                var locGroupInfo = await DC.Set<BasWLocGroup>().Where(t => t.locGroupType == input.regionNo && t.usedFlag == 1).ToListAsync();
                if (locGroupInfo.Count == 0)
                {
                    return result.Error($"未配置【{locGroupType}】类型站台组");
                }
                loclist = locGroupInfo.Select(t => t.locGroupNo).ToList();
                if ("2014".Equals(input.docTypeCode))
                {
                    var locInfos = await DC.Set<BasWLoc>().Where(t => loclist.Contains(t.locGroupNo) && t.locTypeCode == "TICKET_OUT" && t.usedFlag == 1).ToListAsync();
                    if (locInfos.Any())
                    {
                        loclist = locInfos.Select(t => t.locNo).ToList();
                    }
                }
                else
                {
                    var locInfos = await DC.Set<BasWLoc>().Where(t => loclist.Contains(t.locGroupNo) && t.locTypeCode == "UN_TICKET_OUT" && t.usedFlag == 1).ToListAsync();
                    if (locInfos.Any())
                    {
                        loclist = locInfos.Select(t => t.locNo).ToList();
                    }
                }



                //List<string> loclist = new List<string>();
                //var locInfos = await DC.Set<BasWLoc>().Where(t =>t.locGroupNo.Contains(input.regionNo) && t.locTypeCode == input.businessCode && t.usedFlag==1).ToListAsync();
                //if (locInfos.Count > 0)
                //{
                //    loclist = locInfos.Select(t => t.locNo).ToList();
                //}
                //else
                //{
                //    var locGroupInfo = await DC.Set<BasWLocGroup>().Where(t => t.locGroupType == input.regionNo && t.usedFlag==1).ToListAsync();
                //    if (locGroupInfo.Count==0)
                //    {
                //        return result.Error($"未配置【{locGroupType}】类型站台组");
                //    }
                //    loclist = locGroupInfo.Select(t => t.locGroupNo).ToList();
                //}
                //根据任务数量
                var taskInfos = await DC.Set<WmsTask>().Where(t => loclist.Contains(t.toLocationNo) && t.taskStatus < 90).ToListAsync();
                if (taskInfos.Count > 0)
                {
                    List<LocGroupAndTaskDto> locGroupAndTaskList = new List<LocGroupAndTaskDto>();
                    //var groupTask = taskInfos.GroupBy(t => t.toLocationNo).Min(t => t.Count());
                    var groupTask = taskInfos.GroupBy(t => t.toLocationNo);
                    foreach (var item in groupTask)
                    {
                        LocGroupAndTaskDto locGroupAndTask = new LocGroupAndTaskDto()
                        {
                            locGroupNo = item.Key,
                            taskCount = item.Count()
                        };
                        locGroupAndTaskList.Add(locGroupAndTask);
                    }

                    locNo = locGroupAndTaskList.OrderBy(t => t.taskCount).FirstOrDefault().locGroupNo;
                }
                else
                {
                    locNo = loclist.FirstOrDefault();
                }
            }
            if (string.IsNullOrWhiteSpace(locNo))
            {
                return result.Error($"未找到可用站台");
            }
            result.outParams = locNo;
            return result;
        }


        #region 巷道内移库
        public async Task<BusinessResult> MoveHandle(WmsStock moveStock, string invoker)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "巷道内移库:";
            try
            {
                var binInfo = await DC.Set<BasWBin>().Where(t => t.binNo == moveStock.binNo && t.binType == "ST" && t.virtualFlag == 0).FirstOrDefaultAsync();
                if (binInfo == null)
                {
                    msg = $"{desc}移库起始库位【{moveStock.binNo}】未查询到，请先配置数据";
                    return result.Error(msg);
                }
                var docTypeInfo = await DC.Set<CfgDocType>().AsNoTracking().Where(t => t.businessCode == "MOVE" && t.docTypeCode == "5001").FirstOrDefaultAsync();
                if (docTypeInfo == null)
                {
                    msg = $"{desc}未找到业务类型是移库的单据类型，请先配置数据";
                    return result.Error(msg);
                }
                if (moveStock.stockStatus > 50)
                {
                    msg = $"{desc}待移库托盘{moveStock.palletBarcode}状态不是在库，无需移库";
                    return result.Error(msg);
                }
                var stockDtlInfos = await ((DbContext)DC).Set<WmsStockDtl>().Where(t => t.stockDtlStatus == 50 && t.stockCode == moveStock.stockCode).ToListAsync();
                if (stockDtlInfos.Count == 0)
                {
                    msg = $"{desc}待移库托盘{moveStock.palletBarcode}未找到在库库存明细，无需移库";
                    return result.Error(msg);
                }
                var stockDtlInfo = stockDtlInfos.FirstOrDefault(t => t.occupyQty > 0);
                if (stockDtlInfo != null)
                {
                    msg = $"{desc}待移库托盘{moveStock.palletBarcode}对应库存明细【{stockDtlInfo.ID}】存在占用数量，无需移库";
                    return result.Error(msg);
                }

                var taskInfo = await DC.Set<WmsTask>().Where(t => t.palletBarcode == moveStock.palletBarcode && t.taskStatus < 90).FirstOrDefaultAsync();
                if (taskInfo != null)
                {
                    msg = $"{desc}待移库托盘{moveStock.palletBarcode}存在未完成任务【{taskInfo.wmsTaskNo}】，无需移库";
                    return result.Error(msg);
                }
                //分配库位
                #region 分配库位
                BasWBinVM basWBinVM = Wtm.CreateVM<BasWBinVM>();
                AllotBinInputDto allotBinInput = new AllotBinInputDto()
                {
                    palletBarcode = moveStock.palletBarcode,
                    roadwayNos = moveStock.roadwayNo,
                    height = moveStock.height,
                    layer = binInfo.binLayer,
                    isMove = true,
                };
                result = await basWBinVM.AllotBin(allotBinInput);
                if (result.code == ResCode.Error)
                {
                    msg = $"{desc}待移库托盘{moveStock.palletBarcode}分配移库目标库位失败，{result.msg}";
                    return result.Error(msg);
                }
                if (result.outParams == null)
                {
                    msg = $"{desc}待移库托盘{moveStock.palletBarcode}分配移库目标库位失败，返回结果为空";
                    return result.Error(msg);
                }
                AllotBinResultDto allotBinResult = (AllotBinResultDto)result.outParams;

                #endregion
                var stockUniiInfo = await ((DbContext)DC).Set<WmsStockUniicode>().Where(t => t.palletBarcode == moveStock.palletBarcode && t.stockCode == moveStock.stockCode).ToListAsync();
                #region 创建移库库存
                result = await MoveHandleForDataBase(docTypeInfo, moveStock, stockDtlInfos, stockUniiInfo, allotBinResult, invoker);
                #endregion


            }
            catch (Exception e)
            {
                msg = $"{desc}" + e.Message;
                result.msg = msg;
                result.code = ResCode.Error;
            }
            msg = $"时间【{DateTime.Now}】,操作人【{invoker}】:托盘【{moveStock.palletBarcode}】生成移库任务：{result.msg}";
            logger.Warn($"----->Warn----->WMS移库任务生成:{msg} ");
            return result;
        }
        /// <summary>
        /// 生成入库中的库存明细
        /// </summary>
        /// <param name="docTypeInfo"></param>
        /// <param name="moveStock"></param>
        /// <param name="moveStockDtls"></param>
        /// <param name="moveStockUniis"></param>
        /// <param name="allotBinResult"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public BusinessResult MoveHandleForDataBase2(CfgDocType docTypeInfo, WmsStock moveStock, List<WmsStockDtl> moveStockDtls, List<WmsStockUniicode> moveStockUniis, AllotBinResultDto allotBinResult, string invoker)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "物料巷道内移库:";
            using (var tran = ((DbContext)DC).Database.BeginTransaction())
            {
                try
                {
                    var stockInfo = DC.Set<WmsStock>().Where(t => t.binNo == allotBinResult.binNo).FirstOrDefault();
                    if (stockInfo != null)
                    {
                        msg = $"{desc}待移库托盘{moveStock.palletBarcode}分配移库目标库位失败，分配库位【{allotBinResult.binNo}】已存在库存";
                        return result.Error(msg);
                    }

                    //更新库存
                    #region 更新在库库存为出库
                    moveStock.stockStatus = 70;
                    moveStock.UpdateBy = invoker;
                    moveStock.UpdateTime = DateTime.Now;

                    moveStockDtls.ForEach(t =>
                    {
                        t.occupyQty = t.qty;
                        t.stockDtlStatus = 70;
                        t.UpdateBy = invoker;
                        t.UpdateTime = DateTime.Now;
                    });
                    #endregion

                    #region 生成移库入库中库存和移库记录
                    SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStock, WmsStock>());
                    var mapper = config.CreateMapper();
                    WmsStock newStock = mapper.Map<WmsStock>(moveStock);
                    newStock.stockCode = sysSequenceVM.GetSequence(SequenceCode.StockCode.GetCode());
                    newStock.binNo = allotBinResult.binNo;
                    newStock.stockStatus = 20;
                    newStock.CreateBy = invoker;
                    newStock.UpdateBy = invoker;
                    newStock.CreateTime = DateTime.Now;
                    newStock.UpdateTime = DateTime.Now;

                    List<WmsStockDtl> newStockDtls = new List<WmsStockDtl>();
                    List<WmsStockUniicode> newStockUniis = new List<WmsStockUniicode>();
                    List<WmsItnMoveRecord> newItnMoveRecords = new List<WmsItnMoveRecord>();
                    foreach (var dtl in moveStockDtls)
                    {
                        config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStockDtl, WmsStockDtl>());
                        mapper = config.CreateMapper();
                        WmsStockDtl newStockDtl = mapper.Map<WmsStockDtl>(dtl);
                        newStockDtl.stockCode = newStock.stockCode;
                        newStockDtl.stockDtlStatus = newStock.stockStatus;
                        newStockDtl.occupyQty = 0;
                        newStockDtl.CreateBy = invoker;
                        newStockDtl.UpdateBy = invoker;
                        newStockDtl.CreateTime = DateTime.Now;
                        newStockDtl.UpdateTime = DateTime.Now;
                        newStockDtls.Add(newStockDtl);
                        if (moveStock.loadedType == 0)
                        {
                            //移库记录
                            WmsItnMoveRecord wmsItnMoveRecord = CreateMoveRecord(docTypeInfo.docTypeCode, dtl, newStockDtl, moveStock, newStock);
                            newItnMoveRecords.Add(wmsItnMoveRecord);
                        }
                        else
                        {
                            var moveStockUniiForDtls = moveStockUniis.Where(t => t.stockDtlId == dtl.ID).ToList();
                            if (moveStockUniiForDtls.Any())
                            {
                                foreach (var unii in moveStockUniiForDtls)
                                {
                                    config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStockUniicode, WmsStockUniicode>());
                                    mapper = config.CreateMapper();
                                    WmsStockUniicode newStockUnii = mapper.Map<WmsStockUniicode>(unii);
                                    newStockUnii.stockCode = newStock.stockCode;
                                    newStockUnii.stockDtlId = newStockDtl.ID;
                                    newStockUnii.CreateBy = invoker;
                                    newStockUnii.UpdateBy = invoker;
                                    newStockUnii.CreateTime = DateTime.Now;
                                    newStockUnii.UpdateTime = DateTime.Now;
                                    newStockUniis.Add(newStockUnii);
                                    //移库记录
                                    WmsItnMoveRecord wmsItnMoveRecord = CreateMoveRecord(docTypeInfo.docTypeCode, unii, newStockUnii, moveStock, newStock);
                                    newItnMoveRecords.Add(wmsItnMoveRecord);
                                }
                            }
                        }

                    }

                    #region 生成任务
                    WmsStockVM wmsStockVM = Wtm.CreateVM<WmsStockVM>();
                    string wmstaskNo = sysSequenceVM.GetSequence(SequenceCode.wmsTaskNo.GetCode());
                    WmsTask wmsTask = BuildMoveWmsTask(wmstaskNo, moveStock, newStock, docTypeInfo.businessCode, "MOVE", invoker, docTypeInfo.docPriority ?? 99);
                    #endregion

                    ((DbContext)DC).Set<WmsStock>().Update(moveStock);
                    ((DbContext)DC).Set<WmsStockDtl>().UpdateRange(moveStockDtls);
                    ((DbContext)DC).Set<WmsStock>().Add(newStock);
                    ((DbContext)DC).Set<WmsStockDtl>().AddRange(newStockDtls);
                    if (newStockUniis.Any())
                        ((DbContext)DC).Set<WmsStockUniicode>().AddRange(newStockUniis);
                    if (newItnMoveRecords.Any())
                        ((DbContext)DC).Set<WmsItnMoveRecord>().AddRange(newItnMoveRecords);
                    ((DbContext)DC).Set<WmsTask>().Add(wmsTask);
                    BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                    ((DbContext)DC).BulkSaveChanges(t => t = option);
                    tran.Commit();
                    result.code = ResCode.OK;
                    result.msg = $"托盘【{moveStock.palletBarcode}】移库任务生成成功；";

                    #endregion
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return result.Error($"{ex.Message};");
                }
                return result;
            }
        }
        /// <summary>
        /// 不 生成入库中的库存明细
        /// </summary>
        /// <param name="docTypeInfo"></param>
        /// <param name="moveStock"></param>
        /// <param name="moveStockDtls"></param>
        /// <param name="moveStockUniis"></param>
        /// <param name="allotBinResult"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public async Task<BusinessResult> MoveHandleForDataBase(CfgDocType docTypeInfo, WmsStock moveStock, List<WmsStockDtl> moveStockDtls, List<WmsStockUniicode> moveStockUniis, AllotBinResultDto allotBinResult, string invoker)
        {
            BusinessResult result = new BusinessResult();
            string msg = string.Empty;
            string desc = "物料巷道内移库:";
            using (var tran = ((DbContext)DC).Database.BeginTransaction())
            {
                try
                {
                    var stockInfo = await DC.Set<WmsStock>().Where(t => t.binNo == allotBinResult.binNo).FirstOrDefaultAsync();
                    if (stockInfo != null)
                    {
                        msg = $"{desc}待移库托盘{moveStock.palletBarcode}分配移库目标库位失败，分配库位【{allotBinResult.binNo}】已存在库存";
                        return result.Error(msg);
                    }

                    //更新库存
                    #region 更新在库库存为出库
                    moveStock.stockStatus = 70;
                    moveStock.UpdateBy = invoker;
                    moveStock.UpdateTime = DateTime.Now;

                    moveStockDtls.ForEach(t =>
                    {
                        t.occupyQty = t.qty;
                        // t.stockDtlStatus = 70;
                        t.UpdateBy = invoker;
                        t.UpdateTime = DateTime.Now;
                    });
                    moveStockUniis.ForEach(t =>
                    {
                        t.occupyQty = t.qty;
                        t.UpdateBy = invoker;
                        t.UpdateTime = DateTime.Now;
                    });
                    #endregion

                    #region 生成移库入库中库存和移库记录
                    SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStock, WmsStock>());
                    var mapper = config.CreateMapper();
                    WmsStock newStock = mapper.Map<WmsStock>(moveStock);
                    newStock.stockCode = sysSequenceVM.GetSequence(SequenceCode.StockCode.GetCode());
                    newStock.binNo = allotBinResult.binNo;
                    newStock.stockStatus = 20;
                    newStock.CreateBy = invoker;
                    newStock.UpdateBy = invoker;
                    newStock.CreateTime = DateTime.Now;
                    newStock.UpdateTime = DateTime.Now;

                    //List<WmsStockDtl> newStockDtls = new List<WmsStockDtl>();
                    //List<WmsStockUniicode> newStockUniis = new List<WmsStockUniicode>();
                    List<WmsItnMoveRecord> newItnMoveRecords = new List<WmsItnMoveRecord>();
                    foreach (var dtl in moveStockDtls)
                    {
                        if (moveStock.loadedType == 0)
                        {
                            //移库记录
                            WmsItnMoveRecord wmsItnMoveRecord = CreateMoveRecord(docTypeInfo.docTypeCode, dtl, moveStock, newStock, invoker);
                            newItnMoveRecords.Add(wmsItnMoveRecord);
                        }
                        else
                        {
                            var moveStockUniiForDtls = moveStockUniis.Where(t => t.stockDtlId == dtl.ID).ToList();
                            if (moveStockUniiForDtls.Any())
                            {
                                foreach (var unii in moveStockUniiForDtls)
                                {
                                    //移库记录
                                    WmsItnMoveRecord wmsItnMoveRecord = CreateMoveRecord(docTypeInfo.docTypeCode, unii, moveStock, newStock, invoker);
                                    newItnMoveRecords.Add(wmsItnMoveRecord);
                                }
                            }
                        }

                    }

                    #region 生成任务
                    WmsStockVM wmsStockVM = Wtm.CreateVM<WmsStockVM>();
                    string wmstaskNo = sysSequenceVM.GetSequence(SequenceCode.wmsTaskNo.GetCode());
                    WmsTask wmsTask = BuildMoveWmsTask(wmstaskNo, moveStock, newStock, docTypeInfo.businessCode, "MOVE", invoker, docTypeInfo.docPriority ?? 99);
                    #endregion

                    await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(moveStock);
                    await ((DbContext)DC).Set<WmsStockDtl>().BulkUpdateAsync(moveStockDtls);
                    await ((DbContext)DC).Set<WmsStockUniicode>().BulkUpdateAsync(moveStockUniis);
                    await ((DbContext)DC).Set<WmsStock>().AddAsync(newStock);
                    if (newItnMoveRecords.Any())
                        await ((DbContext)DC).Set<WmsItnMoveRecord>().AddRangeAsync(newItnMoveRecords);
                    await ((DbContext)DC).Set<WmsTask>().AddAsync(wmsTask);
                    BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                    await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                    tran.Commit();
                    result.code = ResCode.OK;
                    result.msg = $"托盘【{moveStock.palletBarcode}】移库任务生成成功；";

                    #endregion
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return result.Error($"{ex.Message};");
                }
                return result;
            }
        }
        #endregion


    }
}
