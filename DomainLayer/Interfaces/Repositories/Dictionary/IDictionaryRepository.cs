using DomainLayer.Models.DictionaryModels;

namespace DomainLayer.Interfaces.Repositories.Dictionary;

/// <summary>
/// Репозиторий для взаимодействия с сущностью [DictionaryEntity]
/// </summary>
public interface IDictionaryRepository
{
    public Task<bool> AddDictionary(DictionaryModelShort dictionaryData);
    public IEnumerable<DictionaryModelBase> GetDictionaryList();
    public IEnumerable<DictionaryModel> GetDictionaries(int[]? dictionaryIds);
    public Task<bool> CleanAllDictionaries();
    public Task<bool> DeleteAllDictionaries();
    public Task<bool> CleanDictionaries(int[]? dictionaryIds);
    public Task<bool> DeleteDictionaries(int[]? dictionaryIds);
    public Task<DictionaryModelFull> GetDictionaryById(int dictionaryId);
    public Task<bool> CleanDictionaryById(int dictionaryId);
    public Task<bool> DeleteDictionaryById(int dictionaryId);
    public Task<bool> EditDictionaryById(int dictionaryId, DictionaryModelShort newDictionaryData);
}