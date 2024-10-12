using DataAccessLayer.Entities.DictionaryEntities;
using DomainLayer.Models.DictionaryModels;

namespace DataAccessLayer.Interfaces.Mappers;

public interface IDictionaryMapper
{
    public DictionaryEntity ToEntity(DictionaryModelShort domainModel);
    public DictionaryModelBase ToDomainModelBase(DictionaryEntity entity);
    public DictionaryModel ToDomainModel(DictionaryEntity entity);
}