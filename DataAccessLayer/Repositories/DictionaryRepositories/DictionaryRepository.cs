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

    /// <summary>
    /// Добавляет новый словарь
    /// </summary>
    /// <param name="data">Данные словаря</param>
    /// <param name="toId">Внешний ключ (если есть)</param>
    /// <returns>Результат добавления</returns>
    public async Task<bool> CreateAsync(DictionaryModelShort data, int? toId = null)
    {
        ArgumentNullException.ThrowIfNull(data, nameof(data));

        var dictionary = _dictionaryMapper.ToEntity(data);
        await _dbContext.Dictionaries.AddAsync(dictionary);

        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    /// <summary>
    /// Возвращает список словарей (без их элементов)
    /// </summary>
    /// <returns>Список словарей без элементов</returns>
    public async Task<IEnumerable<DictionaryModelBase>> GetListAsync()
    {
        var dictionaries = await _dbContext.Dictionaries.ToListAsync();
        return dictionaries.Select(a => _dictionaryMapper.ToDomainModelBase(a)).ToList();
    }

    /// <summary>
    /// Возвращает все словари, включая элементы словарей
    /// </summary>
    /// <returns>Список словарей</returns>
    public async Task<IEnumerable<DictionaryModel>> GetAllAsync()
    {
        var dictionaries = await _dbContext.Dictionaries
            .Include(a => a.Elements)
            .ToListAsync();
        return dictionaries.Select(a => _dictionaryMapper.ToDomainModel(a)).ToList();
    }

    /// <summary>
    /// Возвращает выбранные словари
    /// </summary>
    /// <param name="ids">Id словарей</param>
    /// <returns>Список словарей</returns>
    public async Task<IEnumerable<DictionaryModel>> GetAnyAsync(int[] ids)
    {
        ArgumentNullException.ThrowIfNull(ids, nameof(ids));
        ArgumentOutOfRangeException.ThrowIfZero(ids.Length, nameof(ids));

        var dictionaries = await _dbContext.Dictionaries
            .Include(a => a.Elements)
            .Where(b => ids.Contains(b.Id))
            .ToListAsync();
        return dictionaries.Select(entity => _dictionaryMapper.ToDomainModel(entity)).ToList();
    }

    /// <summary>
    /// Возвращает словарь по Id
    /// </summary>
    /// <param name="id">Id словаря</param>
    /// <returns>Словарь</returns>
    public async Task<DictionaryModel?> GetByIdAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));

        var dictionary = await _dbContext.Dictionaries
            .Include(a => a.Elements)
            .FirstOrDefaultAsync(b => b.Id == id);
        return dictionary is null || dictionary.Id <= 0 ? null : _dictionaryMapper.ToDomainModel(dictionary);
    }

    /// <summary>
    /// Обновляет данные словаря
    /// </summary>
    /// <param name="id">Id словаря, который нужно обновить</param>
    /// <param name="newData">Новые данные словаря</param>
    /// <returns>Результат редактирования</returns>
    public async Task<bool> UpdateAsync(int id, DictionaryModelShort newData)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
        ArgumentNullException.ThrowIfNull(newData, nameof(newData));

        var dictionary = await _dbContext.Dictionaries.FirstOrDefaultAsync(b => b.Id == id);
        if (dictionary is null) return false;

        var newDictionaryDataEntity = _dictionaryMapper.ToEntity(newData);

        dictionary.LastUpdate = DateTime.Now;
        dictionary.Title = newDictionaryDataEntity.Title;
        dictionary.Description = newDictionaryDataEntity.Description;
        dictionary.Elements = newDictionaryDataEntity.Elements;

        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    #region DELETE

    /// <summary>
    /// Очищает все словари (только элементы, заголовок описания остаются)
    /// </summary>
    /// <returns>Результат очистки словаря</returns>
    public async Task<bool> CleanAllAsync()
    {
        return await _dbContext.ClearTablesAsync(new List<TableNameEnum> { TableNameEnum.DictionaryElement });
    }

    /// <summary>
    /// Очищает выбранные словари (только элементы)
    /// </summary>
    /// <param name="ids">Id словарей, которые нужно очистить</param>
    /// <returns>Результат очистки</returns>
    public async Task<bool> CleanAnyAsync(int[] ids)
    {
        ArgumentNullException.ThrowIfNull(ids, nameof(ids));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(ids.Length, nameof(ids));

        var dictionaries = await _dbContext.Dictionaries
            .Include(a => a.Elements)
            .Where(a => ids.Contains(a.Id))
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
    /// Очистка выбранного словаря (только элементы)
    /// </summary>
    /// <param name="id">Id словаря, который нужно очистить</param>
    /// <returns>Результат очистки</returns>
    public async Task<bool> CleanByIdAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));

        var dictionary = await _dbContext.Dictionaries
            .Include(a => a.Elements)
            .FirstOrDefaultAsync(a => a.Id == id);
        if (dictionary is null || dictionary.Id <= 0) return false;

        dictionary.Elements.Clear();

        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    /// <summary>
    /// Удаляем все словари и их записи
    /// </summary>
    /// <returns>Результат удаления</returns>
    public async Task<bool> DeleteAllAsync()
    {
        var tables = new List<TableNameEnum> { TableNameEnum.DictionaryElement, TableNameEnum.Dictionary };
        return await _dbContext.ClearTablesAsync(tables);
    }

    /// <summary>
    /// Удаляет выбранные словари
    /// </summary>
    /// <param name="ids">Id словарей, которые нужно удалить</param>
    /// <returns>Результат удаления</returns>
    public async Task<bool> DeleteAnyAsync(int[] ids)
    {
        ArgumentNullException.ThrowIfNull(ids, nameof(ids));

        var dictionaries = await _dbContext.Dictionaries
            .Where(b => ids.Contains(b.Id)).ToListAsync();
        if (dictionaries is null || dictionaries.Count <= 0) return false;

        _dbContext.Dictionaries.RemoveRange(dictionaries);

        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    /// <summary>
    /// Удаляет выбранный словарь по Id
    /// </summary>
    /// <param name="id">Id словаря, который нужно удалить</param>
    /// <returns>Результат удаления</returns>
    public async Task<bool> DeleteByIdAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));

        var dictionary = await _dbContext.Dictionaries.FirstOrDefaultAsync(b => b.Id == id);
        if (dictionary is null || dictionary.Id <= 0) return false;

        _dbContext.Dictionaries.Remove(dictionary);

        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    #endregion
}