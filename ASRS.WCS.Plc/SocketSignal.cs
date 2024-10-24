//=============================================================================
//                                 A220101
//=============================================================================
//
// Plc基础数据类型信号。
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
    public class SocketSignal<T>: BaseSignal
    {


        ///// <summary>
        ///// 数据类型。
        ///// </summary>
        //private PlcDataType dataType;

        ///// <summary>
        ///// 数据类型。
        ///// </summary>
        //public PlcDataType DateType { 
        //    get
        //    {
        //        return dataType;  
        //    }
        //    set
        //    {
        //        this.dataType = value;
        //    }
        //}



        public SocketSignal(string db, string length)
        {
            Address = db+"."+ length;
            bool validate = false;
            if (typeof(Int16) == typeof(T))
            {
                validate = true;
            }
            else if (typeof(Int32) == typeof(T))
            {
                validate = true;
            }
            else if (typeof(bool) == typeof(T))
            {
                validate = true;
            }
            else if (typeof(byte) == typeof(T))
            {
                validate = true;
            }
            else if (typeof(string) == typeof(T))
            {
                validate = true;
            }
            else if (typeof(byte[]) == typeof(T))
            {
                validate = true;
            }
            else if (typeof(Int16[]) == typeof(T))
            {
                validate = true;
            }
            else if (typeof(char[]) == typeof(T))
            {
                validate = true;
            }
            else if (typeof(string[]) == typeof(T))
            {
                validate = true;
            }
            else if (typeof(UInt16) == typeof(T))
            {
                validate = true;
            }
            else if (typeof(float) == typeof(T))
            {
                validate = true;
            }
            else if (typeof(char) == typeof(T))
            {
                validate = true;
            }
            else if (typeof(Int32[]) == typeof(T))
            {
                validate = true;
            }
            else if (typeof(DateTime) == typeof(T))
            {
                validate = true;
            }

            if(validate == false)
            {
                throw new Exception($" datatype {typeof(T)} is not support!");
            }


        }

        private T currentValue;


        private bool isDataChanged = false;
        /// <summary>
        /// 旧值。
        /// </summary>
        private T oldValue ;

        /// <summary>
        /// 旧值。
        /// </summary>
        public T OldValue { get { return oldValue; } set { oldValue = value; } }

        /// <summary>
        /// 真表示数据有修改。
        /// </summary>
        public override bool IsDataChanged()
        {
            return isDataChanged;
        }

        /// <summary>
        /// 第一次加载数据，不触发数据事件
        /// </summary>
        private bool _dataInit = false;

        /// <summary>
        /// 信号值。
        /// </summary>
        public T Value
        {
            get
            {
                return currentValue;
            }
            set
            {
                if (oldValue != null)
                {
                    if (CheckDataEquas(value, currentValue) == false)
                    {
                        this.Address = this.Address;
                        isDataChanged = true;
                        if (DataChangedAction != null && _dataInit == true)
                        {
                            Task.Factory.StartNew(() => DoDataChangedAction());
                        }
                    }
                    else
                    {
                        isDataChanged = false;
                    }
                }
                oldValue = currentValue;
                currentValue = value;
                if (DataUpdatedAction != null && _dataInit == true)
                {
                    //DoDataUpdatedAction();
                    Task.Factory.StartNew(() => DoDataUpdatedAction());
                }
                _dataInit = true;
            }
        }

        /// <summary>
        /// 判断对象o1,o2内容是否相等
        /// </summary>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        private bool CheckDataEquas(object o1, object o2)
        {
            bool iso1Array = o1 is Array;
            bool iso2Array = o2 is Array;
            if (iso1Array == iso2Array && iso1Array)
            {
                var o1Val = o1 as Array;
                var o2Val = o2 as Array;
                if (o1Val.Length != o2Val.Length)
                {
                    return false;
                }
                else
                {
                    for (int i = 0; i < o1Val.Length; i++)
                    {
                        bool valEquals = CheckDataEquas(o1Val.GetValue(i), o2Val.GetValue(i));
                        if (valEquals == false)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else
            {
                if (iso1Array != iso2Array)
                {
                    return false;
                }
                else
                {
                    return o1.Equals(o2);
                }
            }
        }
    }
}
