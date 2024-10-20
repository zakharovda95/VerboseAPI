namespace DataAccessLayer.Entities.DictionaryEntities;

/// <summary>
/// EF сущность словаря.
/// </summary>
public class DictionaryEntity : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<DictionaryElementEntity> Elements { get; set; } = new ();
}