namespace DomainLayer.Models.DictionaryModels;

/// <summary>
/// Модель для получения информации/добавления записи в словаре (которткая версия)
/// </summary>
public class DictionaryElementModelShort
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

/// <summary>
/// Модель для получения информации записи в словаре (полная версия)
/// </summary>
public class DictionaryElementModel : BaseModel
{
    public string Title { get; init; } = string.Empty;
    public int Value { get; init; } 
    
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