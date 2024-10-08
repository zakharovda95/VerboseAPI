using DataAccessLayer.Entities.DictionaryEntities;
using DomainLayer.Models.DictionaryModels;

namespace DataAccessLayer.Mappers;

public class DictionaryElementMapper
{
    public DictionaryElementEntity ToEntity(DictionaryElementModelShort domainModel)
    {
        if (domainModel is null) 
            ArgumentNullException.ThrowIfNull(domainModel, nameof(domainModel));
        
        return new DictionaryElementEntity
        {
            Title = domainModel.Title,
            Value = domainModel.Value
        };
    }

    public DictionaryElementModelShort ToDomainModelShort(DictionaryElementEntity entity)
    {
        if (entity is null)
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        
        return new DictionaryElementModelShort
        {
            Id = entity.Id,
            Title = entity.Title,
            Value = entity.Value
        };
    }
    
    public DictionaryElementModel ToDomainModel(DictionaryElementEntity entity)
    {
        if (entity is null)
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