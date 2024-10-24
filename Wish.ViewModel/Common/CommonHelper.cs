using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common
{
    public static class CommonHelper
    {
        /// <summary>
        /// 注意， 如果要使用这个全局cache，那么key必须是所控制方法名 - 控制数据ID/NO 这样的方式，不能只有用ID/NO。切忌避免被污染
        /// </summary>
        public static MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        /// <summary>
        /// 对象映射
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static T Map<S, T>(S model, T t)
        {
            try
            {
                if (model != null)
                {
                    foreach (PropertyInfo item in typeof(T).GetProperties())
                    {
                        if (item.PropertyType != typeof(T))
                            continue;
                        PropertyInfo property = typeof(S).GetProperty(item.Name);
                        if (property != null)
                        {
                            if (item.CanWrite)
                                item.SetValue(t, property.GetValue(model, null));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return t;
        }

        /// <summary>
        /// 对象映射
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static T Map<S, T>(S model)
        {
            T result = Activator.CreateInstance<T>();
            try
            {
                if (model != null)
                {
                    foreach (PropertyInfo item in typeof(T).GetProperties())
                    {
                        if (item.Name.ToUpper() == "ID" && item.PropertyType != typeof(T))
                            continue;
                        PropertyInfo property = typeof(S).GetProperty(item.Name);
                        if (property != null)
                        {
                            if (item.CanWrite)
                                item.SetValue(result, property.GetValue(model, null));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 对象映射
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static T Map<S, T>(S model, string ignoreField)
        {
            T result = Activator.CreateInstance<T>();
            if (model != null)
            {
                foreach (PropertyInfo item in typeof(T).GetProperties())
                {
                    if (item.Name.ToUpper() == ignoreField)
                        continue;
                    PropertyInfo property = typeof(S).GetProperty(item.Name);
                    if (property != null)
                    {
                        if (item.CanWrite)
                            item.SetValue(result, property.GetValue(model, null));
                    }
                }
            }

            return result;
        }

        public static double GetHoursByDateDif(DateTime startTime, DateTime endTime)
        {
            TimeSpan ts2 = endTime.Subtract(startTime);
            return Math.Round(ts2.TotalHours, 3);
        }

        public static double GetDaysByDateDif(DateTime startTime, DateTime endTime)
        {
            TimeSpan ts2 = endTime.Subtract(startTime);
            return Math.Round(ts2.TotalDays, 3);
        }

        public static bool FileIsExist(string path)
        {
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 使用序列化与反序列化完成深拷贝
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CloneJson<T>(this T source)
        {
            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), deserializeSettings);
        }

        /// <summary>
        /// 获取缓存，如果没有就放入缓存中返回的还是空，不会影响下面逻辑的判断
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static object GetCacheLock(object id)
        {
            lock (cache)
            {
                object res = null;
                var obj = cache.Get(id);
                if (obj == null)
                {
                    cache.Set(id, id);
                }
                else
                {
                    res = obj;
                }

                return res;
            }
        }
        /// <summary>
        /// object转实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ConvertToEntity<T>(object obj) where T : new()
        {
            T entity = new T();
            Type type = typeof(T);

            foreach (PropertyInfo property in type.GetProperties())
            {
                if (obj.GetType().GetProperty(property.Name) != null)
                {
                    property.SetValue(entity, obj.GetType().GetProperty(property.Name).GetValue(obj));
                }
            }

            return entity;
        }
    }
}
