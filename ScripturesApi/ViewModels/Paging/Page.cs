namespace ScripturesApi.ViewModels.Paging;

public class Page
{
    public int Index { get; set; } = 1;
    public int Size { get; set; }
    public int Offset => (Index - 1) * Size;
}
