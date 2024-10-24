using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockAdjustVMs
{
    public partial class WmsStockAdjustTemplateVM : BaseTemplateVM
    {
        [Display(Name = "调整说明")]
        public ExcelPropety adjustDesc_Excel = ExcelPropety.CreateProperty<WmsStockAdjust>(x => x.adjustDesc);
        [Display(Name = "调整操作")]
        public ExcelPropety adjustOperate_Excel = ExcelPropety.CreateProperty<WmsStockAdjust>(x => x.adjustOperate);
        [Display(Name = "调整类型")]
        public ExcelPropety adjustType_Excel = ExcelPropety.CreateProperty<WmsStockAdjust>(x => x.adjustType);
        [Display(Name = "包装条码")]
        public ExcelPropety packageBarcode_Excel = ExcelPropety.CreateProperty<WmsStockAdjust>(x => x.packageBarcode);
        [Display(Name = "载体条码")]
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<WmsStockAdjust>(x => x.palletBarcode);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsStockAdjust>(x => x.proprietorCode);
        [Display(Name = "库存编码")]
        public ExcelPropety stockCode_Excel = ExcelPropety.CreateProperty<WmsStockAdjust>(x => x.stockCode);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsStockAdjust>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsStockAdjustImportVM : BaseImportVM<WmsStockAdjustTemplateVM, WmsStockAdjust>
    {

    }

}
