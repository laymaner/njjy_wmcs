using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessMove.WmsItnMoveDtlHisVMs
{
    public partial class WmsItnMoveDtlHisListVM : BasePagedListVM<WmsItnMoveDtlHis_View, WmsItnMoveDtlHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsItnMoveDtlHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsItnMoveDtlHis_View>>{
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.moveNo),
                this.MakeGridHeader(x => x.regionNo),
                this.MakeGridHeader(x => x.roadwayNo),
                this.MakeGridHeader(x => x.binNo),
                this.MakeGridHeader(x => x.stockCode),
                this.MakeGridHeader(x => x.palletBarcode),
                this.MakeGridHeader(x => x.moveDtlStatus),
                this.MakeGridHeader(x => x.loadedType),
                this.MakeGridHeader(x => x.moveQty),
                this.MakeGridHeader(x => x.createBy),
                this.MakeGridHeader(x => x.createTime),
                this.MakeGridHeader(x => x.updateBy),
                this.MakeGridHeader(x => x.updateTime),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsItnMoveDtlHis_View> GetSearchQuery()
        {
            var query = DC.Set<WmsItnMoveDtlHis>()
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.moveNo, x=>x.moveNo)
                .CheckContain(Searcher.regionNo, x=>x.regionNo)
                .CheckContain(Searcher.roadwayNo, x=>x.roadwayNo)
                .CheckContain(Searcher.binNo, x=>x.binNo)
                .CheckContain(Searcher.stockCode, x=>x.stockCode)
                .CheckContain(Searcher.palletBarcode, x=>x.palletBarcode)
                .CheckEqual(Searcher.moveDtlStatus, x=>x.moveDtlStatus)
                .CheckEqual(Searcher.loadedType, x=>x.loadedType)
                .Select(x => new WmsItnMoveDtlHis_View
                {
				    ID = x.ID,
                    whouseNo = x.whouseNo,
                    proprietorCode = x.proprietorCode,
                    areaNo = x.areaNo,
                    moveNo = x.moveNo,
                    regionNo = x.regionNo,
                    roadwayNo = x.roadwayNo,
                    binNo = x.binNo,
                    stockCode = x.stockCode,
                    palletBarcode = x.palletBarcode,
                    moveDtlStatus = x.moveDtlStatus,
                    loadedType = x.loadedType,
                    moveQty = x.moveQty,
                    createBy = x.createBy,
                    createTime = x.createTime,
                    updateBy = x.updateBy,
                    updateTime = x.updateTime,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsItnMoveDtlHis_View : WmsItnMoveDtlHis{

    }
}
