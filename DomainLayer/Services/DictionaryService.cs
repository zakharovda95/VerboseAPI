using DomainLayer.Interfaces.Repositories.Dictionary;
using DomainLayer.Interfaces.Services;
using DomainLayer.Models.DictionaryModels;

namespace DomainLayer.Services;

public class DictionaryService : IDictionaryService
{
    private readonly IDictionaryRepository _dictionaryRepository;
    public DictionaryService(IDictionaryRepository dictionaryRepository)
    {
        ArgumentNullException.ThrowIfNull(dictionaryRepository);
        _dictionaryRepository = dictionaryRepository;
    }
    
    public async Task<bool> CreateAsync(DictionaryModelShort data, int? toId = null)
    {
        return await _dictionaryRepository.CreateAsync(data, toId);
    }

    public async Task<IEnumerable<DictionaryModel>> GetAllAsync()
    {
        return await _dictionaryRepository.GetAllAsync();
    }

    public async Task<IEnumerable<DictionaryModel>> GetAnyAsync(int[] ids)
    {
        return await _dictionaryRepository.GetAnyAsync(ids);
    }

    public async Task<DictionaryModel?> GetByIdAsync(int id)
    {
        return await _dictionaryRepository.GetByIdAsync(id);
    }

    public async Task<bool> UpdateAsync(int id, DictionaryModelShort newData)
    {
        return await _dictionaryRepository.UpdateAsync(id, newData);
    }

    public async Task<bool> DeleteAllAsync()
    {
        return await _dictionaryRepository.DeleteAllAsync();
    }

    public async Task<bool> DeleteAnyAsync(int[] ids)
    {
        return await _dictionaryRepository.DeleteAnyAsync(ids);
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        return await _dictionaryRepository.DeleteByIdAsync(id);
    }

    public async Task<IEnumerable<DictionaryModelBase>> GetListAsync()
    {
        return await _dictionaryRepository.GetListAsync();
    }

    public async Task<bool> CleanAllAsync()
    {
        return await _dictionaryRepository.CleanAllAsync();
    }

    public async Task<bool> CleanAnyAsync(int[] ids)
    {
        return await _dictionaryRepository.CleanAnyAsync(ids);
    }

    public async Task<bool> CleanByIdAsync(int id)
    {
        return await _dictionaryRepository.CleanByIdAsync(id);
    }
}