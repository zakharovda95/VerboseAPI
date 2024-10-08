using DataAccessLayer.Database;
using DataAccessLayer.Mappers;
using DomainLayer.Interfaces.Repositories.Dictionary;
using DomainLayer.Models.DictionaryModels;

namespace DataAccessLayer.Repositories.DictionaryRepositories;

public class DictionaryElementRepository : IDictionaryElementRepository
{
    private readonly AppDbContext _dbContext;
    private readonly DictionaryElementMapper _dictionaryElementMapper;

    public DictionaryElementRepository(AppDbContext dbContext, DictionaryElementMapper dictionaryElementMapper)
    {
        _dbContext =
            dbContext ?? throw new NullReferenceException(nameof(AppDbContext));
        _dictionaryElementMapper =
            dictionaryElementMapper ?? throw new NullReferenceException(nameof(DictionaryElementMapper));
    }

    #region CREATE

    public Task<bool> AddElement(int dictionaryId, DictionaryElementModelShort elementData)
    {
        throw new NotImplementedException();
    }
    
    public Task<bool> CopyAllElements(int dictionaryId, int toDictionaryId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CopyElements(int[] elementIds, int toDictionaryId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CopyElementById(int elementId, int toDictionaryId)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region READ

    public Task<List<DictionaryElementModel>> GetAllElements()
    {
        throw new NotImplementedException();
    }

    public Task<List<DictionaryElementModel>> GetElements(int[] elementsIds)
    {
        throw new NotImplementedException();
    }

    public Task<DictionaryElementModel> GetElementById(int elementId)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region UPDATE

    public Task<bool> EditElement(int elementId, DictionaryElementModelShort newElementData)
    {
        throw new NotImplementedException();
    }

    public Task<bool> MoveAllElements(int dictionaryId, int toDictionaryId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> MoveElements(int[] elementIds, int toDictionaryId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> MoveElementById(int elementId, int toDictionaryId)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region DELETE

    public Task<bool> DeleteAllElements(int dictionaryId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteElements(int[] elementIds)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteElementById(int elementId)
    {
        throw new NotImplementedException();
    }

    #endregion
}