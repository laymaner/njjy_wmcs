using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessIn.WmsInReceiptUniicodeHisVMs
{
    public partial class WmsInReceiptUniicodeHisListVM : BasePagedListVM<WmsInReceiptUniicodeHis_View, WmsInReceiptUniicodeHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsInReceiptUniicodeHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsInReceiptUniicodeHis_View>>{
                this.MakeGridHeader(x => x.batchNo),
                this.MakeGridHeader(x => x.curPalletBarcode),
                this.MakeGridHeader(x => x.curPositionNo),
                this.MakeGridHeader(x => x.curStockCode),
                this.MakeGridHeader(x => x.curStockDtlId),
                this.MakeGridHeader(x => x.dataCode),
                this.MakeGridHeader(x => x.erpWhouseNo),
                this.MakeGridHeader(x => x.expDate),
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
                this.MakeGridHeader(x => x.externalInDtlId),
                this.MakeGridHeader(x => x.externalInNo),
                this.MakeGridHeader(x => x.inDtlId),
                this.MakeGridHeader(x => x.inNo),
                this.MakeGridHeader(x => x.iqcResultNo),
                this.MakeGridHeader(x => x.materialName),
                this.MakeGridHeader(x => x.materialCode),
                this.MakeGridHeader(x => x.materialSpec),
                this.MakeGridHeader(x => x.mslGradeCode),
                this.MakeGridHeader(x => x.productDate),
                this.MakeGridHeader(x => x.projectNo),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.receiptDtlId),
                this.MakeGridHeader(x => x.receiptNo),
                this.MakeGridHeader(x => x.qty),
                this.MakeGridHeader(x => x.recordQty),
                this.MakeGridHeader(x => x.receiptRecordId),
                this.MakeGridHeader(x => x.supplierExposeTimeDuration),
                this.MakeGridHeader(x => x.supplierName),
                this.MakeGridHeader(x => x.supplierNameAlias),
                this.MakeGridHeader(x => x.supplierNameEn),
                this.MakeGridHeader(x => x.supplierCode),
                this.MakeGridHeader(x => x.skuCode),
                this.MakeGridHeader(x => x.uniicode),
                this.MakeGridHeader(x => x.unitCode),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeader(x => x.runiiStatus),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsInReceiptUniicodeHis_View> GetSearchQuery()
        {
            var query = DC.Set<WmsInReceiptUniicodeHis>()
                .CheckContain(Searcher.batchNo, x=>x.batchNo)
                .CheckContain(Searcher.curPalletBarcode, x=>x.curPalletBarcode)
                .CheckContain(Searcher.curPositionNo, x=>x.curPositionNo)
                .CheckContain(Searcher.curStockCode, x=>x.curStockCode)
                .CheckContain(Searcher.curStockDtlId, x=>x.curStockDtlId)
                .CheckContain(Searcher.dataCode, x=>x.dataCode)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckBetween(Searcher.expDate?.GetStartTime(), Searcher.expDate?.GetEndTime(), x => x.expDate, includeMax: false)
                .CheckContain(Searcher.externalInNo, x=>x.externalInNo)
                .CheckContain(Searcher.inNo, x=>x.inNo)
                .CheckContain(Searcher.iqcResultNo, x=>x.iqcResultNo)
                .CheckContain(Searcher.materialName, x=>x.materialName)
                .CheckContain(Searcher.materialCode, x=>x.materialCode)
                .CheckBetween(Searcher.productDate?.GetStartTime(), Searcher.productDate?.GetEndTime(), x => x.productDate, includeMax: false)
                .CheckContain(Searcher.projectNo, x=>x.projectNo)
                .CheckContain(Searcher.receiptNo, x=>x.receiptNo)
                .CheckEqual(Searcher.qty, x=>x.qty)
                .CheckContain(Searcher.uniicode, x=>x.uniicode)
                .CheckContain(Searcher.unitCode, x=>x.unitCode)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .CheckEqual(Searcher.runiiStatus, x=>x.runiiStatus)
                .Select(x => new WmsInReceiptUniicodeHis_View
                {
				    ID = x.ID,
                    batchNo = x.batchNo,
                    curPalletBarcode = x.curPalletBarcode,
                    curPositionNo = x.curPositionNo,
                    curStockCode = x.curStockCode,
                    curStockDtlId = x.curStockDtlId,
                    dataCode = x.dataCode,
                    erpWhouseNo = x.erpWhouseNo,
                    expDate = x.expDate,
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
                    externalInDtlId = x.externalInDtlId,
                    externalInNo = x.externalInNo,
                    inDtlId = x.inDtlId,
                    inNo = x.inNo,
                    iqcResultNo = x.iqcResultNo,
                    materialName = x.materialName,
                    materialCode = x.materialCode,
                    materialSpec = x.materialSpec,
                    mslGradeCode = x.mslGradeCode,
                    productDate = x.productDate,
                    projectNo = x.projectNo,
                    proprietorCode = x.proprietorCode,
                    receiptDtlId = x.receiptDtlId,
                    receiptNo = x.receiptNo,
                    qty = x.qty,
                    recordQty = x.recordQty,
                    receiptRecordId = x.receiptRecordId,
                    supplierExposeTimeDuration = x.supplierExposeTimeDuration,
                    supplierName = x.supplierName,
                    supplierNameAlias = x.supplierNameAlias,
                    supplierNameEn = x.supplierNameEn,
                    supplierCode = x.supplierCode,
                    skuCode = x.skuCode,
                    uniicode = x.uniicode,
                    unitCode = x.unitCode,
                    whouseNo = x.whouseNo,
                    runiiStatus = x.runiiStatus,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsInReceiptUniicodeHis_View : WmsInReceiptUniicodeHis{

    }
}
