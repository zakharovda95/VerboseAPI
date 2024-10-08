using DomainLayer.Models.DictionaryModels;

namespace DomainLayer.Interfaces.Repositories.Dictionary;

public interface IDictionaryElementRepository
{
    #region CREATE
    
    public Task<bool> AddElement(int dictionaryId, DictionaryElementModelShort elementData);
    public Task<bool> CopyAllElements(int dictionaryId, int toDictionaryId);
    public Task<bool> CopyElements(int[] elementIds, int toDictionaryId);
    public Task<bool> CopyElementById(int elementId, int toDictionaryId);
    
    #endregion
    

    #region READ

    public Task<List<DictionaryElementModel>> GetAllElements();
    public Task<List<DictionaryElementModel>> GetElements(int[] elementsIds);
    public Task<DictionaryElementModel> GetElementById(int elementId);

    #endregion


    #region UPDATE

    public Task<bool> EditElement(int elementId, DictionaryElementModelShort newElementData);
    public Task<bool> MoveAllElements(int dictionaryId, int toDictionaryId);
    public Task<bool> MoveElements(int[] elementIds, int toDictionaryId);
    public Task<bool> MoveElementById(int elementId, int toDictionaryId);

    #endregion


    #region DELETE

    public Task<bool> DeleteAllElements(int dictionaryId);
    public Task<bool> DeleteElements(int[] elementIds);
    public Task<bool> DeleteElementById(int elementId);

    #endregion
    
}