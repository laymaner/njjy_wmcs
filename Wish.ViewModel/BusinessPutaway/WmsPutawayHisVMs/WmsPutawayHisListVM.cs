using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessPutaway.WmsPutawayHisVMs
{
    public partial class WmsPutawayHisListVM : BasePagedListVM<WmsPutawayHis_View, WmsPutawayHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsPutawayHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsPutawayHis_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.loadedType),
                this.MakeGridHeader(x => x.manualFlag),
                this.MakeGridHeader(x => x.onlineLocNo),
                this.MakeGridHeader(x => x.onlineMethod),
                this.MakeGridHeader(x => x.palletBarcode),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.ptaRegionNo),
                this.MakeGridHeader(x => x.putawayNo),
                this.MakeGridHeader(x => x.putawayStatus),
                this.MakeGridHeader(x => x.regionNo),
                this.MakeGridHeader(x => x.stockCode),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsPutawayHis_View> GetSearchQuery()
        {
            var query = DC.Set<WmsPutawayHis>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckEqual(Searcher.loadedType, x=>x.loadedType)
                .CheckEqual(Searcher.manualFlag, x=>x.manualFlag)
                .CheckContain(Searcher.onlineLocNo, x=>x.onlineLocNo)
                .CheckContain(Searcher.onlineMethod, x=>x.onlineMethod)
                .CheckContain(Searcher.palletBarcode, x=>x.palletBarcode)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.ptaRegionNo, x=>x.ptaRegionNo)
                .CheckContain(Searcher.putawayNo, x=>x.putawayNo)
                .CheckEqual(Searcher.putawayStatus, x=>x.putawayStatus)
                .CheckContain(Searcher.regionNo, x=>x.regionNo)
                .CheckContain(Searcher.stockCode, x=>x.stockCode)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new WmsPutawayHis_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    loadedType = x.loadedType,
                    manualFlag = x.manualFlag,
                    onlineLocNo = x.onlineLocNo,
                    onlineMethod = x.onlineMethod,
                    palletBarcode = x.palletBarcode,
                    proprietorCode = x.proprietorCode,
                    ptaRegionNo = x.ptaRegionNo,
                    putawayNo = x.putawayNo,
                    putawayStatus = x.putawayStatus,
                    regionNo = x.regionNo,
                    stockCode = x.stockCode,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsPutawayHis_View : WmsPutawayHis{

    }
}
