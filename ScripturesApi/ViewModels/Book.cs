using System.Text.Json.Serialization;

namespace ScripturesApi.ViewModels;

public class Book
{
    public int Id { get; set; }

    public string? Title { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FullTitle { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Heading { get; set; }

    public string? Slug { get; set; }
}
