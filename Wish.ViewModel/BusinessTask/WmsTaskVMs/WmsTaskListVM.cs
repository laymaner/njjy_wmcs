using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Com.Wish.Model.Business;
using Wish.Model.System;


namespace Wish.ViewModel.BusinessTask.WmsTaskVMs
{
    public partial class WmsTaskListVM : BasePagedListVM<WmsTask_View, WmsTaskSearcher>
    {

        protected override IEnumerable<IGridColumn<WmsTask_View>> InitGridHeader()
        {
            return new List<GridColumn<WmsTask_View>>{
                this.MakeGridHeader(x => x.feedbackDesc),
                this.MakeGridHeader(x => x.feedbackStatus),
                this.MakeGridHeader(x => x.frLocationNo),
                this.MakeGridHeader(x => x.frLocationType),
                this.MakeGridHeader(x => x.loadedType),
                this.MakeGridHeader(x => x.matHeight),
                this.MakeGridHeader(x => x.matLength),
                this.MakeGridHeader(x => x.matQty),
                this.MakeGridHeader(x => x.matWeight),
                this.MakeGridHeader(x => x.matWidth),
                this.MakeGridHeader(x => x.orderNo),
                this.MakeGridHeader(x => x.palletBarcode),
                this.MakeGridHeader(x => x.proprietorCode),
                this.MakeGridHeader(x => x.regionNo),
                this.MakeGridHeader(x => x.roadwayNo),
                this.MakeGridHeader(x => x.stockCode),
                this.MakeGridHeader(x => x.taskDesc),
                this.MakeGridHeader(x => x.taskPriority),
                this.MakeGridHeader(x => x.taskStatus),
                this.MakeGridHeader(x => x.taskTypeNo),
                this.MakeGridHeader(x => x.toLocationNo),
                this.MakeGridHeader(x => x.toLocationType),
                this.MakeGridHeader(x => x.whouseNo),
                this.MakeGridHeader(x => x.wmsTaskNo),
                this.MakeGridHeader(x => x.wmsTaskType),
                this.MakeGridHeader(x => x.CreateTime),
                this.MakeGridHeader(x => x.CreateBy),
                this.MakeGridHeader(x => x.UpdateBy),
                this.MakeGridHeader(x => x.UpdateTime),
                this.MakeGridHeader(x => x.taskStatusDesc),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        public List<SysDictionary> dicEntities = new List<SysDictionary>();
        public override IOrderedQueryable<WmsTask_View> GetSearchQuery()
        {
            var query = DC.Set<WmsTask>()
                .CheckEqual(Searcher.feedbackStatus, x=>x.feedbackStatus)
                .CheckContain(Searcher.frLocationNo, x=>x.frLocationNo)
                .CheckEqual(Searcher.frLocationType, x=>x.frLocationType)
                .CheckEqual(Searcher.loadedType, x=>x.loadedType)
                .CheckContain(Searcher.orderNo, x=>x.orderNo)
                .CheckContain(Searcher.palletBarcode, x=>x.palletBarcode)
                .CheckContain(Searcher.proprietorCode, x=>x.proprietorCode)
                .CheckContain(Searcher.regionNo, x=>x.regionNo)
                .CheckContain(Searcher.roadwayNo, x=>x.roadwayNo)
                .CheckContain(Searcher.stockCode, x=>x.stockCode)
                .CheckEqual(Searcher.taskStatus, x=>x.taskStatus)
                .CheckContain(Searcher.taskTypeNo, x=>x.taskTypeNo)
                .CheckContain(Searcher.toLocationNo, x=>x.toLocationNo)
                .CheckEqual(Searcher.toLocationType, x=>x.toLocationType)
                .CheckContain(Searcher.whouseNo, x=>x.whouseNo)
                .CheckContain(Searcher.wmsTaskNo, x=>x.wmsTaskNo)
                .CheckContain(Searcher.wmsTaskType, x=>x.wmsTaskType)
                .Select(x => new WmsTask_View
                {
				    ID = x.ID,
                    feedbackDesc = x.feedbackDesc,
                    feedbackStatus = x.feedbackStatus,
                    frLocationNo = x.frLocationNo,
                    frLocationType = x.frLocationType,
                    loadedType = x.loadedType,
                    matHeight = x.matHeight,
                    matLength = x.matLength,
                    matQty = x.matQty,
                    matWeight = x.matWeight,
                    matWidth = x.matWidth,
                    orderNo = x.orderNo,
                    palletBarcode = x.palletBarcode,
                    proprietorCode = x.proprietorCode,
                    regionNo = x.regionNo,
                    roadwayNo = x.roadwayNo,
                    stockCode = x.stockCode,
                    taskDesc = x.taskDesc,
                    taskPriority = x.taskPriority,
                    taskStatus = x.taskStatus,
                    taskTypeNo = x.taskTypeNo,
                    toLocationNo = x.toLocationNo,
                    toLocationType = x.toLocationType,
                    whouseNo = x.whouseNo,
                    wmsTaskNo = x.wmsTaskNo,
                    wmsTaskType = x.wmsTaskType,
                    CreateBy = x.CreateBy,
                    CreateTime = x.CreateTime,
                    UpdateBy = x.UpdateBy,
                    UpdateTime = x.UpdateTime,
                })
                .OrderBy(x => x.ID).ToList();
            dicEntities = DC.Set<SysDictionary>().AsNoTracking().ToList();
            foreach (var item in query)
            {
                var entityCmd = dicEntities.FirstOrDefault(t => t.dictionaryCode == "TASK_STATUS" && t.dictionaryItemCode == item.taskStatus.ToString());
                if (entityCmd != null)
                {
                    item.taskStatusDesc = entityCmd.dictionaryItemName;
                }
            }
            var queryList = query.AsQueryable().OrderByDescending(x => x.CreateTime);
            return queryList;
        }

    }

    public class WmsTask_View : WmsTask{
        public string taskStatusDesc {  get; set; }
    }
}
