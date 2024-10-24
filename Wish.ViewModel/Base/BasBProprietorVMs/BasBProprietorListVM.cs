using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBProprietorVMs
{
    public partial class BasBProprietorListVM : BasePagedListVM<BasBProprietor_View, BasBProprietorSearcher>
    {

        protected override IEnumerable<IGridColumn<BasBProprietor_View>> InitGridHeader()
        {
            return new List<GridColumn<BasBProprietor_View>>{
                this.MakeGridHeader(x => x.address),
                this.MakeGridHeader(x => x.contacts),
                this.MakeGridHeader(x => x.description),
                this.MakeGridHeader(x => x.fax),
                this.MakeGridHeader(x => x.mail),
                this.MakeGridHeader(x => x.mobile),
                this.MakeGridHeader(x => x.phone),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.proprietorFullname),
                this.MakeGridHeader(x => x.proprietorFullnameAlias),
                this.MakeGridHeader(x => x.proprietorFullnameEn),
                this.MakeGridHeader(x => x.proprietorName),
                this.MakeGridHeader(x => x.proprietorNameAlias),
                this.MakeGridHeader(x => x.proprietorNameEn),
                this.MakeGridHeader(x => x.usedFlag),
                this.MakeGridHeader(x => x.zip),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasBProprietor_View> GetSearchQuery()
        {
            var query = DC.Set<BasBProprietor>()
                .CheckContain(Searcher.address, x=>x.address)
                .CheckContain(Searcher.contacts, x=>x.contacts)
                .CheckContain(Searcher.description, x=>x.description)
                .CheckContain(Searcher.mail, x=>x.mail)
                .CheckContain(Searcher.mobile, x=>x.mobile)
                .CheckContain(Searcher.phone, x=>x.phone)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.proprietorName, x=>x.proprietorName)
                .CheckEqual(Searcher.usedFlag, x=>x.usedFlag)
                .Select(x => new BasBProprietor_View
                {
				    ID = x.ID,
                    address = x.address,
                    contacts = x.contacts,
                    description = x.description,
                    fax = x.fax,
                    mail = x.mail,
                    mobile = x.mobile,
                    phone = x.phone,
                    proprietorCode = x.proprietorCode,
                    proprietorFullname = x.proprietorFullname,
                    proprietorFullnameAlias = x.proprietorFullnameAlias,
                    proprietorFullnameEn = x.proprietorFullnameEn,
                    proprietorName = x.proprietorName,
                    proprietorNameAlias = x.proprietorNameAlias,
                    proprietorNameEn = x.proprietorNameEn,
                    usedFlag = x.usedFlag,
                    zip = x.zip,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasBProprietor_View : BasBProprietor{

    }
}
