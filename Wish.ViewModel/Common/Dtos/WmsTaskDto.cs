using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common.Dtos
{
    public class taskRequestInputDto
    {
        /// <summary>
        /// 请求类型
        /// </summary>
        public string taskRequestType { get; set; }

        public string locNo { get; set; }

        public string palletBarcode { get; set; }

        public decimal qty { get; set; } = 1;

        public string invoker { get; set; }

        public string palletTypeCode { get; set; }

        public string roadwayNos { get; set; }

        public int? height { get; set; }

        /// <summary>
        /// 自定义数据集
        /// </summary>
        public string data { get; set; } = "";

    }

    public class taskFeedbackInputDto
    {
        /// <summary>
        /// 反馈类型:END、任务正常完成反馈；ERREND、任务异常完成反馈
        /// </summary>
        public string taskFeedbackType { get; set; }
        public string wmsTaskNo { get; set; }

        public string locNo { get; set; }

        public string palletBarcode { get; set; }

        public string binNo { get; set; }
        /// <summary>
        ///异常标记 在FeedbackType =ERREND时使用 --0:无异常，--21空取，22阻挡出库。31排异常
        /// </summary>
        public string exceptionFlag { get; set; }

        public string invoker { get; set; }

        public string feedbackDesc { get; set; }

        /// <summary>
        /// 自定义数据集
        /// </summary>
        public string data { get; set; } = "";


    }


    public class taskOperationDto
    {
        public string invoker { get; set; }

        public Int64? taskId { get; set; }

        public string operationReason { get; set; }
    }

    public class outRecordForPalletDto
    {
        public string regionNo { get; set; }
        public string binNo { get; set; }
        public string palletBarcode { get; set; }
        /// <summary>
        /// 托盘前缀
        /// </summary>
        public string palletPrefix { get; set; }
        public string stockCode { get; set; }

        public string docTypeCode { get; set; }

        public string businessCode { get; set; }

        public int palletPickType { get; set; } = 0;

        public string waveNo { get; set; }

        public string docNo { get; set; }

        public string oldLocNo { get; set; }
        public string targetLocNo { get; set; }
        public int docPriority { get; set; } = 99;

        public string invoker { get; set; }



    }
    public class LocGroupAndTaskDto
    {
        public string locGroupNo { get; set; }
        public int taskCount { get; set; }

    }

    public class outRecordForDocDto
    {
        public string docTypeCode { get; set; }

        public string docTypeName { get; set; }
        public int docPriority { get; set; }

        public string businessCode { get; set; }

        public bool isLimitTaskCount { get; set; }

        public int canDownTaskCount { get; set; }

        public List<outRecordForPalletDto> outRecordForPallets = new List<outRecordForPalletDto>();

    }

    public class DocDownDto
    {
        public string docNo { get; set; }
    }

}
