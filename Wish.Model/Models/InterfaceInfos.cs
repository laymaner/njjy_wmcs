using NPOI.SS.Formula.Functions;
using Wish.Areas.TaskConfig.Model;
using System.Collections.Generic;

namespace Wish.Models
{
    public class ResultInfo
    {
        public string Result { get; set; }
        public string Message { get; set; }
    }
    public class ReturnSrminfo
    {
        public string Message { get; set; }
        public List<SrmtoWms> Data { get; set; }
        public string Result { get; set; }
    }
    public class SrmNo
    {
        public string StackerID { get; set; }
    }
    public class Againin 
    {
        public string StackerID { get; set; }
        //public string ForkNo { get; set; }
        public string TaskID { get; set; }

        public int TaskType { get; set; }

        public string ToStation { get; set; }

        public int ToRow { get; set; }

        public int ToBay { get; set; }

        public int ToLevel { get; set; }

        public int ToLocation { get; set; }
    }
    public  class Carrystation
    {
        public string StackerID { get; set; }
        //public string ForkNo { get; set; }
        public string TaskID { get; set; }

        public int TaskType { get; set; }

        public string FromStation { get; set; }

        public string ToStation { get; set; }
    }
    public class Check
    {
        public string StackerID { get; set; }
        //public string ForkNo { get; set; }
        public string TaskID { get; set; }

        public int TaskType { get; set; }

        public string StationType { get; set; }

        public string CheckStation { get; set; }

        public int FromRow { get; set; }

        public int FromBay { get; set; }

        public int FromLevel { get; set; }

        public int FromLocation { get; set; }
    }
    public class ConfirmCheck
    {
        public string StackerID { get; set; }
        //public string ForkNo { get; set; }

        public string TaskID { get; set; }

        public int NextActionType { get; set; }

        public string StationType { get; set; }

        public string CheckStation { get; set; }

        public int ToRow { get; set; }

        public int ToBay { get; set; }

        public int ToLevel { get; set; }

        public int ToLocation { get; set; }
    }
    public  class In
    {
        public string StackerID { get; set; }

        //public string ForkNo { get; set; }

        public string TaskID { get; set; }

        public int TaskType { get; set; }

        public string FromStation { get; set; }

        public int ToRow { get; set; }

        public int ToBay { get; set; }

        public int ToLevel { get; set; }

        public int ToLocation { get; set; }
    }
    public class Location
    {
        public string StackerID { get; set; }
        //public string ForkNo { get; set; }

        public string TaskID { get; set; }

        public int TaskType { get; set; }

        public string ToStation { get; set; }
    }
    public class Move
    {
        public string StackerID { get; set; }
        //public string ForkNo { get; set; }
        public string TaskID { get; set; }

        public int TaskType { get; set; }

        public int FromRow { get; set; }

        public int FromBay { get; set; }

        public int FromLevel { get; set; }

        public int FromLocation { get; set; }

        public int ToRow { get; set; }

        public int ToBay { get; set; }

        public int ToLevel { get; set; }

        public int ToLocation { get; set; }

    }
    public class Order
    {
        public string StackerID { get; set; }

        public string TaskID { get; set; }

        public int ResultCode { get; set; }


    }

    public class OrderCancel
    {
        public string StackerID { get; set; }

        public string TaskID { get; set; }

        public int TaskType { get; set; }

    }

    public class Out
    {
        public string StackerID { get; set; }
        //public string ForkNo { get; set; }
        public string TaskID { get; set; }

        public int TaskType { get; set; }

        public string ToStation { get; set; }

        public int FromRow { get; set; }

        public int FromBay { get; set; }

        public int FromLevel { get; set; }

        public int FromLocation { get; set; }

    }
}
