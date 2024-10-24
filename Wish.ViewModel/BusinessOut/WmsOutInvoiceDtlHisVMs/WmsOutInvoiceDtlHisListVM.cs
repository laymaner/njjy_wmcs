using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceDtlHisVMs
{
    public partial class WmsOutInvoiceDtlHisListVM : BasePagedListVM<WmsOutInvoiceDtlHis_View, WmsOutInvoiceDtlHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsOutInvoiceDtlHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsOutInvoiceDtlHis_View>>{
                this.MakeGridHeader(x => x.allocatResult),
                this.MakeGridHeader(x => x.allotQty),
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.assemblyIdx),
                this.MakeGridHeader(x => x.batchNo),
                this.MakeGridHeader(x => x.belongDepartment),
                this.MakeGridHeader(x => x.completeQty),
                this.MakeGridHeader(x => x.erpUndeliverQty),
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
                this.MakeGridHeader(x => x.externalOutDtlId),
                this.MakeGridHeader(x => x.externalOutNo),
                this.MakeGridHeader(x => x.invoiceDtlStatus),
                this.MakeGridHeader(x => x.invoiceNo),
                this.MakeGridHeader(x => x.invoiceQty),
                this.MakeGridHeader(x => x.materialName),
                this.MakeGridHeader(x => x.materialCode),
                this.MakeGridHeader(x => x.materialSpec),
                this.MakeGridHeader(x => x.orderId),
                this.MakeGridHeader(x => x.orderNo),
                this.MakeGridHeader(x => x.productLocation),
                this.MakeGridHeader(x => x.productDeptCode),
                this.MakeGridHeader(x => x.productDeptName),
                this.MakeGridHeader(x => x.projectNo),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.putdownQty),
                this.MakeGridHeader(x => x.productSn),
                this.MakeGridHeader(x => x.originalSn),
                this.MakeGridHeader(x => x.supplyType),
                this.MakeGridHeader(x => x.ticketPlanBeginTime),
                this.MakeGridHeader(x => x.waveNo),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeader(x => x.companyCode),
                this.MakeGridHeader(x => x.intfId),
                this.MakeGridHeader(x => x.intfBatchId),
                this.MakeGridHeader(x => x.supplierCode),
                this.MakeGridHeader(x => x.supplierName),
                this.MakeGridHeader(x => x.supplierNameEn),
                this.MakeGridHeader(x => x.supplierNameAlias),
                this.MakeGridHeader(x => x.ticketNo),
                this.MakeGridHeader(x => x.unitCode),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsOutInvoiceDtlHis_View> GetSearchQuery()
        {
            var query = DC.Set<WmsOutInvoiceDtlHis>()
                .CheckContain(Searcher.allocatResult, x=>x.allocatResult)
                .CheckEqual(Searcher.allotQty, x=>x.allotQty)
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.batchNo, x=>x.batchNo)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckContain(Searcher.externalOutNo, x=>x.externalOutNo)
                .CheckEqual(Searcher.invoiceDtlStatus, x=>x.invoiceDtlStatus)
                .CheckContain(Searcher.invoiceNo, x=>x.invoiceNo)
                .CheckContain(Searcher.materialCode, x=>x.materialCode)
                .CheckContain(Searcher.orderNo, x=>x.orderNo)
                .CheckContain(Searcher.productDeptCode, x=>x.productDeptCode)
                .CheckContain(Searcher.productDeptName, x=>x.productDeptName)
                .CheckContain(Searcher.projectNo, x=>x.projectNo)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.productSn, x=>x.productSn)
                .CheckContain(Searcher.supplyType, x=>x.supplyType)
                .CheckContain(Searcher.waveNo, x=>x.waveNo)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .CheckContain(Searcher.companyCode, x=>x.companyCode)
                .CheckContain(Searcher.supplierCode, x=>x.supplierCode)
                .CheckContain(Searcher.ticketNo, x=>x.ticketNo)
                .CheckContain(Searcher.unitCode, x=>x.unitCode)
                .Select(x => new WmsOutInvoiceDtlHis_View
                {
				    ID = x.ID,
                    allocatResult = x.allocatResult,
                    allotQty = x.allotQty,
                    areaNo = x.areaNo,
                    assemblyIdx = x.assemblyIdx,
                    batchNo = x.batchNo,
                    belongDepartment = x.belongDepartment,
                    completeQty = x.completeQty,
                    erpUndeliverQty = x.erpUndeliverQty,
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
                    externalOutDtlId = x.externalOutDtlId,
                    externalOutNo = x.externalOutNo,
                    invoiceDtlStatus = x.invoiceDtlStatus,
                    invoiceNo = x.invoiceNo,
                    invoiceQty = x.invoiceQty,
                    materialName = x.materialName,
                    materialCode = x.materialCode,
                    materialSpec = x.materialSpec,
                    orderId = x.orderId,
                    orderNo = x.orderNo,
                    productLocation = x.productLocation,
                    productDeptCode = x.productDeptCode,
                    productDeptName = x.productDeptName,
                    projectNo = x.projectNo,
                    proprietorCode = x.proprietorCode,
                    putdownQty = x.putdownQty,
                    productSn = x.productSn,
                    originalSn = x.originalSn,
                    supplyType = x.supplyType,
                    ticketPlanBeginTime = x.ticketPlanBeginTime,
                    waveNo = x.waveNo,
                    whouseNo = x.whouseNo,
                    companyCode = x.companyCode,
                    intfId = x.intfId,
                    intfBatchId = x.intfBatchId,
                    supplierCode = x.supplierCode,
                    supplierName = x.supplierName,
                    supplierNameEn = x.supplierNameEn,
                    supplierNameAlias = x.supplierNameAlias,
                    ticketNo = x.ticketNo,
                    unitCode = x.unitCode,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsOutInvoiceDtlHis_View : WmsOutInvoiceDtlHis{

    }
}
