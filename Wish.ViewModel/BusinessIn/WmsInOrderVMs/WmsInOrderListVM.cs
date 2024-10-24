using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInOrderVMs
{
    public partial class WmsInOrderListVM : BasePagedListVM<WmsInOrder_View, WmsInOrderSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsInOrder_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsInOrder_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.cvCode),
                this.MakeGridHeader(x => x.cvName),
                this.MakeGridHeader(x => x.cvNameAlias),
                this.MakeGridHeader(x => x.cvNameEn),
                this.MakeGridHeader(x => x.cvType),
                this.MakeGridHeader(x => x.deliverMode),
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
                this.MakeGridHeader(x => x.externalInId),
                this.MakeGridHeader(x => x.externalInNo),
                this.MakeGridHeader(x => x.inNo),
                this.MakeGridHeader(x => x.inOutName),
                this.MakeGridHeader(x => x.inOutTypeNo),
                this.MakeGridHeader(x => x.inStatus),
                this.MakeGridHeader(x => x.operationReason),
                this.MakeGridHeader(x => x.orderDesc),
                this.MakeGridHeader(x => x.planArrivalDate),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.sourceBy),
                this.MakeGridHeader(x => x.ticketNo),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsInOrder_View> GetSearchQuery()
        {
            var query = DC.Set<WmsInOrder>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.cvCode, x=>x.cvCode)
                .CheckContain(Searcher.cvName, x=>x.cvName)
                .CheckContain(Searcher.cvType, x=>x.cvType)
                .CheckContain(Searcher.deliverMode, x=>x.deliverMode)
                .CheckContain(Searcher.docTypeCode, x=>x.docTypeCode)
                .CheckContain(Searcher.externalInNo, x=>x.externalInNo)
                .CheckContain(Searcher.inNo, x=>x.inNo)
                .CheckEqual(Searcher.inStatus, x=>x.inStatus)
                .CheckEqual(Searcher.sourceBy, x=>x.sourceBy)
                .CheckContain(Searcher.ticketNo, x=>x.ticketNo)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new WmsInOrder_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    cvCode = x.cvCode,
                    cvName = x.cvName,
                    cvNameAlias = x.cvNameAlias,
                    cvNameEn = x.cvNameEn,
                    cvType = x.cvType,
                    deliverMode = x.deliverMode,
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
                    externalInId = x.externalInId,
                    externalInNo = x.externalInNo,
                    inNo = x.inNo,
                    inOutName = x.inOutName,
                    inOutTypeNo = x.inOutTypeNo,
                    inStatus = x.inStatus,
                    operationReason = x.operationReason,
                    orderDesc = x.orderDesc,
                    planArrivalDate = x.planArrivalDate,
                    proprietorCode = x.proprietorCode,
                    sourceBy = x.sourceBy,
                    ticketNo = x.ticketNo,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsInOrder_View : WmsInOrder{

    }
}
