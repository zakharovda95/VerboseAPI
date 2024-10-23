using DomainLayer.Interfaces.Services;
using DomainLayer.Interfaces.UnitOfWorks;
using DomainLayer.Models.DictionaryModels;
using Microsoft.Extensions.Logging;

namespace DomainLayer.Services;

/// <summary>
/// Сервис работы со словарями.
/// </summary>
public class DictionaryService : IDictionaryService
{
    private readonly IDictionaryUnitOfWork _dictionaryUnitOfWork;
    private readonly ILogger<DictionaryService> _logger;
    public DictionaryService(IDictionaryUnitOfWork dictionaryUnitOfWork, ILogger<DictionaryService> logger)
    {
        ArgumentNullException.ThrowIfNull(dictionaryUnitOfWork, nameof(dictionaryUnitOfWork));
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        _dictionaryUnitOfWork = dictionaryUnitOfWork;
        _logger = logger;
    }
    
    public async Task<bool> AddDictionaryAsync(DictionaryModelBase data, int? toId = null)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(data, nameof(data));
            await _dictionaryUnitOfWork.DictionaryRepository.AddAsync(data);
            var res = await _dictionaryUnitOfWork.CommitAsync();
            return res > 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method}; Параметры: data: {@Data}, toId: {@ToId}", nameof(AddDictionaryAsync), data, toId);

            throw;
        }
    }
    
    public async Task<IEnumerable<DictionaryModel>> GetAllDictionariesAsync()
    {
        try
        {
            return await _dictionaryUnitOfWork.DictionaryRepository.GetAllAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method}; Параметры: нет", nameof(GetAllDictionariesAsync));
            throw;
        }
    }
    
    public async Task<DictionaryModel?> GetDictionaryByIdAsync(int id)
    {
        try
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
            return await _dictionaryUnitOfWork.DictionaryRepository.GetByIdAsync(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method} Параметры: id: {@Id}", nameof(GetDictionaryByIdAsync), id);
            throw;
        }
    }
    
    public async Task<bool> UpdateDictionaryAsync(int id, DictionaryModelBase newData)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(newData, nameof(newData));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
            await _dictionaryUnitOfWork.DictionaryRepository.UpdateAsync(id, newData);
            var res = await _dictionaryUnitOfWork.CommitAsync();
            return res > 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method} Параметры: id: {@Id}, newData: {@NewDate}",
                nameof(UpdateDictionaryAsync), id, newData);
            throw;
        }
    }
    
    public async Task<bool> DeleteAllDictionariesAsync()
    {
        try
        {
            await _dictionaryUnitOfWork.DictionaryRepository.DeleteAllAsync();
            var res = await _dictionaryUnitOfWork.CommitAsync();
            return res > 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method} Параметры: нет", nameof(DeleteAllDictionariesAsync));
            throw;
        }
    }
    
    public async Task<bool> DeleteAnyDictionariesAsync(IEnumerable<int> ids)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(ids, nameof(ids));
            await _dictionaryUnitOfWork.DictionaryRepository.DeleteAnyAsync(ids);
            var res = await _dictionaryUnitOfWork.CommitAsync();
            return res > 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method} Параметры: ids: {@Ids}", nameof(DeleteAnyDictionariesAsync), ids);
            throw;
        }
    }
    
    public async Task<bool> DeleteDictionaryByIdAsync(int id)
    {
        try
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
            await _dictionaryUnitOfWork.DictionaryRepository.DeleteByIdAsync(id);
            var res = await _dictionaryUnitOfWork.CommitAsync();
            return res > 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method} Параметры: id: {@Id}", nameof(DeleteDictionaryByIdAsync), id);
            throw;
        }
    }
    
    public async Task<IEnumerable<DictionaryModelInfo>> GetDictionaryInfoListAsync()
    {
        try
        {
            var repositories = await _dictionaryUnitOfWork.DictionaryRepository.GetAllAsync();
            return repositories.Select(a => a.ToDictionaryModelInfo()).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method} Параметры: нет", nameof(GetDictionaryInfoListAsync));
            throw;
        }
    }
    
    public async Task<bool> CleanAllDictionariesAsync()
    {
        try
        {
            await _dictionaryUnitOfWork.DictionaryElementRepository.DeleteAllAsync();
            var res = await _dictionaryUnitOfWork.CommitAsync();
            return res > 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method} Параметры: нет", nameof(CleanAllDictionariesAsync));
            throw;
        }
    }
    
    public async Task<bool> CleanAnyDictionariesAsync(IEnumerable<int> ids)
    {
        try
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
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method} Параметры: ids: {@Ids}", nameof(CleanAnyDictionariesAsync), ids);
            throw;
        }
    }
    
    public async Task<bool> CleanDictionaryByIdAsync(int id)
    {
        try
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
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method} Параметры: id: {@Id}", nameof(CleanDictionaryByIdAsync), id);
            throw;
        }
    }
}