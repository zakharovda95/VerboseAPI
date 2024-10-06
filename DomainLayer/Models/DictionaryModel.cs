namespace DomainLayer.Models;

/// <summary>
/// Модель для создания нового словаря
/// </summary>
public class DictionaryModelShort
{
    public int? Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public List<DictionaryElementModelShort> Elements { get; init; } = new();
}

/// <summary>
/// Модель для получения информации о словаре (без его элементов)
/// </summary>
public class DictionaryModelBase : BaseModel
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}

/// <summary>
/// Модель для получения информации о словаря (включая краткую информацию о его элементах)
/// </summary>
public class DictionaryModel : DictionaryModelBase
{
    public List<DictionaryElementModelShort> Elements { get; init; } = new();
}

/// <summary>
/// Модель для получения информации о словаря (включая полную информацию о его элементах)
/// </summary>
public class DictionaryModelFull : DictionaryModelBase
{
    public List<DictionaryElementModel> Elements { get; init; } = new();
}