
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace LogisticsChain.ServiceRedis
{
    public class DbHelperRedis
    {/// <summary>
     /// 静态变量
     /// </summary>
        private static DbHelperRedis _DbHelperRedis = new DbHelperRedis();
        private static ConfigurationOptions connDCS = ConfigurationOptions.Parse("192.168.1.153:6379");
        private static ConnectionMultiplexer redisConn;
        public static IDatabase database { get; set; }
        private static readonly object Locker = new object();
        public static DbHelperRedis CreateInstance()
        {
            return _DbHelperRedis;
        }
        //public static RedisClient ConnectRedis() {
          
        //    //RedisClient client = new RedisClient("haha@192.168.1.152:6379");
        //    //return client;
        //}
        public static ConnectionMultiplexer getRedisConn()
        {
            if (redisConn == null)
            {
                lock (Locker)
                {
                    if (redisConn == null || !redisConn.IsConnected)
                    {
                        redisConn = ConnectionMultiplexer.Connect(connDCS);
                    }
                }
            }
            return redisConn;
        }

        //封装的ListSet
        public static void ListSet<T>(string key, List<T> value)
        {
           

       //下面的database 是redis的数据库对象.
            foreach (var single in value)
            {
                var s = JsonConvert.SerializeObject(single); //序列化
                database.ListRightPush(key, s); //要一个个的插入
            }
        }

        //封装的ListGet
        public static List<T> ListGet<T>(string key)
        {
            var model1 = JsonConvert.DeserializeObject<List<T>>("");
            //ListRange返回的是一组字符串对象
            //需要逐个反序列化成实体
            var vList = database.ListRange(key);
            List<T> result = new List<T>();
            foreach (var item in vList)
            {
                var model  = JsonConvert.DeserializeObject<T>(item);
              // var model = ConvertObj<T>(item); //反序列化
                result.Add(model);
            }
            return result;
        }

    }
}
