using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInOrderDtlHisVMs
{
    public partial class WmsInOrderDtlHisListVM : BasePagedListVM<WmsInOrderDtlHis_View, WmsInOrderDtlHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsInOrderDtlHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsInOrderDtlHis_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.batchNo),
                this.MakeGridHeader(x => x.companyCode),
                this.MakeGridHeader(x => x.departmentName),
                this.MakeGridHeader(x => x.inQty),
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
                this.MakeGridHeader(x => x.inDtlStatus),
                this.MakeGridHeader(x => x.inNo),
                this.MakeGridHeader(x => x.inspector),
                this.MakeGridHeader(x => x.intfBatchId),
                this.MakeGridHeader(x => x.intfId),
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
                this.MakeGridHeader(x => x.purchaseOrderId),
                this.MakeGridHeader(x => x.purchaseOrderMaker),
                this.MakeGridHeader(x => x.purchaseOrderNo),
                this.MakeGridHeader(x => x.putawayQty),
                this.MakeGridHeader(x => x.qualifiedQty),
                this.MakeGridHeader(x => x.qualifiedSpecialQty),
                this.MakeGridHeader(x => x.receiptQty),
                this.MakeGridHeader(x => x.recordQty),
                this.MakeGridHeader(x => x.returnQty),
                this.MakeGridHeader(x => x.rejectQty),
                this.MakeGridHeader(x => x.supplierName),
                this.MakeGridHeader(x => x.supplierNameAlias),
                this.MakeGridHeader(x => x.supplierNameEn),
                this.MakeGridHeader(x => x.supplierCode),
                this.MakeGridHeader(x => x.unqualifiedQty),
                this.MakeGridHeader(x => x.urgentFlag),
                this.MakeGridHeader(x => x.unitCode),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsInOrderDtlHis_View> GetSearchQuery()
        {
            var query = DC.Set<WmsInOrderDtlHis>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.batchNo, x=>x.batchNo)
                .CheckContain(Searcher.companyCode, x=>x.companyCode)
                .CheckContain(Searcher.departmentName, x=>x.departmentName)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckContain(Searcher.externalInNo, x=>x.externalInNo)
                .CheckEqual(Searcher.inDtlStatus, x=>x.inDtlStatus)
                .CheckContain(Searcher.inNo, x=>x.inNo)
                .CheckContain(Searcher.inspector, x=>x.inspector)
                .CheckContain(Searcher.materialName, x=>x.materialName)
                .CheckContain(Searcher.materialCode, x=>x.materialCode)
                .CheckEqual(Searcher.minPkgQty, x=>x.minPkgQty)
                .CheckContain(Searcher.projectNo, x=>x.projectNo)
                .CheckContain(Searcher.purchaseOrderId, x=>x.purchaseOrderId)
                .CheckContain(Searcher.purchaseOrderMaker, x=>x.purchaseOrderMaker)
                .CheckContain(Searcher.supplierCode, x=>x.supplierCode)
                .CheckEqual(Searcher.urgentFlag, x=>x.urgentFlag)
                .CheckContain(Searcher.unitCode, x=>x.unitCode)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new WmsInOrderDtlHis_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    batchNo = x.batchNo,
                    companyCode = x.companyCode,
                    departmentName = x.departmentName,
                    inQty = x.inQty,
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
                    inDtlStatus = x.inDtlStatus,
                    inNo = x.inNo,
                    inspector = x.inspector,
                    intfBatchId = x.intfBatchId,
                    intfId = x.intfId,
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
                    purchaseOrderId = x.purchaseOrderId,
                    purchaseOrderMaker = x.purchaseOrderMaker,
                    purchaseOrderNo = x.purchaseOrderNo,
                    putawayQty = x.putawayQty,
                    qualifiedQty = x.qualifiedQty,
                    qualifiedSpecialQty = x.qualifiedSpecialQty,
                    receiptQty = x.receiptQty,
                    recordQty = x.recordQty,
                    returnQty = x.returnQty,
                    rejectQty = x.rejectQty,
                    supplierName = x.supplierName,
                    supplierNameAlias = x.supplierNameAlias,
                    supplierNameEn = x.supplierNameEn,
                    supplierCode = x.supplierCode,
                    unqualifiedQty = x.unqualifiedQty,
                    urgentFlag = x.urgentFlag,
                    unitCode = x.unitCode,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsInOrderDtlHis_View : WmsInOrderDtlHis{

    }
}
