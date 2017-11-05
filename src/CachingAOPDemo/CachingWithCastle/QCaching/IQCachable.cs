namespace CachingWithCastle.QCaching
{
    public interface IQCachable
    {
        string CacheKey { get; }
    }

    public class Product : IQCachable
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string CacheKey => this.ProductId > 0 ? this.ProductId.ToString() : "";
    }
}
