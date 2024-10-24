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


namespace Wish.ViewModel.BusinessIn.WmsInReceiptRecordHisVMs
{
    public partial class WmsInReceiptRecordHisListVM : BasePagedListVM<WmsInReceiptRecordHis_View, WmsInReceiptRecordHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsInReceiptRecordHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsInReceiptRecordHis_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.batchNo),
                this.MakeGridHeader(x => x.binNo),
                this.MakeGridHeader(x => x.departmentName),
                this.MakeGridHeader(x => x.docTypeCode),
                this.MakeGridHeader(x => x.erpWhouseNo),
                this.MakeGridHeader(x => x.externalInDtlId),
                this.MakeGridHeader(x => x.externalInNo),
                this.MakeGridHeader(x => x.inDtlId),
                this.MakeGridHeader(x => x.inNo),
                this.MakeGridHeader(x => x.inOutName),
                this.MakeGridHeader(x => x.inOutTypeNo),
                this.MakeGridHeader(x => x.inspectionResult),
                this.MakeGridHeader(x => x.inspector),
                this.MakeGridHeader(x => x.iqcResultNo),
                this.MakeGridHeader(x => x.inRecordStatus),
                this.MakeGridHeader(x => x.loadedType),
                this.MakeGridHeader(x => x.materialName),
                this.MakeGridHeader(x => x.materialCode),
                this.MakeGridHeader(x => x.materialSpec),
                this.MakeGridHeader(x => x.orderDtlId),
                this.MakeGridHeader(x => x.orderNo),
                this.MakeGridHeader(x => x.palletBarcode),
                this.MakeGridHeader(x => x.ptaRegionNo),
                this.MakeGridHeader(x => x.ptaStockCode),
                this.MakeGridHeader(x => x.ptaStockDtlId),
                this.MakeGridHeader(x => x.minPkgQty),
                this.MakeGridHeader(x => x.projectNo),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.ptaBinNo),
                this.MakeGridHeader(x => x.ptaPalletBarcode),
                this.MakeGridHeader(x => x.receiptDtlId),
                this.MakeGridHeader(x => x.receiptNo),
                this.MakeGridHeader(x => x.recordQty),
                this.MakeGridHeader(x => x.regionNo),
                this.MakeGridHeader(x => x.replenishFlag),
                this.MakeGridHeader(x => x.returnFlag),
                this.MakeGridHeader(x => x.returnResult),
                this.MakeGridHeader(x => x.returnTime),
                this.MakeGridHeader(x => x.skuCode),
                this.MakeGridHeader(x => x.sourceBy),
                this.MakeGridHeader(x => x.stockCode),
                this.MakeGridHeader(x => x.stockDtlId),
                this.MakeGridHeader(x => x.supplierCode),
                this.MakeGridHeader(x => x.supplierName),
                this.MakeGridHeader(x => x.supplierNameAlias),
                this.MakeGridHeader(x => x.supplierNameEn),
                this.MakeGridHeader(x => x.ticketNo),
                this.MakeGridHeader(x => x.urgentFlag),
                this.MakeGridHeader(x => x.unitCode),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeader(x => x.UpdateBy),
                this.MakeGridHeader(x => x.UpdateTime),
                this.MakeGridHeader(x => x.CreateTime),
                this.MakeGridHeader(x => x.CreateBy),
                this.MakeGridHeader(x => x.inRecordStatusDesc),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        public List<SysDictionary> dicEntities = new List<SysDictionary>();
        public override IOrderedQueryable<WmsInReceiptRecordHis_View> GetSearchQuery()
        {
            var query = DC.Set<WmsInReceiptRecordHis>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.batchNo, x=>x.batchNo)
                .CheckContain(Searcher.binNo, x=>x.binNo)
                .CheckContain(Searcher.departmentName, x=>x.departmentName)
                .CheckContain(Searcher.docTypeCode, x=>x.docTypeCode)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckContain(Searcher.externalInNo, x=>x.externalInNo)
                .CheckContain(Searcher.inNo, x=>x.inNo)
                .CheckContain(Searcher.iqcResultNo, x=>x.iqcResultNo)
                .CheckEqual(Searcher.inRecordStatus, x=>x.inRecordStatus)
                .CheckContain(Searcher.materialName, x=>x.materialName)
                .CheckContain(Searcher.materialCode, x=>x.materialCode)
                .CheckContain(Searcher.palletBarcode, x=>x.palletBarcode)
                .CheckContain(Searcher.ptaRegionNo, x=>x.ptaRegionNo)
                .CheckContain(Searcher.ptaBinNo, x=>x.ptaBinNo)
                .CheckContain(Searcher.ptaPalletBarcode, x=>x.ptaPalletBarcode)
                .CheckContain(Searcher.receiptNo, x=>x.receiptNo)
                .CheckContain(Searcher.stockCode, x=>x.stockCode)
                .CheckContain(Searcher.supplierCode, x=>x.supplierCode)
                .CheckContain(Searcher.supplierName, x=>x.supplierName)
                .CheckContain(Searcher.ticketNo, x=>x.ticketNo)
                .CheckEqual(Searcher.urgentFlag, x=>x.urgentFlag)
                .CheckContain(Searcher.unitCode, x=>x.unitCode)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new WmsInReceiptRecordHis_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    batchNo = x.batchNo,
                    binNo = x.binNo,
                    departmentName = x.departmentName,
                    docTypeCode = x.docTypeCode,
                    erpWhouseNo = x.erpWhouseNo,
                    externalInDtlId = x.externalInDtlId,
                    externalInNo = x.externalInNo,
                    inDtlId = x.inDtlId,
                    inNo = x.inNo,
                    inOutName = x.inOutName,
                    inOutTypeNo = x.inOutTypeNo,
                    inspectionResult = x.inspectionResult,
                    inspector = x.inspector,
                    iqcResultNo = x.iqcResultNo,
                    inRecordStatus = x.inRecordStatus,
                    loadedType = x.loadedType,
                    materialName = x.materialName,
                    materialCode = x.materialCode,
                    materialSpec = x.materialSpec,
                    orderDtlId = x.orderDtlId,
                    orderNo = x.orderNo,
                    palletBarcode = x.palletBarcode,
                    ptaRegionNo = x.ptaRegionNo,
                    ptaStockCode = x.ptaStockCode,
                    ptaStockDtlId = x.ptaStockDtlId,
                    minPkgQty = x.minPkgQty,
                    projectNo = x.projectNo,
                    proprietorCode = x.proprietorCode,
                    ptaBinNo = x.ptaBinNo,
                    ptaPalletBarcode = x.ptaPalletBarcode,
                    receiptDtlId = x.receiptDtlId,
                    receiptNo = x.receiptNo,
                    recordQty = x.recordQty,
                    regionNo = x.regionNo,
                    replenishFlag = x.replenishFlag,
                    returnFlag = x.returnFlag,
                    returnResult = x.returnResult,
                    returnTime = x.returnTime,
                    skuCode = x.skuCode,
                    sourceBy = x.sourceBy,
                    stockCode = x.stockCode,
                    stockDtlId = x.stockDtlId,
                    supplierCode = x.supplierCode,
                    supplierName = x.supplierName,
                    supplierNameAlias = x.supplierNameAlias,
                    supplierNameEn = x.supplierNameEn,
                    ticketNo = x.ticketNo,
                    urgentFlag = x.urgentFlag,
                    unitCode = x.unitCode,
                    whouseNo = x.whouseNo,
                    CreateBy = x.CreateBy,
                    CreateTime = x.CreateTime,
                    UpdateTime = x.UpdateTime,
                    UpdateBy = x.UpdateBy,
                })
                .OrderBy(x => x.ID).ToList();
            dicEntities = DC.Set<SysDictionary>().AsNoTracking().ToList();
            foreach (var item in query)
            {
                var entity = dicEntities.FirstOrDefault(t => t.dictionaryCode == "IN_RECORD_STATUS" && t.dictionaryItemCode == item.inRecordStatus.ToString());
                if (entity != null)
                {
                    item.inRecordStatusDesc = entity.dictionaryItemName;
                }
            }
            var queryList = query.AsQueryable().OrderByDescending(x => x.ID);
            return queryList;
        }

    }

    public class WmsInReceiptRecordHis_View : WmsInReceiptRecordHis{
        public string inRecordStatusDesc { get; set; }
    }
}
