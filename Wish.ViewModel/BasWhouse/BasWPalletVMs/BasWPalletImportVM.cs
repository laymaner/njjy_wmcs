using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWPalletVMs
{
    public partial class BasWPalletTemplateVM : BaseTemplateVM
    {
        public ExcelPropety palletBarcode_Excel = ExcelPropety.CreateProperty<BasWPallet>(x => x.palletBarcode);
        public ExcelPropety palletDesc_Excel = ExcelPropety.CreateProperty<BasWPallet>(x => x.palletDesc);
        public ExcelPropety palletDescAlias_Excel = ExcelPropety.CreateProperty<BasWPallet>(x => x.palletDescAlias);
        public ExcelPropety palletDescEn_Excel = ExcelPropety.CreateProperty<BasWPallet>(x => x.palletDescEn);
        public ExcelPropety palletTypeCode_Excel = ExcelPropety.CreateProperty<BasWPallet>(x => x.palletTypeCode);
        public ExcelPropety printsQty_Excel = ExcelPropety.CreateProperty<BasWPallet>(x => x.printsQty);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<BasWPallet>(x => x.usedFlag);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<BasWPallet>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class BasWPalletImportVM : BaseImportVM<BasWPalletTemplateVM, BasWPallet>
    {

    }

}
