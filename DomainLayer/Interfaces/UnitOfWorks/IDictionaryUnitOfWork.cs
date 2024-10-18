using DomainLayer.Interfaces.Repositories;
using DomainLayer.Models.DictionaryModels;

namespace DomainLayer.Interfaces.UnitOfWorks;

/// <summary>
/// Интерфейс объекта UnitOfWork для работы со словарями и элементами словарей
/// </summary>
public interface IDictionaryUnitOfWork : IUnitOfWork
{
    IRepository<DictionaryModel, DictionaryModelBase> DictionaryRepository { get; init; }
    IRepository<DictionaryElementModel, DictionaryElementModelBase> DictionaryElementRepository { get; init; }
}