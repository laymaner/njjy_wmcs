using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.Model.DevConfig;


namespace Wish.ViewModel.DevConfig.MotDevPartsHisVMs
{
    public partial class MotDevPartsHisTemplateVM : BaseTemplateVM
    {
        public ExcelPropety AreaNo_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.AreaNo);
        public ExcelPropety DevNo_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.DevNo);
        public ExcelPropety PartNo_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.PartNo);
        public ExcelPropety PartLocNo_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.PartLocNo);
        public ExcelPropety DevRunMode_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.DevRunMode);
        public ExcelPropety SrmRoadway_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.SrmRoadway);
        public ExcelPropety SrmForkType_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.SrmForkType);
        public ExcelPropety SrmExecStep_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.SrmExecStep);
        public ExcelPropety IsInSitu_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.IsInSitu);
        public ExcelPropety IsAlarming_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.IsAlarming);
        public ExcelPropety AlarmCode_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.AlarmCode);
        public ExcelPropety IsFree_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.IsFree);
        public ExcelPropety IsHasGoods_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.IsHasGoods);
        public ExcelPropety CmdNo_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.CmdNo);
        public ExcelPropety PalletNo_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.PalletNo);
        public ExcelPropety OldPalletNo_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.OldPalletNo);
        public ExcelPropety ReadPalletNo_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.ReadPalletNo);
        public ExcelPropety StationNo_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.StationNo);
        public ExcelPropety CurrentX_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.CurrentX);
        public ExcelPropety CurrentY_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.CurrentY);
        public ExcelPropety CurrentZ_Excel = ExcelPropety.CreateProperty<MotDevPartsHis>(x => x.CurrentZ);

	    protected override void InitVM()
        {
        }

    }

    public class MotDevPartsHisImportVM : BaseImportVM<MotDevPartsHisTemplateVM, MotDevPartsHis>
    {

    }

}
