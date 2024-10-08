using System.Runtime.CompilerServices;
using DataAccessLayer.Entities.DictionaryEntities;
using DataAccessLayer.EntityConfigurations;
using DataAccessLayer.Enums;
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

    public async Task<bool> ClearTablesAsync(List<TableNameEnum> tableNames)
    {
        if (tableNames.Count <= 0) return false;

        var operations = new List<string>();
        foreach (var tableName in tableNames)
        {
            var name = tableName.GetTableName();
            if (string.IsNullOrEmpty(name)) return false;
            operations.Add($"DELETE FROM [{name}]");
            operations.Add($"ALTER TABLE [{name}] AUTO_INCREMENT = 1;");
        }

        return await ExecuteTransactionAsync(operations);
    }

    public async Task<bool> ExecuteTransactionAsync(IEnumerable<string> operations)
    {
        var transaction = await Database.BeginTransactionAsync();
        try
        {
            foreach (var operation in operations)
            {
                var formattedOperation = FormattableStringFactory.Create(operation);
                await Database.ExecuteSqlAsync(formattedOperation);
            }

            await transaction.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return false;
        }
    }
    
    public async Task<bool> ExecuteTransactionAsync(Func<Task> operations)
    {
        var transaction = await Database.BeginTransactionAsync();
        try
        {
            await operations.Invoke();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return false;
        }
    }
}