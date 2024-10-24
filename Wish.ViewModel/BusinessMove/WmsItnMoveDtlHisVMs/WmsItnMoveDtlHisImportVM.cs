using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Business;


namespace Wish.ViewModel.BusinessMove.WmsItnMoveDtlHisVMs
{
    public partial class WmsItnMoveDtlHisTemplateVM : BaseTemplateVM
    {
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<WmsItnMoveDtlHis>(x => x.whouseNo);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<WmsItnMoveDtlHis>(x => x.proprietorCode);
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<WmsItnMoveDtlHis>(x => x.areaNo);
        public ExcelPropety moveNo_Excel = ExcelPropety.CreateProperty<WmsItnMoveDtlHis>(x => x.moveNo);
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<WmsItnMoveDtlHis>(x => x.regionNo);
        public ExcelPropety roadwayNo_Excel = ExcelPropety.CreateProperty<WmsItnMoveDtlHis>(x => x.roadwayNo);
        public ExcelPropety binNo_Excel = ExcelPropety.CreateProperty<WmsItnMoveDtlHis>(x => x.binNo);
        public ExcelPropety stockCode_Excel = ExcelPropety.CreateProperty<WmsItnMoveDtlHis>(x => x.stockCode);
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<WmsItnMoveDtlHis>(x => x.palletBarcode);
        public ExcelPropety moveDtlStatus_Excel = ExcelPropety.CreateProperty<WmsItnMoveDtlHis>(x => x.moveDtlStatus);
        public ExcelPropety loadedType_Excel = ExcelPropety.CreateProperty<WmsItnMoveDtlHis>(x => x.loadedType);
        public ExcelPropety moveQty_Excel = ExcelPropety.CreateProperty<WmsItnMoveDtlHis>(x => x.moveQty);
        public ExcelPropety createBy_Excel = ExcelPropety.CreateProperty<WmsItnMoveDtlHis>(x => x.createBy);
        public ExcelPropety createTime_Excel = ExcelPropety.CreateProperty<WmsItnMoveDtlHis>(x => x.createTime);
        public ExcelPropety updateBy_Excel = ExcelPropety.CreateProperty<WmsItnMoveDtlHis>(x => x.updateBy);
        public ExcelPropety updateTime_Excel = ExcelPropety.CreateProperty<WmsItnMoveDtlHis>(x => x.updateTime);

	    protected override void InitVM()
        {
        }

    }

    public class WmsItnMoveDtlHisImportVM : BaseImportVM<WmsItnMoveDtlHisTemplateVM, WmsItnMoveDtlHis>
    {

    }

}
