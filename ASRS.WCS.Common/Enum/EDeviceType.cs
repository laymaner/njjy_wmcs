using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASRS.WCS.Common.Enum
{
    /// <summary>
    /// 设备类型
    /// </summary>
    public enum EDeviceType
    {
        /// <summary>
        /// 单工位堆垛机
        /// </summary>
        [Display(Name = "单工位堆垛机")]
        S1Srm = 10,

        /// <summary>
        /// 双工位堆垛机
        /// </summary>
        [Display(Name = "双工位堆垛机")]
        S2Srm = 11,

        /// <summary>
        /// 输送机
        /// </summary>
        [Display(Name = "输送机")]
        Conveyor = 20,

        /// <summary>
        /// 有轨道小车
        /// </summary>
        [Display(Name = "有轨道小车")]
        RGV = 30,
        //[Display(Name = "无轨道小车")]
        //AGV,

        //[Display(Name = "叠盘机")]
        //Coder,

        //[Display(Name = "提升机")]
        //Lifter
    }
}
