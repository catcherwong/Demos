using Dapper;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SQLiteCachingDemo.Caching
{
    /// <summary>
    /// SQLite caching.
    /// </summary>
    public class SQLiteCaching : ICaching
    {
        /// <summary>
        /// The connection string of SQLite database.
        /// </summary>
        private readonly string connStr = $"Data Source ={Path.Combine(Directory.GetCurrentDirectory(), "localcaching.sqlite")}";

        /// <summary>
        /// The tick to time stamp.
        /// </summary>
        private readonly int TickToTimeStamp = 10000000;

        /// <summary>
        /// Flush all expirated caching items.
        /// </summary>
        /// <returns></returns>
        public async Task FlushAllExpirationAsync()
        {
            using (var conn = new SqliteConnection(connStr))
            {
                var sql = "DELETE FROM [caching] WHERE [expiration] < STRFTIME('%s','now')";
                await conn.ExecuteAsync(sql);
            }
        }

        /// <summary>
        /// Get caching item by cache key.
        /// </summary>
        /// <returns></returns>
        /// <param name="cacheKey">Cache key.</param>
        public async Task<object> GetAsync(string cacheKey)
        {
            using (var conn = new SqliteConnection(connStr))
            {
                var sql = @"SELECT [cachevalue]
	                FROM [caching]
                    WHERE [cachekey] = @cachekey AND [expiration] > STRFTIME('%s','now')";

                var res = await conn.ExecuteScalarAsync(sql, new
                {
                    cachekey = cacheKey
                });

                // deserialize object .
                return res == null ? null : JsonConvert.DeserializeObject(res.ToString());
            }
        }

        /// <summary>
        /// Remove caching item by cache key.
        /// </summary>
        /// <returns></returns>
        /// <param name="cacheKey">Cache key.</param>
        public async Task RemoveAsync(string cacheKey)
        {
            using (var conn = new SqliteConnection(connStr))
            {
                var sql = "DELETE FROM [caching] WHERE [cachekey] = @cachekey";
                await conn.ExecuteAsync(sql , new 
                {
                    cachekey = cacheKey
                });
            }
        }

        /// <summary>
        /// Set caching item.
        /// </summary>
        /// <returns></returns>
        /// <param name="cacheEntry">Cache entry.</param>
        public async Task SetAsync(CacheEntry cacheEntry)
        {            
            using (var conn = new SqliteConnection(connStr))
            {
                //1. Delete the old caching item at first .
                var deleteSql = "DELETE FROM [caching] WHERE [cachekey] = @cachekey";
                await conn.ExecuteAsync(deleteSql, new
                {
                    cachekey = cacheEntry.CacheKey
                });

                //2. Insert a new caching item with specify cache key.
                var insertSql = @"INSERT INTO [caching](cachekey,cachevalue,expiration)
                            VALUES(@cachekey,@cachevalue,@expiration)";
                await conn.ExecuteAsync(insertSql, new
                {
                    cachekey = cacheEntry.CacheKey,
                    cachevalue = cacheEntry.SerializeCacheValue,
                    expiration = await GetCurrentUnixTimestamp(cacheEntry.AbsoluteExpirationRelativeToNow)
                });
            }

            if(cacheEntry.IsRemoveExpiratedAfterSetNewCachingItem)
            {
                // remove all expirated caching item when new caching item was set .
                await FlushAllExpirationAsync();    
            }
        }

        /// <summary>
        /// Get the current unix timestamp.
        /// </summary>
        /// <returns>The current unix timestamp.</returns>
        /// <param name="absoluteExpiration">Absolute expiration.</param>
        private async Task<long> GetCurrentUnixTimestamp(TimeSpan absoluteExpiration)
        {
            using (var conn = new SqliteConnection(connStr))
            {
                var sql = "SELECT STRFTIME('%s','now')";
                var res = await conn.ExecuteScalarAsync(sql);

                //get current utc timestamp and plus absolute expiration 
                return long.Parse(res.ToString()) + (absoluteExpiration.Ticks / TickToTimeStamp);
            }
        }
    }
}