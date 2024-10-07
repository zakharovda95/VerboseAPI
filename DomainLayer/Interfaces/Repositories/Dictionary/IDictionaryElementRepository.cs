using DomainLayer.Models.DictionaryModels;

namespace DomainLayer.Interfaces.Repositories.Dictionary;

public interface IDictionaryElementRepository
{
    public Task<List<DictionaryElementModel>> GetElements(int[]? elementsIds);
    public Task<DictionaryElementModel> GetElementById(int elementId);
    public Task<bool> AddElement(int dictionaryId, DictionaryElementModelShort elementData);
    public Task<bool> EditElement(int dictionaryId, DictionaryElementModelShort newElementData);
    public Task<bool> DeleteElements(int dictionaryId, int[] elementIds);
    public Task<bool> DeleteElementById(int dictionaryId, int elementId);
    public Task<bool> CopyAllElements(int dictionaryId, int toDictionaryId);
    public Task<bool> CopyElements(int dictionaryId, int toDictionaryId, int[] elementIds);
    public Task<bool> CopyElementById(int dictionaryId, int toDictionaryId, int elementId);
    public Task<bool> MoveAllElements(int dictionaryId, int toDictionaryId);
    public Task<bool> MoveElements(int dictionaryId, int toDictionaryId, int[] elementIds);
    public Task<bool> MoveElementById(int dictionaryId, int toDictionaryId, int elementId);
    
}