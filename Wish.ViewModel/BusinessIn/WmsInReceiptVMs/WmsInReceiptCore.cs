using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using System.DirectoryServices.Protocols;
using Wish.Areas.BasWhouse.Model;
using Wish.DtoModel.Common.Dtos;
using WISH.Helper.Common;
using Wish.ViewModel.Base.BasBMaterialVMs;
using Wish.ViewModel.Config.CfgDocTypeVMs;
using Wish.ViewModel.System.SysSequenceVMs;
using Wish.ViewModel.BusinessIn.WmsInOrderVMs;
using Wish.ViewModel.BusinessIn.WmsInReceiptIqcRecordVMs;
using Wish.ViewModel.BusinessIn.WmsInReceiptUniicodeVMs;
using Wish.ViewModel.Base.BasBSupplierVMs;
using Wish.ViewModel.BasWhouse.BasWRegionVMs;
using Wish.ViewModel.BasWhouse.BasWBinVMs;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Wish.ViewModel.Common.Dtos;
using Quartz.Util;

namespace Wish.ViewModel.BusinessIn.WmsInReceiptVMs
{
    public partial class WmsInReceiptVM
    {
        /// <summary>
        /// 收货操作
        /// </summary>
        /// <param name="inRecepit"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public async Task<BusinessResult> DoReceipt(WmsInReceiptInParamDto inRecepit, string invoker)
        {
            string inparam = JsonConvert.SerializeObject(inRecepit);
            BusinessResult result = new BusinessResult();
            string _vpoint = "";
            var hasParentTranslation = false;
            try
            {
                CfgDocTypeVM cfgDocTypeVM = Wtm.CreateVM<CfgDocTypeVM>();
                BasBMaterialVM basBMaterialVM = Wtm.CreateVM<BasBMaterialVM>();
                SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
                WmsInOrderVM wmsInOrderVM = Wtm.CreateVM<WmsInOrderVM>();
                WmsInReceiptIqcRecordVM wmsInReceiptIqcRecordVM = Wtm.CreateVM<WmsInReceiptIqcRecordVM>();
                WmsInReceiptUniicodeVM wmsInReceiptUniicodeVM = Wtm.CreateVM<WmsInReceiptUniicodeVM>();
                BasBSupplierVM basBSupplierVM = Wtm.CreateVM<BasBSupplierVM>();
                BasWRegionVM basWRegionVM = Wtm.CreateVM<BasWRegionVM>();
                BasWBinVM basWBinVM = Wtm.CreateVM<BasWBinVM>();
                List<BasBMaterialDto> basBMaterialViews = new List<BasBMaterialDto>();
                List<WmsInOrderDtl> wmsInOrderDetails = new List<WmsInOrderDtl>();
                CfgDocTypeDto cfgDocTypeView = null;

                Dictionary<Int64, List<WmsInReceiptUniicode>> wmsInOrderDetailUniicode = new Dictionary<Int64, List<WmsInReceiptUniicode>>();
                WmsInOrder wmsInOrder = null;
                WmsInReceipt wmsInReceipt = null;
                BasWRegion basWRegion = null;
                BasWBin basWBins = null;
                List<WmsInReceiptDtl> wmsInReceiptDts = new List<WmsInReceiptDtl>();
                WmsStock wmsStock = null;
                List<WmsStockUniicode> wmsStockUniicodes = new List<WmsStockUniicode>();
                List<WmsInReceiptUniicode> wmsInUniicodes = new List<WmsInReceiptUniicode>();
                List<WmsInReceiptUniicode> updateUniicodes = new List<WmsInReceiptUniicode>();
                var dateSecond = string.Empty;
                // 21-1 生成收货单单号
                string inReceiptNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.InReceiptNo.GetCode());
                if (DC.Database.CurrentTransaction != null)
                {
                    hasParentTranslation = true;
                }

                if (hasParentTranslation == false)
                {
                    await DC.Database.BeginTransactionAsync();
                }

                #region 1  校验

                #region 11 校验入库单信息

                _vpoint = "校验入库单信息";
                DateTime dt1 = DateTime.Now;
                wmsInOrder = await wmsInOrderVM.GetWmsInOrderAsync(inRecepit.inNo);
                wmsInOrderDetails = await wmsInOrderVM.GetWmsInOrderDtlsAsync(inRecepit.inNo);
                if (wmsInOrder == null)
                {
                    throw new Exception($"没有查询到入库单号是 {inRecepit.inNo} 的入库单信息!");
                }

                if ((wmsInOrder.inStatus == Convert.ToInt32(InOrDtlStatus.Init.GetCode()) ||
                     wmsInOrder.inStatus == Convert.ToInt32(InOrDtlStatus.Receipting.GetCode())) == false)
                {
                    throw new Exception($"入库单号是 {inRecepit.inNo} 的入库单的状态不是 [初始状态] 或者 [收货中]!");
                }

                DateTime dt22 = DateTime.Now;
                TimeSpan second22 = dt22 - dt1;
                dateSecond = second22.TotalSeconds.ToString();
                logger.Warn($"收货操作--> {_vpoint}：{dateSecond}");

                // 11-2 检验入库类型参数是否维护
                cfgDocTypeView = await cfgDocTypeVM.GetCfgDocTypeAsync(wmsInOrder.docTypeCode);
                bool isCfgDocTypeConfig = cfgDocTypeView != null;
                if (cfgDocTypeView != null)
                {
                    if (cfgDocTypeView.cfgDocTypeDtls.Count == 0)
                    {
                        isCfgDocTypeConfig = false;
                    }
                }

                if (isCfgDocTypeConfig == false)
                {
                    throw new Exception($"单据类型 {wmsInOrder.docTypeCode} 参数未配置!");
                }
                DateTime dt2 = DateTime.Now;
                TimeSpan second = dt2 - dt1;
                dateSecond = second.TotalSeconds.ToString();
                logger.Warn($"收货操作--> {_vpoint}：{dateSecond}");
                #endregion

                #region 12 根据入库单明细ID获取入库单明细信息
                if (inRecepit.inDtlsInfos == null || inRecepit.inDtlsInfos.Count == 0)
                {
                    throw new Exception($"单据 {inRecepit.inNo} 收货时明细数据为空!");
                }
                _vpoint = "校验入库单明细";
                //12-5 检验物料信息是否维护
                var materialCode = wmsInOrderDetails.Select(x => x.materialCode).ToList();
                basBMaterialViews = await basBMaterialVM.GetBasMaterialsAsync(materialCode);
                if (basBMaterialViews.Count == 0)
                {
                    throw new Exception($"单据 {inRecepit.inNo} 物料维护为空!");
                }
                if (basBMaterialViews.Any())
                {
                    foreach (var item in basBMaterialViews)
                    {
                        if (item == null || item.basBMaterialCategory == null ||
                        item.basBMaterial == null)
                        {
                            throw new Exception($"物料信息 {item.basBMaterial.MaterialCode} 维护不完整!");
                        }
                    }
                }

                // 入库单明细 对应的唯一码列表\
                foreach (var wmsInReceiptInParamInDtlnfoViewItem in inRecepit.inDtlsInfos)
                {
                    var wmsInReceiptUniicode = await wmsInReceiptUniicodeVM.GetWmsInReceiptUniicodeAsync(wmsInReceiptInParamInDtlnfoViewItem.uniicode);
                    var wmsInOrderDetail = wmsInOrderDetails.Where(x => x.ID == wmsInReceiptInParamInDtlnfoViewItem.inDtlId).FirstOrDefault();

                    // 12-1 检验唯一码信息是否存在
                    if (wmsInReceiptUniicode == null)
                    {
                        throw new Exception($"没有查找到入库唯一码信息: {wmsInReceiptInParamInDtlnfoViewItem.inDtlId}!");
                    }

                    // 12-2 检验唯一码状态是否正确
                    if (wmsInReceiptUniicode.runiiStatus != Convert.ToInt32(InUniicodeStatus.InitCreate.GetCode()))
                    {
                        throw new Exception($"入库唯一码 {wmsInReceiptUniicode.uniicode} 的状态不是初始创建状态!");
                    }

                    if (wmsInOrderDetail == null)
                    {
                        throw new Exception($"没有查找到入库单明细ID为: {wmsInReceiptInParamInDtlnfoViewItem.inDtlId} 的数据!");
                    }

                    // 12-3 检验唯一码对应的入库单相关信息（一致/为空)
                    if (wmsInReceiptUniicode.inDtlId==null ||
                         wmsInReceiptUniicode.inDtlId != wmsInReceiptInParamInDtlnfoViewItem.inDtlId)
                    {
                        throw new Exception($"唯一码{wmsInReceiptUniicode.uniicode}与入库单明细信息不一致!");
                    }

                    // 12-4 检验唯一码对应物料是否与入库单明细物料一致
                    if (wmsInReceiptUniicode.materialCode != wmsInOrderDetail.materialCode)
                    {
                        throw new Exception(
                            $"唯一码和入库单对应的物料信息不一致! 唯一码:{wmsInReceiptUniicode.uniicode}, 唯一码物料编码:{wmsInReceiptUniicode.materialCode},入库单物料编码:{wmsInOrderDetail.materialCode}");
                    }

                    // 12-5 检验物料信息是否维护
                    //var _basBMaterialView = basBMaterialVM.GetBasBMaterial(wmsInOrderDetail.materialCode);
                    //if (_basBMaterialView == null || _basBMaterialView.basBMaterialCategory == null ||
                    //    _basBMaterialView.basBMaterial == null)
                    //{
                    //    throw new Exception($"物料信息 {wmsInOrderDetail.materialCode} 维护不完整!");
                    //}

                    //basBMaterialViews.Add(_basBMaterialView);

                    // 12-6 检验供应商信息
                    if (wmsInOrder.docTypeCode == InOrderDocType.PO.GetCode())
                    {
                        if (wmsInOrderDetail.supplierCode.IsNullOrWhiteSpace())
                        {
                            throw new Exception($"采购订单{wmsInOrder.inNo} {wmsInOrderDetail.ID} 的供应商信息不能为空!");
                        }
                        else
                        {
                            var temp = await basBSupplierVM.GetSupplierAsync(wmsInOrderDetail.supplierCode);
                            if (temp == null)
                            {
                                throw new Exception($"采购订单{wmsInOrder.inNo} {wmsInOrderDetail.ID} 的供应商信息在系统中未找到!");
                            }
                        }
                    }

                    if (wmsInOrderDetailUniicode.ContainsKey(wmsInOrderDetail.ID))
                    {
                        wmsInOrderDetailUniicode[wmsInOrderDetail.ID].Add(wmsInReceiptUniicode);
                    }
                    else
                    {
                        wmsInOrderDetailUniicode[wmsInOrderDetail.ID] = new List<WmsInReceiptUniicode>();
                        wmsInOrderDetailUniicode[wmsInOrderDetail.ID].Add(wmsInReceiptUniicode);
                    }

                    // 如果唯一码为空，更新入库明细
                    if (wmsInReceiptUniicode.inDtlId==null)
                    {
                        wmsInReceiptUniicode.inNo = wmsInOrderDetail.inNo;
                        wmsInReceiptUniicode.inDtlId = wmsInOrderDetail.ID;
                        wmsInReceiptUniicode.UpdateBy = invoker;
                        wmsInReceiptUniicode.UpdateTime = DateTime.Now;
                        DC.UpdateEntity(wmsInReceiptUniicode);
                        DC.SaveChanges();
                        await ((DbContext)DC).Set<WmsInReceiptUniicode>().SingleUpdateAsync(wmsInReceiptUniicode);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                    }
                }

                // 12-7 根据入库单明细ID 汇总  【收货单数量】
                // 单据收货数量 小于等于 入库单明细可收货数量
                var qtySumByInDtlNo = from m in inRecepit.inDtlsInfos
                                      group m by m.inDtlId
                    into n
                                      select new
                                      {
                                          inDtlNo = n.Key,
                                          sumQty = n.Sum(p => p.receiptQty)
                                      };
                foreach (var wmsInOrderDetail in wmsInOrderDetails)
                {
                    //var remainReceiptQty = (wmsInOrderDetail.docNum - wmsInOrderDetail.receiptQty) ?? 0;
                    var remainReceiptQty = (wmsInOrderDetail.inQty - wmsInOrderDetail.receiptQty) ?? 0;
                    var temp = qtySumByInDtlNo.Where(x => x.inDtlNo == wmsInOrderDetail.ID).FirstOrDefault();
                    if (temp != null)
                    {
                        if (temp.sumQty > remainReceiptQty)
                        {
                            throw new Exception($"收货操作中入库单{wmsInOrderDetail.inNo}明细ID{wmsInOrderDetail.ID} 对应的单据数量大于入库单明细可收货数量!");
                        }
                    }
                }
                //foreach (var wmsInOrderDetail in wmsInOrderDetails)
                //{
                //    // 入库单明细 -- 可收货数量
                //    var remainReceiptQty = (wmsInOrderDetail.docNum - wmsInOrderDetail.receiptQty) ?? 0;
                //    var temp = qtySumByInDtlNo.Where(x => x.inDtlNo == wmsInOrderDetail.ID).FirstOrDefault();
                //    if (temp != null)
                //    {
                //        if (temp.sumQty > remainReceiptQty)
                //        {
                //            throw new Exception($"收货单中入库单明细ID{wmsInOrderDetail.ID} 对应的单据数量大于入库单明细可收货数量!");
                //        }
                //        //更新入库明细收货数量
                //        //wmsInOrderDetail.receiptQty = wmsInOrderDetail.receiptQty == remainReceiptQty ? wmsInOrderDetail.receiptQty : (wmsInOrderDetail.receiptQty += remainReceiptQty);
                //        wmsInOrderDetail.receiptQty += temp.sumQty;
                //        wmsInOrderDetail.updateBy = invoker;
                //        wmsInOrderDetail.updateTime = DateTime.Now;
                //        DC.UpdateEntity(wmsInOrderDetail);
                //    }
                //}
                //DC.SaveChanges();

                #endregion

                #region 13 校验物料信息

                // 在 "12-5 检验物料信息是否维护"中完成

                #endregion
                DateTime dt3 = DateTime.Now;
                TimeSpan second2 = dt3 - dt1;
                dateSecond = second2.TotalSeconds.ToString();
                logger.Warn($"收货操作--> {_vpoint}：{dateSecond}");
                #region 14 校验供应商信息

                // 14-1 采购入库供应商信息不能为空
                // 在 "12-6 检验供应商信息" 中完成

                #endregion

                #region 15 单据类型 是否是 自动验收

                _vpoint = "校验单据类型";
                var autoQcOK = false;
                var returnSRMFlag = false;
                var autoQcCfgDocTypeDtlView = cfgDocTypeView.cfgDocTypeDtls.Where(x =>
                    x.cfgDocTypeDtl.paramCode == InOrderDocTypeParam.AutoQC.GetCode()).FirstOrDefault();
                if (autoQcCfgDocTypeDtlView == null)
                {
                    throw new Exception($"单据类型 {wmsInOrder.docTypeCode} 参数未配置是否自动质检参数!");
                }

                autoQcOK = autoQcCfgDocTypeDtlView.cfgDocTypeDtl.paramValueCode == YesNoCode.Yes.GetCode();
                var returnSRMCfgDocTypeDtlView = cfgDocTypeView.cfgDocTypeDtls.Where(x =>
                    x.cfgDocTypeDtl.paramCode == InOrderDocTypeParam.ReturnFlag.GetCode()).FirstOrDefault();
                if (returnSRMCfgDocTypeDtlView == null)
                {
                    throw new Exception($"单据类型 {wmsInOrder.docTypeCode} 参数未配置是否回传SRM参数!");
                }

                returnSRMFlag = returnSRMCfgDocTypeDtlView.cfgDocTypeDtl.paramValueCode == YesNoCode.Yes.GetCode();

                // 单据类型是 委外入库 或 采购入库
                var poVsOemDocType = (cfgDocTypeView.cfgDocType.docTypeCode == InOrderDocType.PO.GetCode())
                                     || (cfgDocTypeView.cfgDocType.docTypeCode == InOrderDocType.OEM.GetCode());

                #endregion

                #region 16 通过楼号(区域)获取对应的收货待检区

                _vpoint = "通过楼号(区域)获取对应的收货待检区";
                basWRegion = await basWRegionVM.GetRegionByRegionTypeAsync(wmsInOrder.areaNo, WRgionTypeCode.RC.GetCode());
                if (basWRegion == null)
                {
                    throw new Exception($"区域{wmsInOrder.areaNo} 对应的库区 没有配置 收货暂存区 信息!");
                }

                basWBins = await basWBinVM.GetBinByRegionNoAsync(wmsInOrder.areaNo, basWRegion.regionNo);
                if (basWBins == null)
                {
                    throw new Exception($"系统未配置区域{wmsInOrder.areaNo}的 收货暂存区 的库位信息!");
                }

                #endregion

                #endregion
                DateTime dt4 = DateTime.Now;
                TimeSpan second3 = dt4 - dt1;
                dateSecond = second3.TotalSeconds.ToString();
                logger.Warn($"收货操作-->{_vpoint}：{dateSecond}");
                #region 2  逻辑

                #region 21 生成收货单信息

                _vpoint = "生成收货单信息";
                //// 21-1 生成收货单单号
                //string inReceiptNo = sysSequenceVM.GetSequence(SequenceCode.InReceiptNo.GetCode());

                var inReceiptInfo = CreateInReceiptInfo();

                // 21-2 创建收货单主表信息
                wmsInReceipt = BuildWmsInReceiptByWmsInOrder(wmsInOrder, basWRegion.regionNo, basWBins.binNo, inReceiptNo, returnSRMFlag, invoker);

                // 2023/10/30 批量添加修改
                //DC.AddEntity(wmsInReceipt);
                //DC.SaveChanges();
                await ((DbContext)DC).BulkInsertAsync(new WmsInReceipt[] { wmsInReceipt });

                // 21-3 创建收货单明细数据
                //质检方式，为了区别采购入库、委外入库类型单据收货完成后，不直接在质检页面显示，
                string iqcFlag = string.Empty;
                if (poVsOemDocType == false)
                {
                    iqcFlag = IQCTypeFlag.WMS.GetCode();
                }
                else
                {
                    iqcFlag = IQCTypeFlag.EBS.GetCode();
                }

                List<WmsInReceiptUniicode> wmsInReceiptUniicodeBulk = new List<WmsInReceiptUniicode>();
                for (int i = 0; i < wmsInOrderDetailUniicode.Keys.Count; i++)
                {

                    long item = wmsInOrderDetailUniicode.Keys.ElementAt(i);
                    var inreceiptUnii = wmsInOrderDetailUniicode[item];
                    var detail = wmsInOrderDetails.Where(x => x.ID == item).FirstOrDefault();
                    // 入库单明细 -- 可收货数量
                    //var remainReceiptQty = (detail.docNum - detail.receiptQty) ?? 0;
                    var remainReceiptQty = (detail.inQty - detail.receiptQty) ?? 0;
                    var temp = qtySumByInDtlNo.Where(x => x.inDtlNo == detail.ID).FirstOrDefault();
                    if (temp != null)
                    {
                        WmsInReceiptDtl wmsInReceiptDt = BuildWmsInReceiptDtlByWmsInOrder(wmsInReceipt, detail, invoker);
                        // 把 extend1 扩展字段就是 WMS收货单行号,不能直接拷贝入库单明细的extend1
                        wmsInReceiptDt.extend1 = (i + 1).ToString();
                        //质检方式，为了区别采购入库、委外入库类型单据收货完成后，不直接在质检页面显示，其他入库类型直接在质检页面显示
                        wmsInReceiptDt.extend2 = iqcFlag;
                        wmsInReceiptDt.receiptQty = temp.sumQty;
                        if (string.IsNullOrWhiteSpace(wmsInReceiptDt.batchNo))
                        {
                            wmsInReceiptDt.batchNo = inreceiptUnii.FirstOrDefault()?.batchNo;
                        }
                        //DC.AddEntity(wmsInReceiptDt);
                        wmsInReceiptDts.Add(wmsInReceiptDt);
                        // 更新入库唯一码表的收货单信息
                        foreach (var obj in wmsInOrderDetailUniicode[item])
                        {
                            obj.receiptNo = wmsInReceipt.receiptNo;
                            obj.receiptDtlId = wmsInReceiptDt.ID;
                            if (obj.runiiStatus != Convert.ToInt32(InUniicodeStatus.InitCreate.GetCode()))
                            {
                                throw new Exception($"入库唯一码 {obj.uniicode} 状态不是 初始创建！");
                            }

                            obj.runiiStatus = Convert.ToInt32(InUniicodeStatus.ReceiptFinished.GetCode());
                            obj.UpdateBy = invoker;
                            obj.UpdateTime = DateTime.Now;
                            wmsInReceiptUniicodeBulk.Add(obj);
                            //DC.UpdateEntity(obj);
                        }
                    }
                }

                // 2023/10/30 批量添加修改
                //DC.Set<WmsInReceiptDt>().AddRange(wmsInReceiptDts);
                await ((DbContext)DC).BulkInsertAsync(wmsInReceiptDts);
                await ((DbContext)DC).BulkUpdateAsync(wmsInReceiptUniicodeBulk);

                //DC.SaveChanges();

                /*
                // 21-4 回传SRM 这个在25-2，这段不需要了， modified by Allen 2023年9月27日 09:59:43
                _vpoint = "回传SRM";
                if (returnSRMFlag)
                {
                    // add by Allen 写入回传表 收货回传SRM001(从SRM收货来的，就需要回传) 这个地方改为送货单号
                    var itfRresult = itfReturnVm.addItfReturn(wmsInOrder.docTypeCode, "", "SRM001", wmsInOrder.externalInNo);
                    if (itfRresult.code == ResultCode.Error)
                    {
                        throw new Exception(itfRresult.msg);
                    }
                }
                */

                #endregion

                DateTime dt5 = DateTime.Now;
                TimeSpan second4 = dt5 - dt1;
                dateSecond = second4.TotalSeconds.ToString();
                logger.Warn($"收货操作-->{_vpoint}：{dateSecond}");
                #region 22 生成收货暂存区库存

                _vpoint = "生成收货暂存区库存";
                // 22-1 库存主表
                string inReceiptTStockNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.InReceiptTStockNo.GetCode());
                wmsStock = await BuildInReceiptStockAsync(wmsInReceipt, inReceiptTStockNo, invoker);

                // 2023/10/30 批量添加修改
                //DC.AddEntity(wmsStock);
                //DC.SaveChanges();
                await ((DbContext)DC).BulkInsertAsync(new WmsStock[] { wmsStock });

                // 22-2 库存明细表
                List<WmsInReceiptUniicode> wmsInReceiptUniicodeBulk2 = new List<WmsInReceiptUniicode>();
                List<WmsStockDtl> wmsStockDtls = new List<WmsStockDtl>();
                foreach (var item in wmsInReceiptDts)
                {
                    WmsStockDtl wmsStockDtl = BuildInReceiptStockDtl(wmsStock, item, invoker);
                    //DC.AddEntity(wmsStockDtl);
                    wmsStockDtls.Add(wmsStockDtl);
                    //第一种
                    if (wmsInOrderDetailUniicode.ContainsKey(item.inDtlId))
                    {
                        var uniiInfos = wmsInOrderDetailUniicode[item.inDtlId];
                        foreach (var obj in uniiInfos)
                        {
                            //obj.palletBarcode = wmsStockDtl.palletBarcode;
                            obj.curPalletBarcode = wmsStockDtl.palletBarcode;
                            //obj.stockCode = inReceiptTStockNo;
                            obj.curStockCode = inReceiptTStockNo;
                            //obj.stockDtlId = wmsStockDtl.ID;
                            obj.curStockDtlId = wmsStockDtl.ID;
                            obj.UpdateBy = invoker;
                            obj.UpdateTime = DateTime.Now;
                            //DC.UpdateEntity(obj);
                            wmsInReceiptUniicodeBulk2.Add(obj);

                        }
                    }
                    //第二种
                    //var unii = wmsInOrderDetailUniicode.Where(x => x.Key == item.inDtlId).Select(t => t.Value).FirstOrDefault();
                    //foreach (var obj in unii)
                    //{
                    //    obj.palletBarcode = wmsStockDtl.palletBarcode;
                    //    obj.stockCode = inReceiptTStockNo;
                    //    obj.stockDtlId = wmsStockDtl.ID;
                    //    obj.updateBy = invoker;
                    //    obj.updateTime = DateTime.Now;
                    //    DC.UpdateEntity(obj);
                    //}
                    //第三种
                    //foreach (var items in wmsInOrderDetailUniicode.Keys)
                    //{
                    //    if (item.inDtlId == items)
                    //    {
                    //        // 更新入库唯一码表的库存信息
                    //        foreach (var obj in wmsInOrderDetailUniicode[items])
                    //        {
                    //            obj.palletBarcode = wmsStockDtl.palletBarcode;
                    //            obj.stockCode = inReceiptTStockNo;
                    //            obj.stockDtlId = wmsStockDtl.ID;
                    //            obj.updateBy = invoker;
                    //            obj.updateTime = DateTime.Now;
                    //            DC.UpdateEntity(obj);
                    //        }
                    //    }
                    //}
                }

                // 2023/10/30 批量添加修改
                //DC.SaveChanges();
                await ((DbContext)DC).BulkInsertAsync(wmsStockDtls);
                await ((DbContext)DC).BulkUpdateAsync(wmsInReceiptUniicodeBulk2);

                // 22-3 库存唯一码表
                foreach (var item in wmsInOrderDetailUniicode.Values)
                {
                    foreach (var obj in item)
                    {
                        //WmsStockUniicode wmsStockUniicode = BuildWmsStockUniicode(obj, basBMaterialViews, invoker);
                        //DC.AddEntity(wmsStockUniicode);
                        wmsInUniicodes.Add(obj);
                        //wmsStockUniicodes.Add(wmsStockUniicode);
                    }


                }

                // 2023/10/30 批量添加修改
                //DC.Set<WmsStockUniicode>().AddRange(wmsStockUniicodes);
                //((DbContext)DC).BulkInsert(wmsStockUniicodes);

                //DC.SaveChanges();
                #endregion
                DateTime dt6 = DateTime.Now;
                TimeSpan second5 = dt6 - dt1;
                dateSecond = second5.TotalSeconds.ToString();
                logger.Warn($"收货操作-->{_vpoint}：{dateSecond}");
                #region 23 生成库存调整记录

                _vpoint = "生成库存调整记录";
                //List<WmsStockAdjust> _wmsStockAdjust = new List<WmsStockAdjust>();
                //foreach (var item in wmsStockUniicodes)
                //{
                //    WmsStockAdjust wmsStockAdjust = CreateInReceiptStockAdjust(item, invoker);
                //    _wmsStockAdjust.Add(wmsStockAdjust);
                //    //DC.AddEntity(wmsStockAdjust);
                //}

                //// 2023/10/30 批量添加修改
                ////DC.SaveChanges();
                //((DbContext)DC).BulkInsert(_wmsStockAdjust);

                #endregion
                DateTime dt10 = DateTime.Now;
                TimeSpan second9 = dt10 - dt1;
                dateSecond = second9.TotalSeconds.ToString();
                logger.Warn($"收货操作-->{_vpoint}：{dateSecond}");
                #region 24 更新入库单

                _vpoint = "更新入库单";

                //20231020根据数量更新收货单数量

                // 更新主表状态--收货完成
                wmsInOrder.inStatus = Convert.ToInt32(InOrDtlStatus.Receipted.GetCode());
                wmsInOrder.UpdateBy = invoker;
                wmsInOrder.UpdateTime = DateTime.Now;


                // 2023/10/30 批量添加修改
                //DC.UpdateEntity(wmsInOrder);
                //DC.SaveChanges();
                await ((DbContext)DC).BulkUpdateAsync(new WmsInOrder[] { wmsInOrder });

                // 更新明细表状态--收货完成
                // 更新入库单明细的收货数量
                List<WmsInOrderDtl> wmsInOrderDetailBulk = new List<WmsInOrderDtl>();
                foreach (var item in wmsInOrderDetailUniicode.Keys)
                {
                    var obj = wmsInOrderDetails.Where(x => x.ID == item).FirstOrDefault();

                    // 本次收货数量
                    var temp = qtySumByInDtlNo.Where(x => x.inDtlNo == item).FirstOrDefault().sumQty;
                    //todo:修改状态
                    obj.receiptQty = (obj.receiptQty ?? 0) + temp;
                    obj.inDtlStatus = Convert.ToInt32(InOrDtlStatus.Receipted.GetCode());
                    obj.UpdateBy = invoker;
                    obj.UpdateTime = DateTime.Now;
                    wmsInOrderDetailBulk.Add(obj);
                    //DC.UpdateEntity(obj);
                }

                // 2023/10/30 批量添加修改
                await ((DbContext)DC).BulkUpdateAsync(wmsInOrderDetailBulk);
                await ((DbContext)DC).BulkSaveChangesAsync();

                //DC.SaveChanges();
                //查看入库单明细还有未收货完成的，一并更新状态不更新数量
                //var inOrderDtlList=DC.Set<WmsInOrderDetail>().Where(x=>x.inNo== wmsInOrder.inNo && (x.inDtlStatus=="0" || x.inDtlStatus == "11")).ToList();
                //foreach (var item in inOrderDtlList)
                //{
                //    item.inDtlStatus= InOrDtlStatus.Receipted.GetCode();
                //    item.updateBy = invoker;
                //    item.updateTime = DateTime.Now;
                //    DC.UpdateEntity(item);
                //}
                //DC.SaveChanges();
                #endregion
                DateTime dt7 = DateTime.Now;
                TimeSpan second6 = dt7 - dt1;
                dateSecond = second6.TotalSeconds.ToString();
                logger.Warn($"收货操作-->{_vpoint}：{dateSecond}");

                //throw new Exception("hahahah");

                #region 25 处理单据类型为非采购入库/委外入库

                _vpoint = "处理单据类型为非采购入库/委外入库";
                DateTime dt9 = DateTime.Now;
                TimeSpan second8 = dt9 - dt1;
                dateSecond = second8.TotalSeconds.ToString();
                logger.Warn($"收货操作-->{_vpoint}：{dateSecond}");
                // 25-1 处理自动触发质检 非(采购入库/委外入库) 直接触发质检合格
                if (poVsOemDocType == false)
                {
                    //25-2 处理同批次 非(采购入库/委外入库) 类型的电子料，同一批次已经维护过电子料信息，将维护的信息同步到该收货完成的信息中
                    #region 注释
                    //foreach (var item in basBMaterialViews)
                    //{
                    //    var isElectronicMaterial = item.basBMaterialCategory.materialFlag == MaterialFlag.Electronic.GetCode();
                    //    if (isElectronicMaterial)
                    //    {
                    //        try
                    //        {
                    //            var batchNos = wmsInReceiptDts.Select(x => x.batchNo).Distinct().ToList();
                    //            foreach (var batchNo in batchNos)
                    //            {
                    //                var uniiCodeEleInfos = await wmsInReceiptUniicodeVM.GetUniicodeByBatchNoAsync(batchNo);
                    //                if (uniiCodeEleInfos.Any())
                    //                {
                    //                    //取这个批次的维护电子料信息的最新一条，业主确认过
                    //                    var uniiCode = uniiCodeEleInfos.OrderByDescending(x => x.dataCode).First();
                    //                    foreach (var updateInuniicode in wmsInUniicodes)
                    //                    {
                    //                        updateInuniicode.dataCode = uniiCode.dataCode;
                    //                        updateInuniicode.mslGradeCode = uniiCode.mslGradeCode;
                    //                        updateInuniicode.supplierExposeTimeDuration = uniiCode.supplierExposeTimeDuration;
                    //                        updateInuniicode.productDate = GetWeekStartTime(uniiCode.dataCode);
                    //                        updateInuniicode.expDate = GetExpDate(GetWeekStartTime(uniiCode.dataCode), item.basBMaterial.ematerialVtime);
                    //                        updateInuniicode.updateBy = invoker;
                    //                        updateInuniicode.updateTime = DateTime.Now;
                    //                        //DC.UpdateEntity(updateInuniicode);

                    //                    }
                    //                    await ((DbContext)DC).BulkUpdateAsync(wmsInUniicodes);
                    //                    await ((DbContext)DC).BulkSaveChangesAsync();
                    //                    //foreach (var updateStockuniicode in wmsStockUniicodes)
                    //                    //{
                    //                    //    updateStockuniicode.dataCode = uniiCode.dataCode;
                    //                    //    updateStockuniicode.mslGradeCode = uniiCode.mslGradeCode;
                    //                    //    updateStockuniicode.productDate = GetWeekStartTime(uniiCode.dataCode);
                    //                    //    updateStockuniicode.expDate = GetExpDate(GetWeekStartTime(uniiCode.dataCode), item.basBMaterial.ematerialVtime);
                    //                    //    updateStockuniicode.supplierExposeTimes = uniiCode.supplierExposeTimeDuration;
                    //                    //    updateStockuniicode.realExposeTimes = uniiCode.supplierExposeTimeDuration;
                    //                    //    updateStockuniicode.updateBy = invoker;
                    //                    //    updateStockuniicode.updateTime = DateTime.Now;
                    //                    //    DC.UpdateEntity(updateStockuniicode);
                    //                    //}
                    //                    //DC.SaveChanges();
                    //                }
                    //            }
                    //        }
                    //        catch (Exception ex)
                    //        {

                    //            throw new Exception(ex.Message);
                    //        }

                    //    }
                    //}
                    #endregion


                    // 自动质检
                    if (autoQcOK)
                    {
                        foreach (var item in wmsInReceiptDts)
                        {
                            DoReceiptIqcRecordDto wmsItnQcInParamView = new DoReceiptIqcRecordDto();
                            wmsItnQcInParamView.inReceiptDtlId = item.ID;
                            item.qualifiedQty = item.receiptQty;
                            wmsItnQcInParamView.passQty = (decimal)item.qualifiedQty;
                            wmsItnQcInParamView.noPassQty = 0;
                            //wmsItnQcInParamView.passQty = (decimal)item.receiptQty;
                            wmsItnQcInParamView.detailDescription = "收货自动触发质检合格";
                            if (wmsItnQcInParamView.passQty != 0)
                            {
                                var res = await wmsInReceiptIqcRecordVM.DoInReceiptIqc(wmsItnQcInParamView, invoker);
                                if (res.code == ResCode.Error)
                                {
                                    throw new Exception(res.msg);
                                }
                            }
                        }
                    }
                }

                #endregion

                #endregion
                #region 调用ERP接口回传收货结果
                //_vpoint = "调用ERP接口回传收货结果";
                //if (poVsOemDocType == true)
                //{
                //    DateTime dt30 = DateTime.Now;
                //    // 回传不成功，直接回退，已和客户确认
                //    string sJ013In =await BuildSJ013InParamsAsync(wmsInReceipt);
                //    // 25-1 调用ERP接口回传收货结果
                //    //BusinessResult bResultSJ013 = itfReturnVm.addItfReturn(wmsInOrder.docTypeCode, wmsInReceipt.receiptNo, wmsInReceipt.ID, "", "SJ013", sJ013In, invoker);
                //    BusinessResult businessResultSJ013 =await extVM.SJ013WMSPurchaseReceivesImportAsync(sJ013In, null);
                //    DateTime dt31 = DateTime.Now;
                //    TimeSpan second30 = dt31 - dt30;
                //    dateSecond = second30.TotalSeconds.ToString();
                //    if (businessResultSJ013.code == ResultCode.Error)
                //    {
                //        logger.Warn($"收货操作-->{_vpoint}：{DateTime.Now},用时{dateSecond}秒，收货单号{wmsInReceipt.inNo},调用ERP接口回传收货失败，原因{businessResultSJ013.msg}");
                //        throw new Exception($"异常位置: [ {_vpoint} ], 异常信息:{businessResultSJ013.msg}");
                //    }
                //}
                #endregion

                DateTime dt8 = DateTime.Now;
                TimeSpan second7 = dt8 - dt1;
                dateSecond = second7.TotalSeconds.ToString();
                logger.Warn($"收货操作-->到达事务提交点：{dateSecond}");
                if (hasParentTranslation == false)
                {
                    await DC.Database.CommitTransactionAsync();
                }

                // 25-2 处理单据类型为采购入库/委外入库

                _vpoint = "处理单据类型为采购入库/委外入库回传ERP数据";
                if (poVsOemDocType == true)
                {
                    
                }
                DateTime dt11 = DateTime.Now;
                TimeSpan second10 = dt11 - dt1;
                dateSecond = second10.TotalSeconds.ToString();
                logger.Warn($"收货操作-->{_vpoint}：{dateSecond}");
            }
            catch (Exception ex)
            {
                if (hasParentTranslation == false)
                {
                    if (DC.Database.CurrentTransaction != null)
                    {
                        await DC.Database.RollbackTransactionAsync();
                    }
                }

                result.code = ResCode.Error;
                result.msg = $"WmsInReceiptVM->DoReceipt 执行异常! 异常位置: [ {_vpoint} ], 异常信息: [ {ex.Message} ]";
            }
            logger.Warn($"收货操作-->{_vpoint}：{DateTime.Now},入参{inparam},收货结果{result.msg}");
            return result;
        }

    }
}
