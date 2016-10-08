using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedisDemo.Common
{
    public class RedisSession : ISession
    {
        private IRedis _redis;
        public RedisSession(IRedis redis)
        {
            _redis = redis;
        }

        public string Id
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }

        public bool IsAvailable
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<string> Keys
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public Task LoadAsync()
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            _redis.Del(key);
        }

        public void Set(string key, byte[] value)
        {
            _redis.Set(key, System.Text.Encoding.UTF8.GetString(value), TimeSpan.FromSeconds(60));
        }

        public bool TryGetValue(string key, out byte[] value)
        {

            string res = _redis.Get(key);
            if (string.IsNullOrWhiteSpace(res))
            {
                value = null;
                return false;
            }
            else
            {
                value = System.Text.Encoding.UTF8.GetBytes(res);
                return true;
            }
        }
    }

    public static class SessionExtension
    {
        public static string GetExtension(this ISession session, string key)
        {
            string res = string.Empty;
            byte[] bytes;
            if (session.TryGetValue(key, out bytes))
            {
                res = System.Text.Encoding.UTF8.GetString(bytes);
            }
            return res;
        }
        public static void SetExtension(this ISession session, string key, string value)
        {
            session.Set(key, System.Text.Encoding.UTF8.GetBytes(value));
        }

    }
}
