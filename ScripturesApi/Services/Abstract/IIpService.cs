namespace ScripturesApi.Services.Abstract;

public interface IIpService
{
    Task LogIpAsync(Guid apiKey, string ip, string uri);
    Task<int> GetDailyRequestsAsync(Guid apiKey);
}
