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


namespace Wish.ViewModel.BasWhouse.BasWBinVMs
{
    public partial class BasWBinListVM : BasePagedListVM<BasWBin_View, BasWBinSearcher>
    {

        protected override IEnumerable<IGridColumn<BasWBin_View>> InitGridHeader()
        {
            return new List<GridColumn<BasWBin_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.bearWeight),
                this.MakeGridHeader(x => x.binCol),
                this.MakeGridHeader(x => x.binErrFlag),
                this.MakeGridHeader(x => x.binErrMsg),
                this.MakeGridHeader(x => x.binGroupIdx),
                this.MakeGridHeader(x => x.binGroupNo),
                this.MakeGridHeader(x => x.binHeight),
                this.MakeGridHeader(x => x.binLayer),
                this.MakeGridHeader(x => x.binLength),
                this.MakeGridHeader(x => x.binName),
                this.MakeGridHeader(x => x.binNameAlias),
                this.MakeGridHeader(x => x.binNameEn),
                this.MakeGridHeader(x => x.binNo),
                this.MakeGridHeader(x => x.binPriority),
                this.MakeGridHeader(x => x.binRow),
                this.MakeGridHeader(x => x.binType),
                this.MakeGridHeader(x => x.binWidth),
                this.MakeGridHeader(x => x.capacitySize),
                this.MakeGridHeader(x => x.extensionGroupNo),
                this.MakeGridHeader(x => x.extensionIdx),
                this.MakeGridHeader(x => x.fireFlag),
                this.MakeGridHeader(x => x.isInEnable),
                this.MakeGridHeader(x => x.isOutEnable),
                this.MakeGridHeader(x => x.isValidityPeriod),
                this.MakeGridHeader(x => x.palletDirect),
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
                this.MakeGridHeader(x => x.isOutEnableDesc),
                this.MakeGridHeader(x => x.isInEnableDesc),
                this.MakeGridHeader(x => x.binErrFlagDesc),
                this.MakeGridHeader(x => x.virtualFlagDesc),
                this.MakeGridHeader(x => x.usedFlagDesc),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        public List<SysDictionary> dicEntities = new List<SysDictionary>();
        public override IOrderedQueryable<BasWBin_View> GetSearchQuery()
        {
            var query = DC.Set<BasWBin>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckEqual(Searcher.binCol, x=>x.binCol)
                .CheckContain(Searcher.binErrFlag, x=>x.binErrFlag)
                .CheckEqual(Searcher.binHeight, x=>x.binHeight)
                .CheckEqual(Searcher.binLayer, x=>x.binLayer)
                .CheckEqual(Searcher.binLength, x=>x.binLength)
                .CheckContain(Searcher.binName, x=>x.binName)
                .CheckContain(Searcher.binNo, x=>x.binNo)
                .CheckEqual(Searcher.binRow, x=>x.binRow)
                .CheckContain(Searcher.binType, x=>x.binType)
                .CheckEqual(Searcher.binWidth, x=>x.binWidth)
                .CheckEqual(Searcher.fireFlag, x=>x.fireFlag)
                .CheckContain(Searcher.rackNo, x=>x.rackNo)
                .CheckContain(Searcher.regionNo, x=>x.regionNo)
                .CheckContain(Searcher.roadwayNo, x=>x.roadwayNo)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .CheckEqual(Searcher.virtualFlag, x=>x.virtualFlag)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new BasWBin_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    bearWeight = x.bearWeight,
                    binCol = x.binCol,
                    binErrFlag = x.binErrFlag,
                    binErrMsg = x.binErrMsg,
                    binGroupIdx = x.binGroupIdx,
                    binGroupNo = x.binGroupNo,
                    binHeight = x.binHeight,
                    binLayer = x.binLayer,
                    binLength = x.binLength,
                    binName = x.binName,
                    binNameAlias = x.binNameAlias,
                    binNameEn = x.binNameEn,
                    binNo = x.binNo,
                    binPriority = x.binPriority,
                    binRow = x.binRow,
                    binType = x.binType,
                    binWidth = x.binWidth,
                    capacitySize = x.capacitySize,
                    extensionGroupNo = x.extensionGroupNo,
                    extensionIdx = x.extensionIdx,
                    fireFlag = x.fireFlag,
                    isInEnable = x.isInEnable,
                    isOutEnable = x.isOutEnable,
                    isValidityPeriod = x.isValidityPeriod,
                    palletDirect = x.palletDirect,
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
                var entityCmd = dicEntities.FirstOrDefault(t => t.dictionaryCode == "BIN_ERR_FLAG" && t.dictionaryItemCode == item.binErrFlag.ToString());
                if (entityCmd != null)
                {
                    item.binErrFlagDesc = entityCmd.dictionaryItemName;
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
                var entityIn = dicEntities.FirstOrDefault(t => t.dictionaryCode == "IS_IN_ENABLE" && t.dictionaryItemCode == item.isInEnable.ToString());
                if (entityIn != null)
                {
                    item.isInEnableDesc = entityIn.dictionaryItemName;
                }
                var entityOut = dicEntities.FirstOrDefault(t => t.dictionaryCode == "IS_OUT_ENABLE" && t.dictionaryItemCode == item.isOutEnable.ToString());
                if (entityOut != null)
                {
                    item.isOutEnableDesc = entityOut.dictionaryItemName;
                }
            }
            var queryList = query.AsQueryable().OrderBy(x => x.ID);
            return queryList;
        }

    }

    public class BasWBin_View : BasWBin{
        public string usedFlagDesc { get; set; }
        public string virtualFlagDesc { get; set; }
        public string binErrFlagDesc { get; set; }
        public string isInEnableDesc { get; set; }
        public string isOutEnableDesc { get; set; }
    }
}
