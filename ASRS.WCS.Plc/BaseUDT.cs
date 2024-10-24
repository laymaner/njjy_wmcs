//=============================================================================
//                                 A220101
//=============================================================================
//
// Plc自定义数据类型接口。
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
    public abstract class BaseUdt : BaseMonitor
    {

        /// <summary>
        /// 返回当前结构体的信号列表。
        /// </summary>
        /// <returns></returns>
        public abstract List<object> GetSignals();

        /// <summary>
        /// 判断数据是否变动。
        /// </summary>
        /// <returns></returns>
        public override bool IsDataChanged()
        {
            bool res = false;
            List<object> signals = GetSignals();
            if (signals != null)
            {
                foreach (object signal in signals)
                {
                    if (signal is BaseSignal)
                    {
                        if (((BaseSignal)signal).IsDataChanged())
                        {
                            res = true;
                            break;
                        }
                    }
                    else if (signal is BaseUdt)
                    {
                        if (((BaseMonitor)signal).IsDataChanged())
                        {
                            res = true;
                            break;
                        }
                    }
                }
            }
            return res;
        }

        public string DB { get; set; }
        public int Offset { get; set; }

        protected void fireDataUpdate(object obj)
        {
            bool isChange = false;
            lock (this.GetSignals())
            {
                isChange = IsDataChanged();
            }
            if (isChange)
            {
                DoDataChangedAction();
            }
            DoDataUpdatedAction();
        }


        public abstract string ToJson();
    }

    
}
