namespace CachingWithCastle.QCaching
{
    using System;
    using System.Threading.Tasks;

    public interface ICachingProvider
    {
        object Get(string cacheKey);

        Task<object> GetAsync(string cacheKey);

        void Set(string cacheKey, object cacheValue, TimeSpan absoluteExpirationRelativeToNow);

        Task SetAsync(string cacheKey, object cacheValue, TimeSpan absoluteExpirationRelativeToNow);
    }

}
