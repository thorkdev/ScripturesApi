using ScripturesApi.ViewModels.Paging.Abstract;

namespace ScripturesApi.ViewModels.Paging;

public class PagedResponse<T> : IPagedResponse<T>
{
    public PagedResponse() { }

    public PagedResponse(Page? page)
    {
        Page = page ?? new();
    }

    public Page? Page { get; set; }
    public int TotalCount { get; set; }
    public IList<T> Items { get; set; } = Enumerable.Empty<T>().ToList();
}
