namespace ScripturesApi.Domain.Entities;

public class Chapter : EntityBase
{
    public int BookId { get; set; }
    public int Index { get; set; }
    public string? Reference { get; set; }

    public virtual Book? Book { get; set; }
    public virtual ICollection<Verse>? Verses { get; set; }
}
