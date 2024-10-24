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
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using Wish.Areas.BasWhouse.Model;
using Wish.DtoModel.Common.Dtos;
using Wish.ViewModel.BusinessIn.WmsInOrderVMs;
using Wish.ViewModel.Common.Dtos;
using WISH.Helper.Common.Dictionary.DictionaryHelper;

namespace Wish.ViewModel.BusinessIn.WmsInReceiptVMs
{
    public partial class WmsInReceiptVM
    {
        /// <summary>
        /// 创建收货单
        /// </summary>
        /// <returns></returns>
        private WmsInReceiptDto CreateInReceiptInfo()
        {
            WmsInReceiptDto wir = new WmsInReceiptDto();
            return wir;
        }
        private WmsStockUniicode BuildWmsStockUniicode(WmsInReceiptUniicode uniicode, List<BasBMaterialDto> basBMaterialViews, string invoker)
        {
            //BasBMaterialVM basBMaterialVM = Wtm.CreateVM<BasBMaterialVM>();
            WmsInOrderVM wmsInOrderVm = Wtm.CreateVM<WmsInOrderVM>();
            //var _basBMaterialView = basBMaterialVM.GetBasBMaterial(uniicode.materialCode);
            var _basBMaterialView = basBMaterialViews.Where(x => x.basBMaterial.MaterialCode == uniicode.materialCode).FirstOrDefault();
            if (_basBMaterialView == null || _basBMaterialView.basBMaterialCategory == null ||
                _basBMaterialView.basBMaterial == null)
            {
                throw new Exception($"物料信息 {uniicode.materialCode} 维护不完整!");
            }

            int? unpackStatus = null;
            if (_basBMaterialView.basBMaterialCategory.materialFlag != null && _basBMaterialView.basBMaterialCategory.materialFlag == MaterialFlag.Electronic.GetCode())
            {
                unpackStatus = 0;
            }
            WmsInOrderDtl wmsInOrderDetail = wmsInOrderVm.GetWmsInOrderDtl((long)uniicode.inDtlId);

            WmsStockUniicode wmsStockUniicode = new WmsStockUniicode();
            wmsStockUniicode.whouseNo = uniicode.whouseNo;
            //wmsStockUniicode.areaNo = uniicode.areaNo;
            wmsStockUniicode.areaNo = wmsInOrderDetail.areaNo;
            wmsStockUniicode.erpWhouseNo = uniicode.erpWhouseNo;
            wmsStockUniicode.proprietorCode = uniicode.proprietorCode;
            wmsStockUniicode.uniicode = uniicode.uniicode;
            wmsStockUniicode.stockCode = uniicode.curStockCode;
            wmsStockUniicode.stockDtlId = (long)uniicode.curStockDtlId;
            wmsStockUniicode.palletBarcode = uniicode.curPalletBarcode;
            wmsStockUniicode.positionNo = uniicode.curPositionNo;
            wmsStockUniicode.projectNo = uniicode.projectNo;
            wmsStockUniicode.materialCode = uniicode.materialCode;
            wmsStockUniicode.materialName = uniicode.materialName;
            wmsStockUniicode.supplierCode = uniicode.supplierCode;
            wmsStockUniicode.supplierName = uniicode.supplierName;
            wmsStockUniicode.supplierNameEn = uniicode.supplierNameEn;
            wmsStockUniicode.supplierNameAlias = uniicode.supplierNameAlias;
            wmsStockUniicode.batchNo = uniicode.batchNo;
            wmsStockUniicode.materialSpec = uniicode.materialSpec;
            wmsStockUniicode.qty = uniicode.qty;
            wmsStockUniicode.occupyQty = 0; // wmsStockDtl.occupyQty;
            wmsStockUniicode.inspectionResult = 0;
            wmsStockUniicode.inwhTime = DateTime.Now;
            wmsStockUniicode.mslGradeCode = uniicode.mslGradeCode;
            wmsStockUniicode.dataCode = uniicode.dataCode;
            wmsStockUniicode.productDate = uniicode.productDate;
            wmsStockUniicode.expDate = uniicode.expDate;
            //wmsStockUniicode.delayToEndDate = uniicode.delayToEndDate;
            wmsStockUniicode.delayTimes = 0;
            //wmsStockUniicode.delayReason = wmsStockDtl.delayReason;
            wmsStockUniicode.delayFrozenFlag = 0;
            //wmsStockUniicode.frozenReason = wmsStockDtl.frozenReason;
            wmsStockUniicode.exposeFrozenFlag = 0;
            //wmsStockUniicode.exposeFrozenReason = wmsStockDtl.exposeFrozenReason;
            wmsStockUniicode.supplierExposeTimes = uniicode.supplierExposeTimeDuration;
            wmsStockUniicode.realExposeTimes = uniicode.supplierExposeTimeDuration;
            wmsStockUniicode.unpackStatus = unpackStatus;
            //wmsStockUniicode.unpackTime = wmsStockDtl.unpackTime;
            //wmsStockUniicode.packagedTime = wmsStockDtl.packagedTime;
            //wmsStockUniicode.leftMslTimes = wmsStockDtl.leftMslTimes;
            wmsStockUniicode.driedTimes = 0;
            wmsStockUniicode.driedScrapFlag = 0;
            wmsStockUniicode.skuCode = uniicode.materialCode;
            wmsStockUniicode.CreateBy = invoker;// LoginUserInfo?.ITCode;
            wmsStockUniicode.CreateTime = DateTime.Now;
            wmsStockUniicode.UpdateBy = invoker;// LoginUserInfo?.ITCode;
            wmsStockUniicode.unpackTime = DateTime.Now;
            wmsStockUniicode.extend1 = uniicode.extend1;
            wmsStockUniicode.extend2 = uniicode.extend2;
            wmsStockUniicode.extend3 = uniicode.extend3;
            wmsStockUniicode.extend4 = uniicode.extend4;
            wmsStockUniicode.extend5 = uniicode.extend5;
            wmsStockUniicode.extend6 = uniicode.extend6;
            wmsStockUniicode.extend7 = uniicode.extend7;
            wmsStockUniicode.extend8 = uniicode.extend8;
            wmsStockUniicode.extend9 = uniicode.extend9;
            wmsStockUniicode.extend10 = uniicode.extend10;
            wmsStockUniicode.extend11 = uniicode.extend11;
            wmsStockUniicode.chipSize = uniicode.extend12;
            wmsStockUniicode.chipThickness = uniicode.extend13;
            wmsStockUniicode.chipModel = uniicode.extend14;
            wmsStockUniicode.dafType = uniicode.extend15;

            return wmsStockUniicode;
        }

        private WmsStockDtl BuildInReceiptStockDtl(WmsStock wmsStock,
            WmsInReceiptDtl wmsInReceiptDt, string invoker)
        {
            WmsStockDtl wmsStockDtl = new WmsStockDtl();
            wmsStockDtl.whouseNo = wmsInReceiptDt.whouseNo;
            wmsStockDtl.areaNo = wmsInReceiptDt.areaNo;
            wmsStockDtl.erpWhouseNo = wmsInReceiptDt.erpWhouseNo;
            wmsStockDtl.proprietorCode = wmsStock.proprietorCode;
            wmsStockDtl.stockCode = wmsStock.stockCode;
            wmsStockDtl.palletBarcode = wmsStock.palletBarcode;
            wmsStockDtl.projectNo = wmsInReceiptDt.projectNo;
            wmsStockDtl.projectNoBak = wmsInReceiptDt.projectNo;
            wmsStockDtl.materialCode = wmsInReceiptDt.materialCode;
            wmsStockDtl.materialName = wmsInReceiptDt.materialName;
            wmsStockDtl.supplierCode = wmsInReceiptDt.supplierCode;
            wmsStockDtl.supplierName = wmsInReceiptDt.supplierName;
            wmsStockDtl.supplierNameEn = wmsInReceiptDt.supplierNameEn;
            wmsStockDtl.supplierNameAlias = wmsInReceiptDt.supplierNameAlias;
            //wmsStockDtl.batchNo = wmsInReceiptDt.batchNo;
            wmsStockDtl.materialSpec = wmsInReceiptDt.materialSpec;
            wmsStockDtl.stockDtlStatus = wmsStock.stockStatus;
            wmsStockDtl.qty = wmsInReceiptDt.receiptQty;
            wmsStockDtl.occupyQty = 0;
            wmsStockDtl.inspectionResult = 0;
            wmsStockDtl.lockFlag = 0;
            wmsStockDtl.lockReason = "";
            wmsStockDtl.skuCode = wmsInReceiptDt.materialCode;
            wmsStockDtl.CreateBy = LoginUserInfo?.ITCode;
            wmsStockDtl.CreateTime = DateTime.Now;
            //wmsStockDtl.inwhTime = DateTime.Now;
            wmsStockDtl.UpdateBy = LoginUserInfo?.ITCode;
            wmsStockDtl.UpdateTime = DateTime.Now;
            wmsStockDtl.extend1 = wmsInReceiptDt.extend1;
            wmsStockDtl.extend2 = wmsInReceiptDt.extend2;
            wmsStockDtl.extend3 = wmsInReceiptDt.ID.ToString();//收货明细ID
            wmsStockDtl.extend4 = wmsInReceiptDt.extend4;
            wmsStockDtl.extend5 = wmsInReceiptDt.extend5;
            wmsStockDtl.extend6 = wmsInReceiptDt.extend6;
            wmsStockDtl.extend7 = wmsInReceiptDt.extend7;
            wmsStockDtl.extend8 = wmsInReceiptDt.extend8;
            wmsStockDtl.extend9 = wmsInReceiptDt.extend9;
            wmsStockDtl.extend10 = wmsInReceiptDt.extend10;
            wmsStockDtl.extend11 = wmsInReceiptDt.extend11;
            wmsStockDtl.chipSize = wmsInReceiptDt.extend12;
            wmsStockDtl.chipThickness = wmsInReceiptDt.extend13;
            wmsStockDtl.chipModel = wmsInReceiptDt.extend14;
            wmsStockDtl.dafType = wmsInReceiptDt.extend15;
            //wmsStockDtl.urgentFlag = wmsInReceiptDt.urgentFlag;
            wmsStockDtl.unitCode = wmsInReceiptDt.unitCode;
            wmsStockDtl.CreateBy = invoker;
            wmsStockDtl.CreateTime = DateTime.Now;
            return wmsStockDtl;
        }

        /// <summary>
        /// 生成收货库存信息
        /// </summary>
        /// <returns></returns>
        private WmsStock BuildInReceiptStock(WmsInReceipt wmsInReceipt, string stockCode, string invoker)
        {
            var roadWay = DC.Set<BasWRoadway>().Where(x => x.areaNo == wmsInReceipt.areaNo && x.regionNo == wmsInReceipt.regionNo && x.usedFlag == 1)
                .Select(x => x.roadwayNo).FirstOrDefault();
            WmsStock wmsStock = new WmsStock();
            wmsStock.whouseNo = wmsInReceipt.whouseNo;
            wmsStock.areaNo = wmsInReceipt.areaNo;
            wmsStock.regionNo = wmsInReceipt.regionNo;
            wmsStock.roadwayNo = roadWay == null ? wmsInReceipt.areaNo : roadWay;
            wmsStock.binNo = wmsInReceipt.binNo;
            wmsStock.proprietorCode = wmsInReceipt.proprietorCode;
            wmsStock.stockCode = stockCode;
            wmsStock.palletBarcode = wmsInReceipt.receiptNo; //载体编号==收货单号
            //wmsStock.palletBarcode = stockCode;
            wmsStock.loadedType = 1;
            wmsStock.stockStatus = Int16.Parse(DictonaryHelper.StockStatus.InitCreate.GetCode());
            //wmsStock.locNo = wmsInReceipt.locNo;
            //wmsStock.locAllotGroup = wmsInReceipt.locAllotGroup;
            //wmsStock.weight = wmsInReceipt.weight;
            //wmsStock.height = wmsInReceipt.height;
            wmsStock.errFlag = 0;
            //wmsStock.errMsg = wmsInReceipt.errMsg;
            //wmsStock.invoiceNo = wmsInReceipt.invoiceNo;
            wmsStock.specialFlag = 0;
            //wmsStock.specialMsg = wmsInReceipt.specialMsg;
            //wmsStock.delFlag = wmsInReceipt.delFlag;
            wmsStock.CreateBy = invoker;// LoginUserInfo?.ITCode;
            wmsStock.CreateTime = DateTime.Now;
            wmsStock.UpdateBy = invoker;// LoginUserInfo?.ITCode;
            wmsStock.UpdateTime = DateTime.Now;
            return wmsStock;
        }
        private async Task<WmsStock> BuildInReceiptStockAsync(WmsInReceipt wmsInReceipt, string stockCode, string invoker)
        {
            var roadWay = await DC.Set<BasWRoadway>().Where(x => x.areaNo == wmsInReceipt.areaNo && x.regionNo == wmsInReceipt.regionNo && x.usedFlag == 1).FirstOrDefaultAsync();
            //.Select(x => x.roadwayNo);
            WmsStock wmsStock = new WmsStock();
            wmsStock.whouseNo = wmsInReceipt.whouseNo;
            wmsStock.areaNo = wmsInReceipt.areaNo;
            wmsStock.regionNo = wmsInReceipt.regionNo;
            wmsStock.roadwayNo = roadWay == null ? wmsInReceipt.areaNo : roadWay.roadwayNo;
            wmsStock.binNo = wmsInReceipt.binNo;
            wmsStock.proprietorCode = wmsInReceipt.proprietorCode;
            wmsStock.stockCode = stockCode;
            wmsStock.palletBarcode = wmsInReceipt.receiptNo; //载体编号==收货单号
            //wmsStock.palletBarcode = stockCode;
            wmsStock.loadedType = 1;
            wmsStock.stockStatus = Int16.Parse(DictonaryHelper.StockStatus.InitCreate.GetCode());
            //wmsStock.locNo = wmsInReceipt.locNo;
            //wmsStock.locAllotGroup = wmsInReceipt.locAllotGroup;
            //wmsStock.weight = wmsInReceipt.weight;
            //wmsStock.height = wmsInReceipt.height;
            wmsStock.errFlag = 0;
            //wmsStock.errMsg = wmsInReceipt.errMsg;
            //wmsStock.invoiceNo = wmsInReceipt.invoiceNo;
            wmsStock.specialFlag = 0;
            //wmsStock.specialMsg = wmsInReceipt.specialMsg;
            //wmsStock.delFlag = wmsInReceipt.delFlag;
            wmsStock.CreateBy = invoker;// LoginUserInfo?.ITCode;
            wmsStock.CreateTime = DateTime.Now;
            wmsStock.UpdateBy = invoker;// LoginUserInfo?.ITCode;
            wmsStock.UpdateTime = DateTime.Now;
            return wmsStock;
        }
        /// <summary>
        /// 创建库存调整记录
        /// </summary>
        /// <param name="uniicode"></param>
        /// <returns></returns>
        private WmsStockAdjust CreateInReceiptStockAdjust(WmsStockUniicode uniicode, string invoker)
        {
            WmsStockAdjust wmsStockAdjust = new WmsStockAdjust();
            wmsStockAdjust.whouseNo = uniicode.whouseNo;
            wmsStockAdjust.proprietorCode = uniicode.proprietorCode;
            wmsStockAdjust.stockCode = uniicode.stockCode;
            wmsStockAdjust.palletBarcode = uniicode.palletBarcode;
            wmsStockAdjust.packageBarcode = uniicode.uniicode;
            wmsStockAdjust.adjustOperate = "收货";
            wmsStockAdjust.adjustType = WmsStockAdjustType.New.GetCode();
            wmsStockAdjust.adjustDesc = "收货";
            wmsStockAdjust.CreateBy = invoker;// LoginUserInfo?.ITCode;
            wmsStockAdjust.CreateTime = DateTime.Now;
            return wmsStockAdjust;
        }
        /// <summary>
        /// 创建收货撤销库存记录
        /// </summary>
        /// <param name="uniicode"></param>
        /// <param name="invoker"></param>
        /// <returns></returns>
        private WmsStockAdjust CreateInReceiptRStockAdjust(WmsStockUniicode uniicode, string invoker)
        {
            WmsStockAdjust wmsStockAdjust = new WmsStockAdjust();
            wmsStockAdjust.whouseNo = uniicode.whouseNo;
            wmsStockAdjust.proprietorCode = uniicode.proprietorCode;
            wmsStockAdjust.stockCode = uniicode.stockCode;
            wmsStockAdjust.palletBarcode = uniicode.palletBarcode;
            wmsStockAdjust.packageBarcode = uniicode.uniicode;
            wmsStockAdjust.adjustOperate = "收货撤销";
            wmsStockAdjust.adjustType = WmsStockAdjustType.New.GetCode();
            wmsStockAdjust.adjustDesc = "收货撤销";
            wmsStockAdjust.CreateBy = invoker;// LoginUserInfo?.ITCode;
            wmsStockAdjust.CreateTime = DateTime.Now;
            return wmsStockAdjust;
        }
        /// <summary>
        /// 根据入库单明细
        /// </summary>
        /// <param name="wmsInOrder"></param>
        /// <returns></returns>
        private WmsInReceiptDtl BuildWmsInReceiptDtlByWmsInOrder(WmsInReceipt wmsInOrder, WmsInOrderDtl wmsInOrderDetail, string invoker)
        {
            WmsInReceiptDtl wmsInReceiptDt = new WmsInReceiptDtl();
            wmsInReceiptDt.whouseNo = wmsInOrderDetail.whouseNo;
            wmsInReceiptDt.areaNo = wmsInOrderDetail.areaNo;
            wmsInReceiptDt.erpWhouseNo = wmsInOrderDetail.erpWhouseNo;
            wmsInReceiptDt.proprietorCode = wmsInOrderDetail.proprietorCode;
            wmsInReceiptDt.receiptNo = wmsInOrder.receiptNo;
            wmsInReceiptDt.inNo = wmsInOrderDetail.inNo;
            wmsInReceiptDt.inDtlId = wmsInOrderDetail.ID;
            wmsInReceiptDt.externalInNo = wmsInOrderDetail.externalInNo;
            wmsInReceiptDt.externalInDtlId = wmsInOrderDetail.externalInDtlId;
            wmsInReceiptDt.orderNo = wmsInOrderDetail.orderNo;
            wmsInReceiptDt.orderDtlId = wmsInOrderDetail.orderDtlId;
            wmsInReceiptDt.materialCode = wmsInOrderDetail.materialCode;
            wmsInReceiptDt.materialName = wmsInOrderDetail.materialName;
            wmsInReceiptDt.supplierCode = wmsInOrderDetail.supplierCode;
            wmsInReceiptDt.supplierName = wmsInOrderDetail.supplierName;
            wmsInReceiptDt.supplierNameEn = wmsInOrderDetail.supplierNameEn;
            wmsInReceiptDt.supplierNameAlias = wmsInOrderDetail.supplierNameAlias;
            wmsInReceiptDt.batchNo = wmsInOrderDetail.batchNo;
            wmsInReceiptDt.materialSpec = wmsInOrderDetail.materialSpec;
            //wmsInReceiptDt.receiptQty = wmsInOrderDetail.docNum;
            //wmsInReceiptDt.receiptQty = wmsInOrderDetail.receiptQty;
            wmsInReceiptDt.qualifiedQty = wmsInOrderDetail.qualifiedQty;
            wmsInReceiptDt.qualifiedSpecialQty = wmsInOrderDetail.qualifiedSpecialQty;
            wmsInReceiptDt.unqualifiedQty = wmsInOrderDetail.unqualifiedQty;
            wmsInReceiptDt.returnQty = wmsInOrderDetail.returnQty;
            wmsInReceiptDt.recordQty = wmsInOrderDetail.recordQty;
            wmsInReceiptDt.putawayQty = wmsInOrderDetail.putawayQty;
            wmsInReceiptDt.postBackQty = wmsInOrderDetail.postBackQty;
            wmsInReceiptDt.receiptDtlStatus = wmsInOrderDetail.inDtlStatus;
            wmsInReceiptDt.productSn = wmsInOrderDetail.productSn;
            wmsInReceiptDt.departmentName = wmsInOrderDetail.departmentName;
            wmsInReceiptDt.projectNo = wmsInOrderDetail.projectNo;
            wmsInReceiptDt.inspector = wmsInOrderDetail.inspector;
            wmsInReceiptDt.minPkgQty = wmsInOrderDetail.minPkgQty;
            wmsInReceiptDt.urgentFlag = wmsInOrderDetail.urgentFlag;
            wmsInReceiptDt.unitCode = wmsInOrderDetail.unitCode;
            //wmsInReceiptDt.replenishFlag = wmsInOrderDetail.replenishFlag;
            //wmsInReceiptDt.delFlag = Int16.Parse(DelFlag.NDelete.GetCode());
            //wmsInReceiptDt.createBy = wmsInOrderDetail.createBy;
            wmsInReceiptDt.CreateTime = DateTime.Now;
            //wmsInReceiptDt.updateBy = wmsInOrderDetail.updateBy;
            //wmsInReceiptDt.updateTime = wmsInOrderDetail.updateTime;
            wmsInReceiptDt.extend1 = wmsInOrderDetail.extend1;
            wmsInReceiptDt.extend2 = wmsInOrderDetail.extend2;
            wmsInReceiptDt.extend3 = wmsInOrderDetail.extend3;
            wmsInReceiptDt.extend4 = wmsInOrderDetail.extend4;
            wmsInReceiptDt.extend5 = wmsInOrderDetail.extend5;
            wmsInReceiptDt.extend6 = wmsInOrderDetail.extend6;
            wmsInReceiptDt.extend7 = wmsInOrderDetail.extend7;
            wmsInReceiptDt.extend8 = wmsInOrderDetail.extend8;
            wmsInReceiptDt.extend9 = wmsInOrderDetail.extend9;
            wmsInReceiptDt.extend10 = wmsInOrderDetail.extend10;
            wmsInReceiptDt.extend11 = wmsInOrderDetail.extend11;
            wmsInReceiptDt.extend12 = wmsInOrderDetail.extend12;
            wmsInReceiptDt.extend13 = wmsInOrderDetail.extend13;
            wmsInReceiptDt.extend14 = wmsInOrderDetail.extend14;
            wmsInReceiptDt.extend15 = wmsInOrderDetail.extend15;

            wmsInReceiptDt.CreateBy = invoker;// LoginUserInfo?.ITCode;
            return wmsInReceiptDt;
        }

        /// <summary>
        /// 根据入库单创建收货单
        /// </summary>
        /// <param name="wmsInOrder"></param>
        /// <param name="regionNo"></param>
        /// <param name="binNo"></param>
        /// <param name="inReceiptNo"></param>
        /// <param name="returnSRMFlag"></param>
        /// <returns></returns>
        private WmsInReceipt BuildWmsInReceiptByWmsInOrder(WmsInOrder wmsInOrder,
            string regionNo,
            string binNo,
            string inReceiptNo,
            bool returnSRMFlag,
            string invoker)
        {
            var returnFlag = "";
            if (returnSRMFlag)
            {
                returnFlag = DictonaryHelper.InReceiptReturnFlag.InitCreate.GetCode();
            }
            else
            {
                returnFlag = DictonaryHelper.InReceiptReturnFlag.NoReturn.GetCode();
            }

            WmsInReceipt wmsInReceipt = new WmsInReceipt();
            wmsInReceipt.whouseNo = wmsInOrder.whouseNo;
            wmsInReceipt.areaNo = wmsInOrder.areaNo;
            wmsInReceipt.regionNo = regionNo;
            wmsInReceipt.binNo = binNo;
            wmsInReceipt.proprietorCode = wmsInOrder.proprietorCode;
            wmsInReceipt.receiptNo = inReceiptNo;
            wmsInReceipt.inNo = wmsInOrder.inNo;
            wmsInReceipt.docTypeCode = wmsInOrder.docTypeCode;
            wmsInReceipt.sourceBy = wmsInOrder.sourceBy;
            wmsInReceipt.cvType = wmsInOrder.cvType;
            wmsInReceipt.cvCode = wmsInOrder.cvCode;
            wmsInReceipt.cvName = wmsInOrder.cvName;
            wmsInReceipt.cvNameEn = wmsInOrder.cvNameEn;
            wmsInReceipt.cvNameAlias = wmsInOrder.cvNameAlias;
            wmsInReceipt.receiptStatus = Convert.ToInt32(ReceiptOrDtlStatus.Init.GetCode());
            //wmsInReceipt.returnFlag = returnFlag;
            wmsInReceipt.orderDesc = wmsInOrder.orderDesc;
            wmsInReceipt.operationReason = wmsInOrder.operationReason;
            wmsInReceipt.externalInNo = wmsInOrder.externalInNo;
            wmsInReceipt.externalInId = wmsInOrder.externalInId;
            wmsInReceipt.ticketNo = wmsInOrder.ticketNo;
            wmsInReceipt.receiptTime = DateTime.Now;
            wmsInReceipt.CreateTime = DateTime.Now;
            wmsInReceipt.extend1 = wmsInOrder.extend1;
            wmsInReceipt.extend2 = wmsInOrder.extend2;
            wmsInReceipt.extend3 = wmsInOrder.extend3;
            wmsInReceipt.extend4 = wmsInOrder.extend4;
            wmsInReceipt.extend5 = wmsInOrder.extend5;
            wmsInReceipt.extend6 = wmsInOrder.extend6;
            wmsInReceipt.extend7 = wmsInOrder.extend7;
            wmsInReceipt.extend8 = wmsInOrder.extend8;
            wmsInReceipt.extend9 = wmsInOrder.extend9;
            wmsInReceipt.extend10 = wmsInOrder.extend10;
            wmsInReceipt.extend11 = wmsInOrder.extend11;
            wmsInReceipt.extend12 = wmsInOrder.extend12;
            wmsInReceipt.extend13 = wmsInOrder.extend13;
            wmsInReceipt.extend14 = wmsInOrder.extend14;
            wmsInReceipt.extend15 = wmsInOrder.extend15;

            // 添加操作人员
            wmsInReceipt.CreateBy = invoker;// LoginUserInfo?.ITCode;
            wmsInReceipt.receipter = invoker;// LoginUserInfo?.ITCode;
            return wmsInReceipt;
        }

        /// <summary>
        /// 获取收货单信息
        /// </summary>
        /// <param name="inReceiptNo"></param>
        /// <returns></returns>
        public WmsInReceiptDtl GetWmsInReceiptDt(Int64 inReceiptDtId)
        {
            return DC.Set<WmsInReceiptDtl>().Where(x => x.ID == inReceiptDtId).FirstOrDefault();
        }
        public async Task<WmsInReceiptDtl> GetWmsInReceiptDtAsync(Int64 inReceiptDtId)
        {
            return await DC.Set<WmsInReceiptDtl>().Where(x => x.ID == inReceiptDtId).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 根据到货单号和到货单行号获取收货单明细
        /// </summary>
        /// <param name="dhno"></param>
        /// <param name="dhline"></param>
        /// <returns></returns>
        public WmsInReceiptDtl GetWmsInReceiptDtByExternal(string dhno, string dhline)
        {
            return DC.Set<WmsInReceiptDtl>().Where(x => x.receiptNo == dhno && x.extend1 == dhline).FirstOrDefault();
        }
        public async Task<WmsInReceiptDtl> GetWmsInReceiptDtByExternalAsync(string dhno, string dhline)
        {
            return await DC.Set<WmsInReceiptDtl>().Where(x => x.receiptNo == dhno && x.extend1 == dhline).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 获取收货单信息
        /// </summary>
        /// <param name="inReceiptNo"></param>
        /// <returns></returns>
        public WmsInReceipt GetWmsInReceipt(string inReceiptNo)
        {
            return DC.Set<WmsInReceipt>().Where(x => x.receiptNo == inReceiptNo).FirstOrDefault();
        }
        public async Task<WmsInReceipt> GetWmsInReceiptAsync(string inReceiptNo)
        {
            return await DC.Set<WmsInReceipt>().Where(x => x.receiptNo == inReceiptNo).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 判断收货单明细的状态是否存在小于指定状态的数据
        /// </summary>
        /// <param name="receiptNo"></param>
        /// <param name="inReceiptDtlStaus"></param>
        /// <returns></returns>
        public bool IsExistInReceiptDtlStatusMin(string receiptNo, string inReceiptDtlStaus)
        {
            return DC.Set<WmsInReceiptDtl>()
                        .Where(x => x.receiptNo == receiptNo && x.receiptDtlStatus < Int16.Parse(inReceiptDtlStaus)).Any();
        }
        public async Task<bool> IsExistInReceiptDtlStatusMinAsync(string receiptNo, string inReceiptDtlStaus)
        {
            return await DC.Set<WmsInReceiptDtl>()
                        .Where(x => x.receiptNo == receiptNo && x.receiptDtlStatus < Int16.Parse(inReceiptDtlStaus)).AnyAsync();
        }
        /// <summary>
        /// 检查收货明细的状态是否都是传入的状态，如果都是status状态，将收货主表也更新status;否则将主表状态改为status2
        /// </summary>
        /// <param name="receiptNo"></param>
        /// <param name="status"></param>
        /// <param name="status2"></param>
        public bool UpdateInReceiptStatus(string receiptNo, int? status, int? status2, string invoker)
        {
            var dtls = DC.Set<WmsInReceiptDtl>().Where(x => x.receiptNo == receiptNo).ToList();
            var statusDtls = dtls.Where(x => x.receiptDtlStatus == status).ToList();
            var binNo = DC.Set<WmsInReceiptIqcResult>().Where(x => x.receiptNo == receiptNo).Select(x => x.binNo).FirstOrDefault();
            if (dtls.Count == statusDtls.Count)
            {
                WmsInReceipt wmsInReceipt = DC.Set<WmsInReceipt>().Where(x => x.receiptNo == receiptNo).FirstOrDefault();
                wmsInReceipt.binNo = binNo == null ? wmsInReceipt.binNo : binNo;
                wmsInReceipt.receiptStatus = Convert.ToInt32(status);
                wmsInReceipt.UpdateBy = invoker;
                wmsInReceipt.UpdateTime = DateTime.Now;
                DC.UpdateEntity(wmsInReceipt);
                DC.SaveChanges();
                return true;
            }
            else
            {
                //if (status2.IsNullOrWhiteSpace() == false)
                if (status2 != null)
                {
                    WmsInReceipt wmsInReceipt = DC.Set<WmsInReceipt>().Where(x => x.receiptNo == receiptNo).FirstOrDefault();
                    wmsInReceipt.binNo = binNo == null ? wmsInReceipt.binNo : binNo;
                    wmsInReceipt.receiptStatus = Convert.ToInt32(status2);
                    wmsInReceipt.UpdateBy = invoker;
                    wmsInReceipt.UpdateTime = DateTime.Now;
                    DC.UpdateEntity(wmsInReceipt);
                    DC.SaveChanges();
                }
            }
            return false;
        }

        public async Task<bool> UpdateInReceiptStatusAsync(string receiptNo, int? status, int? status2, string invoker)
        {
            var dtls = await DC.Set<WmsInReceiptDtl>().Where(x => x.receiptNo == receiptNo).ToListAsync();
            var statusDtls = dtls.Where(x => x.receiptDtlStatus == status).ToList();
            var binNo = await DC.Set<WmsInReceiptIqcResult>().Where(x => x.receiptNo == receiptNo).Select(x => x.binNo).FirstOrDefaultAsync();
            if (dtls.Count == statusDtls.Count)
            {
                WmsInReceipt wmsInReceipt = await DC.Set<WmsInReceipt>().Where(x => x.receiptNo == receiptNo).FirstOrDefaultAsync();
                wmsInReceipt.binNo = binNo == null ? wmsInReceipt.binNo : binNo;
                wmsInReceipt.receiptStatus = Convert.ToInt32(status);
                wmsInReceipt.UpdateBy = invoker;
                wmsInReceipt.UpdateTime = DateTime.Now;
                //DC.UpdateEntity(wmsInReceipt);
                //DC.SaveChanges();
                await ((DbContext)DC).Set<WmsInReceipt>().SingleUpdateAsync(wmsInReceipt);
                await ((DbContext)DC).BulkSaveChangesAsync();
                return true;
            }
            else
            {
                //if (status2.IsNullOrWhiteSpace() == false)
                if (status2 != null)
                {
                    WmsInReceipt wmsInReceipt = await DC.Set<WmsInReceipt>().Where(x => x.receiptNo == receiptNo).FirstOrDefaultAsync();
                    wmsInReceipt.binNo = binNo == null ? wmsInReceipt.binNo : binNo;
                    wmsInReceipt.receiptStatus = Convert.ToInt32(status2);
                    wmsInReceipt.UpdateBy = invoker;
                    wmsInReceipt.UpdateTime = DateTime.Now;
                    //DC.UpdateEntity(wmsInReceipt);
                    //DC.SaveChanges();
                    await ((DbContext)DC).Set<WmsInReceipt>().SingleUpdateAsync(wmsInReceipt);
                    await ((DbContext)DC).BulkSaveChangesAsync();
                }
            }
            return false;
        }

    }
    
}
