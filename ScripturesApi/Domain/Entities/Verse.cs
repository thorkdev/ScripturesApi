namespace ScripturesApi.Domain.Entities;

public class Verse : EntityBase
{
    public int ChapterId { get; set; }
    public int Index { get; set; }
    public string? Reference { get; set; }
    public string? Text { get; set; }
    public virtual Chapter? Chapter { get; set; }
}
