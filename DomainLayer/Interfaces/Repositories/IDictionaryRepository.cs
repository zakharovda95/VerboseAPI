using DomainLayer.Models;

namespace DomainLayer.Interfaces.Repositories;

/// <summary>
/// Репозиторий для взаимодействия с сущностью [DictionaryEntity]
/// </summary>
public interface IDictionaryRepository
{
    public Task<bool> AddDictionary(DictionaryModelShort dictionaryData);
    public List<DictionaryModelBase> GetDictionaryList();
    public List<DictionaryModel> GetDictionaries(int[]? dictionaryIds);
    public Task<bool> CleanAllDictionaries();
    public Task<bool> DeleteAllDictionaries();
    public Task<bool> CleanDictionaries(int[]? dictionaryIds);
    public Task<bool> DeleteDictionaries(int[]? dictionaryIds);
    public Task<DictionaryModelFull> GetDictionaryById(int dictionaryId);
    public Task<bool> CleanDictionaryById(int dictionaryId);
    public Task<bool> DeleteDictionaryById(int dictionaryId);
    public Task<bool> EditDictionaryById(int dictionaryId, DictionaryModelShort newDictionaryData);
}