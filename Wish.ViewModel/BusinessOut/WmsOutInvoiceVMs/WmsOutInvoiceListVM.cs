using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceVMs
{
    public partial class WmsOutInvoiceListVM : BasePagedListVM<WmsOutInvoice_View, WmsOutInvoiceSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsOutInvoice_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsOutInvoice_View>>{
                this.MakeGridHeader(x => x.cvCode),
                this.MakeGridHeader(x => x.cvName),
                this.MakeGridHeader(x => x.cvNameAlias),
                this.MakeGridHeader(x => x.cvNameEn),
                this.MakeGridHeader(x => x.cvType),
                this.MakeGridHeader(x => x.docTypeCode),
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
                this.MakeGridHeader(x => x.fpName),
                this.MakeGridHeader(x => x.fpNo),
                this.MakeGridHeader(x => x.fpQty),
                this.MakeGridHeader(x => x.inOutName),
                this.MakeGridHeader(x => x.inOutTypeNo),
                this.MakeGridHeader(x => x.invoiceNo),
                this.MakeGridHeader(x => x.invoiceStatus),
                this.MakeGridHeader(x => x.operationReason),
                this.MakeGridHeader(x => x.orderDesc),
                this.MakeGridHeader(x => x.orderPriority),
                this.MakeGridHeader(x => x.productLocation),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.sourceBy),
                this.MakeGridHeader(x => x.ticketNo),
                this.MakeGridHeader(x => x.ticketType),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeader(x => x.projectNo),
                this.MakeGridHeader(x => x.waveNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsOutInvoice_View> GetSearchQuery()
        {
            var query = DC.Set<WmsOutInvoice>()
                .CheckContain(Searcher.cvCode, x=>x.cvCode)
                .CheckContain(Searcher.cvName, x=>x.cvName)
                .CheckContain(Searcher.cvType, x=>x.cvType)
                .CheckContain(Searcher.docTypeCode, x=>x.docTypeCode)
                .CheckContain(Searcher.externalOutNo, x=>x.externalOutNo)
                .CheckContain(Searcher.fpNo, x=>x.fpNo)
                .CheckContain(Searcher.inOutTypeNo, x=>x.inOutTypeNo)
                .CheckContain(Searcher.invoiceNo, x=>x.invoiceNo)
                .CheckEqual(Searcher.invoiceStatus, x=>x.invoiceStatus)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckEqual(Searcher.sourceBy, x=>x.sourceBy)
                .CheckContain(Searcher.ticketNo, x=>x.ticketNo)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .CheckContain(Searcher.projectNo, x=>x.projectNo)
                .CheckContain(Searcher.waveNo, x=>x.waveNo)
                .Select(x => new WmsOutInvoice_View
                {
				    ID = x.ID,
                    cvCode = x.cvCode,
                    cvName = x.cvName,
                    cvNameAlias = x.cvNameAlias,
                    cvNameEn = x.cvNameEn,
                    cvType = x.cvType,
                    docTypeCode = x.docTypeCode,
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
                    fpName = x.fpName,
                    fpNo = x.fpNo,
                    fpQty = x.fpQty,
                    inOutName = x.inOutName,
                    inOutTypeNo = x.inOutTypeNo,
                    invoiceNo = x.invoiceNo,
                    invoiceStatus = x.invoiceStatus,
                    operationReason = x.operationReason,
                    orderDesc = x.orderDesc,
                    orderPriority = x.orderPriority,
                    productLocation = x.productLocation,
                    proprietorCode = x.proprietorCode,
                    sourceBy = x.sourceBy,
                    ticketNo = x.ticketNo,
                    ticketType = x.ticketType,
                    whouseNo = x.whouseNo,
                    projectNo = x.projectNo,
                    waveNo = x.waveNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsOutInvoice_View : WmsOutInvoice{

    }
}
