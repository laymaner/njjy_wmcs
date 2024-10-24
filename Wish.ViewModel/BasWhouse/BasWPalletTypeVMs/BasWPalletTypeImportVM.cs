using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Areas.BasWhouse.Model;


namespace Wish.ViewModel.BasWhouse.BasWPalletTypeVMs
{
    public partial class BasWPalletTypeTemplateVM : BaseTemplateVM
    {
        public ExcelPropety barcodeFlag_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.barcodeFlag);
        public ExcelPropety checkFormula_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.checkFormula);
        public ExcelPropety checkPalletFlag_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.checkPalletFlag);
        public ExcelPropety chekDesc_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.chekDesc);
        public ExcelPropety developFlag_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.developFlag);
        public ExcelPropety emptyMaxQty_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.emptyMaxQty);
        public ExcelPropety maxWeight_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.maxWeight);
        public ExcelPropety palletHeight_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.palletHeight);
        public ExcelPropety palletLength_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.palletLength);
        public ExcelPropety palletTypeCode_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.palletTypeCode);
        public ExcelPropety palletTypeFlag_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.palletTypeFlag);
        public ExcelPropety palletTypeName_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.palletTypeName);
        public ExcelPropety palletTypeNameAlias_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.palletTypeNameAlias);
        public ExcelPropety palletTypeNameEn_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.palletTypeNameEn);
        public ExcelPropety palletWeight_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.palletWeight);
        public ExcelPropety palletWidth_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.palletWidth);
        public ExcelPropety positionCol_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.positionCol);
        public ExcelPropety positionDirect_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.positionDirect);
        public ExcelPropety positionFlag_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.positionFlag);
        public ExcelPropety positionRow_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.positionRow);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.usedFlag);
        public ExcelPropety whouseNo_Excel = ExcelPropety.CreateProperty<BasWPalletType>(x => x.whouseNo);

	    protected override void InitVM()
        {
        }

    }

    public class BasWPalletTypeImportVM : BaseImportVM<BasWPalletTypeTemplateVM, BasWPalletType>
    {

    }

}
