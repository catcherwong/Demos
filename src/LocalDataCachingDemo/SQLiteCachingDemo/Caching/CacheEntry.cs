using System;
using Newtonsoft.Json;

namespace SQLiteCachingDemo.Caching
{
    /// <summary>
    /// Cache entry.
    /// </summary>
    public class CacheEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SQLiteCachingDemo.Caching.CacheEntry"/> class.
        /// </summary>
        /// <param name="cacheKey">Cache key.</param>
        /// <param name="cacheValue">Cache value.</param>
        /// <param name="absoluteExpirationRelativeToNow">Absolute expiration relative to now.</param>
        /// <param name="isRemoveExpiratedAfterSetNewCachingItem">If set to <c>true</c> is remove expirated after set new caching item.</param>
        public CacheEntry(string cacheKey,
                          object cacheValue,
                          TimeSpan absoluteExpirationRelativeToNow,
                          bool isRemoveExpiratedAfterSetNewCachingItem = true)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            if (cacheValue == null)
            {
                throw new ArgumentNullException(nameof(cacheValue));
            }

            if (absoluteExpirationRelativeToNow <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(
                        nameof(AbsoluteExpirationRelativeToNow),
                        absoluteExpirationRelativeToNow,
                        "The relative expiration value must be positive.");
            }

            this.CacheKey = cacheKey;
            this.CacheValue = cacheValue;
            this.AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
            this.IsRemoveExpiratedAfterSetNewCachingItem = isRemoveExpiratedAfterSetNewCachingItem;
        }

        /// <summary>
        /// Gets the cache key.
        /// </summary>
        /// <value>The cache key.</value>
        public string CacheKey { get; private set; }

        /// <summary>
        /// Gets the cache value.
        /// </summary>
        /// <value>The cache value.</value>
        public object CacheValue { get; private set; }

        /// <summary>
        /// Gets the absolute expiration relative to now.
        /// </summary>
        /// <value>The absolute expiration relative to now.</value>
        public TimeSpan AbsoluteExpirationRelativeToNow { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:SQLiteCachingDemo.Caching.CacheEntry"/> is remove
        /// expirated after set new caching item.
        /// </summary>
        /// <value><c>true</c> if is remove expirated after set new caching item; otherwise, <c>false</c>.</value>
        public bool IsRemoveExpiratedAfterSetNewCachingItem { get; private set; }

        /// <summary>
        /// Gets the serialize cache value.
        /// </summary>
        /// <value>The serialize cache value.</value>
        public string SerializeCacheValue
        {
            get
            {
                if (this.CacheValue == null)
                {
                    throw new ArgumentNullException(nameof(this.CacheValue));
                }
                else
                {
                    return JsonConvert.SerializeObject(this.CacheValue);
                }
            }
        }

    }
}
