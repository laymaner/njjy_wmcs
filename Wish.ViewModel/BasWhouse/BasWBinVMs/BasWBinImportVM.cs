using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWBinVMs
{
    public partial class BasWBinTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.areaNo);
        public ExcelPropety bearWeight_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.bearWeight);
        public ExcelPropety binCol_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.binCol);
        public ExcelPropety binErrFlag_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.binErrFlag);
        public ExcelPropety binErrMsg_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.binErrMsg);
        public ExcelPropety binGroupIdx_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.binGroupIdx);
        public ExcelPropety binGroupNo_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.binGroupNo);
        public ExcelPropety binHeight_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.binHeight);
        public ExcelPropety binLayer_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.binLayer);
        public ExcelPropety binLength_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.binLength);
        public ExcelPropety binName_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.binName);
        public ExcelPropety binNameAlias_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.binNameAlias);
        public ExcelPropety binNameEn_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.binNameEn);
        public ExcelPropety binNo_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.binNo);
        public ExcelPropety binPriority_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.binPriority);
        public ExcelPropety binRow_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.binRow);
        public ExcelPropety binType_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.binType);
        public ExcelPropety binWidth_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.binWidth);
        public ExcelPropety capacitySize_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.capacitySize);
        public ExcelPropety extensionGroupNo_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.extensionGroupNo);
        public ExcelPropety extensionIdx_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.extensionIdx);
        public ExcelPropety fireFlag_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.fireFlag);
        public ExcelPropety isInEnable_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.isInEnable);
        public ExcelPropety isOutEnable_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.isOutEnable);
        public ExcelPropety isValidityPeriod_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.isValidityPeriod);
        public ExcelPropety palletDirect_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.palletDirect);
        public ExcelPropety rackNo_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.rackNo);
        public ExcelPropety regionNo_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.regionNo);
        public ExcelPropety roadwayNo_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.roadwayNo);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.usedFlag);
        public ExcelPropety virtualFlag_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.virtualFlag);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<BasWBin>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class BasWBinImportVM : BaseImportVM<BasWBinTemplateVM, BasWBin>
    {

    }

}
