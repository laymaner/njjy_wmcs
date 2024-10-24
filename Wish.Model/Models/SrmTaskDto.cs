//=============================================================================
//
// 堆垛机任务信息。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/1/18
//      创建
//
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;
using System.Text;
using WalkingTec.Mvvm.Core;


namespace Wish.Model
{
    public class SrmTaskDto
    {
        /// <summary>
        /// 任务编号
        /// </summary>
        public string TaskNo { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        [Display(Name = "流水号")]
        public Int16 SerialNo { get; set; }
        /// <summary>
        /// 动作类型
        /// </summary>
        [Display(Name = "动作类型")]
        public Int16 ActionType { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        [Display(Name = "任务类型")]
        public string TaskType { get; set; }

        /// <summary>
        /// 托盘号
        /// </summary>
        [Display(Name = "托盘号")]
        public Int32 PalletBarcode { get; set; }
        //public string PalletBarcode { get; set; }

        /// <summary>
        /// 源排
        /// </summary>
        [Display(Name = "源排")]
        public Int16 SourceRow { get; set; }
        /// <summary>
        /// 源列
        /// </summary>
        [Display(Name = "源列")]
        public Int16 SourceColumn { get; set; }
        /// <summary>
        /// 源层
        /// </summary>
        [Display(Name = "源层")]
        public Int16 SourceLayer { get; set; }
        /// <summary>
        /// 源库台号
        /// </summary>
        [Display(Name = "源库台号")]
        public Int16 SourceStationNo { get; set; }
        /// <summary>
        /// 目的库台号
        /// </summary>
        public Int16 TargetStationNo { get; set; }

        /// <summary>
        /// 目的排
        /// </summary>
        [Display(Name = "目的排")]
        public Int16 TargetRow { get; set; }
        /// <summary>
        /// 目的列
        /// </summary>
        [Display(Name = "目的列")]
        public Int16 TargetColumn { get; set; }
        /// <summary>
        /// 目的层
        /// </summary>
        [Display(Name = "目的层")]
        public Int16 TargetLayer { get; set; }
        /// <summary>
        /// 故障号
        /// </summary>
        [Display(Name = "故障号")]
        public Int16 AlarmCode { get; set; }= 0;
        /// <summary>
        /// 站
        /// </summary>
        [Display(Name = "站")]
        public Int16 Station { get; set; } = 0;
        /// <summary>
        /// 标志
        /// </summary>
        [Display(Name = "标志")]
        public Int16 Sign { get; set; } = 0;
        /// <summary>
        /// 校验位
        /// </summary>
        [Display(Name = "校验位")]
        public Int16 CheckPoint { get; set; }

        public  string ToJson()
        {
            Dictionary<string, object> jsonDic = new Dictionary<string, object>();
            jsonDic.Add("TaskNo", TaskNo);
            jsonDic.Add("ActionType", ActionType);
            jsonDic.Add("PalletBarcode", PalletBarcode);
            jsonDic.Add("SourceRow", SourceRow);
            jsonDic.Add("SourceColumn", SourceColumn);
            jsonDic.Add("SourceLayer", SourceLayer);
            jsonDic.Add("SourceStationNo", SourceStationNo);
            jsonDic.Add("TargetRow", TargetRow);
            jsonDic.Add("TargetColumn", TargetColumn);
            jsonDic.Add("TargetLayer", TargetLayer);
            jsonDic.Add("TargetStationNo", TargetStationNo);
            jsonDic.Add("AlarmCode", AlarmCode);
            jsonDic.Add("Station", Station);
            jsonDic.Add("Sign", Sign);
            jsonDic.Add("CheckPoint", CheckPoint);
            //jsonDic.Add("stb", stb.Value);
            //jsonDic.Add("ack", ack.Value);
            //jsonDic.Add("SocketByte", SocketByte.Value);
            string json = JsonConvert.SerializeObject(jsonDic);
            return json;
        }
    }
}
