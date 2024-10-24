//=============================================================================
//                                 A220101
//=============================================================================
//
// 数据监控，提供数据修改，数据更新的数据检查接口。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/1/14 
//      创建
//
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ASRS.WCS.PLC
{
    public abstract class BaseMonitor:ICloneable
    {
        /// <summary>
        /// 真表示数据被修改。
        /// </summary>
        /// <returns></returns>
        public abstract bool IsDataChanged();

        /// <summary>
        /// 数据修改回调函数。
        /// </summary>
        public Action<object> DataChangedAction { get; set; }    

        /// <summary>
        /// 数据被更新(不论值是否有变化)回调函数。
        /// </summary>
        public Action<object> DataUpdatedAction { get; set; }

        /// <summary>
        /// 执行数据变化回调函数。
        /// </summary>
        public async Task DoDataChangedAction()
        {
            if (DataChangedAction != null)
            {
                //DataChangedAction(this);
                DataChangedAction(this.Clone());
            }
            await Task.Delay(10);
        }

        /// <summary>
        /// 执行数据更新回调函数。
        /// </summary>
        public async Task DoDataUpdatedAction()
        {
            if (DataUpdatedAction != null)
            {
                //DataUpdatedAction(this);
                DataUpdatedAction(this.Clone());
            }
            await Task.Delay(10);
        }

        /// <summary>
        /// 返回当前的对象的拷贝副本，避免plc线程的干扰。
        /// </summary>
        /// <returns></returns>
        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
