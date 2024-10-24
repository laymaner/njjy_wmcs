using System.Linq;

namespace Wish.Areas.TaskConfig.Model
{
    public class OperationBlock
    {
        #region 字段与属性
        /// <summary>
        /// 任务号
        /// </summary>
        public string TID { get; set; } = "";
        public string _CID = "";
        /// <summary>
        /// 子任务号
        /// </summary>
        public string CID
        {
            get
            {
                return _CID;
            }
            set
            {
                char[] tmp = value.ToArray();
                if (tmp.Length > 10)
                {
                    //throw new Exception("长度超过10个字符");
                }
                _CID = value.PadLeft(10, ' ');
            }
        }
        /// <summary>
        /// 托盘号
        /// </summary>
        public string TUID { get; set; } = "";
        /// <summary>
        /// 源地址
        /// </summary>
        public string Source { get; set; } = "";
        /// <summary>
        /// 目标地址
        /// </summary>
        public string Destination { get; set; } = "";
        /// <summary>
        /// 返回码
        /// </summary>
        public string Returncode { get; set; } = "";
        /// <summary>
        /// 组合号
        /// </summary>
        public string TourNumber { get; set; } = "";
        /// <summary>
        /// 取货序号
        /// </summary>
        public string Pick_ID { get; set; } = "";
        /// <summary>
        /// 放货序号
        /// </summary>
        public string Drop_ID { get; set; } = "";
        /// <summary>
        /// 货叉编号
        /// </summary>
        public string LHD_ID { get; set; } = "";
        #endregion

        public override string ToString()
        {
            return TID + CID + TUID + Source + Destination + Returncode + TourNumber + Pick_ID + Drop_ID + LHD_ID;
        }
    }
}
