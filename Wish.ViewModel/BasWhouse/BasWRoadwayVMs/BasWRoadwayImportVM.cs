using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWRoadwayVMs
{
    public partial class BasWRoadwayTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<BasWRoadway>(x => x.areaNo);
        public ExcelPropety errFlag_Excel = ExcelPropety.CreateProperty<BasWRoadway>(x => x.errFlag);
        public ExcelPropety errMsg_Excel = ExcelPropety.CreateProperty<BasWRoadway>(x => x.errMsg);
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<BasWRoadway>(x => x.regionNo);
        public ExcelPropety reservedQty_Excel = ExcelPropety.CreateProperty<BasWRoadway>(x => x.reservedQty);
        public ExcelPropety roadwayName_Excel = ExcelPropety.CreateProperty<BasWRoadway>(x => x.roadwayName);
        public ExcelPropety roadwayNameAlias_Excel = ExcelPropety.CreateProperty<BasWRoadway>(x => x.roadwayNameAlias);
        public ExcelPropety roadwayNameEn_Excel = ExcelPropety.CreateProperty<BasWRoadway>(x => x.roadwayNameEn);
        public ExcelPropety roadwayNo_Excel = ExcelPropety.CreateProperty<BasWRoadway>(x => x.roadwayNo);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<BasWRoadway>(x => x.usedFlag);
        public ExcelPropety virtualFlag_Excel = ExcelPropety.CreateProperty<BasWRoadway>(x => x.virtualFlag);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<BasWRoadway>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class BasWRoadwayImportVM : BaseImportVM<BasWRoadwayTemplateVM, BasWRoadway>
    {

    }

}
