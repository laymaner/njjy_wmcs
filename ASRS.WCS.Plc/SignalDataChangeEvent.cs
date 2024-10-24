
using System;
using ASRS.WCS.PLC;

namespace ASRS.WCS.PLC
{
    /// <summary>
    /// 关键信号数据变化
    /// </summary>
    public class SignalDataChangeEvent
    {
        private string attribute;
        private object statudDB;
        private string deviceCode;
        private IPlcMsgService msgService;
        public SignalDataChangeEvent(string deviceCode, string attribute, object statusDB, IPlcMsgService msgService)
        {
            this.deviceCode = deviceCode;
            this.attribute = attribute;
            this.statudDB = statusDB;
            this.msgService = msgService;
        }

        public void fireDataChange(object obj)
        {
            // 变化内容
            string str = "";
            if (obj.GetType() == typeof(Signal<bool>))
            {
                Signal<bool> signal = (Signal<bool>)obj;
                str = $"{signal.OldValue} -> {signal.Value}";
            }
            if (obj.GetType() == typeof(Signal<Int16>))
            {
                Signal<Int16> signal = (Signal<Int16>)obj;
                str = $"{signal.OldValue} -> {signal.Value}";
            }
            if (msgService != null)
            {
                msgService.NewDeviceStatusChange(this.deviceCode, this.attribute, str, statudDB);
            }
        }

    }
}
