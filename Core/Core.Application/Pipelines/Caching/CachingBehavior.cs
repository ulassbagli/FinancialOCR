using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Core.Application.Pipelines.Caching;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICachableRequest
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;
    private readonly CacheSettings _cacheSettings;

    public CachingBehavior(IDistributedCache cache, ILogger<CachingBehavior<TRequest, TResponse>> logger, IConfiguration configuration)
    {
        _cache = cache;
        _logger = logger;
        _cacheSettings = configuration.GetSection(nameof(CacheSettings)).Get<CacheSettings>();
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        TResponse response;
        if(request.BypassCache) return await next();

        async Task<TResponse> GetResponseAndAddToCache()
        {
            response = await next();
            TimeSpan? slidingExpiration = request.SlidingExpiration ?? TimeSpan.FromDays(_cacheSettings.SlidingExpiration);
            DistributedCacheEntryOptions cacheEntryOptions = new() { SlidingExpiration = slidingExpiration };
            byte[] serializedResponse = JsonSerializer.SerializeToUtf8Bytes(response);
            await _cache.SetAsync(request.CacheKey, serializedResponse, cacheEntryOptions, cancellationToken);
            return response;
        }
        
        byte[]? cachedResponse = await _cache.GetAsync(request.CacheKey, cancellationToken);
        if(cachedResponse is null)
        {
            _logger.LogInformation("Cache miss and added for key {CacheKey}", request.CacheKey);
            response = await GetResponseAndAddToCache();
        }
        
        response = JsonSerializer.Deserialize<TResponse>(cachedResponse);
        _logger.LogInformation("Cache hit for key {CacheKey}", request.CacheKey);
        return response;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}