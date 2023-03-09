using Microsoft.EntityFrameworkCore;
using ScripturesApi.Domain;
using ScripturesApi.Domain.Entities;
using ScripturesApi.Services.Abstract;

namespace ScripturesApi.Services;

internal class IpService : IIpService
{
    private readonly ILogger<ClientService> _logger;
    private readonly ApplicationDbContext _context;

    public IpService(ILogger<ClientService> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task LogIpAsync(Guid apiKey, string ip, string uri)
    {
        try
        {
            var log = new IpLog
            {
                Ip = ip,
                ClientKeyId = apiKey,
                RequestUri = uri,
                RequestedAtUtc = DateTime.UtcNow
            };

            await _context.IpLogs.AddAsync(log);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, apiKey, ip);
        }
    }

    public async Task<int> GetDailyRequestsAsync(Guid apiKey)
    {
        try
        {
            var today = DateTime.UtcNow.Date;

            return await _context.IpLogs
                .Where(l => l.ClientKeyId == apiKey)
                .CountAsync(l => l.RequestedAtUtc.Date == today);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message, apiKey);
        }

        return 0;
    }
}
