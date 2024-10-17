using DataAccessLayer.Entities.DictionaryEntities;
using DataAccessLayer.Interfaces.Mappers;
using DomainLayer.Models.DictionaryModels;

namespace DataAccessLayer.Mappers;

public class DictionaryElementMapper : IDictionaryElementMapper
{
    public DictionaryElementEntity ToEntity(DictionaryElementModelShort domainModel)
    {
        ArgumentNullException.ThrowIfNull(domainModel, nameof(domainModel));
        return new DictionaryElementEntity
        {
            Title = domainModel.Title,
            Value = domainModel.Value
        };
    }
    
    public DictionaryElementModel ToDomainModel(DictionaryElementEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        return new DictionaryElementModel
        {
            Id = entity.Id,
            Credate = entity.Credate,
            LastUpdate = entity.LastUpdate,
            Type = entity.Type,
            Title = entity.Title,
            Value = entity.Value
        };
    }
}