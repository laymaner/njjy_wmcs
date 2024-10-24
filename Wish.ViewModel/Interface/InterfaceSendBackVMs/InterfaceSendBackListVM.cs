using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Wish.Model.Interface;
using Wish.Model.System;


namespace Wish.ViewModel.Interface.InterfaceSendBackVMs
{
    public partial class InterfaceSendBackListVM : BasePagedListVM<InterfaceSendBack_View, InterfaceSendBackSearcher>
    {

        protected override IEnumerable<IGridColumn<InterfaceSendBack_View>> InitGridHeader()
        {
            return new List<GridColumn<InterfaceSendBack_View>>{
                this.MakeGridHeader(x => x.interfaceCode),
                this.MakeGridHeader(x => x.interfaceName),
                this.MakeGridHeader(x => x.interfaceSendInfo),
                this.MakeGridHeader(x => x.interfaceResult),
                this.MakeGridHeader(x => x.returnFlag),
                this.MakeGridHeader(x => x.returnTimes),
                this.MakeGridHeader(x => x.CreateTime),
                this.MakeGridHeader(x => x.CreateBy),
                this.MakeGridHeader(x => x.UpdateBy),
                this.MakeGridHeader(x => x.UpdateTime),
                this.MakeGridHeader(x => x.returnFlagDesc),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        public List<SysDictionary> dicEntities = new List<SysDictionary>();
        public override IOrderedQueryable<InterfaceSendBack_View> GetSearchQuery()
        {
            var query = DC.Set<InterfaceSendBack>()
                .CheckContain(Searcher.interfaceCode, x=>x.interfaceCode)
                .CheckContain(Searcher.interfaceName, x=>x.interfaceName)
                .CheckContain(Searcher.interfaceSendInfo, x=>x.interfaceSendInfo)
                .CheckContain(Searcher.interfaceResult, x=>x.interfaceResult)
                .CheckEqual(Searcher.returnFlag, x=>x.returnFlag)
                .CheckEqual(Searcher.returnTimes, x=>x.returnTimes)
                .Select(x => new InterfaceSendBack_View
                {
				    ID = x.ID,
                    interfaceCode = x.interfaceCode,
                    interfaceName = x.interfaceName,
                    interfaceSendInfo = x.interfaceSendInfo,
                    interfaceResult = x.interfaceResult,
                    returnFlag = x.returnFlag,
                    returnTimes = x.returnTimes,
                    CreateBy = x.CreateBy,
                    CreateTime = x.CreateTime,
                    UpdateBy = x.UpdateBy,
                    UpdateTime = x.UpdateTime,
                })
                .OrderBy(x => x.ID).ToList();
            dicEntities = DC.Set<SysDictionary>().AsNoTracking().ToList();
            foreach (var item in query)
            {
                var entityCmd = dicEntities.FirstOrDefault(t => t.dictionaryCode == "RETURN_INTER" && t.dictionaryItemCode == item.returnFlag.ToString());
                if (entityCmd != null)
                {
                    item.returnFlagDesc = entityCmd.dictionaryItemName;
                }
            }
            var queryList = query.AsQueryable().OrderByDescending(x => x.CreateTime);
            return queryList;
        }

    }

    public class InterfaceSendBack_View : InterfaceSendBack{
        public string returnFlagDesc {  get; set; }
    }
}
