namespace Wish.Models.ImportDto
{
    public class CreateWmsTaskInfoDto
    {
        public string deviceNo {  get; set; }
        public string palletBarcode {  get; set; }
        public string fromStationNo { get; set; }
        public string toStationNo { get; set; }
        public string fromRow { get; set; }
        public string toRow { get; set; }
        public string fromColumn { get; set; }
        public string toColumn { get; set; }
        public string fromLayer { get; set; }
        public string toLayer { get; set; }

    }
}
