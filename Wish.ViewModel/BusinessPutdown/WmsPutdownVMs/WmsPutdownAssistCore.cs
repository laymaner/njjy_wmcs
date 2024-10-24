using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using Wish.Areas.BasWhouse.Model;
using Wish.ViewModel.BasWhouse.BasWRegionVMs;
using WISH.Helper.Common.Dictionary.DictionaryHelper;

namespace Wish.ViewModel.BusinessPutdown.WmsPutdownVMs
{
    public partial class WmsPutdownVM
    {

        /// <summary>
        /// 生成下架单主表信息
        /// </summary>
        /// <param name="putdownNo"></param>
        /// <param name="wmsStock"></param>
        /// <param name="wmsOutInvoice"></param>
        /// <returns></returns>
        public WmsPutdown BuildWmsPutDownForMerge(string putdownNo, WmsStock wmsStock, string pickupMethod, string docTypeCode, string invoiceNo, string invoker)
        {

            WmsPutdown wmsPutdown = new WmsPutdown();
            wmsPutdown.whouseNo = wmsStock.whouseNo;
            wmsPutdown.areaNo = wmsStock.areaNo;
            wmsPutdown.regionNo = wmsStock.regionNo;
            wmsPutdown.proprietorCode = wmsStock.proprietorCode;
            wmsPutdown.putdownNo = putdownNo;
            wmsPutdown.docTypeCode = docTypeCode;
            wmsPutdown.orderNo = invoiceNo;
            wmsPutdown.stockCode = wmsStock.stockCode;
            wmsPutdown.palletBarcode = wmsStock.palletBarcode;
            wmsPutdown.loadedType = wmsStock.loadedType;
            wmsPutdown.binNo = wmsStock.binNo;
            wmsPutdown.pickupMethod = pickupMethod;
            //wmsPutdown.targetLocNo = obj.targetLocNo;
            wmsPutdown.putdownStatus = 0;
            wmsPutdown.CreateBy = invoker;
            wmsPutdown.CreateTime = DateTime.Now;


            return wmsPutdown;
        }

        /// <summary>
        /// 生成下架明细数据
        /// </summary>
        /// <param name="wmsStockDtl"></param>
        /// <param name="putdownNo"></param>
        /// <returns></returns>
        public WmsPutdownDtl BuildWmsPutDownDtl(WmsStockDtl wmsStockDtl, string putdownNo, string invoker)
        {
            WmsPutdownDtl wmsPutdownDtl = new WmsPutdownDtl();
            wmsPutdownDtl.whouseNo = wmsStockDtl.whouseNo;
            wmsPutdownDtl.erpWhouseNo = wmsStockDtl.erpWhouseNo;
            wmsPutdownDtl.areaNo = wmsStockDtl.areaNo;
            wmsPutdownDtl.proprietorCode = wmsStockDtl.proprietorCode;
            wmsPutdownDtl.putdownNo = putdownNo;
            wmsPutdownDtl.stockCode = wmsStockDtl.stockCode;
            wmsPutdownDtl.stockDtlId = wmsStockDtl.ID;
            wmsPutdownDtl.palletBarcode = wmsStockDtl.palletBarcode;
            //wmsPutdownDtl.positionNo = wmsStockDtl.positionNo;
            wmsPutdownDtl.projectNo = wmsStockDtl.projectNo;
            wmsPutdownDtl.materialCode = wmsStockDtl.materialCode;
            wmsPutdownDtl.materialName = wmsStockDtl.materialName;
            wmsPutdownDtl.supplierCode = wmsStockDtl.supplierCode;
            wmsPutdownDtl.supplierName = wmsStockDtl.supplierName;
            wmsPutdownDtl.supplierNameEn = wmsStockDtl.supplierNameEn;
            wmsPutdownDtl.supplierNameAlias = wmsStockDtl.supplierNameAlias;
            //wmsPutdownDtl.batchNo = wmsStockDtl.batchNo;
            //TODO：下架单批次
            wmsPutdownDtl.materialSpec = wmsStockDtl.materialSpec;
            wmsPutdownDtl.stockQty = wmsStockDtl.qty;
            wmsPutdownDtl.occupyQty = wmsStockDtl.occupyQty;
            wmsPutdownDtl.inspectionResult = wmsStockDtl.inspectionResult;
            wmsPutdownDtl.skuCode = wmsStockDtl.skuCode;
            wmsPutdownDtl.CreateBy = invoker;
            wmsPutdownDtl.CreateTime = DateTime.Now;
            //wmsPutdownDtl.updateBy = wmsStockDtl.updateBy;
            //wmsPutdownDtl.updateTime = wmsStockDtl.updateTime;
            wmsPutdownDtl.extend1 = wmsStockDtl.extend1;
            wmsPutdownDtl.extend2 = wmsStockDtl.extend2;
            wmsPutdownDtl.extend3 = wmsStockDtl.extend3;
            wmsPutdownDtl.extend4 = wmsStockDtl.extend4;
            wmsPutdownDtl.extend5 = wmsStockDtl.extend5;
            wmsPutdownDtl.extend6 = wmsStockDtl.extend6;
            wmsPutdownDtl.extend7 = wmsStockDtl.extend7;
            wmsPutdownDtl.extend8 = wmsStockDtl.extend8;
            wmsPutdownDtl.extend9 = wmsStockDtl.extend9;
            wmsPutdownDtl.extend10 = wmsStockDtl.extend10;
            wmsPutdownDtl.extend11 = wmsStockDtl.extend11;
            wmsPutdownDtl.extend12 = wmsStockDtl.chipSize;
            wmsPutdownDtl.extend13 = wmsStockDtl.chipThickness;
            wmsPutdownDtl.extend14 = wmsStockDtl.chipModel;
            wmsPutdownDtl.extend15 = wmsStockDtl.dafType;
            return wmsPutdownDtl;
        }


        /// <summary>
        /// 生成下架单主表信息
        /// </summary>
        /// <param name="putdownNo"></param>
        /// <param name="wmsStock"></param>
        /// <param name="wmsOutInvoice"></param>
        /// <returns></returns>
        private WmsPutdown BuildWmsPutDown(string putdownNo, WmsStock wmsStock, WmsOutInvoice wmsOutInvoice, string invoker)
        {
            BasWRegionVM basWRegionApiVM = Wtm.CreateVM<BasWRegionVM>();
            BasWRegion region = basWRegionApiVM.GetRegion(wmsStock.regionNo);
            WmsPutdown wmsPutdown = new WmsPutdown();
            wmsPutdown.whouseNo = wmsStock.whouseNo;
            wmsPutdown.areaNo = wmsStock.areaNo;
            wmsPutdown.regionNo = wmsStock.regionNo;
            wmsPutdown.proprietorCode = wmsStock.proprietorCode;
            wmsPutdown.putdownNo = putdownNo;
            wmsPutdown.docTypeCode = wmsOutInvoice.docTypeCode;
            wmsPutdown.orderNo = wmsOutInvoice.invoiceNo;
            wmsPutdown.stockCode = wmsStock.stockCode;
            wmsPutdown.palletBarcode = wmsStock.palletBarcode;
            wmsPutdown.loadedType = wmsStock.loadedType;
            wmsPutdown.binNo = wmsStock.binNo;
            wmsPutdown.pickupMethod = region.pickupMethod;
            //wmsPutdown.targetLocNo = obj.targetLocNo;
            wmsPutdown.putdownStatus = 0;
            wmsPutdown.CreateBy = invoker;
            wmsPutdown.CreateTime = DateTime.Now;


            return wmsPutdown;
        }

        /// <summary>
        /// 库存调整记录
        /// </summary>
        /// <param name="uniicode"></param>
        /// <returns></returns>
        private WmsStockAdjust BuildOutReceiptStockAdjust(WmsStockUniicode uniicode, string invoker)
        {
            WmsStockAdjust wmsStockAdjust = new WmsStockAdjust();
            wmsStockAdjust.whouseNo = uniicode.whouseNo;
            wmsStockAdjust.proprietorCode = uniicode.proprietorCode;
            wmsStockAdjust.stockCode = uniicode.stockCode;
            wmsStockAdjust.palletBarcode = uniicode.palletBarcode;
            wmsStockAdjust.packageBarcode = uniicode.uniicode;
            wmsStockAdjust.adjustOperate = "出库分配";
            wmsStockAdjust.adjustType = WmsStockAdjustType.Modify.GetCode();
            wmsStockAdjust.adjustDesc = "出库分配";
            wmsStockAdjust.CreateBy = invoker;
            wmsStockAdjust.CreateTime = DateTime.Now;
            return wmsStockAdjust;
        }

        public int? GetIntForString(string str)
        {
            int result = 0;
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            else
            {
                try
                {
                    int.TryParse(str, out result);
                }
                catch
                {
                    return null;
                }
            }
            return result;
        }

        /// <summary>
        /// 创建出库记录
        /// </summary>
        /// <param name="wmsOutInvoice"></param>
        /// <param name="wmsOutInvoiceDtl"></param>
        /// <param name="wmsStock"></param>
        /// <param name="wmsStockDtl"></param>
        /// <param name="pickTaskNo"></param>
        /// <param name="allotType"></param>
        /// <param name="isBatch"></param>
        /// <param name="pickType"></param>
        /// <param name="palletPickType"></param>
        /// <returns></returns>
        public WmsOutInvoiceRecord BuildOutInvoiceRecord(
            WmsOutInvoice wmsOutInvoice,
            WmsOutInvoiceDtl wmsOutInvoiceDtl,
            WmsStock wmsStock,
            WmsStockDtl wmsStockDtl,
            string pickTaskNo,
            string allotType,
            bool isBatch,
            string pickType,
            string palletPickType,
            string invoker,
            decimal allotQty,
            string locNo,
            bool isPk = true
            )
        {
            WmsOutInvoiceRecord wmsOutInvoiceRecord = new WmsOutInvoiceRecord();
            wmsOutInvoiceRecord.whouseNo = wmsOutInvoiceDtl.whouseNo;
            wmsOutInvoiceRecord.erpWhouseNo = wmsStockDtl.erpWhouseNo;
            wmsOutInvoiceRecord.areaNo = wmsStock.areaNo;
            wmsOutInvoiceRecord.regionNo = wmsStock.regionNo;
            wmsOutInvoiceRecord.loadedTtype = wmsStock.loadedType;
            wmsOutInvoiceRecord.binNo = wmsStock.binNo;
            wmsOutInvoiceRecord.proprietorCode = wmsStock.proprietorCode;
            wmsOutInvoiceRecord.pickTaskNo = pickTaskNo;
            wmsOutInvoiceRecord.invoiceNo = wmsOutInvoiceDtl.invoiceNo;
            wmsOutInvoiceRecord.invoiceDtlId = wmsOutInvoiceDtl.ID;
            wmsOutInvoiceRecord.sourceBy = wmsOutInvoice.sourceBy;
            wmsOutInvoiceRecord.waveNo = wmsOutInvoiceDtl.waveNo;
            wmsOutInvoiceRecord.stockCode = wmsStock.stockCode;
            wmsOutInvoiceRecord.palletBarcode = wmsStock.palletBarcode;
            wmsOutInvoiceRecord.preStockDtlId = wmsStockDtl.ID;
            wmsOutInvoiceRecord.docTypeCode = wmsOutInvoice.docTypeCode;
            wmsOutInvoiceRecord.pickType = GetIntForString(pickType);
            wmsOutInvoiceRecord.palletPickType = GetIntForString(palletPickType);
            wmsOutInvoiceRecord.allotType = GetIntForString(allotType);
            //wmsOutInvoiceRecord.outBarcode = obj.outBarcode;                                  
            wmsOutInvoiceRecord.isBatch = isBatch ? 1 : 0;
            //wmsOutInvoiceRecord.pickLocNo = obj.pickLocNo;
            wmsOutInvoiceRecord.deliveryLocNo = locNo;
            wmsOutInvoiceRecord.reversePickFlag = 0;
            wmsOutInvoiceRecord.skuCode = wmsStockDtl.skuCode;
            wmsOutInvoiceRecord.materialCode = wmsStockDtl.materialCode;
            wmsOutInvoiceRecord.materialName = wmsStockDtl.materialName;
            wmsOutInvoiceRecord.materialSpec = wmsStockDtl.materialSpec;
            wmsOutInvoiceRecord.batchNo = wmsOutInvoiceDtl.batchNo;
            wmsOutInvoiceRecord.supplierCode = wmsStockDtl.supplierCode;
            wmsOutInvoiceRecord.supplierName = wmsStockDtl.supplierName;
            wmsOutInvoiceRecord.supplierNameEn = wmsStockDtl.supplierNameEn;
            wmsOutInvoiceRecord.supplierNameAlias = wmsStockDtl.supplierNameAlias;
            wmsOutInvoiceRecord.inspectionResult = wmsStockDtl.inspectionResult;
            //wmsOutInvoiceRecord.erpBinNo = wmsStockDtl.erpWhouseNo;
            //wmsOutInvoiceRecord.urgentFlag = wmsStockDtl.urgentFlag == "Y" ? "Y" : "N";
            //TODO:出库记录急料标记
            wmsOutInvoiceRecord.allotQty = allotQty;
            wmsOutInvoiceRecord.pickQty = 0;
            //wmsOutInvoiceRecord.issuedResult = obj.issuedResult;
            wmsOutInvoiceRecord.outRecordStatus = isPk ? 40 : 0;
            wmsOutInvoiceRecord.allocatResult = "";
            wmsOutInvoiceRecord.ticketPlanBeginTime = wmsOutInvoiceDtl.ticketPlanBeginTime;
            wmsOutInvoiceRecord.supplyType = wmsOutInvoiceDtl.supplyType;
            wmsOutInvoiceRecord.belongDepartment = wmsOutInvoiceDtl.belongDepartment;
            wmsOutInvoiceRecord.ticketType = wmsOutInvoice.ticketType;
            wmsOutInvoiceRecord.assemblyIdx = wmsOutInvoiceDtl.assemblyIdx;
            wmsOutInvoiceRecord.productLocation = wmsOutInvoice.productLocation;
            wmsOutInvoiceRecord.orderNo = wmsOutInvoiceDtl.orderNo;
            wmsOutInvoiceRecord.orderDtlId = wmsOutInvoiceDtl.orderDtlId;
            wmsOutInvoiceRecord.projectNo = wmsOutInvoiceDtl.projectNo;
            // wmsOutInvoiceRecord.productDeptCode = wmsOutInvoiceDtl.productDeptNo;
            wmsOutInvoiceRecord.productDeptName = wmsOutInvoiceDtl.productDeptName;
            wmsOutInvoiceRecord.fpNo = wmsOutInvoice.fpNo;
            wmsOutInvoiceRecord.fpName = wmsOutInvoice.fpName;
            wmsOutInvoiceRecord.fpQty = wmsOutInvoice.fpQty;
            wmsOutInvoiceRecord.ticketNo = wmsOutInvoiceDtl.ticketNo;
            wmsOutInvoiceRecord.externalOutNo = wmsOutInvoiceDtl.externalOutNo;
            wmsOutInvoiceRecord.externalOutDtlId = wmsOutInvoiceDtl.externalOutDtlId;
            wmsOutInvoiceRecord.inOutTypeNo = wmsOutInvoice.inOutTypeNo;
            wmsOutInvoiceRecord.inOutName = wmsOutInvoice.inOutName;
            wmsOutInvoiceRecord.CreateBy = invoker;
            wmsOutInvoiceRecord.CreateTime = DateTime.Now;
            wmsOutInvoiceRecord.extend1 = wmsOutInvoice.extend1;
            wmsOutInvoiceRecord.extend2 = wmsOutInvoice.extend2;
            wmsOutInvoiceRecord.extend3 = wmsOutInvoice.extend3;
            wmsOutInvoiceRecord.extend4 = wmsOutInvoice.extend4;
            wmsOutInvoiceRecord.extend5 = wmsOutInvoice.extend5;
            wmsOutInvoiceRecord.extend6 = wmsOutInvoice.extend6;
            wmsOutInvoiceRecord.extend7 = wmsOutInvoice.extend7;
            wmsOutInvoiceRecord.extend8 = wmsOutInvoice.extend8;
            wmsOutInvoiceRecord.extend9 = wmsOutInvoice.extend9;
            wmsOutInvoiceRecord.extend10 = wmsOutInvoice.extend10;
            wmsOutInvoiceRecord.extend11 = wmsOutInvoice.extend11;
            wmsOutInvoiceRecord.extend12 = wmsOutInvoice.extend12;
            wmsOutInvoiceRecord.extend13 = wmsOutInvoice.extend13;
            wmsOutInvoiceRecord.extend14 = wmsOutInvoice.extend14;
            wmsOutInvoiceRecord.extend15 = wmsOutInvoice.extend15;
            wmsOutInvoiceRecord.UpdateBy = invoker;
            wmsOutInvoiceRecord.UpdateTime = DateTime.Now;
            return wmsOutInvoiceRecord;
        }


    }
}
