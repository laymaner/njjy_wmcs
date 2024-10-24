namespace Wish.Models.ImportDto
{
    public class DealCmdSendDto
    {
        public string deviceNo { get; set; }
        public int? taskNo { get; set; }
        public int? palletBarcode { get; set; }
        //public string palletBarcode { get; set; }
        public int? checkPoint { get; set; }
    }
}
