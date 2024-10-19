using AutoMapper;
using DataAccessLayer.Entities.DictionaryEntities;
using DomainLayer.Models.DictionaryModels;

namespace DataAccessLayer.AutoMapperProfiles.DictionaryAutoMapperProfiles;

/// <summary>
/// Профиль автомаппера для преобразований элемента словаря
/// </summary>
public class DictionaryElementAutoMapperProfile : Profile
{
    public DictionaryElementAutoMapperProfile()
    {
        CreateMap<DictionaryElementModel, DictionaryElementEntity>();
        CreateMap<DictionaryElementModel, DictionaryElementModelBase>();
        CreateMap<DictionaryElementEntity, DictionaryElementModel>();
    }
}