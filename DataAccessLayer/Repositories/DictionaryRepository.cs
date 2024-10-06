using DataAccessLayer.Database;
using DataAccessLayer.Entities;
using DataAccessLayer.Mappers;
using DomainLayer.Interfaces.Repositories;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class DictionaryRepository : IDictionaryRepository
{
    private readonly AppDbContext _dbContext;
    private readonly DictionaryMapper _dictionaryMapper;
    private readonly DictionaryElementMapper _dictionaryElementMapper;

    public DictionaryRepository(
        AppDbContext dbContext,
        DictionaryMapper dictionaryMapper,
        DictionaryElementMapper dictionaryElementMapper)
    {
        _dbContext =
            dbContext ?? throw new NullReferenceException(nameof(AppDbContext));
        _dictionaryMapper =
            dictionaryMapper ?? throw new NullReferenceException(nameof(DictionaryMapper));
        _dictionaryElementMapper =
            dictionaryElementMapper ?? throw new NullReferenceException(nameof(DictionaryElementMapper));
    }

    public async Task<bool> AddDictionary(DictionaryModelShort dictionaryData)
    {
        if (dictionaryData is null)
            throw new NullReferenceException(nameof(DictionaryModelShort));
        var dictionaryEntity = _dictionaryMapper.ToEntity(dictionaryData);
        await _dbContext.Dictionaries.AddAsync(dictionaryEntity);
        int res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    public List<DictionaryModelBase> GetDictionaryList()
    {
        var models = new List<DictionaryModelBase>();
        var dictionaryEntities = _dbContext.Dictionaries
            .Include(a => a.Elements)
            .ToList();

        if (dictionaryEntities is null || dictionaryEntities.Count <= 0) return models;

        foreach (var entity in dictionaryEntities)
            models.Add(_dictionaryMapper.ToDomainModelBase(entity));

        return models;
    }

    public List<DictionaryModel> GetDictionaries(int[]? dictionaryIds)
    {
        var models = new List<DictionaryModel>();
        List<DictionaryEntity> dictionaryEntities;
        
        if (dictionaryIds is null || dictionaryIds.Length <= 0)
        {
            dictionaryEntities = _dbContext.Dictionaries
                .Include(a => a.Elements)
                .ToList();
        }
        else
        {
            dictionaryEntities = _dbContext.Dictionaries
                .Include(a => a.Elements)
                .Where(b => dictionaryIds.Contains(b.Id))
                .ToList();
        }
        
        if (dictionaryEntities is not null && dictionaryEntities.Count > 0 )
        {
            foreach (var entity in dictionaryEntities)
                models.Add(_dictionaryMapper.ToDomainModel(entity));
        }

        return models;
    }

    public Task<bool> CleanAllDictionaries()
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAllDictionaries()
    {
        throw new NotImplementedException();
    }

    public Task<bool> CleanDictionaries(int[]? dictionaryIds)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteDictionaries(int[]? dictionaryIds)
    {
        throw new NotImplementedException();
    }

    public Task<DictionaryModelFull> GetDictionaryById(int dictionaryId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CleanDictionaryById(int dictionaryId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteDictionaryById(int dictionaryId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> EditDictionaryById(int dictionaryId, DictionaryModelShort newDictionaryData)
    {
        throw new NotImplementedException();
    }
}