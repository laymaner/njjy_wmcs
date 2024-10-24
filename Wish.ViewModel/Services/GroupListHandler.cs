using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Services
{
    public class GroupListHandler
    {
        private static ConcurrentDictionary<string, List<string>> Groups = new ConcurrentDictionary<string, List<string>>();
        private static GroupListHandler instance = null;
        private static readonly object locker = new object();

        public GroupListHandler()
        {
        }

        public static GroupListHandler GetInstance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                        instance = new GroupListHandler();
                }
            }
            return instance;
        }

        public ConcurrentDictionary<string, List<string>> GroupList
        { get { return Groups; } }

        /// <summary>
        ///  上线时, 将信息加入到组中
        /// </summary>
        /// <param name="connectId"></param>
        /// <param name="methodName"></param>
        public int AddConnectedId(string connectId, string methodName)
        {
            var connectionList = Groups.Where(p => p.Key == methodName.Trim());
            if (connectionList != null && connectionList.Count() > 0)
            {
                var users = connectionList.FirstOrDefault().Value;
                var user = users.Where(a => a == connectId);
                if (user == null || user.Count() == 0)
                    users.Add(connectId);
                else
                    return 0;
            }
            else
            {
                Groups.TryAdd(methodName, new List<string>() { connectId });
                return 2;
            }
            return 1;
        }

        /// <summary>
        /// 离线/异常断线时, 将信息从组中移除
        /// </summary>
        /// <param name="conId"></param>
        /// <param name="listgroup"></param>
        /// <param name="list"></param>
        public void RemoveConnectedId(string conId, out ConcurrentBag<string> listgroup, out ConcurrentBag<string> list)
        {
            listgroup = new ConcurrentBag<string>();
            list = new ConcurrentBag<string>();
            var dic = Groups;
            foreach (var key in dic.Keys)
            {
                if (dic[key].Where(a => a == conId) != null)
                {
                    dic[key].Remove(conId);
                    list.Add(key);
                    if (dic[key].Count <= 0)
                    {
                        //dic.Remove(key);
                        listgroup.Add(key);
                    }
                }
            }
            foreach (var item in listgroup)
            {
                //dic.Remove(item);
                //dic.TryRemove();
                Groups.TryRemove(new KeyValuePair<string, List<string>>(item, Groups[item]));
            }
        }
    }
}
