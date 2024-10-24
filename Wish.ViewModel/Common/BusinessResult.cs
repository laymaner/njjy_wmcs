using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WISH.Helper.Common.Dictionary.DictionaryHelper;
using ResCode = WISH.Helper.Common.Dictionary.DictionaryHelper.DictonaryHelper.ResCode;

namespace WISH.Helper.Common
{
    /// <summary>
    /// 业务执行结果
    /// </summary>
    public class BusinessResult
    {
        public BusinessResult()
        {
        }

        public BusinessResult(ResCode code, string msg)
        {
            this.code = code;
            this.msg = msg;
            outParams = null;
        }

        public BusinessResult(ResCode code, string msg, object data)
        {
            this.code = code;
            this.msg = msg;
            outParams = data;
        }
        public BusinessResult Error()
        {
            code = ResCode.Error;
            return this;
        }

        public BusinessResult Error(string msg)
        {
            code = ResCode.Error;
            this.msg = msg;
            //this.outParams = data;
            return this;
        }
        public BusinessResult Success(string msg, object data = null)
        {
            code = ResCode.OK;
            this.msg = msg;
            outParams = data;
            return this;
        }

        public BusinessResult Ok(string msg, object data)
        {
            code = ResCode.OK;
            this.msg = msg;
            outParams = data;
            return this;
        }

        
        /// <summary>
        /// 执行代码
        /// </summary>
        public ResCode code { get; set; } = ResCode.OK;

        /// <summary>
        /// 反馈消息
        /// </summary>

        public string msg { get; set; } = "执行成功";

        /// <summary>
        /// 返回参数
        /// </summary>
        public object outParams { get; set; }
    }

    public class ApiResponseRes
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public ApiResponseRes()
        {
        }
        public ApiResponseRes(int code, string message, object data)
        {
            Code = code;
            Message = message;
            Data = data;
        }
    }
}