using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

namespace GdeShawerma.Db;

public class TgBotContext : DbContext
{
    public DbSet<DbUser> Users { get; set; }

    public TgBotContext(DbContextOptions<TgBotContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbUser>()
            .ToContainer("Users")
            .HasPartitionKey(o => o.BotId)
            .HasNoDiscriminator()
            .UseETagConcurrency()
            ;


        modelBuilder.Entity<DbUser>().OwnsOne(
            o => o.LastLocation,
            sa =>
            {
                sa.ToJsonProperty("LastLocation");
                sa.Property(p => p.Latitude).ToJsonProperty("Latitude");
                sa.Property(p => p.Longitude).ToJsonProperty("Longitude");
            });
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}