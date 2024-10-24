using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBMaterialVMs
{
    public partial class BasBMaterialListVM : BasePagedListVM<BasBMaterial_View, BasBMaterialSearcher>
    {

        protected override IEnumerable<IGridColumn<BasBMaterial_View>> InitGridHeader()
        {
            return new List<GridColumn<BasBMaterial_View>>{
                this.MakeGridHeader(x => x.BarcodeFlag),
                this.MakeGridHeader(x => x.Brand),
                this.MakeGridHeader(x => x.BuyerCode),
                this.MakeGridHeader(x => x.BuyerName),
                this.MakeGridHeader(x => x.EmaterialVtime),
                this.MakeGridHeader(x => x.ErpBinNo),
                this.MakeGridHeader(x => x.Material),
                this.MakeGridHeader(x => x.MaterialCategoryCode),
                this.MakeGridHeader(x => x.MaterialName),
                this.MakeGridHeader(x => x.MaterialNameAlias),
                this.MakeGridHeader(x => x.MaterialNameEn),
                this.MakeGridHeader(x => x.MaterialCode),
                this.MakeGridHeader(x => x.MaterialSpec),
                this.MakeGridHeader(x => x.MaterialTypeDesc),
                this.MakeGridHeader(x => x.MaterialTypeCode),
                this.MakeGridHeader(x => x.MaxDelayTimes),
                this.MakeGridHeader(x => x.MaxDriedTimes),
                this.MakeGridHeader(x => x.MinPkgQty),
                this.MakeGridHeader(x => x.ProjectDrawingNo),
                this.MakeGridHeader(x => x.ProprietorCode),
                this.MakeGridHeader(x => x.QcFlag),
                this.MakeGridHeader(x => x.SharedFalg),
                this.MakeGridHeader(x => x.SkuRuleNo),
                this.MakeGridHeader(x => x.SluggishTime),
                this.MakeGridHeader(x => x.TechParm),
                this.MakeGridHeader(x => x.UnitCode),
                this.MakeGridHeader(x => x.UnitWeight),
                this.MakeGridHeader(x => x.UsedFlag),
                this.MakeGridHeader(x => x.VirtualFlag),
                this.MakeGridHeader(x => x.WarnOverdueLen),
                this.MakeGridHeader(x => x.WhouseNo),
                this.MakeGridHeader(x => x.CompanyCode),
                this.MakeGridHeader(x => x.Extend1),
                this.MakeGridHeader(x => x.Extend2),
                this.MakeGridHeader(x => x.Extend3),
                this.MakeGridHeader(x => x.Extend4),
                this.MakeGridHeader(x => x.Extend5),
                this.MakeGridHeader(x => x.Extend6),
                this.MakeGridHeader(x => x.Extend7),
                this.MakeGridHeader(x => x.Extend8),
                this.MakeGridHeader(x => x.Extend9),
                this.MakeGridHeader(x => x.Extend10),
                this.MakeGridHeader(x => x.Extend11),
                this.MakeGridHeader(x => x.Extend12),
                this.MakeGridHeader(x => x.Extend13),
                this.MakeGridHeader(x => x.Extend14),
                this.MakeGridHeader(x => x.Extend15),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasBMaterial_View> GetSearchQuery()
        {
            var query = DC.Set<BasBMaterial>()
                .CheckEqual(Searcher.BarcodeFlag, x=>x.BarcodeFlag)
                .CheckContain(Searcher.Brand, x=>x.Brand)
                .CheckContain(Searcher.BuyerCode, x=>x.BuyerCode)
                .CheckContain(Searcher.BuyerName, x=>x.BuyerName)
                .CheckContain(Searcher.ErpBinNo, x=>x.ErpBinNo)
                .CheckContain(Searcher.MaterialCategoryCode, x=>x.MaterialCategoryCode)
                .CheckContain(Searcher.MaterialCode, x=>x.MaterialCode)
                .CheckContain(Searcher.MaterialTypeCode, x=>x.MaterialTypeCode)
                .CheckContain(Searcher.UnitCode, x=>x.UnitCode)
                .CheckContain(Searcher.WhouseNo, x=>x.WhouseNo)
                .CheckContain(Searcher.CompanyCode, x=>x.CompanyCode)
                .Select(x => new BasBMaterial_View
                {
				    ID = x.ID,
                    BarcodeFlag = x.BarcodeFlag,
                    Brand = x.Brand,
                    BuyerCode = x.BuyerCode,
                    BuyerName = x.BuyerName,
                    EmaterialVtime = x.EmaterialVtime,
                    ErpBinNo = x.ErpBinNo,
                    Material = x.Material,
                    MaterialCategoryCode = x.MaterialCategoryCode,
                    MaterialName = x.MaterialName,
                    MaterialNameAlias = x.MaterialNameAlias,
                    MaterialNameEn = x.MaterialNameEn,
                    MaterialCode = x.MaterialCode,
                    MaterialSpec = x.MaterialSpec,
                    MaterialTypeDesc = x.MaterialTypeDesc,
                    MaterialTypeCode = x.MaterialTypeCode,
                    MaxDelayTimes = x.MaxDelayTimes,
                    MaxDriedTimes = x.MaxDriedTimes,
                    MinPkgQty = x.MinPkgQty,
                    ProjectDrawingNo = x.ProjectDrawingNo,
                    ProprietorCode = x.ProprietorCode,
                    QcFlag = x.QcFlag,
                    SharedFalg = x.SharedFalg,
                    SkuRuleNo = x.SkuRuleNo,
                    SluggishTime = x.SluggishTime,
                    TechParm = x.TechParm,
                    UnitCode = x.UnitCode,
                    UnitWeight = x.UnitWeight,
                    UsedFlag = x.UsedFlag,
                    VirtualFlag = x.VirtualFlag,
                    WarnOverdueLen = x.WarnOverdueLen,
                    WhouseNo = x.WhouseNo,
                    CompanyCode = x.CompanyCode,
                    Extend1 = x.Extend1,
                    Extend2 = x.Extend2,
                    Extend3 = x.Extend3,
                    Extend4 = x.Extend4,
                    Extend5 = x.Extend5,
                    Extend6 = x.Extend6,
                    Extend7 = x.Extend7,
                    Extend8 = x.Extend8,
                    Extend9 = x.Extend9,
                    Extend10 = x.Extend10,
                    Extend11 = x.Extend11,
                    Extend12 = x.Extend12,
                    Extend13 = x.Extend13,
                    Extend14 = x.Extend14,
                    Extend15 = x.Extend15,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasBMaterial_View : BasBMaterial{

    }
}
