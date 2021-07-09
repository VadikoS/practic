using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Cache
{
    class Cache<T>
    {
        private TimeSpan time;
        private int size;
        private Dictionary<string, Tuple<T, DateTime>> memory;

        public Cache(TimeSpan tm, int sz)
        {
            time = tm;
            size = sz;
            memory = new Dictionary<string, Tuple<T, DateTime>>();
        }
        public void Save(string key, T data)
        {
            Tuple<T, DateTime> val;
            DeleteOldCache();
            if (memory.TryGetValue(key, out val))
            {
                throw new ArgumentException();
            }
            else if (memory.Count == size)
            {
                TimeSpan diff = new TimeSpan(0);
                DateTime curr = DateTime.Now;
                DateTime create;
                string max_key = null;
                foreach (var elem in memory)
                {
                    create = elem.Value.Item2;
                    if (curr - create > diff)
                    {
                        diff = curr - create;
                        max_key = elem.Key;
                    }
                }
                memory.Remove(max_key);
                memory.Add(key, new Tuple<T, DateTime>(data, DateTime.Now));
            }
            else
            {
                memory.Add(key, new Tuple<T, DateTime>(data, DateTime.Now));
            }
        }
        public T Get(string key)
        {
            Tuple<T, DateTime> data;
            DeleteOldCache();
            if (!memory.TryGetValue(key, out data))
            {
                throw new KeyNotFoundException();
            }
            return data.Item1;
        }
        private void DeleteOldCache()
        {
            TimeSpan diff;
            DateTime curr, create;
            foreach (var elem in memory)
            {
                curr = DateTime.Now;
                create = elem.Value.Item2;
                diff = curr - create;
                if (diff >= time)
                {
                    memory.Remove(elem.Key);
                }
            }
        }
    }
}