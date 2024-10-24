using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.System.Model;


namespace Wish.ViewModel.System.SysSequenceVMs
{
    public partial class SysSequenceTemplateVM : BaseTemplateVM
    {
        public ExcelPropety SeqCode_Excel = ExcelPropety.CreateProperty<SysSequence>(x => x.SeqCode);
        public ExcelPropety SeqDesc_Excel = ExcelPropety.CreateProperty<SysSequence>(x => x.SeqDesc);
        public ExcelPropety SeqType_Excel = ExcelPropety.CreateProperty<SysSequence>(x => x.SeqType);
        public ExcelPropety NowSn_Excel = ExcelPropety.CreateProperty<SysSequence>(x => x.NowSn);
        public ExcelPropety MinSn_Excel = ExcelPropety.CreateProperty<SysSequence>(x => x.MinSn);
        public ExcelPropety MaxSn_Excel = ExcelPropety.CreateProperty<SysSequence>(x => x.MaxSn);
        public ExcelPropety SeqSnLen_Excel = ExcelPropety.CreateProperty<SysSequence>(x => x.SeqSnLen);
        public ExcelPropety SeqPrefix_Excel = ExcelPropety.CreateProperty<SysSequence>(x => x.SeqPrefix);
        public ExcelPropety SeqDate_Excel = ExcelPropety.CreateProperty<SysSequence>(x => x.SeqDate);

	    protected override void InitVM()
        {
        }

    }

    public class SysSequenceImportVM : BaseImportVM<SysSequenceTemplateVM, SysSequence>
    {

    }

}
