namespace RedisLockDemo
{
    using StackExchange.Redis;
    using System;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            string lockKey = "lock:eat";
            TimeSpan expiration = TimeSpan.FromSeconds(5);
            //5 person eat something...
            Parallel.For(0, 5, x =>
            {
                string person = $"person:{x}";
                var val = 0;
                bool isLocked = AcquireLock(lockKey, person, expiration);
                while (!isLocked && val <= 5000)
                {
                    val += 250;
                    System.Threading.Thread.Sleep(250);
                    isLocked = AcquireLock(lockKey, person, expiration);
                }

                if (isLocked)
                {
                    Console.WriteLine($"{person} begin eat food(with lock) at {DateTimeOffset.Now.ToUnixTimeMilliseconds()}.");
                    if (new Random().NextDouble() < 0.6)
                    {
                        Console.WriteLine($"{person} release lock {ReleaseLock(lockKey, person)}  {DateTimeOffset.Now.ToUnixTimeMilliseconds()}");
                    }
                    else
                    {
                        Console.WriteLine($"{person} do not release lock ....");
                    }
                }
                else
                {
                    Console.WriteLine($"{person} begin eat food(without lock) at {DateTimeOffset.Now.ToUnixTimeMilliseconds()}.");
                }
            });

            Console.WriteLine("end");
            Console.Read();

        }

        /// <summary>
        /// Acquires the lock.
        /// </summary>
        /// <returns><c>true</c>, if lock was acquired, <c>false</c> otherwise.</returns>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <param name="expiration">Expiration.</param>
        static bool AcquireLock(string key, string value, TimeSpan expiration)
        {
            bool flag = false;

            try
            {
                flag = Connection.GetDatabase().StringSet(key, value, expiration, When.NotExists);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Acquire lock fail...{ex.Message}");
                flag = true;
            }

            return flag;
        }

        /// <summary>
        /// Releases the lock.
        /// </summary>
        /// <returns><c>true</c>, if lock was released, <c>false</c> otherwise.</returns>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        static bool ReleaseLock(string key, string value)
        {
            string lua_script = @"
            if (redis.call('GET', KEYS[1]) == ARGV[1]) then
                redis.call('DEL', KEYS[1])
                return true
            else
                return false
            end
            ";

            try
            {
                var res = Connection.GetDatabase().ScriptEvaluate(lua_script,
                                                           new RedisKey[] { key },
                                                           new RedisValue[] { value });
                return (bool)res;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ReleaseLock lock fail...{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// The lazy connection.
        /// </summary>
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            ConfigurationOptions configuration = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                ConnectTimeout = 5000,
            };

            configuration.EndPoints.Add("localhost", 6379);

            return ConnectionMultiplexer.Connect(configuration.ToString());
        });

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public static ConnectionMultiplexer Connection => lazyConnection.Value;
    }
}
