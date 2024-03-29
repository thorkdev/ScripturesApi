﻿using ScripturesApi.ViewModels.Paging;
using ScripturesApi.ViewModels.Paging.Abstract;
using System.ComponentModel.DataAnnotations;

namespace ScripturesApi.Requests;

public class FilterBooksRequest : IFilter
{
    public FilterBooksRequest()
    {
        Page = new();
    }

    [MinLength(3)]
    public string? Title { get; set; }

    [MinLength(3)]
    public string? Slug { get; set; }

    public Page? Page { get; set; }
}
