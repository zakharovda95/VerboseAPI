using DataAccessLayer.Entities.DictionaryEntities;
using DataAccessLayer.Interfaces.Mappers;
using DomainLayer.Models.DictionaryModels;

namespace DataAccessLayer.Mappers;

public class DictionaryMapper : IDictionaryMapper
{
    public DictionaryEntity ToEntity(DictionaryModelShort domainModel)
    {
        ArgumentNullException.ThrowIfNull(domainModel, nameof(domainModel));

        var elements = new List<DictionaryElementEntity>();

        if (domainModel.Elements is not null && domainModel.Elements.Any())
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

    public DictionaryModelBase ToDomainModelBase(DictionaryEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

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
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

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