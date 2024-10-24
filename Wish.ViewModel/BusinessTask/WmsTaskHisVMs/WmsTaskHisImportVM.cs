using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessTask.WmsTaskHisVMs
{
    public partial class WmsTaskHisTemplateVM : BaseTemplateVM
    {
        public ExcelPropety feedbackDesc_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.feedbackDesc);
        public ExcelPropety feedbackStatus_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.feedbackStatus);
        public ExcelPropety frLocationNo_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.frLocationNo);
        public ExcelPropety frLocationType_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.frLocationType);
        public ExcelPropety loadedType_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.loadedType);
        public ExcelPropety matHeight_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.matHeight);
        public ExcelPropety matLength_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.matLength);
        public ExcelPropety matQty_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.matQty);
        public ExcelPropety matWeight_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.matWeight);
        public ExcelPropety matWidth_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.matWidth);
        public ExcelPropety orderNo_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.orderNo);
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.palletBarcode);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.proprietorCode);
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.regionNo);
        public ExcelPropety roadwayNo_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.roadwayNo);
        public ExcelPropety stockCode_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.stockCode);
        public ExcelPropety taskDesc_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.taskDesc);
        public ExcelPropety taskPriority_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.taskPriority);
        public ExcelPropety taskStatus_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.taskStatus);
        public ExcelPropety taskTypeNo_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.taskTypeNo);
        public ExcelPropety toLocationNo_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.toLocationNo);
        public ExcelPropety toLocationType_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.toLocationType);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.whouseNo);
        public ExcelPropety wmsTaskNo_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.wmsTaskNo);
        public ExcelPropety wmsTaskType_Excel = ExcelPropety.CreateProperty<WmsTaskHis>(x => x.wmsTaskType);

	    protected override void InitVM()
        {
        }

    }

    public class WmsTaskHisImportVM : BaseImportVM<WmsTaskHisTemplateVM, WmsTaskHis>
    {

    }

}
