namespace DomainLayer.Models.DictionaryModels;

/// <summary>
/// Модель для создания нового словаря
/// </summary>
public class DictionaryModelShort
{
    public int? Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public IEnumerable<DictionaryElementModelShort>? Elements { get; init; }

    public void Deconstruct(
        out int? id, 
        out string title, 
        out string description,
        out IEnumerable<DictionaryElementModelShort>? elements)
    {
        id = Id;
        title = Title;
        description = Description;
        elements = Elements;
    }
}

/// <summary>
/// Модель для получения информации о словаре (без его элементов)
/// </summary>
public class DictionaryModelBase : BaseModel
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;

    public void Deconstruct(
        out int? id, 
        out DateTime? credate, 
        out DateTime? lastUpdate, 
        out int? type,
        out string title, 
        out string description)
    {
        base.Deconstruct(out id, out credate, out lastUpdate, out type);
        title = Title;
        description = Description;
    }
}

/// <summary>
/// Модель для получения информации о словаря (включая краткую информацию о его элементах)
/// </summary>
public class DictionaryModel : DictionaryModelBase
{
    public IEnumerable<DictionaryElementModelShort>? Elements { get; init; }
    
    public void Deconstruct(
        out int? id, 
        out DateTime? credate, 
        out DateTime? lastUpdate, 
        out int? type,
        out string title, 
        out string description,
        out IEnumerable<DictionaryElementModelShort>? elements)
    {
        base.Deconstruct(out id, out credate, out lastUpdate, out type, out title, out description);
        elements = Elements;
    }
}