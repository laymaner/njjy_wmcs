using Com.Wish.Model.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WISH.ViewModels.Common.Dtos
{
    /// <summary>
    /// 入库单信息
    /// </summary>
    public class WmsInOrderDto
    {
        /// <summary>
        /// 入库主表
        /// </summary>
        public WmsInOrder wmsInOrderMain { get; set; }

        /// <summary>
        /// 入库明细表
        /// </summary>
        public List<WmsInOrderDtl> wmsInOrderDetail { get; set; }
    }


}
