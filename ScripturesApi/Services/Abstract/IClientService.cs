using ScripturesApi.Domain.Entities;

namespace ScripturesApi.Services.Abstract;

public interface IClientService
{
    Task<bool> ApiKeyExistsAsync(Guid key);
    Task<bool> ApiKeyIsActiveAsync(Guid key);
    Task<ClientKeyRole?> GetClientRoleAsync(Guid key);
    Task<Guid?> CreateApiKeyAsync(ClientKeyRole? role);
    Task ToggleApiKeyAsync(Guid key);
}
