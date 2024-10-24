using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Wish.HWConfig.Models;


namespace Wish.ViewModel.DevConfig.DBConfigVMs
{
    public partial class DBConfigTemplateVM : BaseTemplateVM
    {
        [Display(Name = "DBInfo.DBNo")]
        public ExcelPropety Block_Code_Excel = ExcelPropety.CreateProperty<DBConfig>(x => x.Block_Code);
        [Display(Name = "DBInfo.DBName")]
        public ExcelPropety Block_Name_Excel = ExcelPropety.CreateProperty<DBConfig>(x => x.Block_Name);
        [Display(Name = "DBInfo.Offset")]
        public ExcelPropety Block_Offset_Excel = ExcelPropety.CreateProperty<DBConfig>(x => x.Block_Offset);
        [Display(Name = "DBInfo.TotalLength")]
        public ExcelPropety Block_Length_Excel = ExcelPropety.CreateProperty<DBConfig>(x => x.Block_Length);
        public ExcelPropety PlcConfig_Excel = ExcelPropety.CreateProperty<DBConfig>(x => x.PlcConfigId);
        [Display(Name = "DeviceInfo.Remark")]
        public ExcelPropety Describe_Excel = ExcelPropety.CreateProperty<DBConfig>(x => x.Describe);

	    protected override void InitVM()
        {
            PlcConfig_Excel.DataType = ColumnDataType.ComboBox;
            PlcConfig_Excel.ListItems = DC.Set<PlcConfig>().GetSelectListItems(Wtm, y => y.Plc_Name);
        }

    }

    public class DBConfigImportVM : BaseImportVM<DBConfigTemplateVM, DBConfig>
    {

    }

}
