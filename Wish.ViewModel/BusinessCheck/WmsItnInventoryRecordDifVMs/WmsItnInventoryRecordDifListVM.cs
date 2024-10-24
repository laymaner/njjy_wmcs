using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryRecordDifVMs
{
    public partial class WmsItnInventoryRecordDifListVM : BasePagedListVM<WmsItnInventoryRecordDif_View, WmsItnInventoryRecordDifSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsItnInventoryRecordDif_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsItnInventoryRecordDif_View>>{
                this.MakeGridHeader(x => x.batchNo),
                this.MakeGridHeader(x => x.dataCode),
                this.MakeGridHeader(x => x.delayFrozenFlag),
                this.MakeGridHeader(x => x.delayFrozenReason),
                this.MakeGridHeader(x => x.delayReason),
                this.MakeGridHeader(x => x.delayTimes),
                this.MakeGridHeader(x => x.delayToEndDate),
                this.MakeGridHeader(x => x.difQty),
                this.MakeGridHeader(x => x.driedScrapFlag),
                this.MakeGridHeader(x => x.driedTimes),
                this.MakeGridHeader(x => x.expDate),
                this.MakeGridHeader(x => x.exposeFrozenFlag),
                this.MakeGridHeader(x => x.exposeFrozenReason),
                this.MakeGridHeader(x => x.extend1),
                this.MakeGridHeader(x => x.extend10),
                this.MakeGridHeader(x => x.extend11),
                this.MakeGridHeader(x => x.extend12),
                this.MakeGridHeader(x => x.extend13),
                this.MakeGridHeader(x => x.extend14),
                this.MakeGridHeader(x => x.extend15),
                this.MakeGridHeader(x => x.extend2),
                this.MakeGridHeader(x => x.extend3),
                this.MakeGridHeader(x => x.extend4),
                this.MakeGridHeader(x => x.extend5),
                this.MakeGridHeader(x => x.extend6),
                this.MakeGridHeader(x => x.extend7),
                this.MakeGridHeader(x => x.extend8),
                this.MakeGridHeader(x => x.extend9),
                this.MakeGridHeader(x => x.inspectionResult),
                this.MakeGridHeader(x => x.inventoryDtlId),
                this.MakeGridHeader(x => x.inventoryNo),
                this.MakeGridHeader(x => x.inventoryQty),
                this.MakeGridHeader(x => x.inventoryRecordId),
                this.MakeGridHeader(x => x.leftMslTimes),
                this.MakeGridHeader(x => x.materialName),
                this.MakeGridHeader(x => x.materialCode),
                this.MakeGridHeader(x => x.materialSpec),
                this.MakeGridHeader(x => x.mslGradeCode),
                this.MakeGridHeader(x => x.packageTime),
                this.MakeGridHeader(x => x.palletBarcode),
                this.MakeGridHeader(x => x.positionNo),
                this.MakeGridHeader(x => x.productDate),
                this.MakeGridHeader(x => x.projectNo),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.realExposeTimes),
                this.MakeGridHeader(x => x.skuCode),
                this.MakeGridHeader(x => x.stockCode),
                this.MakeGridHeader(x => x.stockDtlId),
                this.MakeGridHeader(x => x.supplierCode),
                this.MakeGridHeader(x => x.supplierExposeTimes),
                this.MakeGridHeader(x => x.supplierName),
                this.MakeGridHeader(x => x.supplierNameAlias),
                this.MakeGridHeader(x => x.supplierNameEn),
                this.MakeGridHeader(x => x.uniicode),
                this.MakeGridHeader(x => x.unpackTime),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.delFlag),
                this.MakeGridHeader(x => x.erpWhouseNo),
                this.MakeGridHeader(x => x.inwhTime),
                this.MakeGridHeader(x => x.occupyQty),
                this.MakeGridHeader(x => x.qty),
                this.MakeGridHeader(x => x.unitCode),
                this.MakeGridHeader(x => x.fileedId),
                this.MakeGridHeader(x => x.fileedName),
                this.MakeGridHeader(x => x.oldStockDtlId),
                this.MakeGridHeader(x => x.projectNoBak),
                this.MakeGridHeader(x => x.unpackStatus),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsItnInventoryRecordDif_View> GetSearchQuery()
        {
            var query = DC.Set<WmsItnInventoryRecordDif>()
                .CheckContain(Searcher.batchNo, x=>x.batchNo)
                .CheckContain(Searcher.dataCode, x=>x.dataCode)
                .CheckEqual(Searcher.delayFrozenFlag, x=>x.delayFrozenFlag)
                .CheckEqual(Searcher.delayTimes, x=>x.delayTimes)
                .CheckEqual(Searcher.difQty, x=>x.difQty)
                .CheckEqual(Searcher.driedScrapFlag, x=>x.driedScrapFlag)
                .CheckEqual(Searcher.exposeFrozenFlag, x=>x.exposeFrozenFlag)
                .CheckContain(Searcher.exposeFrozenReason, x=>x.exposeFrozenReason)
                .CheckEqual(Searcher.inspectionResult, x=>x.inspectionResult)
                .CheckContain(Searcher.inventoryDtlId, x=>x.inventoryDtlId)
                .CheckContain(Searcher.inventoryNo, x=>x.inventoryNo)
                .CheckEqual(Searcher.inventoryQty, x=>x.inventoryQty)
                .CheckContain(Searcher.materialName, x=>x.materialName)
                .CheckContain(Searcher.materialCode, x=>x.materialCode)
                .CheckContain(Searcher.mslGradeCode, x=>x.mslGradeCode)
                .CheckContain(Searcher.palletBarcode, x=>x.palletBarcode)
                .CheckContain(Searcher.positionNo, x=>x.positionNo)
                .CheckContain(Searcher.projectNo, x=>x.projectNo)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.skuCode, x=>x.skuCode)
                .CheckContain(Searcher.stockCode, x=>x.stockCode)
                .CheckContain(Searcher.supplierCode, x=>x.supplierCode)
                .CheckContain(Searcher.supplierName, x=>x.supplierName)
                .CheckContain(Searcher.uniicode, x=>x.uniicode)
                .CheckBetween(Searcher.unpackTime?.GetStartTime(), Searcher.unpackTime?.GetEndTime(), x => x.unpackTime, includeMax: false)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.delFlag, x=>x.delFlag)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckContain(Searcher.unitCode, x=>x.unitCode)
                .CheckContain(Searcher.projectNoBak, x=>x.projectNoBak)
                .CheckEqual(Searcher.unpackStatus, x=>x.unpackStatus)
                .Select(x => new WmsItnInventoryRecordDif_View
                {
				    ID = x.ID,
                    batchNo = x.batchNo,
                    dataCode = x.dataCode,
                    delayFrozenFlag = x.delayFrozenFlag,
                    delayFrozenReason = x.delayFrozenReason,
                    delayReason = x.delayReason,
                    delayTimes = x.delayTimes,
                    delayToEndDate = x.delayToEndDate,
                    difQty = x.difQty,
                    driedScrapFlag = x.driedScrapFlag,
                    driedTimes = x.driedTimes,
                    expDate = x.expDate,
                    exposeFrozenFlag = x.exposeFrozenFlag,
                    exposeFrozenReason = x.exposeFrozenReason,
                    extend1 = x.extend1,
                    extend10 = x.extend10,
                    extend11 = x.extend11,
                    extend12 = x.extend12,
                    extend13 = x.extend13,
                    extend14 = x.extend14,
                    extend15 = x.extend15,
                    extend2 = x.extend2,
                    extend3 = x.extend3,
                    extend4 = x.extend4,
                    extend5 = x.extend5,
                    extend6 = x.extend6,
                    extend7 = x.extend7,
                    extend8 = x.extend8,
                    extend9 = x.extend9,
                    inspectionResult = x.inspectionResult,
                    inventoryDtlId = x.inventoryDtlId,
                    inventoryNo = x.inventoryNo,
                    inventoryQty = x.inventoryQty,
                    inventoryRecordId = x.inventoryRecordId,
                    leftMslTimes = x.leftMslTimes,
                    materialName = x.materialName,
                    materialCode = x.materialCode,
                    materialSpec = x.materialSpec,
                    mslGradeCode = x.mslGradeCode,
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
                    supplierCode = x.supplierCode,
                    supplierExposeTimes = x.supplierExposeTimes,
                    supplierName = x.supplierName,
                    supplierNameAlias = x.supplierNameAlias,
                    supplierNameEn = x.supplierNameEn,
                    uniicode = x.uniicode,
                    unpackTime = x.unpackTime,
                    whouseNo = x.whouseNo,
                    areaNo = x.areaNo,
                    delFlag = x.delFlag,
                    erpWhouseNo = x.erpWhouseNo,
                    inwhTime = x.inwhTime,
                    occupyQty = x.occupyQty,
                    qty = x.qty,
                    unitCode = x.unitCode,
                    fileedId = x.fileedId,
                    fileedName = x.fileedName,
                    oldStockDtlId = x.oldStockDtlId,
                    projectNoBak = x.projectNoBak,
                    unpackStatus = x.unpackStatus,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsItnInventoryRecordDif_View : WmsItnInventoryRecordDif{

    }
}
