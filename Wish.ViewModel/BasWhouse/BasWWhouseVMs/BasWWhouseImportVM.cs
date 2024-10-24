using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWWhouseVMs
{
    public partial class BasWWhouseTemplateVM : BaseTemplateVM
    {
        public ExcelPropety contacts_Excel = ExcelPropety.CreateProperty<BasWWhouse>(x => x.contacts);
        public ExcelPropety maxTaskQty_Excel = ExcelPropety.CreateProperty<BasWWhouse>(x => x.maxTaskQty);
        public ExcelPropety telephone_Excel = ExcelPropety.CreateProperty<BasWWhouse>(x => x.telephone);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<BasWWhouse>(x => x.usedFlag);
        public ExcelPropety whouseAddress_Excel = ExcelPropety.CreateProperty<BasWWhouse>(x => x.whouseAddress);
        public ExcelPropety whouseName_Excel = ExcelPropety.CreateProperty<BasWWhouse>(x => x.whouseName);
        public ExcelPropety whouseNameAlias_Excel = ExcelPropety.CreateProperty<BasWWhouse>(x => x.whouseNameAlias);
        public ExcelPropety whouseNameEn_Excel = ExcelPropety.CreateProperty<BasWWhouse>(x => x.whouseNameEn);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<BasWWhouse>(x => x.whouseNo);
        public ExcelPropety whouseType_Excel = ExcelPropety.CreateProperty<BasWWhouse>(x => x.whouseType);

	    protected override void InitVM()
        {
        }

    }

    public class BasWWhouseImportVM : BaseImportVM<BasWWhouseTemplateVM, BasWWhouse>
    {

    }

}
