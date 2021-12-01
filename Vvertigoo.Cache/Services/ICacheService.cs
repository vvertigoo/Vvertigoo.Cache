namespace Vvertigoo.Cache.Services
{
    public interface ICacheService
    {
        Task<string?> Get(string key);        
        Task Set(string key, string value);
        int ItemsCount { get; }
        IEnumerable<string> Keys { get; }
    }
}
