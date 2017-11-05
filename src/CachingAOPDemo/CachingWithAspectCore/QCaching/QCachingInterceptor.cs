namespace CachingWithAspectCore.QCaching
{
    using AspectCore.DynamicProxy;
    using AspectCore.Injector;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class QCachingInterceptor : AbstractInterceptor
    {
        [FromContainer]
        public ICachingProvider CacheProvider { get; set; }

        private char _linkChar = ':';

        /// <summary>
        /// Invoke the specified context and next.
        /// </summary>
        /// <returns>The invoke.</returns>
        /// <param name="context">Context.</param>
        /// <param name="next">Next.</param>
        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            var qCachingAttribute = GetQCachingAttributeInfo(context.ServiceMethod);
            if (qCachingAttribute != null)
            {
                await ProceedCaching(context, next, qCachingAttribute);
            }
            else
            {
                await next(context);
            }
        }

        /// <summary>
        /// Gets the QC aching attribute info.
        /// </summary>
        /// <returns>The QC aching attribute info.</returns>
        /// <param name="method">Method.</param>
        private QCachingAttribute GetQCachingAttributeInfo(MethodInfo method)
        {
            return method.GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(QCachingAttribute)) as QCachingAttribute;
        }

        /// <summary>
        /// Proceeds the caching.
        /// </summary>
        /// <returns>The caching.</returns>
        /// <param name="context">Context.</param>
        /// <param name="next">Next.</param>
        /// <param name="attribute">Attribute.</param>
        private async Task ProceedCaching(AspectContext context, AspectDelegate next, QCachingAttribute attribute)
        {
            var cacheKey = GenerateCacheKey(context);

            var cacheValue = CacheProvider.Get(cacheKey);
            if (cacheValue != null)
            {
                context.ReturnValue = cacheValue;
                return;
            }

            await next(context);

            if (!string.IsNullOrWhiteSpace(cacheKey))
            {
                CacheProvider.Set(cacheKey, context.ReturnValue, TimeSpan.FromSeconds(attribute.AbsoluteExpiration));
            }
        }

        /// <summary>
        /// Generates the cache key.
        /// </summary>
        /// <returns>The cache key.</returns>
        /// <param name="context">Context.</param>
        private string GenerateCacheKey(AspectContext context)
        {
            var typeName = context.ServiceMethod.DeclaringType.Name;
            var methodName = context.ServiceMethod.Name;
            var methodArguments = this.FormatArgumentsToPartOfCacheKey(context.ServiceMethod.GetParameters());

            return this.GenerateCacheKey(typeName, methodName, methodArguments);
        }

        /// <summary>
        /// Generates the cache key.
        /// </summary>
        /// <returns>The cache key.</returns>
        /// <param name="typeName">Type name.</param>
        /// <param name="methodName">Method name.</param>
        /// <param name="parameters">Parameters.</param>
        private string GenerateCacheKey(string typeName, string methodName, IList<string> parameters)
        {
            var builder = new StringBuilder();

            builder.Append(typeName);
            builder.Append(_linkChar);

            builder.Append(methodName);
            builder.Append(_linkChar);

            foreach (var param in parameters)
            {
                builder.Append(param);
                builder.Append(_linkChar);
            }

            return builder.ToString().TrimEnd(_linkChar);
        }

        /// <summary>
        /// Formats the arguments to part of cache key.
        /// </summary>
        /// <returns>The arguments to part of cache key.</returns>
        /// <param name="methodArguments">Method arguments.</param>
        /// <param name="maxCount">Max count.</param>
        private IList<string> FormatArgumentsToPartOfCacheKey(IList<ParameterInfo> methodArguments, int maxCount = 5)
        {
            return methodArguments.Select(this.GetArgumentValue).Take(maxCount).ToList();
        }

        /// <summary>
        /// Gets the argument value.
        /// </summary>
        /// <returns>The argument value.</returns>
        /// <param name="arg">Argument.</param>
        private string GetArgumentValue(object arg)
        {
            if (arg is int || arg is long || arg is string)
                return arg.ToString();

            if (arg is DateTime)
                return ((DateTime)arg).ToString("yyyyMMddHHmmss");

            if (arg is IQCachable)
                return ((IQCachable)arg).CacheKey;

            return null;
        }

    }
}
