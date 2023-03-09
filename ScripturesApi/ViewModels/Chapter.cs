namespace ScripturesApi.ViewModels;

public class Chapter
{
    public int Index { get; set; }
    public string? Reference { get; set; }
    public Book? Book { get; set; }
}
