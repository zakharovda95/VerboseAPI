using DomainLayer.Models.DictionaryModels;

namespace DomainLayer.Interfaces.Services;

public interface IDictionaryService
{
    Task<bool> CreateAsync(DictionaryModelBase data, int? toId = null);
    Task<IEnumerable<DictionaryModel>> GetAllAsync();
    Task<IEnumerable<DictionaryModel>> GetAnyAsync(int[] ids);
    Task<DictionaryModel?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, DictionaryModelBase newData);
    Task<bool> DeleteAllAsync();
    Task<bool> DeleteAnyAsync(int[] ids);
    Task<bool> DeleteByIdAsync(int id);
    public Task<IEnumerable<DictionaryModelBase>> GetListAsync();
    public Task<bool> CleanAllAsync();
    public Task<bool> CleanAnyAsync(int[] ids);
    public Task<bool> CleanByIdAsync(int id);
}