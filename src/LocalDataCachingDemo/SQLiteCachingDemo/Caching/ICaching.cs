using System.Threading.Tasks;

namespace SQLiteCachingDemo.Caching
{
    /// <summary>
    /// Caching Interface.
    /// </summary>
    public interface ICaching
    {     
        /// <summary>
        /// Sets the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="cacheEntry">Cache entry.</param>
        Task SetAsync(CacheEntry cacheEntry);
             
        /// <summary>
        /// Gets the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="cacheKey">Cache key.</param>
        Task<object> GetAsync(string cacheKey);            

        /// <summary>
        /// Removes the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="cacheKey">Cache key.</param>
        Task RemoveAsync(string cacheKey);           

        /// <summary>
        /// Flushs all expiration async.
        /// </summary>
        /// <returns>The all expiration async.</returns>
        Task FlushAllExpirationAsync();
    }
}
