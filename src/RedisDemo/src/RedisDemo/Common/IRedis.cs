using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedisDemo.Common
{
    public interface IRedis
    {
        ITransaction GetTransaction(int db = 0, bool isRead = false);

        #region String
        #region get
        /// <summary>
        /// get the string value
        /// </summary>
        /// <param name="key">the key of value</param>
        /// <param name="flag">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        RedisValue Get(string key, CommandFlags flag = CommandFlags.None, int db = 0);
        /// <summary>
        /// get the string value(Asynchronous)
        /// </summary>
        /// <param name="key">the key of value</param>
        /// <param name="flag">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<RedisValue> GetAsync(string key, CommandFlags flag = CommandFlags.None, int db = 0);
        /// <summary>
        /// get the entity by deserialization
        /// </summary>
        /// <param name="key">the key of value</param>
        /// <param name="flag">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        T Get<T>(string key, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// get the entity by deserialization(Asynchronous)
        /// </summary>
        /// <param name="key">the key of value</param>
        /// <param name="flag">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key, CommandFlags flags = CommandFlags.None, int db = 0);


        bool SetBit(RedisKey key, long offset, bool bit, CommandFlags flags = CommandFlags.None, int db = 0);

        Task<bool> SetBitAsync(RedisKey key, long offset, bool bit, CommandFlags flags = CommandFlags.None, int db = 0);



        bool GetBit(RedisKey key, long offset, CommandFlags flags = CommandFlags.None, int db = 0);


        Task<bool> GetBitAsync(RedisKey key, long offset, CommandFlags flags = CommandFlags.None, int db = 0);


        void BitOP(Bitwise operation, RedisKey destination, IList<RedisKey> keys, CommandFlags flags = CommandFlags.None, int db = 0);

        long BitCount(RedisKey key, long start = 0, long end = 0, CommandFlags flags = CommandFlags.None, int db = 0);

        bool Del(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region set
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
        RedisValue Set(string key, string value, TimeSpan? expiry = default(TimeSpan?), When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0);
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
        Task<bool> SetAsync(string key, string value, TimeSpan? expiry = default(TimeSpan?), When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region mget
        /// <summary>
        /// get multi values 
        /// </summary>
        /// <param name="keys">the keys of the values</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        IList<RedisValue> MGet(List<RedisKey> keys, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// get multi values(Asynchronous)
        /// </summary>
        /// <param name="keys">the keys of the values</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<RedisValue[]> MGetAsync(List<RedisKey> keys, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region mset
        /// <summary>
        /// set multi values
        /// </summary>
        /// <param name="kvs">key-values</param>
        /// <param name="when">when this operation should be performed</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        bool MSet(List<KeyValuePair<RedisKey, RedisValue>> kvs, When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// set multi values(Asynchronous)
        /// </summary>
        /// <param name="kvs">key-values</param>
        /// <param name="when">when this operation should be performed</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<bool> MSetAsync(List<KeyValuePair<RedisKey, RedisValue>> kvs, When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region incr incrby incrbyfloat decr decrby
        /// <summary>
        /// handle the numeric value
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="amount">value to increase or decrease</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        double IncrOrDecrBy(RedisKey key, double value, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// handle the numeric value(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="amount">value to increase or decrease</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<double> IncrOrDecrByAsync(RedisKey key, double value, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region append
        /// <summary>
        /// append value to the key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value to append</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        long Append(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// append value to the key(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value to append</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<long> AppendAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region strlen
        /// <summary>
        /// get the value's length by the key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        long StrLen(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// get the value's length by the key(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<long> StrLenAsync(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion
        #endregion

        #region Hash
        #region hget
        /// <summary>
        /// get the value of key's field
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="field">field of key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<RedisValue> HGetAsync(RedisKey key, RedisValue field, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// get the value of key's field(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="field">field of key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        RedisValue HGet(RedisKey key, RedisValue field, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region hset
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
        bool HSet(RedisKey key, RedisValue field, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0);
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
        Task<bool> HSetAsync(RedisKey key, RedisValue field, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region hmget
        /// <summary>
        /// get multi values of key's fields
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="fields">fields of key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        IList<RedisValue> HMGet(RedisKey key, List<RedisValue> fields, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// get multi values of key's fields(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="fields">fields of key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<RedisValue[]> HMGetAsync(RedisKey key, List<RedisValue> fields, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region hmset
        /// <summary>
        /// set multi values of key's fields
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="entry">name/value pair</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        void HMSet(RedisKey key, List<HashEntry> entry, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// set multi values of key's fields(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="entry">name/value pair</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        Task HMSetAsync(RedisKey key, List<HashEntry> entry, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region hkeys
        /// <summary>
        /// get all the fields of the key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        IList<RedisValue> HKeys(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// get all the fields of the key(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<RedisValue[]> HKeysAsync(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region hvals
        /// <summary>
        /// get all the values of key's fields
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        IList<RedisValue> HVals(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// get all the values of key's fields(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<RedisValue[]> HValsAsync(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region hincr hincrby hincrbyfloat
        /// <summary>
        /// handle the numeric value
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">field of key</param>
        /// <param name="amount">value to increase or decrease</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        double HIncrOrDecrBy(RedisKey key, RedisValue hashField, double amount = 1, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// handle the numeric value(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">field of key</param>
        /// <param name="amount">value to increase or decrease</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<double> HIncrOrDecrByAsync(RedisKey key, RedisValue hashField, double amount = 1, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region hexists
        /// <summary>
        /// whether a field exists in the key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">field of key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        bool HExists(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// whether a field exists in the key(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">field of key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<bool> HExistsAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region hdel
        /// <summary>
        /// delete the field from the key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">field of key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        bool HDel(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// delete the field from the key(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashField">field of key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<bool> HDelAsync(RedisKey key, RedisValue hashField, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// delete fields from the key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashFields">fields of key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        long HDel(RedisKey key, IList<RedisValue> hashFields, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// delete fields from the key(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="hashFields">fields of key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<long> HDelAsync(RedisKey key, IList<RedisValue> hashFields, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion
        #endregion

        #region List

        #region lpush
        /// <summary>
        /// insert the value to the head of list
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">node's value</param>
        /// <param name="when">when this operation should be performed</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        long LPush(RedisKey key, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// insert the value to the head of list(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">node's value</param>
        /// <param name="when">when this operation should be performed</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<long> LPushAsync(RedisKey key, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region rpush
        /// <summary>
        /// insert the value to the tail of list
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">node's value</param>
        /// <param name="when">when this operation should be performed</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        long RPush(RedisKey key, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// insert the value to the tail of list(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">node's value</param>
        /// <param name="when">when this operation should be performed</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<long> RPushAsync(RedisKey key, RedisValue value, When when = When.Always, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region lpop
        /// <summary>
        /// removes the first element of the list
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        RedisValue LPop(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// removes the first element of the list(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<RedisValue> LPopAsync(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region lrem
        /// <summary>
        ///  removes the first count occurrences of elements equal to value from the list
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="count">amount of the node's value equal to the value</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        long LRem(RedisKey key, RedisValue value, long count = 0, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        ///  removes the first count occurrences of elements equal to value from the list(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="count">amount of the node's value equal to the value</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<long> LRemAsync(RedisKey key, RedisValue value, long count = 0, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region lrange
        /// <summary>
        /// get the specified elements of the list stored at key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="start">start index</param>
        /// <param name="stop">stop index</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        IList<RedisValue> LRange(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// get the specified elements of the list stored at key(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="start">start index</param>
        /// <param name="stop">stop index</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<RedisValue[]> LRangeAsync(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region llen
        /// <summary>
        /// get the length of the list
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        long LLen(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// get the length of the list(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<long> LLenAsync(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        /// <summary>
        ///  get the element at index index in the list
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="index">index</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        RedisValue LIndex(RedisKey key, long index, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        ///  get the element at index index in the list(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="index">index</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<RedisValue> LIndexAsync(RedisKey key, long index, CommandFlags flags = CommandFlags.None, int db = 0);

        #region LInsert
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
        long LInsert(RedisKey key, RedisValue pivot, RedisValue value, CommandFlags flags = CommandFlags.None, bool isAfter = false, int db = 0);
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
        Task<long> LInsertAsync(RedisKey key, RedisValue pivot, RedisValue value, CommandFlags flags = CommandFlags.None, bool isAfter = false, int db = 0);
        #endregion

        void LTrim(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None, int db = 0);

        Task LTrimAsnyc(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None, int db = 0);

        #endregion

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
        bool SAdd(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// add a member to a set(Asynchronous)
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="value">value of the key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<bool> SAddAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// add multi members to a set
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="values">values of the key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        long SAdd(RedisKey key, IList<RedisValue> values, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// add multi members to a set(Asynchronous)
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="values">values of the key</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<long> SAddAsync(RedisKey key, IList<RedisValue> values, CommandFlags flags = CommandFlags.None, int db = 0);
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
        bool SIsMember(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// whether member is a member of the set (Asynchronous)
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="value">value</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<bool> SIsMemberAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region smembers
        /// <summary>
        /// get all the members of the set 
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        IList<RedisValue> SMembers(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// get all the members of the set(Asynchronous) 
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<RedisValue[]> SMembersAsync(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region spop
        /// <summary>
        /// Removes a random elements from the set
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        RedisValue SPop(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// Removes a random elements from the set(Asynchronous)
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<RedisValue> SPopAsync(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0);
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
        bool SRem(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// Remove the specified members from the set(Asynchronous)
        /// </summary>
        /// <param name="key">key of the set</param>
        /// <param name="value">member to remove</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<bool> SRemAsync(RedisKey key, RedisValue value, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// Remove the specified members from the set
        /// </summary>
        /// <param name="key">key of the set</param>
        /// <param name="value">member to remove</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        long SRem(RedisKey key, IList<RedisValue> values, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// Remove the specified members from the set(Asynchronous)
        /// </summary>
        /// <param name="key">key of the set</param>
        /// <param name="value">member to remove</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<long> SRemAsync(RedisKey key, IList<RedisValue> values, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region scard
        /// <summary>
        /// get the number of elements in the set
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        long SCard(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// get the number of elements in the set(Asynchronous)
        /// </summary>
        /// <param name="key">key of set</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<long> SCardAsync(RedisKey key, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region sinter
        /// <summary>
        /// get the members of the set resulting from the intersection of all the given sets.
        /// </summary>
        /// <param name="keys">keys</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        IList<RedisValue> SInter(IList<RedisKey> keys, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// get the members of the set resulting from the intersection of all the given sets.(Asynchronous)
        /// </summary>
        /// <param name="keys">keys</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<RedisValue[]> SInterAsync(IList<RedisKey> keys, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region sdiff
        /// <summary>
        /// get the members of the set resulting from the difference between the first set and all the successive sets.
        /// </summary>
        /// <param name="keys">keys</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        IList<RedisValue> SDiff(IList<RedisKey> keys, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// get the members of the set resulting from the difference between the first set and all the successive sets.(Asynchronous)
        /// </summary>
        /// <param name="keys">keys</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<RedisValue[]> SDiffAsync(IList<RedisKey> keys, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region sunion
        /// <summary>
        /// get the members of the set resulting from the union of all the given sets.
        /// </summary>
        /// <param name="keys">keys</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        IList<RedisValue> SUnion(IList<RedisKey> keys, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// get the members of the set resulting from the union of all the given sets.(Asynchronous)
        /// </summary>
        /// <param name="keys">keys</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<RedisValue[]> SUnionAsync(IList<RedisKey> keys, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion
        #endregion

        #region SortedSet
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
        bool ZAdd(RedisKey key, RedisValue member, double score, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// Adds a member with the score to the sorted set stored at key(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="member">member</param>
        /// <param name="score">score</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<bool> ZAddAsync(RedisKey key, RedisValue member, double score, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// Adds members with scores to the sorted set stored at key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="values">sortedset entity</param>        
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        long ZAdd(RedisKey key, IList<SortedSetEntry> values, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// Adds members with scores to the sorted set stored at key(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="values">sortedset entity</param>        
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<long> ZAddAsync(RedisKey key, IList<SortedSetEntry> values, CommandFlags flags = CommandFlags.None, int db = 0);
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
        bool ZRem(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// Removes a member from the sorted set stored at key(Async)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="member">member</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<bool> ZRemAsync(RedisKey key, RedisValue member, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// Removes members from the sorted set stored at key
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="members">members</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        long ZRem(RedisKey key, IList<RedisValue> members, CommandFlags flags = CommandFlags.None, int db = 0);
        /// <summary>
        /// Removes members from the sorted set stored at key(Asynchronous)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="members">members</param>
        /// <param name="flags">behaviour</param>
        /// <param name="db">index of database</param>
        /// <returns></returns>
        Task<long> ZRemAsync(RedisKey key, IList<RedisValue> members, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region zrange
        IList<RedisValue> ZRange(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None, int db = 0);

        Task<RedisValue[]> ZRangeAsync(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region zrevrange
        IList<RedisValue> ZRevRange(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None, int db = 0);

        Task<RedisValue[]> ZRevRangeAsync(RedisKey key, long start, long stop, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion

        #region zincrby
        double ZIncrby(RedisKey key, RedisValue member, double value = 1, CommandFlags flags = CommandFlags.None, int db = 0);

        Task<double> ZIncrbyAsync(RedisKey key, RedisValue member, double value = 1, CommandFlags flags = CommandFlags.None, int db = 0);
        #endregion
        #endregion

        #region pub/sub
        void Subscribe(RedisChannel channel, Action<RedisChannel, RedisValue> handle, CommandFlags flags = CommandFlags.None, bool isRead = false);

        long Publish(RedisChannel channel, RedisValue value, CommandFlags flags = CommandFlags.None, bool isRead = false);

        void UnSubscrribe(RedisChannel channel, Action<RedisChannel, RedisValue> handle, CommandFlags flags = CommandFlags.None, bool isRead = false);
        #endregion
    }
}
