using ScripturesApi.Domain.Entities;
using System.Linq.Expressions;

namespace ScripturesApi.Domain.SelectModels.Verses;

public class VerseBase : ISelectStatement<Verse, Verse>
{
    public Expression<Func<Verse, Verse>> SelectStatement => v => new()
    {
        Id = v.Id,
        Index = v.Index,
        Reference = v.Reference,
        Text = v.Text,
        ChapterId = v.ChapterId,
        Chapter = v.Chapter == null ? null : new()
        {
            Id = v.Chapter.Id,
            BookId = v.Chapter.BookId,
            Index = v.Chapter.Index,
            Reference = v.Chapter.Reference,
        }
    };
}
