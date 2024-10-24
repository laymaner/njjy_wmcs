using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptIqcRecordHisVMs
{
    public partial class WmsInReceiptIqcRecordHisListVM : BasePagedListVM<WmsInReceiptIqcRecordHis_View, WmsInReceiptIqcRecordHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsInReceiptIqcRecordHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsInReceiptIqcRecordHis_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.badDescription),
                this.MakeGridHeader(x => x.badOptions),
                this.MakeGridHeader(x => x.badSolveType),
                this.MakeGridHeader(x => x.batchNo),
                this.MakeGridHeader(x => x.binNo),
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
                this.MakeGridHeader(x => x.inspector),
                this.MakeGridHeader(x => x.iqcRecordNo),
                this.MakeGridHeader(x => x.iqcRecordStatus),
                this.MakeGridHeader(x => x.iqcType),
                this.MakeGridHeader(x => x.materialName),
                this.MakeGridHeader(x => x.materialCode),
                this.MakeGridHeader(x => x.materialSpec),
                this.MakeGridHeader(x => x.minPkgQty),
                this.MakeGridHeader(x => x.orderDtlId),
                this.MakeGridHeader(x => x.orderNo),
                this.MakeGridHeader(x => x.orderDesc),
                this.MakeGridHeader(x => x.projectNo),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.qualifiedQty),
                this.MakeGridHeader(x => x.erpQualifiedSpecialQty),
                this.MakeGridHeader(x => x.receiptDtlId),
                this.MakeGridHeader(x => x.receiptNo),
                this.MakeGridHeader(x => x.receiptQty),
                this.MakeGridHeader(x => x.regionNo),
                this.MakeGridHeader(x => x.sourceBy),
                this.MakeGridHeader(x => x.supplierName),
                this.MakeGridHeader(x => x.supplierNameAlias),
                this.MakeGridHeader(x => x.supplierNameEn),
                this.MakeGridHeader(x => x.supplierCode),
                this.MakeGridHeader(x => x.wmsUnqualifiedQty),
                this.MakeGridHeader(x => x.erpUnqualifiedQty),
                this.MakeGridHeader(x => x.urgentFlag),
                this.MakeGridHeader(x => x.unitCode),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsInReceiptIqcRecordHis_View> GetSearchQuery()
        {
            var query = DC.Set<WmsInReceiptIqcRecordHis>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.badDescription, x=>x.badDescription)
                .CheckContain(Searcher.badOptions, x=>x.badOptions)
                .CheckContain(Searcher.badSolveType, x=>x.badSolveType)
                .CheckContain(Searcher.binNo, x=>x.binNo)
                .CheckContain(Searcher.docTypeCode, x=>x.docTypeCode)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckContain(Searcher.externalInNo, x=>x.externalInNo)
                .CheckContain(Searcher.inNo, x=>x.inNo)
                .CheckContain(Searcher.inOutTypeNo, x=>x.inOutTypeNo)
                .CheckContain(Searcher.iqcRecordNo, x=>x.iqcRecordNo)
                .CheckEqual(Searcher.iqcRecordStatus, x=>x.iqcRecordStatus)
                .CheckContain(Searcher.iqcType, x=>x.iqcType)
                .CheckContain(Searcher.materialName, x=>x.materialName)
                .CheckContain(Searcher.materialCode, x=>x.materialCode)
                .CheckContain(Searcher.materialSpec, x=>x.materialSpec)
                .CheckContain(Searcher.orderNo, x=>x.orderNo)
                .CheckContain(Searcher.projectNo, x=>x.projectNo)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.receiptNo, x=>x.receiptNo)
                .CheckEqual(Searcher.receiptQty, x=>x.receiptQty)
                .CheckContain(Searcher.regionNo, x=>x.regionNo)
                .CheckContain(Searcher.supplierName, x=>x.supplierName)
                .CheckContain(Searcher.supplierCode, x=>x.supplierCode)
                .CheckEqual(Searcher.urgentFlag, x=>x.urgentFlag)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new WmsInReceiptIqcRecordHis_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    badDescription = x.badDescription,
                    badOptions = x.badOptions,
                    badSolveType = x.badSolveType,
                    batchNo = x.batchNo,
                    binNo = x.binNo,
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
                    inspector = x.inspector,
                    iqcRecordNo = x.iqcRecordNo,
                    iqcRecordStatus = x.iqcRecordStatus,
                    iqcType = x.iqcType,
                    materialName = x.materialName,
                    materialCode = x.materialCode,
                    materialSpec = x.materialSpec,
                    minPkgQty = x.minPkgQty,
                    orderDtlId = x.orderDtlId,
                    orderNo = x.orderNo,
                    orderDesc = x.orderDesc,
                    projectNo = x.projectNo,
                    proprietorCode = x.proprietorCode,
                    qualifiedQty = x.qualifiedQty,
                    erpQualifiedSpecialQty = x.erpQualifiedSpecialQty,
                    receiptDtlId = x.receiptDtlId,
                    receiptNo = x.receiptNo,
                    receiptQty = x.receiptQty,
                    regionNo = x.regionNo,
                    sourceBy = x.sourceBy,
                    supplierName = x.supplierName,
                    supplierNameAlias = x.supplierNameAlias,
                    supplierNameEn = x.supplierNameEn,
                    supplierCode = x.supplierCode,
                    wmsUnqualifiedQty = x.wmsUnqualifiedQty,
                    erpUnqualifiedQty = x.erpUnqualifiedQty,
                    urgentFlag = x.urgentFlag,
                    unitCode = x.unitCode,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsInReceiptIqcRecordHis_View : WmsInReceiptIqcRecordHis{

    }
}
