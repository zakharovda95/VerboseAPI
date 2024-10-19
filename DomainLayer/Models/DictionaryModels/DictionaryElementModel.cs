namespace DomainLayer.Models.DictionaryModels;

/// <summary>
/// Базовая модель элемента словаря.
/// Включает:
///  - мета-данные элемента словаря
///  - основную информацию элемента словаря
///  - метод преобразования к [DictionaryElementModelBase]
/// </summary>
public class DictionaryElementModel : BaseModel
{
    public string Title { get; init; } = string.Empty;
    public int Value { get; init; }

    /// <summary>
    /// Приводит к [DictionaryElementModelBase]
    /// </summary>
    public DictionaryElementModelBase ToDictionaryElementModelBase()
    {
        return new DictionaryElementModelBase
        {
            Id = Id,
            Title = Title,
            Value = Value
        };
    }
    
    public void Deconstruct(
        out int? id, 
        out DateTime? credate, 
        out DateTime? lastUpdate, 
        out int? type,
        out string title, 
        out int value)
    {
        base.Deconstruct(out id, out credate, out lastUpdate, out type);
        title = Title;
        value = Value;
    }
}

/// <summary>
/// Основные данные элемента словаря.
/// Включает:
///  - основную информацию элемента словаря (без мета-данных)
/// </summary>
public class DictionaryElementModelBase
{
    public int? Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public int Value { get; init; } 
    
    public void Deconstruct(out int? id, out string title, out int value)
    {
        id = Id;
        title = Title;
        value = Value;
    }
}