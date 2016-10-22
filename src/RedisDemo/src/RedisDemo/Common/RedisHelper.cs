using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.IO;

namespace RedisDemo.Common
{
    public class RedisHelper : IRedis
    {
        private static RedisConfigService _service;
        public RedisHelper(RedisConfigService service)
        {
            _service = service;
        }

        //private static IConfigurationRoot GetConnStr()
        //{
        //    var builder = new ConfigurationBuilder();
        //    builder.SetBasePath(Directory.GetCurrentDirectory());
        //    builder.AddJsonFile("appsettings.json");
        //    var config = builder.Build();
        //    return config;
        //}

        private Lazy<ConnectionMultiplexer> _writeConn = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(_service.RedisConfig.MasterServer);
            //return ConnectionMultiplexer.Connect(GetConnStr().GetValue<string>("RedisConfig:MasterServer"));
        });

        private Lazy<ConnectionMultiplexer> _readConn = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(_service.RedisConfig.SlaveServer);
            //return ConnectionMultiplexer.Connect(GetConnStr().GetValue<string>("RedisConfig:SlaveServer"));
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


        private ISubscriber GetSubscriber(bool isRead = false, object asyncState = null)
        {
            return isRead ?
                   ReadConn.GetSubscriber(asyncState) :
                   WriteConn.GetSubscriber(asyncState);
        }

        public ITransaction GetTransaction(int db = 0, bool isRead = false)
        {
            return GetDatabase(db).CreateTransaction();
        }

        #region string
        /// <summary>
        /// get the string value
        /// </summary>
        /// <param name="key">the key of value</param>
        /// <param name="flag">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public RedisValue Get(string key, CommandFlags flag = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).StringGet(key, flag);
        }
        /// <summary>
        /// get the string value(Asynchronous)
        /// </summary>
        /// <param name="key">the key of value</param>
        /// <param name="flag">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<RedisValue> GetAsync(string key, CommandFlags flag = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).StringGetAsync(key, flag);
        }

        /// <summary>
        /// set value to key
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="value">the value of the key</param>
        /// <param name="expiry">time to expiry</param>
        /// <param name="when">when this operation should be performed</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public RedisValue Set(string key, string value, TimeSpan? expiry = default(TimeSpan?), When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).StringSet(key, value, expiry, when, flags);
        }
        /// <summary>
        /// set value to key(Asynchronous)
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="value">the value of the key</param>
        /// <param name="expiry">time to expiry</param>
        /// <param name="when">when this operation should be performed</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<bool> SetAsync(string key, string value, TimeSpan? expiry = default(TimeSpan?), When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).StringSetAsync(key, value, expiry, when, flags);
        }

        /// <summary>
        /// get the entity by deserialization
        /// </summary>
        /// <param name="key">the key of value</param>
        /// <param name="flag">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public T Get<T>(string key, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(GetDatabase(db, true).StringGet(key, flags));
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
        /// <summary>
        /// get the entity by deserialization(Asynchronous)
        /// </summary>
        /// <param name="key">the key of value</param>
        /// <param name="flag">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<T> GetAsync<T>(string key, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            try
            {
                var res = GetDatabase(db, true).StringGetAsync(key, flags);
                return JsonConvert.DeserializeObject<Task<T>>(res.ToString());
            }
            catch (Exception ex)
            {
                return default(Task<T>);
            }
        }

        /// <summary>
        /// get multi values 
        /// </summary>
        /// <param name="keys">the keys of the values</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public IList<RedisValue> MGet(List<RedisKey> keys, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).StringGet(keys.ToArray(), flags);
        }
        /// <summary>
        /// get multi values(Asynchronous)
        /// </summary>
        /// <param name="keys">the keys of the values</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<RedisValue[]> MGetAsync(List<RedisKey> keys, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).StringGetAsync(keys.ToArray(), flags);
        }

        /// <summary>
        /// set multi values
        /// </summary>
        /// <param name="kvs">key-values</param>
        /// <param name="when">when this operation should be performed</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public bool MSet(List<KeyValuePair<RedisKey, RedisValue>> kvs, When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).StringSet(kvs.ToArray(), when, flags);
        }
        /// <summary>
        /// set multi values(Asynchronous)
        /// </summary>
        /// <param name="kvs">key-values</param>
        /// <param name="when">when this operation should be performed</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<bool> MSetAsync(List<KeyValuePair<RedisKey, RedisValue>> kvs, When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).StringSetAsync(kvs.ToArray(), when, flags);
        }

        /// <summary>
        /// handle the numeric value
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="amount">value to increase or decrease</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public double IncrOrDecrBy(RedisKey key, double amount, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).StringIncrement(key, amount, flags);
        }
        /// <summary>
        /// handle the numeric value(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="amount">value to increase or decrease</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<double> IncrOrDecrByAsync(RedisKey key, double value, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).StringIncrementAsync(key, value, flags);
        }

        /// <summary>
        /// append value to the key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value to append</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public long Append(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).StringAppend(key, value, flags);
        }
        /// <summary>
        /// append value to the key(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value to append</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<long> AppendAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).StringAppendAsync(key, value, flags);
        }

        /// <summary>
        /// get the value's length by the key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public long StrLen(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).StringLength(key, flags);
        }
        /// <summary>
        /// get the value's length by the key(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<long> StrLenAsync(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).StringLengthAsync(key, flags);
        }


        public bool SetBit(RedisKey key, long offset, bool bit, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).StringSetBit(key, offset, bit, flags);
        }

        public Task<bool> SetBitAsync(RedisKey key, long offset, bool bit, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).StringSetBitAsync(key, offset, bit, flags);
        }


        public bool GetBit(RedisKey key, long offset, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).StringGetBit(key, offset, flags);
        }

        public Task<bool> GetBitAsync(RedisKey key, long offset, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).StringGetBitAsync(key, offset, flags);
        }

        public void BitOP(Bitwise operation, RedisKey destination, IList<RedisKey> keys, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            GetDatabase(db).StringBitOperation(operation, destination, keys.ToArray(), flags);
        }

        public long BitCount(RedisKey key, long start = 0, long end = 0, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).StringBitCount(key, start, end, flags);
        }

        public bool Del(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).KeyDelete(key, flags);
        }

        #endregion

        #region Hash
        /// <summary>
        /// get the value of key's field
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="field">field of key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public RedisValue HGet(RedisKey key, RedisValue field, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).HashGet(key, field, CommandFlags.None);
        }
        /// <summary>
        /// get the value of key's field(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="field">field of key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<RedisValue> HGetAsync(RedisKey key, RedisValue field, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).HashGetAsync(key, field, CommandFlags.None);
        }

        /// <summary>
        /// set the field and value of the key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="field">field of the key</param>
        /// <param name="value">value of the field</param>
        /// <param name="when">when this operation should be performed</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public bool HSet(RedisKey key, RedisValue field, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).HashSet(key, field, value, When.Always, CommandFlags.None);
        }
        /// <summary>
        /// set the field and value of the key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="field">field of the key</param>
        /// <param name="value">value of the field</param>
        /// <param name="when">when this operation should be performed</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<bool> HSetAsync(RedisKey key, RedisValue field, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).HashSetAsync(key, field, value, When.Always, CommandFlags.None);
        }

        /// <summary>
        /// get multi values of key's fields
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="fields">fields of key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public IList<RedisValue> HMGet(RedisKey key, List<RedisValue> fields, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).HashGet(key, fields.ToArray(), flags);
        }
        /// <summary>
        /// get multi values of key's fields(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="fields">fields of key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<RedisValue[]> HMGetAsync(RedisKey key, List<RedisValue> fields, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).HashGetAsync(key, fields.ToArray(), flags);
        }

        /// <summary>
        /// set multi values of key's fields
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="entry">name/value pair</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        public void HMSet(RedisKey key, List<HashEntry> entry, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            GetDatabase(db).HashSet(key, entry.ToArray(), flags);
        }
        /// <summary>
        /// set multi values of key's fields(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="entry">name/value pair</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        public Task HMSetAsync(RedisKey key, List<HashEntry> entry, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).HashSetAsync(key, entry.ToArray(), flags);
        }

        /// <summary>
        /// get all the fields of the key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public IList<RedisValue> HKeys(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).HashKeys(key, flags).ToList();
        }
        /// <summary>
        /// get all the fields of the key(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<RedisValue[]> HKeysAsync(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).HashKeysAsync(key, flags);
        }

        /// <summary>
        /// get all the values of key's fields
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public IList<RedisValue> HVals(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).HashValues(key, flags).ToList();
        }
        /// <summary>
        /// get all the values of key's fields(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<RedisValue[]> HValsAsync(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).HashValuesAsync(key, flags);
        }

        /// <summary>
        /// handle the numeric value
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">field of key</param>
        /// <param name="amount">value to increase or decrease</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public double HIncrOrDecrBy(RedisKey key, RedisValue hashField, double amount = 1, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).HashIncrement(key, hashField, amount, flags);
        }
        /// <summary>
        /// handle the numeric value(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">field of key</param>
        /// <param name="amount">value to increase or decrease</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<double> HIncrOrDecrByAsync(RedisKey key, RedisValue hashField, double amount = 1, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).HashIncrementAsync(key, hashField, amount, flags);
        }

        /// <summary>
        /// whether a field exists in the key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">field of key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public bool HExists(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(0, true).HashExists(key, hashField, flags);
        }
        /// <summary>
        /// whether a field exists in the key(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">field of key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<bool> HExistsAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(0, true).HashExistsAsync(key, hashField, flags);
        }

        /// <summary>
        /// delete the field from the key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">field of key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public bool HDel(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).HashDelete(key, hashField, flags);
        }
        /// <summary>
        /// delete the field from the key(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">field of key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<bool> HDelAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).HashDeleteAsync(key, hashField, flags);
        }
        /// <summary>
        /// delete fields from the key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashFields">fields of key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public long HDel(RedisKey key, IList<RedisValue> hashFields, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).HashDelete(key, hashFields.ToArray(), flags);
        }
        /// <summary>
        /// delete fields from the key(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashFields">fields of key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<long> HDelAsync(RedisKey key, IList<RedisValue> hashFields, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).HashDeleteAsync(key, hashFields.ToArray(), flags);
        }
        #endregion

        #region List
        /// <summary>
        /// insert the value to the head of list
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">node's value</param>
        /// <param name="when">when this operation should be performed</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public long LPush(RedisKey key, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).ListLeftPush(key, value, when, flags);
        }
        /// <summary>
        /// insert the value to the head of list(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">node's value</param>
        /// <param name="when">when this operation should be performed</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<long> LPushAsync(RedisKey key, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).ListLeftPushAsync(key, value, when, flags);
        }

        /// <summary>
        /// insert the value to the tail of list
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">node's value</param>
        /// <param name="when">when this operation should be performed</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public long RPush(RedisKey key, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).ListRightPush(key, value, when, flags);
        }
        /// <summary>
        /// insert the value to the tail of list(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">node's value</param>
        /// <param name="when">when this operation should be performed</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<long> RPushAsync(RedisKey key, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).ListRightPushAsync(key, value, when, flags);
        }

        /// <summary>
        /// removes the first element of the list
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public RedisValue LPop(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).ListLeftPop(key, flags);
        }
        /// <summary>
        /// removes the first element of the list(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<RedisValue> LPopAsync(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).ListLeftPopAsync(key, flags);
        }

        /// <summary>
        ///  removes the first count occurrences of elements equal to value from the list
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="count">amount of the node's value equal to the value</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public long LRem(RedisKey key, RedisValue value, long count = 0, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).ListRemove(key, value, count, flags);
        }
        /// <summary>
        ///  removes the first count occurrences of elements equal to value from the list(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="count">amount of the node's value equal to the value</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<long> LRemAsync(RedisKey key, RedisValue value, long count = 0, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).ListRemoveAsync(key, value, count, flags);
        }

        /// <summary>
        /// get the specified elements of the list stored at key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="start">start index</param>
        /// <param name="stop">stop index</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public IList<RedisValue> LRange(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).ListRange(key, start, stop, flags).ToList();
        }
        /// <summary>
        /// get the specified elements of the list stored at key(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="start">start index</param>
        /// <param name="stop">stop index</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<RedisValue[]> LRangeAsync(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).ListRangeAsync(key, start, stop, flags);
        }

        /// <summary>
        /// get the length of the list
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public long LLen(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).ListLength(key, flags);
        }
        /// <summary>
        /// get the length of the list(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<long> LLenAsync(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).ListLengthAsync(key, flags);
        }

        /// <summary>
        ///  get the element at index index in the list
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="index">index</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public RedisValue LIndex(RedisKey key, long index, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).ListGetByIndex(key, index, flags);
        }
        /// <summary>
        ///  get the element at index index in the list(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="index">index</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<RedisValue> LIndexAsync(RedisKey key, long index, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).ListGetByIndexAsync(key, index, flags);
        }

        /// <summary>
        /// inserts value in the list stored at key either before or after the reference value pivot.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pivot"></param>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <param name="isAfter"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public long LInsert(RedisKey key, RedisValue pivot, RedisValue value, CommandFlags flags = CommandFlags.None, bool isAfter = false, int db = 0)
        {
            return isAfter
                ? GetDatabase(db).ListInsertAfter(key, pivot, value, flags)
                : GetDatabase(db).ListInsertBefore(key, pivot, value, flags);
        }
        /// <summary>
        /// inserts value in the list stored at key either before or after the reference value pivot.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pivot"></param>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <param name="isAfter"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public Task<long> LInsertAsync(RedisKey key, RedisValue pivot, RedisValue value, CommandFlags flags = CommandFlags.None, bool isAfter = false, int db = 0)
        {
            return isAfter
                ? GetDatabase(db).ListInsertAfterAsync(key, pivot, value, flags)
                : GetDatabase(db).ListInsertBeforeAsync(key, pivot, value, flags);
        }
        #endregion


        public void LTrim(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            GetDatabase(db).ListTrim(key, start, stop, flags);
        }

        public Task LTrimAsnyc(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).ListTrimAsync(key, start, stop, flags);
        }


        #region Set

        #region sadd
        /// <summary>
        /// add a member to a set
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="value">value of the key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public bool SAdd(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).SetAdd(key, value, flags);
        }
        /// <summary>
        /// add a member to a set(Asynchronous)
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="value">value of the key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<bool> SAddAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).SetAddAsync(key, value, flags);
        }
        /// <summary>
        /// add multi members to a set
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="values">values of the key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public long SAdd(RedisKey key, IList<RedisValue> values, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).SetAdd(key, values.ToArray(), flags);
        }
        /// <summary>
        /// add multi members to a set(Asynchronous)
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="values">values of the key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<long> SAddAsync(RedisKey key, IList<RedisValue> values, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).SetAddAsync(key, values.ToArray(), flags);
        }
        #endregion

        #region sismember
        /// <summary>
        /// whether member is a member of the set 
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="value">value</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public bool SIsMember(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).SetContains(key, value, flags);
        }
        /// <summary>
        /// whether member is a member of the set (Asynchronous)
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="value">value</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<bool> SIsMemberAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).SetContainsAsync(key, value, flags);
        }
        #endregion

        #region smembers
        /// <summary>
        /// get all the members of the set 
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public IList<RedisValue> SMembers(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).SetMembers(key, flags).ToList();
        }
        /// <summary>
        /// get all the members of the set(Asynchronous) 
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<RedisValue[]> SMembersAsync(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).SetMembersAsync(key, flags);
        }
        #endregion

        #region spop
        /// <summary>
        /// Removes a random elements from the set
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public RedisValue SPop(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).SetPop(key, flags);
        }
        /// <summary>
        /// Removes a random elements from the set(Asynchronous)
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<RedisValue> SPopAsync(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).SetPopAsync(key, flags);
        }
        #endregion

        #region srem
        /// <summary>
        /// Remove the specified members from the set
        /// </summary>
        /// <param name="key">key of the set</param>
        /// <param name="value">member to remove</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public bool SRem(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).SetRemove(key, value, flags);
        }
        /// <summary>
        /// Remove the specified members from the set(Asynchronous)
        /// </summary>
        /// <param name="key">key of the set</param>
        /// <param name="value">member to remove</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<bool> SRemAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).SetRemoveAsync(key, value, flags);
        }
        /// <summary>
        /// Remove the specified members from the set
        /// </summary>
        /// <param name="key">key of the set</param>
        /// <param name="value">member to remove</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public long SRem(RedisKey key, IList<RedisValue> values, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).SetRemove(key, values.ToArray(), flags);
        }
        /// <summary>
        /// Remove the specified members from the set(Asynchronous)
        /// </summary>
        /// <param name="key">key of the set</param>
        /// <param name="value">member to remove</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<long> SRemAsync(RedisKey key, IList<RedisValue> values, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).SetRemoveAsync(key, values.ToArray(), flags);
        }
        #endregion

        #region scard
        /// <summary>
        /// get the number of elements in the set
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public long SCard(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).SetLength(key, flags);
        }
        /// <summary>
        /// get the number of elements in the set(Asynchronous)
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<long> SCardAsync(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).SetLengthAsync(key, flags);
        }
        #endregion

        #region sinter
        /// <summary>
        /// get the members of the set resulting from the intersection of all the given sets.
        /// </summary>
        /// <param name="keys">keys</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public IList<RedisValue> SInter(IList<RedisKey> keys, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).SetCombine(SetOperation.Intersect, keys.ToArray(), flags).ToList();
        }
        /// <summary>
        /// get the members of the set resulting from the intersection of all the given sets.(Asynchronous)
        /// </summary>
        /// <param name="keys">keys</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<RedisValue[]> SInterAsync(IList<RedisKey> keys, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).SetCombineAsync(SetOperation.Intersect, keys.ToArray(), flags);
        }
        #endregion

        #region sdiff
        /// <summary>
        /// get the members of the set resulting from the difference between the first set and all the successive sets.
        /// </summary>
        /// <param name="keys">keys</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public IList<RedisValue> SDiff(IList<RedisKey> keys, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).SetCombine(SetOperation.Difference, keys.ToArray(), flags).ToList();
        }
        /// <summary>
        /// get the members of the set resulting from the difference between the first set and all the successive sets.(Asynchronous)
        /// </summary>
        /// <param name="keys">keys</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<RedisValue[]> SDiffAsync(IList<RedisKey> keys, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).SetCombineAsync(SetOperation.Difference, keys.ToArray(), flags);
        }
        #endregion

        #region sunion
        /// <summary>
        /// get the members of the set resulting from the union of all the given sets.
        /// </summary>
        /// <param name="keys">keys</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public IList<RedisValue> SUnion(IList<RedisKey> keys, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).SetCombine(SetOperation.Union, keys.ToArray(), flags).ToList();
        }
        /// <summary>
        /// get the members of the set resulting from the union of all the given sets.(Asynchronous)
        /// </summary>
        /// <param name="keys">keys</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<RedisValue[]> SUnionAsync(IList<RedisKey> keys, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).SetCombineAsync(SetOperation.Union, keys.ToArray(), flags);
        }
        #endregion
        #endregion

        #region Sorted Set
        #region zadd
        /// <summary>
        /// Adds a member with the score to the sorted set stored at key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="member">member</param>
        /// <param name="score">score</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public bool ZAdd(RedisKey key, RedisValue member, double score, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).SortedSetAdd(key, member, score, flags);
        }
        /// <summary>
        /// Adds a member with the score to the sorted set stored at key(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="member">member</param>
        /// <param name="score">score</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<bool> ZAddAsync(RedisKey key, RedisValue member, double score, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).SortedSetAddAsync(key, member, score, flags);
        }
        /// <summary>
        /// Adds members with scores to the sorted set stored at key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="values">sortedset entity</param>        
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public long ZAdd(RedisKey key, IList<SortedSetEntry> values, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).SortedSetAdd(key, values.ToArray(), flags);
        }
        /// <summary>
        /// Adds members with scores to the sorted set stored at key(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="values">sortedset entity</param>        
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<long> ZAddAsync(RedisKey key, IList<SortedSetEntry> values, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).SortedSetAddAsync(key, values.ToArray(), flags);
        }
        #endregion

        #region zrem
        /// <summary>
        /// Removes a member from the sorted set stored at key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="member">member</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public bool ZRem(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).SortedSetRemove(key, member, flags);
        }
        /// <summary>
        /// Removes a member from the sorted set stored at key(Async)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="member">member</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<bool> ZRemAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).SortedSetRemoveAsync(key, member, flags);
        }
        /// <summary>
        /// Removes members from the sorted set stored at key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="members">members</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public long ZRem(RedisKey key, IList<RedisValue> members, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).SortedSetRemove(key, members.ToArray(), flags);
        }
        /// <summary>
        /// Removes members from the sorted set stored at key(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="members">members</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        public Task<long> ZRemAsync(RedisKey key, IList<RedisValue> members, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).SortedSetRemoveAsync(key, members.ToArray(), flags);
        }
        #endregion

        #region zrange
        public IList<RedisValue> ZRange(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).SortedSetRangeByRank(key, start, stop, Order.Ascending, flags).ToList();
        }
        public Task<RedisValue[]> ZRangeAsync(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).SortedSetRangeByRankAsync(key, start, stop, Order.Ascending, flags);
        }
        #endregion

        #region zrevrange
        public IList<RedisValue> ZRevRange(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).SortedSetRangeByRank(key, start, stop, Order.Descending, flags).ToList();
        }
        public Task<RedisValue[]> ZRevRangeAsync(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db, true).SortedSetRangeByRankAsync(key, start, stop, Order.Descending, flags);
        }
        #endregion

        #region zincrby
        public double ZIncrby(RedisKey key, RedisValue member, double value = 1, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).SortedSetIncrement(key, member, value, flags);
        }
        public Task<double> ZIncrbyAsync(RedisKey key, RedisValue member, double value = 1, CommandFlags flags = CommandFlags.None, int db = 0)
        {
            return GetDatabase(db).SortedSetIncrementAsync(key, member, value, flags);
        }
        #endregion
        #endregion

        #region pub/sub
        public long Publish(RedisChannel channel, RedisValue value, CommandFlags flags = CommandFlags.None, bool isRead = false)
        {
            return GetSubscriber(isRead).Publish(channel, value, flags);
        }
        public void Subscribe(RedisChannel channel, Action<RedisChannel, RedisValue> handle, CommandFlags flags = CommandFlags.None, bool isRead = false)
        {
            GetSubscriber(isRead).Subscribe(channel, handle, flags);
        }
        public void UnSubscrribe(RedisChannel channel, Action<RedisChannel, RedisValue> handle, CommandFlags flags = CommandFlags.None, bool isRead = false)
        {
            GetSubscriber(isRead).Unsubscribe(channel, handle, flags);
        }
        #endregion
    }
}
