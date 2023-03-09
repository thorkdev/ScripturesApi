namespace ScripturesApi.ViewModels;

public class Verse
{
    public int Index { get; set; }
    public string? Reference { get; set; }
    public string? Text { get; set; }
    public Chapter? Chapter { get; set; }
}
