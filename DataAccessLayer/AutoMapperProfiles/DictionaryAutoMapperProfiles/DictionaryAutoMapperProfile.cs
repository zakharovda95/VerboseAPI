using AutoMapper;
using DataAccessLayer.Entities.DictionaryEntities;
using DomainLayer.Models.DictionaryModels;

namespace DataAccessLayer.AutoMapperProfiles.DictionaryAutoMapperProfiles;

/// <summary>
/// Профиль автомаппера для преобразования словаря (EF-сущность <-> Доменная модель).
/// </summary>
public class DictionaryAutoMapperProfile : Profile
{
    public DictionaryAutoMapperProfile()
    {
        CreateMap<DictionaryModel, DictionaryEntity>();
        CreateMap<DictionaryEntity, DictionaryModel>();
    }
}