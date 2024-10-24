using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessCheck.WmsItnInventoryDtlVMs
{
    public partial class WmsItnInventoryDtlTemplateVM : BaseTemplateVM
    {
        [Display(Name = "批次号")]
        public ExcelPropety batchNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtl>(x => x.batchNo);
        [Display(Name = "确认数量")]
        public ExcelPropety confirmQty_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtl>(x => x.confirmQty);
        [Display(Name = "是否差异")]
        public ExcelPropety difFlag_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtl>(x => x.difFlag);
        public ExcelPropety inspectionResult_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtl>(x => x.inspectionResult);
        [Display(Name = "盘点明细状态")]
        public ExcelPropety inventoryDtlStatus_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtl>(x => x.inventoryDtlStatus);
        [Display(Name = "盘点单号")]
        public ExcelPropety inventoryNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtl>(x => x.inventoryNo);
        [Display(Name = "盘点数量")]
        public ExcelPropety inventoryQty_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtl>(x => x.inventoryQty);
        [Display(Name = "物料编码")]
        public ExcelPropety materialCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtl>(x => x.materialCode);
        [Display(Name = "物料名称")]
        public ExcelPropety materialName_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtl>(x => x.materialName);
        [Display(Name = "物料规格")]
        public ExcelPropety materialSpec_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtl>(x => x.materialSpec);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtl>(x => x.proprietorCode);
        [Display(Name = "计划数量")]
        public ExcelPropety qty_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtl>(x => x.qty);
        [Display(Name = "单位")]
        public ExcelPropety unitCode_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtl>(x => x.unitCode);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsItnInventoryDtl>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsItnInventoryDtlImportVM : BaseImportVM<WmsItnInventoryDtlTemplateVM, WmsItnInventoryDtl>
    {

    }

}
