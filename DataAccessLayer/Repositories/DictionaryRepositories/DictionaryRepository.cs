using DataAccessLayer.Database;
using DataAccessLayer.Enums;
using DataAccessLayer.Mappers;
using DomainLayer.Interfaces.Repositories.Dictionary;
using DomainLayer.Models.DictionaryModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.DictionaryRepositories;

public class DictionaryRepository : IDictionaryRepository
{
    private readonly AppDbContext _dbContext;
    private readonly DictionaryMapper _dictionaryMapper;

    public DictionaryRepository(AppDbContext dbContext, DictionaryMapper dictionaryMapper)
    {
        _dbContext =
            dbContext ?? throw new NullReferenceException(nameof(AppDbContext));
        _dictionaryMapper =
            dictionaryMapper ?? throw new NullReferenceException(nameof(DictionaryMapper));
    }

    #region CREATE

    public async Task<bool> AddDictionary(DictionaryModelShort dictionaryData)
    {
        if (dictionaryData is null)
            throw new NullReferenceException(nameof(DictionaryModelShort));

        var dictionaryEntity = _dictionaryMapper.ToEntity(dictionaryData);
        await _dbContext.Dictionaries.AddAsync(dictionaryEntity);
        int res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    #endregion
    
    #region READ

    public async Task<IEnumerable<DictionaryModelBase>> GetDictionaryList()
    {
        var models = new List<DictionaryModelBase>();
        var dictionaryEntities = await _dbContext.Dictionaries
            .Include(a => a.Elements)
            .ToListAsync();

        if (dictionaryEntities is null || dictionaryEntities.Count <= 0) return models;

        foreach (var entity in dictionaryEntities)
            models.Add(_dictionaryMapper.ToDomainModelBase(entity));

        return models;
    }

    public async Task<IEnumerable<DictionaryModel>> GetAllDictionaries()
    {
        var dictionaryEntities = await _dbContext.Dictionaries
            .Include(a => a.Elements)
            .ToListAsync();

        return dictionaryEntities.Select(a => _dictionaryMapper.ToDomainModel(a));
    }

    public async Task<IEnumerable<DictionaryModel>> GetDictionaries(int[] dictionaryIds)
    {
        if (dictionaryIds is null || dictionaryIds.Length <= 0)
            throw new ArgumentOutOfRangeException(nameof(dictionaryIds));

        var dictionaryEntities = await _dbContext.Dictionaries
            .Include(a => a.Elements)
            .Where(b => dictionaryIds.Contains(b.Id))
            .ToListAsync();

        return dictionaryEntities.Select(entity => _dictionaryMapper.ToDomainModel(entity));
    }

    public async Task<DictionaryModelFull?> GetDictionaryById(int dictionaryId)
    {
        if (dictionaryId <= 0) throw new ArgumentOutOfRangeException(nameof(dictionaryId));

        var dictionaryEntity = await _dbContext.Dictionaries
            .Include(a => a.Elements)
            .FirstOrDefaultAsync(b => b.Id == dictionaryId);
        return dictionaryEntity is null ? null : _dictionaryMapper.ToDomainModelFull(dictionaryEntity);
    }

    #endregion
    
    #region UPDATE

    public async Task<bool> EditDictionaryById(int dictionaryId, DictionaryModelShort newDictionaryData)
    {
        if (dictionaryId <= 0) throw new ArgumentOutOfRangeException(nameof(dictionaryId));
        if (newDictionaryData is null)
            throw new NullReferenceException(nameof(DictionaryModelShort));

        var newDictionaryDataEntity = _dictionaryMapper.ToEntity(newDictionaryData);
        if (newDictionaryData is null)
            throw new NullReferenceException(nameof(newDictionaryDataEntity));

        var dictionaryEntity = await _dbContext.Dictionaries.FirstOrDefaultAsync(b => b.Id == dictionaryId);
        if (dictionaryEntity is null) throw new NullReferenceException(nameof(dictionaryEntity));


        dictionaryEntity.Elements = newDictionaryDataEntity.Elements;
        dictionaryEntity.Description = newDictionaryDataEntity.Description;
        dictionaryEntity.Title = newDictionaryDataEntity.Title;
        dictionaryEntity.LastUpdate = DateTime.Now;

        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    #endregion

    #region DELETE

     public async Task<bool> CleanAllDictionaries()
    {
        return await _dbContext.ClearTablesAsync(new List<TableNameEnum> { TableNameEnum.DictionaryElement });
    }

    public async Task<bool> CleanDictionaries(int[] dictionaryIds)
    {
        if (dictionaryIds is null) throw new NullReferenceException(nameof(dictionaryIds));
        if (dictionaryIds.Length <= 0) throw new ArgumentOutOfRangeException(nameof(dictionaryIds));

        var dictionaries = await _dbContext.DictionaryElements
            .Where(b => dictionaryIds.Contains(b.DictionaryId)).ToListAsync();
        if (dictionaries.Count <= 0) return false;
        _dbContext.DictionaryElements.RemoveRange(dictionaries);
        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> CleanDictionaryById(int dictionaryId)
    {
        if (dictionaryId <= 0) throw new ArgumentOutOfRangeException(nameof(dictionaryId));

        var dictionaryEntity = await _dbContext.DictionaryElements
            .FirstOrDefaultAsync(b => b.DictionaryId == dictionaryId);
        if (dictionaryEntity is null) throw new NullReferenceException(nameof(dictionaryEntity));
        _dbContext.DictionaryElements.Remove(dictionaryEntity);
        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> DeleteAllDictionaries()
    {
        var tables = new List<TableNameEnum> { TableNameEnum.DictionaryElement, TableNameEnum.Dictionary };
        return await _dbContext.ClearTablesAsync(tables);
    }

    public async Task<bool> DeleteDictionaries(int[] dictionaryIds)
    {
        if (dictionaryIds is null) throw new NullReferenceException(nameof(dictionaryIds));
        if (dictionaryIds.Length <= 0) throw new ArgumentOutOfRangeException(nameof(dictionaryIds));

        var dictionaries = await _dbContext.Dictionaries
            .Where(b => dictionaryIds.Contains(b.Id)).ToListAsync();
        if (dictionaries.Count <= 0) return false;
        _dbContext.Dictionaries.RemoveRange(dictionaries);
        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> DeleteDictionaryById(int dictionaryId)
    {
        if (dictionaryId <= 0) throw new ArgumentOutOfRangeException(nameof(dictionaryId));

        var dictionaryEntity = await _dbContext.Dictionaries.FirstOrDefaultAsync(b => b.Id == dictionaryId);
        if (dictionaryEntity is null) throw new NullReferenceException(nameof(dictionaryEntity));
        _dbContext.Dictionaries.Remove(dictionaryEntity);
        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    #endregion
}