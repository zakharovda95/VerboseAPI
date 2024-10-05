using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Database;

public sealed class AppDbContext : DbContext
{
    private readonly string _connectionString;
    public AppDbContext(string connectionString)
    {
        _connectionString = connectionString;
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}