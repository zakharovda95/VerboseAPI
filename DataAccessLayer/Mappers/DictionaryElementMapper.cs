using DataAccessLayer.Entities.DictionaryEntities;
using DomainLayer.Models.DictionaryModels;

namespace DataAccessLayer.Mappers;

public class DictionaryElementMapper
{
    public DictionaryElementEntity ToEntity(DictionaryElementModelShort domainModel)
    {
        if (domainModel is null)
            throw new NullReferenceException(nameof(DictionaryElementModelShort));
        
        return new DictionaryElementEntity
        {
            Title = domainModel.Title,
            Value = domainModel.Value
        };
    }

    public DictionaryElementModelShort ToDomainModelShort(DictionaryElementEntity entity)
    {
        if (entity is null)
            throw new NullReferenceException(nameof(DictionaryElementEntity));
        
        return new DictionaryElementModelShort
        {
            Id = entity.Id,
            Title = entity.Title,
            Value = entity.Value
        };
    }
    
    public DictionaryElementModel ToDomainModel(DictionaryElementModel entity)
    {
        if (entity is null)
            throw new NullReferenceException(nameof(DictionaryElementEntity));
        
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