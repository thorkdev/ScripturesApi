namespace ScripturesApi.Domain.Entities;

public class Book : EntityBase
{
    public string? Title { get; set; }
    public string? FullTitle { get; set; }
    public string? Heading { get; set; }
    public string? Slug { get; set; }

    public virtual ICollection<Chapter>? Chapters { get; set; }
}
