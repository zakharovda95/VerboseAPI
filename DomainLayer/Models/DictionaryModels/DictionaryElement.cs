namespace DomainLayer.Models.DictionaryModels;

/// <summary>
/// Модель для получения информации/добавления записи в словаре (которткая версия)
/// </summary>
public class DictionaryElementModelShort
{
    public int? Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public int Value { get; init; } 
}

/// <summary>
/// Модель для получения информации записи в словаре (полная версия)
/// </summary>
public class DictionaryElementModel : BaseModel
{
    public string Title { get; init; } = string.Empty;
    public int Value { get; init; } 
}