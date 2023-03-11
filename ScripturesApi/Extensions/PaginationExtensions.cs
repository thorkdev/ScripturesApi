using ScripturesApi.ViewModels.Paging;
using ScripturesApi.ViewModels.Paging.Abstract;

namespace ScripturesApi.Extensions;

public static class PaginationExtensions
{
    public static T DefaultPageSize<T>(this T filter, int size) where T : IFilter, new()
    {
        if (filter == null)
        {
            return new()
            {
                Page = new()
                {
                    Index = 1,
                    Size = size
                }
            };
        }

        if (filter.Page == null)
        {
            filter.Page = new()
            {
                Index = 1,
                Size = size
            };
        }
        else if (filter.Page.Size == 0)
        {
            filter.Page.Size = size;
        }

        return filter;
    }

    public static IQueryable<T> TakePage<T>(this IQueryable<T> source, Page? page)
    {
        if (page == null)
        {
            throw new ArgumentNullException(nameof(page));
        }

        if (page.Offset > 0)
        {
            source = source.Skip(page.Offset);
        }

        if (page.Size > 0)
        {
            source = source.Take(page.Size);
        }

        return source;
    }
}
