using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBProprietorVMs
{
    public partial class BasBProprietorTemplateVM : BaseTemplateVM
    {
        public ExcelPropety address_Excel = ExcelPropety.CreateProperty<BasBProprietor>(x => x.address);
        public ExcelPropety contacts_Excel = ExcelPropety.CreateProperty<BasBProprietor>(x => x.contacts);
        public ExcelPropety description_Excel = ExcelPropety.CreateProperty<BasBProprietor>(x => x.description);
        public ExcelPropety fax_Excel = ExcelPropety.CreateProperty<BasBProprietor>(x => x.fax);
        public ExcelPropety mail_Excel = ExcelPropety.CreateProperty<BasBProprietor>(x => x.mail);
        public ExcelPropety mobile_Excel = ExcelPropety.CreateProperty<BasBProprietor>(x => x.mobile);
        public ExcelPropety phone_Excel = ExcelPropety.CreateProperty<BasBProprietor>(x => x.phone);
        public ExcelPropety proprietorCode_Excel = ExcelPropety.CreateProperty<BasBProprietor>(x => x.proprietorCode);
        public ExcelPropety proprietorFullname_Excel = ExcelPropety.CreateProperty<BasBProprietor>(x => x.proprietorFullname);
        public ExcelPropety proprietorFullnameAlias_Excel = ExcelPropety.CreateProperty<BasBProprietor>(x => x.proprietorFullnameAlias);
        public ExcelPropety proprietorFullnameEn_Excel = ExcelPropety.CreateProperty<BasBProprietor>(x => x.proprietorFullnameEn);
        public ExcelPropety proprietorName_Excel = ExcelPropety.CreateProperty<BasBProprietor>(x => x.proprietorName);
        public ExcelPropety proprietorNameAlias_Excel = ExcelPropety.CreateProperty<BasBProprietor>(x => x.proprietorNameAlias);
        public ExcelPropety proprietorNameEn_Excel = ExcelPropety.CreateProperty<BasBProprietor>(x => x.proprietorNameEn);
        public ExcelPropety usedFlag_Excel = ExcelPropety.CreateProperty<BasBProprietor>(x => x.usedFlag);
        public ExcelPropety zip_Excel = ExcelPropety.CreateProperty<BasBProprietor>(x => x.zip);

	    protected override void InitVM()
        {
        }

    }

    public class BasBProprietorImportVM : BaseImportVM<BasBProprietorTemplateVM, BasBProprietor>
    {

    }

}
