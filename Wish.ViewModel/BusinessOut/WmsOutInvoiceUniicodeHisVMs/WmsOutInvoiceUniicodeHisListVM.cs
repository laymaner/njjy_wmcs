using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceUniicodeHisVMs
{
    public partial class WmsOutInvoiceUniicodeHisListVM : BasePagedListVM<WmsOutInvoiceUniicodeHis_View, WmsOutInvoiceUniicodeHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsOutInvoiceUniicodeHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsOutInvoiceUniicodeHis_View>>{
                this.MakeGridHeader(x => x.allotQty),
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.batchNo),
                this.MakeGridHeader(x => x.dataCode),
                this.MakeGridHeader(x => x.delayReason),
                this.MakeGridHeader(x => x.delayTimes),
                this.MakeGridHeader(x => x.delayToEndDate),
                this.MakeGridHeader(x => x.driedScrapFlag),
                this.MakeGridHeader(x => x.driedTimes),
                this.MakeGridHeader(x => x.erpBinNo),
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
                this.MakeGridHeader(x => x.inspectionResult),
                this.MakeGridHeader(x => x.invoiceDtlId),
                this.MakeGridHeader(x => x.invoiceNo),
                this.MakeGridHeader(x => x.invoiceRecordId),
                this.MakeGridHeader(x => x.leftMslTimes),
                this.MakeGridHeader(x => x.materialName),
                this.MakeGridHeader(x => x.materialCode),
                this.MakeGridHeader(x => x.materialSpec),
                this.MakeGridHeader(x => x.mslGradeCode),
                this.MakeGridHeader(x => x.packageTime),
                this.MakeGridHeader(x => x.palletBarcode),
                this.MakeGridHeader(x => x.pickQty),
                this.MakeGridHeader(x => x.pickTaskNo),
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
                this.MakeGridHeader(x => x.waveNo),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeader(x => x.outBarcode),
                this.MakeGridHeader(x => x.ouniiStatus),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsOutInvoiceUniicodeHis_View> GetSearchQuery()
        {
            var query = DC.Set<WmsOutInvoiceUniicodeHis>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.batchNo, x=>x.batchNo)
                .CheckContain(Searcher.dataCode, x=>x.dataCode)
                .CheckContain(Searcher.erpBinNo, x=>x.erpBinNo)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckBetween(Searcher.expDate?.GetStartTime(), Searcher.expDate?.GetEndTime(), x => x.expDate, includeMax: false)
                .CheckEqual(Searcher.inspectionResult, x=>x.inspectionResult)
                .CheckContain(Searcher.invoiceNo, x=>x.invoiceNo)
                .CheckContain(Searcher.materialName, x=>x.materialName)
                .CheckContain(Searcher.materialCode, x=>x.materialCode)
                .CheckContain(Searcher.palletBarcode, x=>x.palletBarcode)
                .CheckContain(Searcher.projectNo, x=>x.projectNo)
                .CheckContain(Searcher.skuCode, x=>x.skuCode)
                .CheckContain(Searcher.stockCode, x=>x.stockCode)
                .CheckContain(Searcher.supplierCode, x=>x.supplierCode)
                .CheckContain(Searcher.supplierName, x=>x.supplierName)
                .CheckContain(Searcher.uniicode, x=>x.uniicode)
                .CheckContain(Searcher.waveNo, x=>x.waveNo)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .CheckContain(Searcher.outBarcode, x=>x.outBarcode)
                .CheckContain(Searcher.ouniiStatus, x=>x.ouniiStatus)
                .Select(x => new WmsOutInvoiceUniicodeHis_View
                {
				    ID = x.ID,
                    allotQty = x.allotQty,
                    areaNo = x.areaNo,
                    batchNo = x.batchNo,
                    dataCode = x.dataCode,
                    delayReason = x.delayReason,
                    delayTimes = x.delayTimes,
                    delayToEndDate = x.delayToEndDate,
                    driedScrapFlag = x.driedScrapFlag,
                    driedTimes = x.driedTimes,
                    erpBinNo = x.erpBinNo,
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
                    inspectionResult = x.inspectionResult,
                    invoiceDtlId = x.invoiceDtlId,
                    invoiceNo = x.invoiceNo,
                    invoiceRecordId = x.invoiceRecordId,
                    leftMslTimes = x.leftMslTimes,
                    materialName = x.materialName,
                    materialCode = x.materialCode,
                    materialSpec = x.materialSpec,
                    mslGradeCode = x.mslGradeCode,
                    packageTime = x.packageTime,
                    palletBarcode = x.palletBarcode,
                    pickQty = x.pickQty,
                    pickTaskNo = x.pickTaskNo,
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
                    waveNo = x.waveNo,
                    whouseNo = x.whouseNo,
                    outBarcode = x.outBarcode,
                    ouniiStatus = x.ouniiStatus,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsOutInvoiceUniicodeHis_View : WmsOutInvoiceUniicodeHis{

    }
}
