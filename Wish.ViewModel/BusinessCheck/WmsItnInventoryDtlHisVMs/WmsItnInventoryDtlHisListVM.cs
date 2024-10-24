using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryDtlHisVMs
{
    public partial class WmsItnInventoryDtlHisListVM : BasePagedListVM<WmsItnInventoryDtlHis_View, WmsItnInventoryDtlHisSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsItnInventoryDtlHis_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsItnInventoryDtlHis_View>>{
                this.MakeGridHeader(x => x.batchNo),
                this.MakeGridHeader(x => x.confirmQty),
                this.MakeGridHeader(x => x.difFlag),
                this.MakeGridHeader(x => x.inspectionResult),
                this.MakeGridHeader(x => x.inventoryDtlStatus),
                this.MakeGridHeader(x => x.inventoryNo),
                this.MakeGridHeader(x => x.inventoryQty),
                this.MakeGridHeader(x => x.materialCode),
                this.MakeGridHeader(x => x.materialName),
                this.MakeGridHeader(x => x.materialSpec),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.qty),
                this.MakeGridHeader(x => x.unitCode),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<WmsItnInventoryDtlHis_View> GetSearchQuery()
        {
            var query = DC.Set<WmsItnInventoryDtlHis>()
                .CheckContain(Searcher.batchNo, x=>x.batchNo)
                .CheckEqual(Searcher.confirmQty, x=>x.confirmQty)
                .CheckEqual(Searcher.inspectionResult, x=>x.inspectionResult)
                .CheckEqual(Searcher.inventoryDtlStatus, x=>x.inventoryDtlStatus)
                .CheckContain(Searcher.inventoryNo, x=>x.inventoryNo)
                .CheckContain(Searcher.materialCode, x=>x.materialCode)
                .CheckContain(Searcher.materialName, x=>x.materialName)
                .CheckContain(Searcher.materialSpec, x=>x.materialSpec)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.unitCode, x=>x.unitCode)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new WmsItnInventoryDtlHis_View
                {
				    ID = x.ID,
                    batchNo = x.batchNo,
                    confirmQty = x.confirmQty,
                    difFlag = x.difFlag,
                    inspectionResult = x.inspectionResult,
                    inventoryDtlStatus = x.inventoryDtlStatus,
                    inventoryNo = x.inventoryNo,
                    inventoryQty = x.inventoryQty,
                    materialCode = x.materialCode,
                    materialName = x.materialName,
                    materialSpec = x.materialSpec,
                    proprietorCode = x.proprietorCode,
                    qty = x.qty,
                    unitCode = x.unitCode,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class WmsItnInventoryDtlHis_View : WmsItnInventoryDtlHis{

    }
}
