namespace DomainLayer.Interfaces.UnitOfWorks;

/// <summary>
/// Базовый интерфейс паттерна UnitOfWork.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Сохранить изменения в БД.
    /// </summary>
    /// <returns>Количество сохраненных строк.</returns>
    Task<int> CommitAsync();
}