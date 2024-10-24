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
using WISH.Helper.Common;
using Wish.ViewModel.Base.BasBMaterialVMs;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using Wish.ViewModel.BusinessIn.WmsInReceiptVMs;
using Wish.ViewModel.BusinessIn.WmsInReceiptUniicodeVMs;
using Wish.ViewModel.BusinessIn.WmsInReceiptIqcResultVMs;
using Wish.ViewModel.System.SysSequenceVMs;
using Wish.ViewModel.Common.Dtos;
using Wish.DtoModel.Common.Dtos;

namespace Wish.ViewModel.BusinessIn.WmsInReceiptIqcRecordVMs
{
    public partial class WmsInReceiptIqcRecordVM
    {
        private WmsInReceiptIqcRecord BuildInReceiptIqcRecord(WmsInReceipt wmsInReceipt,
            WmsInReceiptDtl wmsInReceiptDt,
            string iqcRecordNo,
            DoReceiptIqcRecordDto wmsItnQcInParamView, string invoker)
        {
            WmsInReceiptIqcRecord wmsInReceiptIqcRecord = new WmsInReceiptIqcRecord();

            wmsInReceiptIqcRecord.whouseNo = wmsInReceiptDt.whouseNo;
            wmsInReceiptIqcRecord.areaNo = wmsInReceiptDt.areaNo;
            wmsInReceiptIqcRecord.erpWhouseNo = wmsInReceiptDt.erpWhouseNo;
            wmsInReceiptIqcRecord.regionNo = wmsInReceipt.regionNo;
            wmsInReceiptIqcRecord.binNo = wmsInReceipt.binNo;
            wmsInReceiptIqcRecord.proprietorCode = wmsInReceiptDt.proprietorCode;
            wmsInReceiptIqcRecord.iqcRecordNo = iqcRecordNo;
            wmsInReceiptIqcRecord.receiptNo = wmsInReceipt.receiptNo;
            wmsInReceiptIqcRecord.receiptDtlId = wmsInReceiptDt.ID;
            wmsInReceiptIqcRecord.inNo = wmsInReceiptDt.inNo;
            wmsInReceiptIqcRecord.inDtlId = wmsInReceiptDt.inDtlId;
            wmsInReceiptIqcRecord.externalInNo = wmsInReceiptDt.externalInNo;
            wmsInReceiptIqcRecord.externalInDtlId = wmsInReceiptDt.externalInDtlId;
            wmsInReceiptIqcRecord.projectNo = wmsInReceiptDt.projectNo;
            wmsInReceiptIqcRecord.docTypeCode = wmsInReceipt.docTypeCode;
            wmsInReceiptIqcRecord.sourceBy = wmsInReceipt.sourceBy;
            wmsInReceiptIqcRecord.inOutTypeNo = wmsInReceipt.inOutTypeNo;
            wmsInReceiptIqcRecord.inOutName = wmsInReceipt.inOutName;

            // 质检方式;WMS质检、ERP质检
            wmsInReceiptIqcRecord.iqcType = invoker.Contains("EBS") ? InReceiptIqcType.ERP.GetCode() : InReceiptIqcType.WMS.GetCode();
            wmsInReceiptIqcRecord.materialCode = wmsInReceiptDt.materialCode;
            wmsInReceiptIqcRecord.materialName = wmsInReceiptDt.materialName;
            wmsInReceiptIqcRecord.supplierCode = wmsInReceiptDt.supplierCode;
            wmsInReceiptIqcRecord.supplierName = wmsInReceiptDt.supplierName;
            wmsInReceiptIqcRecord.supplierNameEn = wmsInReceiptDt.supplierNameEn;
            wmsInReceiptIqcRecord.supplierNameAlias = wmsInReceiptDt.supplierNameAlias;
            wmsInReceiptIqcRecord.batchNo = wmsInReceiptDt.batchNo;
            wmsInReceiptIqcRecord.materialSpec = wmsInReceiptDt.materialSpec;
            wmsInReceiptIqcRecord.receiptQty = wmsInReceiptDt.receiptQty;
            wmsInReceiptIqcRecord.qualifiedQty = wmsItnQcInParamView.passQty ?? 0;
            //wmsInReceiptIqcRecord.unqualifiedQty = wmsInReceiptDt.unqualifiedQty;//页面质检只做合格，外部质检推送不合格数量
            //wmsInReceiptIqcRecord.unqualifiedQty = wmsItnQcInParamView.noPassQty ?? 0;
            wmsInReceiptIqcRecord.wmsUnqualifiedQty = wmsItnQcInParamView.noPassQty ?? 0;
            //wmsInReceiptIqcRecord.qualifiedSpecialQty = wmsInReceiptDt.qualifiedSpecialQty;
            wmsInReceiptIqcRecord.erpQualifiedSpecialQty = wmsInReceiptDt.qualifiedSpecialQty;
            //wmsInReceiptIqcRecord.unqualifiedSpecialQty = 0;
            wmsInReceiptIqcRecord.erpUnqualifiedQty = 0;
            wmsInReceiptIqcRecord.iqcRecordStatus = Convert.ToInt32(ReceiptOrDtlStatus.Init.GetCode());
            wmsInReceiptIqcRecord.inspector = wmsInReceiptDt.inspector;
            wmsInReceiptIqcRecord.badOptions = wmsItnQcInParamView.badOptions;
            //wmsInReceiptIqcRecord.detailDescription = wmsItnQcInParamView.detailDescription;
            wmsInReceiptIqcRecord.badSolveType = wmsItnQcInParamView.badOptions;
            wmsInReceiptIqcRecord.minPkgQty = wmsInReceiptDt.minPkgQty;
            wmsInReceiptIqcRecord.urgentFlag = wmsInReceiptDt.urgentFlag;
            wmsInReceiptIqcRecord.unitCode = wmsInReceiptDt.unitCode;
            //wmsInReceiptIqcRecord.delFlag = DelFlag.NDelete.GetCode();
            wmsInReceiptIqcRecord.CreateBy = invoker; // string.IsNullOrWhiteSpace(workNo) ? "EBS" : workNo;
            wmsInReceiptIqcRecord.CreateTime = DateTime.Now;
            wmsInReceiptIqcRecord.extend1 = wmsInReceiptDt.extend1;
            wmsInReceiptIqcRecord.extend2 = wmsInReceiptDt.extend2;
            wmsInReceiptIqcRecord.extend3 = wmsInReceiptDt.extend3;
            wmsInReceiptIqcRecord.extend4 = wmsInReceiptDt.extend4;
            wmsInReceiptIqcRecord.extend5 = wmsInReceiptDt.extend5;
            wmsInReceiptIqcRecord.extend6 = wmsInReceiptDt.extend6;
            wmsInReceiptIqcRecord.extend7 = wmsInReceiptDt.extend7;
            wmsInReceiptIqcRecord.extend8 = wmsInReceiptDt.extend8;
            wmsInReceiptIqcRecord.extend9 = wmsInReceiptDt.extend9;
            wmsInReceiptIqcRecord.extend10 = wmsInReceiptDt.extend10;
            wmsInReceiptIqcRecord.extend11 = wmsInReceiptDt.extend11;
            wmsInReceiptIqcRecord.extend12 = wmsInReceiptDt.extend12;
            wmsInReceiptIqcRecord.extend13 = wmsInReceiptDt.extend13;
            wmsInReceiptIqcRecord.extend14 = wmsInReceiptDt.extend14;
            wmsInReceiptIqcRecord.extend15 = wmsInReceiptDt.extend15;


            return wmsInReceiptIqcRecord;
        }

    }
}
