using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessPutdown.WmsPutdownVMs
{
    public partial class WmsPutdownListVM : BasePagedListVM<WmsPutdown_View, WmsPutdownSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsPutdown_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsPutdown_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.binNo),
                this.MakeGridHeader(x => x.docTypeCode),
                this.MakeGridHeader(x => x.loadedType),
                this.MakeGridHeader(x => x.orderNo),
                this.MakeGridHeader(x => x.palletBarcode),
                this.MakeGridHeader(x => x.pickupMethod),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.putdownNo),
                this.MakeGridHeader(x => x.putdownStatus),
                this.MakeGridHeader(x => x.regionNo),
                this.MakeGridHeader(x => x.stockCode),
                this.MakeGridHeader(x => x.targetLocNo),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsPutdown_View> GetSearchQuery()
        {
            var query = DC.Set<WmsPutdown>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.binNo, x=>x.binNo)
                .CheckContain(Searcher.docTypeCode, x=>x.docTypeCode)
                .CheckEqual(Searcher.loadedType, x=>x.loadedType)
                .CheckContain(Searcher.orderNo, x=>x.orderNo)
                .CheckContain(Searcher.palletBarcode, x=>x.palletBarcode)
                .CheckContain(Searcher.pickupMethod, x=>x.pickupMethod)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.putdownNo, x=>x.putdownNo)
                .CheckEqual(Searcher.putdownStatus, x=>x.putdownStatus)
                .CheckContain(Searcher.regionNo, x=>x.regionNo)
                .CheckContain(Searcher.stockCode, x=>x.stockCode)
                .CheckContain(Searcher.targetLocNo, x=>x.targetLocNo)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new WmsPutdown_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    binNo = x.binNo,
                    docTypeCode = x.docTypeCode,
                    loadedType = x.loadedType,
                    orderNo = x.orderNo,
                    palletBarcode = x.palletBarcode,
                    pickupMethod = x.pickupMethod,
                    proprietorCode = x.proprietorCode,
                    putdownNo = x.putdownNo,
                    putdownStatus = x.putdownStatus,
                    regionNo = x.regionNo,
                    stockCode = x.stockCode,
                    targetLocNo = x.targetLocNo,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsPutdown_View : WmsPutdown{

    }
}
