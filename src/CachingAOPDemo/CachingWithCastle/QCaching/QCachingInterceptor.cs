namespace CachingWithCastle.QCaching
{
    using Castle.DynamicProxy;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class QCachingInterceptor : IInterceptor
    {
        private ICachingProvider _cacheProvider;
        private char _linkChar = ':';

        public QCachingInterceptor(ICachingProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        /// <summary>
        /// Intercept the specified invocation.
        /// </summary>
        /// <returns>The intercept.</returns>
        /// <param name="invocation">Invocation.</param>
        public void Intercept(IInvocation invocation)
        {
            var qCachingAttribute = this.GetQCachingAttributeInfo(invocation.MethodInvocationTarget ?? invocation.Method);
            if (qCachingAttribute != null)
            {
                ProceedCaching(invocation, qCachingAttribute);
            }
            else
            {
                invocation.Proceed();
            }
        }

        /// <summary>
        /// Gets the QCaching attribute info.
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
        /// <param name="invocation">Invocation.</param>
        /// <param name="attribute">Attribute.</param>
        private void ProceedCaching(IInvocation invocation, QCachingAttribute attribute)
        {
            var cacheKey = GenerateCacheKey(invocation);

            var cacheValue = _cacheProvider.Get(cacheKey);
            if (cacheValue != null)
            {
                invocation.ReturnValue = cacheValue;
                return;
            }

            invocation.Proceed();

            if (!string.IsNullOrWhiteSpace(cacheKey))
            {
                _cacheProvider.Set(cacheKey, invocation.ReturnValue, TimeSpan.FromSeconds(attribute.AbsoluteExpiration));
            }
        }

        /// <summary>
        /// Generates the cache key.
        /// </summary>
        /// <returns>The cache key.</returns>
        /// <param name="invocation">Invocation.</param>
        private string GenerateCacheKey(IInvocation invocation)
        {
            var typeName = invocation.TargetType.Name;
            var methodName = invocation.Method.Name;
            var methodArguments = this.FormatArgumentsToPartOfCacheKey(invocation.Arguments);

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
        private IList<string> FormatArgumentsToPartOfCacheKey(IList<object> methodArguments, int maxCount = 5)
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
