using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.BasWhouse.Model;
using Wish.Model.System;


namespace Wish.ViewModel.BasWhouse.BasWRoadwayVMs
{
    public partial class BasWRoadwayListVM : BasePagedListVM<BasWRoadway_View, BasWRoadwaySearcher>
    {

        protected override IEnumerable<IGridColumn<BasWRoadway_View>> InitGridHeader()
        {
            return new List<GridColumn<BasWRoadway_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.errFlag),
                this.MakeGridHeader(x => x.errMsg),
                this.MakeGridHeader(x => x.regionNo),
                this.MakeGridHeader(x => x.reservedQty),
                this.MakeGridHeader(x => x.roadwayName),
                this.MakeGridHeader(x => x.roadwayNameAlias),
                this.MakeGridHeader(x => x.roadwayNameEn),
                this.MakeGridHeader(x => x.roadwayNo),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeader(x => x.virtualFlag),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeader(x => x.CreateTime),
                this.MakeGridHeader(x => x.CreateBy),
                this.MakeGridHeader(x => x.UpdateBy),
                this.MakeGridHeader(x => x.UpdateTime),
                this.MakeGridHeader(x => x.errFlagDesc),
                this.MakeGridHeader(x => x.usedFlagDesc),
                this.MakeGridHeader(x => x.virtualFlagDesc),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        public List<SysDictionary> dicEntities = new List<SysDictionary>();
        public override IOrderedQueryable<BasWRoadway_View> GetSearchQuery()
        {
            var query = DC.Set<BasWRoadway>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckEqual(Searcher.errFlag, x=>x.errFlag)
                .CheckContain(Searcher.regionNo, x=>x.regionNo)
                .CheckContain(Searcher.roadwayName, x=>x.roadwayName)
                .CheckContain(Searcher.roadwayNo, x=>x.roadwayNo)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .CheckEqual(Searcher.virtualFlag, x=>x.virtualFlag)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new BasWRoadway_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    errFlag = x.errFlag,
                    errMsg = x.errMsg,
                    regionNo = x.regionNo,
                    reservedQty = x.reservedQty,
                    roadwayName = x.roadwayName,
                    roadwayNameAlias = x.roadwayNameAlias,
                    roadwayNameEn = x.roadwayNameEn,
                    roadwayNo = x.roadwayNo,
                    usedFlag = x.usedFlag,
                    virtualFlag = x.virtualFlag,
                    whouseNo = x.whouseNo,
                    CreateBy = x.CreateBy,
                    CreateTime = x.CreateTime,
                    UpdateBy = x.UpdateBy,
                    UpdateTime = x.UpdateTime,
                })
                .OrderBy(x => x.ID).ToList();
            dicEntities = DC.Set<SysDictionary>().AsNoTracking().ToList();
            foreach (var item in query)
            {
                var entityCmd = dicEntities.FirstOrDefault(t => t.dictionaryCode == "ERR_FLAG" && t.dictionaryItemCode == item.errFlag.ToString());
                if (entityCmd != null)
                {
                    item.errFlagDesc = entityCmd.dictionaryItemName;
                }
                var entityUse = dicEntities.FirstOrDefault(t => t.dictionaryCode == "USED_FLAG" && t.dictionaryItemCode == item.usedFlag.ToString());
                if (entityUse != null)
                {
                    item.usedFlagDesc = entityUse.dictionaryItemName;
                }
                var entityVirtual = dicEntities.FirstOrDefault(t => t.dictionaryCode == "VIRTUAL_FLAG" && t.dictionaryItemCode == item.virtualFlag.ToString());
                if (entityVirtual != null)
                {
                    item.virtualFlagDesc = entityVirtual.dictionaryItemName;
                }
            }
            var queryList = query.AsQueryable().OrderBy(x => x.ID);
            return queryList;
        }

    }

    public class BasWRoadway_View : BasWRoadway{
        public string errFlagDesc {  get; set; }
        public string usedFlagDesc {  get; set; }
        public string virtualFlagDesc {  get; set; }
    }
}
