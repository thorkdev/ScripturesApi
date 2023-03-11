namespace ScripturesApi.ViewModels.Paging.Abstract;

public interface IPagedResponse<T>
{
    public Page? Page { get; set; }

    public int TotalCount { get; set; }

    public IList<T> Items { get; set; }
}
