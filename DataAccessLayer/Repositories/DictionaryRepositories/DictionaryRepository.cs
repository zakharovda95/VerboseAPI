using DataAccessLayer.Database;
using DataAccessLayer.Enums;
using DataAccessLayer.Mappers;
using DomainLayer.Interfaces.Repositories.Dictionary;
using DomainLayer.Models.DictionaryModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.DictionaryRepositories;

/// <summary>
/// Репозиторий Словарей
/// </summary>
public class DictionaryRepository : IDictionaryRepository
{
    private readonly AppDbContext _dbContext;
    private readonly DictionaryMapper _dictionaryMapper;

    public DictionaryRepository(AppDbContext dbContext, DictionaryMapper dictionaryMapper)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
        ArgumentNullException.ThrowIfNull(dictionaryMapper, nameof(dictionaryMapper));
        
        _dbContext = dbContext;
        _dictionaryMapper = dictionaryMapper;
    }

    #region CREATE

    /// <summary>
    /// Добавляет новый словарь
    /// </summary>
    /// <param name="dictionaryData">Данные словаря</param>
    /// <returns>Результат добавления</returns>
    public async Task<bool> AddDictionary(DictionaryModelShort dictionaryData)
    {
        ArgumentNullException.ThrowIfNull(dictionaryData, nameof(dictionaryData));

        var dictionary = _dictionaryMapper.ToEntity(dictionaryData);
        await _dbContext.Dictionaries.AddAsync(dictionary);
        
        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    #endregion
    
    #region READ

    /// <summary>
    /// Возвращает список словарей (без их элементов)
    /// </summary>
    /// <returns>Список словарей без элементов</returns>
    public async Task<List<DictionaryModelBase>> GetDictionaryList()
    {
        var dictionaries = await _dbContext.Dictionaries.ToListAsync();
        return dictionaries.Select(a => _dictionaryMapper.ToDomainModelBase(a)).ToList();
    }

    /// <summary>
    /// Возвращает все словари, включая элементы словарей
    /// </summary>
    /// <returns>Список словарей</returns>
    public async Task<List<DictionaryModel>> GetAllDictionaries()
    {
        var dictionaries = await _dbContext.Dictionaries
            .Include(a => a.Elements)
            .ToListAsync();
        return dictionaries.Select(a => _dictionaryMapper.ToDomainModel(a)).ToList();
    }

    /// <summary>
    /// Возвращает выбранные словари
    /// </summary>
    /// <param name="dictionaryIds">Id словарей</param>
    /// <returns>Список словарей</returns>
    public async Task<List<DictionaryModel>> GetDictionaries(int[] dictionaryIds)
    {
        ArgumentNullException.ThrowIfNull(dictionaryIds, nameof(dictionaryIds));
        ArgumentOutOfRangeException.ThrowIfZero(dictionaryIds.Length, nameof(dictionaryIds));

        var dictionaries = await _dbContext.Dictionaries
            .Include(a => a.Elements)
            .Where(b => dictionaryIds.Contains(b.Id))
            .ToListAsync();
        return dictionaries.Select(entity => _dictionaryMapper.ToDomainModel(entity)).ToList();
    }

    /// <summary>
    /// Возвращает словарь по Id
    /// </summary>
    /// <param name="dictionaryId">Id словаря</param>
    /// <returns>Словарь</returns>
    public async Task<DictionaryModelFull?> GetDictionaryById(int dictionaryId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(dictionaryId, nameof(dictionaryId));

        var dictionary = await _dbContext.Dictionaries
            .Include(a => a.Elements)
            .FirstOrDefaultAsync(b => b.Id == dictionaryId);
        return dictionary is null || dictionary.Id <= 0 ? null : _dictionaryMapper.ToDomainModelFull(dictionary);
    }

    #endregion
    
    #region UPDATE

    /// <summary>
    /// Обновляет данные словаря
    /// </summary>
    /// <param name="dictionaryId">Id словаря, который нужно обновить</param>
    /// <param name="newDictionaryData">Новые данные словаря</param>
    /// <returns>Результат редактирования</returns>
    public async Task<bool> EditDictionaryById(int dictionaryId, DictionaryModelShort newDictionaryData)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(dictionaryId, nameof(dictionaryId));
        ArgumentNullException.ThrowIfNull(newDictionaryData, nameof(newDictionaryData));

        var dictionary = await _dbContext.Dictionaries.FirstOrDefaultAsync(b => b.Id == dictionaryId);
        if (dictionary is null) return false;

        var newDictionaryDataEntity = _dictionaryMapper.ToEntity(newDictionaryData);

        dictionary.LastUpdate = DateTime.Now;
        dictionary.Title = newDictionaryDataEntity.Title;
        dictionary.Description = newDictionaryDataEntity.Description;
        dictionary.Elements = newDictionaryDataEntity.Elements;

        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    #endregion

    #region DELETE

    /// <summary>
    /// Очищает все словари (только элементы, заголовок описания остаются)
    /// </summary>
    /// <returns>Результат очистки словаря</returns>
     public async Task<bool> CleanAllDictionaries()
    {
        return await _dbContext.ClearTablesAsync(new List<TableNameEnum> { TableNameEnum.DictionaryElement });
    }

    /// <summary>
    /// Очищает выбранные словари (только элементы)
    /// </summary>
    /// <param name="dictionaryIds">Id словарей, которые нужно очистить</param>
    /// <returns>Результат очистки</returns>
    public async Task<bool> CleanDictionaries(int[] dictionaryIds)
    {
        ArgumentNullException.ThrowIfNull(dictionaryIds, nameof(dictionaryIds));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(dictionaryIds.Length, nameof(dictionaryIds));

        var dictionaries = await _dbContext.Dictionaries
            .Include(a => a.Elements)
            .Where(a => dictionaryIds.Contains(a.Id))
            .ToListAsync();
        if (dictionaries is null || dictionaries.Count <= 0) return false;

        return await _dbContext.ExecuteTransactionAsync(async () =>
        {
            foreach (var dictionary in dictionaries)
                dictionary.Elements.Clear();

            await _dbContext.SaveChangesAsync();
        });
    }

    /// <summary>
    /// очистка выбранного словаря (только элементы)
    /// </summary>
    /// <param name="dictionaryId">Id словаря, который нужно очистить</param>
    /// <returns>Результат очистки</returns>
    public async Task<bool> CleanDictionaryById(int dictionaryId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(dictionaryId, nameof(dictionaryId));

        var dictionary = await _dbContext.Dictionaries
            .Include(a => a.Elements)
            .FirstOrDefaultAsync(a => a.Id == dictionaryId);
        if (dictionary is null || dictionary.Id <= 0) return false;
        
        dictionary.Elements.Clear();
        
        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    /// <summary>
    /// Удаляем все словари и их записи
    /// </summary>
    /// <returns>Результат удаления</returns>
    public async Task<bool> DeleteAllDictionaries()
    {
        var tables = new List<TableNameEnum> { TableNameEnum.DictionaryElement, TableNameEnum.Dictionary };
        return await _dbContext.ClearTablesAsync(tables);
    }

    /// <summary>
    /// Удаляет выбранные словари
    /// </summary>
    /// <param name="dictionaryIds">Id словарей, которые нужно удалить</param>
    /// <returns>Результат удаления</returns>
    public async Task<bool> DeleteDictionaries(int[] dictionaryIds)
    {
        ArgumentNullException.ThrowIfNull(dictionaryIds, nameof(dictionaryIds));

        var dictionaries = await _dbContext.Dictionaries
            .Where(b => dictionaryIds.Contains(b.Id)).ToListAsync();
        if (dictionaries is null || dictionaries.Count <= 0) return false;
        
        _dbContext.Dictionaries.RemoveRange(dictionaries);
        
        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    /// <summary>
    /// Удаляет выбранный словарь по Id
    /// </summary>
    /// <param name="dictionaryId">Id словаря, который нужно удалить</param>
    /// <returns>Результат удаления</returns>
    public async Task<bool> DeleteDictionaryById(int dictionaryId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(dictionaryId, nameof(dictionaryId));

        var dictionary = await _dbContext.Dictionaries.FirstOrDefaultAsync(b => b.Id == dictionaryId);
        if (dictionary is null || dictionary.Id <= 0) return false;
        
        _dbContext.Dictionaries.Remove(dictionary);
        
        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    #endregion
}