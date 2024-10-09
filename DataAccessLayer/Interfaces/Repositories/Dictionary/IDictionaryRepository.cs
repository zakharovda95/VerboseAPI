using DomainLayer.Models.DictionaryModels;

namespace DataAccessLayer.Interfaces.Repositories.Dictionary;

/// <summary>
/// Репозиторий для взаимодействия со словарями
/// </summary>
public interface IDictionaryRepository : IRepository<DictionaryModel, DictionaryModelShort>
{
    public Task<List<DictionaryModelBase>> GetListAsync();
    public Task<bool> CleanAllAsync();
    public Task<bool> CleanAnyAsync(int[] ids);
    public Task<bool> CleanByIdAsync(int id);
}