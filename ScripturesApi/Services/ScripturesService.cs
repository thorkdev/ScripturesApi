using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScripturesApi.Domain;
using ScripturesApi.Domain.Entities;
using ScripturesApi.Domain.SelectModels;
using ScripturesApi.Extensions;
using ScripturesApi.Requests;
using ScripturesApi.Services.Abstract;
using ScripturesApi.ViewModels.Paging;

namespace ScripturesApi.Services;

internal class ScripturesService : IScripturesService
{
    private readonly ILogger<ClientService> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ScripturesService(ILogger<ClientService> logger, ApplicationDbContext context, IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
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

    public async Task<PagedResponse<T>> FilterBooksAsync<T>(FilterBooksRequest filter) where T : ViewModels.Book
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

            var total = query.Count();

            var results = await query
                .TakePage(filter.Page)
                .ToListAsync();

            var books = _mapper.Map<IList<T>>(results);

            return new PagedResponse<T>
            {
                Items = books,
                TotalCount = total,
                Page = filter.Page
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error filtering books. Message: {ex.Message}", filter);
        }

        return new(filter.Page);
    }

    public async Task<Chapter?> GetChapterAsync<T>(int id) where T : ISelectStatement<Chapter, Chapter>, new()
    {
        try
        {
            return await _context.Chapters
                .Select(new T().SelectStatement)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting chapter. Message: {ex.Message}", id);
        }

        return null;
    }

    public async Task<PagedResponse<T1>> FilterChaptersAsync<T1, T2>(FilterChaptersRequest filter) 
        where T1 : ViewModels.Chapter 
        where T2 : ISelectStatement<Chapter, Chapter>, new()
    {
        try
        {
            var query = _context.Chapters.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Reference))
            {
                query = query.Where(c => !string.IsNullOrEmpty(c.Reference) && EF.Functions.Like(c.Reference.ToLower(), $"%{filter.Reference.ToLower()}%"));
            }

            if (filter.Index.HasValue)
            {
                query = query.Where(c => c.Index == filter.Index);
            }

            if (filter.BookIds != null)
            {
                query = query.Where(c => filter.BookIds.Contains(c.BookId));
            }

            var total = query.Count();

            var results = await query
                .Select(new T2().SelectStatement)
                .TakePage(filter.Page)
                .ToListAsync();

            var chapters = _mapper.Map<IList<T1>>(results);

            return new PagedResponse<T1>
            {
                Items = chapters,
                TotalCount = total,
                Page = filter.Page
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error filtering chapters. Message: {ex.Message}", filter);
        }

        return new(filter.Page);
    }

    public async Task<Verse?> GetVerseAsync<T>(int id) where T : ISelectStatement<Verse, Verse>, new()
    {
        try
        {
            return await _context.Verses
                .Select(new T().SelectStatement)
                .FirstOrDefaultAsync(v => v.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting verse. Message: {ex.Message}", id);
        }

        return null;
    }

    public async Task<PagedResponse<T1>> GetVersesAsync<T1, T2>(PagedVersesRequest request)
        where T1 : ViewModels.Verse
        where T2 : ISelectStatement<Verse, Verse>, new()
    {
        try
        {
            
            var query = _context.Verses
                .Where(v => v.ChapterId == request.ChapterId)
                .AsQueryable();

            var total = query.Count();

            var results = await query
                .Select(new T2().SelectStatement)
                .TakePage(request.Page)
                .ToListAsync();

            var verses = _mapper.Map<IList<T1>>(results);

            return new PagedResponse<T1>
            {
                Items = verses,
                TotalCount = total,
                Page = request.Page
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting verses. Message: {ex.Message}", request);
        }

        return new(request.Page);
    }
}
