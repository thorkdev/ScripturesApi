using Microsoft.EntityFrameworkCore;
using ScripturesApi.Domain;
using ScripturesApi.Domain.Entities;
using ScripturesApi.Requests;
using ScripturesApi.Services.Abstract;

namespace ScripturesApi.Services;

internal class ScripturesService : IScripturesService
{
    private readonly ILogger<ClientService> _logger;
    private readonly ApplicationDbContext _context;

    public ScripturesService(ILogger<ClientService> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<Book?> GetBookAsync(int id)
    {
        try
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting book. Message: {ex.Message}", id);
        }

        return null;
    }

    public async Task<List<Book>> FilterBooksAsync(FilterBooksRequest filter)
    {
        try
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(b => !string.IsNullOrEmpty(b.Title) && EF.Functions.Like(b.Title.ToLower(), $"%{filter.Title.ToLower()}%"));
            }

            if (!string.IsNullOrEmpty(filter.Slug))
            {
                query = query.Where(b => !string.IsNullOrEmpty(b.Slug) && EF.Functions.Like(b.Slug.ToLower(), $"%{filter.Slug.ToLower()}%"));
            }

            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error filtering books. Message: {ex.Message}", filter);
        }

        return new();
    }
}
