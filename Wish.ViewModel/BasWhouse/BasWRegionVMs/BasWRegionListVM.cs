using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWRegionVMs
{
    public partial class BasWRegionListVM : BasePagedListVM<BasWRegion_View, BasWRegionSearcher>
    {

        protected override IEnumerable<IGridColumn<BasWRegion_View>> InitGridHeader()
        {
            return new List<GridColumn<BasWRegion_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.manualFlag),
                this.MakeGridHeader(x => x.palletMgt),
                this.MakeGridHeader(x => x.pickupMethod),
                this.MakeGridHeader(x => x.regionName),
                this.MakeGridHeader(x => x.regionNameAlias),
                this.MakeGridHeader(x => x.regionNameEn),
                this.MakeGridHeader(x => x.regionNo),
                this.MakeGridHeader(x => x.regionTypeCode),
                this.MakeGridHeader(x => x.sdType),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeader(x => x.virtualFlag),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasWRegion_View> GetSearchQuery()
        {
            var query = DC.Set<BasWRegion>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckEqual(Searcher.manualFlag, x=>x.manualFlag)
                .CheckContain(Searcher.pickupMethod, x=>x.pickupMethod)
                .CheckContain(Searcher.regionName, x=>x.regionName)
                .CheckContain(Searcher.regionNo, x=>x.regionNo)
                .CheckContain(Searcher.regionTypeCode, x=>x.regionTypeCode)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new BasWRegion_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    manualFlag = x.manualFlag,
                    palletMgt = x.palletMgt,
                    pickupMethod = x.pickupMethod,
                    regionName = x.regionName,
                    regionNameAlias = x.regionNameAlias,
                    regionNameEn = x.regionNameEn,
                    regionNo = x.regionNo,
                    regionTypeCode = x.regionTypeCode,
                    sdType = x.sdType,
                    usedFlag = x.usedFlag,
                    virtualFlag = x.virtualFlag,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasWRegion_View : BasWRegion{

    }
}
