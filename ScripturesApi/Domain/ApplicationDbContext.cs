using Microsoft.EntityFrameworkCore;
using ScripturesApi.Domain.Entities;

namespace ScripturesApi.Domain;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ClientKey> ClientKeys { get; set; }
    public DbSet<IpLog> IpLogs { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Chapter> Chapters { get; set; }
    public DbSet<Verse> Verses { get; set; }
}
