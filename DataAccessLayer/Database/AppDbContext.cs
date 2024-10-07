using DataAccessLayer.Entities.DictionaryEntities;
using DataAccessLayer.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Database;

/// <summary>
/// Клиент EF для MySQL
/// </summary>
public sealed class AppDbContext : DbContext
{
    public DbSet<DictionaryEntity> Dictionaries => Set<DictionaryEntity>();
    public DbSet<DictionaryElementEntity> DictionaryElements => Set<DictionaryElementEntity>();
    public AppDbContext(DbContextOptions<AppDbContext> optionsBuilder) : base(optionsBuilder)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DictionaryConfiguration());
        modelBuilder.ApplyConfiguration(new DictionaryElementConfiguration());
    }
}