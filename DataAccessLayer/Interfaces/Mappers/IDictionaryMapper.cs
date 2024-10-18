using DataAccessLayer.Entities.DictionaryEntities;
using DomainLayer.Models.DictionaryModels;

namespace DataAccessLayer.Interfaces.Mappers;

public interface IDictionaryMapper
{
    public DictionaryEntity ToEntity(DictionaryModelBase domainModel);
    public DictionaryModelInfo ToDomainModelBase(DictionaryEntity entity);
    public DictionaryModel ToDomainModel(DictionaryEntity entity);
}