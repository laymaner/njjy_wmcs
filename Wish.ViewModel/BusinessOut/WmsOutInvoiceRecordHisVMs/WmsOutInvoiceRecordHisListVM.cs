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


namespace Wish.ViewModel.BusinessOut.WmsOutInvoiceRecordHisVMs
{
    public partial class WmsOutInvoiceRecordHisListVM : BasePagedListVM<WmsOutInvoiceRecordHis_View, WmsOutInvoiceRecordHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsOutInvoiceRecordHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsOutInvoiceRecordHis_View>>{
                this.MakeGridHeader(x => x.allocatResult),
                this.MakeGridHeader(x => x.allotQty),
                this.MakeGridHeader(x => x.allotType),
                this.MakeGridHeader(x => x.docTypeCode),
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.assemblyIdx),
                this.MakeGridHeader(x => x.batchNo),
                this.MakeGridHeader(x => x.belongDepartment),
                this.MakeGridHeader(x => x.binNo),
                this.MakeGridHeader(x => x.deliveryLocNo),
                this.MakeGridHeader(x => x.erpWhouseNo),
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
                this.MakeGridHeader(x => x.externalOutDtlId),
                this.MakeGridHeader(x => x.externalOutNo),
                this.MakeGridHeader(x => x.fpName),
                this.MakeGridHeader(x => x.fpNo),
                this.MakeGridHeader(x => x.fpQty),
                this.MakeGridHeader(x => x.inOutName),
                this.MakeGridHeader(x => x.inOutTypeNo),
                this.MakeGridHeader(x => x.inspectionResult),
                this.MakeGridHeader(x => x.invoiceDtlId),
                this.MakeGridHeader(x => x.invoiceNo),
                this.MakeGridHeader(x => x.isBatch),
                this.MakeGridHeader(x => x.issuedResult),
                this.MakeGridHeader(x => x.materialName),
                this.MakeGridHeader(x => x.materialCode),
                this.MakeGridHeader(x => x.materialSpec),
                this.MakeGridHeader(x => x.orderDtlId),
                this.MakeGridHeader(x => x.orderNo),
                this.MakeGridHeader(x => x.outRecordStatus),
                this.MakeGridHeader(x => x.palletBarcode),
                this.MakeGridHeader(x => x.palletPickType),
                this.MakeGridHeader(x => x.pickLocNo),
                this.MakeGridHeader(x => x.pickQty),
                this.MakeGridHeader(x => x.pickTaskNo),
                this.MakeGridHeader(x => x.pickType),
                this.MakeGridHeader(x => x.preStockDtlId),
                this.MakeGridHeader(x => x.productDeptCode),
                this.MakeGridHeader(x => x.productDeptName),
                this.MakeGridHeader(x => x.productLocation),
                this.MakeGridHeader(x => x.projectNo),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.regionNo),
                this.MakeGridHeader(x => x.reversePickFlag),
                this.MakeGridHeader(x => x.skuCode),
                this.MakeGridHeader(x => x.sourceBy),
                this.MakeGridHeader(x => x.stockCode),
                this.MakeGridHeader(x => x.stockDtlId),
                this.MakeGridHeader(x => x.supplierCode),
                this.MakeGridHeader(x => x.supplierName),
                this.MakeGridHeader(x => x.supplierNameAlias),
                this.MakeGridHeader(x => x.supplierNameEn),
                this.MakeGridHeader(x => x.supplyType),
                this.MakeGridHeader(x => x.ticketNo),
                this.MakeGridHeader(x => x.ticketPlanBeginTime),
                this.MakeGridHeader(x => x.ticketType),
                this.MakeGridHeader(x => x.unitCode),
                this.MakeGridHeader(x => x.waveNo),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeader(x => x.operationMode),
                this.MakeGridHeader(x => x.outBarCode),
                this.MakeGridHeader(x => x.urgentFlag),
                this.MakeGridHeader(x => x.loadedTtype),
                this.MakeGridHeader(x => x.productSn),
                this.MakeGridHeader(x => x.lightColor),
                this.MakeGridHeader(x => x.CreateTime),
                this.MakeGridHeader(x => x.CreateBy),
                this.MakeGridHeader(x => x.UpdateBy),
                this.MakeGridHeader(x => x.UpdateTime),
                this.MakeGridHeader(x => x.outRecordStatusDesc),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        public List<SysDictionary> dicEntities = new List<SysDictionary>();
        public override IOrderedQueryable<WmsOutInvoiceRecordHis_View> GetSearchQuery()
        {
            var query = DC.Set<WmsOutInvoiceRecordHis>()
                .CheckContain(Searcher.allocatResult, x=>x.allocatResult)
                .CheckEqual(Searcher.allotType, x=>x.allotType)
                .CheckContain(Searcher.docTypeCode, x=>x.docTypeCode)
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.batchNo, x=>x.batchNo)
                .CheckContain(Searcher.belongDepartment, x=>x.belongDepartment)
                .CheckContain(Searcher.binNo, x=>x.binNo)
                .CheckContain(Searcher.deliveryLocNo, x=>x.deliveryLocNo)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckContain(Searcher.externalOutNo, x=>x.externalOutNo)
                .CheckContain(Searcher.fpNo, x=>x.fpNo)
                .CheckEqual(Searcher.inspectionResult, x=>x.inspectionResult)
                .CheckContain(Searcher.invoiceNo, x=>x.invoiceNo)
                .CheckContain(Searcher.materialCode, x=>x.materialCode)
                .CheckContain(Searcher.orderNo, x=>x.orderNo)
                .CheckEqual(Searcher.outRecordStatus, x=>x.outRecordStatus)
                .CheckContain(Searcher.pickLocNo, x=>x.pickLocNo)
                .CheckContain(Searcher.pickTaskNo, x=>x.pickTaskNo)
                .CheckContain(Searcher.projectNo, x=>x.projectNo)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.regionNo, x=>x.regionNo)
                .CheckContain(Searcher.skuCode, x=>x.skuCode)
                .CheckEqual(Searcher.sourceBy, x=>x.sourceBy)
                .CheckContain(Searcher.stockCode, x=>x.stockCode)
                .CheckContain(Searcher.supplierCode, x=>x.supplierCode)
                .CheckContain(Searcher.ticketNo, x=>x.ticketNo)
                .CheckContain(Searcher.unitCode, x=>x.unitCode)
                .CheckContain(Searcher.waveNo, x=>x.waveNo)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .CheckEqual(Searcher.loadedTtype, x=>x.loadedTtype)
                .Select(x => new WmsOutInvoiceRecordHis_View
                {
				    ID = x.ID,
                    allocatResult = x.allocatResult,
                    allotQty = x.allotQty,
                    allotType = x.allotType,
                    docTypeCode = x.docTypeCode,
                    areaNo = x.areaNo,
                    assemblyIdx = x.assemblyIdx,
                    batchNo = x.batchNo,
                    belongDepartment = x.belongDepartment,
                    binNo = x.binNo,
                    deliveryLocNo = x.deliveryLocNo,
                    erpWhouseNo = x.erpWhouseNo,
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
                    externalOutDtlId = x.externalOutDtlId,
                    externalOutNo = x.externalOutNo,
                    fpName = x.fpName,
                    fpNo = x.fpNo,
                    fpQty = x.fpQty,
                    inOutName = x.inOutName,
                    inOutTypeNo = x.inOutTypeNo,
                    inspectionResult = x.inspectionResult,
                    invoiceDtlId = x.invoiceDtlId,
                    invoiceNo = x.invoiceNo,
                    isBatch = x.isBatch,
                    issuedResult = x.issuedResult,
                    materialName = x.materialName,
                    materialCode = x.materialCode,
                    materialSpec = x.materialSpec,
                    orderDtlId = x.orderDtlId,
                    orderNo = x.orderNo,
                    outRecordStatus = x.outRecordStatus,
                    palletBarcode = x.palletBarcode,
                    palletPickType = x.palletPickType,
                    pickLocNo = x.pickLocNo,
                    pickQty = x.pickQty,
                    pickTaskNo = x.pickTaskNo,
                    pickType = x.pickType,
                    preStockDtlId = x.preStockDtlId,
                    productDeptCode = x.productDeptCode,
                    productDeptName = x.productDeptName,
                    productLocation = x.productLocation,
                    projectNo = x.projectNo,
                    proprietorCode = x.proprietorCode,
                    regionNo = x.regionNo,
                    reversePickFlag = x.reversePickFlag,
                    skuCode = x.skuCode,
                    sourceBy = x.sourceBy,
                    stockCode = x.stockCode,
                    stockDtlId = x.stockDtlId,
                    supplierCode = x.supplierCode,
                    supplierName = x.supplierName,
                    supplierNameAlias = x.supplierNameAlias,
                    supplierNameEn = x.supplierNameEn,
                    supplyType = x.supplyType,
                    ticketNo = x.ticketNo,
                    ticketPlanBeginTime = x.ticketPlanBeginTime,
                    ticketType = x.ticketType,
                    unitCode = x.unitCode,
                    waveNo = x.waveNo,
                    whouseNo = x.whouseNo,
                    operationMode = x.operationMode,
                    outBarCode = x.outBarCode,
                    urgentFlag = x.urgentFlag,
                    loadedTtype = x.loadedTtype,
                    productSn = x.productSn,
                    lightColor = x.lightColor,
                    CreateBy = x.CreateBy,
                    CreateTime = x.CreateTime,
                    UpdateBy = x.UpdateBy,
                    UpdateTime = x.UpdateTime,
                })
                .OrderBy(x => x.ID).ToList();
            dicEntities = DC.Set<SysDictionary>().AsNoTracking().ToList();
            foreach (var item in query)
            {
                var entityCmd = dicEntities.FirstOrDefault(t => t.dictionaryCode == "OUT_RECORD_STATUS" && t.dictionaryItemCode == item.outRecordStatus.ToString());
                if (entityCmd != null)
                {
                    item.outRecordStatusDesc = entityCmd.dictionaryItemName;
                }
            }
            var queryList = query.AsQueryable().OrderByDescending(x => x.CreateTime);
            return queryList;
        }

    }

    public class WmsOutInvoiceRecordHis_View : WmsOutInvoiceRecordHis{
        public string outRecordStatusDesc { get; set; }
    }
}
