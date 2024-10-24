//=============================================================================
//                                 A220101
//=============================================================================
//
// 上下文公共服务，提供日志、字典对象等。
//
//-----------------------------------------------------------------------------
//
// V1.0 2022/2/6
//      创建
//
//-----------------------------------------------------------------------------
using Wish.Model.System;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Wish.Service
{
    public class ContextService
    {
        private static ILog _log;
        /// <summary>
        /// 系统日志
        /// </summary>
        public static ILog Log
        {
            get
            {
                if (_log == null)
                {
                    try
                    {
                        var log4netRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                        XmlConfigurator.Configure(log4netRepository, new FileInfo("log4net.config"));
                        _log = LogManager.GetLogger(log4netRepository.Name, "NETCorelog4net");
                    }                     
                    catch(Exception ex)
                    {
                        string str = ex.Message;
                    }
                }
                return _log;
            }
        }

        private static Dictionary<string, Dictionary<string, object>> _dict;
        public static Dictionary<string, Dictionary<string, object>> Dict
        {
            get
            {
                //if (_dict == null)
                //{
                //    _dict = new Dictionary<string, Dictionary<string, object>>();
                //    List<SysDictionary> dicts = DCService.GetInstance().GetDC().Set<SysDictionary>().ToList();
                //    foreach (SysDictionary dict in dicts)
                //    {
                //        if (_dict.Keys.Contains(dict.Type))
                //        {
                //            Dictionary<string, object> dict2 = _dict[dict.Type];// new Dictionary<string,object>();
                //            if(dict2.Keys.Contains(dict.Value) == false)
                //            {
                //                dict2[dict.Value] = dict.Label;
                //            }
                //        }
                //        else
                //        {
                //            Dictionary<string, object> dict2 = new Dictionary<string,object>();
                //            dict2[dict.Value] = dict.Label;
                //            _dict[dict.Type] = dict2;
                //        }
                //    }
                //}
                return _dict;
            }
        }
    }
}
