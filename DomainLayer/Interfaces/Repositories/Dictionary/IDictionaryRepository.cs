using DomainLayer.Models.DictionaryModels;

namespace DomainLayer.Interfaces.Repositories.Dictionary;

/// <summary>
/// Репозиторий для взаимодействия с сущностью [DictionaryEntity]
/// </summary>
public interface IDictionaryRepository
{
    #region CREATE

    public Task<bool> AddDictionary(DictionaryModelShort dictionaryData);

    #endregion


    #region READ

    public Task<List<DictionaryModelBase>> GetDictionaryList();
    public Task<List<DictionaryModel>> GetAllDictionaries();
    public Task<List<DictionaryModel> > GetDictionaries(int[] dictionaryIds);
    public Task<DictionaryModelFull?> GetDictionaryById(int dictionaryId);

    #endregion


    #region UPDATE

    public Task<bool> EditDictionaryById(int dictionaryId, DictionaryModelShort newDictionaryData);

    #endregion

    #region DELETE

    public Task<bool> CleanAllDictionaries();
    public Task<bool> CleanDictionaries(int[] dictionaryIds);
    public Task<bool> CleanDictionaryById(int dictionaryId);
    public Task<bool> DeleteAllDictionaries();
    public Task<bool> DeleteDictionaries(int[] dictionaryIds);
    public Task<bool> DeleteDictionaryById(int dictionaryId);

    #endregion
}