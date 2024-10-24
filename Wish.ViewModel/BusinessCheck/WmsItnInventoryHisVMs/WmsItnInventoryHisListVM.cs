using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryHisVMs
{
    public partial class WmsItnInventoryHisListVM : BasePagedListVM<WmsItnInventoryHis_View, WmsItnInventoryHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsItnInventoryHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsItnInventoryHis_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.blindFlag),
                this.MakeGridHeader(x => x.docTypeCode),
                this.MakeGridHeader(x => x.erpWhouseNo),
                this.MakeGridHeader(x => x.inventoryLocNo),
                this.MakeGridHeader(x => x.inventoryNo),
                this.MakeGridHeader(x => x.inventoryStatus),
                this.MakeGridHeader(x => x.issuedFlag),
                this.MakeGridHeader(x => x.issuedOperator),
                this.MakeGridHeader(x => x.issuedTime),
                this.MakeGridHeader(x => x.operationReason),
                this.MakeGridHeader(x => x.orderDesc),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsItnInventoryHis_View> GetSearchQuery()
        {
            var query = DC.Set<WmsItnInventoryHis>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckEqual(Searcher.blindFlag, x=>x.blindFlag)
                .CheckContain(Searcher.docTypeCode, x=>x.docTypeCode)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckContain(Searcher.inventoryLocNo, x=>x.inventoryLocNo)
                .CheckContain(Searcher.inventoryNo, x=>x.inventoryNo)
                .CheckEqual(Searcher.inventoryStatus, x=>x.inventoryStatus)
                .CheckEqual(Searcher.issuedFlag, x=>x.issuedFlag)
                .CheckContain(Searcher.issuedOperator, x=>x.issuedOperator)
                .CheckBetween(Searcher.issuedTime?.GetStartTime(), Searcher.issuedTime?.GetEndTime(), x => x.issuedTime, includeMax: false)
                .CheckContain(Searcher.operationReason, x=>x.operationReason)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new WmsItnInventoryHis_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    blindFlag = x.blindFlag,
                    docTypeCode = x.docTypeCode,
                    erpWhouseNo = x.erpWhouseNo,
                    inventoryLocNo = x.inventoryLocNo,
                    inventoryNo = x.inventoryNo,
                    inventoryStatus = x.inventoryStatus,
                    issuedFlag = x.issuedFlag,
                    issuedOperator = x.issuedOperator,
                    issuedTime = x.issuedTime,
                    operationReason = x.operationReason,
                    orderDesc = x.orderDesc,
                    proprietorCode = x.proprietorCode,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsItnInventoryHis_View : WmsItnInventoryHis{

    }
}
