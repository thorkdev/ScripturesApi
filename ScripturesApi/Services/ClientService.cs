using Microsoft.EntityFrameworkCore;
using ScripturesApi.Domain;
using ScripturesApi.Domain.Entities;
using ScripturesApi.Services.Abstract;

namespace ScripturesApi.Services;

internal class ClientService : IClientService
{
    private readonly ILogger<ClientService> _logger;
    private readonly ApplicationDbContext _context;

    public ClientService(ILogger<ClientService> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<bool> ApiKeyExistsAsync(Guid key)
    {
        try
        {
            var keyExists = await _context.ClientKeys
                .AnyAsync(x => x.ApiKey == key);

            return keyExists;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, key);
        }

        return false;
    }

    public async Task<bool> ApiKeyIsActiveAsync(Guid key)
    {
        try
        {
            var keyIsActive = await _context.ClientKeys
                .Where(x => x.ApiKey == key)
                .Select(x => x.IsActive)
                .FirstOrDefaultAsync();

            return keyIsActive;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, key);
        }

        return false;
    }

    public async Task<ClientKeyRole?> GetClientRoleAsync(Guid key)
    {
        try
        {
            var role = await _context.ClientKeys
                .Where(x => x.ApiKey == key)
                .Select(x => x.ClientKeyRole)
                .FirstOrDefaultAsync();

            return role;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, key);
        }

        return null;
    }

    public async Task<Guid?> CreateApiKeyAsync()
    {
        try
        {
            var clientKey = new ClientKey
            {
                IsActive = true,
                CreatedAtUtc = DateTime.UtcNow
            };

            await _context.ClientKeys.AddAsync(clientKey);

            await _context.SaveChangesAsync();

            _logger.LogInformation($"Api key created at {DateTime.UtcNow}.", clientKey.ApiKey);

            return clientKey.ApiKey;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return null;
    }

    public async Task ToggleApiKeyAsync(Guid key)
    {
        try
        {
            var clientKey = await _context.ClientKeys
                .FirstOrDefaultAsync(x => x.ApiKey == key);

            if (clientKey == null)
            {
                throw new NullReferenceException($"Failed to find client with api key ({key}).");
            }

            clientKey.IsActive = !clientKey.IsActive;
            clientKey.UpdatedAtUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation($"Api key toggled at {DateTime.UtcNow}.", clientKey.ApiKey);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, key);
        }
    }
}
