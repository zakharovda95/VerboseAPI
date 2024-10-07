namespace DataAccessLayer.Entities.DictionaryEntities;

public class DictionaryElementEntity : BaseEntity
{
    public string Title { get; init; } = string.Empty;
    public int Value { get; init; }
    
    public int DictionaryId { get; set; }
    public DictionaryEntity? Dictionary { get; set; }
}