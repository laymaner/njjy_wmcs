using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessPutaway.WmsPutawayVMs
{
    public partial class WmsPutawayTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsPutaway>(x => x.areaNo);
        public ExcelPropety loadedType_Excel = ExcelPropety.CreateProperty<WmsPutaway>(x => x.loadedType);
        public ExcelPropety manualFlag_Excel = ExcelPropety.CreateProperty<WmsPutaway>(x => x.manualFlag);
        public ExcelPropety onlineLocNo_Excel = ExcelPropety.CreateProperty<WmsPutaway>(x => x.onlineLocNo);
        public ExcelPropety onlineMethod_Excel = ExcelPropety.CreateProperty<WmsPutaway>(x => x.onlineMethod);
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<WmsPutaway>(x => x.palletBarcode);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsPutaway>(x => x.proprietorCode);
        public ExcelPropety ptaRegionNo_Excel = ExcelPropety.CreateProperty<WmsPutaway>(x => x.ptaRegionNo);
        public ExcelPropety putawayNo_Excel = ExcelPropety.CreateProperty<WmsPutaway>(x => x.putawayNo);
        public ExcelPropety putawayStatus_Excel = ExcelPropety.CreateProperty<WmsPutaway>(x => x.putawayStatus);
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<WmsPutaway>(x => x.regionNo);
        public ExcelPropety stockCode_Excel = ExcelPropety.CreateProperty<WmsPutaway>(x => x.stockCode);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsPutaway>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsPutawayImportVM : BaseImportVM<WmsPutawayTemplateVM, WmsPutaway>
    {

    }

}
