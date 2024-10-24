using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockDtlHisVMs
{
    public partial class WmsStockDtlHisTemplateVM : BaseTemplateVM
    {
        [Display(Name = "楼号")]
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.areaNo);
        [Display(Name = "ERP仓库")]
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.erpWhouseNo);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.chipSize);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.chipThickness);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.chipModel);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.dafType);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.extend9);
        [Display(Name = "质量标记")]
        public ExcelPropety inspectionResult_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.inspectionResult);
        [Display(Name = "锁定状态")]
        public ExcelPropety lockFlag_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.lockFlag);
        [Display(Name = "锁定原因")]
        public ExcelPropety lockReason_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.lockReason);
        [Display(Name = "物料名称")]
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.materialName);
        [Display(Name = "物料编码")]
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.materialCode);
        [Display(Name = "规格型号")]
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.materialSpec);
        [Display(Name = "占用数量")]
        public ExcelPropety occupyQty_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.occupyQty);
        [Display(Name = "载体条码")]
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.palletBarcode);
        [Display(Name = "项目号")]
        public ExcelPropety projectNo_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.projectNo);
        [Display(Name = "备用项目号")]
        public ExcelPropety projectNoBak_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.projectNoBak);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.proprietorCode);
        [Display(Name = "库存数量")]
        public ExcelPropety qty_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.qty);
        public ExcelPropety skuCode_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.skuCode);
        public ExcelPropety stockCode_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.stockCode);
        [Display(Name = "库存明细状态")]
        public ExcelPropety stockDtlStatus_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.stockDtlStatus);
        [Display(Name = "供应商编码")]
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.supplierCode);
        [Display(Name = "供方名称")]
        public ExcelPropety supplierName_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.supplierName);
        public ExcelPropety supplierNameAlias_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.supplierNameAlias);
        public ExcelPropety supplierNameEn_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.supplierNameEn);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.whouseNo);
        [Display(Name = "单位")]
        public ExcelPropety unitCode_Excel = ExcelPropety.CreateProperty<WmsStockDtlHis>(x => x.unitCode);

	    protected override void InitVM()
        {
        }

    }

    public class WmsStockDtlHisImportVM : BaseImportVM<WmsStockDtlHisTemplateVM, WmsStockDtlHis>
    {

    }

}
