using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.DevConfig;


namespace Wish.ViewModel.DevConfig.MotDevPartsVMs
{
    public partial class MotDevPartsTemplateVM : BaseTemplateVM
    {
        public ExcelPropety AreaNo_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.AreaNo);
        public ExcelPropety DevNo_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.DevNo);
        public ExcelPropety PartNo_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.PartNo);
        public ExcelPropety PartLocNo_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.PartLocNo);
        public ExcelPropety DevRunMode_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.DevRunMode);
        public ExcelPropety SrmRoadway_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.SrmRoadway);
        public ExcelPropety SrmForkType_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.SrmForkType);
        public ExcelPropety SrmExecStep_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.SrmExecStep);
        public ExcelPropety IsInSitu_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.IsInSitu);
        public ExcelPropety IsAlarming_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.IsAlarming);
        public ExcelPropety AlarmCode_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.AlarmCode);
        public ExcelPropety IsFree_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.IsFree);
        public ExcelPropety IsHasGoods_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.IsHasGoods);
        public ExcelPropety CmdNo_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.CmdNo);
        public ExcelPropety PalletNo_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.PalletNo);
        public ExcelPropety OldPalletNo_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.OldPalletNo);
        public ExcelPropety ReadPalletNo_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.ReadPalletNo);
        public ExcelPropety StationNo_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.StationNo);
        public ExcelPropety CurrentX_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.CurrentX);
        public ExcelPropety CurrentY_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.CurrentY);
        public ExcelPropety CurrentZ_Excel = ExcelPropety.CreateProperty<MotDevParts>(x => x.CurrentZ);

	    protected override void InitVM()
        {
        }

    }

    public class MotDevPartsImportVM : BaseImportVM<MotDevPartsTemplateVM, MotDevParts>
    {

    }

}
