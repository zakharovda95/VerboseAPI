using DomainLayer.Models.DictionaryModels;

namespace DataAccessLayer.Interfaces.Repositories.Dictionary;

public interface IDictionaryElementRepository : IRepository<DictionaryElementModel, DictionaryElementModelShort>
{
    public Task<bool> CopyAllAsync(int fromDictionaryId, int toDictionaryId);
    public Task<bool> CopyAnyAsync(int[] ids, int toDictionaryId);
    public Task<bool> CopyByIdAsync(int id, int toDictionaryId);
    public Task<bool> MoveAllAsync(int fromDictionaryId, int toDictionaryId);
    public Task<bool> MoveAnyAsync(int[] ids, int toDictionaryId);
    public Task<bool> MoveByIdAsync(int ids, int toDictionaryId);
}