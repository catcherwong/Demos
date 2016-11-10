using System;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace JWT.Common.Helpers
{
    public class RedisHelper
    {
        private static RedisOptions _options;

        public RedisHelper(IOptions<RedisOptions> options)
        {
            _options = options.Value;
        }
       
        private Lazy<ConnectionMultiplexer> _writeConn = new Lazy<ConnectionMultiplexer>(() =>
        {            
            return ConnectionMultiplexer.Connect(_options.MasterServer);
        });

        private Lazy<ConnectionMultiplexer> _readConn = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(_options.SlaveServer);
        });

        public ConnectionMultiplexer WriteConn
        {
            get { return _writeConn.Value; }
        }

        public ConnectionMultiplexer ReadConn
        {
            get { return _readConn.Value; }
        }

        private IDatabase GetDatabase(int db = 0, bool isRead = false)
        {
            return isRead ?
                   ReadConn.GetDatabase(db) :
                   WriteConn.GetDatabase(db);
        }

        public RedisValue Get(string key, CommandFlags flag = CommandFlags.None, int db = 0)
        {
            return ReadConn.GetDatabase(db).StringGet(key, flag);
        }

        public RedisValue Set(string key, string value, TimeSpan? expiry = default(TimeSpan?), When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return WriteConn.GetDatabase(db).StringSet(key, value, expiry, when, flags);
        }

    }
}
