using Microsoft.Extensions.Options;

namespace RedisDemo.Common
{
    public class RedisConfigService
    {
        private readonly IOptions<RedisConfig> _redisConfiguration;
        public RedisConfigService(IOptions<RedisConfig> redisConfiguration)
        {
            _redisConfiguration = redisConfiguration;
        }

        public RedisConfig RedisConfig
        {
            get
            {
                return _redisConfiguration.Value;
            }
        }
    }
}
