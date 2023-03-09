namespace ScripturesApi.Services.Abstract;

public interface IClientService
{
    Task<bool> ApiKeyExistsAsync(Guid key);
    Task<bool> ApiKeyIsActiveAsync(Guid key);
    Task<Guid?> CreateApiKeyAsync();
    Task ToggleApiKeyAsync(Guid key);
}
