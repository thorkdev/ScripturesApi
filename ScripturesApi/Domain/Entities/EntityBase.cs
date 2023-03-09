using System.ComponentModel.DataAnnotations;

namespace ScripturesApi.Domain.Entities;

public class EntityBase
{
    [Key]
    public int Id { get; set; }
}
