using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessStock.WmsStockVMs
{
    public partial class WmsStockTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsStock>(x => x.areaNo);
        public ExcelPropety binNo_Excel = ExcelPropety.CreateProperty<WmsStock>(x => x.binNo);
        public ExcelPropety errFlag_Excel = ExcelPropety.CreateProperty<WmsStock>(x => x.errFlag);
        public ExcelPropety errMsg_Excel = ExcelPropety.CreateProperty<WmsStock>(x => x.errMsg);
        public ExcelPropety height_Excel = ExcelPropety.CreateProperty<WmsStock>(x => x.height);
        public ExcelPropety invoiceNo_Excel = ExcelPropety.CreateProperty<WmsStock>(x => x.invoiceNo);
        public ExcelPropety loadedType_Excel = ExcelPropety.CreateProperty<WmsStock>(x => x.loadedType);
        public ExcelPropety locAllotGroup_Excel = ExcelPropety.CreateProperty<WmsStock>(x => x.locAllotGroup);
        public ExcelPropety locNo_Excel = ExcelPropety.CreateProperty<WmsStock>(x => x.locNo);
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<WmsStock>(x => x.palletBarcode);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsStock>(x => x.proprietorCode);
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<WmsStock>(x => x.regionNo);
        public ExcelPropety roadwayNo_Excel = ExcelPropety.CreateProperty<WmsStock>(x => x.roadwayNo);
        public ExcelPropety specialFlag_Excel = ExcelPropety.CreateProperty<WmsStock>(x => x.specialFlag);
        public ExcelPropety specialMsg_Excel = ExcelPropety.CreateProperty<WmsStock>(x => x.specialMsg);
        public ExcelPropety stockCode_Excel = ExcelPropety.CreateProperty<WmsStock>(x => x.stockCode);
        public ExcelPropety stockStatus_Excel = ExcelPropety.CreateProperty<WmsStock>(x => x.stockStatus);
        public ExcelPropety weight_Excel = ExcelPropety.CreateProperty<WmsStock>(x => x.weight);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsStock>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsStockImportVM : BaseImportVM<WmsStockTemplateVM, WmsStock>
    {

    }

}
