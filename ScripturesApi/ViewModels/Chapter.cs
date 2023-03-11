using System.Text.Json.Serialization;

namespace ScripturesApi.ViewModels;

public class Chapter
{
    public int Id { get; set; }

    public int Index { get; set; }

    public string? Reference { get; set; }

    public int BookId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Book? Book { get; set; }
}
