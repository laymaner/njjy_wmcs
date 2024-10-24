using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessPutaway.WmsPutawayHisVMs
{
    public partial class WmsPutawayHisTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsPutawayHis>(x => x.areaNo);
        public ExcelPropety loadedType_Excel = ExcelPropety.CreateProperty<WmsPutawayHis>(x => x.loadedType);
        public ExcelPropety manualFlag_Excel = ExcelPropety.CreateProperty<WmsPutawayHis>(x => x.manualFlag);
        public ExcelPropety onlineLocNo_Excel = ExcelPropety.CreateProperty<WmsPutawayHis>(x => x.onlineLocNo);
        public ExcelPropety onlineMethod_Excel = ExcelPropety.CreateProperty<WmsPutawayHis>(x => x.onlineMethod);
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<WmsPutawayHis>(x => x.palletBarcode);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsPutawayHis>(x => x.proprietorCode);
        public ExcelPropety ptaRegionNo_Excel = ExcelPropety.CreateProperty<WmsPutawayHis>(x => x.ptaRegionNo);
        public ExcelPropety putawayNo_Excel = ExcelPropety.CreateProperty<WmsPutawayHis>(x => x.putawayNo);
        public ExcelPropety putawayStatus_Excel = ExcelPropety.CreateProperty<WmsPutawayHis>(x => x.putawayStatus);
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<WmsPutawayHis>(x => x.regionNo);
        public ExcelPropety stockCode_Excel = ExcelPropety.CreateProperty<WmsPutawayHis>(x => x.stockCode);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsPutawayHis>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class WmsPutawayHisImportVM : BaseImportVM<WmsPutawayHisTemplateVM, WmsPutawayHis>
    {

    }

}
