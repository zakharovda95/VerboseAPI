using DomainLayer.Models.DictionaryModels;

namespace DomainLayer.Interfaces.Services;

public interface IDictionaryElementService
{
    Task<bool> CreateAsync(DictionaryElementModelShort data, int? toId = null);
    Task<IEnumerable<DictionaryElementModel>> GetAllAsync();
    Task<IEnumerable<DictionaryElementModel>> GetAnyAsync(int[] ids);
    Task<DictionaryElementModel?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, DictionaryElementModelShort newData);
    Task<bool> DeleteAllAsync();
    Task<bool> DeleteAnyAsync(int[] ids);
    Task<bool> DeleteByIdAsync(int id);
    public Task<bool> CopyAllAsync(int fromDictionaryId, int toDictionaryId);
    public Task<bool> CopyAnyAsync(int[] ids, int toDictionaryId);
    public Task<bool> CopyByIdAsync(int id, int toDictionaryId);
    public Task<bool> MoveAllAsync(int fromDictionaryId, int toDictionaryId);
    public Task<bool> MoveAnyAsync(int[] ids, int toDictionaryId);
    public Task<bool> MoveByIdAsync(int id, int toDictionaryId);
}