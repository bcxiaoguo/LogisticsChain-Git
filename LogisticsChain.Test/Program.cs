using LogisticsChain.ServiceRedis;
using LogisticsChain.Tool;
using RestSharp;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace LogisticsChain.Test
{
    class Program
    {
        static string _url = "http://localhost:58547";
        static void Main(string[] args)
        {

            //  ServiceRedis();
            TestAuthentic();


        }

        /// <summary>
        /// 测试认证
        /// </summary>
        public static void TestAuthentic() {
            Console.Title = "TestClient";
            dynamic token = null;
            while (true)
            {
                Console.WriteLine("1、登录【admin】 2、登录【system】 3、登录【错误用户名密码】 4、查询HisUser数据  5、查询LisUser数据 ");
                var mark = Console.ReadLine();
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                switch (mark)
                {
                    case "1":
                        token = AdminLogin();
                        break;
                    case "2":
                      //  token = SystemLogin();
                        break;
                    case "3":
                       // token = NullLogin();
                        break;
                    case "4":
                         DemoAAPI(token);
                        break;
                    case "5":
                     //   DemoBAPI(token);
                        break;
                }
                stopwatch.Stop();
                TimeSpan timespan = stopwatch.Elapsed;
                Console.WriteLine($"间隔时间：{timespan.TotalSeconds}");
                tokenString = "Bearer " + Convert.ToString(token?.access_token);
            }
        }
        static string tokenString = "";
        static dynamic AdminLogin()
        {
            //var loginClient = new RestClient(_url);
            //var loginRequest = new RestRequest("/Authentication/AuthenticService", Method.GET);
            //loginRequest.AddParameter("username", "gsw");
            ////  loginRequest.AddParameter("password", "111111");
            //IRestResponse loginResponse = loginClient.Execute(loginRequest);
            //var loginContent = loginResponse.Content;
            //Console.WriteLine(loginContent);
            //return Newtonsoft.Json.JsonConvert.DeserializeObject(loginContent);





            string content = "{\"username\":\"gsw\",\"password\":\"111111\"}"; //要发送的内容
            string contentType = "application/json"; //Content-Type
            try
            {
                var client = new RestClient(_url);
                var request = new RestRequest("/Authentication/AuthenticService", Method.POST);
                request.Timeout = 10000;
                //request.AddHeader("Cache-Control", "no-cache");          
                request.AddParameter(contentType, content, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
                // return response.Content; //返回的结果
                var loginContent = response.Content;
                Console.WriteLine(loginContent);
                return Newtonsoft.Json.JsonConvert.DeserializeObject(loginContent);


            }
            catch (Exception ex)
            {
                return "连接服务器出错：\r\n" + ex.Message;
            }

            //var loginClient = new RestClient(_url);
            //var loginRequest = new RestRequest("/api/Values", Method.POST);
            //loginRequest.AddParameter("username", "gsw");
            //loginRequest.AddParameter("password", "111111");
            //IRestResponse loginResponse = loginClient.Execute(loginRequest);
            //var loginContent = loginResponse.Content;
            //Console.WriteLine(loginContent);
            //return Newtonsoft.Json.JsonConvert.DeserializeObject(loginContent);

            //var loginClient = new RestClient(_url);
            //var loginRequest = new RestRequest("/api/Values", Method.POST);
            //loginRequest.AddParameter("username", "gsw");
            // loginRequest.AddParameter("password", "111111");
            //IRestResponse loginResponse = loginClient.Execute(loginRequest);
            //var loginContent = loginResponse.Content;
            //Console.WriteLine(loginContent);
            //return Newtonsoft.Json.JsonConvert.DeserializeObject(loginContent);



        }
        static void DemoAAPI(dynamic token)
        {
            var client = new RestClient(_url);
            //这里要在获取的令牌字符串前加Bearer
            string tk = "Bearer " + Convert.ToString(token?.access_token);
            client.AddDefaultHeader("Authorization", tk);
            var request = new RestRequest("/AppService/get", Method.GET);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            Console.WriteLine($"状态码：{(int)response.StatusCode} 状态信息：{response.StatusCode}  返回结果：{content}");
        }


        /// <summary>
        /// 测试redis
        /// </summary>
        public static void TestRedis() {

            
            

            RedisHelper redis = new RedisHelper(1);

            #region String

            string str = "123";
            Student demo = new Student()
            {
                ID = "1",
                Name = "123"
            };
            var resukt = redis.StringSet("redis_string_test", str);
            var str1 = redis.StringGet("redis_string_test");
            redis.StringSet("redis_string_model", demo);
            var model = redis.StringGet<Student>("redis_string_model");

            for (int i = 0; i < 10; i++)
            {
                redis.StringIncrement("StringIncrement", 2);
            }
            for (int i = 0; i < 10; i++)
            {
                redis.StringDecrement("StringIncrement");
            }
            redis.StringSet("redis_string_model1", demo, TimeSpan.FromSeconds(10));

            #endregion String

            #region List

            for (int i = 0; i < 10; i++)
            {
                redis.ListRightPush("list", i);
            }

            for (int i = 10; i < 20; i++)
            {
                redis.ListLeftPush("list", i);
            }
            var length = redis.ListLength("list");

            var leftpop = redis.ListLeftPop<string>("list");
            var rightPop = redis.ListRightPop<string>("list");

            var list = redis.ListRange<int>("list");

            #endregion List

            #region Hash

            redis.HashSet("user", "u1", "123");
            redis.HashSet("user", "u2", "1234");
            redis.HashSet("user", "u3", "1235");
            var news = redis.HashGet<string>("user", "u2");

            #endregion Hash

            #region 发布订阅

            redis.Subscribe("Channel1");
            for (int i = 0; i < 10; i++)
            {
                redis.Publish("Channel1", "msg" + i);
                if (i == 2)
                {
                    redis.Unsubscribe("Channel1");
                }
            }

            #endregion 发布订阅

            #region 事务

            var tran = redis.CreateTransaction();

            tran.StringSetAsync("tran_string", "test1");
            tran.StringSetAsync("tran_string1", "test2");
            bool committed = tran.Execute();

            #endregion 事务

            #region Lock

            var db = redis.GetDatabase();
            RedisValue token = Environment.MachineName;
            if (db.LockTake("lock_test", token, TimeSpan.FromSeconds(10)))
            {
                try
                {
                    //TODO:开始做你需要的事情
                    Thread.Sleep(5000);
                }
                finally
                {
                    db.LockRelease("lock_test", token);
                }
            }

            #endregion Lock


            Console.WriteLine();












            //var   redisConn = DbHelperRedis.getRedisConn();
            //  var db = redisConn.GetDatabase();
            //  DbHelperRedis.database = db;
            //set get
            //  string strKey = "Hello";
            //  string strValue = "DCS for Redis!";
            //  //db.SetAdd(strKey, strValue);
            //  // db.StringAppend(strKey, strValue);
            ////  db.StringSet(strKey, strValue);
            //  Console.WriteLine(strKey + ", " + db.StringGet(strKey));
            //  string counterKey = "counter";
            //  long counterValue = db.StringIncrement(counterKey);
            //  Console.WriteLine("incr " + counterKey + ", result is " + counterValue);
            //string listKey = "myList";

            ////rpush
            //db.ListRightPush(listKey, "a");
            //db.ListRightPush(listKey, "b");
            //db.ListRightPush(listKey, "c");

            ////lrange
            //RedisValue[] values = db.ListRange(listKey, 0, -1);

            //Console.Write("lrange " + listKey + " 0 -1, result is ");
            //for (int i = 0; i < values.Length; i++)
            //{
            //    Console.Write(values[i] + " ");
            //}
            //Console.WriteLine();
            //string sortedSetKey = "myZset";

            ////sadd
            //db.SortedSetAdd(sortedSetKey, "xiaoming", 85);
            //db.SortedSetAdd(sortedSetKey, "xiaohong", 100);
            //db.SortedSetAdd(sortedSetKey, "xiaofei", 62);
            //db.SortedSetAdd(sortedSetKey, "xiaotang", 73);

            ////zrevrangebyscore
            //RedisValue[] names = db.SortedSetRangeByRank(sortedSetKey, 0,3, Order.Ascending);
            //Console.Write("zrevrangebyscore " + sortedSetKey + " 0 2, result is ");
            //for (int i = 0; i < names.Length; i++)
            //{
            //    Console.Write(names[i] + " ");
            //}


            //List<Student> list = new List<Student>();
            //List<Student> list2;
            //Student stud = new Student() { ID = "1001", Name = "李四" };
            //Student stud1 = new Student() { ID = "1002", Name = "李2" };
            //list.Add(stud);
            //list.Add(stud1);
            //DbHelperRedis.ListSet<Student>("gg", list);

            //list2 =DbHelperRedis.ListGet<Student>("gg");

            //Console.WriteLine(ConfigurationManager.AppSettings["RedisServer"]);



            //List<Student> list = new List<Student>();
            //List<Student> list2;
            //Student stud = new Student() { ID = "1001", Name = "李四" };
            //Student stud1 = new Student() { ID = "1002", Name = "李2" };
            //list.Add(stud);

            //Redis r = new Redis();
            //r.Insert("k001", list);
            //list2 = r.Get<List<Student>>("k001");
            //  Console.WriteLine(oo);




            Console.ReadLine();

             
        }

     public static  void ServiceRedis()
        {
     //var client =   DbHelperRedis.ConnectRedis ();

            //client.Add<string>("StringValueTime", "我已设置过期时间噢30秒后会消失", DateTime.Now.AddMilliseconds(10000));
            //while (true)
            //{
            //    if (client.ContainsKey("StringValueTime"))
            //    {
            //        Console.WriteLine("String.键:StringValue,值:{0} {1}", client.Get<string>("StringValueTime"), DateTime.Now);
            //        Thread.Sleep(10000);
            //    }
            //    else
            //    {
            //        Console.WriteLine("键:StringValue,值:我已过期 {0}", DateTime.Now);
            //        break;
            //    }
            //}

            //client.Add<string>("StringValue", " String和Memcached操作方法差不多");
            //Console.WriteLine("数据类型为：String.键:StringValue,值:{0}", client.Get<string>("StringValue"));

            //Student stud = new Student() { ID = "1001", Name = "李四" };
            //client.Add<Student>("StringEntity", stud);
            //Student Get_stud = client.Get<Student>("StringEntity");
            //Console.WriteLine("数据类型为：String.键:StringEntity,值:{0} {1}", Get_stud.ID, Get_stud.Name);

            //List<Student> list = new List<Student>();
            //List<Student> list2 = new List<Student>();
            //Student stud = new Student() { ID = "1001", Name = "李四" };
            //Student stud1 = new Student() { ID = "1002", Name = "李2" };
            //list.Add(stud);
            //list.Add(stud1);
            //list2.Add(stud1);
            //client.Add<List<Student>>("StringEntity", list);
            //client.PushItemToList("StringEntity", "1111");
            //List<Student> Get_stud = client.Get<List<Student>>("StringEntity");
            //foreach (var item in Get_stud)
            //{
            //    Console.WriteLine("数据类型为：String.键:StringEntity,值:{0} {1}", item.ID, item.Name);

            //}

            //for (int i = 0; i < 10000; i++)
            //{
            //    client.Add<string>("StringValue", " String和Memcached操作方法差不多");
            //    Console.WriteLine(i.ToString());
            //}

       

           




            //client.FlushAll();


            //client.PushItemToList("StringValueTime1", "我已设置过期时间噢30秒后会消失");
            //client.PushItemToList("StringValueTime1", "22");
            //  //  Console.WriteLine(client.Get<string>("StringValueTime"));
            //  long p = client.GetListCount("StringValueTime1");
            //  for (int i = 0; i < p; i++)
            //  {
            //      Console.WriteLine(client.PopItemFromList("StringValueTime1"));
            //  }


            Console.ReadKey();
        }

    }
    public class Student {
        public string  ID{get; set;}
        public string  Name{get; set; }

    }
}
