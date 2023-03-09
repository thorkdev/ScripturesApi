using System.ComponentModel.DataAnnotations;

namespace ScripturesApi.Domain.Entities;

public class IpLog : EntityBase
{
    public Guid ClientKeyId { get; set; }

    [StringLength(45)]
    public string? Ip { get; set; }

    public string? RequestUri { get; set; }

    public DateTime RequestedAtUtc { get; set; }

    public virtual ClientKey? ClientKey { get; set; }
}
