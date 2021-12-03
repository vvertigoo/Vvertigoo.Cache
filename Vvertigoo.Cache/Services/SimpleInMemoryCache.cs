using System.Collections.Concurrent;

namespace Vvertigoo.Cache.Services;

public class SimpleInMemoryCache : ICacheService
{
    private readonly ConcurrentDictionary<string, string> Cache;

    public SimpleInMemoryCache()
    {
        Cache = new ConcurrentDictionary<string, string>(2, 100);
    }

    public int ItemsCount => Cache.Count;

    public IEnumerable<string> Keys => Cache.Keys;

    public Task<string?> Get(string key)
    {
        Cache.TryGetValue(key, out var value);
        return Task.FromResult(value);
    }

    public Task Set(string key, string value)
    {
        if (!Cache.TryAdd(key, value)) Cache[key] = value;

        return Task.CompletedTask;
    }
}
