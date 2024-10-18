namespace DomainLayer.Interfaces.UnitOfWorks;

/// <summary>
/// Базовый интерфейс паттерна UnitOfWork
/// </summary>
public interface IUnitOfWork
{
    Task<int> CommitAsync();
}