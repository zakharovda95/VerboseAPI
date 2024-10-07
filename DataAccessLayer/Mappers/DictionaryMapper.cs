using DataAccessLayer.Entities.DictionaryEntities;
using DomainLayer.Models.DictionaryModels;

namespace DataAccessLayer.Mappers;

public class DictionaryMapper
{
    public DictionaryEntity ToEntity(DictionaryModelShort domainModel)
    {
        if (domainModel is null)
            throw new NullReferenceException(nameof(DictionaryModelShort));
        
        var elements = new List<DictionaryElementEntity>();
        if (elements.Count > 0)
        {
            foreach (var domainElement in domainModel.Elements)
            {
                elements.Add(new DictionaryElementEntity
                {
                    Title = domainElement.Title,
                    Value = domainElement.Value
                });
            }
        }
        
        return new DictionaryEntity
        {
            Title = domainModel.Title,
            Description = domainModel.Description,
            Elements = elements
        };
    }

    public DictionaryModelShort ToDomainModelShort(DictionaryEntity entity)
    {
        if (entity is null)
            throw new NullReferenceException(nameof(DictionaryEntity));

        return new DictionaryModelShort
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Elements = _getElementsShort(entity.Elements)
        };
    }
    
    public DictionaryModelBase ToDomainModelBase(DictionaryEntity entity)
    {
        if (entity is null)
            throw new NullReferenceException(nameof(DictionaryEntity));

        return new DictionaryModelBase
        {
            Id = entity.Id,
            Credate = entity.Credate,
            LastUpdate = entity.LastUpdate,
            Type = entity.Type,
            Title = entity.Title,
            Description = entity.Description,
        };
    }
    
    public DictionaryModel ToDomainModel(DictionaryEntity entity)
    {
        if (entity is null)
            throw new NullReferenceException(nameof(DictionaryEntity));

        return new DictionaryModel
        {
            Id = entity.Id,
            Credate = entity.Credate,
            LastUpdate = entity.LastUpdate,
            Type = entity.Type,
            Title = entity.Title,
            Description = entity.Description,
            Elements = _getElementsShort(entity.Elements)
        };
    }
    
    public DictionaryModelFull ToDomainModelFull(DictionaryEntity entity)
    {
        if (entity is null)
            throw new NullReferenceException(nameof(DictionaryEntity));

        var elements = new List<DictionaryElementModel>();
        if (entity.Elements.Count > 0)
        {
            foreach (var entityElement in entity.Elements)
            {
                elements.Add(new DictionaryElementModel
                {
                    Id = entityElement.Id,
                    Credate = entityElement.Credate,
                    LastUpdate = entityElement.LastUpdate,
                    Type = entityElement.Type,
                    Title = entityElement.Title,
                    Value = entityElement.Value,
                });
            }
        }
        
        return new DictionaryModelFull()
        {
            Id = entity.Id,
            Credate = entity.Credate,
            LastUpdate = entity.LastUpdate,
            Type = entity.Type,
            Title = entity.Title,
            Description = entity.Description,
            Elements = elements
        };
    }

    private List<DictionaryElementModelShort> _getElementsShort(List<DictionaryElementEntity> elementEntities)
    {
        var elements = new List<DictionaryElementModelShort>();
        if (elementEntities.Count > 0)
        {
            foreach (var entityElement in elementEntities)
            {
                elements.Add(new DictionaryElementModelShort()
                {
                    Id = entityElement.Id,
                    Title = entityElement.Title,
                    Value = entityElement.Value
                });
            }
        }

        return elements;
    }
}