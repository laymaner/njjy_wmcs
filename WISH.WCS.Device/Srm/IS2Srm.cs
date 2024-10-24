//=============================================================================
//                                 A220101
//=============================================================================
//
// 单工位堆垛机接口。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/2/6
//      创建
//
//-----------------------------------------------------------------------------

using ASRS.WCS.PLC;
//using WISH.WCS.PLC;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WISH.WCS.Device.SrmSocket
{
    public interface IS2Srm : IPlcDevice
    {

        //[Display(Name ="载货台有货")]
        ///// <summary>
        ///// 真表示有托盘
        ///// </summary>
        //public bool SwHasPallet { get;  }

        ///// <summary>
        ///// 真表示货叉原位
        ///// </summary>
        //[Display(Name ="货叉原位")]
        //public bool SwIsAxisZCenter { get;  }

        ///// <summary>
        ///// 当前行走坐标
        ///// </summary>
        //[Display(Name = "行走位置")]
        //public int SwAxisXPos { get;  }

        ///// <summary>
        ///// 当前行走位置[mm]
        ///// </summary>
        //[Display(Name = "行走坐标")]
        //public int SwAxisXPosMm { get;  }

        ///// <summary>
        ///// 当前升降位置
        ///// </summary>
        //[Display(Name = "升降位置")]
        //public int SwAxisYPos { get; }

        ///// <summary>
        ///// 当前升降位置[mm]
        ///// </summary>
        //[Display(Name = "升降坐标")]
        //public int SwAxisYPosMm { get;  }

        ///// <summary>
        ///// 当前货叉1位置[mm]
        ///// </summary>
        //[Display(Name = "货叉1坐标")]
        //public int SwAxisZ1PosMm { get;  }

        ///// <summary>
        ///// 当前货叉2位置[mm]
        ///// </summary>
        //[Display(Name = "货叉2坐标")]
        //public int SwAxisZ2PosMm { get; }

        ///// <summary>
        ///// 任务类型
        ///// </summary>
        //[Display(Name = "任务类型")]
        //public string SwTaskType { get;  }

        ///// <summary>
        ///// 任务步骤，字符串以逗号分隔 ： 任务步骤,任务步骤名称
        ///// </summary>
        //[Display(Name = "任务步骤")]
        //public string TaskStep { get;  }

        ///// <summary>
        ///// 取货完成
        ///// </summary>
        //[Display(Name = "取货完成")]
        //public bool TaskPickFinish { get;  }

        ///// <summary>
        ///// 返货完成
        ///// </summary>
        //[Display(Name = "放货完成")]
        //public bool TaskDeliveryFinish { get; }



        ///// <summary>
        ///// 设备模式
        ///// </summary>
        //public string SwMode { get; }
    }
}
