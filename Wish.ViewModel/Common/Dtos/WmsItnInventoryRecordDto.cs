using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common.Dtos
{
    internal class WmsItnInventoryRecordDto
    {
    }
    public class CreateInventoryTaskDto
    {
        public List<string> uniiCodes { get; set; } = new List<string>();
        public string invoker { get; set; }
        public string inventoryReason { get; set; }
    }

    public class ConfirmInventoryTaskDto
    {
        public long? ID {  get; set; }
        public decimal? confirmQty { get; set; } = 0;
        public string confirmReason {  get; set; }
        public string confirmBy {  get; set; }
        /// <summary>
        /// 是否回库 1：回库，0：出库
        /// </summary>
        public int? isBack {  get; set; }
        /// <summary>
        /// 差异标识;0无差异，1盘盈，2盘亏
        /// </summary>
        public int? differenceFlag {  get; set; }
    }
}
