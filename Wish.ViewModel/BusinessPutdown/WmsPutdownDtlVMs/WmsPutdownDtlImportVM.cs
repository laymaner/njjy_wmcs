using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessPutdown.WmsPutdownDtlVMs
{
    public partial class WmsPutdownDtlTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.areaNo);
        public ExcelPropety erpWhouseNo_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.erpWhouseNo);
        public ExcelPropety extend1_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.extend1);
        public ExcelPropety extend10_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.extend10);
        public ExcelPropety extend11_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.extend11);
        public ExcelPropety extend12_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.extend12);
        public ExcelPropety extend13_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.extend13);
        public ExcelPropety extend14_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.extend14);
        public ExcelPropety extend15_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.extend15);
        public ExcelPropety extend2_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.extend2);
        public ExcelPropety extend3_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.extend3);
        public ExcelPropety extend4_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.extend4);
        public ExcelPropety extend5_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.extend5);
        public ExcelPropety extend6_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.extend6);
        public ExcelPropety extend7_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.extend7);
        public ExcelPropety extend8_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.extend8);
        public ExcelPropety extend9_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.extend9);
        [Display(Name = "质量标记")]
        public ExcelPropety inspectionResult_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.inspectionResult);
        [Display(Name = "物料名称")]
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.materialName);
        [Display(Name = "物料编码")]
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.materialCode);
        [Display(Name = "规格型号")]
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.materialSpec);
        [Display(Name = "占用数量")]
        public ExcelPropety occupyQty_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.occupyQty);
        [Display(Name = "载体条码")]
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.palletBarcode);
        public ExcelPropety projectNo_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.projectNo);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.proprietorCode);
        public ExcelPropety putdownNo_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.putdownNo);
        [Display(Name = "SKU编码")]
        public ExcelPropety skuCode_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.skuCode);
        [Display(Name = "库存编码")]
        public ExcelPropety stockCode_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.stockCode);
        public ExcelPropety stockDtlId_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.stockDtlId);
        [Display(Name = "库存数量")]
        public ExcelPropety stockQty_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.stockQty);
        public ExcelPropety supplierName_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.supplierName);
        public ExcelPropety supplierNameAlias_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.supplierNameAlias);
        public ExcelPropety supplierNameEn_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.supplierNameEn);
        public ExcelPropety supplierCode_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.supplierCode);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.whouseNo);
        public ExcelPropety putdownDtlStatus_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.putdownDtlStatus);
        public ExcelPropety unitCode_Excel = ExcelPropety.CreateProperty<WmsPutdownDtl>(x => x.unitCode);

	    protected override void InitVM()
        {
        }

    }

    public class WmsPutdownDtlImportVM : BaseImportVM<WmsPutdownDtlTemplateVM, WmsPutdownDtl>
    {

    }

}
