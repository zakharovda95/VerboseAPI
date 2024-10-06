using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EntityConfigurations;

/// <summary>
/// Определяет настройки для базовых колонок таблицы БД
/// </summary>
/// <param name="tableName">
/// Название таблицы из перечисления [TableNameEnum]
/// Является префиксом для названия колонки
/// </param>
/// <typeparam name="T">Тип сущности</typeparam>
internal abstract class BaseEntityConfiguration<T>(string tableName) : 
    IEntityTypeConfiguration<T> where T : class
{
    /// <summary>
    /// Виртуальный метод установки настроек базовых колонок таблицы,
    /// Необходимо переопределить в дочернем классе с вызовом базового функционала (base.Configure())
    /// </summary>
    /// <param name="builder">Объект строителя конфигурации EF</param>
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.ToTable(tableName);
        builder.HasKey("Id").HasName($"{tableName}_PK");
        builder.Property("Id")
            .HasColumnName($"{tableName}_ID")
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property("Credate").HasColumnName($"{tableName}_CREDATE").IsRequired();
        builder.Property("LastUpdate").HasColumnName($"{tableName}_LASTUPDATE").IsRequired();
        builder.Property("Type").HasColumnName($"{tableName}_TYPE");
    }
}