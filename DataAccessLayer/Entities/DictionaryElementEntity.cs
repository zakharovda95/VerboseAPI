using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class DictionaryElementEntity : BaseEntity
{
    public string Title { get; init; } = string.Empty;
    public int Value { get; init; }
    
    public int DictionaryId { get; set; }
    public DictionaryEntity? Dictionary { get; set; }
}