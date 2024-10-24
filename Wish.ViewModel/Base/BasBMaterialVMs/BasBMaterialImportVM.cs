using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBMaterialVMs
{
    public partial class BasBMaterialTemplateVM : BaseTemplateVM
    {
        public ExcelPropety BarcodeFlag_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.BarcodeFlag);
        public ExcelPropety Brand_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.Brand);
        public ExcelPropety BuyerCode_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.BuyerCode);
        public ExcelPropety BuyerName_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.BuyerName);
        public ExcelPropety EmaterialVtime_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.EmaterialVtime);
        public ExcelPropety ErpBinNo_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.ErpBinNo);
        public ExcelPropety Material_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.Material);
        public ExcelPropety MaterialCategoryCode_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.MaterialCategoryCode);
        public ExcelPropety MaterialName_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.MaterialName);
        public ExcelPropety MaterialNameAlias_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.MaterialNameAlias);
        public ExcelPropety MaterialNameEn_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.MaterialNameEn);
        public ExcelPropety MaterialCode_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.MaterialCode);
        public ExcelPropety MaterialSpec_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.MaterialSpec);
        public ExcelPropety MaterialTypeDesc_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.MaterialTypeDesc);
        public ExcelPropety MaterialTypeCode_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.MaterialTypeCode);
        public ExcelPropety MaxDelayTimes_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.MaxDelayTimes);
        public ExcelPropety MaxDriedTimes_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.MaxDriedTimes);
        public ExcelPropety MinPkgQty_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.MinPkgQty);
        public ExcelPropety ProjectDrawingNo_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.ProjectDrawingNo);
        public ExcelPropety ProprietorCode_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.ProprietorCode);
        public ExcelPropety QcFlag_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.QcFlag);
        public ExcelPropety SharedFalg_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.SharedFalg);
        public ExcelPropety SkuRuleNo_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.SkuRuleNo);
        public ExcelPropety SluggishTime_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.SluggishTime);
        public ExcelPropety TechParm_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.TechParm);
        public ExcelPropety UnitCode_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.UnitCode);
        public ExcelPropety UnitWeight_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.UnitWeight);
        public ExcelPropety UsedFlag_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.UsedFlag);
        public ExcelPropety VirtualFlag_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.VirtualFlag);
        public ExcelPropety WarnOverdueLen_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.WarnOverdueLen);
        public ExcelPropety WhouseNo_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.WhouseNo);
        public ExcelPropety CompanyCode_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.CompanyCode);
        public ExcelPropety Extend1_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.Extend1);
        public ExcelPropety Extend2_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.Extend2);
        public ExcelPropety Extend3_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.Extend3);
        public ExcelPropety Extend4_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.Extend4);
        public ExcelPropety Extend5_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.Extend5);
        public ExcelPropety Extend6_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.Extend6);
        public ExcelPropety Extend7_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.Extend7);
        public ExcelPropety Extend8_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.Extend8);
        public ExcelPropety Extend9_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.Extend9);
        public ExcelPropety Extend10_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.Extend10);
        public ExcelPropety Extend11_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.Extend11);
        public ExcelPropety Extend12_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.Extend12);
        public ExcelPropety Extend13_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.Extend13);
        public ExcelPropety Extend14_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.Extend14);
        public ExcelPropety Extend15_Excel = ExcelPropety.CreateProperty<BasBMaterial>(x => x.Extend15);

	    protected override void InitVM()
        {
        }

    }

    public class BasBMaterialImportVM : BaseImportVM<BasBMaterialTemplateVM, BasBMaterial>
    {

    }

}
