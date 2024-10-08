using DataAccessLayer.Database;
using DataAccessLayer.Entities.DictionaryEntities;
using DataAccessLayer.Mappers;
using DomainLayer.Interfaces.Repositories.Dictionary;
using DomainLayer.Models.DictionaryModels;
using Microsoft.EntityFrameworkCore;

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

    public async Task<bool> AddElement(int dictionaryId, DictionaryElementModelShort elementData)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(dictionaryId, nameof(dictionaryId));
        ArgumentNullException.ThrowIfNull(elementData, nameof(elementData));

        var mappedData = _dictionaryElementMapper.ToEntity(elementData);

        await _dbContext.DictionaryElements.AddAsync(mappedData);
        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> CopyAllElements(int dictionaryId, int toDictionaryId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(dictionaryId);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toDictionaryId);

        var dictionaries = await _dbContext.Dictionaries
            .Include(a => a.Elements)
            .Where(b => b.Id == dictionaryId || b.Id == toDictionaryId)
            .ToListAsync();

        if (dictionaries is null) return false;
        var fromDictionary = dictionaries.FirstOrDefault(a => a.Id == dictionaryId);
        var toDictionary = dictionaries.FirstOrDefault(a => a.Id == toDictionaryId);

        if (fromDictionary is null || toDictionary is null) return false;
        return await _dbContext.ExecuteTransactionAsync(async () =>
        {
            foreach (var element in fromDictionary.Elements)
            {
                toDictionary.Elements.Add(new DictionaryElementEntity
                {
                    Title = element.Title,
                    Value = element.Value
                });
            }

            await _dbContext.SaveChangesAsync();
        });
    }

    public async Task<bool> CopyElements(int[] elementIds, int toDictionaryId)
    {
        ArgumentNullException.ThrowIfNull(elementIds, nameof(elementIds));
        ArgumentOutOfRangeException.ThrowIfZero(elementIds.Length, nameof(elementIds));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toDictionaryId, nameof(toDictionaryId));

        var toDictionary = await _dbContext.Dictionaries
            .Include(a => a.Elements)
            .FirstOrDefaultAsync(b => b.Id == toDictionaryId);

        if (toDictionary is null) return false;

        var elements = await _dbContext.DictionaryElements
            .Where(a => elementIds.Contains(a.Id))
            .ToListAsync();

        if (elements is null || elements.Count <= 0) return false;

        return await _dbContext.ExecuteTransactionAsync(async () =>
        {
            foreach (var element in elements)
            {
                toDictionary.Elements.Add(new DictionaryElementEntity
                {
                    Title = element.Title,
                    Value = element.Value
                });
            }

            await _dbContext.SaveChangesAsync();
        });
    }

    public async Task<bool> CopyElementById(int elementId, int toDictionaryId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(elementId, nameof(elementId));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toDictionaryId, nameof(toDictionaryId));

        var toDictionary = await _dbContext.Dictionaries
            .Include(a => a.Elements)
            .FirstOrDefaultAsync(b => b.Id == toDictionaryId);

        if (toDictionary is null) return false;

        var element = await _dbContext.DictionaryElements
            .FirstOrDefaultAsync(a => a.Id == elementId);

        if (element is null) return false;
        toDictionary.Elements.Add(new DictionaryElementEntity
        {
            Title = element.Title,
            Value = element.Value
        });

        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    #endregion

    #region READ

    public async Task<List<DictionaryElementModel>> GetAllElements()
    {
        var elements = await _dbContext.DictionaryElements.ToListAsync();
        return elements
            .Select(a => _dictionaryElementMapper.ToDomainModel(a))
            .ToList();
    }

    public async Task<List<DictionaryElementModel>> GetElements(int[] elementsIds)
    {
        ArgumentOutOfRangeException.ThrowIfZero(elementsIds.Length, nameof(elementsIds));
        var elements = await _dbContext.DictionaryElements
            .Where(a => elementsIds.Contains(a.Id))
            .ToListAsync();
        return elements.Select(a => _dictionaryElementMapper.ToDomainModel(a)).ToList();
    }

    public async Task<DictionaryElementModel?> GetElementById(int elementId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(elementId, nameof(elementId));
        var element = await _dbContext.DictionaryElements.FirstOrDefaultAsync(a => a.Id == elementId);
        if (element is null) return null;
        return _dictionaryElementMapper.ToDomainModel(element);
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