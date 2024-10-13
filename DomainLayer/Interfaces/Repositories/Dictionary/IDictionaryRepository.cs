using DomainLayer.Models.DictionaryModels;

namespace DomainLayer.Interfaces.Repositories.Dictionary;

/// <summary>
/// Репозиторий для взаимодействия со словарями
/// </summary>
public interface IDictionaryRepository : IRepository<DictionaryModel, DictionaryModelShort>
{
    public Task<IEnumerable<DictionaryModelBase>> GetListAsync();
    public Task<bool> CleanAllAsync();
    public Task<bool> CleanAnyAsync(int[] ids);
    public Task<bool> CleanByIdAsync(int id);
}