using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using AutoMapper;
using Com.Wish.Model.Base;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz.Util;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using System.DirectoryServices.Protocols;
using System.Globalization;
using Wish.Areas.BasWhouse.Model;
using WISH.Helper.Common;
using Wish.ViewModel.Base.BasBMaterialVMs;
using Wish.ViewModel.BasWhouse.BasWPalletTypeVMs;
using Wish.ViewModel.Common;
using Wish.ViewModel.Config.CfgDocTypeVMs;
using Wish.ViewModel.Common.Dtos;
using Wish.ViewModel.BusinessIn.WmsInReceiptIqcResultVMs;
using Wish.ViewModel.BusinessIn.WmsInReceiptUniicodeVMs;
using Wish.ViewModel.BasWhouse.BasWErpWhouseBinVMs;
using Wish.ViewModel.BusinessStock.WmsStockVMs;
using Wish.ViewModel.Config.CfgRelationshipVMs;
using Wish.ViewModel.BasWhouse.BasWRegionVMs;
using Wish.ViewModel.BasWhouse.BasWBinVMs;
using Wish.ViewModel.System.SysSequenceVMs;
using Wish.ViewModel.BusinessStock.WmsStockUniicodeVMs;
using Wish.ViewModel.BusinessIn.WmsInOrderVMs;
using Wish.ViewModel.BusinessIn.WmsInReceiptVMs;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Wish.Areas.Config.Model;
using Org.BouncyCastle.Pqc.Crypto.Frodo;
using MediatR;
using Wish.DtoModel.Common.Dtos;

namespace Wish.ViewModel.BusinessPutaway.WmsPutawayVMs
{
    public partial class WmsPutawayVM
    {
        /// <summary>
        /// 组盘上架
        /// </summary>
        /// <returns></returns>
        public async Task<BusinessResult> DoPutaway(DoPutawayInParamsDto inParams, string invoker)
        {
            BusinessResult result = new BusinessResult();
            var hasParentTransaction = false;
            string _vpoint = "";
            // 回调接口编码
            string itfCode = "";
            // 回调入参
            string sjParam = "";
            // 回调返回结果
            BusinessResult sjBusinessResult = new BusinessResult();
            // 入参json
            string inJson = JsonConvert.SerializeObject(inParams);
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

                WmsInReceiptIqcResultVM wmsInReceiptIqcResultVM = Wtm.CreateVM<WmsInReceiptIqcResultVM>();
                WmsInReceiptUniicodeVM wmsInReceiptUniicodeVM = Wtm.CreateVM<WmsInReceiptUniicodeVM>();
                BasWPalletTypeVM basWPalletTypeVM = Wtm.CreateVM<BasWPalletTypeVM>();
                BasWErpWhouseBinVM basWErpWhouseBinVM = Wtm.CreateVM<BasWErpWhouseBinVM>();
                WmsStockVM wmsStockVM = Wtm.CreateVM<WmsStockVM>();
                CfgRelationshipVM cfgRelationshipVM = Wtm.CreateVM<CfgRelationshipVM>();
                BasBMaterialVM basBMaterialVM = Wtm.CreateVM<BasBMaterialVM>();
                BasWRegionVM basWRegionVM = Wtm.CreateVM<BasWRegionVM>();
                BasWBinVM basWBinVM = Wtm.CreateVM<BasWBinVM>();
                SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
                WmsStockUniicodeVM wmsStockUniicodeVM = Wtm.CreateVM<WmsStockUniicodeVM>();
                WmsPutawayVM wmsPutawayVM = Wtm.CreateVM<WmsPutawayVM>();
                WmsInOrderVM wmsInOrderVM = Wtm.CreateVM<WmsInOrderVM>();
                CfgDocTypeVM cfgDocTypeVM = Wtm.CreateVM<CfgDocTypeVM>();
                WmsInReceiptVM wmsInReceiptVM = Wtm.CreateVM<WmsInReceiptVM>();
                var dateSecond = string.Empty;
                List<WmsStockUniicode> nobatchStockUniicode = new List<WmsStockUniicode>();
                List<WmsInReceiptUniicode> wmsinReceiptUniicodeLists = new List<WmsInReceiptUniicode>();
                List<string> uniiCodeLists = new List<string>();
                List<WmsInReceiptRecord> addReceiptRecordLists = new List<WmsInReceiptRecord>();
                List<WmsStockDtl> updateStockDtlLists = new List<WmsStockDtl>();
                List<WmsStockDtl> addStockDtlLists = new List<WmsStockDtl>();
                //List<WmsStockDtl> allStockDtlLists = new List<WmsStockDtl>();
                List<WmsStockUniicode> stockUniicodereturnLists = new List<WmsStockUniicode>();
                #region 1  校验

                #region 11 通过质检结果单号获取质检结果信息

                _vpoint = "11 通过质检结果单号获取质检结果信息";
                DateTime dt100 = DateTime.Now;
                decimal uniicodeQtys = inParams.uniicodes.Values.Sum();
                // 11-1 检查质检结果单据状态
                WmsInReceiptIqcResult wmsInReceiptIqcResult = await wmsInReceiptIqcResultVM.GetWmsInReceiptIqcResultAsync(inParams.iqcResultNo);
                // todo add by Allen 添加查看数据库查询出来的数据
                logger.Warn($"----->WmsPutawayVM.DoPutaway----->最初查询---数据库查询数据：组盘上架，操作人:{invoker}，质检结果单单号:{wmsInReceiptIqcResult.iqcResultNo}，组盘数量:{wmsInReceiptIqcResult.recordQty}，入参唯一码数量:{uniicodeQtys}，质检结果数量:{wmsInReceiptIqcResult.qty}，检验结果状态:{wmsInReceiptIqcResult.iqcResultStatus}");
                // add by Allen 查询
                if ((wmsInReceiptIqcResult.iqcResultStatus == Convert.ToInt32(IqcResultStatus.Init.GetCode()) ||
                     wmsInReceiptIqcResult.iqcResultStatus == Convert.ToInt32(IqcResultStatus.PutAwaying.GetCode())) == false)
                {
                    throw new Exception($"质检结果单号 {inParams.iqcResultNo} 的单据状态不是 初始创建 或 组盘中!");
                }

                // 11-2 只有 合格 和 ERP特采的才可以组盘上架
                if ((wmsInReceiptIqcResult.inspectionResult == Convert.ToInt32(InspectionResult.Qualitified.GetCode()) ||
                     wmsInReceiptIqcResult.inspectionResult == Convert.ToInt32(InspectionResult.AcceptOnDeviation.GetCode())) == false)
                {
                    throw new Exception($"质检单 {inParams.iqcResultNo} 的质检结果不是 【合格】 或 【ERP特采】，不可以组盘上架!");
                }
                string inspectionResult = InspectionResult.Qualitified.GetCode();
                BasBMaterialDto basBMaterialView = await basBMaterialVM.GetBasBMaterialAsync(wmsInReceiptIqcResult.materialCode);

                var regionNo = wmsInReceiptIqcResult.regionNo; // 库区编号
                var erpWhouseNo = wmsInReceiptIqcResult.erpWhouseNo; // ERP仓库编码
                var areaNo = wmsInReceiptIqcResult.areaNo; // 区域编号

                // 11-3 检查待组盘数量与唯一码汇总数量对比
                //decimal uniicodeQtys = inParams.uniicodes.Values.Sum();
                if ((wmsInReceiptIqcResult.qty - wmsInReceiptIqcResult.recordQty) < uniicodeQtys)
                {
                    throw new Exception($"质检结果单号 {inParams.iqcResultNo} 的待组盘数量{(wmsInReceiptIqcResult.qty - wmsInReceiptIqcResult.recordQty)} 小于 本次组盘数量{uniicodeQtys}");
                }
                ///暂时发布
                //if (wmsInReceiptIqcResult.docTypeCode != BusinessCode.InPurchase.GetCode()
                //       && wmsInReceiptIqcResult.docTypeCode != BusinessCode.OEM.GetCode()
                //       && wmsInReceiptIqcResult.docTypeCode != BusinessCode.InTransferRequest.GetCode())
                //{
                //    if ((wmsInReceiptIqcResult.qty - wmsInReceiptIqcResult.recordQty) != uniicodeQtys)
                //    {
                //        throw new Exception($"该单据质检结果单号 {inParams.iqcResultNo} 的待组盘数量{(wmsInReceiptIqcResult.qty - wmsInReceiptIqcResult.recordQty)}与条码数量{uniicodeQtys}不相等，不可提交");
                //    }
                //}
                DateTime dt101 = DateTime.Now;
                TimeSpan second101 = dt101 - dt100;
                dateSecond = second101.TotalSeconds.ToString();
                logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}用时：{dateSecond}");
                #endregion

                #region 12 载体/库位校验：通过编码规则判断
                _vpoint = "12 载体/库位校验：通过编码规则判断";
                BasWPalletTypeDto basWPalletTypeView = await basWPalletTypeVM.GetPalletTypeAsync(inParams.barCode);
                //if (basWPalletTypeView.palletType== PalletTypeExtend.UnKnown)
                //{
                //    throw new Exception($"托盘{inParams.barCode}规则不正确!");
                //}
                WmsStock wmsStock = null;
                List<WmsStockDtl> stockDetls = null;

                // 是否是平库组盘
                bool isPK = false;

                // 托盘编码
                var palletBarcode = inParams.barCode;

                // 立库 | 平库托盘
                if (basWPalletTypeView.palletType == PalletTypeExtend.Pallet || basWPalletTypeView.palletType == PalletTypeExtend.Box || basWPalletTypeView.palletType == PalletTypeExtend.Steel)
                {
                    var isPallet = basWPalletTypeView.palletType == PalletTypeExtend.Pallet;
                    var isBox = basWPalletTypeView.palletType == PalletTypeExtend.Box;
                    var prefix = isPallet ? "托盘" : (isBox ? "料箱" : "钢托盘");
                    var wRegionDeviceFlag = isPallet ? WRegionDeviceFlag.Pallet : (isBox ? WRegionDeviceFlag.Box : WRegionDeviceFlag.Pallet);

                    // 13-1 区域+ERP仓库是否存在托盘库库位
                    // 从质检结果单 获取 ERP仓库编码  区域编号 筛选出立库的库区
                    // >> ERP仓库对应库位表  ERP仓库编码 + 区域编号 + 库区编码  -> 库位

                    // 13-3 托盘是否存在库存
                    // 托盘库存状态校验：初始创建状态
                    // >> 库存表 WMS_STOCK
                    wmsStock = await wmsStockVM.GetWmsStockByPalletCodeAsync(inParams.barCode);
                    if (wmsStock != null)
                    {
                        if (wmsStock.stockStatus != Int16.Parse(StockStatus.InitCreate.GetCode()))
                        {
                            throw new Exception($"{prefix} {inParams.barCode}在库存中已存在，但是不是初始创建状态!");
                        }
                    }

                    // 13-4 是否存在上架单
                    // 如果没有则创建
                    // bool isExistPutway = CheckExistPutwayByPalletCode(inParams.barCode);

                    // 13-5 是否属于同一个ERP仓库、区域
                    // 不允许混ERP仓库/区域
                    // >> 从 WMS_STOCK_DTL（ERP仓库、区域）
                    if (wmsStock != null)
                    {
                        stockDetls = await wmsStockVM.GetWmsStockDtlAsync(wmsStock.stockCode);
                        //if (stockDetls != null)
                        if (stockDetls.Any())
                        {
                            //int size = stockDetls.Where(x => x.erpWhouseNo == erpWhouseNo && x.areaNo == areaNo).Count();
                            int size = stockDetls.Where(x => x.erpWhouseNo == erpWhouseNo).Count();
                            if (size != stockDetls.Count)
                            {
                                //throw new Exception($"托盘{inParams.barCode}在库存中不是同一个 ERP仓库 和 区域!");
                                throw new Exception($"托盘{inParams.barCode}在库存中不是同一个 ERP仓库!");
                            }
                        }
                    }
                }
                else
                {
                    //库位是否属于ERP仓库，不对区域做校验
                    var isErpWhouseBin = await basWErpWhouseBinVM.CheckIsErpWhouseBinAsync(erpWhouseNo, inParams.barCode);
                    if (isErpWhouseBin == false)
                    {
                        throw new Exception($"库位{inParams.barCode} 不是 ERP仓库 {erpWhouseNo} 的货位!");
                    }

                    isPK = true;
                }
                DateTime dt102 = DateTime.Now;
                TimeSpan second102 = dt102 - dt100;
                dateSecond = second102.TotalSeconds.ToString();
                logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}用时：{dateSecond}");
                #endregion

                #region 无批次时，通过批次生成唯一码后，上架回写补充批次生成的唯一码信息
                _vpoint = "无批次时，通过批次生成唯一码后，上架回写补充批次生成的唯一码信息";
                CfgDocTypeDto cfgDocTypeView = await cfgDocTypeVM.GetCfgDocTypeAsync(wmsInReceiptIqcResult.docTypeCode);

                // 创建单据类型

                //cfgDocTypeView =await cfgDocTypeVM.GetCfgDocTypeAsync(wmsInReceiptIqcResult.docTypeCode);

                if (cfgDocTypeView.cfgDocTypeDtls.Count == 0)
                {
                    throw new Exception($"单据类型 {wmsInReceiptIqcResult.docTypeCode} 参数未配置!");
                }

                // 获取单据类型相关配置参数
                // isBatch==False,不批次管理
                bool isBatch = cfgDocTypeView.GetParamCodeValue(InOrderDocTypeParam.IsBatch.GetCode()) == YesNoCode.Yes.GetCode();
                // 借入借用单 类型单据 且没有批次的情况，走无批次处理流程
                bool isSpecialDoc = (wmsInReceiptIqcResult.docTypeCode == BusinessCode.InBorrowingSlip.GetCode() && wmsInReceiptIqcResult.batchNo.IsNullOrWhiteSpace());
                if (isSpecialDoc)
                {
                    isBatch = false;
                }


                DateTime dt103 = DateTime.Now;
                TimeSpan second103 = dt103 - dt100;
                dateSecond = second103.TotalSeconds.ToString();
                logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}用时：{dateSecond}");
                #endregion


                #region 13 唯一码校验
                _vpoint = "13 唯一码校验";
                foreach (var uniicode in inParams.uniicodes.Keys)
                {
                    decimal qty = inParams.uniicodes[uniicode];
                    // 成品：入库唯一码收货明细ID是否一致，非成品存在入库唯一码无单据信息
                    var isProduct = basBMaterialView.basBMaterialCategory.materialFlag == MaterialFlag.Product.GetCode();
                    // 13-1 是否存在入库唯一码表信息 WMS_IN_RECEIPT_UNIICODE
                    WmsInReceiptUniicode wmsInReceiptUniicode = await wmsInReceiptUniicodeVM.GetWmsInReceiptUniicodeAsync(uniicode);
                    uniiCodeLists.Add(uniicode);
                    if (wmsInReceiptUniicode == null)
                    {
                        throw new Exception($"在入库唯一码表中未找到入库唯一码 {uniicode} 的信息!");
                    }

                    if (0 >= inParams.uniicodes[uniicode] || wmsInReceiptUniicode.qty < inParams.uniicodes[uniicode])
                    {
                        throw new Exception($"唯一码数量{inParams.uniicodes[uniicode]}小于等于0，或与库存数量{wmsInReceiptUniicode.qty}不一致!");
                    }
                    // 13-2 检验收货明细ID是否一致


                    if (wmsInReceiptUniicode.materialCode != basBMaterialView.basBMaterial.MaterialCode)
                    {
                        throw new Exception($"质检结果物料：{basBMaterialView.basBMaterial.MaterialCode} 与 入库唯一码物料：{wmsInReceiptUniicode.materialCode} 不一致!");
                    }
                    #region 13.1 处理部分业务唯一码与单据无匹配关系的情况
                    //判断上架的入库唯一码是不是与收货信息有关联管理，没有关系，绑定关系 不属于收货阶段的入库唯一吗
                    if (wmsInReceiptUniicode.receiptDtlId == null && wmsInReceiptUniicode.receiptNo.IsNullOrWhiteSpace())
                    {

                        //获取收货阶段的入库唯一码
                        var receiptUniicode = await DC.Set<WmsInReceiptUniicode>().Where(x => x.materialCode == wmsInReceiptUniicode.materialCode && x.batchNo == wmsInReceiptUniicode.batchNo && x.receiptNo == wmsInReceiptIqcResult.receiptNo && x.receiptDtlId == wmsInReceiptIqcResult.receiptDtlId).FirstOrDefaultAsync();
                        if (receiptUniicode != null)
                        {
                            wmsInReceiptUniicode.dataCode = receiptUniicode.dataCode;
                            wmsInReceiptUniicode.expDate = receiptUniicode.expDate;
                            wmsInReceiptUniicode.mslGradeCode = receiptUniicode.mslGradeCode;
                            wmsInReceiptUniicode.curPositionNo = receiptUniicode.curPositionNo;
                            wmsInReceiptUniicode.productDate = receiptUniicode.productDate;
                            wmsInReceiptUniicode.projectNo = receiptUniicode.projectNo;
                            //wmsInReceiptUniicode.curPalletBarcode = receiptUniicode.curPalletBarcode;
                            wmsInReceiptUniicode.curPalletBarcode = wmsInReceiptIqcResult.receiptNo;
                            wmsInReceiptUniicode.supplierExposeTimeDuration = receiptUniicode.supplierExposeTimeDuration;
                            wmsInReceiptUniicode.runiiStatus = receiptUniicode.runiiStatus;
                            wmsInReceiptUniicode.receiptRecordId = receiptUniicode.receiptRecordId;
                            wmsInReceiptUniicode.curStockCode = receiptUniicode.curStockCode;
                            wmsInReceiptUniicode.curStockDtlId = receiptUniicode.curStockDtlId;

                            wmsInReceiptUniicode.iqcResultNo = wmsInReceiptIqcResult.iqcResultNo;
                            wmsInReceiptUniicode.receiptDtlId = wmsInReceiptIqcResult.receiptDtlId;
                            wmsInReceiptUniicode.receiptNo = wmsInReceiptIqcResult.receiptNo;
                            //wmsInReceiptUniicode.areaNo = wmsInReceiptIqcResult.areaNo;
                            wmsInReceiptUniicode.erpWhouseNo = wmsInReceiptIqcResult.erpWhouseNo;
                            wmsInReceiptUniicode.inDtlId = wmsInReceiptIqcResult.inDtlId;
                            wmsInReceiptUniicode.inNo = wmsInReceiptIqcResult.inNo;
                        }
                        else
                        {
                            wmsInReceiptUniicode.iqcResultNo = wmsInReceiptIqcResult.iqcResultNo;
                            wmsInReceiptUniicode.receiptDtlId = wmsInReceiptIqcResult.receiptDtlId;
                            wmsInReceiptUniicode.receiptNo = wmsInReceiptIqcResult.receiptNo;
                            //wmsInReceiptUniicode.areaNo = wmsInReceiptIqcResult.areaNo;
                            wmsInReceiptUniicode.erpWhouseNo = wmsInReceiptIqcResult.erpWhouseNo;
                            wmsInReceiptUniicode.inDtlId = wmsInReceiptIqcResult.inDtlId;
                            wmsInReceiptUniicode.inNo = wmsInReceiptIqcResult.inNo;
                            wmsInReceiptUniicode.runiiStatus = receiptUniicode.runiiStatus;
                        }
                        //DC.UpdateEntity(wmsInReceiptUniicode);
                        await ((DbContext)DC).Set<WmsInReceiptUniicode>().SingleUpdateAsync(wmsInReceiptUniicode);
                    }
                    #endregion
                    // 13-3 是否存在库存明细表信息
                    // 唯一码 -> 库存唯一码 -> 库存明细ID，库存ID -> 库存主表 是否是收货暂存区

                    var isInReceiptUniicodeReceiptDtlIdIsNull = wmsInReceiptUniicode.receiptDtlId == null;
                    if (isInReceiptUniicodeReceiptDtlIdIsNull == false || isProduct)
                    {
                        if (wmsInReceiptUniicode.receiptDtlId != wmsInReceiptIqcResult.receiptDtlId)
                        {
                            throw new Exception($"质检结果.收货单明细ID 与 入库唯一码.收货单明细ID 不一致!");
                        }
                    }
                    // 14-4 是否已组盘
                    if (wmsInReceiptUniicode.runiiStatus != Convert.ToInt32(InUniicodeStatus.ReceiptFinished.GetCode()))
                    {
                        throw new Exception($"入库唯一码 {uniicode} 不是【已收货】状态!");
                    }
                    if (isBatch)
                    {
                        if (!wmsInReceiptUniicode.batchNo.IsNullOrWhiteSpace() && !isProduct)
                        {
                            if (wmsInReceiptUniicode.batchNo != wmsInReceiptIqcResult.batchNo)
                            {
                                throw new Exception($"质检结果批次号（{wmsInReceiptIqcResult.batchNo}）与 入库唯一码批次号（{wmsInReceiptUniicode.batchNo}）不一致!");
                            }
                        }
                        //List<WmsStockUniicode> wmsStockUniicodeList = DC.Set<WmsStockUniicode>().Where(x => x.uniicode == uniicode).ToList();
                        //WmsStockUniicode wmsStockUniicodeZc = wmsStockUniicodeList.Where(x => x.uniicode == uniicode && x.palletBarcode == wmsInReceiptUniicode.curStockCode).FirstOrDefault();
                        //if (wmsStockUniicodeZc == null)
                        //{
                        //    throw new Exception($"根据唯一码{uniicode}和托盘号{wmsInReceiptUniicode.curStockCode}查询不到暂存区库存唯一码!");
                        //}
                        WmsStockUniicode wmsStockUniicodeKc = await DC.Set<WmsStockUniicode>().Where(x => x.uniicode == uniicode && x.palletBarcode != wmsInReceiptUniicode.curStockCode).FirstOrDefaultAsync();
                        if (wmsStockUniicodeKc != null)
                        {
                            WmsStockDtl wmsStockDtlZk = await DC.Set<WmsStockDtl>().Where(x => x.ID == wmsStockUniicodeKc.stockDtlId && x.stockDtlStatus == 50).FirstOrDefaultAsync();
                            if (wmsStockDtlZk != null)
                            {
                                throw new Exception($"唯一码{uniicode}有在库库存，需排查!");
                            }
                        }
                        WmsStockDtl wmsStockDtlShzc = await DC.Set<WmsStockDtl>().Where(x => x.materialCode == wmsInReceiptIqcResult.materialCode && (x.extend3 == wmsInReceiptIqcResult.receiptDtlId.ToString() || x.extend1 == wmsInReceiptIqcResult.extend1) && x.palletBarcode == wmsInReceiptIqcResult.receiptNo && (x.stockDtlStatus == 0 || x.stockDtlStatus == 20)).FirstOrDefaultAsync();
                        if (wmsStockDtlShzc == null)
                        {
                            throw new Exception($"物料{wmsInReceiptIqcResult.materialCode}，收货明细ID{wmsInReceiptIqcResult.receiptDtlId}，收货单号{wmsInReceiptIqcResult.receiptNo}，查不到初始和入库中的库存明细记录，需排查!");
                        }
                        WmsStock _wmsStock = await wmsStockVM.GetWmsStockPalletCodeAsync(wmsStockDtlShzc.stockCode, wmsInReceiptUniicode.curPalletBarcode);
                        if (_wmsStock == null)
                        {
                            throw new Exception($"根据载体号{wmsInReceiptUniicode.curPalletBarcode}和库存编号{wmsInReceiptUniicode.curStockCode}查询不到暂存区库存主表!");
                        }

                        var _checkExistWmsStockDtlRC = false;
                        if (_wmsStock != null)
                        {
                            BasWRegion _baseWRegion =
                               await basWRegionVM.GetRegionByAreaAndTypeAsync(_wmsStock.regionNo, WRgionTypeCode.RC.GetCode(), _wmsStock.areaNo);
                            if ((_baseWRegion != null))
                            {
                                _checkExistWmsStockDtlRC = true;
                            }
                        }

                        if (_checkExistWmsStockDtlRC == false)
                        {
                            throw new Exception($"入库唯一码 {uniicode} 在库存明细表信息中不存在收货暂存区的数据!");
                        }

                        //// 14-4 是否已组盘
                        //if (wmsInReceiptUniicode.runiiStatus != Convert.ToInt32(InUniicodeStatus.ReceiptFinished.GetCode()))
                        //{
                        //    throw new Exception($"入库唯一码 {uniicode} 不是【已收货】状态!");
                        //}
                    }
                }
                //DC.SaveChanges();
                await ((DbContext)DC).BulkSaveChangesAsync();
                wmsinReceiptUniicodeLists = await DC.Set<WmsInReceiptUniicode>().Where(x => uniiCodeLists.Contains(x.uniicode)).ToListAsync();
                DateTime dt104 = DateTime.Now;
                TimeSpan second104 = dt104 - dt100;
                dateSecond = second104.TotalSeconds.ToString();
                logger.Warn($"--->{DateTime.Now}：组盘上架，{_vpoint}单号{wmsInReceiptIqcResult.iqcResultNo}，入库唯一码{JsonConvert.SerializeObject(uniiCodeLists)}，操作人:{invoker}，{_vpoint}用时：{dateSecond}");
                #endregion

                #region 14 库区、库位
                _vpoint = "14 库区、库位";
                BasWRegion basWRegions = null;
                BasWBin basWBins = null;
                if (isPK == false)
                {
                    basWRegions = await basWRegionVM.GetRegionByRegionTypeAsync(areaNo, WRgionTypeCode.WS.GetCode());
                    if (basWRegions == null)
                    {
                        throw new Exception($"系统未配置区域{areaNo}的上架暂存区信息!");
                    }

                    basWBins = await basWBinVM.GetBinByRegionNoAsync(areaNo, basWRegions.regionNo);
                    if (basWBins == null)
                    {
                        throw new Exception($"系统未配置区域{areaNo}的上架暂存区的库位信息!");
                    }
                }
                else
                {
                    basWBins = await basWBinVM.GetBinByBinNoAsync(inParams.barCode);
                    basWRegions = await basWRegionVM.GetRegionAsync(basWBins.regionNo);
                    if (basWBins == null)
                    {
                        throw new Exception($"系统库位中库位{inParams.barCode}不存在!");
                    }
                }

                //单据类型委外退料入库和调拨入库，需校验当前库存是否与供应商库位一致
                if (isPK)
                {
                    if ((wmsInReceiptIqcResult.docTypeCode == BusinessCode.InOutSourceReturn.GetCode() ||
                        wmsInReceiptIqcResult.docTypeCode == BusinessCode.InTransferRequest.GetCode()) && wmsInReceiptIqcResult.erpWhouseNo == "01B")
                    {
                        var supperList = await DC.Set<BasBSupplierBin>().Where(x => x.supplierCode == wmsInReceiptIqcResult.supplierCode && x.erpWhouseNo == wmsInReceiptIqcResult.erpWhouseNo).ToListAsync();
                        //if (supperList.Count == 0)
                        //{
                        //    throw new Exception($"供应商：{wmsInReceiptIqcResult.supplierCode}未维护ERP库位!");
                        //}
                        if (supperList.Any())
                        {
                            var supperBin = supperList.Where(x => x.supplierCode == wmsInReceiptIqcResult.supplierCode && x.binNo == inParams.barCode).FirstOrDefault();
                            if (supperBin == null)
                            {
                                throw new Exception($"ERP库位:{inParams.barCode}与供应商：{wmsInReceiptIqcResult.supplierCode}库位不一致!");
                            }
                            else
                            {
                                //库位是否属于ERP仓库
                                var isErpWhouseBin = await basWErpWhouseBinVM.CheckIsErpWhouseBinAsync(supperBin.erpWhouseNo, supperBin.binNo);
                                if (isErpWhouseBin == false)
                                {
                                    throw new Exception($"供应商：{wmsInReceiptIqcResult.supplierCode}库位:{inParams.barCode} 不是 ERP仓库 {erpWhouseNo} 的货位!");
                                }
                            }
                        }
                    }
                }
                DateTime dt105 = DateTime.Now;
                TimeSpan second105 = dt105 - dt100;
                dateSecond = second105.TotalSeconds.ToString();
                logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}用时：{dateSecond}");
                #endregion

                #endregion

                #region 2  处理逻辑

                #region 21 生成库存 先删除暂存区库存再进行生成上架库存

                #region 21-5 删除库存明细 库存唯一码，插入库存记录
                _vpoint = "21-5 删除库存明细";

                WmsStockDtl wmsStockDtlZc = new WmsStockDtl();
                List<WmsStockUniicodeHis> uniicodeHisList = new List<WmsStockUniicodeHis>();
                List<WmsStockUniicode> wmsStockUniicodeDelZc = new List<WmsStockUniicode>();
                string stockCodeDel = String.Empty;
                var config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStockDtl, WmsStockDtlHis>());
                var mapper = config.CreateMapper();
                //查询暂存库存明细
                wmsStockDtlZc = await DC.Set<WmsStockDtl>().Where(x => x.materialCode == wmsInReceiptIqcResult.materialCode && (x.extend3 == wmsInReceiptIqcResult.receiptDtlId.ToString() || x.extend1 == wmsInReceiptIqcResult.extend1) && x.palletBarcode == wmsInReceiptIqcResult.receiptNo && (x.stockDtlStatus == 0 || x.stockDtlStatus == 20)).FirstOrDefaultAsync();
                if (wmsStockDtlZc != null)
                {
                    stockCodeDel = wmsStockDtlZc.stockCode;
                    if (wmsStockDtlZc.qty - uniicodeQtys == 0)
                    {
                        //1、直接删除库存明细转历史表
                        WmsStockDtlHis dtlhis = mapper.Map<WmsStockDtlHis>(wmsStockDtlZc);
                        //2、删除库存唯一码 转历史表
                        wmsStockUniicodeDelZc = await DC.Set<WmsStockUniicode>().Where(x => x.stockDtlId == wmsStockDtlZc.ID && x.materialCode == wmsStockDtlZc.materialCode && x.palletBarcode == wmsStockDtlZc.palletBarcode).ToListAsync();
                        if (wmsStockUniicodeDelZc.Any())
                        {
                            foreach (var uniiCode in wmsStockUniicodeDelZc)
                            {
                                WmsStockUniicodeHis uniicodeHis = null;
                                config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStockUniicode, WmsStockUniicodeHis>());
                                mapper = config.CreateMapper();
                                uniicodeHis = mapper.Map<WmsStockUniicodeHis>(uniiCode);
                                if (uniicodeHis != null)
                                {
                                    uniicodeHisList.Add(uniicodeHis);
                                }
                            }
                        }

                        if (dtlhis != null)
                        {
                            //DC.AddEntity(dtlhis);
                            await ((DbContext)DC).Set<WmsStockDtlHis>().SingleInsertAsync(dtlhis);
                        }
                        if (uniicodeHisList.Any())
                        {
                            //DC.Set<WmsStockUniicodeHis>().AddRange(uniicodeHisList);
                            await ((DbContext)DC).Set<WmsStockUniicodeHis>().BulkInsertAsync(uniicodeHisList);
                        }
                        if (wmsStockUniicodeDelZc.Any())
                        {
                            //DC.Set<WmsStockUniicode>().RemoveRange(wmsStockUniicodeDelZc);
                            await ((DbContext)DC).Set<WmsStockUniicode>().BulkDeleteAsync(wmsStockUniicodeDelZc);
                        }
                        //DC.DeleteEntity(wmsStockDtlZc);
                        await ((DbContext)DC).Set<WmsStockDtl>().SingleDeleteAsync(wmsStockDtlZc);
                        logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}单号{wmsInReceiptIqcResult.iqcResultNo}，库存明细ID{wmsStockDtlZc.ID}，入参{inJson}，入参唯一码数量{uniicodeQtys}，暂存库存数量{wmsStockDtlZc.qty}，暂存数量-唯一码数量{wmsStockDtlZc.qty - uniicodeQtys}");
                    }
                    else if (wmsStockDtlZc.qty - uniicodeQtys > 0)
                    {
                        //1、减暂存区库存数量 更新
                        wmsStockDtlZc.qty = wmsStockDtlZc.qty - uniicodeQtys;
                        wmsStockDtlZc.UpdateBy = invoker;
                        wmsStockDtlZc.UpdateTime = DateTime.Now;
                        foreach (var inUniicode in wmsinReceiptUniicodeLists)
                        {
                            WmsStockUniicodeHis uniicodeHis = null;
                            var stockUniicodeZc = await DC.Set<WmsStockUniicode>().Where(x => x.stockDtlId == wmsStockDtlZc.ID && x.materialCode == wmsStockDtlZc.materialCode && x.palletBarcode == wmsStockDtlZc.palletBarcode && x.uniicode == inUniicode.uniicode).FirstOrDefaultAsync();
                            //var stockUniicodeZc = DC.Set<WmsStockUniicode>().Where(x => x.stockDtlId == wmsStockDtlZc.ID && x.materialCode == wmsStockDtlZc.materialCode && x.palletBarcode == wmsStockDtlZc.palletBarcode).FirstOrDefault();
                            if (stockUniicodeZc != null)
                            {
                                config = new MapperConfiguration(cfg => cfg.CreateMap<WmsStockUniicode, WmsStockUniicodeHis>());
                                mapper = config.CreateMapper();
                                uniicodeHis = mapper.Map<WmsStockUniicodeHis>(stockUniicodeZc);
                                if (uniicodeHis != null)
                                {
                                    uniicodeHisList.Add(uniicodeHis);
                                }
                                wmsStockUniicodeDelZc.Add(stockUniicodeZc);
                            }
                        }
                        if (wmsStockUniicodeDelZc.Any())
                        {
                            //DC.Set<WmsStockUniicode>().RemoveRange(wmsStockUniicodeDelZc);
                            await ((DbContext)DC).Set<WmsStockUniicode>().BulkDeleteAsync(wmsStockUniicodeDelZc);
                        }
                        if (uniicodeHisList.Any())
                        {
                            //DC.Set<WmsStockUniicodeHis>().AddRange(uniicodeHisList);
                            await ((DbContext)DC).Set<WmsStockUniicodeHis>().BulkInsertAsync(uniicodeHisList);
                        }
                        //DC.UpdateEntity(wmsStockDtlZc);
                        await ((DbContext)DC).Set<WmsStockDtl>().SingleUpdateAsync(wmsStockDtlZc);
                        logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}单号{wmsInReceiptIqcResult.iqcResultNo}，库存明细ID{wmsStockDtlZc.ID}，入参{inJson}，入参唯一码数量{uniicodeQtys}，暂存库存数量{wmsStockDtlZc.qty}，暂存数量-唯一码数量{wmsStockDtlZc.qty - uniicodeQtys}");
                    }
                    else
                    {
                        //实际上架数量与暂存区数量不一致 抛出异常，排查
                        throw new Exception($"组盘上架，操作人:{invoker},{_vpoint}删除暂存区库存明细ID{wmsStockDtlZc.ID}：上架唯一码数量:{uniicodeQtys} 减-暂存区库存明细库存数量 {wmsStockDtlZc.qty}小于0");
                    }
                    //DC.SaveChanges();
                    await ((DbContext)DC).BulkSaveChangesAsync();
                }


                DateTime dt110 = DateTime.Now;
                TimeSpan second110 = dt110 - dt100;
                dateSecond = second110.TotalSeconds.ToString();
                logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}用时：{dateSecond}");

                #region 21-7 生成库存调整记录
                _vpoint = "21-7 生成库存调整记录";
                List<WmsStockAdjust> wmsStockAdjusts = new List<WmsStockAdjust>();
                if (uniicodeHisList.Any())
                {
                    foreach (var _uniicodeHis in uniicodeHisList)
                    {
                        WmsStockAdjust stockAdjust = new WmsStockAdjust();
                        stockAdjust.whouseNo = _uniicodeHis.whouseNo; // 仓库号
                        stockAdjust.proprietorCode = _uniicodeHis.proprietorCode; // 货主
                        stockAdjust.stockCode = _uniicodeHis.stockCode; // 库存编码
                        stockAdjust.palletBarcode = _uniicodeHis.palletBarcode; // 载体条码
                        stockAdjust.adjustOperate = "删除收货暂存区库存"; // 调整操作
                        stockAdjust.adjustType = "删除"; // 调整类型;新增、修改、删除
                        stockAdjust.adjustDesc = "删除收货暂存区库存"; // 调整说明
                        stockAdjust.packageBarcode = _uniicodeHis.uniicode; // 包装条码
                        stockAdjust.CreateBy = invoker;
                        stockAdjust.CreateTime = DateTime.Now;
                        wmsStockAdjusts.Add(stockAdjust);
                    }
                }
                if (wmsStockAdjusts.Count != 0)
                {
                    //DC.Set<WmsStockAdjust>().AddRange(wmsStockAdjusts);
                    await ((DbContext)DC).Set<WmsStockAdjust>().BulkInsertAsync(wmsStockAdjusts);
                }
                //DC.SaveChanges();
                await ((DbContext)DC).BulkSaveChangesAsync();
                DateTime dt112 = DateTime.Now;
                TimeSpan second112 = dt112 - dt100;
                dateSecond = second112.TotalSeconds.ToString();
                logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}用时：{dateSecond}");

                #endregion

                #endregion

                #region 21-1 生成库存主表
                _vpoint = "21-1 生成库存主表";
                // 在【库存表】 检测 库位 、 载体编号 是否存在数据
                // 平库 >  库位:具体库位号,      载体编号:库位
                // 立库 >  库位:上架暂存区库位,  载体编号:托盘编号
                wmsStock = await wmsStockVM.GetWmsStockByBinPalletAsync(isPK ? inParams.barCode : basWBins.binNo, palletBarcode);
                if (wmsStock == null)
                {
                    wmsStock = new WmsStock();
                    //wmsStock.areaNo = areaNo; // 区域编码(楼号)
                    wmsStock.areaNo = basWBins.areaNo; // 区域编码(楼号)
                    //wmsStock.binNo = inParams.barCode; // 库位号
                    wmsStock.binNo = isPK ? inParams.barCode : basWBins.binNo; // 库位号
                    wmsStock.errFlag = 0; // 异常标记(0正常，10异常，20火警)
                    wmsStock.errMsg = null; // 异常说明
                    wmsStock.height = null; // 组盘后托盘高度
                    wmsStock.invoiceNo = null; // 预拣选完成后的发货单号
                    wmsStock.loadedType = 1; // 装载类型(1:实盘 ；2:工装；0：空盘)
                    wmsStock.locAllotGroup = null; // 分配站台组，双工位使用
                    wmsStock.locNo = null; // 当前站台号。应该为虚拟站台号，数据定义好后进行填充。
                    //wmsStock.palletBarcode = inParams.barCode; // 载体条码
                    wmsStock.palletBarcode = inParams.barCode; // 载体条码
                    wmsStock.proprietorCode = wmsInReceiptIqcResult.proprietorCode; // 货主
                    wmsStock.regionNo = basWBins.regionNo; // 库区编号
                    wmsStock.roadwayNo = basWBins.roadwayNo; // 巷道编号
                    wmsStock.specialFlag = 0; // 特殊标记
                    wmsStock.specialMsg = null; // 特殊说明
                    wmsStock.stockCode = await sysSequenceVM.GetSequenceAsync(SequenceCode.StockCode.GetCode()); // 库存编码
                    wmsStock.stockStatus = Convert.ToInt32(StockStatus.InitCreate.GetCode()); // 库存状态（0：初始创建；20：入库中；50：在库；70：出库中；90：托盘出库完成(生命周期结束)；92删除（撤销）；93强制完成）
                    wmsStock.weight = null; // 组盘重量
                    wmsStock.whouseNo = wmsInReceiptIqcResult.whouseNo; // 仓库号
                    wmsStock.CreateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                    wmsStock.CreateTime = DateTime.Now;
                    wmsStock.UpdateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                    wmsStock.UpdateTime = DateTime.Now;
                    if (isPK)
                    {
                        wmsStock.stockStatus = Convert.ToInt32(StockStatus.InStoreing.GetCode());
                    }

                    //DC.AddEntity(wmsStock);
                    //DC.SaveChanges();
                    await ((DbContext)DC).Set<WmsStock>().SingleInsertAsync(wmsStock);
                    await ((DbContext)DC).BulkSaveChangesAsync();
                }
                DateTime dt106 = DateTime.Now;
                TimeSpan second106 = dt106 - dt100;
                dateSecond = second106.TotalSeconds.ToString();
                logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}用时：{dateSecond}");
                #endregion

                #region 21-2 生成入库记录  库存明细  库存唯一码 库存调整记录 更新入库唯一码状态
                _vpoint = "21-2 生成入库记录--库存明细";
                int returnFlag = 0;
                List<WmsStockUniicode> addWmsStockUniicodeList = new List<WmsStockUniicode>();
                List<WmsStockAdjust> addWmsStockAdjustList = new List<WmsStockAdjust>();
                foreach (var uniiCode in wmsinReceiptUniicodeLists)
                {
                    decimal stockQty = inParams.uniicodes[uniiCode.uniicode];
                    #region 21-3 生成库存明细表
                    WmsInReceiptRecord wmsInReceiptRecord = BuildWmsInReceiptRecordByUniicode(wmsInReceiptIqcResult, wmsStock, uniiCode.batchNo, isPK, returnFlag, stockQty, invoker);
                    //wmsInReceiptRecord.extend11 = uniiCode.uniicode;
                    WmsStockDtl updatewmStockDtl = null;
                    WmsStockDtl addwmStockDtl = null;
                    //updatewmStockDtl = DC.Set<WmsStockDtl>().Where(t => t.stockCode == wmsStock.stockCode && t.materialCode == wmsInReceiptRecord.materialCode && t.batchNo == wmsInReceiptRecord.batchNo).FirstOrDefault();
                    //updatewmStockDtl =await DC.Set<WmsStockDtl>().Where(t => t.stockCode == wmsStock.stockCode && t.materialCode == wmsInReceiptRecord.materialCode /*&& t.batchNo == wmsInReceiptRecord.batchNo*/).FirstOrDefaultAsync();
                    updatewmStockDtl = await DC.Set<WmsStockDtl>().Where(t => t.stockCode == wmsStock.stockCode && t.materialCode == wmsInReceiptRecord.materialCode && t.erpWhouseNo == wmsInReceiptRecord.erpWhouseNo).FirstOrDefaultAsync();
                    if (updatewmStockDtl != null)
                    {
                        if (wmsInReceiptRecord.recordQty == null)
                            wmsInReceiptRecord.recordQty = 0;
                        if (updatewmStockDtl.qty == null)
                            updatewmStockDtl.qty = 0;
                        updatewmStockDtl.qty = updatewmStockDtl.qty + wmsInReceiptRecord.recordQty;
                        updatewmStockDtl.UpdateBy = invoker;
                        updatewmStockDtl.UpdateTime = DateTime.Now;
                    }
                    else
                    {
                        addwmStockDtl = BuildWmsStockDtl(wmsStock, wmsInReceiptRecord, invoker);
                        if (isPK)
                        {
                            addwmStockDtl.stockDtlStatus = Convert.ToInt32(StockStatus.InStoreing.GetCode());
                        }
                        //DC.AddEntity(addwmStockDtl);
                        //DC.SaveChanges();
                        await ((DbContext)DC).Set<WmsStockDtl>().SingleInsertAsync(addwmStockDtl);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                    }
                    bool isAddStockDtl = false;
                    if (updatewmStockDtl != null)
                    {
                        wmsInReceiptRecord.stockDtlId = updatewmStockDtl.ID;
                        logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}单号{wmsInReceiptIqcResult.iqcResultNo}，库存明细ID{updatewmStockDtl.ID}---更新，入参唯一码{uniiCode.uniicode}数量{stockQty}，更新后库存数量{updatewmStockDtl.qty}，入参{inJson}");
                    }
                    if (addwmStockDtl != null)
                    {
                        isAddStockDtl = true;
                        wmsInReceiptRecord.stockDtlId = addwmStockDtl.ID;
                        logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}单号{wmsInReceiptIqcResult.iqcResultNo}，库存明细ID{addwmStockDtl.ID}---新增，入参唯一码{uniiCode.uniicode}数量{stockQty}，库存数量{addwmStockDtl.qty}，入参{inJson}");
                    }
                    if (wmsInReceiptRecord != null)
                    {
                        addReceiptRecordLists.Add(wmsInReceiptRecord);
                    }
                    if (updatewmStockDtl != null)
                    {
                        updateStockDtlLists.Add(updatewmStockDtl);
                    }
                    //if (addwmStockDtl!=null)
                    //{
                    //    addStockDtlLists.Add(addwmStockDtl);
                    //}
                    #endregion

                    #region 21-9 更新入库唯一码信息 , 生成【库存唯一码】、生成【库存调整记录】
                    WmsStockUniicode stockUniicodeSh = new WmsStockUniicode();
                    stockUniicodeSh = wmsStockUniicodeDelZc.Where(x => x.uniicode == uniiCode.uniicode).FirstOrDefault();

                    WmsStockUniicode wmsStockUniicode = BuildWmsStockUniicode(stockQty, uniiCode, stockUniicodeSh, wmsInReceiptIqcResult, isAddStockDtl ? addwmStockDtl : updatewmStockDtl, invoker, inParams.barCode);
                    //DC.AddEntity(wmsStockUniicode);
                    if (wmsStockUniicode != null)
                    {
                        addWmsStockUniicodeList.Add(wmsStockUniicode);
                        stockUniicodereturnLists.Add(wmsStockUniicode);
                    }
                    WmsStockAdjust wmsStockAdjust = new WmsStockAdjust();
                    wmsStockAdjust.whouseNo = wmsStock.whouseNo; // 仓库号
                    wmsStockAdjust.proprietorCode = wmsStock.proprietorCode; // 货主
                    wmsStockAdjust.stockCode = wmsStock.stockCode; // 库存编码
                    wmsStockAdjust.palletBarcode = wmsStock.palletBarcode; // 载体条码
                    wmsStockAdjust.adjustType = "新增"; // 调整类型;新增、修改、删除
                    wmsStockAdjust.packageBarcode = wmsStockUniicode.uniicode; // 包装条码
                    if (isPK)
                    {
                        wmsStockAdjust.adjustDesc = $"平库{wmsStock.binNo} 新增入库，数量：{stockQty}";
                        //wmsStockAdjust.adjustDesc = $"平库{wmsStock.binNo} 新增入库，数量：{inParams.uniicodes[stockuniicode.uniicode]}";
                        wmsStockAdjust.adjustOperate = "组盘上架"; // 调整操作
                    }
                    else
                    {
                        wmsStockAdjust.adjustDesc = "新增待上架区库存，数量：" + stockQty;
                        //wmsStockAdjust.adjustDesc = "新增待上架区库存，数量：" + inParams.uniicodes[stockuniicode.uniicode];
                        wmsStockAdjust.adjustOperate = "组盘上架"; // 调整操作
                    }

                    wmsStockAdjust.CreateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                    wmsStockAdjust.CreateTime = DateTime.Now;
                    //DC.AddEntity(wmsStockAdjust);
                    if (wmsStockAdjust != null)
                    {
                        addWmsStockAdjustList.Add(wmsStockAdjust);
                    }
                    #endregion

                    uniiCode.recordQty = stockQty;
                    if (isPK)
                    {
                        uniiCode.runiiStatus = Convert.ToInt32(InUniicodeStatus.InStoreFinished.GetCode());
                    }
                    else
                    {
                        uniiCode.runiiStatus = Convert.ToInt32(InUniicodeStatus.PutwayFinished.GetCode());
                    }
                    if (isAddStockDtl)
                    {
                        uniiCode.curStockDtlId = addwmStockDtl.ID;
                        uniiCode.curStockCode = addwmStockDtl.stockCode;
                        uniiCode.curPalletBarcode = addwmStockDtl.palletBarcode;
                    }
                    else
                    {
                        uniiCode.curStockDtlId = updatewmStockDtl.ID;
                        uniiCode.curStockCode = updatewmStockDtl.stockCode;
                        uniiCode.curPalletBarcode = updatewmStockDtl.palletBarcode;
                    }
                    //uniiCode.curPalletBarcode = inParams.barCode;
                    uniiCode.receiptRecordId = wmsInReceiptRecord.ID;
                    uniiCode.UpdateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                    uniiCode.UpdateTime = DateTime.Now;
                }
                if (addReceiptRecordLists.Any())
                {
                    //DC.Set<WmsInReceiptRecord>().AddRange(addReceiptRecordLists);
                    await ((DbContext)DC).Set<WmsInReceiptRecord>().BulkInsertAsync(addReceiptRecordLists);
                }
                if (updateStockDtlLists.Any())
                {
                    //DC.Set<WmsStockDtl>().UpdateRange(updateStockDtlLists);
                    await ((DbContext)DC).Set<WmsStockDtl>().BulkUpdateAsync(updateStockDtlLists);
                }
                //if (addStockDtlLists.Any())
                //{
                //    DC.Set<WmsStockDtl>().AddRange(addStockDtlLists);
                //}
                if (addWmsStockUniicodeList.Any())
                {
                    //DC.Set<WmsStockUniicode>().AddRange(addWmsStockUniicodeList);
                    await ((DbContext)DC).Set<WmsStockUniicode>().BulkInsertAsync(addWmsStockUniicodeList);
                }
                if (addWmsStockAdjustList.Any())
                {
                    await ((DbContext)DC).Set<WmsStockAdjust>().BulkInsertAsync(addWmsStockAdjustList);
                    //DC.Set<WmsStockAdjust>().AddRange(addWmsStockAdjustList);
                }
                //DC.Set<WmsInReceiptUniicode>().UpdateRange(wmsinReceiptUniicodeLists);
                await ((DbContext)DC).Set<WmsInReceiptUniicode>().BulkUpdateAsync(wmsinReceiptUniicodeLists);
                //DC.SaveChanges();
                await ((DbContext)DC).BulkSaveChangesAsync();

                DateTime dt107 = DateTime.Now;
                TimeSpan second107 = dt107 - dt100;
                dateSecond = second107.TotalSeconds.ToString();
                logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}用时：{dateSecond}");
                #endregion
                #endregion

                #region 22 生成上架单
                _vpoint = "22 生成上架单";
                WmsPutaway wmsPutaway = null;
                if (isPK == false)
                {
                    wmsPutaway = await GetPutwayByPalletInfo(palletBarcode, wmsStock.stockCode, Convert.ToInt32(PutAwayOrDtlStatus.Init.GetCode()));
                }
                else
                {
                    wmsPutaway = await GetPutwayByPalletInfo(palletBarcode, wmsStock.stockCode, Convert.ToInt32(PutAwayOrDtlStatus.StoreIned.GetCode()));
                }

                if (wmsPutaway == null)
                {
                    wmsPutaway = new WmsPutaway();
                    var putawayNoSeq = await sysSequenceVM.GetSequenceAsync(SequenceCode.WmsPutawayNo.GetCode());
                    wmsPutaway.whouseNo = wmsInReceiptIqcResult.whouseNo; // 仓库号
                    //wmsPutaway.areaNo = wmsInReceiptIqcResult.areaNo; // 区域编码(楼号)
                    //wmsPutaway.areaNo = wmsStock.areaNo; // 区域编码(楼号)
                    wmsPutaway.areaNo = basWBins.areaNo; ; // 区域编码(楼号)
                    //wmsPutaway.delFlag = "0"; // 删除标志;0-有效,1-已删除
                    //wmsPutaway.erpWhouseNo = wmsInReceiptIqcResult.erpWhouseNo; // ERP仓库
                    wmsPutaway.loadedType = 1; // 装载类型;1:实盘 ；2:工装；0：空盘；
                    wmsPutaway.manualFlag = 0; // 是否允许人工上架;0默认不允许，1允许
                    wmsPutaway.onlineLocNo = ""; // 上线站台：WCS请求时的站台
                    wmsPutaway.onlineMethod = isPK ? "3" : "0"; // 上线方式;0自动上线；1人工上线；2组盘上线；3直接上架
                    wmsPutaway.palletBarcode = palletBarcode; // 载体条码
                    wmsPutaway.proprietorCode = wmsInReceiptIqcResult.proprietorCode; // 货主
                    wmsPutaway.ptaRegionNo = regionNo; // 上架库区编号
                    wmsPutaway.putawayNo = putawayNoSeq; // 上架单编号
                    wmsPutaway.putawayStatus = isPK ? Convert.ToInt32(IqcResultStatus.StoreIned.GetCode()) : Convert.ToInt32(PutAwayOrDtlStatus.Init.GetCode()); // 状态;0：初始创建（组盘完成）；41：入库中；90：上架完成；92删除；93强制完成
                    wmsPutaway.regionNo = areaNo; // 库区编号
                    wmsPutaway.stockCode = wmsStock.stockCode; // 库存编码
                    //wmsPutaway.extend1 = wmsInReceiptIqcResult.extend1; // 扩展字段1
                    //wmsPutaway.extend2 = wmsInReceiptIqcResult.extend2; // 扩展字段2
                    //wmsPutaway.extend3 = wmsInReceiptIqcResult.extend3; // 扩展字段3
                    //wmsPutaway.extend4 = wmsInReceiptIqcResult.extend4; // 扩展字段4
                    //wmsPutaway.extend5 = wmsInReceiptIqcResult.extend5; // 扩展字段5
                    //wmsPutaway.extend6 = wmsInReceiptIqcResult.extend6; // 扩展字段6
                    //wmsPutaway.extend7 = wmsInReceiptIqcResult.extend7; // 扩展字段7
                    //wmsPutaway.extend8 = wmsInReceiptIqcResult.extend8; // 扩展字段8
                    //wmsPutaway.extend9 = wmsInReceiptIqcResult.extend9; // 扩展字段9
                    //wmsPutaway.extend10 = wmsInReceiptIqcResult.extend10; // 扩展字段10
                    //wmsPutaway.extend11 = wmsInReceiptIqcResult.extend11; // 扩展字段11
                    //wmsPutaway.extend12 = wmsInReceiptIqcResult.extend12; // 扩展字段12
                    //wmsPutaway.extend13 = wmsInReceiptIqcResult.extend13; // 扩展字段13
                    //wmsPutaway.extend14 = wmsInReceiptIqcResult.extend14; // 扩展字段14
                    //wmsPutaway.extend15 = wmsInReceiptIqcResult.extend15; // 扩展字段15
                    wmsPutaway.CreateTime = DateTime.Now;
                    //DC.AddEntity(wmsPutaway);
                    await ((DbContext)DC).Set<WmsPutaway>().SingleInsertAsync(wmsPutaway);
                }

                //DC.SaveChanges();
                await ((DbContext)DC).BulkSaveChangesAsync();
                DateTime dt114 = DateTime.Now;
                TimeSpan second114 = dt114 - dt100;
                dateSecond = second114.TotalSeconds.ToString();
                logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}用时：{dateSecond}");

                #endregion

                #region 23 生成上架单明细
                _vpoint = "23 生成上架单明细";
                foreach (var item in wmsinReceiptUniicodeLists)
                {
                    decimal stockQty = inParams.uniicodes[item.uniicode];
                    WmsInOrder inOrder = await wmsInOrderVM.GetWmsInOrderAsync(wmsInReceiptIqcResult.inNo);
                    //var wmStockDtl = DC.Set<WmsStockDtl>().Where(x => x.ID == item.curStockDtlId && x.stockCode == item.curStockCode).FirstOrDefault();
                    WmsPutawayDtl wmsPutawayDtl = new WmsPutawayDtl();
                    //wmsPutawayDtl.batchNo = item.batchNo; // 批次号
                    //wmsPutawayDtl.areaNo = wmsStock.areaNo; // 区域编码(楼号)
                    wmsPutawayDtl.areaNo = basWBins.areaNo; // 区域编码(楼号)
                    wmsPutawayDtl.binNo = isPK ? inParams.suggestBin : null; // 系统推荐库位号
                    //wmsPutawayDtl.delFlag = DelFlag.NDelete.GetCode();
                    ; // 删除标志;0-有效,1-已删除
                    wmsPutawayDtl.docTypeCode = wmsInReceiptIqcResult.docTypeCode; // 单据类型
                    //wmsPutawayDtl.erpWhouseNo = wmsInReceiptIqcResult.erpWhouseNo; // ERP仓库
                    wmsPutawayDtl.inspectionResult = wmsInReceiptIqcResult.inspectionResult; // 质量标记;0：待检；1：合格；2：不合格；3：免检；4：冻结；
                    wmsPutawayDtl.materialCode = wmsInReceiptIqcResult.materialCode; // 物料编码
                    wmsPutawayDtl.materialName = wmsInReceiptIqcResult.materialName; // 物料编码
                    wmsPutawayDtl.unitCode = wmsInReceiptIqcResult.unitCode; // 物料编码
                    wmsPutawayDtl.materialSpec = wmsInReceiptIqcResult.materialSpec; // 物料规格
                    wmsPutawayDtl.orderDtlId = wmsInReceiptIqcResult.orderDtlId; // 关联单据明细ID
                    wmsPutawayDtl.orderNo = wmsInReceiptIqcResult.orderNo; // 关联单据编号
                    wmsPutawayDtl.palletBarcode = palletBarcode; // 载体条码
                    //wmsPutawayDtl.projectNo = wmsInReceiptIqcResult.projectNo; // 项目号
                    wmsPutawayDtl.proprietorCode = wmsInReceiptIqcResult.proprietorCode; // 货主
                    wmsPutawayDtl.ptaBinNo = isPK ? basWBins.binNo : null; // 上架库位
                    wmsPutawayDtl.putawayDtlStatus =
                        isPK ? Convert.ToInt32(PutAwayOrDtlStatus.StoreIned.GetCode()) : Convert.ToInt32(PutAwayOrDtlStatus.Init.GetCode()); // 状态;0：初始创建（组盘完成）；11：入库中；90：上架完成；92删除；93强制完成
                    wmsPutawayDtl.putawayNo = wmsPutaway.putawayNo; // 上架单编号
                    //wmsPutawayDtl.recordId = wmsInReceiptIqcResult.iqcResultNo; // 记录ID
                    wmsPutawayDtl.recordId = item.receiptRecordId; // 记录ID
                    //wmsPutawayDtl.recordQty = uniicodeQtys; // 数量(组盘数量)
                    //wmsPutawayDtl.qty = stockQty; // 数量(组盘数量)
                    //wmsPutawayDtl.qty = item.qty; // 数量(组盘数量)
                    wmsPutawayDtl.recordQty = item.qty; // 数量(组盘数量)
                    wmsPutawayDtl.regionNo = regionNo; // 库区编号
                    wmsPutawayDtl.roadwayNo = isPK ? wmsStock.roadwayNo : "00"; // 巷道
                    wmsPutawayDtl.skuCode = wmsInReceiptIqcResult.skuCode; // SKU编码
                    wmsPutawayDtl.stockCode = wmsStock.stockCode; // 库存编码
                    //wmsPutawayDtl.stockDtlId = wmStockDtl?.ID; // 库存明细ID
                    wmsPutawayDtl.stockDtlId = (long)item.curStockDtlId; // 库存明细ID
                    //wmsPutawayDtl.supplierBatchNo = null; // 供应商批次
                    wmsPutawayDtl.supplierCode = wmsInReceiptIqcResult.supplierCode; // 供应商编码
                    wmsPutawayDtl.supplierName = wmsInReceiptIqcResult.supplierName; // 供方名称
                    wmsPutawayDtl.supplierNameAlias = wmsInReceiptIqcResult.supplierNameAlias; // 供方名称-其他
                    wmsPutawayDtl.supplierNameEn = wmsInReceiptIqcResult.supplierNameEn; // 供方名称-英文
                    //wmsPutawayDtl.supplierType = inOrder?.cvCode; // 供货方类型;供应商、产线
                    wmsPutawayDtl.whouseNo = wmsInReceiptIqcResult.whouseNo; // 仓库号
                    wmsPutawayDtl.CreateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                    wmsPutawayDtl.CreateTime = DateTime.Now;
                    //DC.AddEntity(wmsPutawayDtl);
                    await ((DbContext)DC).Set<WmsPutawayDtl>().SingleInsertAsync(wmsPutawayDtl);
                }
                //DC.SaveChanges();
                await ((DbContext)DC).BulkSaveChangesAsync();

                DateTime dt115 = DateTime.Now;
                TimeSpan second115 = dt115 - dt100;
                dateSecond = second115.TotalSeconds.ToString();
                logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}用时：{dateSecond}");

                #endregion

                #region 24 更新质检结果单
                _vpoint = "24 更新质检结果单";
                //var inReceiptIqcResult = DC.Set<WmsInReceiptIqcResult>().Where(x => x.iqcResultNo == wmsInReceiptIqcResult.iqcResultNo).FirstOrDefault();
                //config = new MapperConfiguration(cfg => cfg.CreateMap<WmsInReceiptIqcResult, WmsInReceiptIqcResult>());
                //mapper = config.CreateMapper();
                //wmsInReceiptIqcResult = mapper.Map<WmsInReceiptIqcResult>(inReceiptIqcResult);
                wmsInReceiptIqcResult = await DC.Set<WmsInReceiptIqcResult>().Where(x => x.iqcResultNo == wmsInReceiptIqcResult.iqcResultNo).FirstOrDefaultAsync();

                wmsInReceiptIqcResult.recordQty = (wmsInReceiptIqcResult.recordQty ?? 0) + uniicodeQtys;
                if (isPK)
                {
                    wmsInReceiptIqcResult.binNo = basWBins.binNo;
                    wmsInReceiptIqcResult.putawayQty = wmsInReceiptIqcResult.recordQty;
                }

                if (wmsInReceiptIqcResult.recordQty == wmsInReceiptIqcResult.qty)
                {

                    if (isPK)
                    {
                        wmsInReceiptIqcResult.iqcResultStatus = Convert.ToInt32(IqcResultStatus.StoreIned.GetCode());
                    }
                    else
                    {
                        wmsInReceiptIqcResult.iqcResultStatus = Convert.ToInt32(IqcResultStatus.PutAwayed.GetCode());
                    }
                }
                else
                {
                    wmsInReceiptIqcResult.iqcResultStatus = Convert.ToInt32(IqcResultStatus.PutAwaying.GetCode());
                }

                logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}单号{wmsInReceiptIqcResult.iqcResultNo}，组盘数量{wmsInReceiptIqcResult.recordQty}，入参唯一码数量{uniicodeQtys}，质检结果数量{wmsInReceiptIqcResult.qty}，更新状态{wmsInReceiptIqcResult.iqcResultStatus}");
                wmsInReceiptIqcResult.UpdateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                wmsInReceiptIqcResult.UpdateTime = DateTime.Now;
                //DC.UpdateEntity(wmsInReceiptIqcResult);
                //DC.SaveChanges();
                await ((DbContext)DC).Set<WmsInReceiptIqcResult>().SingleUpdateAsync(wmsInReceiptIqcResult);
                await ((DbContext)DC).BulkSaveChangesAsync();

                DateTime dt116 = DateTime.Now;
                TimeSpan second116 = dt116 - dt100;
                dateSecond = second116.TotalSeconds.ToString();
                logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}用时：{dateSecond}");
                #endregion

                #region 25 更新收货信息
                _vpoint = "25 更新收货信息";
                WmsInReceiptDtl wmsInReceiptDt = await wmsInReceiptVM.GetWmsInReceiptDtAsync(wmsInReceiptIqcResult.receiptDtlId);
                wmsInReceiptDt.recordQty = (wmsInReceiptDt.recordQty ?? 0) + uniicodeQtys;
                if (isPK)
                {
                    wmsInReceiptDt.putawayQty = wmsInReceiptDt.recordQty;
                }
                //组盘数量=合格数量+特采数量
                if (wmsInReceiptDt.recordQty == wmsInReceiptDt.qualifiedQty + wmsInReceiptDt.qualifiedSpecialQty)
                {
                    if (isPK)
                    {
                        if (wmsInReceiptDt.recordQty + wmsInReceiptDt.returnQty < wmsInReceiptDt.receiptQty)
                        {
                            wmsInReceiptDt.receiptDtlStatus = Int16.Parse(ReceiptOrDtlStatus.PutAwaying.GetCode());
                        }

                        if (wmsInReceiptDt.recordQty + wmsInReceiptDt.returnQty == wmsInReceiptDt.receiptQty)
                        {
                            wmsInReceiptDt.receiptDtlStatus = Int16.Parse(ReceiptOrDtlStatus.StoreIned.GetCode());
                        }
                    }
                    else
                    {
                        wmsInReceiptDt.receiptDtlStatus = Int16.Parse(ReceiptOrDtlStatus.PutAwayed.GetCode());
                    }

                    // 不合格处理
                    //if (wmsInReceiptDt.recordQty != wmsInReceiptDt.receiptQty)
                    //{
                    //    wmsInReceiptIqcResultVM.DealUnqualified(wmsInReceiptDt.receiptNo, invoker);
                    //}
                }
                else if (wmsInReceiptDt.recordQty < wmsInReceiptDt.qualifiedQty + wmsInReceiptDt.qualifiedSpecialQty)
                {
                    wmsInReceiptDt.receiptDtlStatus = Int16.Parse(ReceiptOrDtlStatus.PutAwaying.GetCode());
                }
                else
                {
                    throw new Exception($"收货单明细更新异常：组盘数量{wmsInReceiptDt.recordQty}大于，合格数量{wmsInReceiptDt.qualifiedQty}加特采数量 {wmsInReceiptDt.qualifiedSpecialQty}，需排查上架条码!");
                }

                wmsInReceiptDt.UpdateBy = invoker; // LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                wmsInReceiptDt.UpdateTime = DateTime.Now;
                //DC.UpdateEntity(wmsInReceiptDt);
                //DC.SaveChanges();
                await ((DbContext)DC).Set<WmsInReceiptDtl>().SingleUpdateAsync(wmsInReceiptDt);
                await ((DbContext)DC).BulkSaveChangesAsync();

                bool isReceiptFinish = false;
                if (isPK)
                {
                    isReceiptFinish = await wmsInReceiptVM.UpdateInReceiptStatusAsync(wmsInReceiptIqcResult.receiptNo, Convert.ToInt32(ReceiptOrDtlStatus.StoreIned.GetCode()),
                        Convert.ToInt32(ReceiptOrDtlStatus.PutAwaying.GetCode()), invoker);
                }
                else
                {
                    isReceiptFinish = await wmsInReceiptVM.UpdateInReceiptStatusAsync(wmsInReceiptIqcResult.receiptNo, Convert.ToInt32(ReceiptOrDtlStatus.PutAwayed.GetCode()),
                        Convert.ToInt32(ReceiptOrDtlStatus.PutAwaying.GetCode()), invoker);
                }
                #region 库存主表--收货单组盘完成
                _vpoint = "库存主表--收货单组盘完成";
                logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}用时：{dateSecond},更新状态为{isReceiptFinish}");
                if (isReceiptFinish)
                {
                    var wmsStockDel = await DC.Set<WmsStock>().Where(x => x.stockCode == stockCodeDel && x.palletBarcode == wmsInReceiptIqcResult.receiptNo).FirstOrDefaultAsync();
                    if (wmsStockDel != null)
                    {
                        WmsStockHis wmsStockHis = null;
                        wmsStockHis = CommonHelper.Map<WmsStock, WmsStockHis>(wmsStockDel, "ID");

                        if (wmsStockHis != null)
                        {
                            //DC.AddEntity(wmsStockHis);
                            await ((DbContext)DC).Set<WmsStockHis>().SingleInsertAsync(wmsStockHis);
                        }
                        //DC.DeleteEntity(wmsStockDel);
                        await ((DbContext)DC).Set<WmsStock>().SingleDeleteAsync(wmsStockDel);
                        logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}用时：{dateSecond},更新状态为{isReceiptFinish}，删除暂存库存主表成功");
                    }
                    //DC.SaveChanges();
                    await ((DbContext)DC).BulkSaveChangesAsync();
                }
                #endregion

                DateTime dt117 = DateTime.Now;
                TimeSpan second117 = dt117 - dt100;
                dateSecond = second117.TotalSeconds.ToString();
                logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}用时：{dateSecond}");
                #endregion

                #region 27 更新入库单信息
                _vpoint = "27 更新入库单信息";
                WmsInOrderDtl wmsInOrderDetail = await wmsInOrderVM.GetWmsInOrderDtlAsync(wmsInReceiptIqcResult.inDtlId);
                wmsInOrderDetail.recordQty = (wmsInOrderDetail.recordQty ?? 0) + uniicodeQtys;
                if (isPK)
                {
                    wmsInOrderDetail.putawayQty = wmsInOrderDetail.recordQty;
                }

                if (wmsInOrderDetail.recordQty == wmsInOrderDetail.qualifiedQty + wmsInOrderDetail.qualifiedSpecialQty)
                {
                    if (isPK)
                    {
                        wmsInOrderDetail.inDtlStatus = Convert.ToInt32(InOrDtlStatus.StoreIned.GetCode());
                    }
                    else
                    {
                        wmsInOrderDetail.inDtlStatus = Convert.ToInt32(InOrDtlStatus.PutAwayed.GetCode());
                    }
                }
                else if (wmsInOrderDetail.recordQty < wmsInOrderDetail.qualifiedQty + wmsInOrderDetail.qualifiedSpecialQty)
                {
                    wmsInOrderDetail.inDtlStatus = Convert.ToInt32(InOrDtlStatus.PutAwaying.GetCode());
                }
                else
                {
                    throw new Exception($"入库单明细更新异常：组盘数量{wmsInReceiptDt.recordQty}大于，合格数量{wmsInReceiptDt.qualifiedQty}加特采数量 {wmsInReceiptDt.qualifiedSpecialQty}，需排查上架条码!");
                }

                wmsInOrderDetail.UpdateBy = LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                wmsInOrderDetail.UpdateTime = DateTime.Now;
                //DC.UpdateEntity(wmsInOrderDetail);
                //DC.SaveChanges();
                await ((DbContext)DC).Set<WmsInOrderDtl>().SingleUpdateAsync(wmsInOrderDetail);
                await ((DbContext)DC).BulkSaveChangesAsync();
                if (isPK)
                {
                    await wmsInOrderVM.UpdateInOrderStatusAsync(wmsInReceiptIqcResult.inNo, Convert.ToInt32(InOrDtlStatus.StoreIned.GetCode()),
                         Convert.ToInt32(InOrDtlStatus.PutAwaying.GetCode()), invoker);
                }
                else
                {
                    await wmsInOrderVM.UpdateInOrderStatusAsync(wmsInReceiptIqcResult.inNo, Convert.ToInt32(InOrDtlStatus.PutAwayed.GetCode()),
                         Convert.ToInt32(InOrDtlStatus.PutAwaying.GetCode()), invoker);
                }
                DateTime dt118 = DateTime.Now;
                TimeSpan second118 = dt118 - dt100;
                dateSecond = second118.TotalSeconds.ToString();
                logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}用时：{dateSecond}");
                #endregion

                #region 28 更新库存信息
                _vpoint = "28 更新库存信息";
                // 更新收货明细对应的库存明细状态
                //if (wmsInReceiptDt.inDtlStatus == Int16.Parse(ReceiptOrDtlStatus.StoreIned.GetCode()))
                //{
                foreach (var item in wmsinReceiptUniicodeLists)
                {
                    var wmStockDtl = await DC.Set<WmsStockDtl>().Where(x => x.ID == item.curStockDtlId && x.stockCode == item.curStockCode).FirstOrDefaultAsync();
                    if (wmStockDtl != null)
                    {
                        if (isPK)
                        {
                            wmStockDtl.stockDtlStatus = Convert.ToInt32(StockStatus.InStore.GetCode());
                            wmStockDtl.UpdateBy = LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                            wmStockDtl.UpdateTime = DateTime.Now;
                            wmStockDtl.lockFlag = 1;
                            //wmStockDtl.inwhTime = DateTime.Now;
                            //DC.UpdateEntity(wmStockDtl);
                            //DC.SaveChanges();
                            await ((DbContext)DC).Set<WmsStockDtl>().SingleUpdateAsync(wmStockDtl);
                            await ((DbContext)DC).BulkSaveChangesAsync();
                            // 更新库存主表状态
                            await wmsStockVM.UpdateStockStatusAsync(wmStockDtl.stockCode, Convert.ToInt32(StockStatus.InStore.GetCode()), isPK ? Convert.ToInt32(StockStatus.InStore.GetCode()) : null, invoker);
                        }
                        else
                        {
                            if (wmsInReceiptDt.receiptDtlStatus == Int16.Parse(ReceiptOrDtlStatus.PutAwaying.GetCode()) || wmsInReceiptDt.receiptDtlStatus == Int16.Parse(ReceiptOrDtlStatus.PutAwayed.GetCode()))
                            {
                                //wmStockDtl.stockDtlStatus = Convert.ToInt32(StockStatus.InStoreing.GetCode());
                                wmStockDtl.UpdateBy = LoginUserInfo == null ? "EBS" : LoginUserInfo.ITCode;
                                wmStockDtl.UpdateTime = DateTime.Now;
                                wmStockDtl.lockFlag = 1;
                                //wmStockDtl.inwhTime = DateTime.Now;
                                //DC.UpdateEntity(wmStockDtl);
                                //DC.SaveChanges();
                                await ((DbContext)DC).Set<WmsStockDtl>().SingleUpdateAsync(wmStockDtl);
                                await ((DbContext)DC).BulkSaveChangesAsync();
                            }
                        }
                        // 更新库存主表状态
                        //await wmsStockVM.UpdateStockStatusAsync(wmStockDtl.stockCode, Convert.ToInt32(StockStatus.InStore.GetCode()), isPK ? Convert.ToInt32(StockStatus.InStore.GetCode()) : null, invoker);
                    }
                }

                DateTime dt119 = DateTime.Now;
                TimeSpan second119 = dt119 - dt100;
                dateSecond = second119.TotalSeconds.ToString();
                logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}用时：{dateSecond}");
                #endregion

                #region 30 回传SRM

                // 内部创建无需回传
                if (wmsInReceiptIqcResult.sourceBy == Convert.ToInt32(SourceBy.WMS.GetCode()))
                {
                    returnFlag = 4;
                }
                else
                {
                    returnFlag = 3;
                }

                //更新入库记录回传标记
                foreach (var wmsInReceiptRecord in addReceiptRecordLists)
                {
                    wmsInReceiptRecord.returnFlag = Convert.ToInt32(returnFlag);
                    wmsInReceiptRecord.UpdateBy = invoker;
                    wmsInReceiptRecord.UpdateTime = DateTime.Now;
                    //DC.UpdateEntity(wmsInReceiptRecord);
                    await ((DbContext)DC).Set<WmsInReceiptRecord>().SingleUpdateAsync(wmsInReceiptRecord);
                }
                //DC.SaveChanges();
                await ((DbContext)DC).BulkSaveChangesAsync();
                #endregion

                DateTime dt120 = DateTime.Now;
                TimeSpan second120 = dt120 - dt100;
                dateSecond = second120.TotalSeconds.ToString();
                logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，{_vpoint}用时：{dateSecond}");
                if (hasParentTransaction == false)
                {
                    await DC.Database.CommitTransactionAsync();
                }

                #endregion
            }
            catch (Exception e)
            {
                if (hasParentTransaction == false)
                {
                    await DC.Database.RollbackTransactionAsync();
                }

                result.code = ResCode.Error;
                result.msg = $"WmsPutawayVM->DoPutaway 执行异常! 异常位置: [ {_vpoint} ], 异常信息: [ {e.Message} ]";
            }
            finally
            {
                logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，质检结果单号{inParams.iqcResultNo}入参{inJson},处理结果：{result.msg}");
            }

            return result;
        }


        /// <summary>
        /// 入库单据直接生成
        /// </summary>
        /// <param name="input"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public async Task<BusinessResult> DoSimplePut(DoSimpleDto input, string invoker)
        {
            BusinessResult result = new BusinessResult();
            var hasParentTransaction = false;
            string _vpoint = "";
            // 入参json
            string inJson = JsonConvert.SerializeObject(input);
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
                WmsInOrderVM wmsInOrderVM = Wtm.CreateVM<WmsInOrderVM>();
                WmsInReceiptUniicodeVM wmsInReceiptUniicodeVM = Wtm.CreateVM<WmsInReceiptUniicodeVM>();
                CfgDocTypeVM cfgDocTypeVM = Wtm.CreateVM<CfgDocTypeVM>();
                SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
                #region 判断入库单
                #region 校验入库单据
                _vpoint = "校验入库单据";
                var wmsInOrder = await wmsInOrderVM.GetWmsInOrderAsync(input.inNo);
                var wmsInOrderDetails = await wmsInOrderVM.GetWmsInOrderDtlsAsync(input.inNo);
                var wmsInReceiptUniicodes = await wmsInReceiptUniicodeVM.GetWmsInReceiptUniicodeByInNoAsync(input.inNo);
                if (wmsInOrder == null)
                {
                    throw new Exception($"没有查询到入库单号是 {input.inNo} 的入库单信息!");
                }

                if ((wmsInOrder.inStatus == Convert.ToInt32(InOrDtlStatus.Init.GetCode())) == false)
                {
                    throw new Exception($"入库单号是 {input.inNo} 的入库单的状态不是 [初始状态] 或者 [收货中]!");
                }
                // 11-2 检验入库类型参数是否维护
                CfgDocTypeDto cfgDocTypeView = await cfgDocTypeVM.GetCfgDocTypeAsync(wmsInOrder.docTypeCode);
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
                #endregion
                #region 校验入库明细单据
                _vpoint = "校验入库明细单据";
                if (wmsInOrderDetails.Count == 0)
                {
                    throw new Exception($"没有查询到入库单号是 {input.inNo} 的入库明细单信息!");
                }

                #endregion
                #region 校验入库唯一码信息
                _vpoint = "校验入库唯一码信息";
                if (wmsInReceiptUniicodes.Count == 0)
                {
                    throw new Exception($"没有查询到入库单号是 {input.inNo} 的入库唯一码信息!");
                }

                #endregion
                #endregion

                #region 业务逻辑

                #region 生成库存主表
                _vpoint = "生成库存主表";
                var stockCodeStr = await sysSequenceVM.GetSequenceAsync(SequenceCode.StockCode.GetCode()); // 库存编码
                WmsStock wmsStock = new WmsStock()
                {
                    areaNo = wmsInOrder.areaNo,
                    binNo = "",
                    errFlag = 0,
                    errMsg = "",
                    height = 0,
                    invoiceNo = "",
                    loadedType = 1,
                    locAllotGroup = "",
                    locNo = "",
                    palletBarcode = input.barCode,
                    proprietorCode = wmsInOrder.proprietorCode,
                    regionNo = "",
                    roadwayNo = "",
                    specialFlag = 0,
                    specialMsg = "",
                    stockCode = stockCodeStr,//
                    stockStatus = Convert.ToInt32(StockStatus.InitCreate.GetCode()),
                    weight = 0,
                    whouseNo = wmsInOrder.whouseNo,
                    CreateBy = invoker,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    UpdateBy = invoker,
                };
                await ((DbContext)DC).SingleInsertAsync(wmsStock);
                await ((DbContext)DC).BulkSaveChangesAsync();
                #endregion

                #region 生成库存明细表
                _vpoint = "生成库存明细表";
                //List<WmsStockDtl> wmsStockDtls = new List<WmsStockDtl>();
                foreach (var item in wmsInOrderDetails)
                {
                    var uniiCodeList = wmsInReceiptUniicodes.Where(x => x.inDtlId == item.ID).ToList();
                    WmsStockDtl wmsStockDtl = new WmsStockDtl()
                    {
                        areaNo = item.areaNo,
                        erpWhouseNo = item.erpWhouseNo,
                        inspectionResult = Convert.ToInt32(InspectionResult.Qualitified.GetCode()),
                        lockFlag = 0,
                        lockReason = "",
                        materialCode = item.materialCode,
                        materialName = item.materialName,
                        materialSpec = item.materialSpec,
                        occupyQty = 0,
                        palletBarcode = input.barCode,
                        projectNo = item.projectNo,
                        proprietorCode = item.proprietorCode,
                        qty = item.inQty,
                        skuCode = item.materialCode,
                        stockDtlStatus = Convert.ToInt32(StockStatus.InitCreate.GetCode()),
                        stockCode = wmsStock.stockCode,
                        supplierCode = item.supplierCode,
                        supplierName = item.supplierName,
                        supplierNameAlias = item.supplierNameAlias,
                        supplierNameEn = item.supplierNameEn,
                        whouseNo = item.whouseNo,
                        unitCode = item.unitCode,
                        CreateBy = item.CreateBy,
                        CreateTime = item.CreateTime,
                        UpdateBy = item.UpdateBy,
                        UpdateTime = item.UpdateTime,
                    };
                    if (wmsStockDtl != null)
                    {
                        await ((DbContext)DC).SingleInsertAsync(wmsStockDtl);
                        await ((DbContext)DC).BulkSaveChangesAsync();
                        #region 生成库存唯一码表
                        _vpoint = "生成库存唯一码表";
                        var stockId = wmsStockDtl.ID;
                        foreach (var unii in uniiCodeList)
                        {
                            WmsStockUniicode wmsStockUniicode = new WmsStockUniicode()
                            {
                                areaNo = wmsInOrder.areaNo,
                                batchNo = item.batchNo,
                                dataCode = unii.dataCode,
                                delayFrozenFlag = 0,
                                delayTimes = 0,
                                driedScrapFlag = 0,
                                driedTimes = 0,
                                erpWhouseNo = item.erpWhouseNo,
                                exposeFrozenFlag = 0,
                                inspectionResult = Convert.ToInt32(InspectionResult.Qualitified.GetCode()),
                                inwhTime = DateTime.Now,
                                leftMslTimes = 0,
                                materialCode = item.materialCode,
                                materialName = item.materialName,
                                materialSpec = item.materialSpec,
                                occupyQty = 0,
                                palletBarcode = input.barCode,
                                projectNo = item.projectNo,
                                proprietorCode = item.proprietorCode,
                                skuCode = unii.skuCode,
                                stockCode = wmsStock.stockCode,
                                stockDtlId = stockId,
                                qty = unii.qty,
                                supplierCode = unii.supplierCode,
                                supplierExposeTimes = 0,
                                supplierName = unii.supplierName,
                                supplierNameAlias = unii.supplierNameAlias,
                                supplierNameEn = unii.supplierNameEn,
                                uniicode = unii.uniicode,
                                unitCode = unii.unitCode,
                                whouseNo = item.whouseNo,
                                CreateBy = invoker,
                                CreateTime = DateTime.Now,
                                UpdateBy = invoker,
                                UpdateTime = DateTime.Now,
                            };
                            #region 更新入库唯一码
                            _vpoint = "更新入库唯一码";
                            unii.curStockCode = wmsStock.stockCode;
                            unii.curStockDtlId = stockId;
                            unii.curPalletBarcode = wmsStock.palletBarcode;
                            unii.runiiStatus = Convert.ToInt32(InUniicodeStatus.PutwayFinished.GetCode());
                            unii.UpdateBy = invoker;
                            unii.UpdateTime = DateTime.Now;
                            #endregion
                            await ((DbContext)DC).SingleInsertAsync(wmsStockUniicode);
                            await ((DbContext)DC).SingleInsertAsync(unii);
                            await ((DbContext)DC).BulkSaveChangesAsync();
                        }

                        #endregion
                    }
                }

                #endregion
                #region 生成上架单
                _vpoint = "生成上架单";
                var putawayNoSeq = await sysSequenceVM.GetSequenceAsync(SequenceCode.WmsPutawayNo.GetCode());
                WmsPutaway wmsPutaway = new WmsPutaway()
                {
                    areaNo = wmsInOrder.areaNo,
                    loadedType = 1,
                    manualFlag = 0,
                    onlineLocNo = "",
                    onlineMethod = "3",//直接上架
                    palletBarcode = input.barCode,
                    proprietorCode = wmsInOrder.proprietorCode,
                    ptaRegionNo = "",//上架库区
                    putawayNo = putawayNoSeq,//上架单号
                    putawayStatus = Convert.ToInt32(PutAwayOrDtlStatus.Init.GetCode()),
                    regionNo = "",
                    stockCode = "",
                    whouseNo = wmsInOrder.whouseNo,
                    CreateBy = invoker,
                    CreateTime = DateTime.Now,
                    UpdateBy = invoker,
                    UpdateTime = DateTime.Now
                };
                await ((DbContext)DC).SingleInsertAsync(wmsPutaway);
                await ((DbContext)DC).BulkSaveChangesAsync();
                #endregion
                #region 生成上架明细单
                _vpoint = "生成上架明细单";
                wmsInReceiptUniicodes = await wmsInReceiptUniicodeVM.GetWmsInReceiptUniicodeByInNoAsync(input.inNo);
                List<WmsPutawayDtl> wmsPutawayDtls = new List<WmsPutawayDtl>();
                foreach (var item in wmsInReceiptUniicodes)
                {
                    WmsPutawayDtl wmsPutawayDtl = new WmsPutawayDtl()
                    {
                        areaNo = wmsInOrder.areaNo,
                        binNo = "",
                        docTypeCode = wmsInOrder.docTypeCode,
                        erpWhouseNo = item.erpWhouseNo,
                        inspectionResult = Convert.ToInt32(InspectionResult.Qualitified.GetCode()),
                        materialCode = item.materialCode,
                        materialName = item.materialName,
                        materialSpec = item.materialSpec,
                        orderDtlId = item.ID,
                        orderNo = item.inNo,
                        palletBarcode = input.barCode,
                        projectNo = item.projectNo,
                        proprietorCode = item.proprietorCode,
                        ptaBinNo = "",
                        putawayDtlStatus = Convert.ToInt32(PutAwayOrDtlStatus.Init.GetCode()),
                        putawayNo = wmsPutaway.putawayNo,
                        recordQty = item.qty,
                        regionNo = "",
                        roadwayNo="",
                        skuCode=item.materialCode,
                        stockDtlId=(long)item.curStockDtlId,
                        stockCode=item.curStockCode,
                        supplierCode=item.supplierCode,
                        supplierName=item.supplierName,
                        supplierNameAlias=item.supplierNameAlias,
                        supplierNameEn=item.supplierNameEn,
                        whouseNo=item.whouseNo,
                        unitCode=item.unitCode,
                        CreateBy = invoker,
                        CreateTime = DateTime.Now,
                        UpdateBy = invoker,
                        UpdateTime = DateTime.Now,
                    };
                    if (wmsPutawayDtl!=null)
                    {
                        wmsPutawayDtls.Add(wmsPutawayDtl);
                    }
                }
                await ((DbContext)DC).BulkInsertAsync(wmsPutawayDtls);
                await ((DbContext)DC).BulkSaveChangesAsync();
                #endregion

                #region 更新入库明细
                _vpoint = "更新入库明细";
                foreach (var item in wmsInOrderDetails)
                {
                    item.recordQty = item.inQty;
                    item.putawayQty = item.recordQty;
                    item.inDtlStatus = Convert.ToInt32(InOrDtlStatus.PutAwayed.GetCode());
                    item.UpdateBy = LoginUserInfo == null ? "WMS" : LoginUserInfo.ITCode;
                    item.UpdateTime = DateTime.Now;
                }
                await ((DbContext)DC).BulkInsertAsync(wmsInOrderDetails);
                await ((DbContext)DC).BulkSaveChangesAsync();
                #endregion

                #region 更新入库单
                _vpoint = "更新入库单";
                await wmsInOrderVM.UpdateInOrderStatusAsync(input.inNo, Convert.ToInt32(InOrDtlStatus.PutAwayed.GetCode()),
                         Convert.ToInt32(InOrDtlStatus.PutAwaying.GetCode()), invoker);

                #endregion
                #region 上线操作
                _vpoint = "上线操作";
                PutAwayOnlineDto putAwayOnlineParams = new PutAwayOnlineDto()
                {
                    palletBarcode = input.barCode,
                    locNo = "2601"
                };
                result = await PutAwayOnlineAsync(putAwayOnlineParams, invoker);

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
                result.code = ResCode.Error;
                result.msg = $"WmsPutawayVM->DoPutaway 执行异常! 异常位置: [ {_vpoint} ], 异常信息: [ {ex.Message} ]";
            }
            finally
            {
                logger.Warn($"--->{DateTime.Now}：组盘上架，操作人:{invoker}，入库单号DoSimplePut{input.inNo}入参{inJson},处理结果：{result.msg}");
            }
            return result;
        }
    }
}
