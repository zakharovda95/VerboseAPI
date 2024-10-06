using System.Reflection;

namespace DataAccessLayer.Enums;

/// <summary>
/// Перечисление названий таблиц БД
/// </summary>
public enum TableNameEnum
{
    [TableName("DICTIONARY")]
    Dictionary,
    [TableName("DICTIONARYELEMENT")]
    DictionaryElement,
}

/// <summary>
/// Добавляет строковое название таблицы к перечислению [TableNameEnum]
/// </summary>
/// <param name="tableName">Название таблицы</param>
[AttributeUsage(AttributeTargets.Field)]
public class TableNameAttribute(string tableName) : Attribute
{
    public string TableName { get; } = tableName;
}

/// <summary>
/// Расширение для получения строкового представления поля перечисления [TableNameEnum] 
/// </summary>
public static class TableNameExtension
{
    public static string? GetTableName(this TableNameEnum value)
    {
        Type type = value.GetType();
        FieldInfo? field  = type.GetField(value.ToString());
        if (field is null) return null;
        TableNameAttribute? attribute = field.GetCustomAttribute<TableNameAttribute>();
        if (attribute is null) return null;
        return attribute.TableName;
    }
}

