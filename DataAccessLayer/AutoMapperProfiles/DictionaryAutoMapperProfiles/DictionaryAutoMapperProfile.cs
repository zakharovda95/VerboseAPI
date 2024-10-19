using AutoMapper;
using DataAccessLayer.Entities.DictionaryEntities;
using DomainLayer.Models.DictionaryModels;

namespace DataAccessLayer.AutoMapperProfiles.DictionaryAutoMapperProfiles;

/// <summary>
/// Профиль автомаппера для преобразования словаря
/// </summary>
public class DictionaryAutoMapperProfile : Profile
{
    public DictionaryAutoMapperProfile()
    {
        CreateMap<DictionaryModel, DictionaryEntity>();
        CreateMap<DictionaryModel, DictionaryModelBase>();
        CreateMap<DictionaryModel, DictionaryModelInfo>();
        CreateMap<DictionaryEntity, DictionaryModel>();
    }
}