using DataAccessLayer.Entities.DictionaryEntities;
using DomainLayer.Models.DictionaryModels;

namespace DataAccessLayer.Interfaces.Mappers;

public interface IDictionaryElementMapper
{
    public DictionaryElementEntity ToEntity(DictionaryElementModelBase domainModel);
    public DictionaryElementModel ToDomainModel(DictionaryElementEntity entity);
}