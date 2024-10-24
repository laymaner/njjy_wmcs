using NPOI.SS.Formula.Functions;
using Rebus.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common.Dtos
{
    public class ItfResult
    {
        public int resultCode { get; set; } = 0;
        public string resultMsg { get; set; } = "";
        public List<object> data { get; set; } = new List<object>();

        public ItfResult(){

        }
        public ItfResult(int code, string message, List<object> datas)
        {
            resultCode = code;
            resultMsg = message;
            data = datas;
        }
    }

    public class FeedbackInputDto
    {
        public string waferID {  get; set; }
    }

    public class FeedbackDto
    {
        public string waferID { get; set; }
        public string sourceBy { get; set; }
        public string batchNo { get; set; }
        public string dafType { get; set; }
        public string chipModel { get; set; }
        public decimal qty { get; set; }
        public DateTime? dafFilmAppTime { get; set; }
        public DateTime? dafExpiryTime { get; set; }
        //public int? chipThickness { get; set; }
        public string chipThickness { get; set; }
        public string chipSize { get; set; }
        public bool? isOrder { get; set; }
        public string invoiceNo { get; set; }
        public string customerNo { get; set; }
    }

    public class RequestInputDto
    {
        public string invoiceNo { get; set; }
    }

    public class RequestDto
    {
        public string invoiceNo { get; set; }
        public List<string> waferIDs { get; set; } = new List<string>();
    }

    public class HandleInterBackDto
    {
        public List<long> ids { get; set; }= new List<long>();
        public string invoker {  get; set; }
    }

}
