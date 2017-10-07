using Microsoft.AspNetCore.Mvc;
using SQLiteCachingDemo.Caching;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SQLiteCachingDemo.Controllers
{
    public class HomeController : Controller
    {
        #region Some Private Parameters
        private readonly int _insertCount = 1000;
        private readonly int _getCount = 10000;
        private readonly int _removeCount = 1000; 
        #endregion

        #region Usages Here
        private readonly ICaching _caching;
        public HomeController(ICaching caching)
        {
            this._caching = caching;
        }

        public async Task<IActionResult> Usage()
        {
            // 1. insert a new caching item.
            var obj = new Product()
            {
                Id = 100,
                Name = "Product100"
            };
            var cacheEntry = new CacheEntry("mykey", obj, TimeSpan.FromSeconds(3600));
            await _caching.SetAsync(cacheEntry);

            // 2. get a caching item by specify key.
            dynamic product = await _caching.GetAsync("mykey");
            var id = product.Id;
            var name = product.Name;

            // 3. remove caching item.
            // 3.1 remove caching item by specify key.
            await _caching.RemoveAsync("mykey");
            // 3.2 remove all expirate caching item.
            await _caching.FlushAllExpirationAsync();

            return Content("ok");
        } 
        #endregion

        #region Tests Here
        public IActionResult Set()
        {
            var sw = new Stopwatch();
            sw.Start();

            Parallel.For(0, _insertCount, async i =>
            {
                var productObj = new Product()
                {
                    Id = i + 1,
                    Name = string.Concat("Product_", (i + 1).ToString())
                };
                var cacheEntry = new CacheEntry(string.Concat("Product_", (i + 1).ToString()), productObj, TimeSpan.FromSeconds(3600));
                await _caching.SetAsync(cacheEntry);
            });

            sw.Stop();
            long mSec = sw.ElapsedMilliseconds;
            return Content($"{mSec}");
        }

        public IActionResult Get()
        {
            var sw = new Stopwatch();
            sw.Start();

            Parallel.For(0, _getCount, async i =>
            {
                var cacheKey = string.Concat("Product_", new Random().Next(1, 100000000).ToString());
                dynamic product = await _caching.GetAsync(cacheKey);
            });

            sw.Stop();
            long mSec = sw.ElapsedMilliseconds;
            return Content($"{mSec}");
        }

        public IActionResult Remove()
        {
            var sw = new Stopwatch();
            sw.Start();

            Parallel.For(0, _removeCount, async i =>
            {
                var cacheKey = string.Concat("Product_", new Random().Next(1, 100000000).ToString());
                await _caching.RemoveAsync(cacheKey);
            });

            sw.Stop();
            long mSec = sw.ElapsedMilliseconds;
            return Content($"{mSec}");
        }

        public async Task<IActionResult> Flush()
        {
            var sw = new Stopwatch();
            sw.Start();

            await _caching.FlushAllExpirationAsync();

            sw.Stop();
            long mSec = sw.ElapsedMilliseconds;
            return Content($"{mSec}");
        } 
        #endregion

        public IActionResult Index()
        {            
            return View();
        }       
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
