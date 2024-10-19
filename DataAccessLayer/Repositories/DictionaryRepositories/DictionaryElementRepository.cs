using AutoMapper;
using DataAccessLayer.Database;
using DataAccessLayer.Entities.DictionaryEntities;
using DataAccessLayer.Enums;
using DomainLayer.Interfaces.Repositories;
using DomainLayer.Models.DictionaryModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.DictionaryRepositories;

/// <summary>
/// Репозиторий элементов словарей
/// </summary>
public class DictionaryElementRepository : IRepository<DictionaryElementModel, DictionaryElementModelBase>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public DictionaryElementRepository(AppDbContext dbContext, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

        _dbContext = dbContext;
        _mapper = mapper;
    }

    /// <summary>
    /// Добавляет элемент в словарь
    /// </summary>
    /// <param name="data">Данные элемента словаря</param>
    /// <param name="toId">Id словаря</param>
    /// <returns>Результат добавления</returns>
    public async Task<bool> CreateAsync(DictionaryElementModelBase data, int? toId)
    {
        ArgumentNullException.ThrowIfNull(data, nameof(data));
        ArgumentNullException.ThrowIfNull(toId, nameof(toId));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero((int)toId, nameof(toId));

        var mappedData = _mapper.Map<DictionaryElementEntity>(data);

        await _dbContext.DictionaryElements.AddAsync(mappedData);
        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    // /// <summary>
    // /// Копирует все элементы одного словаря в другой
    // /// </summary>
    // /// <param name="fromDictionaryId">Id словаря, элементы которого нужно скопировать</param>
    // /// <param name="toDictionaryId">Id словаря, куда нужно скопировать элементы</param>
    // /// <returns>Результат копирования</returns>
    // public async Task<bool> CopyAllAsync(int fromDictionaryId, int toDictionaryId)
    // {
    //     ArgumentOutOfRangeException.ThrowIfNegativeOrZero(fromDictionaryId);
    //     ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toDictionaryId);
    //
    //     var dictionaries = await _dbContext.Dictionaries
    //         .Include(a => a.Elements)
    //         .Where(b => b.Id == fromDictionaryId || b.Id == toDictionaryId)
    //         .ToListAsync();
    //     if (dictionaries is null) return false;
    //
    //     var fromDictionary = dictionaries.FirstOrDefault(a => a.Id == fromDictionaryId);
    //     var toDictionary = dictionaries.FirstOrDefault(a => a.Id == toDictionaryId);
    //     if (fromDictionary is null || toDictionary is null) return false;
    //
    //     toDictionary.Elements.AddRange(fromDictionary.Elements);
    //     var res = await _dbContext.SaveChangesAsync();
    //     return res > 0;
    // }

    // /// <summary>
    // /// Копирует некоторые элементы в словарь
    // /// </summary>
    // /// <param name="ids">Id элементов, которые нужно скопировать</param>
    // /// <param name="toDictionaryId">Id словаря, куда нужно скопировать элементы</param>
    // /// <returns>Результат копирования</returns>
    // public async Task<bool> CopyAnyAsync(int[] ids, int toDictionaryId)
    // {
    //     ArgumentNullException.ThrowIfNull(ids, nameof(ids));
    //     ArgumentOutOfRangeException.ThrowIfZero(ids.Length, nameof(ids));
    //     ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toDictionaryId, nameof(toDictionaryId));
    //
    //     var toDictionary = await _dbContext.Dictionaries
    //         .Include(a => a.Elements)
    //         .FirstOrDefaultAsync(b => b.Id == toDictionaryId);
    //     if (toDictionary is null) return false;
    //
    //     var elements = await _dbContext.DictionaryElements
    //         .Where(a => ids.Contains(a.Id))
    //         .ToListAsync();
    //     if (elements is null || elements.Count <= 0) return false;
    //
    //     toDictionary.Elements.AddRange(elements);
    //
    //     var res = await _dbContext.SaveChangesAsync();
    //     return res > 0;
    // }

    // /// <summary>
    // /// Копирует указанный элемент в словарь
    // /// </summary>
    // /// <param name="id">Id элемента, который нужно скопировать</param>
    // /// <param name="toDictionaryId">Id словаря, куда нужно скопировать элемент</param>
    // /// <returns>Результат копирования</returns>
    // public async Task<bool> CopyByIdAsync(int id, int toDictionaryId)
    // {
    //     ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
    //     ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toDictionaryId, nameof(toDictionaryId));
    //
    //     var toDictionary = await _dbContext.Dictionaries.FirstOrDefaultAsync(b => b.Id == toDictionaryId);
    //     if (toDictionary is null) return false;
    //
    //     var element = await _dbContext.DictionaryElements.FirstOrDefaultAsync(a => a.Id == id);
    //     if (element is null || element.Id <= 0) return false;
    //
    //     toDictionary.Elements.Add(element);
    //
    //     var res = await _dbContext.SaveChangesAsync();
    //     return res > 0;
    // }

    #region READ

    /// <summary>
    /// Возвращает все элементы словарей
    /// </summary>
    /// <returns>Элементы словарей</returns>
    public async Task<IEnumerable<DictionaryElementModel>> GetAllAsync()
    {
        var elements = await _dbContext.DictionaryElements.ToListAsync();
        return elements
            .Select(a => _mapper.Map<DictionaryElementModel>(a))
            .ToList();
    }

    // /// <summary>
    // /// Возвращает выбранные элементы словарей
    // /// </summary>
    // /// <param name="ids">Id элементов</param>
    // /// <returns>Элементы словарей</returns>
    // public async Task<IEnumerable<DictionaryElementModel>> GetAnyAsync(int[] ids)
    // {
    //     ArgumentNullException.ThrowIfNull(ids);
    //     ArgumentOutOfRangeException.ThrowIfZero(ids.Length, nameof(ids));
    //
    //     var elements = await _dbContext.DictionaryElements
    //         .Where(a => ids.Contains(a.Id))
    //         .ToListAsync();
    //
    //     return elements
    //         .Select(a => _mapper.Map<DictionaryElementModel>(a))
    //         .ToList();
    // }

    /// <summary>
    /// Возвращает выбранный элемент словаря
    /// </summary>
    /// <param name="id">Id элемента словаря</param>
    /// <returns>Элемент словаря</returns>
    public async Task<DictionaryElementModel?> GetByIdAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));

        var element = await _dbContext.DictionaryElements.FirstOrDefaultAsync(a => a.Id == id);
        if (element is null || element.Id <= 0) return null;

        return _mapper.Map<DictionaryElementModel>(element);
    }

    #endregion

    /// <summary>
    /// Обновляет данные элемента словаря
    /// </summary>
    /// <param name="id">Id элемента словаря</param>
    /// <param name="newData">Новые данные элемента словаря</param>
    /// <returns>Результат редактирвоания</returns>
    public async Task<bool> UpdateAsync(int id, DictionaryElementModelBase newData)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
        ArgumentNullException.ThrowIfNull(newData, nameof(newData));

        var element = await _dbContext.DictionaryElements.FirstOrDefaultAsync(a => a.Id == id);
        if (element is null || element.Id <= 0) return false;

        element.LastUpdate = DateTime.Now;
        element.Title = newData.Title;
        element.Value = newData.Value;

        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    // /// <summary>
    // /// Переносит все элементы из одного словаря в другой (с очисткой первого)
    // /// </summary>
    // /// <param name="fromDictionaryId">Id словаря, из которого нужно перенести элеметы</param>
    // /// <param name="toDictionaryId">Id словаря, куда нужно перенести элементы</param>
    // /// <returns>Результат переноса</returns>
    // public async Task<bool> MoveAllAsync(int fromDictionaryId, int toDictionaryId)
    // {
    //     ArgumentOutOfRangeException.ThrowIfNegativeOrZero(fromDictionaryId, nameof(toDictionaryId));
    //     ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toDictionaryId, nameof(toDictionaryId));
    //
    //     var dictionaries = await _dbContext.Dictionaries
    //         .Where(b => b.Id == fromDictionaryId || b.Id == toDictionaryId)
    //         .Include(dictionaryEntity => dictionaryEntity.Elements)
    //         .ToListAsync();
    //     if (dictionaries is null || dictionaries.Count <= 0) return false;
    //
    //     var fromDictionary = dictionaries.FirstOrDefault(a => a.Id == fromDictionaryId);
    //     var toDictionary = dictionaries.FirstOrDefault(a => a.Id == toDictionaryId);
    //     if (fromDictionary is null || toDictionary is null) return false;
    //
    //     return await _dbContext.ExecuteTransactionAsync(async () =>
    //     {
    //         toDictionary.Elements.AddRange(fromDictionary.Elements);
    //         fromDictionary.Elements.Clear();
    //         await _dbContext.SaveChangesAsync();
    //     });
    // }

    // /// <summary>
    // /// Переносит выбранные элементы словаря в другой словарь (с удалением из первого)
    // /// </summary>
    // /// <param name="ids">Id элементов, которые нужно перенести</param>
    // /// <param name="toDictionaryId">Id словаря, куда нужно перенести элементы</param>
    // /// <returns>Результат переноса</returns>
    // public async Task<bool> MoveAnyAsync(int[] ids, int toDictionaryId)
    // {
    //     ArgumentNullException.ThrowIfNull(ids, nameof(ids));
    //     ArgumentOutOfRangeException.ThrowIfZero(ids.Length, nameof(ids));
    //     ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toDictionaryId, nameof(toDictionaryId));
    //
    //     var toDictionary = await _dbContext.Dictionaries.FirstOrDefaultAsync(a => a.Id == toDictionaryId);
    //     if (toDictionary is null || toDictionary.Id <= 0) return false;
    //
    //     var elements = await _dbContext.DictionaryElements
    //         .Where(a => ids.Contains(a.Id))
    //         .ToListAsync();
    //     if (elements is null || elements.Count <= 0) return false;
    //
    //     return await _dbContext.ExecuteTransactionAsync(async () =>
    //     {
    //         toDictionary.Elements.AddRange(elements);
    //         _dbContext.DictionaryElements.RemoveRange(elements);
    //         await _dbContext.SaveChangesAsync();
    //     });
    // }

    // /// <summary>
    // /// Переносит выбранный элемент в другой словарь
    // /// </summary>
    // /// <param name="id">Id элемента, который нужно перенести переноса</param>
    // /// <param name="toDictionaryId">Id слваря, куда нужно перенести</param>
    // /// <returns>Результат переноса</returns>
    // public async Task<bool> MoveByIdAsync(int id, int toDictionaryId)
    // {
    //     ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
    //     ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toDictionaryId, nameof(toDictionaryId));
    //
    //     var toDictionary = await _dbContext.Dictionaries.FirstOrDefaultAsync(a => a.Id == toDictionaryId);
    //     if (toDictionary is null || toDictionary.Id <= 0) return false;
    //
    //     var element = await _dbContext.DictionaryElements.FirstOrDefaultAsync(a => a.Id == id);
    //     if (element is null || element.Id <= 0) return false;
    //
    //     return await _dbContext.ExecuteTransactionAsync(async () =>
    //     {
    //         toDictionary.Elements.Add(element);
    //         _dbContext.DictionaryElements.Remove(element);
    //         await _dbContext.SaveChangesAsync();
    //     });
    // }

    /// <summary>
    /// Удаляет все элементы словарей
    /// </summary>
    /// <returns>Результат удаления</returns>
    public async Task<bool> DeleteAllAsync()
    {
        return await _dbContext.ClearTablesAsync(new List<TableNameEnum> { TableNameEnum.DictionaryElement });
    }

    // /// <summary>
    // /// Удаляет выбранные элементы словарей
    // /// </summary>
    // /// <param name="ids">Id элементов словарей</param>
    // /// <returns>Результат удаления</returns>
    // public async Task<bool> DeleteAnyAsync(int[] ids)
    // {
    //     ArgumentNullException.ThrowIfNull(ids, nameof(ids));
    //     ArgumentOutOfRangeException.ThrowIfZero(ids.Length, nameof(ids));
    //
    //     var elements = await _dbContext.DictionaryElements
    //         .Where(a => ids.Contains(a.Id))
    //         .ToListAsync();
    //     if (elements is null || elements.Count <= 0) return false;
    //
    //     _dbContext.DictionaryElements.RemoveRange(elements);
    //
    //     var res = await _dbContext.SaveChangesAsync();
    //     return res > 0;
    // }

    /// <summary>
    /// Удаляет выбранный элемент словаря
    /// </summary>
    /// <param name="id">Id элемента словаря</param>
    /// <returns>Результат удаления</returns>
    public async Task<bool> DeleteByIdAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfZero(id, nameof(id));

        var element = await _dbContext.DictionaryElements.FirstOrDefaultAsync(a => a.Id == id);
        if (element is null || element.Id <= 0) return false;

        _dbContext.DictionaryElements.Remove(element);

        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }
}