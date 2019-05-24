using LogisticsChain.Tool;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsChain.ServiceRedis
{
    public class Redis : ICache
    {
        int DEFAULT_TMEOUT = 600;//默认超时时间（单位秒）
        string address;
        JsonSerializerSettings jsonConfig = new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Ignore };
        ConnectionMultiplexer connectionMultiplexer;
        IDatabase database;
        //ISubscriber sub;

        class CacheObject<T>
        {
            public int ExpireTime { get; set; }
            public bool ForceOutofDate { get; set; }
            public T Value { get; set; }
        }

        public Redis()
        {
            this.address = ConfigurationManager.AppSettings["RedisServer"];

            if (this.address == null || string.IsNullOrWhiteSpace(this.address.ToString()))
                throw new ApplicationException("配置文件中未找到RedisServer的有效配置");
            connectionMultiplexer = ConnectionMultiplexer.Connect(address);
            database = connectionMultiplexer.GetDatabase();
           // sub = connectionMultiplexer.GetSubscriber();
        }

        /// <summary>
        /// 连接超时设置
        /// </summary>
        public int TimeOut
        {
            get
            {
                return DEFAULT_TMEOUT;
            }
            set
            {
                DEFAULT_TMEOUT = value;
            }
        }

        public object Get(string key)
        {
            return Get<object>(key);
        }

        public T Get<T>(string key)
        {

            DateTime begin = DateTime.Now;
            var cacheValue = database.StringGet(key);
            DateTime endCache = DateTime.Now;
            var value = default(T);
            if (!cacheValue.IsNull)
            {
                var cacheObject = JsonConvert.DeserializeObject<CacheObject<T>>(cacheValue, jsonConfig);
                if (!cacheObject.ForceOutofDate)
                    database.KeyExpire(key, new TimeSpan(0, 0, cacheObject.ExpireTime));
                value = cacheObject.Value;
            }
            DateTime endJson = DateTime.Now;
            #if DEBUG
            //Log.Debug("redis取数据时间:" + endCache.Subtract(begin).TotalMilliseconds + "毫秒,转JSON时间:" + endJson.Subtract(endCache).TotalMilliseconds + "毫秒,总耗时:" + endJson.Subtract(begin).TotalMilliseconds + "毫秒");
            #endif
            return value;

        }

        public void Insert(string key, object data)
        {
            var currentTime = DateTime.Now;
            var timeSpan = currentTime.AddSeconds(TimeOut) - currentTime;
            DateTime begin = DateTime.Now;
            var jsonData = GetJsonData(data, TimeOut, false);
            DateTime endJson = DateTime.Now;
            database.StringSet(key, jsonData);
            DateTime endCache = DateTime.Now;
            #if DEBUG
            //Log.Debug("redis插入数据时间:" + endCache.Subtract(endJson).TotalMilliseconds + "毫秒,转JSON时间:" + endJson.Subtract(begin).TotalMilliseconds + "毫秒,总耗时:" + endCache.Subtract(begin).TotalMilliseconds + "毫秒");
            #endif
        }

        public void Insert(string key, object data, int cacheTime)
        {
            var currentTime = DateTime.Now;
            var timeSpan = TimeSpan.FromSeconds(cacheTime);
            DateTime begin = DateTime.Now;
            var jsonData = GetJsonData(data, TimeOut, true);
            DateTime endJson = DateTime.Now;
            database.StringSet(key, jsonData, timeSpan);
            DateTime endCache = DateTime.Now;
            #if DEBUG
            //Log.Debug("redis插入数据时间:" + endCache.Subtract(endJson).TotalMilliseconds + "毫秒,转JSON时间:" + endJson.Subtract(begin).TotalMilliseconds + "毫秒,总耗时:" + endCache.Subtract(begin).TotalMilliseconds + "毫秒");
            #endif
        }

        public void Insert(string key, object data, DateTime cacheTime)
        {
            var currentTime = DateTime.Now;
            var timeSpan = cacheTime - DateTime.Now;
            DateTime begin = DateTime.Now;
            var jsonData = GetJsonData(data, TimeOut, true);
            DateTime endJson = DateTime.Now;
            database.StringSet(key, jsonData, timeSpan);
            DateTime endCache = DateTime.Now;
            #if DEBUG
            //Log.Debug("redis插入数据时间:" + endCache.Subtract(endJson).TotalMilliseconds + "毫秒,转JSON时间:" + endJson.Subtract(begin).TotalMilliseconds + "毫秒,总耗时:" + endCache.Subtract(begin).TotalMilliseconds + "毫秒");
            #endif
        }

        public void Insert<T>(string key, T data)
        {
            var currentTime = DateTime.Now;
            var timeSpan = currentTime.AddSeconds(TimeOut) - currentTime;
            DateTime begin = DateTime.Now;
            var jsonData = GetJsonData<T>(data, TimeOut, false);
            DateTime endJson = DateTime.Now;
            database.StringSet(key, jsonData);
            DateTime endCache = DateTime.Now;
            #if DEBUG
            //Log.Debug("redis插入数据时间:" + endCache.Subtract(endJson).TotalMilliseconds + "毫秒,转JSON时间:" + endJson.Subtract(begin).TotalMilliseconds + "毫秒,总耗时:" + endCache.Subtract(begin).TotalMilliseconds + "毫秒");
            #endif
        }

        public void Insert<T>(string key, T data, int cacheTime)
        {
            var currentTime = DateTime.Now;
            var timeSpan = TimeSpan.FromSeconds(cacheTime);
            DateTime begin = DateTime.Now;
            var jsonData = GetJsonData<T>(data, TimeOut, true);
            DateTime endJson = DateTime.Now;
            database.StringSet(key, jsonData, timeSpan);
            DateTime endCache = DateTime.Now;
            #if DEBUG
            //Log.Debug("redis插入数据时间:" + endCache.Subtract(endJson).TotalMilliseconds + "毫秒,转JSON时间:" + endJson.Subtract(begin).TotalMilliseconds + "毫秒,总耗时:" + endCache.Subtract(begin).TotalMilliseconds + "毫秒");
            #endif
        }

        public void Insert<T>(string key, T data, DateTime cacheTime)
        {
            var currentTime = DateTime.Now;
            var timeSpan = cacheTime - DateTime.Now;
            DateTime begin = DateTime.Now;
            var jsonData = GetJsonData<T>(data, TimeOut, true);
            DateTime endJson = DateTime.Now;
            database.StringSet(key, jsonData, timeSpan);
            DateTime endCache = DateTime.Now;
            #if DEBUG
            //Log.Debug("redis插入数据时间:" + endCache.Subtract(endJson).TotalMilliseconds + "毫秒,转JSON时间:" + endJson.Subtract(begin).TotalMilliseconds + "毫秒,总耗时:" + endCache.Subtract(begin).TotalMilliseconds + "毫秒");
            #endif
        }


        string GetJsonData(object data, int cacheTime, bool forceOutOfDate)
        {
            var cacheObject = new CacheObject<object>() { Value = data, ExpireTime = cacheTime, ForceOutofDate = forceOutOfDate };
            return JsonConvert.SerializeObject(cacheObject, jsonConfig);//序列化对象
        }

        string GetJsonData<T>(T data, int cacheTime, bool forceOutOfDate)
        {
            var cacheObject = new CacheObject<T>() { Value = data, ExpireTime = cacheTime, ForceOutofDate = forceOutOfDate };
            return JsonConvert.SerializeObject(cacheObject, jsonConfig);//序列化对象
        }

        public void Remove(string key)
        {
            database.KeyDelete(key, CommandFlags.HighPriority);
        }

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        public bool Exists(string key)
        {
            return database.KeyExists(key);
        }
     
    }
}
