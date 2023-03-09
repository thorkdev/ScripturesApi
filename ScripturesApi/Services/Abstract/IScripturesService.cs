using ScripturesApi.Domain.Entities;
using ScripturesApi.Requests;

namespace ScripturesApi.Services.Abstract;

public interface IScripturesService
{
    Task<Book?> GetBookAsync(int id);
    Task<List<Book>> FilterBooksAsync(FilterBooksRequest filter);
}
