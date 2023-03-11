using ScripturesApi.ViewModels.Paging;
using ScripturesApi.ViewModels.Paging.Abstract;
using System.ComponentModel.DataAnnotations;

namespace ScripturesApi.Requests;

public class FilterChaptersRequest : IFilter
{
    public FilterChaptersRequest()
    {
        Page = new();
    }

    public int? Index { get; set; }

    [MinLength(3)]
    public string? Reference { get; set; }

    public List<int>? BookIds { get; set; }

    public Page? Page { get; set; }
}
