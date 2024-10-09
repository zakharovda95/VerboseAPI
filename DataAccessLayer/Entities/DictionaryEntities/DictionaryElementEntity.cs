namespace DataAccessLayer.Entities.DictionaryEntities;

public class DictionaryElementEntity : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public int Value { get; set; }
    
    public int DictionaryId { get; set; }
    public DictionaryEntity? Dictionary { get; set; }
}