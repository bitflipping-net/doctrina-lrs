using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Doctrina.Application.Common.Caching
{
    public abstract class MemoryCache<TRequest, TResponse> : ICache<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IMemoryCache _memoryCache;

        protected virtual DateTime? AbsoluteExpiration { get; }
        protected virtual TimeSpan? AbsoluteExpirationRelativeToNow { get; }
        protected virtual TimeSpan? SlidingExpiration { get; }

        protected MemoryCache(
            IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        protected abstract string GetCacheKeyIdentifier(TRequest request);

        private static string GetCacheKey(string id)
        {
            return $"{typeof(TRequest).FullName}:{id}";
        }

        private string GetCacheKey(TRequest request)
        {
            return GetCacheKey(GetCacheKeyIdentifier(request));
        }

        public virtual Task<TResponse> Get(TRequest request)
        {
            return _memoryCache.TryGetValue(GetCacheKey(request), out var cachedResponse)
                ? Task.FromResult((TResponse)cachedResponse)
                : Task.FromResult(default(TResponse));
        }

        public virtual Task Set(TRequest request, TResponse value)
        {
            // var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value));
            _memoryCache.Set(
                GetCacheKey(request),
                value,
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = AbsoluteExpiration,
                    AbsoluteExpirationRelativeToNow = AbsoluteExpirationRelativeToNow,
                    SlidingExpiration = SlidingExpiration
                });
            return Task.CompletedTask;
        }

        public Task Remove(string cacheKeyIdentifier)
        {
            _memoryCache.Remove(GetCacheKey(cacheKeyIdentifier));
            return Task.CompletedTask;
        }
    }
}