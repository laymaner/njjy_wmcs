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


namespace Wish.ViewModel.BasWhouse.BasWRackVMs
{
    public partial class BasWRackListVM : BasePagedListVM<BasWRack_View, BasWRackSearcher>
    {

        protected override IEnumerable<IGridColumn<BasWRack_View>> InitGridHeader()
        {
            return new List<GridColumn<BasWRack_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.isInEnable),
                this.MakeGridHeader(x => x.isOutEnable),
                this.MakeGridHeader(x => x.rackIdx),
                this.MakeGridHeader(x => x.rackName),
                this.MakeGridHeader(x => x.rackNameAlias),
                this.MakeGridHeader(x => x.rackNameEn),
                this.MakeGridHeader(x => x.rackNo),
                this.MakeGridHeader(x => x.regionNo),
                this.MakeGridHeader(x => x.roadwayNo),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeader(x => x.virtualFlag),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeader(x => x.CreateTime),
                this.MakeGridHeader(x => x.CreateBy),
                this.MakeGridHeader(x => x.UpdateBy),
                this.MakeGridHeader(x => x.UpdateTime),
                this.MakeGridHeader(x => x.usedFlagDesc),
                this.MakeGridHeader(x => x.virtualFlagDesc),
                this.MakeGridHeader(x => x.isInEnableDesc),
                this.MakeGridHeader(x => x.isOutEnableDesc),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        public List<SysDictionary> dicEntities = new List<SysDictionary>();
        public override IOrderedQueryable<BasWRack_View> GetSearchQuery()
        {
            var query = DC.Set<BasWRack>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.rackName, x=>x.rackName)
                .CheckContain(Searcher.rackNo, x=>x.rackNo)
                .CheckContain(Searcher.regionNo, x=>x.regionNo)
                .CheckContain(Searcher.roadwayNo, x=>x.roadwayNo)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .CheckEqual(Searcher.virtualFlag, x=>x.virtualFlag)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new BasWRack_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    isInEnable = x.isInEnable,
                    isOutEnable = x.isOutEnable,
                    rackIdx = x.rackIdx,
                    rackName = x.rackName,
                    rackNameAlias = x.rackNameAlias,
                    rackNameEn = x.rackNameEn,
                    rackNo = x.rackNo,
                    regionNo = x.regionNo,
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
                var entityCmd = dicEntities.FirstOrDefault(t => t.dictionaryCode == "IS_IN_ENABLE" && t.dictionaryItemCode == item.isInEnable.ToString());
                if (entityCmd != null)
                {
                    item.isInEnableDesc = entityCmd.dictionaryItemName;
                }
                var entityOut = dicEntities.FirstOrDefault(t => t.dictionaryCode == "IS_OUT_ENABLE" && t.dictionaryItemCode == item.isOutEnable.ToString());
                if (entityOut != null)
                {
                    item.isOutEnableDesc = entityOut.dictionaryItemName;
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

    public class BasWRack_View : BasWRack{
        public string usedFlagDesc { get; set; }
        public string virtualFlagDesc { get; set; }
        public string isInEnableDesc { get; set; }
        public string isOutEnableDesc { get; set; }
    }
}
