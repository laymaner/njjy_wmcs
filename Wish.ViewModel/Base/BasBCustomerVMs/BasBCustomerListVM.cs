using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Base;


namespace Wish.ViewModel.Base.BasBCustomerVMs
{
    public partial class BasBCustomerListVM : BasePagedListVM<BasBCustomer_View, BasBCustomerSearcher>
    {

        protected override IEnumerable<IGridColumn<BasBCustomer_View>> InitGridHeader()
        {
            return new List<GridColumn<BasBCustomer_View>>{
                this.MakeGridHeader(x => x.Address),
                this.MakeGridHeader(x => x.Contacts),
                this.MakeGridHeader(x => x.CustomerFullname),
                this.MakeGridHeader(x => x.CustomerFullnameAlias),
                this.MakeGridHeader(x => x.CustomerFullnameEn),
                this.MakeGridHeader(x => x.CustomerName),
                this.MakeGridHeader(x => x.CustomerNameAlias),
                this.MakeGridHeader(x => x.CustomerNameEn),
                this.MakeGridHeader(x => x.CustomerCode),
                this.MakeGridHeader(x => x.Description),
                this.MakeGridHeader(x => x.Fax),
                this.MakeGridHeader(x => x.Mail),
                this.MakeGridHeader(x => x.Mobile),
                this.MakeGridHeader(x => x.Phone),
                this.MakeGridHeader(x => x.ProprietorCode),
                this.MakeGridHeader(x => x.UsedFlag),
                this.MakeGridHeader(x => x.WhouseNo),
                this.MakeGridHeader(x => x.Zip),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasBCustomer_View> GetSearchQuery()
        {
            var query = DC.Set<BasBCustomer>()
                .CheckContain(Searcher.Address, x=>x.Address)
                .CheckContain(Searcher.Contacts, x=>x.Contacts)
                .CheckContain(Searcher.CustomerFullname, x=>x.CustomerFullname)
                .CheckContain(Searcher.CustomerName, x=>x.CustomerName)
                .CheckContain(Searcher.CustomerCode, x=>x.CustomerCode)
                .CheckContain(Searcher.Description, x=>x.Description)
                .CheckContain(Searcher.Fax, x=>x.Fax)
                .CheckContain(Searcher.Mail, x=>x.Mail)
                .CheckContain(Searcher.Mobile, x=>x.Mobile)
                .CheckContain(Searcher.Phone, x=>x.Phone)
                .CheckContain(Searcher.ProprietorCode, x=>x.ProprietorCode)
                .CheckContain(Searcher.WhouseNo, x=>x.WhouseNo)
                .Select(x => new BasBCustomer_View
                {
				    ID = x.ID,
                    Address = x.Address,
                    Contacts = x.Contacts,
                    CustomerFullname = x.CustomerFullname,
                    CustomerFullnameAlias = x.CustomerFullnameAlias,
                    CustomerFullnameEn = x.CustomerFullnameEn,
                    CustomerName = x.CustomerName,
                    CustomerNameAlias = x.CustomerNameAlias,
                    CustomerNameEn = x.CustomerNameEn,
                    CustomerCode = x.CustomerCode,
                    Description = x.Description,
                    Fax = x.Fax,
                    Mail = x.Mail,
                    Mobile = x.Mobile,
                    Phone = x.Phone,
                    ProprietorCode = x.ProprietorCode,
                    UsedFlag = x.UsedFlag,
                    WhouseNo = x.WhouseNo,
                    Zip = x.Zip,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasBCustomer_View : BasBCustomer{

    }
}
