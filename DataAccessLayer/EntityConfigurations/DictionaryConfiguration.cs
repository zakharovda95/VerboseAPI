using DataAccessLayer.Entities.DictionaryEntities;
using DataAccessLayer.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EntityConfigurations;

/// <summary>
/// Конфигурация сущности словаря.
/// </summary>
internal class DictionaryConfiguration() : 
    BaseEntityConfiguration<DictionaryEntity>(TableNameEnum.Dictionary.GetTableName())
{
    private readonly string? _tableName = TableNameEnum.Dictionary.GetTableName();
    public override void Configure(EntityTypeBuilder<DictionaryEntity> builder)
    {
        if (string.IsNullOrEmpty(_tableName)) 
            throw new NullReferenceException(TableNameEnum.Dictionary.ToString());
        
        base.Configure(builder);
        builder.Property(a => a.Title).HasColumnName($"{_tableName}_TITLE").IsRequired();
        builder.Property(a => a.Description).HasColumnName($"{_tableName}_DESCRIPTION").IsRequired();
        builder.Property(a => a.Elements).HasColumnName($"{_tableName}_DICTIONARYELEMENTS").IsRequired();
        
        builder
            .HasMany(a => a.Elements)
            .WithOne(b => b.Dictionary)
            .HasForeignKey(c => c.DictionaryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}