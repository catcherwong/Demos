using System;

namespace RedisLockDemo
{
    using StackExchange.Redis;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            string lockKey = "lock:eat";
            TimeSpan expiration = TimeSpan.FromSeconds(5);
            var val = 0;
            Parallel.For(0, 5, x =>
            {
                string person = $"person:{x}";
                bool canGetLock = AcquireLock(lockKey, person ,expiration);
                while (!canGetLock && val < 5000)
                {
                    val += 180;
                    Task.Delay(180);
                    canGetLock = AcquireLock(lockKey, person ,expiration);
                }   

                if(canGetLock)
                {
                    Console.WriteLine($"{person} begin eat food(true) at {DateTime.Now.ToString()}.");

                    if(x==3)
                    {
                        Console.WriteLine($"release {person} lock == {ReleaseLock(lockKey, person)}"); 
                    }
                }
                else
                {
                    Console.WriteLine($"{person} begin eat food(false) at {DateTime.Now.ToString()}.");
                }
            });

            Console.WriteLine("end");
            Console.Read();

        }

        static bool AcquireLock(string key, string value,TimeSpan expiration)
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

        static bool ReleaseLock(string key,string value)
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
                Console.WriteLine($"{res.ToString()}");
                return (bool)res;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ReleaseLock lock fail...{ex.Message}");
                return false;
            }
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect("localhost:6379");
        });

        public static ConnectionMultiplexer Connection => lazyConnection.Value;

    }
}
