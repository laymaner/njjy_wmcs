using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWAreaVMs
{
    public partial class BasWAreaTemplateVM : BaseTemplateVM
    {
        public ExcelPropety areaName_Excel = ExcelPropety.CreateProperty<BasWArea>(x => x.areaName);
        public ExcelPropety areaNameAlias_Excel = ExcelPropety.CreateProperty<BasWArea>(x => x.areaNameAlias);
        public ExcelPropety areaNameEn_Excel = ExcelPropety.CreateProperty<BasWArea>(x => x.areaNameEn);
        public ExcelPropety areaNo_Excel = ExcelPropety.CreateProperty<BasWArea>(x => x.areaNo);
        public ExcelPropety areaType_Excel = ExcelPropety.CreateProperty<BasWArea>(x => x.areaType);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<BasWArea>(x => x.usedFlag);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<BasWArea>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class BasWAreaImportVM : BaseImportVM<BasWAreaTemplateVM, BasWArea>
    {

    }

}
