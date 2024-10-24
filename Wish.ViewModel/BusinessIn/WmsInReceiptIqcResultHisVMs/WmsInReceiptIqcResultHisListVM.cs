using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptIqcResultHisVMs
{
    public partial class WmsInReceiptIqcResultHisListVM : BasePagedListVM<WmsInReceiptIqcResultHis_View, WmsInReceiptIqcResultHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsInReceiptIqcResultHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsInReceiptIqcResultHis_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.badDescription),
                this.MakeGridHeader(x => x.batchNo),
                this.MakeGridHeader(x => x.binNo),
                this.MakeGridHeader(x => x.departmentName),
                this.MakeGridHeader(x => x.docTypeCode),
                this.MakeGridHeader(x => x.erpWhouseNo),
                this.MakeGridHeader(x => x.extend1),
                this.MakeGridHeader(x => x.extend10),
                this.MakeGridHeader(x => x.extend11),
                this.MakeGridHeader(x => x.extend12),
                this.MakeGridHeader(x => x.extend13),
                this.MakeGridHeader(x => x.extend14),
                this.MakeGridHeader(x => x.extend15),
                this.MakeGridHeader(x => x.extend2),
                this.MakeGridHeader(x => x.extend3),
                this.MakeGridHeader(x => x.extend4),
                this.MakeGridHeader(x => x.extend5),
                this.MakeGridHeader(x => x.extend6),
                this.MakeGridHeader(x => x.extend7),
                this.MakeGridHeader(x => x.extend8),
                this.MakeGridHeader(x => x.extend9),
                this.MakeGridHeader(x => x.externalInDtlId),
                this.MakeGridHeader(x => x.externalInNo),
                this.MakeGridHeader(x => x.inDtlId),
                this.MakeGridHeader(x => x.inNo),
                this.MakeGridHeader(x => x.inOutName),
                this.MakeGridHeader(x => x.inOutTypeNo),
                this.MakeGridHeader(x => x.inspectionResult),
                this.MakeGridHeader(x => x.inspector),
                this.MakeGridHeader(x => x.iqcRecordNo),
                this.MakeGridHeader(x => x.iqcResultNo),
                this.MakeGridHeader(x => x.iqcResultStatus),
                this.MakeGridHeader(x => x.iqcType),
                this.MakeGridHeader(x => x.isReturnFlag),
                this.MakeGridHeader(x => x.materialName),
                this.MakeGridHeader(x => x.materialCode),
                this.MakeGridHeader(x => x.materialSpec),
                this.MakeGridHeader(x => x.orderDesc),
                this.MakeGridHeader(x => x.orderDtlId),
                this.MakeGridHeader(x => x.orderNo),
                this.MakeGridHeader(x => x.minPkgQty),
                this.MakeGridHeader(x => x.postBackQty),
                this.MakeGridHeader(x => x.productSn),
                this.MakeGridHeader(x => x.projectNo),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.putawayQty),
                this.MakeGridHeader(x => x.qty),
                this.MakeGridHeader(x => x.receiptDtlId),
                this.MakeGridHeader(x => x.receiptNo),
                this.MakeGridHeader(x => x.recordQty),
                this.MakeGridHeader(x => x.regionNo),
                this.MakeGridHeader(x => x.returnQty),
                this.MakeGridHeader(x => x.skuCode),
                this.MakeGridHeader(x => x.sourceBy),
                this.MakeGridHeader(x => x.supplierName),
                this.MakeGridHeader(x => x.supplierNameAlias),
                this.MakeGridHeader(x => x.supplierNameEn),
                this.MakeGridHeader(x => x.supplierCode),
                this.MakeGridHeader(x => x.ticketNo),
                this.MakeGridHeader(x => x.unitCode),
                this.MakeGridHeader(x => x.urgentFlag),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsInReceiptIqcResultHis_View> GetSearchQuery()
        {
            var query = DC.Set<WmsInReceiptIqcResultHis>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.badDescription, x=>x.badDescription)
                .CheckContain(Searcher.batchNo, x=>x.batchNo)
                .CheckContain(Searcher.binNo, x=>x.binNo)
                .CheckContain(Searcher.departmentName, x=>x.departmentName)
                .CheckContain(Searcher.docTypeCode, x=>x.docTypeCode)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckContain(Searcher.externalInNo, x=>x.externalInNo)
                .CheckContain(Searcher.inNo, x=>x.inNo)
                .CheckContain(Searcher.inOutName, x=>x.inOutName)
                .CheckContain(Searcher.inOutTypeNo, x=>x.inOutTypeNo)
                .CheckEqual(Searcher.inspectionResult, x=>x.inspectionResult)
                .CheckContain(Searcher.inspector, x=>x.inspector)
                .CheckContain(Searcher.iqcRecordNo, x=>x.iqcRecordNo)
                .CheckContain(Searcher.iqcResultNo, x=>x.iqcResultNo)
                .CheckEqual(Searcher.iqcResultStatus, x=>x.iqcResultStatus)
                .CheckContain(Searcher.iqcType, x=>x.iqcType)
                .CheckContain(Searcher.materialCode, x=>x.materialCode)
                .CheckContain(Searcher.materialSpec, x=>x.materialSpec)
                .CheckContain(Searcher.orderNo, x=>x.orderNo)
                .CheckEqual(Searcher.minPkgQty, x=>x.minPkgQty)
                .CheckContain(Searcher.projectNo, x=>x.projectNo)
                .CheckContain(Searcher.receiptNo, x=>x.receiptNo)
                .CheckContain(Searcher.supplierName, x=>x.supplierName)
                .CheckContain(Searcher.supplierCode, x=>x.supplierCode)
                .CheckContain(Searcher.unitCode, x=>x.unitCode)
                .CheckEqual(Searcher.urgentFlag, x=>x.urgentFlag)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new WmsInReceiptIqcResultHis_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    badDescription = x.badDescription,
                    batchNo = x.batchNo,
                    binNo = x.binNo,
                    departmentName = x.departmentName,
                    docTypeCode = x.docTypeCode,
                    erpWhouseNo = x.erpWhouseNo,
                    extend1 = x.extend1,
                    extend10 = x.extend10,
                    extend11 = x.extend11,
                    extend12 = x.extend12,
                    extend13 = x.extend13,
                    extend14 = x.extend14,
                    extend15 = x.extend15,
                    extend2 = x.extend2,
                    extend3 = x.extend3,
                    extend4 = x.extend4,
                    extend5 = x.extend5,
                    extend6 = x.extend6,
                    extend7 = x.extend7,
                    extend8 = x.extend8,
                    extend9 = x.extend9,
                    externalInDtlId = x.externalInDtlId,
                    externalInNo = x.externalInNo,
                    inDtlId = x.inDtlId,
                    inNo = x.inNo,
                    inOutName = x.inOutName,
                    inOutTypeNo = x.inOutTypeNo,
                    inspectionResult = x.inspectionResult,
                    inspector = x.inspector,
                    iqcRecordNo = x.iqcRecordNo,
                    iqcResultNo = x.iqcResultNo,
                    iqcResultStatus = x.iqcResultStatus,
                    iqcType = x.iqcType,
                    isReturnFlag = x.isReturnFlag,
                    materialName = x.materialName,
                    materialCode = x.materialCode,
                    materialSpec = x.materialSpec,
                    orderDesc = x.orderDesc,
                    orderDtlId = x.orderDtlId,
                    orderNo = x.orderNo,
                    minPkgQty = x.minPkgQty,
                    postBackQty = x.postBackQty,
                    productSn = x.productSn,
                    projectNo = x.projectNo,
                    proprietorCode = x.proprietorCode,
                    putawayQty = x.putawayQty,
                    qty = x.qty,
                    receiptDtlId = x.receiptDtlId,
                    receiptNo = x.receiptNo,
                    recordQty = x.recordQty,
                    regionNo = x.regionNo,
                    returnQty = x.returnQty,
                    skuCode = x.skuCode,
                    sourceBy = x.sourceBy,
                    supplierName = x.supplierName,
                    supplierNameAlias = x.supplierNameAlias,
                    supplierNameEn = x.supplierNameEn,
                    supplierCode = x.supplierCode,
                    ticketNo = x.ticketNo,
                    unitCode = x.unitCode,
                    urgentFlag = x.urgentFlag,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsInReceiptIqcResultHis_View : WmsInReceiptIqcResultHis{

    }
}
