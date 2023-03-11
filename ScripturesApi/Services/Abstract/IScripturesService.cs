using ScripturesApi.Domain.Entities;
using ScripturesApi.Domain.SelectModels;
using ScripturesApi.Requests;
using ScripturesApi.ViewModels.Paging;

namespace ScripturesApi.Services.Abstract;

public interface IScripturesService
{
    Task<Book?> GetBookAsync(int id);
    Task<PagedResponse<T>> FilterBooksAsync<T>(FilterBooksRequest filter) where T : ViewModels.Book;
    Task<Chapter?> GetChapterAsync<T>(int id) where T : ISelectStatement<Chapter, Chapter>, new();
    Task<PagedResponse<T1>> FilterChaptersAsync<T1, T2>(FilterChaptersRequest filter) where T1 : ViewModels.Chapter where T2 : ISelectStatement<Chapter, Chapter>, new();
    Task<Verse?> GetVerseAsync<T>(int id) where T : ISelectStatement<Verse, Verse>, new();
    Task<PagedResponse<T1>> GetVersesAsync<T1, T2>(PagedVersesRequest request) where T1 : ViewModels.Verse where T2 : ISelectStatement<Verse, Verse>, new();
}
