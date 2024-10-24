using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;
using Wish.Model.System;
using Wish.ViewModel.BusinessStock.WmsStockUniicodeVMs;


namespace Wish.ViewModel.BusinessStock.WmsStockUniicodeHisVMs
{
    public partial class WmsStockUniicodeHisListVM : BasePagedListVM<WmsStockUniicodeHis_View, WmsStockUniicodeHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsStockUniicodeHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsStockUniicodeHis_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.uniicode),
                this.MakeGridHeader(x => x.supplierCode),
                this.MakeGridHeader(x => x.projectNo),
                this.MakeGridHeader(x => x.batchNo),
                this.MakeGridHeader(x => x.binNo),
                this.MakeGridHeader(x => x.stockStatusDesc),
                this.MakeGridHeader(x => x.unpackStatusDesc),
                //this.MakeGridHeader(x => x.dataCode),
                //this.MakeGridHeader(x => x.delayFrozenFlag),
                //this.MakeGridHeader(x => x.delayFrozenReason),
                //this.MakeGridHeader(x => x.delayReason),
                //this.MakeGridHeader(x => x.delayTimes),
                this.MakeGridHeader(x => x.delayToEndDate),
                //this.MakeGridHeader(x => x.driedScrapFlag),
                //this.MakeGridHeader(x => x.driedTimes),
                //this.MakeGridHeader(x => x.erpWhouseNo),
                this.MakeGridHeader(x => x.expDate),
                //this.MakeGridHeader(x => x.exposeFrozenFlag),
                //this.MakeGridHeader(x => x.exposeFrozenReason),
                //this.MakeGridHeader(x => x.extend1),
                //this.MakeGridHeader(x => x.extend10),
                //this.MakeGridHeader(x => x.extend11),
                this.MakeGridHeader(x => x.chipSize),
                this.MakeGridHeader(x => x.chipThickness),
                this.MakeGridHeader(x => x.chipModel),
                this.MakeGridHeader(x => x.dafType),
                //this.MakeGridHeader(x => x.extend2),
                //this.MakeGridHeader(x => x.extend3),
                //this.MakeGridHeader(x => x.extend4),
                //this.MakeGridHeader(x => x.extend5),
                //this.MakeGridHeader(x => x.extend6),
                //this.MakeGridHeader(x => x.extend7),
                //this.MakeGridHeader(x => x.extend8),
                //this.MakeGridHeader(x => x.extend9),
                //this.MakeGridHeader(x => x.inspectionResult),
                this.MakeGridHeader(x => x.inwhTime),
                //this.MakeGridHeader(x => x.leftMslTimes),
                this.MakeGridHeader(x => x.materialName),
                this.MakeGridHeader(x => x.materialCode),
                this.MakeGridHeader(x => x.materialSpec),
                //this.MakeGridHeader(x => x.mslGradeCode),
                this.MakeGridHeader(x => x.qty),
                this.MakeGridHeader(x => x.occupyQty),
                //this.MakeGridHeader(x => x.packageTime),
                this.MakeGridHeader(x => x.palletBarcode),
                //this.MakeGridHeader(x => x.positionNo),
                //this.MakeGridHeader(x => x.productDate),
                //this.MakeGridHeader(x => x.proprietorCode),
                //this.MakeGridHeader(x => x.realExposeTimes),
                //this.MakeGridHeader(x => x.skuCode),
                this.MakeGridHeader(x => x.stockCode),
                this.MakeGridHeader(x => x.stockDtlId),
                //this.MakeGridHeader(x => x.supplierExposeTimes),
                //this.MakeGridHeader(x => x.supplierName),
                //this.MakeGridHeader(x => x.supplierNameAlias),
                //this.MakeGridHeader(x => x.supplierNameEn),
                this.MakeGridHeader(x => x.stockStatus),
                this.MakeGridHeader(x => x.unpackStatus),
                //this.MakeGridHeader(x => x.unpackTime),
                this.MakeGridHeader(x => x.unitCode),
                //this.MakeGridHeader(x => x.whouseNo),
                //this.MakeGridHeader(x => x.fileedId),
                //this.MakeGridHeader(x => x.fileedName),
                //this.MakeGridHeader(x => x.projectNoBak),
                this.MakeGridHeader(x => x.CreateTime),
                this.MakeGridHeader(x => x.CreateBy),
                this.MakeGridHeader(x => x.UpdateBy),
                this.MakeGridHeader(x => x.UpdateTime),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        public List<SysDictionary> dicEntities = new List<SysDictionary>();
        public override IOrderedQueryable<WmsStockUniicodeHis_View> GetSearchQuery()
        {
            var query = (from x in DC.Set<WmsStockUniicodeHis>()
                         join stock in DC.Set<WmsStockHis>() on x.stockCode equals stock.stockCode into stockTemp
                         from stock in stockTemp.DefaultIfEmpty()
                         select new WmsStockUniicodeHis_View
                         {
                             ID = x.ID,
                             areaNo = x.areaNo,
                             batchNo = x.batchNo,
                             dataCode = x.dataCode,
                             delayFrozenFlag = x.delayFrozenFlag,
                             delayFrozenReason = x.delayFrozenReason,
                             delayReason = x.delayReason,
                             delayTimes = x.delayTimes,
                             delayToEndDate = x.delayToEndDate,
                             driedScrapFlag = x.driedScrapFlag,
                             driedTimes = x.driedTimes,
                             erpWhouseNo = x.erpWhouseNo,
                             expDate = x.expDate,
                             exposeFrozenFlag = x.exposeFrozenFlag,
                             exposeFrozenReason = x.exposeFrozenReason,
                             extend1 = x.extend1,
                             extend10 = x.extend10,
                             extend11 = x.extend11,
                             chipSize = x.chipSize,
                             chipThickness = x.chipThickness,
                             chipModel = x.chipModel,
                             dafType = x.dafType,
                             extend2 = x.extend2,
                             extend3 = x.extend3,
                             extend4 = x.extend4,
                             extend5 = x.extend5,
                             extend6 = x.extend6,
                             extend7 = x.extend7,
                             extend8 = x.extend8,
                             extend9 = x.extend9,
                             inspectionResult = x.inspectionResult,
                             inwhTime = x.inwhTime,
                             leftMslTimes = x.leftMslTimes,
                             materialName = x.materialName,
                             materialCode = x.materialCode,
                             materialSpec = x.materialSpec,
                             mslGradeCode = x.mslGradeCode,
                             occupyQty = x.occupyQty,
                             packageTime = x.packageTime,
                             palletBarcode = x.palletBarcode,
                             positionNo = x.positionNo,
                             productDate = x.productDate,
                             projectNo = x.projectNo,
                             proprietorCode = x.proprietorCode,
                             realExposeTimes = x.realExposeTimes,
                             skuCode = x.skuCode,
                             stockCode = x.stockCode,
                             stockDtlId = x.stockDtlId,
                             qty = x.qty,
                             supplierCode = x.supplierCode,
                             supplierExposeTimes = x.supplierExposeTimes,
                             supplierName = x.supplierName,
                             supplierNameAlias = x.supplierNameAlias,
                             supplierNameEn = x.supplierNameEn,
                             uniicode = x.uniicode,
                             unpackStatus = x.unpackStatus,
                             unpackTime = x.unpackTime,
                             unitCode = x.unitCode,
                             whouseNo = x.whouseNo,
                             fileedId = x.fileedId,
                             fileedName = x.fileedName,
                             projectNoBak = x.projectNoBak,
                             CreateBy = x.CreateBy,
                             CreateTime = x.CreateTime,
                             UpdateBy = x.UpdateBy,
                             UpdateTime = x.UpdateTime,
                             binNo = stock.binNo,
                             stockStatus = stock.stockStatus
                         })
                       .CheckContain(Searcher.areaNo, x => x.areaNo)
                       .CheckContain(Searcher.batchNo, x => x.batchNo)
                       .CheckEqual(Searcher.delayFrozenFlag, x => x.delayFrozenFlag)
                       .CheckEqual(Searcher.unpackStatus, x => x.unpackStatus)
                       .CheckContain(Searcher.erpWhouseNo, x => x.erpWhouseNo)
                       //.CheckEqual(Searcher.exposeFrozenFlag, x => x.exposeFrozenFlag)
                       .CheckEqual(Searcher.stockStatus, x => x.stockStatus)
                       .CheckContain(Searcher.binNo, x => x.binNo)
                       .CheckContain(Searcher.materialCode, x => x.materialCode)
                       //.CheckContain(Searcher.materialSpec, x => x.materialSpec)
                       .CheckContain(Searcher.palletBarcode, x => x.palletBarcode)
                       .CheckContain(Searcher.projectNo, x => x.projectNo)
                       .CheckContain(Searcher.skuCode, x => x.skuCode)
                       .CheckContain(Searcher.stockCode, x => x.stockCode)
                       .CheckContain(Searcher.supplierCode, x => x.supplierCode)
                       .CheckContain(Searcher.uniicode, x => x.uniicode)
                       .CheckContain(Searcher.unitCode, x => x.unitCode)
                       .CheckContain(Searcher.whouseNo, x => x.whouseNo)
                       .OrderBy(x => x.ID).ToList();
            dicEntities = DC.Set<SysDictionary>().AsNoTracking().ToList();
            foreach (var item in query)
            {
                var entityCmd = dicEntities.FirstOrDefault(t => t.dictionaryCode == "STOCK_STATUS" && t.dictionaryItemCode == item.stockStatus.ToString());
                if (entityCmd != null)
                {
                    item.stockStatusDesc = entityCmd.dictionaryItemName;
                }
                var entityOrder = dicEntities.FirstOrDefault(t => t.dictionaryCode == "UNPACK_FLAG" && t.dictionaryItemCode == item.unpackStatus.ToString());
                if (entityOrder != null)
                {
                    item.unpackStatusDesc = entityOrder.dictionaryItemName;
                }
            }
            var queryList = query.AsQueryable().OrderByDescending(x => x.UpdateTime).ThenByDescending(x=>x.CreateTime);
            return queryList;
        
        }

    }

    public class WmsStockUniicodeHis_View : WmsStockUniicodeHis{
        [Display(Name = "货位")]
        public string binNo { get; set; }
        [Display(Name = "库存状态")]
        public int stockStatus { get; set; }
        [Display(Name = "库存状态（含义）")]
        public string stockStatusDesc { get; set; }
        [Display(Name = "是否下单（含义）")]
        public string unpackStatusDesc { get; set; }
    }
}
