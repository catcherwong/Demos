namespace Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Options;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IMemoryCache _cache;

        private readonly PluginsOptions _options;

        public ValuesController(
            IMemoryCache cache,
            IOptions<PluginsOptions> optionsAccessor)
        {
            this._cache = cache;
            this._options = optionsAccessor.Value;
        }

        // GET api/values?type=xxx
        [HttpGet]
        public async Task<string> GetAsync(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return "type is empty";
            }

            var plugin = await this.GetPlugin(type);

            if (plugin != null)
            {
                return plugin.Handle();
            }

            return "default";
        }

        private async Task<Common.BasePluginsService> GetPlugin(string type)
        {
            string cacheKey = $"plugin:{type}";

            if (_cache.TryGetValue(cacheKey, out Common.BasePluginsService service))
            {
                return service;
            }
            else
            {
                var baseDirectory = Directory.GetCurrentDirectory();                
                var dll = $"Plugins.{type}.dll";
                var path = Path.Combine(baseDirectory, _options.PluginsPath, dll);                
                try
                {                       
                    byte[] bytes = await System.IO.File.ReadAllBytesAsync(path);
                    var assembly = Assembly.Load(bytes);
                    var obj = (Common.BasePluginsService)assembly.CreateInstance($"Plugins.{type}.PluginsService");

                    if (obj != null)
                    {
                        _cache.Set(cacheKey, obj, DateTimeOffset.Now.AddSeconds(5));
                    }

                    return obj;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
