using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryDtlVMs
{
    public partial class WmsItnInventoryDtlListVM : BasePagedListVM<WmsItnInventoryDtl_View, WmsItnInventoryDtlSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsItnInventoryDtl_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsItnInventoryDtl_View>>{
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

        public override IOrderedQueryable<WmsItnInventoryDtl_View> GetSearchQuery()
        {
            var query = DC.Set<WmsItnInventoryDtl>()
                .CheckContain(Searcher.batchNo, x=>x.batchNo)
                .CheckEqual(Searcher.difFlag, x=>x.difFlag)
                .CheckEqual(Searcher.inspectionResult, x=>x.inspectionResult)
                .CheckEqual(Searcher.inventoryDtlStatus, x=>x.inventoryDtlStatus)
                .CheckContain(Searcher.inventoryNo, x=>x.inventoryNo)
                .CheckEqual(Searcher.inventoryQty, x=>x.inventoryQty)
                .CheckContain(Searcher.materialCode, x=>x.materialCode)
                .CheckContain(Searcher.materialName, x=>x.materialName)
                .CheckContain(Searcher.materialSpec, x=>x.materialSpec)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckEqual(Searcher.qty, x=>x.qty)
                .CheckContain(Searcher.unitCode, x=>x.unitCode)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new WmsItnInventoryDtl_View
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

    public class WmsItnInventoryDtl_View : WmsItnInventoryDtl{

    }
}
