using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptDtlVMs
{
    public partial class WmsInReceiptDtlListVM : BasePagedListVM<WmsInReceiptDtl_View, WmsInReceiptDtlSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsInReceiptDtl_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsInReceiptDtl_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.batchNo),
                this.MakeGridHeader(x => x.departmentName),
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
                this.MakeGridHeader(x => x.receiptDtlStatus),
                this.MakeGridHeader(x => x.inNo),
                this.MakeGridHeader(x => x.inspector),
                this.MakeGridHeader(x => x.materialName),
                this.MakeGridHeader(x => x.materialCode),
                this.MakeGridHeader(x => x.materialSpec),
                this.MakeGridHeader(x => x.orderDtlId),
                this.MakeGridHeader(x => x.orderNo),
                this.MakeGridHeader(x => x.minPkgQty),
                this.MakeGridHeader(x => x.postBackQty),
                this.MakeGridHeader(x => x.productSn),
                this.MakeGridHeader(x => x.projectNo),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.putawayQty),
                this.MakeGridHeader(x => x.qualifiedQty),
                this.MakeGridHeader(x => x.qualifiedSpecialQty),
                this.MakeGridHeader(x => x.receiptNo),
                this.MakeGridHeader(x => x.receiptQty),
                this.MakeGridHeader(x => x.recordQty),
                this.MakeGridHeader(x => x.returnQty),
                this.MakeGridHeader(x => x.supplierName),
                this.MakeGridHeader(x => x.supplierNameAlias),
                this.MakeGridHeader(x => x.supplierNameEn),
                this.MakeGridHeader(x => x.supplierCode),
                this.MakeGridHeader(x => x.unqualifiedQty),
                this.MakeGridHeader(x => x.urgentFlag),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeader(x => x.unitCode),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsInReceiptDtl_View> GetSearchQuery()
        {
            var query = DC.Set<WmsInReceiptDtl>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.batchNo, x=>x.batchNo)
                .CheckContain(Searcher.departmentName, x=>x.departmentName)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckContain(Searcher.externalInNo, x=>x.externalInNo)
                .CheckEqual(Searcher.receiptDtlStatus, x=>x.receiptDtlStatus)
                .CheckContain(Searcher.inNo, x=>x.inNo)
                .CheckContain(Searcher.materialName, x=>x.materialName)
                .CheckContain(Searcher.materialCode, x=>x.materialCode)
                .CheckContain(Searcher.orderNo, x=>x.orderNo)
                .CheckContain(Searcher.projectNo, x=>x.projectNo)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.receiptNo, x=>x.receiptNo)
                .CheckContain(Searcher.supplierName, x=>x.supplierName)
                .CheckContain(Searcher.supplierCode, x=>x.supplierCode)
                .CheckEqual(Searcher.urgentFlag, x=>x.urgentFlag)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .CheckContain(Searcher.unitCode, x=>x.unitCode)
                .Select(x => new WmsInReceiptDtl_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    batchNo = x.batchNo,
                    departmentName = x.departmentName,
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
                    receiptDtlStatus = x.receiptDtlStatus,
                    inNo = x.inNo,
                    inspector = x.inspector,
                    materialName = x.materialName,
                    materialCode = x.materialCode,
                    materialSpec = x.materialSpec,
                    orderDtlId = x.orderDtlId,
                    orderNo = x.orderNo,
                    minPkgQty = x.minPkgQty,
                    postBackQty = x.postBackQty,
                    productSn = x.productSn,
                    projectNo = x.projectNo,
                    proprietorCode = x.proprietorCode,
                    putawayQty = x.putawayQty,
                    qualifiedQty = x.qualifiedQty,
                    qualifiedSpecialQty = x.qualifiedSpecialQty,
                    receiptNo = x.receiptNo,
                    receiptQty = x.receiptQty,
                    recordQty = x.recordQty,
                    returnQty = x.returnQty,
                    supplierName = x.supplierName,
                    supplierNameAlias = x.supplierNameAlias,
                    supplierNameEn = x.supplierNameEn,
                    supplierCode = x.supplierCode,
                    unqualifiedQty = x.unqualifiedQty,
                    urgentFlag = x.urgentFlag,
                    whouseNo = x.whouseNo,
                    unitCode = x.unitCode,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsInReceiptDtl_View : WmsInReceiptDtl{

    }
}
