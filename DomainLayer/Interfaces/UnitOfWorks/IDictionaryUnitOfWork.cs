using DomainLayer.Interfaces.Repositories;
using DomainLayer.Models.DictionaryModels;

namespace DomainLayer.Interfaces.UnitOfWorks;

/// <summary>
/// Интерфейс объекта UnitOfWork для работы со словарями и элементами словарей.
/// </summary>
public interface IDictionaryUnitOfWork : IUnitOfWork
{
    /// <summary>
    /// Репозиторий словарей.
    /// </summary>
    IRepository<DictionaryModel, DictionaryModelBase> DictionaryRepository { get; init; }
    
    /// <summary>
    /// Репозиторий элементов словарей.
    /// </summary>
    IRepository<DictionaryElementModel, DictionaryElementModelBase> DictionaryElementRepository { get; init; }
}