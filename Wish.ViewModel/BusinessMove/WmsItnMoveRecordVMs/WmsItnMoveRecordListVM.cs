using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessMove.WmsItnMoveRecordVMs
{
    public partial class WmsItnMoveRecordListVM : BasePagedListVM<WmsItnMoveRecord_View, WmsItnMoveRecordSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsItnMoveRecord_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsItnMoveRecord_View>>{
                this.MakeGridHeader(x => x.barcodeFlag),
                this.MakeGridHeader(x => x.batchNo),
                this.MakeGridHeader(x => x.confirmQty),
                this.MakeGridHeader(x => x.curLocationNo),
                this.MakeGridHeader(x => x.curLocationType),
                this.MakeGridHeader(x => x.curPalletbarCode),
                this.MakeGridHeader(x => x.curStockCode),
                this.MakeGridHeader(x => x.curStockDtlId),
                this.MakeGridHeader(x => x.docTypeCode),
                this.MakeGridHeader(x => x.frLocationNo),
                this.MakeGridHeader(x => x.frLocationType),
                this.MakeGridHeader(x => x.frPalletBarcode),
                this.MakeGridHeader(x => x.frRegionNo),
                this.MakeGridHeader(x => x.frStockCode),
                this.MakeGridHeader(x => x.frStockDtlId),
                this.MakeGridHeader(x => x.inspectionResult),
                this.MakeGridHeader(x => x.materialCode),
                this.MakeGridHeader(x => x.materialSpec),
                this.MakeGridHeader(x => x.moveNo),
                this.MakeGridHeader(x => x.moveDtlId),
                this.MakeGridHeader(x => x.moveQty),
                this.MakeGridHeader(x => x.moveRecordStatus),
                this.MakeGridHeader(x => x.pickMethod),
                this.MakeGridHeader(x => x.pickType),
                this.MakeGridHeader(x => x.productDate),
                this.MakeGridHeader(x => x.expDate),
                this.MakeGridHeader(x => x.putDownLocNo),
                this.MakeGridHeader(x => x.skuCode),
                this.MakeGridHeader(x => x.stockQty),
                this.MakeGridHeader(x => x.supplierBatchNo),
                this.MakeGridHeader(x => x.supplierCode),
                this.MakeGridHeader(x => x.supplierName),
                this.MakeGridHeader(x => x.supplierNameEn),
                this.MakeGridHeader(x => x.supplierNameAlias),
                this.MakeGridHeader(x => x.supplierType),
                this.MakeGridHeader(x => x.toLocationNo),
                this.MakeGridHeader(x => x.toLocationType),
                this.MakeGridHeader(x => x.toPalletBarcode),
                this.MakeGridHeader(x => x.toRegionNo),
                this.MakeGridHeader(x => x.toStockCode),
                this.MakeGridHeader(x => x.toStockDtlId),
                this.MakeGridHeader(x => x.unitCode),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsItnMoveRecord_View> GetSearchQuery()
        {
            var query = DC.Set<WmsItnMoveRecord>()
                .CheckEqual(Searcher.barcodeFlag, x=>x.barcodeFlag)
                .CheckContain(Searcher.batchNo, x=>x.batchNo)
                .CheckContain(Searcher.curLocationNo, x=>x.curLocationNo)
                .CheckContain(Searcher.curLocationType, x=>x.curLocationType)
                .CheckContain(Searcher.curPalletbarCode, x=>x.curPalletbarCode)
                .CheckContain(Searcher.curStockCode, x=>x.curStockCode)
                .CheckContain(Searcher.docTypeCode, x=>x.docTypeCode)
                .CheckContain(Searcher.frLocationNo, x=>x.frLocationNo)
                .CheckContain(Searcher.frLocationType, x=>x.frLocationType)
                .CheckContain(Searcher.frPalletBarcode, x=>x.frPalletBarcode)
                .CheckContain(Searcher.frRegionNo, x=>x.frRegionNo)
                .CheckContain(Searcher.frStockCode, x=>x.frStockCode)
                .CheckEqual(Searcher.inspectionResult, x=>x.inspectionResult)
                .CheckContain(Searcher.materialCode, x=>x.materialCode)
                .CheckContain(Searcher.moveNo, x=>x.moveNo)
                .CheckEqual(Searcher.moveRecordStatus, x=>x.moveRecordStatus)
                .CheckContain(Searcher.pickMethod, x=>x.pickMethod)
                .CheckContain(Searcher.pickType, x=>x.pickType)
                .CheckContain(Searcher.putDownLocNo, x=>x.putDownLocNo)
                .CheckContain(Searcher.skuCode, x=>x.skuCode)
                .CheckContain(Searcher.supplierBatchNo, x=>x.supplierBatchNo)
                .CheckContain(Searcher.supplierCode, x=>x.supplierCode)
                .CheckContain(Searcher.supplierName, x=>x.supplierName)
                .CheckContain(Searcher.supplierType, x=>x.supplierType)
                .CheckContain(Searcher.toRegionNo, x=>x.toRegionNo)
                .CheckContain(Searcher.toStockCode, x=>x.toStockCode)
                .CheckContain(Searcher.unitCode, x=>x.unitCode)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new WmsItnMoveRecord_View
                {
				    ID = x.ID,
                    barcodeFlag = x.barcodeFlag,
                    batchNo = x.batchNo,
                    confirmQty = x.confirmQty,
                    curLocationNo = x.curLocationNo,
                    curLocationType = x.curLocationType,
                    curPalletbarCode = x.curPalletbarCode,
                    curStockCode = x.curStockCode,
                    curStockDtlId = x.curStockDtlId,
                    docTypeCode = x.docTypeCode,
                    frLocationNo = x.frLocationNo,
                    frLocationType = x.frLocationType,
                    frPalletBarcode = x.frPalletBarcode,
                    frRegionNo = x.frRegionNo,
                    frStockCode = x.frStockCode,
                    frStockDtlId = x.frStockDtlId,
                    inspectionResult = x.inspectionResult,
                    materialCode = x.materialCode,
                    materialSpec = x.materialSpec,
                    moveNo = x.moveNo,
                    moveDtlId = x.moveDtlId,
                    moveQty = x.moveQty,
                    moveRecordStatus = x.moveRecordStatus,
                    pickMethod = x.pickMethod,
                    pickType = x.pickType,
                    productDate = x.productDate,
                    expDate = x.expDate,
                    putDownLocNo = x.putDownLocNo,
                    skuCode = x.skuCode,
                    stockQty = x.stockQty,
                    supplierBatchNo = x.supplierBatchNo,
                    supplierCode = x.supplierCode,
                    supplierName = x.supplierName,
                    supplierNameEn = x.supplierNameEn,
                    supplierNameAlias = x.supplierNameAlias,
                    supplierType = x.supplierType,
                    toLocationNo = x.toLocationNo,
                    toLocationType = x.toLocationType,
                    toPalletBarcode = x.toPalletBarcode,
                    toRegionNo = x.toRegionNo,
                    toStockCode = x.toStockCode,
                    toStockDtlId = x.toStockDtlId,
                    unitCode = x.unitCode,
                    proprietorCode = x.proprietorCode,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsItnMoveRecord_View : WmsItnMoveRecord{

    }
}
