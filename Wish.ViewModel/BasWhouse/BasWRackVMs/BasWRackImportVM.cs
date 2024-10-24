using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWRackVMs
{
    public partial class BasWRackTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<BasWRack>(x => x.areaNo);
        public ExcelPropety isInEnable_Excel = ExcelPropety.CreateProperty<BasWRack>(x => x.isInEnable);
        public ExcelPropety isOutEnable_Excel = ExcelPropety.CreateProperty<BasWRack>(x => x.isOutEnable);
        public ExcelPropety rackIdx_Excel = ExcelPropety.CreateProperty<BasWRack>(x => x.rackIdx);
        public ExcelPropety rackName_Excel = ExcelPropety.CreateProperty<BasWRack>(x => x.rackName);
        public ExcelPropety rackNameAlias_Excel = ExcelPropety.CreateProperty<BasWRack>(x => x.rackNameAlias);
        public ExcelPropety rackNameEn_Excel = ExcelPropety.CreateProperty<BasWRack>(x => x.rackNameEn);
        public ExcelPropety rackNo_Excel = ExcelPropety.CreateProperty<BasWRack>(x => x.rackNo);
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<BasWRack>(x => x.regionNo);
        public ExcelPropety roadwayNo_Excel = ExcelPropety.CreateProperty<BasWRack>(x => x.roadwayNo);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<BasWRack>(x => x.usedFlag);
        public ExcelPropety virtualFlag_Excel = ExcelPropety.CreateProperty<BasWRack>(x => x.virtualFlag);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<BasWRack>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class BasWRackImportVM : BaseImportVM<BasWRackTemplateVM, BasWRack>
    {

    }

}
