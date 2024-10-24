using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using Com.Wish.Model.Base;
using Microsoft.EntityFrameworkCore;
using System.DirectoryServices.Protocols;
using Wish.Areas.BasWhouse.Model;
using Wish.Areas.Config.Model;
using Wish.ViewModel.Base.BasBMaterialVMs;
using Wish.ViewModel.Config.CfgDocTypeVMs;
using WISH.Helper.Common;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using WISH.ViewModels.Common.Dtos;
using Wish.ViewModel.BusinessIn.WmsInReceiptUniicodeVMs;
using Wish.ViewModel.System.SysSequenceVMs;
using Wish.ViewModel.BusinessIn.WmsInReceiptVMs;
using Wish.DtoModel.Common.Dtos;
using Wish.ViewModel.Common.Dtos;
using Quartz.Util;

namespace Wish.ViewModel.BusinessIn.WmsInOrderVMs
{
    public partial class WmsInOrderVM
    {

        /// <summary>
        /// 通过WMS系统创建入库订单
        /// </summary>
        /// <param name="orderView"></param>
        /// <returns></returns>
        public async Task<BusinessResult> CreateInOrderByWMS(WmsInOrderDto orderView, string invoker)
        {
            WmsInOrderDto wmsInOrderView = new WmsInOrderDto();
            List<WmsInOrderDtl> detailList = new List<WmsInOrderDtl>();
            WmsInOrder wmsInOrder = new WmsInOrder();

            //TODO：补齐创建参数

            #region 1、主表参数验证

            BusinessResult result = new BusinessResult();
            if (orderView.wmsInOrderMain.cvName == "" || orderView.wmsInOrderMain.cvName == null)
            {
                result.code = ResCode.Error;
                result.msg = $"入参客供名称cvName为空";
                return result;
            }

            if (orderView.wmsInOrderMain.docTypeCode == "" || orderView.wmsInOrderMain.docTypeCode == null)
            {
                result.code = ResCode.Error;
                result.msg = $"入参单据类型docTypeCode为空";
                return result;
            }
            if (orderView.wmsInOrderMain.areaNo == "" || orderView.wmsInOrderMain.areaNo == null)
            {
                result.code = ResCode.Error;
                result.msg = $"入参楼号areaNo为空";
                return result;
            }

            if (string.IsNullOrWhiteSpace(orderView.wmsInOrderMain.erpWhouseNo))
            {
                result.code = ResCode.Error;
                result.msg = $"入参ERP仓库erpWhouseNo为空";
                return result;
            }


            #endregion

            #region 主表数据补齐

            //根据单据类型查询查询客供类型
            var cvTypeDoc = await DC.Set<CfgDocType>().Where(x => x.usedFlag == 1 && x.docTypeCode == orderView.wmsInOrderMain.docTypeCode).FirstOrDefaultAsync();
            if (cvTypeDoc == null || string.IsNullOrWhiteSpace(cvTypeDoc.cvType))
            {
                result.code = ResCode.Error;
                result.msg = $"根据单据类型查询客供类型为空";
                return result;
            }

            var supplier = await DC.Set<BasBSupplier>().Where(x =>
                    x.usedFlag == 1 && (x.supplierCode == orderView.wmsInOrderMain.cvCode || x.supplierCode == orderView.wmsInOrderMain.cvName))
                .FirstOrDefaultAsync();
            if (supplier == null || supplier.supplierName == "")
            {
                result.code = ResCode.Error;
                result.msg = $"根据客供类型查询供应商类型为空";
                return result;
            }

            wmsInOrder.docTypeCode = orderView.wmsInOrderMain.docTypeCode;
            wmsInOrder.cvType = cvTypeDoc.cvType;
            wmsInOrder.cvCode = orderView.wmsInOrderMain.cvName;
            wmsInOrder.cvName = supplier.supplierName;
            wmsInOrder.cvNameEn = supplier.supplierNameEn;
            wmsInOrder.cvNameAlias = supplier.supplierNameAlias;
            wmsInOrder.proprietorCode = string.IsNullOrWhiteSpace(orderView.wmsInOrderMain.proprietorCode) ? "TZ" : orderView.wmsInOrderMain.proprietorCode;
            //根据楼号查询仓库号
            var whouseArea = await DC.Set<BasWArea>().Where(x => x.usedFlag == 1 && x.areaNo == orderView.wmsInOrderMain.areaNo).FirstOrDefaultAsync();
            if (whouseArea == null || whouseArea.whouseNo == "")
            {
                result.code = ResCode.Error;
                result.msg = $"根据楼号查询仓库编码为空";
                return result;
            }

            wmsInOrder.areaNo = orderView.wmsInOrderMain.areaNo;
            wmsInOrder.whouseNo = whouseArea.whouseNo;
            // todo:送货方式
            wmsInOrder.deliverMode = "";
            // todo:预计到货日期
            wmsInOrder.planArrivalDate = DateTime.Now;
            // todo:操作理由
            wmsInOrder.operationReason = "";
            wmsInOrder.orderDesc = orderView.wmsInOrderMain.orderDesc;
            // todo:外部单号ID
            wmsInOrder.externalInId = string.IsNullOrWhiteSpace(orderView.wmsInOrderMain.externalInId) ? "" : orderView.wmsInOrderMain.externalInId;
            wmsInOrder.externalInNo = orderView.wmsInOrderMain.externalInNo;
            // todo:出入库类别代码
            wmsInOrder.inOutTypeNo = "IN";
            // todo:出入库名称
            wmsInOrder.inOutName = "入库";
            wmsInOrder.CreateBy = invoker;
            wmsInOrder.CreateTime = DateTime.Now;
            wmsInOrder.sourceBy = 0;
            wmsInOrder.ticketNo = orderView.wmsInOrderMain.ticketNo;

            #endregion

            #region 明细数据验证

            if (orderView.wmsInOrderDetail.Count() == 0 || orderView.wmsInOrderDetail == null)
            {
                result.code = ResCode.Error;
                result.msg = $"入参明细单orderView.wmsInOrderDetail为空";
                return result;
            }

            #endregion

            #region 明细数据补齐
            for (int i = 0; i < orderView.wmsInOrderDetail.Count; i++)
            {
                int row = i + 1;
                if (orderView.wmsInOrderDetail[i].supplierCode == "")
                {
                    result.code = ResCode.Error;
                    result.msg = $"入参明细单供应商supplierCode为空";
                    return result;
                }

                if (orderView.wmsInOrderDetail[i].materialCode == "")
                {
                    result.code = ResCode.Error;
                    result.msg = $"入参明细单物料编码supplierCode为空";
                    return result;
                }

                var material = await DC.Set<BasBMaterial>().Where(x => x.UsedFlag == 1 && x.MaterialCode == orderView.wmsInOrderDetail[i].materialCode).FirstOrDefaultAsync();
                if (material == null || material.MaterialName == "")
                {
                    result.code = ResCode.Error;
                    result.msg = $"根据物料编码查询物料信息为空";
                    return result;
                }

                WmsInOrderDtl wmsInOrderDetail = new WmsInOrderDtl();
                wmsInOrderDetail.whouseNo = whouseArea.whouseNo;
                wmsInOrderDetail.areaNo = orderView.wmsInOrderMain.areaNo;
                wmsInOrderDetail.erpWhouseNo = orderView.wmsInOrderMain.erpWhouseNo;
                wmsInOrderDetail.proprietorCode = wmsInOrder.proprietorCode;
                //wmsInOrderDetail.inNo = inOrderNo;
                wmsInOrderDetail.externalInNo = orderView.wmsInOrderMain.externalInNo;
                wmsInOrderDetail.externalInDtlId = string.IsNullOrWhiteSpace(orderView.wmsInOrderMain.externalInId) ? "" : orderView.wmsInOrderMain.externalInId;
                wmsInOrderDetail.orderNo = "";
                wmsInOrderDetail.orderDtlId = "";
                wmsInOrderDetail.materialCode = orderView.wmsInOrderDetail[i].materialCode;
                wmsInOrderDetail.materialName = orderView.wmsInOrderDetail[i].materialName;
                wmsInOrderDetail.supplierCode = orderView.wmsInOrderDetail[i].supplierCode;
                wmsInOrderDetail.supplierName = supplier.supplierName;
                wmsInOrderDetail.supplierNameEn = supplier.suppilerFullnameEn;
                wmsInOrderDetail.supplierNameAlias = supplier.suppilerFullnameAlias;
                wmsInOrderDetail.batchNo = orderView.wmsInOrderDetail[i].batchNo;
                wmsInOrderDetail.materialSpec = material.MaterialSpec;
                //wmsInOrderDetail.docNum = orderView.wmsInOrderDetail[i].docNum;
                wmsInOrderDetail.inQty = orderView.wmsInOrderDetail[i].inQty;
                wmsInOrderDetail.receiptQty = 0;
                wmsInOrderDetail.qualifiedQty = 0;
                wmsInOrderDetail.qualifiedSpecialQty = 0;
                wmsInOrderDetail.unqualifiedQty = 0;
                wmsInOrderDetail.returnQty = 0;
                wmsInOrderDetail.rejectQty = 0;
                wmsInOrderDetail.recordQty = 0;
                wmsInOrderDetail.putawayQty = 0;
                wmsInOrderDetail.postBackQty = 0;
                //wmsInOrderDetail.inDtlStatus = "0";
                wmsInOrderDetail.productSn = string.IsNullOrWhiteSpace(orderView.wmsInOrderDetail[i].productSn) ? "" : orderView.wmsInOrderDetail[i].productSn;
                wmsInOrderDetail.departmentName = orderView.wmsInOrderDetail[i].departmentName;
                wmsInOrderDetail.projectNo = orderView.wmsInOrderDetail[i].projectNo ?? "";
                wmsInOrderDetail.inspector = "";
                wmsInOrderDetail.purchaseOrderNo = orderView.wmsInOrderDetail[i].purchaseOrderNo;
                wmsInOrderDetail.purchaseOrderId = orderView.wmsInOrderDetail[i].purchaseOrderId;
                wmsInOrderDetail.purchaseOrderMaker = orderView.wmsInOrderDetail[i].purchaseOrderMaker;
                wmsInOrderDetail.minPkgQty = material.MinPkgQty;
                wmsInOrderDetail.urgentFlag = 0;
                wmsInOrderDetail.unitCode = material.UnitCode;
                //wmsInOrderDetail.replenishFlag = "0";
                wmsInOrderDetail.intfId = "";
                wmsInOrderDetail.intfBatchId = "";
                wmsInOrderDetail.companyCode = "A01";
                //wmsInOrderDetail.delFlag = "0";
                wmsInOrderDetail.CreateBy = invoker;
                wmsInOrderDetail.CreateTime = DateTime.Now;
                wmsInOrderDetail.extend1 = row.ToString();
                detailList.Add(wmsInOrderDetail);
            }

            #endregion

            wmsInOrderView.wmsInOrderMain = wmsInOrder;
            wmsInOrderView.wmsInOrderDetail = detailList;
            result = await CreateInOrder(wmsInOrderView, invoker);
            return result;
        }

        /// <summary>
        /// 创建入库订单
        /// </summary>
        /// <param name="orderView"></param>
        public async Task<BusinessResult> CreateInOrder(WmsInOrderDto orderView, string invoker)
        {
            BusinessResult result = new BusinessResult();
            result.msg = "执行成功!";
            string _vpoint = "";
            var hasParentTransaction = false;
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

                _vpoint = "创建业务相关的基础VM对象";
                CfgDocTypeVM cfgDocTypeVM = Wtm.CreateVM<CfgDocTypeVM>();
                WmsInReceiptUniicodeVM wmsInReceiptUniicodeVM = Wtm.CreateVM<WmsInReceiptUniicodeVM>();
                BasBMaterialVM basBMaterialVM = Wtm.CreateVM<BasBMaterialVM>();
                SysSequenceVM sysSequenceVM = Wtm.CreateVM<SysSequenceVM>();
                WmsInReceiptVM wmsInReceiptVM = Wtm.CreateVM<WmsInReceiptVM>();
                WmsInReceiptInParamDto wmsInReceiptInParamView = new WmsInReceiptInParamDto();
                //wmsInReceiptInParamView.inNo = orderView.wmsInOrderMain.inNo;
                Dictionary<string, BasBMaterialDto> basBMaterialViews = new Dictionary<string, BasBMaterialDto>();

                #region 1  校验

                _vpoint = "检查物料信息是否在物料表中存在";
                var order = orderView.wmsInOrderMain;
                var orderDetails = orderView.wmsInOrderDetail;
                #region 10 检查 入参是否与已存在的单据一致
                /*防止外部接口调用同一个接口，同一个参数两次
                 * 主表判断
                 * 1、在外部单号不为空情况下，判断是否存在同一个外部单号，外部单行号
                 */
                //foreach (var item in orderDetails)
                //{
                //    if (!item.externalInNo.IsNullOrWhiteSpace() && !item.externalInId.IsNullOrWhiteSpace())
                //    {
                //        var inOrderDtlOld = DC.Set<WmsInOrderDetail>().Where(x => x.externalInNo == item.externalInNo && x.externalInId == item.externalInId).FirstOrDefault();
                //        if (inOrderDtlOld != null)
                //        {
                //            throw new Exception($"外部入库单号 {order.externalInNo}，行号{order.externalInId} 已生成入库单，如需更新明细请更新入库单。");
                //        }
                //    }
                //}
                if (!string.IsNullOrWhiteSpace(order.externalInNo))
                {
                    var oldWmsInOrder = await DC.Set<WmsInOrder>().Where(t => t.externalInNo == order.externalInNo).FirstOrDefaultAsync();
                    if (oldWmsInOrder != null)
                    {
                        result = await UpdateInOrder(orderView, invoker);
                        if (result.code == ResCode.OK)
                        {
                            if (hasParentTransaction == false)
                            {
                                await DC.Database.CommitTransactionAsync();
                            }
                        }
                        return result;
                    }
                }

                #endregion

                #region 11 检查物料信息是否存在


                var materialCodes = orderView.wmsInOrderDetail.Select(x => x.materialCode).ToList();
                List<string> notExitsMaterials = await basBMaterialVM.SelectNotExistMaterialsAsync(materialCodes);
                if (notExitsMaterials.Any())
                {
                    throw new Exception($"物料编码 {notExitsMaterials.First()} 在物料表中不存在!");
                }

                // 填充物料信息
                foreach (var item in orderDetails)
                {
                    if (basBMaterialViews.ContainsKey(item.materialCode) == false)
                    {
                        basBMaterialViews.Add(item.materialCode, await basBMaterialVM.GetBasBMaterialAsync(item.materialCode));
                    }
                }

                #endregion

                #region 12 检查单据类型参数

                _vpoint = "检查单据类型参数";

                // 检查是否直接生成唯一码
                bool generateUniicode = false;
                bool print = false;
                bool autoReceipt = false;
                bool feedbackSRM = false;
                bool isBatch = false;

                #endregion

                #region 13 成品物料检验
                #region 21 单据类型是否存在

                _vpoint = "检查单据类型是否存在";
                if (order.docTypeCode == null)
                {
                    throw new Exception($"入库单单据类型不能为空!");
                }

                CfgDocTypeDto cfgDocTypeView = await cfgDocTypeVM.GetCfgDocTypeAsync(order.docTypeCode);

                // 创建单据类型
                if (cfgDocTypeView.cfgDocType == null)
                {
                    await cfgDocTypeVM.CreateInOrderCfgDocTypeAsync(order.docTypeCode, invoker);
                }

                //cfgDocTypeView = cfgDocTypeVM.GetCfgDocType(order.docTypeCode);

                if (cfgDocTypeView.cfgDocTypeDtls.Count == 0)
                {
                    throw new Exception($"单据类型 {order.docTypeCode} 参数未配置!");
                }

                // 获取单据类型相关配置参数
                generateUniicode = cfgDocTypeView.GetParamCodeValue(InOrderDocTypeParam.GenerateUnnicode.GetCode()) == YesNoCode.Yes.GetCode();
                print = cfgDocTypeView.GetParamCodeValue(InOrderDocTypeParam.Print.GetCode()) == YesNoCode.Yes.GetCode();
                autoReceipt = cfgDocTypeView.GetParamCodeValue(InOrderDocTypeParam.AutoReceipt.GetCode()) == YesNoCode.Yes.GetCode();
                feedbackSRM = cfgDocTypeView.GetParamCodeValue(InOrderDocTypeParam.ReturnFlag.GetCode()) == YesNoCode.Yes.GetCode();
                // isBatch==False,不批次管理
                isBatch = cfgDocTypeView.GetParamCodeValue(InOrderDocTypeParam.IsBatch.GetCode()) == YesNoCode.Yes.GetCode();

                #endregion

                _vpoint = "成品物料检验";
                foreach (var m in orderDetails)
                {
                    if (basBMaterialViews[m.materialCode].basBMaterialCategory == null)
                    {
                        //throw new Exception($"物料 {m.materialCode} --{m.materialName}没有物料大类信息!");
                    }
                    if (basBMaterialViews[m.materialCode].basBMaterialType == null)
                    {
                        //TODO：要不要判断物料类型为空
                        //throw new Exception($"物料 {m.materialCode} --{m.materialName}没有物料类型信息!");
                    }

                    if (basBMaterialViews[m.materialCode].basBMaterial.Extend2.Equals(MaterialFlag.Product.GetCode()))
                    {
                        // 检查成品的序列码不为空
                        if (string.IsNullOrWhiteSpace(m.productSn))
                        {
                            throw new Exception($"物料 {m.materialCode} {m.materialName} 是成品物料, 成品序列码不能为空!");
                        }

                        // 检查成品唯一码是否已经在入库唯一码中存在
                        bool exist = await wmsInReceiptUniicodeVM.CheckUniicodeExistAsync(m.productSn);
                        if (exist)
                        {
                            if (isBatch)
                            {
                                throw new Exception($"成品序列码 {m.productSn} 在入库唯一码表中已经存在!");
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(m.productSn))
                        {
                            throw new Exception($"成品序列码【{m.productSn}】不为空，但物料【{m.materialCode}】在WMS中维护的是【{basBMaterialViews[m.materialCode].basBMaterial.Extend2}】类型");
                        }
                    }
                }

                #endregion

                #endregion

                #region 2  处理逻辑


                #region 22 入库单处理

                _vpoint = "插入入库单主表";
                order.inStatus = Convert.ToInt32(InOrDtlStatus.Init.GetCode());
                order.inNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.InOrderNo.GetCode());
                //order.ID = Guid.NewGuid().ToString();

                // 修改回传标记
                if (feedbackSRM)
                {
                    //order.returnFlag = InOrderReturnFlag.InitCreate.GetCode();
                }
                else
                {
                    //order.returnFlag = InOrderReturnFlag.NoReturn.GetCode();
                }

                order.CreateTime = DateTime.Now;
                order.CreateBy = invoker;// "Admin";
                wmsInReceiptInParamView.inNo = order.inNo;
                //DC.AddEntity(order);
                //DC.SaveChanges();
                //await ((DbContext)DC).BulkInsertAsync(new WmsInOrder[] { order });
                await ((DbContext)DC).SingleInsertAsync(order);
                await ((DbContext)DC).SaveChangesAsync();
                #endregion

                #region 23 入库单明细处理

                _vpoint = "入库单明细处理";
                foreach (var m in orderDetails)
                {
                    // 内部单据需要生成批次号
                    if (orderView.wmsInOrderMain.sourceBy == Convert.ToInt32(SourceBy.WMS.GetCode()))
                    {
                        if (string.IsNullOrWhiteSpace(m.batchNo))
                        {
                            m.batchNo = await sysSequenceVM.GetSequenceAsync(SequenceCode.SrmBatchNo.GetCode());
                        }
                    }

                    m.inNo = order.inNo;
                    m.inDtlStatus = Convert.ToInt32(InOrDtlStatus.Init.GetCode());
                    m.CreateBy = invoker;
                    m.CreateTime = DateTime.Now;
                }
                //DC.Set<WmsInOrderDetail>().AddRange(orderDetails);
                //DC.SaveChanges();
                await ((DbContext)DC).BulkInsertAsync(orderDetails);
                await ((DbContext)DC).SaveChangesAsync();
                #endregion

                #region 24 入库唯一码处理

                _vpoint = "入库唯一码处理";
                List<WmsInReceiptUniicode> uniicodeWaitAdd = new List<WmsInReceiptUniicode>();
                foreach (var m in orderDetails)
                {
                    if (cfgDocTypeView.cfgDocType.docTypeCode == BusinessCode.InSemiProFinish.GetCode())
                    {
                        if (m.batchNo.IsNullOrWhiteSpace())
                        {
                            m.batchNo = await sysSequenceVM.GetSequenceAsync(DictonaryHelper.SequenceCode.SrmBatchNo.GetCode());
                        }
                    }

                    // 成品：插入入库唯一码表：直接生成入库唯一码信息
                    if (basBMaterialViews[m.materialCode].basBMaterial.Extend2.Equals(MaterialFlag.Product.GetCode()))
                    {
                        WmsInReceiptInParamInDtlnfoDto wmsInReceiptInParamInDtlnfoView = new WmsInReceiptInParamInDtlnfoDto();
                        wmsInReceiptInParamInDtlnfoView.inDtlId = m.ID;
                        WmsInReceiptUniicode uniicode = await wmsInReceiptUniicodeVM.MapWmsInReceiptUniicode(m, invoker);
                        uniicode.uniicode = m.productSn;
                        wmsInReceiptInParamInDtlnfoView.uniicode = uniicode.uniicode;
                        //wmsInReceiptInParamInDtlnfoView.receiptQty = (decimal)m.docNum;
                        wmsInReceiptInParamInDtlnfoView.receiptQty = (decimal)m.inQty;

                        uniicodeWaitAdd.Add(uniicode);
                        wmsInReceiptInParamView.inDtlsInfos.Add(wmsInReceiptInParamInDtlnfoView);
                    }
                    else
                    {
                        // 生成唯一码
                        if (generateUniicode)
                        {
                            // 直接生成，一行明细生成一条唯一码
                            WmsInReceiptUniicodeGenerateDto inputParm = new WmsInReceiptUniicodeGenerateDto
                            {
                                materialCode = m.materialCode,
                                supplierCode = m.supplierCode,
                                //qty = (decimal)m.docNum,
                                //packageQty = (decimal)m.pkgQty,
                                qty = (decimal)m.inQty,
                                packageQty = (decimal)m.minPkgQty,
                                batchNo = m.batchNo,
                                inDtlID = m.ID
                            };

                            var uniicodeResult = await wmsInReceiptUniicodeVM.GenerateUniicode(inputParm, invoker);
                            List<UniicodeDto> uniicodes = new List<UniicodeDto>();
                            if (uniicodeResult.code != 0)
                            {
                                throw new Exception(uniicodeResult.msg);
                            }
                            else
                            {

                                uniicodes = (List<UniicodeDto>)uniicodeResult.outParams;
                                foreach (var uniicodeView in uniicodes)
                                {
                                    WmsInReceiptInParamInDtlnfoDto wmsInReceiptInParamInDtlnfoView = new WmsInReceiptInParamInDtlnfoDto();
                                    wmsInReceiptInParamInDtlnfoView.inDtlId = m.ID;
                                    wmsInReceiptInParamInDtlnfoView.uniicode = uniicodeView.uniicode;
                                    wmsInReceiptInParamInDtlnfoView.receiptQty = uniicodeView.qty;
                                    wmsInReceiptInParamInDtlnfoView.batchNo = m.batchNo;
                                    wmsInReceiptInParamView.inDtlsInfos.Add(wmsInReceiptInParamInDtlnfoView);
                                }
                            }
                        }
                    }
                }

                // 批量添加成品唯一码信息
                if (uniicodeWaitAdd.Count > 0)
                {
                    //DC.Set<WmsInReceiptUniicode>().AddRange(uniicodeWaitAdd);
                    //DC.SaveChanges();
                    await ((DbContext)DC).BulkInsertAsync(uniicodeWaitAdd);
                    await ((DbContext)DC).SaveChangesAsync();
                }

                #endregion

                #region 25 自动收货

                _vpoint = "自动收货";
                if (generateUniicode == false && autoReceipt == true)
                {
                    throw new Exception($"如果自动收货，则单据类型应该配置为 自动生成唯一码!");
                }
                if (autoReceipt && generateUniicode)
                {
                    var receiptResult = await wmsInReceiptVM.DoReceipt(wmsInReceiptInParamView, invoker);
                    if (receiptResult.code != 0)
                    {
                        throw new Exception(receiptResult.msg);
                    }
                }

                #endregion
                #region 26 直接生成上架单，上架中库存，任务

                #endregion

                #endregion


                if (hasParentTransaction == false)
                {
                    await DC.Database.CommitTransactionAsync();
                }

                result.outParams = order;
            }
            catch (Exception ex)
            {
                if (hasParentTransaction == false)
                {
                    await DC.Database.RollbackTransactionAsync();
                }

                result.code = ResCode.Error;
                result.msg = $"WmsInOrderVM->CreateInOrder执行异常! 异常位置: [ {_vpoint} ], 异常信息: [ {ex.Message} ]";
            }

            return result;
        }
    }
}
