using ScripturesApi.Domain.Entities;
using System.Linq.Expressions;

namespace ScripturesApi.Domain.SelectModels.Chapters;

public class ChapterBase : ISelectStatement<Chapter, Chapter>
{
    public Expression<Func<Chapter, Chapter>> SelectStatement => c => new()
    {
        Id = c.Id,
        Index = c.Index,
        Reference = c.Reference,
        BookId = c.BookId,
        Book = c.Book == null ? null : new()
        {
            Id = c.Book.Id,
            Title = c.Book.Title,
            Slug = c.Book.Slug
        }
    };
}
