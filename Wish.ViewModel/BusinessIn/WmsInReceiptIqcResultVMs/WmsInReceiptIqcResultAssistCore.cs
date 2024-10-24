using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;
using static WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper;
using Wish.ViewModel.Common.Dtos;
using WISH.Helper.Common.Dictionary.DictionaryHelper;

namespace Wish.ViewModel.BusinessIn.WmsInReceiptIqcResultVMs
{

    public partial class WmsInReceiptIqcResultVM
    {
        /// <summary>
        /// 生成质检结果信息
        /// </summary>
        /// <param name="InReceiptIqcRecord"></param>
        /// <param name="doInReceiptIqcResultParam"></param>
        /// <param name="wmsInOrderDetail"></param>
        /// <param name="wmsInOrder"></param>
        /// <returns></returns>
        private WmsInReceiptIqcResult BuildWmsInReceiptIqcResult(
            WmsInReceiptIqcRecord InReceiptIqcRecord,
            DoInReceiptIqcResultDto doInReceiptIqcResultParam,
            WmsInOrderDtl wmsInOrderDetail,
            WmsInOrder wmsInOrder,
            string invoker)
        {
            WmsInReceiptIqcResult wmsInReceiptIqcResult = new WmsInReceiptIqcResult();
            wmsInReceiptIqcResult.whouseNo = InReceiptIqcRecord.whouseNo;
            wmsInReceiptIqcResult.areaNo = InReceiptIqcRecord.areaNo;
            wmsInReceiptIqcResult.erpWhouseNo = InReceiptIqcRecord.erpWhouseNo;
            wmsInReceiptIqcResult.regionNo = InReceiptIqcRecord.regionNo;
            wmsInReceiptIqcResult.binNo = InReceiptIqcRecord.binNo;
            wmsInReceiptIqcResult.proprietorCode = InReceiptIqcRecord.proprietorCode;
            wmsInReceiptIqcResult.receiptNo = InReceiptIqcRecord.receiptNo;
            wmsInReceiptIqcResult.receiptDtlId = InReceiptIqcRecord.receiptDtlId;
            wmsInReceiptIqcResult.inNo = InReceiptIqcRecord.inNo;
            wmsInReceiptIqcResult.inDtlId = InReceiptIqcRecord.inDtlId;
            wmsInReceiptIqcResult.externalInNo = InReceiptIqcRecord.externalInNo;
            wmsInReceiptIqcResult.externalInDtlId = InReceiptIqcRecord.externalInDtlId;
            wmsInReceiptIqcResult.orderNo = InReceiptIqcRecord.inNo;
            //wmsInReceiptIqcResult.orderDtlId = InReceiptIqcRecord.inDtlId;
            wmsInReceiptIqcResult.docTypeCode = InReceiptIqcRecord.docTypeCode;
            wmsInReceiptIqcResult.sourceBy = InReceiptIqcRecord.sourceBy;
            wmsInReceiptIqcResult.iqcType = InReceiptIqcRecord.iqcType;
            wmsInReceiptIqcResult.materialCode = InReceiptIqcRecord.materialCode;
            wmsInReceiptIqcResult.materialName = InReceiptIqcRecord.materialName;
            wmsInReceiptIqcResult.supplierCode = InReceiptIqcRecord.supplierCode;
            wmsInReceiptIqcResult.supplierName = InReceiptIqcRecord.supplierName;
            wmsInReceiptIqcResult.supplierNameEn = InReceiptIqcRecord.supplierNameEn;
            wmsInReceiptIqcResult.supplierNameAlias = InReceiptIqcRecord.supplierNameAlias;
            wmsInReceiptIqcResult.batchNo = InReceiptIqcRecord.batchNo;
            wmsInReceiptIqcResult.materialSpec = InReceiptIqcRecord.materialSpec;
            wmsInReceiptIqcResult.qty = doInReceiptIqcResultParam.qty;
            wmsInReceiptIqcResult.iqcRecordNo = InReceiptIqcRecord.iqcRecordNo;
            wmsInReceiptIqcResult.returnQty = doInReceiptIqcResultParam.qcResult == InspectionResult.UnQualitified.GetCode() ? (doInReceiptIqcResultParam.qty ?? 0) : 0;
            wmsInReceiptIqcResult.recordQty = 0;
            wmsInReceiptIqcResult.putawayQty = 0;
            wmsInReceiptIqcResult.postBackQty = 0;

            wmsInReceiptIqcResult.inspectionResult = doInReceiptIqcResultParam.qcResult == InspectionResult.UnQualitified.GetCode() ? Convert.ToInt32(InspectionResult.UnQualitified.GetCode()) : Convert.ToInt32(InspectionResult.Qualitified.GetCode());
            //wmsInReceiptIqcResult.inspectionResult = InspectionResult.Qualitified.GetCode();
            wmsInReceiptIqcResult.skuCode = InReceiptIqcRecord.materialCode;
            wmsInReceiptIqcResult.iqcResultStatus = Convert.ToInt32(IqcResultStatus.Init.GetCode());
            wmsInReceiptIqcResult.badDescription = doInReceiptIqcResultParam.ngReason;

            // wmsInReceiptIqcResult.isReturnFlag = wmsInOrderDetail;

            wmsInReceiptIqcResult.productSn = wmsInOrderDetail.productSn;
            wmsInReceiptIqcResult.departmentName = wmsInOrderDetail.departmentName;
            wmsInReceiptIqcResult.projectNo = InReceiptIqcRecord.projectNo;
            //wmsInReceiptIqcResult.ticketNo = wmsInOrder.ticketNo;
            wmsInReceiptIqcResult.ticketNo = wmsInOrderDetail.orderNo;
            wmsInReceiptIqcResult.inOutTypeNo = InReceiptIqcRecord.inOutTypeNo;
            wmsInReceiptIqcResult.inOutName = InReceiptIqcRecord.inOutName;
            wmsInReceiptIqcResult.inspector = InReceiptIqcRecord.inspector;
            wmsInReceiptIqcResult.minPkgQty = InReceiptIqcRecord.minPkgQty;
            wmsInReceiptIqcResult.urgentFlag = InReceiptIqcRecord.urgentFlag;
            wmsInReceiptIqcResult.unitCode = InReceiptIqcRecord.unitCode;
            //wmsInReceiptIqcResult.delFlag = InReceiptIqcRecord.delFlag;
            //wmsInReceiptIqcResult.CreateBy = LoginUserInfo.ITCode;
            wmsInReceiptIqcResult.CreateBy = invoker;
            wmsInReceiptIqcResult.CreateTime = DateTime.Now;
            wmsInReceiptIqcResult.extend1 = InReceiptIqcRecord.extend1;
            wmsInReceiptIqcResult.extend2 = InReceiptIqcRecord.extend2;
            wmsInReceiptIqcResult.extend3 = InReceiptIqcRecord.extend3;
            wmsInReceiptIqcResult.extend4 = InReceiptIqcRecord.extend4;
            wmsInReceiptIqcResult.extend5 = InReceiptIqcRecord.extend5;
            wmsInReceiptIqcResult.extend6 = InReceiptIqcRecord.extend6;
            wmsInReceiptIqcResult.extend7 = InReceiptIqcRecord.extend7;
            wmsInReceiptIqcResult.extend8 = InReceiptIqcRecord.extend8;
            wmsInReceiptIqcResult.extend9 = InReceiptIqcRecord.extend9;
            wmsInReceiptIqcResult.extend10 = InReceiptIqcRecord.extend10;
            wmsInReceiptIqcResult.extend11 = InReceiptIqcRecord.extend11;
            wmsInReceiptIqcResult.extend12 = InReceiptIqcRecord.extend12;
            wmsInReceiptIqcResult.extend13 = InReceiptIqcRecord.extend13;
            wmsInReceiptIqcResult.extend14 = InReceiptIqcRecord.extend14;
            wmsInReceiptIqcResult.extend15 = InReceiptIqcRecord.extend15;

            return wmsInReceiptIqcResult;
        }

    }
}
