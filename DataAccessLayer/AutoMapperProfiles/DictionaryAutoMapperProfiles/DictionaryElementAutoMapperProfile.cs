using AutoMapper;
using DataAccessLayer.Entities.DictionaryEntities;
using DomainLayer.Models.DictionaryModels;

namespace DataAccessLayer.AutoMapperProfiles.DictionaryAutoMapperProfiles;

/// <summary>
/// Профиль автомаппера для преобразований элемента словаря (EF-сущность <-> Доменная модель),
/// </summary>
public class DictionaryElementAutoMapperProfile : Profile
{
    public DictionaryElementAutoMapperProfile()
    {
        CreateMap<DictionaryElementModel, DictionaryElementEntity>();
        CreateMap<DictionaryElementEntity, DictionaryElementModel>();
    }
}