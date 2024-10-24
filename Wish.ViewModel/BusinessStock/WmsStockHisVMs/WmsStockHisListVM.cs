using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockHisVMs
{
    public partial class WmsStockHisListVM : BasePagedListVM<WmsStockHis_View, WmsStockHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsStockHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsStockHis_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.binNo),
                this.MakeGridHeader(x => x.errFlag),
                this.MakeGridHeader(x => x.errMsg),
                this.MakeGridHeader(x => x.height),
                this.MakeGridHeader(x => x.invoiceNo),
                this.MakeGridHeader(x => x.loadedType),
                this.MakeGridHeader(x => x.locAllotGroup),
                this.MakeGridHeader(x => x.locNo),
                this.MakeGridHeader(x => x.palletBarcode),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.regionNo),
                this.MakeGridHeader(x => x.roadwayNo),
                this.MakeGridHeader(x => x.specialFlag),
                this.MakeGridHeader(x => x.specialMsg),
                this.MakeGridHeader(x => x.stockCode),
                this.MakeGridHeader(x => x.stockStatus),
                this.MakeGridHeader(x => x.weight),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsStockHis_View> GetSearchQuery()
        {
            var query = DC.Set<WmsStockHis>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.binNo, x=>x.binNo)
                .CheckEqual(Searcher.errFlag, x=>x.errFlag)
                .CheckContain(Searcher.invoiceNo, x=>x.invoiceNo)
                .CheckContain(Searcher.locNo, x=>x.locNo)
                .CheckContain(Searcher.palletBarcode, x=>x.palletBarcode)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.regionNo, x=>x.regionNo)
                .CheckContain(Searcher.roadwayNo, x=>x.roadwayNo)
                .CheckContain(Searcher.stockCode, x=>x.stockCode)
                .CheckEqual(Searcher.stockStatus, x=>x.stockStatus)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new WmsStockHis_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    binNo = x.binNo,
                    errFlag = x.errFlag,
                    errMsg = x.errMsg,
                    height = x.height,
                    invoiceNo = x.invoiceNo,
                    loadedType = x.loadedType,
                    locAllotGroup = x.locAllotGroup,
                    locNo = x.locNo,
                    palletBarcode = x.palletBarcode,
                    proprietorCode = x.proprietorCode,
                    regionNo = x.regionNo,
                    roadwayNo = x.roadwayNo,
                    specialFlag = x.specialFlag,
                    specialMsg = x.specialMsg,
                    stockCode = x.stockCode,
                    stockStatus = x.stockStatus,
                    weight = x.weight,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsStockHis_View : WmsStockHis{

    }
}
