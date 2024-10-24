using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWPalletTypeVMs
{
    public partial class BasWPalletTypeListVM : BasePagedListVM<BasWPalletType_View, BasWPalletTypeSearcher>
    {

        protected override IEnumerable<IGridColumn<BasWPalletType_View>> InitGridHeader()
        {
            return new List<GridColumn<BasWPalletType_View>>{
                this.MakeGridHeader(x => x.barcodeFlag),
                this.MakeGridHeader(x => x.checkFormula),
                this.MakeGridHeader(x => x.checkPalletFlag),
                this.MakeGridHeader(x => x.chekDesc),
                this.MakeGridHeader(x => x.developFlag),
                this.MakeGridHeader(x => x.emptyMaxQty),
                this.MakeGridHeader(x => x.maxWeight),
                this.MakeGridHeader(x => x.palletHeight),
                this.MakeGridHeader(x => x.palletLength),
                this.MakeGridHeader(x => x.palletTypeCode),
                this.MakeGridHeader(x => x.palletTypeFlag),
                this.MakeGridHeader(x => x.palletTypeName),
                this.MakeGridHeader(x => x.palletTypeNameAlias),
                this.MakeGridHeader(x => x.palletTypeNameEn),
                this.MakeGridHeader(x => x.palletWeight),
                this.MakeGridHeader(x => x.palletWidth),
                this.MakeGridHeader(x => x.positionCol),
                this.MakeGridHeader(x => x.positionDirect),
                this.MakeGridHeader(x => x.positionFlag),
                this.MakeGridHeader(x => x.positionRow),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasWPalletType_View> GetSearchQuery()
        {
            var query = DC.Set<BasWPalletType>()
                .CheckEqual(Searcher.barcodeFlag, x=>x.barcodeFlag)
                .CheckEqual(Searcher.developFlag, x=>x.developFlag)
                .CheckContain(Searcher.palletTypeCode, x=>x.palletTypeCode)
                .CheckEqual(Searcher.palletTypeFlag, x=>x.palletTypeFlag)
                .CheckContain(Searcher.palletTypeName, x=>x.palletTypeName)
                .CheckEqual(Searcher.palletWeight, x=>x.palletWeight)
                .CheckEqual(Searcher.palletWidth, x=>x.palletWidth)
                .CheckEqual(Searcher.positionCol, x=>x.positionCol)
                .CheckEqual(Searcher.positionDirect, x=>x.positionDirect)
                .CheckEqual(Searcher.positionFlag, x=>x.positionFlag)
                .CheckEqual(Searcher.positionRow, x=>x.positionRow)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .Select(x => new BasWPalletType_View
                {
				    ID = x.ID,
                    barcodeFlag = x.barcodeFlag,
                    checkFormula = x.checkFormula,
                    checkPalletFlag = x.checkPalletFlag,
                    chekDesc = x.chekDesc,
                    developFlag = x.developFlag,
                    emptyMaxQty = x.emptyMaxQty,
                    maxWeight = x.maxWeight,
                    palletHeight = x.palletHeight,
                    palletLength = x.palletLength,
                    palletTypeCode = x.palletTypeCode,
                    palletTypeFlag = x.palletTypeFlag,
                    palletTypeName = x.palletTypeName,
                    palletTypeNameAlias = x.palletTypeNameAlias,
                    palletTypeNameEn = x.palletTypeNameEn,
                    palletWeight = x.palletWeight,
                    palletWidth = x.palletWidth,
                    positionCol = x.positionCol,
                    positionDirect = x.positionDirect,
                    positionFlag = x.positionFlag,
                    positionRow = x.positionRow,
                    usedFlag = x.usedFlag,
                    whouseNo = x.whouseNo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasWPalletType_View : BasWPalletType{

    }
}
