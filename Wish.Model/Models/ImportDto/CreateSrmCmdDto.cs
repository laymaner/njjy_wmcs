namespace Wish.Models.ImportDto
{
    public class CreateSrmCmdDto
    {
        public string deviceNo { get; set; }
        public int palletBarcode { get; set; }
        //public string palletBarcode { get; set; }
        public short? fromStationNo { get; set; }
        public short? fromRow { get; set; }
        public short? fromColumn { get; set; }
        public short? fromLayer { get; set; }

        public short? toStationNo { get; set; }
        public short? toRow { get; set; }
        public short? toColumn { get; set; }
        public short? toLayer { get; set; }
    }
}
