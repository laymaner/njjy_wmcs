namespace Wish.Helper
{
    public class DealMesHelper
    {
        public static object AddOk()
        {
            return new { status = 0, msg = "添加成功", data = new { } };
        }

        public static object ActionOk(string msg)
        {
            return new { status = 0, msg = msg, data = new { } };
        }
        public static object DealOk(string msg,object data)
        {
            return new { status = 0, msg = msg, data = data };
        }
        public static object ActionError(string errorInfo)
        {
            return new { status = 1, msg = errorInfo, data = new { } };
        }

        public static object SearchOk(object data)
        {
            return new { status = 0, msg = "查询成功", data = data };
        }

        public static object AddError(string errorInfo)
        {
            return new { status = 1, msg = "添加失败:" + errorInfo, data = new { } };
        }

        public static object EditOk()
        {
            return new { status = 0, msg = "修改成功", data = new { } };
        }

        public static object EditError(string errorInfo)
        {
            return new { status = 1, msg = "修改失败:" + errorInfo, data = new { } };
        }

        public static object DeleteOk()
        {
            return new { status = 0, msg = "删除成功", data = new { } };
        }

        public static object DeleteError(string errorInfo)
        {
            return new { status = 1, msg = "删除失败:" + errorInfo, data = new { } };
        }

        public static object EnableOk()
        {
            return new { status = 0, msg = "启用成功", data = new { } };
        }

        public static object EnableError(string errorInfo)
        {
            return new { status = 1, msg = "启用失败:" + errorInfo, data = new { } };
        }

        public static object DisEnableOk()
        {
            return new { status = 0, msg = "禁用成功", data = new { } };
        }

        public static object DisEnableError(string errorInfo)
        {
            return new { status = 1, msg = "禁用失败:" + errorInfo, data = new { } };
        }

        public static object SearchError(string errorInfo)
        {
            return new { status = 1, msg = "查询失败:" + errorInfo, data = new { } };
        }

        public static object ImportOk(object okInfo)
        {
            return new { status = 0, msg = "导入成功:" + okInfo.ToString(), data = new { } };
        }

        public static object ImportError(object errorInfo)
        {
            return new { status = 1, msg = "导入失败", data = errorInfo };
        }

        public static object Ok()
        {
            return new { status = 0, msg = "执行成功", data = new { } };
        }

        public static object Ok(string msg)
        {
            return new { status = 0, msg = $"执行成功:{msg}", data = new { } };
        }

        public static object Ok(string msg, object returnData)
        {
            return new { status = 0, msg = $"{msg}", data = returnData };
        }

        public static object Error(string errorInfo)
        {
            return new { status = 1, msg = "执行失败:" + errorInfo, data = new { } };
        }

        public static object SubmitOk()
        {
            return new { status = 0, msg = "提交成功", data = new { } };
        }
        public static object SubmitOk(string msg,object data=null)
        {
            return new { status = 0, msg = msg, data = data };
        }

        public static object SubmitError(string errorInfo)
        {
            return new { status = 1, msg = "提交失败:" + errorInfo, data = new { } };
        }
    }
}