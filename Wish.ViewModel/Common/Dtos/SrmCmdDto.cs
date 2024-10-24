using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common.Dtos
{
    internal class SrmCmdDto
    {
    }
    public class CurrentTaskDto
    {
        public string name { get; set; }

        public int value { get; set; }
    }

    public class HistoryTaskDto
    {
        public string name { get; set; }

        public int value1 { get; set; }
        public int value2 { get; set; }
    }

    public class OutTaskDto
    {
        public string name { get; set; }

        public string value1 { get; set; }
        public string value2 { get; set; }
        public DateTime? createTime { get; set; }
    }

    public class InterSrmCmdDto
    {
        public string deviceNo { get; set; }
        //public string palletBarcode { get; set; }
        public Int32 palletBarcode { get; set; }
    }

    public class CreateSrmOutDto
    {
        public string invoiceNo { get; set; } 
    }

    public class devInfoDto
    {
        ///<summary>
        ///设备编码
        ///</summary>
        public string devNo { get; set; }

        ///<summary>
        ///设备名称
        ///</summary>
        public string devName { get; set; }

        ///<summary>
        ///设备类型
        ///</summary>
        public string devTypeNo { get; set; }

        ///<summary>
        ///设备类型
        ///</summary>
        public string devTypeName { get; set; }

        ///<summary>
        ///是否已连接：0未连接，1已连接
        ///</summary>
        public int? isConnect { get; set; }

        ///<summary>
        ///是否已连接：0未连接，1已连接
        ///</summary>
        public string connectStatus { get; set; }

        ///<summary>
        ///使用标志。0：停用；1：启用；
        ///</summary>
        public int? usedFlag { get; set; }

        ///<summary>
        ///使用标志。0：停用；1：启用；
        ///</summary>
        public string usedStatus { get; set; }
    }


    public class alarmInfoDto
    {
        ///<summary>
        ///设备号
        ///</summary>
        public string devNo { get; set; }

        ///<summary>
        ///设备名称
        ///</summary>
        public string devName { get; set; } = "";

        ///<summary>
        ///部件号
        ///</summary>
        public string partNo { get; set; }

        ///<summary>
        ///部件名称
        ///</summary>
        public string partName { get; set; } = "";

        ///<summary>
        ///部件对应站台
        ///</summary>
        public string partLocNo { get; set; }

        ///<summary>
        ///报警代码
        ///</summary>
        public string alarmCode { get; set; }

        ///<summary>
        ///报警说明
        ///</summary>
        public string alarmDesc { get; set; } = "";


        ///<summary>
        ///报警开始时间
        ///</summary>
        public DateTime? beginTime { get; set; }
    }

}
