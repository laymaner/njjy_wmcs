using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;
using log4net;
using AutoMapper;
using Com.Wish.Model.Business;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using WISH.Helper.Common;
using Z.BulkOperations;
using Wish.ViewModel.Common.Dtos;
using Wish.ViewModel.System.SysSequenceVMs;
using WISH.Helper.Common.Dictionary.DictionaryHelper;


namespace Wish.ViewModel.BasWhouse.BasWBinVMs
{
    public partial class BasWBinVM
    {
        /// <summary>
        /// 启用禁用货位
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BusinessResult> OpenOrCloseBin(OpenOrCloseBinDto input)
        {
            BusinessResult result = new BusinessResult();
            string desc = string.Empty;
            if (input == null)
            {
                logger.Warn($"---->操作人：{input.invoker},库位号：【{input.binNo}】库位启用、禁用入参为空");
                return result.Error($"库位启用、禁用入参为空");
            }
            if (input.operationType == 10)
            {
                desc = "启用货位：";
            }
            else if (input.operationType == 11)
            {
                desc = "禁用货位：";
            }
            var inputJson = JsonConvert.SerializeObject(input);
            if (string.IsNullOrWhiteSpace(input.binNo))
            {
                logger.Warn($"---->{desc}，操作人：{input.invoker},库位号：{desc}库位编码为空");
                return result.Error($"{desc}库位编码为空");
            }
            try
            {
                BasWBin basWBin = await DC.Set<BasWBin>().Where(x => x.binNo == input.binNo).FirstOrDefaultAsync();
                if (basWBin == null)
                {
                    logger.Warn($"---->{desc}，操作人：{input.invoker},库位号：{desc}根据货位{input.binNo}查不到库位信息");
                    return result.Error($"{desc}根据货位{input.binNo}查不到库位信息");
                }
                if (input.operationType == 10)
                {
                    if (basWBin.usedFlag == 0)
                    {
                        basWBin.usedFlag = 1;
                        basWBin.isInEnable = 1;
                        basWBin.isOutEnable = 1;
                        basWBin.binErrFlag = "0";
                        basWBin.binErrMsg = "";
                        basWBin.UpdateBy = input.invoker;
                        basWBin.UpdateTime = DateTime.Now;
                        await ((DbContext)DC).Set<BasWBin>().SingleUpdateAsync(basWBin);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                    }
                }else if (input.operationType == 11)
                {
                    if (basWBin.usedFlag != 0)
                    {
                        basWBin.usedFlag = 0;
                        basWBin.isInEnable = 0;
                        basWBin.isOutEnable = 0;
                        basWBin.binErrFlag = "0";
                        basWBin.binErrMsg = $"用户{input.invoker}手动禁用";
                        basWBin.UpdateBy = input.invoker;
                        basWBin.UpdateTime = DateTime.Now;
                        await ((DbContext)DC).Set<BasWBin>().SingleUpdateAsync(basWBin);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                    }
                }
                
            }
            catch (Exception ex)
            {
                result.Error(ex.Message);
                logger.Warn($"---->{desc}，操作人：{input.invoker},库位号：{ex.Message}");
            }
            return result;
        }
        public async Task<BusinessResult> BinException(BinExceptionDto input, string invoker)
        {
            BusinessResult result = new BusinessResult();
            SysSequenceVM sysSequenceApiVM = Wtm.CreateVM<SysSequenceVM>();
            var hasParentTransaction = false;
            string _vpoint = "";
            var inputJson = JsonConvert.SerializeObject(input);
            result = ParamsValid(input);
            if (result.code == ResCode.OK)
            {
                try
                {
                    if (DC.Database.CurrentTransaction != null)
                    {
                        hasParentTransaction = true;
                    }
                    if (hasParentTransaction == false)
                    {
                        DC.Database.BeginTransaction();
                    }
                    #region 数据校验
                    _vpoint = $"查询库位信息";
                    string outBinNo = string.Empty;
                    WmsStock wmsStock = null;
                    BasWBin basWBin = await ((DbContext)DC).Set<BasWBin>().Where(x => x.binNo == input.binNo).FirstOrDefaultAsync();
                    if (basWBin == null)
                    {
                        throw new Exception($"库位：{input.binNo}，未找到库位信息。");
                    }
                    if (basWBin.virtualFlag == 1)
                    {
                        throw new Exception($"库位：{input.binNo}是虚拟库位。");
                    }
                    BasWBin basWBin1 = await ((DbContext)DC).Set<BasWBin>().Where(x => x.extensionIdx == 1 && x.extensionGroupNo == basWBin.extensionGroupNo).FirstOrDefaultAsync();

                    if (basWBin.binErrFlag == "0")
                    {
                        throw new Exception($"库位：{input.binNo}，库位无异常无需处理。");
                    }
                    else if (basWBin.binErrFlag == "11")
                    {
                        _vpoint = $"满入异常处理";
                        outBinNo = basWBin.binNo;
                    }
                    else if (basWBin.binErrFlag == "12")
                    {
                        if (basWBin1 == null)
                        {
                            throw new Exception($"库位：{input.binNo}未找到深位组{basWBin.extensionGroupNo}的近伸货位。");
                        }
                        wmsStock = await ((DbContext)DC).Set<WmsStock>().Where(x => x.binNo == basWBin1.binNo).FirstOrDefaultAsync();
                        if (wmsStock != null)
                        {
                            throw new Exception($"库位：{input.binNo}，近深位库位：{basWBin1.binNo}存在库存，请先移库。");
                        }
                        _vpoint = $"入库阻挡，不存在库位，异常出库";
                        outBinNo = basWBin1.binNo;
                    }
                    else if (basWBin.binErrFlag == "21")
                    {
                        _vpoint = $"空出异常处理";
                        wmsStock = await ((DbContext)DC).Set<WmsStock>().Where(x => x.binNo == basWBin.binNo).FirstOrDefaultAsync();
                        if (wmsStock == null)
                        {
                            input.operationType = 0;
                        }
                    }
                    else if (basWBin.binErrFlag == "22")
                    {
                        if (basWBin1 == null)
                        {
                            throw new Exception($"库位：{input.binNo}未找到深位组{basWBin.extensionGroupNo}的近伸货位。");
                        }
                        _vpoint = $"出库阻挡异常处理";
                        wmsStock = await ((DbContext)DC).Set<WmsStock>().Where(x => input.binNo == basWBin1.binNo).FirstOrDefaultAsync();
                        if (wmsStock != null)
                        {
                            _vpoint = $"出库阻挡异常处理,近伸存在库存，任务下发顺序问题，不需要处理，解除异常";
                            input.operationType = 0;
                        }
                        else
                        {
                            _vpoint = $"出库阻挡异常处理,近伸不存在库存，异常出库";
                            outBinNo = basWBin1.binNo;
                        }
                    }
                    else
                    {
                        throw new Exception($"库位：{input.binNo}，错误标记：【{basWBin.binErrFlag}】无法识别。");
                    }
                    #endregion

                    #region 处理逻辑
                    List<BasWBin> basWBinGroup = await ((DbContext)DC).Set<BasWBin>().Where(x => x.extensionGroupNo == basWBin.extensionGroupNo && x.regionNo == basWBin.regionNo).ToListAsync();
                    if (input.operationType == 0)
                    {
                        if (basWBin.binErrFlag == "11")
                        {
                            _vpoint = $"满入取消异常";//误报，直接更新当前库位异常状态
                            basWBin.binErrFlag = "0";
                            basWBin.binErrMsg = "满入取消异常";
                            basWBin.UpdateBy = invoker;
                            basWBin.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).Set<BasWBin>().SingleUpdateAsync(basWBin);
                            await ((DbContext)DC).BulkSaveChangesAsync();
                        }
                        else if (basWBin.binErrFlag == "12")
                        {
                            _vpoint = $"阻挡入库取消异常";//误报，直接更新当前库位伸位组库位异常状态
                            basWBinGroup.ForEach(t =>
                            {
                                t.binErrFlag = "0";
                                t.binErrMsg = "阻挡入库取消异常";
                                t.UpdateBy = invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            await ((DbContext)DC).Set<BasWBin>().BulkUpdateAsync(basWBinGroup);
                            await ((DbContext)DC).BulkSaveChangesAsync();
                        }
                        else if (basWBin.binErrFlag == "21")
                        {
                            _vpoint = $"空取取消异常";
                            basWBin.binErrFlag = "0";
                            basWBin.binErrMsg = "空取取消异常";
                            basWBin.UpdateBy = invoker;
                            basWBin.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).Set<BasWBin>().SingleUpdateAsync(basWBin);
                            await ((DbContext)DC).BulkSaveChangesAsync();
                        }
                        else if (basWBin.binErrFlag == "22")
                        {
                            _vpoint = $"阻挡出库取消异常";
                            basWBinGroup.ForEach(t =>
                            {
                                t.binErrFlag = "0";
                                t.binErrMsg = "阻挡出库取消异常";
                                t.UpdateBy = invoker;
                                t.UpdateTime = DateTime.Now;
                            });
                            await ((DbContext)DC).Set<BasWBin>().BulkUpdateAsync(basWBinGroup);
                            await ((DbContext)DC).BulkSaveChangesAsync();
                        }
                    }
                    else if (input.operationType == 1)
                    {
                        if (basWBin.binErrFlag == "11")
                        {
                            var locGroupInfo = await ((DbContext)DC).Set<BasWLocGroup>().Where(t => t.locGroupType == basWBin.regionNo).FirstOrDefaultAsync();
                            if (locGroupInfo == null)
                            {
                                throw new Exception($"库位：{input.binNo}，对应库区{basWBin.regionNo}未找到站台组。");
                            }
                            var taskInfo = await ((DbContext)DC).Set<WmsTask>().Where(t => t.frLocationNo == outBinNo && t.taskStatus < 90).FirstOrDefaultAsync();
                            if (taskInfo != null)
                            {
                                throw new Exception($"库位：{input.binNo}已存在任务");
                            }
                            _vpoint = $"满入确认异常--生成WMS任务";
                            WmsTask wmsTaskEntity = new WmsTask();
                            wmsTaskEntity.feedbackDesc = String.Empty;
                            wmsTaskEntity.feedbackStatus = 0;
                            wmsTaskEntity.frLocationNo = outBinNo;
                            wmsTaskEntity.frLocationType = 1;
                            wmsTaskEntity.loadedType = wmsStock?.loadedType == null ? 1 : wmsStock?.loadedType;
                            wmsTaskEntity.matHeight = wmsStock?.height;//高
                            wmsTaskEntity.matLength = null;//长
                            wmsTaskEntity.matQty = null;
                            wmsTaskEntity.matWeight = null;//重量
                            wmsTaskEntity.matWidth = null;//宽
                            wmsTaskEntity.orderNo = null;
                            wmsTaskEntity.palletBarcode = "99999999";
                            wmsTaskEntity.proprietorCode = "TZ";
                            wmsTaskEntity.regionNo = basWBin.regionNo;
                            wmsTaskEntity.roadwayNo = basWBin.roadwayNo;
                            wmsTaskEntity.stockCode = "99999";
                            wmsTaskEntity.taskDesc = "满入确认异常,初始创建";
                            wmsTaskEntity.taskPriority = 100;
                            wmsTaskEntity.taskStatus = 0;
                            wmsTaskEntity.taskTypeNo = "OUT";
                            wmsTaskEntity.toLocationNo = locGroupInfo.locGroupNo;
                            wmsTaskEntity.toLocationType = 0;
                            wmsTaskEntity.whouseNo = basWBin.whouseNo;
                            wmsTaskEntity.wmsTaskNo = await sysSequenceApiVM.GetSequenceAsync(SequenceCode.wmsTaskNo.GetCode());
                            wmsTaskEntity.wmsTaskType = "EXCEPTION_OUT";
                            wmsTaskEntity.CreateBy = invoker;
                            wmsTaskEntity.CreateTime = DateTime.Now;
                            wmsTaskEntity.UpdateBy = invoker;
                            wmsTaskEntity.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).Set<WmsTask>().AddAsync(wmsTaskEntity);
                            await ((DbContext)DC).BulkSaveChangesAsync();
                        }
                        else if (basWBin.binErrFlag == "12")
                        {
                            _vpoint = $"阻挡入库确认异常";
                            if (wmsStock.stockStatus > 0)
                            {
                                _vpoint = $"阻挡入库确认异常--存在库存，近伸库位解除异常，可以移库";
                                if (basWBin1 != null)
                                {
                                    basWBin1.binErrFlag = "0";
                                    basWBin1.binErrMsg = "存在库存，近伸库位解除异常，可以移库";
                                    basWBin1.UpdateBy = invoker;
                                    basWBin1.UpdateTime = DateTime.Now;
                                    await ((DbContext)DC).Set<BasWBin>().SingleUpdateAsync(basWBin1);
                                    await ((DbContext)DC).BulkSaveChangesAsync();
                                }

                            }
                            else
                            {
                                var locGroupInfo = await ((DbContext)DC).Set<BasWLocGroup>().Where(t => t.locGroupType == basWBin.regionNo).FirstOrDefaultAsync();
                                if (locGroupInfo == null)
                                {
                                    throw new Exception($"库位：{input.binNo}，对应库区{basWBin.regionNo}未找到站台组。");
                                }
                                var taskInfo = await ((DbContext)DC).Set<WmsTask>().Where(t => t.frLocationNo == outBinNo && t.taskStatus < 90).FirstOrDefaultAsync();
                                if (taskInfo != null)
                                {
                                    throw new Exception($"库位：{input.binNo}已存在任务");
                                }
                                _vpoint = $"阻挡入库确认异常--生成WMS任务";
                                WmsTask wmsTaskEntity = new WmsTask();
                                wmsTaskEntity.feedbackDesc = String.Empty;
                                wmsTaskEntity.feedbackStatus = 0;
                                wmsTaskEntity.frLocationNo = outBinNo;
                                wmsTaskEntity.frLocationType = 1;
                                wmsTaskEntity.loadedType = wmsStock?.loadedType == null ? 1 : wmsStock?.loadedType;
                                wmsTaskEntity.matHeight = wmsStock?.height;//高
                                wmsTaskEntity.matLength = null;//长
                                wmsTaskEntity.matQty = null;
                                wmsTaskEntity.matWeight = null;//重量
                                wmsTaskEntity.matWidth = null;//宽
                                wmsTaskEntity.orderNo = null;
                                wmsTaskEntity.palletBarcode = "99999999";
                                wmsTaskEntity.proprietorCode = "TZ";
                                wmsTaskEntity.regionNo = basWBin.regionNo;
                                wmsTaskEntity.roadwayNo = basWBin.roadwayNo;
                                wmsTaskEntity.stockCode = "99999";
                                wmsTaskEntity.taskDesc = "阻挡入库确认异常,初始创建";
                                wmsTaskEntity.taskPriority = 100;
                                wmsTaskEntity.taskStatus = 0;
                                wmsTaskEntity.taskTypeNo = "OUT";
                                wmsTaskEntity.toLocationNo = locGroupInfo.locGroupNo;
                                wmsTaskEntity.toLocationType = 0;
                                wmsTaskEntity.whouseNo = basWBin.whouseNo;
                                wmsTaskEntity.wmsTaskNo = await sysSequenceApiVM.GetSequenceAsync(SequenceCode.wmsTaskNo.GetCode());
                                wmsTaskEntity.wmsTaskType = "EXCEPTION_OUT";
                                wmsTaskEntity.CreateBy = invoker;
                                wmsTaskEntity.CreateTime = DateTime.Now;
                                wmsTaskEntity.UpdateBy = invoker;
                                wmsTaskEntity.UpdateTime = DateTime.Now;
                                await ((DbContext)DC).Set<WmsTask>().AddAsync(wmsTaskEntity);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                            }
                        }
                        else if (basWBin.binErrFlag == "21")
                        {
                            _vpoint = $"空取确认异常";
                            basWBin.binErrFlag = "0";
                            basWBin.binErrMsg = "空取确认异常";
                            basWBin.UpdateBy = invoker;
                            basWBin.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).Set<BasWBin>().SingleUpdateAsync(basWBin);
                            await ((DbContext)DC).BulkSaveChangesAsync();

                            #region 处理库存
                            if (wmsStock != null)
                            {

                                var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStock, WmsStockHis>());
                                var mapper = config.CreateMapper();
                                WmsStockHis stockHis = mapper.Map<WmsStockHis>(wmsStock);
                                await ((DbContext)DC).Set<WmsStock>().SingleDeleteAsync(wmsStock);
                                await ((DbContext)DC).Set<WmsStockHis>().AddAsync(stockHis);

                                config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStockDtl, WmsStockDtlHis>());
                                mapper = config.CreateMapper();
                                //库存明细
                                var wmsStockDtls = await ((DbContext)DC).Set<WmsStockDtl>().Where(x => x.stockCode == wmsStock.stockCode).ToListAsync();
                                List<WmsStockDtlHis> stockDtlHisList = mapper.Map<List<WmsStockDtlHis>>(wmsStockDtls);

                                await ((DbContext)DC).Set<WmsStockDtl>().BulkDeleteAsync(wmsStockDtls);
                                await ((DbContext)DC).Set<WmsStockDtlHis>().AddRangeAsync(stockDtlHisList);

                                var stockUniiInfos = await ((DbContext)DC).Set<WmsStockUniicode>().Where(t => t.stockCode == wmsStock.stockCode).ToListAsync();
                                if (stockUniiInfos.Count > 0)
                                {
                                    config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStockUniicode, WmsStockUniicodeHis>());
                                    mapper = config.CreateMapper();
                                    List<WmsStockUniicodeHis> stockUniiHisList = mapper.Map<List<WmsStockUniicodeHis>>(stockUniiInfos);

                                    await ((DbContext)DC).Set<WmsStockUniicode>().BulkDeleteAsync(stockUniiInfos);
                                    await ((DbContext)DC).Set<WmsStockUniicodeHis>().AddRangeAsync(stockUniiHisList);
                                }


                                //库存调整记录（新增）
                                WmsStockAdjust wmsStockAdjust = new WmsStockAdjust();
                                wmsStockAdjust.whouseNo = wmsStock.whouseNo; // 仓库号
                                wmsStockAdjust.proprietorCode = wmsStock.proprietorCode; // 货主
                                wmsStockAdjust.stockCode = wmsStock.stockCode; // 库存编码
                                wmsStockAdjust.palletBarcode = wmsStock.palletBarcode; // 载体条码
                                wmsStockAdjust.adjustType = "DEL"; // 调整类型;新增、修改、删除
                                wmsStockAdjust.adjustDesc = "空取确认，删除库存";
                                wmsStockAdjust.adjustOperate = "空取确认，删除库存"; // 调整操作
                                wmsStockAdjust.CreateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                                wmsStockAdjust.CreateTime = DateTime.Now;
                                if (wmsStockAdjust != null)
                                {
                                    await ((DbContext)DC).Set<WmsStockAdjust>().AddAsync(wmsStockAdjust);
                                }
                                BulkOperation option = new BulkOperation() { BatchSize = 2000, AllowConcurrency = true };
                                await ((DbContext)DC).BulkSaveChangesAsync(t => t = option);
                            }
                            #endregion
                        }
                        else if (basWBin.binErrFlag == "22")
                        {
                            var locGroupInfo = await ((DbContext)DC).Set<BasWLocGroup>().Where(t => t.locGroupType == basWBin.regionNo).FirstOrDefaultAsync();
                            if (locGroupInfo == null)
                            {
                                throw new Exception($"库位：{input.binNo}，对应库区{basWBin.regionNo}未找到站台组。");
                            }
                            var taskInfo = await ((DbContext)DC).Set<WmsTask>().Where(t => t.frLocationNo == outBinNo && t.taskStatus < 90).FirstOrDefaultAsync();
                            if (taskInfo != null)
                            {
                                throw new Exception($"库位：{input.binNo}已存在任务");
                            }
                            _vpoint = $"阻挡出库确认异常--生成WMS任务";//将阻挡库位的货物取出，生成任务
                            WmsTask wmsTaskEntity = new WmsTask();
                            wmsTaskEntity.feedbackDesc = String.Empty;
                            wmsTaskEntity.feedbackStatus = 0;
                            wmsTaskEntity.frLocationNo = outBinNo;
                            wmsTaskEntity.frLocationType = 1;
                            wmsTaskEntity.loadedType = wmsStock?.loadedType == null ? 0 : wmsStock?.loadedType;
                            wmsTaskEntity.matHeight = null;//高
                            wmsTaskEntity.matLength = null;//长
                            wmsTaskEntity.matQty = null;
                            wmsTaskEntity.matWeight = null;//重量
                            wmsTaskEntity.matWidth = null;//宽
                            wmsTaskEntity.orderNo = null;
                            wmsTaskEntity.palletBarcode = "99999999";
                            wmsTaskEntity.proprietorCode = "TZ";
                            wmsTaskEntity.regionNo = basWBin.regionNo;
                            wmsTaskEntity.roadwayNo = basWBin.roadwayNo;
                            wmsTaskEntity.stockCode = "99999";
                            wmsTaskEntity.taskDesc = "初始创建";
                            wmsTaskEntity.taskPriority = 100;
                            wmsTaskEntity.taskStatus = 0;
                            wmsTaskEntity.taskTypeNo = "OUT";
                            wmsTaskEntity.toLocationNo = locGroupInfo.locGroupNo;//todo站台
                            wmsTaskEntity.toLocationType = 0;
                            wmsTaskEntity.whouseNo = basWBin.whouseNo;
                            wmsTaskEntity.wmsTaskNo = await sysSequenceApiVM.GetSequenceAsync(SequenceCode.wmsTaskNo.GetCode());
                            wmsTaskEntity.wmsTaskType = "EXCEPTION_OUT";
                            wmsTaskEntity.CreateBy = invoker;
                            wmsTaskEntity.CreateTime = DateTime.Now;
                            wmsTaskEntity.UpdateBy = invoker;
                            wmsTaskEntity.UpdateTime = DateTime.Now;
                            await ((DbContext)DC).Set<WmsTask>().AddAsync(wmsTaskEntity);
                            await ((DbContext)DC).BulkSaveChangesAsync();
                        }
                    }
                    #endregion
                    if (hasParentTransaction == false)
                    {
                        await ((DbContext)DC).Database.CommitTransactionAsync();
                    }
                }
                catch (Exception ex)
                {
                    if (hasParentTransaction == false)
                    {
                        await ((DbContext)DC).Database.RollbackTransactionAsync();
                    }

                    result.code = ResCode.Error;
                    result.msg = $"---->库位异常处理BasWBinApiVM->BinException 执行异常! 异常位置: [ {_vpoint} ], 异常信息: [ {ex.Message} ]";
                }
            }
            string outJson = JsonConvert.SerializeObject(result);
            logger.Warn($"---->库位异常处理，操作人：{invoker},库位号：【{input.binNo}】,入参：【{inputJson}】,出参：【{outJson}】");
            return result;
        }
        private BusinessResult ParamsValid(BinExceptionDto input)
        {
            BusinessResult result = new BusinessResult();
            if (input == null)
            {
                result.code = ResCode.Error;
                result.msg = $"入参input为空。";
                return result;
            }
            if (string.IsNullOrWhiteSpace(input.binNo))
            {
                result.code = ResCode.Error;
                result.msg = $"入参binNo为空；";
            }
            if (input.operationType == null)
            {
                result.code = ResCode.Error;
                result.msg = result.msg + $"入参operationType为空；";
            }
            return result;
        }
    }
}
