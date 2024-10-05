using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Database;

/// <summary>
/// Клиент EF для MS SQL Server
/// </summary>
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