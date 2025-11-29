public interface ICacheService
{
    Task SetAsync<T>(string key, T value, TimeSpan duration);
    Task<T?> GetAsync<T>(string key);
}
