using DomainLayer.Interfaces.Services;
using DomainLayer.Interfaces.UnitOfWorks;
using DomainLayer.Models.DictionaryModels;

namespace DomainLayer.Services;

/// <summary>
/// Сервис работы с элементами словарей.
/// </summary>
public class DictionaryElementService : IDictionaryElementService
{
    private readonly IDictionaryUnitOfWork _dictionaryUnitOfWork;

    public DictionaryElementService(IDictionaryUnitOfWork dictionaryUnitOfWork)
    {
        ArgumentNullException.ThrowIfNull(dictionaryUnitOfWork, nameof(dictionaryUnitOfWork));
        _dictionaryUnitOfWork = dictionaryUnitOfWork;
    }

    public async Task<bool> AddDictionaryElementAsync(DictionaryElementModelBase data, int? toId = null)
    {
        ArgumentNullException.ThrowIfNull(data, nameof(data));
        ArgumentNullException.ThrowIfNull(toId, nameof(toId));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero((int)toId, nameof(toId));

        await _dictionaryUnitOfWork.DictionaryElementRepository.AddAsync(data, toId);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }

    public async Task<IEnumerable<DictionaryElementModel>> GetAllDictionaryElementsAsync()
    {
        return await _dictionaryUnitOfWork.DictionaryElementRepository.GetAllAsync();
    }

    public async Task<DictionaryElementModel?> GetDictionaryElementByIdAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
        return await _dictionaryUnitOfWork.DictionaryElementRepository.GetByIdAsync(id);
    }

    public async Task<bool> UpdateDictionaryElementAsync(int id, DictionaryElementModelBase newData)
    {
        ArgumentNullException.ThrowIfNull(newData, nameof(newData));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));

        await _dictionaryUnitOfWork.DictionaryElementRepository.UpdateAsync(id, newData);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }

    public async Task<bool> DeleteAllDictionaryElementsAsync()
    {
        await _dictionaryUnitOfWork.DictionaryElementRepository.DeleteAllAsync();
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }

    public async Task<bool> DeleteAnyDictionaryElementsAsync(IEnumerable<int> ids)
    {
        ArgumentNullException.ThrowIfNull(ids, nameof(ids));

        await _dictionaryUnitOfWork.DictionaryElementRepository.DeleteAnyAsync(ids);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }

    public async Task<bool> DeleteDictionaryElementByIdAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));

        await _dictionaryUnitOfWork.DictionaryElementRepository.DeleteByIdAsync(id);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }

    public async Task<bool> CopyAllDictionaryElementsFromDictionaryAsync(int fromDictionaryId, int toDictionaryId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(fromDictionaryId, nameof(fromDictionaryId));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toDictionaryId, nameof(toDictionaryId));

        var fromDictionary = await _dictionaryUnitOfWork.DictionaryRepository.GetByIdAsync(fromDictionaryId);
        var elementsForCopy = fromDictionary?.Elements;
        if (elementsForCopy is null) return false; // добавить ошибку
        
        await _dictionaryUnitOfWork.DictionaryElementRepository.AddRangeAsync(elementsForCopy, toDictionaryId);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }

    public async Task<bool> CopyAnyDictionaryElementsFromDictionaryAsync(IEnumerable<int> ids, int toDictionaryId)
    {
        ArgumentNullException.ThrowIfNull(ids, nameof(ids));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toDictionaryId, nameof(toDictionaryId));
        
        var elements = await _dictionaryUnitOfWork.DictionaryElementRepository.GetAnyAsync(ids);
        var mappedElements = elements.Select(a => a.ToDictionaryElementModelBase());
        await _dictionaryUnitOfWork.DictionaryElementRepository.AddRangeAsync(mappedElements, toDictionaryId);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }

    public async Task<bool> CopyDictionaryElementByIdFromDictionaryAsync(int id, int toDictionaryId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toDictionaryId, nameof(toDictionaryId));

        var element = await _dictionaryUnitOfWork.DictionaryElementRepository.GetByIdAsync(id);
        if (element is null) return false; // ошибка

        await _dictionaryUnitOfWork.DictionaryElementRepository.AddAsync(element.ToDictionaryElementModelBase(), toDictionaryId);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }
    
    public async Task<bool> MoveAllDictionaryElementsFromDictionaryAsync(int fromDictionaryId, int toDictionaryId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(fromDictionaryId, nameof(fromDictionaryId));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toDictionaryId, nameof(toDictionaryId));

        var dictionary = await _dictionaryUnitOfWork.DictionaryRepository.GetByIdAsync(fromDictionaryId);
        var elements = dictionary?.Elements;
        if (elements is null) return false; // ошибка

        var elementsList = elements.ToList();
        await _dictionaryUnitOfWork.DictionaryElementRepository.AddRangeAsync(elementsList, toDictionaryId);

        var elementIds = elementsList
            .Select(a => a.Id)
            .Where(b => b.HasValue)
            .Select(c => c!.Value)
            .ToList();
        await _dictionaryUnitOfWork.DictionaryElementRepository.DeleteAnyAsync(elementIds);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }

    public async Task<bool> MoveAnyDictionaryElementsFromDictionaryAsync(IEnumerable<int> ids, int toDictionaryId)
    {
        ArgumentNullException.ThrowIfNull(ids, nameof(ids));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toDictionaryId, nameof(toDictionaryId));

        var elements = await _dictionaryUnitOfWork.DictionaryElementRepository.GetAnyAsync(ids);
        var mappedElements = elements.Select(a => a.ToDictionaryElementModelBase());
        await _dictionaryUnitOfWork.DictionaryElementRepository.AddRangeAsync(mappedElements, toDictionaryId);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }

    public async Task<bool> MoveDictionaryElementByIdFromDictionaryAsync(int id, int toDictionaryId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toDictionaryId, nameof(toDictionaryId));

        var element = await _dictionaryUnitOfWork.DictionaryElementRepository.GetByIdAsync(id);
        if (element is null) return false; // ошибка
        await _dictionaryUnitOfWork.DictionaryElementRepository.AddAsync(element.ToDictionaryElementModelBase(), toDictionaryId);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }
}