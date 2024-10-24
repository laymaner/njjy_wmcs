using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWRegionVMs
{
    public partial class BasWRegionTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<BasWRegion>(x => x.areaNo);
        public ExcelPropety manualFlag_Excel = ExcelPropety.CreateProperty<BasWRegion>(x => x.manualFlag);
        public ExcelPropety palletMgt_Excel = ExcelPropety.CreateProperty<BasWRegion>(x => x.palletMgt);
        public ExcelPropety pickupMethod_Excel = ExcelPropety.CreateProperty<BasWRegion>(x => x.pickupMethod);
        public ExcelPropety regionName_Excel = ExcelPropety.CreateProperty<BasWRegion>(x => x.regionName);
        public ExcelPropety regionNameAlias_Excel = ExcelPropety.CreateProperty<BasWRegion>(x => x.regionNameAlias);
        public ExcelPropety regionNameEn_Excel = ExcelPropety.CreateProperty<BasWRegion>(x => x.regionNameEn);
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<BasWRegion>(x => x.regionNo);
        public ExcelPropety regionTypeCode_Excel = ExcelPropety.CreateProperty<BasWRegion>(x => x.regionTypeCode);
        public ExcelPropety sdType_Excel = ExcelPropety.CreateProperty<BasWRegion>(x => x.sdType);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<BasWRegion>(x => x.usedFlag);
        public ExcelPropety virtualFlag_Excel = ExcelPropety.CreateProperty<BasWRegion>(x => x.virtualFlag);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<BasWRegion>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class BasWRegionImportVM : BaseImportVM<BasWRegionTemplateVM, BasWRegion>
    {

    }

}
