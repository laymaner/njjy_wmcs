namespace Wish.Areas.TaskConfig.Model
{
    public class SrmtoWms 
    {
        public string StackerID { get; set; }
        //public int ForkNo { get; set; }

        public string TaskID { get; set; }
        public int TaskTypeCode { get; set; }
        public int TaskExeStep { get; set; }

        public int ResultCode { get; set; }

        public string FromStation { get; set; }

        public int FromRow { get; set; }

        public int FromBay { get; set; }

        public int FromLevel { get; set; }

        public int FromLocation { get; set; }

        public string ToStation { get; set; }

        public int ToRow { get; set; }

        public int ToBay { get; set; }

        public int ToLevel { get; set; }

        public int ToLocation { get; set; }

        public int DeviceStatus { get; set; }

        public int DeviceRunMode { get; set; }

        public int CurrentRow { get; set; }

        public int CurrentBay { get; set; }

        public int CurrentLevel { get; set; }

        public int CurrPosX { get; set; }

        public int CurrPosY { get; set; }

        public int CurrPosZ { get; set; }

        public string AlarmCode { get; set; }

        public string AlarmInfo { get; set; }

        public string Spare1 { get; set; }

        public string Spare2 { get; set; }

        public string Spare3 { get; set; }

        public string Spare4 { get; set; }
    }
}
