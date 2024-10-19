namespace DomainLayer.Models.DictionaryModels;

/// <summary>
/// Базовая модель словаря.
/// Включает:
///  - мета-данные словаря
///  - основную информацию элементов словаря
///  - методы преобразования к типам [DictionaryModelBase], [DictionaryModelInfo]
/// </summary>
public class DictionaryModel : BaseModel
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public IEnumerable<DictionaryElementModelBase>? Elements { get; init; }

    /// <summary>
    /// Приводит к [DictionaryModelBase]
    /// </summary>
    public DictionaryModelBase ToDictionaryModelBase()
    {
        return new DictionaryModelBase
        {
            Id = Id,
            Title = Title,
            Description = Description,
            Elements = Elements,
        };
    }
    
    /// <summary>
    /// Приводит к [DictionaryModelInfo]
    /// </summary>
    public DictionaryModelInfo ToDictionaryModelInfo()
    {
        return new DictionaryModelInfo
        {
            Id = Id,
            Credate = Credate,
            LastUpdate = LastUpdate,
            Type = Type,
            Title = Title,
            Description = Description,
        };
    }
    
    public void Deconstruct(
        out int? id, 
        out DateTime? credate, 
        out DateTime? lastUpdate, 
        out int? type,
        out string title, 
        out string description,
        out IEnumerable<DictionaryElementModelBase>? elements)
    {
        base.Deconstruct(out id, out credate, out lastUpdate, out type);
        title = Title;
        description = Description;
        elements = Elements;
    }
}

/// <summary>
/// Основные данные словаря.
/// Включает:
///  - основную информацию словаря (без мета-данных)
///  - основную информацию элементов словаря
/// Применяется для создания / чтения словаря
/// </summary>
public class DictionaryModelBase
{
    public int? Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public IEnumerable<DictionaryElementModelBase>? Elements { get; init; }

    public void Deconstruct(
        out int? id, 
        out string title, 
        out string description,
        out IEnumerable<DictionaryElementModelBase>? elements)
    {
        id = Id;
        title = Title;
        description = Description;
        elements = Elements;
    }
}

/// <summary>
/// Информация о словаре.
/// Включает:
///  - мета-данные словаря
///  - основную информацию словаря (без информации элементов словаря)
/// </summary>
public class DictionaryModelInfo : BaseModel
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