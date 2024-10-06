using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EntityConfigurations;

internal class DictionaryElementConfiguration() : 
    BaseEntityConfiguration<DictionaryElementEntity>(TableNameEnum.DictionaryElement.GetTableName())
{
    private readonly string? _tableName = TableNameEnum.DictionaryElement.GetTableName();
    public override void Configure(EntityTypeBuilder<DictionaryElementEntity> builder)
    {
        if (string.IsNullOrEmpty(_tableName)) 
            throw new NullReferenceException(TableNameEnum.Dictionary.ToString());
        base.Configure(builder);
        builder.Property(a => a.Title).HasColumnName($"{_tableName}_TITLE");
        builder.Property(a => a.Value).HasColumnName($"{_tableName}_VALUE");
        builder.Property(a => a.DictionaryId).HasColumnName($"{_tableName}_DICTIONARYID");
        builder.Property(a => a.Dictionary).HasColumnName($"{_tableName}_DICTIONARY");
        
        builder
            .HasOne(a => a.Dictionary)
            .WithMany(b => b.Elements)
            .HasForeignKey(c => c.DictionaryId);
    }
}