//=============================================================================
//                                 A220101
//=============================================================================
//
// Plc数据块配置信息。
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

    /// <summary>
    /// Plc的数据块配置信息。
    /// </summary>
    public class PlcDataBlock
    {
        /// <summary>
        /// 数据块编号，唯一。
        /// </summary>
        public string Id { set; get; }

        /// <summary>
        /// 数据块名称。
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 读取byte数据起始地址偏移值。
        /// </summary>
        public int Offset { set; get; } 

        /// <summary>
        /// 数据库读取byte长度。
        /// </summary>
        public int Length { set; get; }

        /// <summary>
        /// DB块数据,byte数组。
        /// </summary>
        public  byte[] Data { set; get; }

        public string Address
        {

            get
            {
                return $"{Id}.{Offset}";
            }
        }
    }
}
