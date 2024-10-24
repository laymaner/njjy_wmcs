namespace Wish.Models.ImportDto
{
    public class DealSrmTaskDto
    {
        public string deviceNo { get; set; }
        public int? taskNo { get; set; }
        public int? palletBarcode { get; set; }
        public string waferID { get; set; }
        /// <summary>
        /// 动作类型：1联机申请，2联机，3脱机，5入库申请，7出库申请，11待机，12故障，13工作，14入库完成，15出库完成，16倒库完成，17申请卸货，19取货完毕，20放货完毕，21人工取货完成，22清洗卡夹MASK信息
        /// </summary>
        public int? actionType {  get; set; }
        public int? checkPoint {  get; set; }
    }
}
