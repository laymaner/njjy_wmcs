using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common.Dtos
{
    public class DevDetailInfoDto
    {
        /// <summary>
        /// 设备类型
        /// </summary>
        public string devType { get; set; }

        /// <summary>
        /// 设备类型名称
        /// </summary>
        public string devTypeName { get; set; }

        /// <summary>
        /// 站台类别
        /// </summary>
        public string category { get; set; } = "NORMAL";

        /// <summary>
        /// 编码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 是否禁用：1:启用；0：禁用
        /// </summary>
        public int? usedFlag { get; set; }

        /// <summary>
        /// 是否连接：1：连接；0：未连接
        /// </summary>
        public int? isConnect { get; set; }

        /// <summary>
        /// 是否自动：1：自动；0：运行
        /// </summary>
        public int? isAuto { get; set; }

        /// <summary>
        /// 是否空闲：1：空闲；0：不空闲
        /// </summary>
        public int? isFree { get; set; }

        /// <summary>
        /// 是否有载：1：有载；0：无载
        /// </summary>
        public int? isHasGoods { get; set; }

        /// <summary>
        /// 是否报警：1：报警；0：无报警
        /// </summary>
        public int? isAlarm { get; set; }

        /// <summary>
        /// 报警信息
        /// </summary>

        public string alarmMessage { get; set; }
        /// <summary>
        /// 托盘号
        /// </summary>

        public string palletNo { get; set; } = "";
        /// <summary>
        /// 指令号
        /// </summary>

        public string cmdNo { get; set; } = "";

        /// <summary>
        /// RGV取放货站台
        /// </summary>
        public string stationNo { get; set; }

        /// <summary>
        /// RGV真实位置
        /// </summary>
        public int? loctionX { get; set; }

        /// <summary>
        /// RGV真实位置
        /// </summary>
        public int? length { get; set; }

        /// <summary>
        /// 排
        /// </summary>
        public int? x { get; set; }

        /// <summary>
        /// 列
        /// </summary>
        public int? y { get; set; }

        /// <summary>
        /// 层
        /// </summary>
        public int? z { get; set; }

        /// <summary>
        /// 总列数
        /// </summary>
        public int? totalCol { get; set; } = 0;

        public string direction { get; set; }
    }

}
