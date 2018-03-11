namespace CachingSerializer
{
    using StackExchange.Redis;
    using Enyim.Caching;
    using Enyim.Caching.Memcached;

    using MessagePack;
    using MessagePack.Resolvers;
    using Newtonsoft.Json;
    using ProtoBuf;
    using System.Runtime.Serialization.Formatters.Binary;

    using System;
    using System.IO;    
    using System.Text;
    using Microsoft.Extensions.DependencyInjection; 

    class Program
    {
        static MemcachedClient _client;

        static void Main(string[] args)
        {
            Product product = new Product
            {
                Id = 999,
                Name = "Product999"
            };

            //RedisBinaryFormatter(product);
            //RedisMessagePack(product);
            //RedisJson(product);
            //RedisProtobuf(product);
           
            //string transcoder = "";
            string transcoder = "BinaryFormatterTranscoder";
            //string transcoder = "CachingSerializer.MessagePackTranscoder,CachingSerializer";
            InitMemcached(transcoder);
            MemcachedTrancode(product);



            Console.ReadKey();
        }

        #region Redis
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            ConfigurationOptions config = new ConfigurationOptions();
            config.EndPoints.Add("127.0.0.1:6379");
            return ConnectionMultiplexer.Connect(config);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        private static void RedisBinaryFormatter(Product product)
        {
            var formatter = new BinaryFormatter();

            var db = Connection.GetDatabase();

            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, product);
                db.StringSet("binaryformatter", ms.ToArray(), TimeSpan.FromMinutes(1));
            }

            Console.WriteLine("binaryformatter serialize succeed!");

            var value = db.StringGet("binaryformatter");

            using (var ms = new MemoryStream(value))
            {
                var desValue = (Product)(new BinaryFormatter().Deserialize(ms));
                Console.WriteLine($"{desValue.Id}-{desValue.Name}");
            }

            Console.WriteLine("binaryformatter deserialize succeed!");
        }

        private static void RedisMessagePack(Product product)
        {
            var db = Connection.GetDatabase();

            var serValue = MessagePackSerializer.Serialize(product, ContractlessStandardResolver.Instance);
            db.StringSet("messagepack", serValue, TimeSpan.FromMinutes(1));
            Console.WriteLine("messagepack serialize succeed!");

            var value = db.StringGet("messagepack");
            var desValue = MessagePackSerializer.Deserialize<Product>(value, ContractlessStandardResolver.Instance);
            Console.WriteLine($"{desValue.Id}-{desValue.Name}");
            Console.WriteLine("messagepack deserialize succeed!");
        }

        private static void RedisJson(Product product)
        {
            var jsonSerializer = new JsonSerializer();

            var db = Connection.GetDatabase();

            using (var ms = new MemoryStream())
            {
                using (var sr = new StreamWriter(ms, Encoding.UTF8))
                using (var jtr = new JsonTextWriter(sr))
                {
                    jsonSerializer.Serialize(jtr, product);
                }
                db.StringSet("json", ms.ToArray(), TimeSpan.FromMinutes(1));
            }
                    
            Console.WriteLine("json serialize succeed!");

            var bytes = db.StringGet("json");

            using (var ms = new MemoryStream(bytes))
            using (var sr = new StreamReader(ms, Encoding.UTF8))
            using (var jtr = new JsonTextReader(sr))
            {
                var desValue = jsonSerializer.Deserialize<Product>(jtr);
                Console.WriteLine($"{desValue.Id}-{desValue.Name}");
            }

            Console.WriteLine("json deserialize succeed!");

            ////other way
            //var objStr = JsonConvert.SerializeObject(product);
            //db.StringSet("json", Encoding.UTF8.GetBytes(objStr), TimeSpan.FromMinutes(1));
            //var resStr = Encoding.UTF8.GetString(db.StringGet("json"));
            //var res = JsonConvert.DeserializeObject<Product>(resStr);
        }

        private static void RedisProtobuf(Product product)
        {
            var db = Connection.GetDatabase();

            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, product);
                db.StringSet("protobuf", ms.ToArray(), TimeSpan.FromMinutes(1));
            }

            Console.WriteLine("protobuf serialize succeed!");

            var value = db.StringGet("protobuf");

            using (var ms = new MemoryStream(value))
            {
                var desValue = Serializer.Deserialize<Product>(ms);
                Console.WriteLine($"{desValue.Id}-{desValue.Name}");
            }

            Console.WriteLine("protobuf deserialize succeed!");
        }
        #endregion

        #region Memcaced
        private static void InitMemcached(string transcoder = "")
        {
            IServiceCollection services = new ServiceCollection();
            services.AddEnyimMemcached(options =>
            {
                options.AddServer("127.0.0.1", 11211);
                options.Transcoder = transcoder;
            });

            services.AddSingleton<ITranscoder, MessagePackTranscoder>();
            services.AddLogging();
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            _client = serviceProvider.GetService<IMemcachedClient>() as MemcachedClient;
        }

        private static void MemcachedTrancode(Product product)
        {
            bool flag = _client.Store(StoreMode.Set, "defalut", product, DateTime.Now.AddMinutes(1));

            if (flag)
            {
                Console.WriteLine("serialize succeed!");
            }
            else
            {
                Console.WriteLine("serialize fail!");
            }
            

            var desValue = _client.Get<Product>("defalut");
            //var desValue = _client.ExecuteGet<Product>("defalut").Value;

            Console.WriteLine($"{desValue.Id}-{desValue.Name}");
            Console.WriteLine("deserialize succeed!");
        }
        #endregion
    }

    public class MessagePackTranscoder : DefaultTranscoder
    {
        protected override ArraySegment<byte> SerializeObject(object value)
        {
            return MessagePackSerializer.SerializeUnsafe(value, TypelessContractlessStandardResolver.Instance);
        }

        public override T Deserialize<T>(CacheItem item)
        {
            return (T)base.Deserialize(item);
        }

        protected override object DeserializeObject(ArraySegment<byte> value)
        {
            return MessagePackSerializer.Deserialize<object>(value, TypelessContractlessStandardResolver.Instance);
        }
    }

    [ProtoContract]
    [Serializable]
    public class Product
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; }
    }
}
