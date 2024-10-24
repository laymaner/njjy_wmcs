//=============================================================================
//                                 A220101
//=============================================================================
//
// 自动刷新数据接口。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/1/9 
//      创建
//
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASRS.WCS.PLC
{
    public interface IAutoRead
    {
        /// <summary>
        /// 返回需要自动更新的信号。
        /// </summary>
        public List<object> GetAutoReadSignals();

        public bool IsEnabled { get; set; }


    }
}
