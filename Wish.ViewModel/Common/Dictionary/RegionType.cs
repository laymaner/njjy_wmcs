using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WISH.Helper.Common.Dictionary.DictionaryHelper
{
    public partial class DictonaryHelper
    {
        /// <summary>
        /// 库区类型
        /// </summary>
        public enum WRgionTypeCode
        {

            /// <summary>
            /// 待上架库区
            /// </summary>
            [Description("WS")]
            WS,

            /// <summary>
            /// 入库在途
            /// </summary>
            [Description("WI")]
            WI,

            /// <summary>
            /// 存储区
            /// </summary>
            [Description("ST")]
            ST,

            /// <summary>
            /// 拣选区
            /// </summary>
            [Description("PK")]
            PK,

            /// <summary>
            /// 收货区
            /// </summary>
            [Description("RC")]
            RC,

            /// <summary>
            /// 存拣区
            /// </summary>
            [Description("SP")]
            SP,

            /// <summary>
            /// 出库在途
            /// </summary>
            [Description("WO")]
            WO,

            /// <summary>
            /// 待拣区
            /// </summary>
            [Description("WP")]
            WP,

            /// <summary>
            /// 待发货区
            /// </summary>
            [Description("WD")]
            WD,

            /// <summary>
            /// 盘点暂存区
            /// </summary>
            [Description("IVT")]
            IVT,

            /// <summary>
            /// 抽检暂存区
            /// </summary>
            [Description("QC")]
            QC,

            /// <summary>
            /// 移库暂存区
            /// </summary>
            [Description("MOVE")]
            MOVE,

            /// <summary>
            /// 异常区
            /// </summary>
            [Description("YC")]
            YC,

            /// <summary>
            /// 电子货架
            /// </summary>
            [Description("DZHJ")]
            DZHJ,
        }
    }
}
