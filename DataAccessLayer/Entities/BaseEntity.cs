using DataAccessLayer.Enums;

namespace DataAccessLayer.Entities;

/// <summary>
/// Базовые поля EF сущностей (обязательны во всех таблицах).
/// </summary>
public class BaseEntity
{
    public int Id { get; init; }
    public DateTime Credate { get; init; } = DateTime.Now;
    public DateTime LastUpdate { get; set; } = DateTime.Now;
    public int Type { get; set; } = (int)ColumnTypeEnum.None;
};