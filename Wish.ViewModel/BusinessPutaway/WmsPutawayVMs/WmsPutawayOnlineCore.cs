using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using log4net;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using Wish.Areas.BasWhouse.Model;
using WISH.Helper.Common;
using Wish.ViewModel.BasWhouse.BasWPalletTypeVMs;
using Wish.ViewModel.Common.Dtos;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Wish.ViewModel.System.SysSequenceVMs;
using Wish.TaskConfig.Model;

namespace Wish.ViewModel.BusinessPutaway.WmsPutawayVMs
{
    public partial class WmsPutawayVM
    {
        /// <summary>
        /// 入库业务：上线
        /// </summary>
        /// <param name="input"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public async Task<BusinessResult> PutAwayOnline(PutAwayOnlineDto input, string invoker)
        {
            BusinessResult result = new BusinessResult();
            SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
            BasWPalletTypeVM basWPalletTypeVM = Wtm.CreateVM<BasWPalletTypeVM>();
            var hasParentTransaction = false;
            string _vpoint = "";
            var inputJson = JsonConvert.SerializeObject(input);
            result = await ParamsValid(input);
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
                        await DC.Database.BeginTransactionAsync();
                    }
                    var moveStock = await DC.Set<WmsStock>().Where(x => x.palletBarcode == input.palletBarcode).FirstOrDefaultAsync();
                    if (moveStock == null)
                    {
                        throw new Exception($"该托盘号{input.palletBarcode}查不到库存主表记录。");
                    }
                    var moveStockDtl = await DC.Set<WmsStockDtl>().Where(x => x.stockCode == moveStock.stockCode).ToListAsync();
                    if (!moveStockDtl.Any())
                    {
                        throw new Exception($"该托盘号{input.palletBarcode}，库存编码{moveStock.stockCode}，查不到库存明细记录。");
                    }
                    bool isMove = false;
                    var stockDtlStatus = moveStockDtl.Select(x => x.stockDtlStatus == 0).ToList();
                    if (moveStock.stockStatus == 0 && stockDtlStatus.Count > 0)
                    {
                        var wmsPutaway = await DC.Set<WmsPutaway>().Where(x => x.palletBarcode == input.palletBarcode).FirstOrDefaultAsync();
                        if (wmsPutaway == null)
                        {
                            //if (moveStock.regionNo == WRgionTypeCode.MOVE.GetCode())
                            //{
                            isMove = true;
                            //}
                        }
                    }
                    string toLocNo = "8010";
                    BasWPalletTypeDto basWPalletTypeView = await basWPalletTypeVM.GetPalletTypeAsync(input.palletBarcode);
                    if (basWPalletTypeView.palletType == PalletTypeExtend.UnKnown)
                    {
                        throw new Exception($"托盘{input.palletBarcode}规则不正确!");
                    }
                    if (basWPalletTypeView.basWPalletType.palletTypeCode == PalletTypeExtend.Box.GetCode())
                    {
                        toLocNo = "9010";
                    }
                    if (!isMove)
                    {
                        #region 入库逻辑

                        #region 通过托盘号获取上架单主表信息
                        _vpoint = $"通过托盘号获取上架单主表信息";
                        List<WmsPutaway> wmsPutaways = await DC.Set<WmsPutaway>().Where(x => x.palletBarcode == input.palletBarcode && x.putawayStatus == 0).ToListAsync();
                        if (wmsPutaways.Count > 1)
                        {
                            throw new Exception($"根据托盘号{input.palletBarcode}，查询到多条上架单主表。");
                        }
                        else if (wmsPutaways.Count == 0)
                        {
                            throw new Exception($"根据托盘号{input.palletBarcode}，查询不到上架单主表。");
                        }
                        var wmsPutAwayEntity = wmsPutaways.FirstOrDefault();

                        WmsTask wmsTask = await DC.Set<WmsTask>().Where(x => x.palletBarcode == input.palletBarcode).FirstOrDefaultAsync();
                        if (wmsTask != null)
                        {
                            throw new Exception($"该托盘号{input.palletBarcode}，已生成任务，任务号{wmsTask.wmsTaskNo}。");
                        }
                        #endregion

                        #region 校验是否维护在途库区/库位
                        _vpoint = $"校验是否维护在库库区/库位";
                        BasWRegion basWRegion = await DC.Set<BasWRegion>().Where(x => x.regionTypeCode == WRgionTypeCode.WI.GetCode() && x.areaNo == wmsPutAwayEntity.areaNo).FirstOrDefaultAsync();
                        if (basWRegion == null)
                        {
                            throw new Exception($"该区域{wmsPutAwayEntity.areaNo}，没有维护在途库区。");
                        }
                        BasWBin basWBin = await DC.Set<BasWBin>().Where(x => x.regionNo == basWRegion.regionNo).FirstOrDefaultAsync();
                        if (basWBin == null)
                        {
                            throw new Exception($"该区域{wmsPutAwayEntity.areaNo}，在途库区{basWRegion.regionNo}，没有维护在途库位。");
                        }
                        #endregion

                        #region 校验上架单明细信息
                        _vpoint = $"校验上架单明细信息";
                        List<WmsPutawayDtl> wmsPutawayDtls = await DC.Set<WmsPutawayDtl>().Where(x => x.putawayNo == wmsPutAwayEntity.putawayNo).ToListAsync();
                        var wmsPutawayDtl = wmsPutawayDtls.Where(x => x.palletBarcode != input.palletBarcode).FirstOrDefault();
                        if (wmsPutawayDtl != null)
                        {
                            throw new Exception($"该上架单号{wmsPutAwayEntity.putawayNo}，有不是该载体{input.palletBarcode}的上架明细记录，或平库区域记录,库位{wmsPutawayDtl.ptaBinNo}。");
                        }
                        //decimal? qty = wmsPutawayDtls.Sum(x => x.qty);
                        decimal? qty = wmsPutawayDtls.Sum(x => x.recordQty);

                        #endregion

                        #region 逻辑处理
                        _vpoint = $"更新库存主表";
                        WmsStock wmsStock = await DC.Set<WmsStock>().Where(x => x.stockCode == wmsPutAwayEntity.stockCode).FirstOrDefaultAsync();
                        if (wmsStock == null)
                        {
                            throw new Exception($"该上架单号{wmsPutAwayEntity.putawayNo}的库存编码{wmsPutAwayEntity.stockCode}查不到库存主表记录。");
                        }
                        wmsStock.locNo = input.locNo;
                        wmsStock.regionNo = basWRegion.regionNo;
                        wmsStock.roadwayNo = basWBin.roadwayNo;
                        wmsStock.binNo = basWBin.binNo;
                        wmsStock.stockStatus = Convert.ToInt32(StockStatus.InStoreing.GetCode());
                        wmsStock.UpdateBy = invoker;
                        wmsStock.UpdateTime = DateTime.Now;
                        //DC.UpdateEntity(wmsStock);
                        await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(wmsStock);

                        _vpoint = $"更新库存明细";
                        List<WmsStockDtl> wmsStockDtls = await DC.Set<WmsStockDtl>().Where(x => x.stockCode == wmsStock.stockCode).ToListAsync();
                        wmsStockDtls.ForEach(t =>
                        {
                            t.stockDtlStatus = Convert.ToInt32(StockStatus.InStoreing.GetCode());
                            t.UpdateBy = invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        if (wmsStockDtls.Any())
                        {
                            //DC.Set<WmsStockDtl>().UpdateRange(wmsStockDtls);
                            await ((DbContext)DC).BulkUpdateAsync(wmsStockDtls);
                        }

                        _vpoint = $"插入库存调整记录";
                        WmsStockAdjust wmsStockAdjust = new WmsStockAdjust();
                        wmsStockAdjust.whouseNo = wmsStock.whouseNo; // 仓库号
                        wmsStockAdjust.proprietorCode = wmsStock.proprietorCode; // 货主
                        wmsStockAdjust.stockCode = wmsStock.stockCode; // 库存编码
                        wmsStockAdjust.palletBarcode = wmsStock.palletBarcode; // 载体条码
                        wmsStockAdjust.adjustType = "上线"; // 调整类型;新增、修改、删除
                                                          //wmsStockAdjust.packageBarcode = wmsStockUniicode.uniicode; // 包装条码
                        wmsStockAdjust.adjustDesc = "更新库存状态更新为入库中！上线站台：" + input.locNo;
                        wmsStockAdjust.adjustOperate = "上线"; // 调整操作
                        wmsStockAdjust.CreateBy = invoker;
                        wmsStockAdjust.CreateTime = DateTime.Now;
                        //DC.AddEntity(wmsStockAdjust);
                        await ((DbContext)DC).Set<WmsStockAdjust>().SingleInsertAsync(wmsStockAdjust);

                        _vpoint = $"更新上架主表";
                        wmsPutAwayEntity.onlineLocNo = input.locNo;
                        wmsPutAwayEntity.putawayStatus = Convert.ToInt32(IqcResultStatus.StoreIning.GetCode());
                        wmsPutAwayEntity.UpdateBy = invoker;
                        wmsPutAwayEntity.UpdateTime = DateTime.Now;
                        //DC.UpdateEntity(wmsPutAwayEntity);
                        await ((DbContext)DC).Set<WmsPutaway>().SingleUpdateAsync(wmsPutAwayEntity);
                        _vpoint = $"更新上架明细表";
                        wmsPutawayDtls.ForEach(t =>
                        {
                            t.putawayDtlStatus = Convert.ToInt32(IqcResultStatus.StoreIning.GetCode());
                            t.UpdateBy = invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        //DC.Set<WmsPutawayDtl>().UpdateRange(wmsPutawayDtls);
                        await ((DbContext)DC).BulkUpdateAsync(wmsPutawayDtls);

                        _vpoint = $"生成WMS任务";
                        WmsTask wmsTaskEntity = new WmsTask();
                        wmsTaskEntity.feedbackDesc = String.Empty;
                        wmsTaskEntity.feedbackStatus = 0;
                        wmsTaskEntity.frLocationNo = input.locNo;
                        wmsTaskEntity.frLocationType = 0;
                        wmsTaskEntity.loadedType = wmsStock.loadedType;
                        wmsTaskEntity.matHeight = wmsStock.height;//高
                        wmsTaskEntity.matLength = null;//长
                        wmsTaskEntity.matQty = null;
                        wmsTaskEntity.matWeight = null;//重量
                        wmsTaskEntity.matWidth = null;//宽
                        wmsTaskEntity.orderNo = wmsPutAwayEntity.putawayNo;
                        wmsTaskEntity.palletBarcode = input.palletBarcode;
                        wmsTaskEntity.proprietorCode = "TZ";
                        wmsTaskEntity.regionNo = basWRegion.regionNo;
                        wmsTaskEntity.roadwayNo = basWBin.roadwayNo;
                        wmsTaskEntity.stockCode = wmsStock.stockCode;
                        wmsTaskEntity.taskDesc = "初始创建";
                        wmsTaskEntity.taskPriority = 100;
                        wmsTaskEntity.taskStatus = 0;
                        wmsTaskEntity.taskTypeNo = "IN";
                        wmsTaskEntity.toLocationNo = toLocNo;
                        wmsTaskEntity.toLocationType = 0;
                        wmsTaskEntity.whouseNo = wmsPutAwayEntity.whouseNo;
                        wmsTaskEntity.wmsTaskNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.wmsTaskNo.GetCode());
                        wmsTaskEntity.wmsTaskType = wmsPutAwayEntity.loadedType == 1 ? "IN" : "EMPTY_IN";
                        wmsTaskEntity.CreateBy = invoker;
                        wmsTaskEntity.CreateTime = DateTime.Now;
                        wmsTaskEntity.UpdateBy = invoker;
                        wmsTaskEntity.UpdateTime = DateTime.Now;
                        //DC.AddEntity(wmsTaskEntity);
                        await ((DbContext)DC).Set<WmsTask>().SingleInsertAsync(wmsTaskEntity);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                        //DC.SaveChanges();
                        #endregion

                        #endregion
                    }
                    else
                    {
                        #region 移库逻辑
                        _vpoint = $"更新库存主表";

                        moveStock.locNo = input.locNo;
                        //moveStock.regionNo = basWRegion.regionNo;
                        //moveStock.roadwayNo = basWBin.roadwayNo;
                        //moveStock.binNo = basWBin.binNo;
                        moveStock.stockStatus = Convert.ToInt32(StockStatus.InStoreing.GetCode());
                        moveStock.UpdateBy = invoker;
                        moveStock.UpdateTime = DateTime.Now;
                        //DC.UpdateEntity(wmsStock);
                        await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(moveStock);

                        _vpoint = $"更新库存明细";
                        List<WmsStockDtl> wmsStockDtls = await DC.Set<WmsStockDtl>().Where(x => x.stockCode == moveStock.stockCode).ToListAsync();
                        wmsStockDtls.ForEach(t =>
                        {
                            t.stockDtlStatus = Convert.ToInt32(StockStatus.InStoreing.GetCode());
                            t.UpdateBy = invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        if (wmsStockDtls.Any())
                        {
                            //DC.Set<WmsStockDtl>().UpdateRange(wmsStockDtls);
                            await ((DbContext)DC).BulkUpdateAsync(wmsStockDtls);
                        }

                        _vpoint = $"插入库存调整记录";
                        WmsStockAdjust wmsStockAdjust = new WmsStockAdjust();
                        wmsStockAdjust.whouseNo = moveStock.whouseNo; // 仓库号
                        wmsStockAdjust.proprietorCode = moveStock.proprietorCode; // 货主
                        wmsStockAdjust.stockCode = moveStock.stockCode; // 库存编码
                        wmsStockAdjust.palletBarcode = moveStock.palletBarcode; // 载体条码
                        wmsStockAdjust.adjustType = "移库上线"; // 调整类型;新增、修改、删除
                                                            //wmsStockAdjust.packageBarcode = wmsStockUniicode.uniicode; // 包装条码
                        wmsStockAdjust.adjustDesc = "更新库存状态更新为入库中！上线站台：" + input.locNo;
                        wmsStockAdjust.adjustOperate = "上线"; // 调整操作
                        wmsStockAdjust.CreateBy = invoker;
                        wmsStockAdjust.CreateTime = DateTime.Now;
                        //DC.AddEntity(wmsStockAdjust);
                        await ((DbContext)DC).Set<WmsStockAdjust>().SingleInsertAsync(wmsStockAdjust);
                        _vpoint = $"生成上架单";
                        WmsPutaway movePutaway = new WmsPutaway();
                        var putawayNoSeq = await sysSequenceVM.GetSequenceAsync(SequenceCode.WmsPutawayNo.GetCode());
                        movePutaway.whouseNo = moveStock.whouseNo; // 仓库号
                        movePutaway.areaNo = moveStock.areaNo; // 区域编码(楼号)
                        movePutaway.loadedType = 1; // 装载类型;1:实盘 ；2:工装；0：空盘；
                        movePutaway.manualFlag = 0; // 是否允许人工上架;0默认不允许，1允许
                        movePutaway.onlineLocNo = ""; // 上线站台：WCS请求时的站台
                        movePutaway.onlineMethod = "0"; // 上线方式;0自动上线；1人工上线；2组盘上线；3直接上架
                        movePutaway.palletBarcode = moveStock.palletBarcode; // 载体条码
                        movePutaway.proprietorCode = moveStock.proprietorCode; // 货主
                        movePutaway.ptaRegionNo = moveStock.regionNo; // 上架库区编号
                        movePutaway.putawayNo = putawayNoSeq; // 上架单编号
                        movePutaway.putawayStatus = Convert.ToInt32(IqcResultStatus.StoreIning.GetCode()); // 状态;0：初始创建（组盘完成）；41：入库中；90：上架完成；92删除；93强制完成
                        movePutaway.regionNo = moveStock.areaNo; // 库区编号
                        movePutaway.stockCode = moveStock.stockCode; // 库存编码
                        movePutaway.CreateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                        movePutaway.CreateTime = DateTime.Now;
                        //DC.AddEntity(wmsPutaway);
                        await ((DbContext)DC).Set<WmsPutaway>().SingleInsertAsync(movePutaway);
                        _vpoint = $"生成上架单明细";
                        List<WmsPutawayDtl> wmsPutawayDtls = new List<WmsPutawayDtl>();
                        foreach (var item in wmsStockDtls)
                        {
                            var wmsMoveRecord = await DC.Set<WmsItnMoveRecord>().Where(x => x.frStockDtlId == item.ID).FirstOrDefaultAsync();
                            WmsPutawayDtl wmsPutawayDtl = new WmsPutawayDtl();
                            wmsPutawayDtl.areaNo = item.areaNo; // 区域编码(楼号)
                            wmsPutawayDtl.binNo = moveStock.binNo; // 系统推荐库位号
                            wmsPutawayDtl.docTypeCode = "5001"; // 单据类型 巷道内移库
                            wmsPutawayDtl.inspectionResult = item.inspectionResult; // 质量标记;0：待检；1：合格；2：不合格；3：免检；4：冻结；
                            wmsPutawayDtl.materialCode = item.materialCode; // 物料编码
                            wmsPutawayDtl.materialName = item.materialName; // 物料编码
                            wmsPutawayDtl.unitCode = item.unitCode; // 物料编码
                            wmsPutawayDtl.materialSpec = item.materialSpec; // 物料规格
                            wmsPutawayDtl.orderDtlId = wmsMoveRecord?.moveDtlId; // 关联单据明细ID
                            wmsPutawayDtl.orderNo = wmsMoveRecord?.moveNo; // 关联单据编号
                            wmsPutawayDtl.palletBarcode = moveStock.palletBarcode; // 载体条码
                            wmsPutawayDtl.proprietorCode = moveStock.proprietorCode; // 货主
                            wmsPutawayDtl.ptaBinNo = null; // 上架库位
                            wmsPutawayDtl.putawayDtlStatus = Convert.ToInt32(PutAwayOrDtlStatus.StoreIning.GetCode()); // 状态;0：初始创建（组盘完成）；11：入库中；90：上架完成；92删除；93强制完成
                            wmsPutawayDtl.putawayNo = movePutaway.putawayNo; // 上架单编号
                            wmsPutawayDtl.recordId = wmsMoveRecord?.ID; // 记录ID
                            wmsPutawayDtl.recordQty = item.qty; // 数量(组盘数量)
                            wmsPutawayDtl.regionNo = moveStock.regionNo; // 库区编号
                            wmsPutawayDtl.roadwayNo = moveStock.roadwayNo; // 巷道
                            wmsPutawayDtl.skuCode = item.skuCode; // SKU编码
                            wmsPutawayDtl.stockCode = moveStock.stockCode; // 库存编码
                            wmsPutawayDtl.stockDtlId = item.ID; // 库存明细ID
                            wmsPutawayDtl.supplierCode = item.supplierCode; // 供应商编码
                            wmsPutawayDtl.supplierName = item.supplierName; // 供方名称
                            wmsPutawayDtl.supplierNameAlias = item.supplierNameAlias; // 供方名称-其他
                            wmsPutawayDtl.supplierNameEn = item.supplierNameEn; // 供方名称-英文
                            wmsPutawayDtl.whouseNo = item.whouseNo; // 仓库号
                            wmsPutawayDtl.CreateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                            wmsPutawayDtl.CreateTime = DateTime.Now;
                            //DC.AddEntity(wmsPutawayDtl);
                            wmsPutawayDtls.Add(wmsPutawayDtl);
                        }
                        await ((DbContext)DC).Set<WmsPutawayDtl>().BulkInsertAsync(wmsPutawayDtls);

                        _vpoint = $"生成WMS任务";
                        WmsTask wmsTaskEntity = new WmsTask();
                        wmsTaskEntity.feedbackDesc = String.Empty;
                        wmsTaskEntity.feedbackStatus = 0;
                        wmsTaskEntity.frLocationNo = input.locNo;
                        wmsTaskEntity.frLocationType = 0;
                        wmsTaskEntity.loadedType = moveStock.loadedType;
                        wmsTaskEntity.matHeight = moveStock.height;//高
                        wmsTaskEntity.matLength = null;//长
                        wmsTaskEntity.matQty = null;
                        wmsTaskEntity.matWeight = null;//重量
                        wmsTaskEntity.matWidth = null;//宽
                        wmsTaskEntity.orderNo = movePutaway.putawayNo;
                        wmsTaskEntity.palletBarcode = input.palletBarcode;
                        wmsTaskEntity.proprietorCode = "TZ";
                        wmsTaskEntity.regionNo = moveStock.regionNo;
                        wmsTaskEntity.roadwayNo = moveStock.roadwayNo;
                        wmsTaskEntity.stockCode = moveStock.stockCode;
                        wmsTaskEntity.taskDesc = "初始创建";
                        wmsTaskEntity.taskPriority = 100;
                        wmsTaskEntity.taskStatus = 0;
                        wmsTaskEntity.taskTypeNo = "IN";
                        wmsTaskEntity.toLocationNo = toLocNo;
                        wmsTaskEntity.toLocationType = 0;
                        wmsTaskEntity.whouseNo = movePutaway.whouseNo;
                        wmsTaskEntity.wmsTaskNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.wmsTaskNo.GetCode());
                        wmsTaskEntity.wmsTaskType = "IN";
                        wmsTaskEntity.CreateBy = invoker;
                        wmsTaskEntity.CreateTime = DateTime.Now;
                        wmsTaskEntity.UpdateBy = invoker;
                        wmsTaskEntity.UpdateTime = DateTime.Now;
                        //DC.AddEntity(wmsTaskEntity);
                        await ((DbContext)DC).Set<WmsTask>().SingleInsertAsync(wmsTaskEntity);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                        #endregion
                    }

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

                    result.code = ResCode.Error;
                    result.msg = $"WmsPutawayVM->PutAwayOnline 执行异常! 异常位置: [ {_vpoint} ], 异常信息: [ {ex.Message} ]";
                }
            }
            string outJson = JsonConvert.SerializeObject(result);
            logger.Warn($"---->上线操作，操作人：{invoker},托盘号：【{input.palletBarcode}】,站台号：【{input.locNo}】,入参：【{inputJson}】,出参：【{outJson}】");
            return result;
        }

        /// <summary>
        /// 入库业务：直接上线
        /// </summary>
        /// <param name="input"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public async Task<BusinessResult> PutAwayOnlineAsync(PutAwayOnlineDto input, string invoker)
        {
            BusinessResult result = new BusinessResult();
            SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
            BasWPalletTypeVM basWPalletTypeVM = Wtm.CreateVM<BasWPalletTypeVM>();
            var hasParentTransaction = false;
            string _vpoint = "";
            var inputJson = JsonConvert.SerializeObject(input);
            result = await ParamsValid(input);
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
                        await DC.Database.BeginTransactionAsync();
                    }
                    var moveStock = await DC.Set<WmsStock>().Where(x => x.palletBarcode == input.palletBarcode).FirstOrDefaultAsync();
                    if (moveStock == null)
                    {
                        throw new Exception($"该托盘号{input.palletBarcode}查不到库存主表记录。");
                    }
                    var moveStockDtl = await DC.Set<WmsStockDtl>().Where(x => x.stockCode == moveStock.stockCode).ToListAsync();
                    if (!moveStockDtl.Any())
                    {
                        throw new Exception($"该托盘号{input.palletBarcode}，库存编码{moveStock.stockCode}，查不到库存明细记录。");
                    }
                    bool isMove = false;
                    var stockDtlStatus = moveStockDtl.Select(x => x.stockDtlStatus == 0).ToList();
                    if (moveStock.stockStatus == 0 && stockDtlStatus.Count > 0)
                    {
                        var wmsPutaway = await DC.Set<WmsPutaway>().Where(x => x.palletBarcode == input.palletBarcode).FirstOrDefaultAsync();
                        if (wmsPutaway == null)
                        {
                            //if (moveStock.regionNo == WRgionTypeCode.MOVE.GetCode())
                            //{
                            isMove = true;
                            //}
                        }
                    }
                    string toLocNo = "8010";
                    BasWPalletTypeDto basWPalletTypeView = await basWPalletTypeVM.GetPalletTypeAsync(input.palletBarcode);
                    if (basWPalletTypeView.palletType == PalletTypeExtend.UnKnown)
                    {
                        throw new Exception($"托盘{input.palletBarcode}规则不正确!");
                    }
                    if (basWPalletTypeView.basWPalletType.palletTypeCode == PalletTypeExtend.Box.GetCode())
                    {
                        toLocNo = "9010";
                    }
                    if (!isMove)
                    {
                        #region 入库逻辑

                        #region 通过托盘号获取上架单主表信息
                        _vpoint = $"通过托盘号获取上架单主表信息";
                        List<WmsPutaway> wmsPutaways = await DC.Set<WmsPutaway>().Where(x => x.palletBarcode == input.palletBarcode && x.putawayStatus == 0).ToListAsync();
                        if (wmsPutaways.Count > 1)
                        {
                            throw new Exception($"根据托盘号{input.palletBarcode}，查询到多条上架单主表。");
                        }
                        else if (wmsPutaways.Count == 0)
                        {
                            throw new Exception($"根据托盘号{input.palletBarcode}，查询不到上架单主表。");
                        }
                        var wmsPutAwayEntity = wmsPutaways.FirstOrDefault();

                        WmsTask wmsTask = await DC.Set<WmsTask>().Where(x => x.palletBarcode == input.palletBarcode).FirstOrDefaultAsync();
                        if (wmsTask != null)
                        {
                            throw new Exception($"该托盘号{input.palletBarcode}，已生成任务，任务号{wmsTask.wmsTaskNo}。");
                        }
                        #endregion

                        #region 校验是否维护在途库区/库位
                        _vpoint = $"校验是否维护在库库区/库位";
                        BasWRegion basWRegion = await DC.Set<BasWRegion>().Where(x => x.regionTypeCode == WRgionTypeCode.WI.GetCode() && x.areaNo == wmsPutAwayEntity.areaNo).FirstOrDefaultAsync();
                        if (basWRegion == null)
                        {
                            throw new Exception($"该区域{wmsPutAwayEntity.areaNo}，没有维护在途库区。");
                        }
                        BasWBin basWBin = await DC.Set<BasWBin>().Where(x => x.regionNo == basWRegion.regionNo).FirstOrDefaultAsync();
                        if (basWBin == null)
                        {
                            throw new Exception($"该区域{wmsPutAwayEntity.areaNo}，在途库区{basWRegion.regionNo}，没有维护在途库位。");
                        }
                        #endregion

                        #region 校验上架单明细信息
                        _vpoint = $"校验上架单明细信息";
                        List<WmsPutawayDtl> wmsPutawayDtls = await DC.Set<WmsPutawayDtl>().Where(x => x.putawayNo == wmsPutAwayEntity.putawayNo).ToListAsync();
                        var wmsPutawayDtl = wmsPutawayDtls.Where(x => x.palletBarcode != input.palletBarcode).FirstOrDefault();
                        if (wmsPutawayDtl != null)
                        {
                            throw new Exception($"该上架单号{wmsPutAwayEntity.putawayNo}，有不是该载体{input.palletBarcode}的上架明细记录，或平库区域记录,库位{wmsPutawayDtl.ptaBinNo}。");
                        }
                        //decimal? qty = wmsPutawayDtls.Sum(x => x.qty);
                        decimal? qty = wmsPutawayDtls.Sum(x => x.recordQty);

                        #endregion

                        #region 逻辑处理
                        _vpoint = $"更新库存主表";
                        WmsStock wmsStock = await DC.Set<WmsStock>().Where(x => x.stockCode == wmsPutAwayEntity.stockCode).FirstOrDefaultAsync();
                        if (wmsStock == null)
                        {
                            throw new Exception($"该上架单号{wmsPutAwayEntity.putawayNo}的库存编码{wmsPutAwayEntity.stockCode}查不到库存主表记录。");
                        }
                        wmsStock.locNo = input.locNo;
                        wmsStock.regionNo = basWRegion.regionNo;
                        wmsStock.roadwayNo = basWBin.roadwayNo;
                        wmsStock.binNo = basWBin.binNo;
                        wmsStock.stockStatus = Convert.ToInt32(StockStatus.InStoreing.GetCode());
                        wmsStock.UpdateBy = invoker;
                        wmsStock.UpdateTime = DateTime.Now;
                        //DC.UpdateEntity(wmsStock);
                        await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(wmsStock);

                        _vpoint = $"更新库存明细";
                        List<WmsStockDtl> wmsStockDtls = await DC.Set<WmsStockDtl>().Where(x => x.stockCode == wmsStock.stockCode).ToListAsync();
                        wmsStockDtls.ForEach(t =>
                        {
                            t.stockDtlStatus = Convert.ToInt32(StockStatus.InStoreing.GetCode());
                            t.UpdateBy = invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        if (wmsStockDtls.Any())
                        {
                            //DC.Set<WmsStockDtl>().UpdateRange(wmsStockDtls);
                            await ((DbContext)DC).BulkUpdateAsync(wmsStockDtls);
                        }

                        _vpoint = $"插入库存调整记录";
                        WmsStockAdjust wmsStockAdjust = new WmsStockAdjust();
                        wmsStockAdjust.whouseNo = wmsStock.whouseNo; // 仓库号
                        wmsStockAdjust.proprietorCode = wmsStock.proprietorCode; // 货主
                        wmsStockAdjust.stockCode = wmsStock.stockCode; // 库存编码
                        wmsStockAdjust.palletBarcode = wmsStock.palletBarcode; // 载体条码
                        wmsStockAdjust.adjustType = "上线"; // 调整类型;新增、修改、删除
                                                          //wmsStockAdjust.packageBarcode = wmsStockUniicode.uniicode; // 包装条码
                        wmsStockAdjust.adjustDesc = "更新库存状态更新为入库中！上线站台：" + input.locNo;
                        wmsStockAdjust.adjustOperate = "上线"; // 调整操作
                        wmsStockAdjust.CreateBy = invoker;
                        wmsStockAdjust.CreateTime = DateTime.Now;
                        //DC.AddEntity(wmsStockAdjust);
                        await ((DbContext)DC).Set<WmsStockAdjust>().SingleInsertAsync(wmsStockAdjust);

                        _vpoint = $"更新上架主表";
                        wmsPutAwayEntity.onlineLocNo = input.locNo;
                        wmsPutAwayEntity.putawayStatus = Convert.ToInt32(IqcResultStatus.StoreIning.GetCode());
                        wmsPutAwayEntity.UpdateBy = invoker;
                        wmsPutAwayEntity.UpdateTime = DateTime.Now;
                        //DC.UpdateEntity(wmsPutAwayEntity);
                        await ((DbContext)DC).Set<WmsPutaway>().SingleUpdateAsync(wmsPutAwayEntity);
                        _vpoint = $"更新上架明细表";
                        wmsPutawayDtls.ForEach(t =>
                        {
                            t.putawayDtlStatus = Convert.ToInt32(IqcResultStatus.StoreIning.GetCode());
                            t.UpdateBy = invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        //DC.Set<WmsPutawayDtl>().UpdateRange(wmsPutawayDtls);
                        await ((DbContext)DC).BulkUpdateAsync(wmsPutawayDtls);

                        _vpoint = $"生成WMS任务";
                        WmsTask wmsTaskEntity = new WmsTask();
                        wmsTaskEntity.feedbackDesc = String.Empty;
                        wmsTaskEntity.feedbackStatus = 0;
                        wmsTaskEntity.frLocationNo = input.locNo;
                        wmsTaskEntity.frLocationType = 0;
                        wmsTaskEntity.loadedType = wmsStock.loadedType;
                        wmsTaskEntity.matHeight = wmsStock.height;//高
                        wmsTaskEntity.matLength = null;//长
                        wmsTaskEntity.matQty = null;
                        wmsTaskEntity.matWeight = null;//重量
                        wmsTaskEntity.matWidth = null;//宽
                        wmsTaskEntity.orderNo = wmsPutAwayEntity.putawayNo;
                        wmsTaskEntity.palletBarcode = input.palletBarcode;
                        wmsTaskEntity.proprietorCode = wmsPutAwayEntity.proprietorCode;
                        wmsTaskEntity.regionNo = basWRegion.regionNo;
                        wmsTaskEntity.roadwayNo = basWBin.roadwayNo;
                        wmsTaskEntity.stockCode = wmsStock.stockCode;
                        wmsTaskEntity.taskDesc = "初始创建";
                        wmsTaskEntity.taskPriority = 100;
                        wmsTaskEntity.taskStatus = 0;
                        wmsTaskEntity.taskTypeNo = "IN";
                        wmsTaskEntity.toLocationNo = toLocNo;
                        wmsTaskEntity.toLocationType = 0;
                        wmsTaskEntity.whouseNo = wmsPutAwayEntity.whouseNo;
                        wmsTaskEntity.wmsTaskNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.wmsTaskNo.GetCode());
                        wmsTaskEntity.wmsTaskType = wmsPutAwayEntity.loadedType == 1 ? "IN" : "EMPTY_IN";
                        wmsTaskEntity.CreateBy = invoker;
                        wmsTaskEntity.CreateTime = DateTime.Now;
                        wmsTaskEntity.UpdateBy = invoker;
                        wmsTaskEntity.UpdateTime = DateTime.Now;
                        //DC.AddEntity(wmsTaskEntity);
                        await ((DbContext)DC).Set<WmsTask>().SingleInsertAsync(wmsTaskEntity);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                        //DC.SaveChanges();

                        _vpoint = $"生成WCS指令";
                        SrmCmd srmCmd = new SrmCmd();
                        srmCmd.SubTask_No = await sysSequenceVM.GetSequenceAsync(SequenceCode.srmCmdNo.GetCode());
                        srmCmd.Task_No = wmsTaskEntity.wmsTaskNo;
                        srmCmd.Serial_No = 0;
                        srmCmd.Device_No = "SRM";
                        srmCmd.Fork_No = "1";
                        srmCmd.Station_Type = "0";
                        srmCmd.Check_Point =0;
                        srmCmd.From_Station = Convert.ToInt16(input.locNo);
                        srmCmd.Task_Type = "IN";
                        srmCmd.To_Column = 0;
                        srmCmd.To_ForkDirection = 0;
                        srmCmd.To_Layer = 0;
                        srmCmd.To_Deep = 0;
                        srmCmd.From_Column = 0;
                        srmCmd.From_ForkDirection = 0;
                        srmCmd.From_Layer = 0;
                        srmCmd.From_Deep = 0;
                        srmCmd.Task_Cmd = 4;
                        srmCmd.Pallet_Barcode = Convert.ToInt32(input.palletBarcode);
                        //srmCmd.Pallet_Barcode = input.palletBarcode;
                        srmCmd.Exec_Status = 3;
                        srmCmd.Recive_Date = DateTime.Now;
                        srmCmd.CreateBy = invoker;
                        srmCmd.CreateTime = DateTime.Now;
                        await ((DbContext)DC).Set<SrmCmd>().SingleInsertAsync(srmCmd);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                        #endregion

                        #endregion
                    }
                    else
                    {
                        #region 移库逻辑
                        _vpoint = $"更新库存主表";

                        moveStock.locNo = input.locNo;
                        //moveStock.regionNo = basWRegion.regionNo;
                        //moveStock.roadwayNo = basWBin.roadwayNo;
                        //moveStock.binNo = basWBin.binNo;
                        moveStock.stockStatus = Convert.ToInt32(StockStatus.InStoreing.GetCode());
                        moveStock.UpdateBy = invoker;
                        moveStock.UpdateTime = DateTime.Now;
                        //DC.UpdateEntity(wmsStock);
                        await ((DbContext)DC).Set<WmsStock>().SingleUpdateAsync(moveStock);

                        _vpoint = $"更新库存明细";
                        List<WmsStockDtl> wmsStockDtls = await DC.Set<WmsStockDtl>().Where(x => x.stockCode == moveStock.stockCode).ToListAsync();
                        wmsStockDtls.ForEach(t =>
                        {
                            t.stockDtlStatus = Convert.ToInt32(StockStatus.InStoreing.GetCode());
                            t.UpdateBy = invoker;
                            t.UpdateTime = DateTime.Now;
                        });
                        if (wmsStockDtls.Any())
                        {
                            //DC.Set<WmsStockDtl>().UpdateRange(wmsStockDtls);
                            await ((DbContext)DC).BulkUpdateAsync(wmsStockDtls);
                        }

                        _vpoint = $"插入库存调整记录";
                        WmsStockAdjust wmsStockAdjust = new WmsStockAdjust();
                        wmsStockAdjust.whouseNo = moveStock.whouseNo; // 仓库号
                        wmsStockAdjust.proprietorCode = moveStock.proprietorCode; // 货主
                        wmsStockAdjust.stockCode = moveStock.stockCode; // 库存编码
                        wmsStockAdjust.palletBarcode = moveStock.palletBarcode; // 载体条码
                        wmsStockAdjust.adjustType = "移库上线"; // 调整类型;新增、修改、删除
                                                            //wmsStockAdjust.packageBarcode = wmsStockUniicode.uniicode; // 包装条码
                        wmsStockAdjust.adjustDesc = "更新库存状态更新为入库中！上线站台：" + input.locNo;
                        wmsStockAdjust.adjustOperate = "上线"; // 调整操作
                        wmsStockAdjust.CreateBy = invoker;
                        wmsStockAdjust.CreateTime = DateTime.Now;
                        //DC.AddEntity(wmsStockAdjust);
                        await ((DbContext)DC).Set<WmsStockAdjust>().SingleInsertAsync(wmsStockAdjust);
                        _vpoint = $"生成上架单";
                        WmsPutaway movePutaway = new WmsPutaway();
                        var putawayNoSeq = await sysSequenceVM.GetSequenceAsync(SequenceCode.WmsPutawayNo.GetCode());
                        movePutaway.whouseNo = moveStock.whouseNo; // 仓库号
                        movePutaway.areaNo = moveStock.areaNo; // 区域编码(楼号)
                        movePutaway.loadedType = 1; // 装载类型;1:实盘 ；2:工装；0：空盘；
                        movePutaway.manualFlag = 0; // 是否允许人工上架;0默认不允许，1允许
                        movePutaway.onlineLocNo = ""; // 上线站台：WCS请求时的站台
                        movePutaway.onlineMethod = "0"; // 上线方式;0自动上线；1人工上线；2组盘上线；3直接上架
                        movePutaway.palletBarcode = moveStock.palletBarcode; // 载体条码
                        movePutaway.proprietorCode = moveStock.proprietorCode; // 货主
                        movePutaway.ptaRegionNo = moveStock.regionNo; // 上架库区编号
                        movePutaway.putawayNo = putawayNoSeq; // 上架单编号
                        movePutaway.putawayStatus = Convert.ToInt32(IqcResultStatus.StoreIning.GetCode()); // 状态;0：初始创建（组盘完成）；41：入库中；90：上架完成；92删除；93强制完成
                        movePutaway.regionNo = moveStock.areaNo; // 库区编号
                        movePutaway.stockCode = moveStock.stockCode; // 库存编码
                        movePutaway.CreateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                        movePutaway.CreateTime = DateTime.Now;
                        //DC.AddEntity(wmsPutaway);
                        await ((DbContext)DC).Set<WmsPutaway>().SingleInsertAsync(movePutaway);
                        _vpoint = $"生成上架单明细";
                        List<WmsPutawayDtl> wmsPutawayDtls = new List<WmsPutawayDtl>();
                        foreach (var item in wmsStockDtls)
                        {
                            var wmsMoveRecord = await DC.Set<WmsItnMoveRecord>().Where(x => x.frStockDtlId == item.ID).FirstOrDefaultAsync();
                            WmsPutawayDtl wmsPutawayDtl = new WmsPutawayDtl();
                            wmsPutawayDtl.areaNo = item.areaNo; // 区域编码(楼号)
                            wmsPutawayDtl.binNo = moveStock.binNo; // 系统推荐库位号
                            wmsPutawayDtl.docTypeCode = "5001"; // 单据类型 巷道内移库
                            wmsPutawayDtl.inspectionResult = item.inspectionResult; // 质量标记;0：待检；1：合格；2：不合格；3：免检；4：冻结；
                            wmsPutawayDtl.materialCode = item.materialCode; // 物料编码
                            wmsPutawayDtl.materialName = item.materialName; // 物料编码
                            wmsPutawayDtl.unitCode = item.unitCode; // 物料编码
                            wmsPutawayDtl.materialSpec = item.materialSpec; // 物料规格
                            wmsPutawayDtl.orderDtlId = wmsMoveRecord?.moveDtlId; // 关联单据明细ID
                            wmsPutawayDtl.orderNo = wmsMoveRecord?.moveNo; // 关联单据编号
                            wmsPutawayDtl.palletBarcode = moveStock.palletBarcode; // 载体条码
                            wmsPutawayDtl.proprietorCode = moveStock.proprietorCode; // 货主
                            wmsPutawayDtl.ptaBinNo = null; // 上架库位
                            wmsPutawayDtl.putawayDtlStatus = Convert.ToInt32(PutAwayOrDtlStatus.StoreIning.GetCode()); // 状态;0：初始创建（组盘完成）；11：入库中；90：上架完成；92删除；93强制完成
                            wmsPutawayDtl.putawayNo = movePutaway.putawayNo; // 上架单编号
                            wmsPutawayDtl.recordId = wmsMoveRecord?.ID; // 记录ID
                            wmsPutawayDtl.recordQty = item.qty; // 数量(组盘数量)
                            wmsPutawayDtl.regionNo = moveStock.regionNo; // 库区编号
                            wmsPutawayDtl.roadwayNo = moveStock.roadwayNo; // 巷道
                            wmsPutawayDtl.skuCode = item.skuCode; // SKU编码
                            wmsPutawayDtl.stockCode = moveStock.stockCode; // 库存编码
                            wmsPutawayDtl.stockDtlId = item.ID; // 库存明细ID
                            wmsPutawayDtl.supplierCode = item.supplierCode; // 供应商编码
                            wmsPutawayDtl.supplierName = item.supplierName; // 供方名称
                            wmsPutawayDtl.supplierNameAlias = item.supplierNameAlias; // 供方名称-其他
                            wmsPutawayDtl.supplierNameEn = item.supplierNameEn; // 供方名称-英文
                            wmsPutawayDtl.whouseNo = item.whouseNo; // 仓库号
                            wmsPutawayDtl.CreateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                            wmsPutawayDtl.CreateTime = DateTime.Now;
                            //DC.AddEntity(wmsPutawayDtl);
                            wmsPutawayDtls.Add(wmsPutawayDtl);
                        }
                        await ((DbContext)DC).Set<WmsPutawayDtl>().BulkInsertAsync(wmsPutawayDtls);

                        _vpoint = $"生成WMS任务";
                        WmsTask wmsTaskEntity = new WmsTask();
                        wmsTaskEntity.feedbackDesc = String.Empty;
                        wmsTaskEntity.feedbackStatus = 0;
                        wmsTaskEntity.frLocationNo = input.locNo;
                        wmsTaskEntity.frLocationType = 0;
                        wmsTaskEntity.loadedType = moveStock.loadedType;
                        wmsTaskEntity.matHeight = moveStock.height;//高
                        wmsTaskEntity.matLength = null;//长
                        wmsTaskEntity.matQty = null;
                        wmsTaskEntity.matWeight = null;//重量
                        wmsTaskEntity.matWidth = null;//宽
                        wmsTaskEntity.orderNo = movePutaway.putawayNo;
                        wmsTaskEntity.palletBarcode = input.palletBarcode;
                        wmsTaskEntity.proprietorCode = "TZ";
                        wmsTaskEntity.regionNo = moveStock.regionNo;
                        wmsTaskEntity.roadwayNo = moveStock.roadwayNo;
                        wmsTaskEntity.stockCode = moveStock.stockCode;
                        wmsTaskEntity.taskDesc = "初始创建";
                        wmsTaskEntity.taskPriority = 100;
                        wmsTaskEntity.taskStatus = 0;
                        wmsTaskEntity.taskTypeNo = "IN";
                        wmsTaskEntity.toLocationNo = toLocNo;
                        wmsTaskEntity.toLocationType = 0;
                        wmsTaskEntity.whouseNo = movePutaway.whouseNo;
                        wmsTaskEntity.wmsTaskNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.wmsTaskNo.GetCode());
                        wmsTaskEntity.wmsTaskType = "IN";
                        wmsTaskEntity.CreateBy = invoker;
                        wmsTaskEntity.CreateTime = DateTime.Now;
                        wmsTaskEntity.UpdateBy = invoker;
                        wmsTaskEntity.UpdateTime = DateTime.Now;
                        //DC.AddEntity(wmsTaskEntity);
                        await ((DbContext)DC).Set<WmsTask>().SingleInsertAsync(wmsTaskEntity);
                        await ((DbContext)DC).BulkSaveChangesAsync();

                        _vpoint = $"生成WCS指令";
                        SrmCmd srmCmd = new SrmCmd();
                        srmCmd.SubTask_No = await sysSequenceVM.GetSequenceAsync(SequenceCode.srmCmdNo.GetCode());
                        srmCmd.Task_No = wmsTaskEntity.wmsTaskNo;
                        srmCmd.Serial_No = 0;
                        srmCmd.Device_No = "SRM";
                        srmCmd.Fork_No = "1";
                        srmCmd.Station_Type = "0";
                        srmCmd.Check_Point = 0;
                        srmCmd.From_Station = Convert.ToInt16(input.locNo);
                        srmCmd.Task_Type = "IN";
                        srmCmd.To_Column = 0;
                        srmCmd.To_ForkDirection = 0;
                        srmCmd.To_Layer = 0;
                        srmCmd.To_Deep = 0;
                        srmCmd.From_Column = 0;
                        srmCmd.From_ForkDirection = 0;
                        srmCmd.From_Layer = 0;
                        srmCmd.From_Deep = 0;
                        srmCmd.Task_Cmd = 4;
                        srmCmd.Pallet_Barcode = Convert.ToInt32(input.palletBarcode);
                        //srmCmd.Pallet_Barcode = input.palletBarcode;
                        srmCmd.Exec_Status = 3;
                        srmCmd.Recive_Date = DateTime.Now;
                        srmCmd.CreateBy = invoker;
                        srmCmd.CreateTime = DateTime.Now;
                        await ((DbContext)DC).Set<SrmCmd>().SingleInsertAsync(srmCmd);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                        #endregion

                    }

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

                    result.code = ResCode.Error;
                    result.msg = $"WmsPutawayVM->PutAwayOnline 执行异常! 异常位置: [ {_vpoint} ], 异常信息: [ {ex.Message} ]";
                }
            }
            string outJson = JsonConvert.SerializeObject(result);
            logger.Warn($"---->上线操作，操作人：{invoker},托盘号：【{input.palletBarcode}】,站台号：【{input.locNo}】,入参：【{inputJson}】,出参：【{outJson}】");
            return result;
        }
        private async Task<BusinessResult> ParamsValid(PutAwayOnlineDto input)
        {
            BusinessResult result = new BusinessResult();
            if (input == null)
            {
                result.code = ResCode.Error;
                result.msg = $"入参input为空。";
                return result;
            }
            if (string.IsNullOrWhiteSpace(input.locNo))
            {
                result.code = ResCode.Error;
                result.msg = $"入参locNo为空；";
            }
            if (string.IsNullOrWhiteSpace(input.palletBarcode))
            {
                result.code = ResCode.Error;
                result.msg = result.msg + $"入参palletBarcode为空；";
            }
            return result;
        }
    }
}
