using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessPutaway.WmsPutawayDtlHisVMs
{
    public partial class WmsPutawayDtlHisListVM : BasePagedListVM<WmsPutawayDtlHis_View, WmsPutawayDtlHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsPutawayDtlHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsPutawayDtlHis_View>>{
                this.MakeGridHeader(x => x.areaNo),
                this.MakeGridHeader(x => x.binNo),
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
                this.MakeGridHeader(x => x.materialCode),
                this.MakeGridHeader(x => x.materialName),
                this.MakeGridHeader(x => x.materialSpec),
                this.MakeGridHeader(x => x.orderDtlId),
                this.MakeGridHeader(x => x.orderNo),
                this.MakeGridHeader(x => x.palletBarcode),
                this.MakeGridHeader(x => x.projectNo),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.ptaBinNo),
                this.MakeGridHeader(x => x.putawayDtlStatus),
                this.MakeGridHeader(x => x.putawayNo),
                this.MakeGridHeader(x => x.recordId),
                this.MakeGridHeader(x => x.recordQty),
                this.MakeGridHeader(x => x.regionNo),
                this.MakeGridHeader(x => x.roadwayNo),
                this.MakeGridHeader(x => x.skuCode),
                this.MakeGridHeader(x => x.stockCode),
                this.MakeGridHeader(x => x.stockDtlId),
                this.MakeGridHeader(x => x.supplierCode),
                this.MakeGridHeader(x => x.supplierName),
                this.MakeGridHeader(x => x.supplierNameAlias),
                this.MakeGridHeader(x => x.supplierNameEn),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeader(x => x.unitCode),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsPutawayDtlHis_View> GetSearchQuery()
        {
            var query = DC.Set<WmsPutawayDtlHis>()
                .CheckContain(Searcher.areaNo, x=>x.areaNo)
                .CheckContain(Searcher.binNo, x=>x.binNo)
                .CheckContain(Searcher.docTypeCode, x=>x.docTypeCode)
                .CheckContain(Searcher.erpWhouseNo, x=>x.erpWhouseNo)
                .CheckContain(Searcher.materialCode, x=>x.materialCode)
                .CheckContain(Searcher.materialSpec, x=>x.materialSpec)
                .CheckContain(Searcher.palletBarcode, x=>x.palletBarcode)
                .CheckContain(Searcher.projectNo, x=>x.projectNo)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckEqual(Searcher.putawayDtlStatus, x=>x.putawayDtlStatus)
                .CheckContain(Searcher.putawayNo, x=>x.putawayNo)
                .CheckContain(Searcher.roadwayNo, x=>x.roadwayNo)
                .CheckContain(Searcher.stockCode, x=>x.stockCode)
                //.CheckContain(Searcher.stockDtlId, x=>x.stockDtlId)
                .CheckContain(Searcher.supplierCode, x=>x.supplierCode)
                .CheckContain(Searcher.unitCode, x=>x.unitCode)
                .Select(x => new WmsPutawayDtlHis_View
                {
				    ID = x.ID,
                    areaNo = x.areaNo,
                    binNo = x.binNo,
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
                    materialCode = x.materialCode,
                    materialName = x.materialName,
                    materialSpec = x.materialSpec,
                    orderDtlId = x.orderDtlId,
                    orderNo = x.orderNo,
                    palletBarcode = x.palletBarcode,
                    projectNo = x.projectNo,
                    proprietorCode = x.proprietorCode,
                    ptaBinNo = x.ptaBinNo,
                    putawayDtlStatus = x.putawayDtlStatus,
                    putawayNo = x.putawayNo,
                    recordId = x.recordId,
                    recordQty = x.recordQty,
                    regionNo = x.regionNo,
                    roadwayNo = x.roadwayNo,
                    skuCode = x.skuCode,
                    stockCode = x.stockCode,
                    stockDtlId = x.stockDtlId,
                    supplierCode = x.supplierCode,
                    supplierName = x.supplierName,
                    supplierNameAlias = x.supplierNameAlias,
                    supplierNameEn = x.supplierNameEn,
                    whouseNo = x.whouseNo,
                    unitCode = x.unitCode,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsPutawayDtlHis_View : WmsPutawayDtlHis{

    }
}
