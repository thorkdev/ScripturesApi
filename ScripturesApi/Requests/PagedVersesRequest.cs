using ScripturesApi.ViewModels.Paging;
using ScripturesApi.ViewModels.Paging.Abstract;
using System.ComponentModel.DataAnnotations;

namespace ScripturesApi.Requests;

public class PagedVersesRequest : IFilter
{
    public PagedVersesRequest()
    {
        Page = new();
    }

    [Required]
    public int ChapterId { get; set; }

    public Page? Page { get; set; }
}
