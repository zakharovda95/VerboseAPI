using DomainLayer.Interfaces.Repositories.Dictionary;
using DomainLayer.Interfaces.Services;
using DomainLayer.Models.DictionaryModels;

namespace DomainLayer.Services;

public class DictionaryElementService : IDictionaryElementService
{
    private IDictionaryElementRepository _dictionaryElementRepository;

    public DictionaryElementService(IDictionaryElementRepository dictionaryElementRepository)
    {
        ArgumentNullException.ThrowIfNull(dictionaryElementRepository);
        _dictionaryElementRepository = dictionaryElementRepository;
    }

    public async Task<bool> CreateAsync(DictionaryElementModelShort data, int? toId = null)
    {
        return await _dictionaryElementRepository.CreateAsync(data, toId);
    }

    public async Task<IEnumerable<DictionaryElementModel>> GetAllAsync()
    {
        return await _dictionaryElementRepository.GetAllAsync();
    }

    public async Task<IEnumerable<DictionaryElementModel>> GetAnyAsync(int[] ids)
    {
        return await _dictionaryElementRepository.GetAnyAsync(ids);
    }

    public async Task<DictionaryElementModel?> GetByIdAsync(int id)
    {
        return await _dictionaryElementRepository.GetByIdAsync(id);
    }

    public async Task<bool> UpdateAsync(int id, DictionaryElementModelShort newData)
    {
        return await _dictionaryElementRepository.UpdateAsync(id, newData);
    }

    public async Task<bool> DeleteAllAsync()
    {
        return await _dictionaryElementRepository.DeleteAllAsync();
    }

    public async Task<bool> DeleteAnyAsync(int[] ids)
    {
        return await _dictionaryElementRepository.DeleteAnyAsync(ids);
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        return await _dictionaryElementRepository.DeleteByIdAsync(id);
    }

    public async Task<bool> CopyAllAsync(int fromDictionaryId, int toDictionaryId)
    {
        return await _dictionaryElementRepository.CopyAllAsync(fromDictionaryId, toDictionaryId);
    }

    public async Task<bool> CopyAnyAsync(int[] ids, int toDictionaryId)
    {
        return await _dictionaryElementRepository.CopyAnyAsync(ids, toDictionaryId);
    }

    public async Task<bool> CopyByIdAsync(int id, int toDictionaryId)
    {
        return await _dictionaryElementRepository.CopyByIdAsync(id, toDictionaryId);
    }

    public async Task<bool> MoveAllAsync(int fromDictionaryId, int toDictionaryId)
    {
        return await _dictionaryElementRepository.MoveAllAsync(fromDictionaryId, toDictionaryId);
    }

    public async Task<bool> MoveAnyAsync(int[] ids, int toDictionaryId)
    {
        return await _dictionaryElementRepository.MoveAnyAsync(ids, toDictionaryId);
    }

    public async Task<bool> MoveByIdAsync(int id, int toDictionaryId)
    {
        return await _dictionaryElementRepository.MoveByIdAsync(id, toDictionaryId);
    }
}