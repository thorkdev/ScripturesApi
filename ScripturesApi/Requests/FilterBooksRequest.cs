using System.ComponentModel.DataAnnotations;

namespace ScripturesApi.Requests;

public class FilterBooksRequest
{
    [MinLength(3)]
    public string? Title { get; set; }

    [MinLength(3)]
    public string? Slug { get; set; }
}
