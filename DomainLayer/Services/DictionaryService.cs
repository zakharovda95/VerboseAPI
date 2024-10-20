using DomainLayer.Interfaces.Services;
using DomainLayer.Interfaces.UnitOfWorks;
using DomainLayer.Models.DictionaryModels;

namespace DomainLayer.Services;

/// <summary>
/// Сервис работы со словарями.
/// </summary>
public class DictionaryService : IDictionaryService
{
    private readonly IDictionaryUnitOfWork _dictionaryUnitOfWork;

    public DictionaryService(IDictionaryUnitOfWork dictionaryUnitOfWork)
    {
        ArgumentNullException.ThrowIfNull(dictionaryUnitOfWork, nameof(dictionaryUnitOfWork));
        _dictionaryUnitOfWork = dictionaryUnitOfWork;
    }
    
    public async Task<bool> AddDictionaryAsync(DictionaryModelBase data, int? toId = null)
    {
        ArgumentNullException.ThrowIfNull(data, nameof(data));
        
        await _dictionaryUnitOfWork.DictionaryRepository.AddAsync(data);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }
    
    public async Task<IEnumerable<DictionaryModel>> GetAllDictionariesAsync()
    {
        return await _dictionaryUnitOfWork.DictionaryRepository.GetAllAsync();
    }
    
    public async Task<DictionaryModel?> GetDictionaryByIdAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));

        return await _dictionaryUnitOfWork.DictionaryRepository.GetByIdAsync(id);
    }
    
    public async Task<bool> UpdateDictionaryAsync(int id, DictionaryModelBase newData)
    {
        ArgumentNullException.ThrowIfNull(newData, nameof(newData));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));

        await _dictionaryUnitOfWork.DictionaryRepository.UpdateAsync(id, newData);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }
    
    public async Task<bool> DeleteAllDictionariesAsync()
    {
        await _dictionaryUnitOfWork.DictionaryRepository.DeleteAllAsync();
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }
    
    public async Task<bool> DeleteAnyDictionariesAsync(IEnumerable<int> ids)
    {
        ArgumentNullException.ThrowIfNull(ids, nameof(ids));

        await _dictionaryUnitOfWork.DictionaryRepository.DeleteAnyAsync(ids);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }
    
    public async Task<bool> DeleteDictionaryByIdAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
        await _dictionaryUnitOfWork.DictionaryRepository.DeleteByIdAsync(id);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }
    
    public async Task<IEnumerable<DictionaryModelInfo>> GetDictionaryInfoListAsync()
    {
        var repositories = await _dictionaryUnitOfWork.DictionaryRepository.GetAllAsync();
        return repositories.Select(a => a.ToDictionaryModelInfo()).ToList();
    }
    
    public async Task<bool> CleanAllDictionariesAsync()
    {
        await _dictionaryUnitOfWork.DictionaryElementRepository.DeleteAllAsync();
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }
    
    public async Task<bool> CleanAnyDictionariesAsync(IEnumerable<int> ids)
    {
        ArgumentNullException.ThrowIfNull(ids, nameof(ids));

        var dictionaries = await _dictionaryUnitOfWork.DictionaryRepository.GetAnyAsync(ids);
        ArgumentNullException.ThrowIfNull(dictionaries, nameof(dictionaries));
        
        var elementsForDeleting = new List<int>();
        foreach (var dictionary in dictionaries)
        {
            ArgumentNullException.ThrowIfNull(dictionary, nameof(dictionary));

            var dictionaryElements = dictionary.Elements;
            ArgumentNullException.ThrowIfNull(dictionaryElements, nameof(dictionaryElements));
            
            var elementsIds = dictionaryElements
                .Select(b => b.Id)
                .Where(id => id.HasValue)
                .Select(id => id!.Value)
                .ToList();
            if (elementsIds != null) elementsForDeleting.AddRange(elementsIds);
        }

        await _dictionaryUnitOfWork.DictionaryElementRepository.DeleteAnyAsync(elementsForDeleting);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }
    
    public async Task<bool> CleanDictionaryByIdAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
        
        var dictionary = await _dictionaryUnitOfWork.DictionaryRepository.GetByIdAsync(id);
        ArgumentNullException.ThrowIfNull(dictionary, nameof(dictionary));
        
        var elements = dictionary.Elements;
        ArgumentNullException.ThrowIfNull(elements, nameof(elements));

        var elementIds = elements
            .Select(a => a.Id)
            .Where(b => b.HasValue)
            .Select(c => c!.Value)
            .ToArray();

        await _dictionaryUnitOfWork.DictionaryElementRepository.DeleteAnyAsync(elementIds);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }
}