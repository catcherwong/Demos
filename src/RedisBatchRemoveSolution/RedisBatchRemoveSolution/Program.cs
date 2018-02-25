namespace RedisBatchRemoveSolution
{
    using StackExchange.Redis;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var db = Connection.GetDatabase();
            var server = Connection.GetServer(EndPointsString);
            string pattern = "Batch:*";

            //remove all data
            server.FlushAllDatabases();

            //add test data
            AddSomeTestData(db);
            Console.WriteLine($"AddSomeTestData, Count = {SearchRedisKeys(server, pattern).Length}");

            //use IServer.Keys
            KeysOrScanSolution(server, db, pattern);
            Console.WriteLine($"KeysOrScanSolution, Count = {SearchRedisKeys(server, pattern).Length}");

            ////use IDatabase.Execute
            //ExecuteSolution(db, pattern);
            //Console.WriteLine($"ExecuteSolution, Count = {SearchRedisKeys(db, pattern).Length}");

            Console.ReadKey();
        }       

        #region IDatabase.Execute
        private static void ExecuteSolution(IDatabase db, string pattern)
        {
            db.KeyDelete(SearchRedisKeys(db, pattern));
        }

        private static RedisKey[] SearchRedisKeys(IDatabase db, string pattern)
        {
            var keys = new HashSet<RedisKey>();

            int nextCursor = 0;
            do
            {
                RedisResult redisResult = db.Execute("SCAN", nextCursor.ToString(), "MATCH", pattern, "COUNT", "1000");
                var innerResult = (RedisResult[])redisResult;

                nextCursor = int.Parse((string)innerResult[0]);

                List<RedisKey> resultLines = ((RedisKey[])innerResult[1]).ToList();

                keys.UnionWith(resultLines);
            }
            while (nextCursor != 0);

            return keys.ToArray();
        }
        #endregion

        #region IServer.Keys
        private static RedisKey[] SearchRedisKeys(IServer server, string pattern)
        {
            return server.Keys(pattern: pattern, pageSize:100).ToArray();
        }

        private static void KeysOrScanSolution(IServer server, IDatabase db, string pattern)
        {
            db.KeyDelete(SearchRedisKeys(server, pattern));
        } 
        #endregion

        private static void AddSomeTestData(IDatabase db)
        {            
            var batch = db.CreateBatch();
            for (int i = 0; i < 20; i++)
                batch.StringSetAsync($"Batch:{i}", Guid.NewGuid().ToString());            
            batch.Execute();
        }

        #region Connection
        private const string EndPointsString = "127.0.0.1:6379";

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            ConfigurationOptions config = new ConfigurationOptions();
            config.EndPoints.Add(EndPointsString);
            config.AbortOnConnectFail = false;
            config.ConnectRetry = 5;
            config.ConnectTimeout = 1000;
            config.AllowAdmin = true;
            return ConnectionMultiplexer.Connect(config);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }   
        #endregion
    }
}
