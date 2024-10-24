using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryRecordVMs
{
    public partial class WmsItnInventoryRecordListVM : BasePagedListVM<WmsItnInventoryRecord_View, WmsItnInventoryRecordSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsItnInventoryRecord_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsItnInventoryRecord_View>>{
                this.MakeGridHeader(x => x.blindFlag),
                this.MakeGridHeader(x => x.confirmBy),
                this.MakeGridHeader(x => x.confirmQty),
                this.MakeGridHeader(x => x.confirmReason),
                this.MakeGridHeader(x => x.differenceFlag),
                this.MakeGridHeader(x => x.docTypeCode),
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
                this.MakeGridHeader(x => x.inspectionResult),
                this.MakeGridHeader(x => x.inventoryBy),
                this.MakeGridHeader(x => x.inventoryDtlId),
                this.MakeGridHeader(x => x.inventoryNo),
                this.MakeGridHeader(x => x.inventoryQty),
                this.MakeGridHeader(x => x.inventoryReason),
                this.MakeGridHeader(x => x.inventoryRecordStatus),
                this.MakeGridHeader(x => x.materialName),
                this.MakeGridHeader(x => x.materialCode),
                this.MakeGridHeader(x => x.materialSpec),
                this.MakeGridHeader(x => x.occupyQty),
                this.MakeGridHeader(x => x.palletBarcode),
                this.MakeGridHeader(x => x.projectNo),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.putdownLocNo),
                this.MakeGridHeader(x => x.skuCode),
                this.MakeGridHeader(x => x.stockCode),
                this.MakeGridHeader(x => x.stockDtlId),
                this.MakeGridHeader(x => x.stockQty),
                this.MakeGridHeader(x => x.supplierCode),
                this.MakeGridHeader(x => x.supplierName),
                this.MakeGridHeader(x => x.supplierNameAlias),
                this.MakeGridHeader(x => x.supplierNameEn),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeader(x => x.inOutTypeNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsItnInventoryRecord_View> GetSearchQuery()
        {
            var query = DC.Set<WmsItnInventoryRecord>()
                .CheckEqual(Searcher.blindFlag, x=>x.blindFlag)
                .CheckContain(Searcher.confirmBy, x=>x.confirmBy)
                .CheckContain(Searcher.inOutTypeNo, x=>x.inOutTypeNo)
                .CheckEqual(Searcher.confirmQty, x=>x.confirmQty)
                .CheckContain(Searcher.confirmReason, x=>x.confirmReason)
                .CheckEqual(Searcher.differenceFlag, x=>x.differenceFlag)
                .CheckContain(Searcher.docTypeCode, x=>x.docTypeCode)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckEqual(Searcher.inspectionResult, x=>x.inspectionResult)
                .CheckContain(Searcher.inventoryBy, x=>x.inventoryBy)
                .CheckContain(Searcher.inventoryNo, x=>x.inventoryNo)
                .CheckContain(Searcher.inventoryReason, x=>x.inventoryReason)
                .CheckEqual(Searcher.inventoryRecordStatus, x=>x.inventoryRecordStatus)
                .CheckContain(Searcher.materialName, x=>x.materialName)
                .CheckContain(Searcher.materialCode, x=>x.materialCode)
                .CheckContain(Searcher.palletBarcode, x=>x.palletBarcode)
                .CheckContain(Searcher.projectNo, x=>x.projectNo)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.putdownLocNo, x=>x.putdownLocNo)
                .CheckContain(Searcher.skuCode, x=>x.skuCode)
                .CheckContain(Searcher.stockCode, x=>x.stockCode)
                .CheckContain(Searcher.supplierCode, x=>x.supplierCode)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new WmsItnInventoryRecord_View
                {
				    ID = x.ID,
                    blindFlag = x.blindFlag,
                    confirmBy = x.confirmBy,
                    confirmQty = x.confirmQty,
                    confirmReason = x.confirmReason,
                    differenceFlag = x.differenceFlag,
                    docTypeCode = x.docTypeCode,
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
                    inspectionResult = x.inspectionResult,
                    inventoryBy = x.inventoryBy,
                    inventoryDtlId = x.inventoryDtlId,
                    inventoryNo = x.inventoryNo,
                    inventoryQty = x.inventoryQty,
                    inventoryReason = x.inventoryReason,
                    inventoryRecordStatus = x.inventoryRecordStatus,
                    materialName = x.materialName,
                    materialCode = x.materialCode,
                    materialSpec = x.materialSpec,
                    occupyQty = x.occupyQty,
                    palletBarcode = x.palletBarcode,
                    projectNo = x.projectNo,
                    proprietorCode = x.proprietorCode,
                    putdownLocNo = x.putdownLocNo,
                    skuCode = x.skuCode,
                    stockCode = x.stockCode,
                    stockDtlId = x.stockDtlId,
                    stockQty = x.stockQty,
                    supplierCode = x.supplierCode,
                    supplierName = x.supplierName,
                    supplierNameAlias = x.supplierNameAlias,
                    supplierNameEn = x.supplierNameEn,
                    whouseNo = x.whouseNo,
                    inOutTypeNo = x.inOutTypeNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsItnInventoryRecord_View : WmsItnInventoryRecord{

    }
}
