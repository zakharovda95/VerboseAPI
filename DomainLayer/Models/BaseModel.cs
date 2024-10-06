namespace DomainLayer.Models;

/// <summary>
/// Базовая модель
/// </summary>
public class BaseModel
{
    public int? Id { get; init; }
    public DateTime? Credate { get; init; }
    public DateTime? LastUpdate { get; set; }
    public int? Type { get; set; }
}