using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Model.DevConfig;


namespace Wish.ViewModel.DevConfig.MotDevPartsVMs
{
    public partial class MotDevPartsListVM : BasePagedListVM<MotDevParts_View, MotDevPartsSearcher>
    {

        protected override IEnumerable<IGridColumn<MotDevParts_View>> InitGridHeader()
        {
            return new List<GridColumn<MotDevParts_View>>{
                this.MakeGridHeader(x => x.AreaNo),
                this.MakeGridHeader(x => x.DevNo),
                this.MakeGridHeader(x => x.PartNo),
                this.MakeGridHeader(x => x.PartLocNo),
                this.MakeGridHeader(x => x.DevRunMode),
                this.MakeGridHeader(x => x.SrmRoadway),
                this.MakeGridHeader(x => x.SrmForkType),
                this.MakeGridHeader(x => x.SrmExecStep),
                this.MakeGridHeader(x => x.IsInSitu),
                this.MakeGridHeader(x => x.IsAlarming),
                this.MakeGridHeader(x => x.AlarmCode),
                this.MakeGridHeader(x => x.IsFree),
                this.MakeGridHeader(x => x.IsHasGoods),
                this.MakeGridHeader(x => x.CmdNo),
                this.MakeGridHeader(x => x.PalletNo),
                this.MakeGridHeader(x => x.OldPalletNo),
                this.MakeGridHeader(x => x.ReadPalletNo),
                this.MakeGridHeader(x => x.StationNo),
                this.MakeGridHeader(x => x.CurrentX),
                this.MakeGridHeader(x => x.CurrentY),
                this.MakeGridHeader(x => x.CurrentZ),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<MotDevParts_View> GetSearchQuery()
        {
            var query = DC.Set<MotDevParts>()
                .CheckContain(Searcher.DevNo, x=>x.DevNo)
                .CheckContain(Searcher.PartNo, x=>x.PartNo)
                .CheckContain(Searcher.PartLocNo, x=>x.PartLocNo)
                .CheckEqual(Searcher.DevRunMode, x=>x.DevRunMode)
                .CheckContain(Searcher.SrmRoadway, x=>x.SrmRoadway)
                .CheckEqual(Searcher.SrmExecStep, x=>x.SrmExecStep)
                .CheckEqual(Searcher.IsAlarming, x=>x.IsAlarming)
                .CheckContain(Searcher.AlarmCode, x=>x.AlarmCode)
                .CheckEqual(Searcher.IsFree, x=>x.IsFree)
                .CheckEqual(Searcher.IsHasGoods, x=>x.IsHasGoods)
                .CheckContain(Searcher.CmdNo, x=>x.CmdNo)
                .CheckContain(Searcher.PalletNo, x=>x.PalletNo)
                .CheckContain(Searcher.ReadPalletNo, x=>x.ReadPalletNo)
                .CheckEqual(Searcher.CurrentX, x=>x.CurrentX)
                .CheckEqual(Searcher.CurrentY, x=>x.CurrentY)
                .CheckEqual(Searcher.CurrentZ, x=>x.CurrentZ)
                .Select(x => new MotDevParts_View
                {
				    ID = x.ID,
                    AreaNo = x.AreaNo,
                    DevNo = x.DevNo,
                    PartNo = x.PartNo,
                    PartLocNo = x.PartLocNo,
                    DevRunMode = x.DevRunMode,
                    SrmRoadway = x.SrmRoadway,
                    SrmForkType = x.SrmForkType,
                    SrmExecStep = x.SrmExecStep,
                    IsInSitu = x.IsInSitu,
                    IsAlarming = x.IsAlarming,
                    AlarmCode = x.AlarmCode,
                    IsFree = x.IsFree,
                    IsHasGoods = x.IsHasGoods,
                    CmdNo = x.CmdNo,
                    PalletNo = x.PalletNo,
                    OldPalletNo = x.OldPalletNo,
                    ReadPalletNo = x.ReadPalletNo,
                    StationNo = x.StationNo,
                    CurrentX = x.CurrentX,
                    CurrentY = x.CurrentY,
                    CurrentZ = x.CurrentZ,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class MotDevParts_View : MotDevParts{

    }
}
