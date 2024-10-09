using DataAccessLayer.Database;
using DataAccessLayer.Enums;
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
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
        ArgumentNullException.ThrowIfNull(dictionaryElementMapper, nameof(dictionaryElementMapper));
        
        _dbContext = dbContext;
        _dictionaryElementMapper = dictionaryElementMapper;
    }

    #region CREATE

    /// <summary>
    /// Добавляет элемент в словарь
    /// </summary>
    /// <param name="dictionaryId">Id словаря</param>
    /// <param name="elementData">Данные элемента словаря</param>
    /// <returns>Результат добавления</returns>
    public async Task<bool> AddElement(int dictionaryId, DictionaryElementModelShort elementData)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(dictionaryId, nameof(dictionaryId));
        ArgumentNullException.ThrowIfNull(elementData, nameof(elementData));

        var mappedData = _dictionaryElementMapper.ToEntity(elementData);

        await _dbContext.DictionaryElements.AddAsync(mappedData);
        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    /// <summary>
    /// Копирует все элементы одного словаря в другой
    /// </summary>
    /// <param name="dictionaryId">Id словаря, элементы которого нужно скопировать</param>
    /// <param name="toDictionaryId">Id словаря, куда нужно скопировать элементы</param>
    /// <returns>Результат копирования</returns>
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
        
        toDictionary.Elements.AddRange(fromDictionary.Elements);
        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    /// <summary>
    /// Копирует некоторые элементы в словарь
    /// </summary>
    /// <param name="elementIds">Id элементов, которые нужно скопировать</param>
    /// <param name="toDictionaryId">Id словаря, куда нужно скопировать элементы</param>
    /// <returns>Результат копирования</returns>
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

        toDictionary.Elements.AddRange(elements);
        
        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    /// <summary>
    /// Копирует указанный элемент в словарь
    /// </summary>
    /// <param name="elementId">Id элемента, который нужно скопировать</param>
    /// <param name="toDictionaryId">Id словаря, куда нужно скопировать элемент</param>
    /// <returns>Результат копирования</returns>
    public async Task<bool> CopyElementById(int elementId, int toDictionaryId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(elementId, nameof(elementId));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toDictionaryId, nameof(toDictionaryId));

        var toDictionary = await _dbContext.Dictionaries.FirstOrDefaultAsync(b => b.Id == toDictionaryId);
        if (toDictionary is null) return false;

        var element = await _dbContext.DictionaryElements.FirstOrDefaultAsync(a => a.Id == elementId);
        if (element is null || element.Id <= 0) return false;
        
        toDictionary.Elements.Add(element);

        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    #endregion

    #region READ

    /// <summary>
    /// Возвращает все элементы словарей
    /// </summary>
    /// <returns>Элементы словарей</returns>
    public async Task<List<DictionaryElementModel>> GetAllElements()
    {
        var elements = await _dbContext.DictionaryElements.ToListAsync();
        return elements
            .Select(a => _dictionaryElementMapper.ToDomainModel(a))
            .ToList();
    }

    /// <summary>
    /// Возвращает выбранные элементы словарей
    /// </summary>
    /// <param name="elementsIds">Id элементов</param>
    /// <returns>Элементы словарей</returns>
    public async Task<List<DictionaryElementModel>> GetElements(int[] elementsIds)
    {
        ArgumentNullException.ThrowIfNull(elementsIds);
        ArgumentOutOfRangeException.ThrowIfZero(elementsIds.Length, nameof(elementsIds));
        
        var elements = await _dbContext.DictionaryElements
            .Where(a => elementsIds.Contains(a.Id))
            .ToListAsync();
        
        return elements.Select(a => _dictionaryElementMapper.ToDomainModel(a)).ToList();
    }

    /// <summary>
    /// Возвращает выбранный элемент словаря
    /// </summary>
    /// <param name="elementId">Id элемента словаря</param>
    /// <returns>Элемент словаря</returns>
    public async Task<DictionaryElementModel?> GetElementById(int elementId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(elementId, nameof(elementId));
        
        var element = await _dbContext.DictionaryElements.FirstOrDefaultAsync(a => a.Id == elementId);
        if (element is null || element.Id <= 0) return null;
        
        return _dictionaryElementMapper.ToDomainModel(element);
    }

    #endregion

    #region UPDATE

    /// <summary>
    /// Обновляет данные элемента словаря
    /// </summary>
    /// <param name="elementId">Id элемента словаря</param>
    /// <param name="newElementData">Новые данные элемента словаря</param>
    /// <returns>Результат редактирвоания</returns>
    public async Task<bool> EditElement(int elementId, DictionaryElementModelShort newElementData)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(elementId, nameof(elementId));
        ArgumentNullException.ThrowIfNull(newElementData, nameof(newElementData));

        var element = await _dbContext.DictionaryElements.FirstOrDefaultAsync(a => a.Id == elementId);
        if (element is null || element.Id <= 0) return false;

        element.LastUpdate = DateTime.Now;
        element.Title = newElementData.Title;
        element.Value = newElementData.Value;

        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    /// <summary>
    /// Переносит все элементы из одного словаря в другой (с очисткой первого)
    /// </summary>
    /// <param name="dictionaryId">Id словаря, из которого нужно перенести элеметы</param>
    /// <param name="toDictionaryId">Id словаря, куда нужно перенести элементы</param>
    /// <returns>Результат переноса</returns>
    public async Task<bool> MoveAllElements(int dictionaryId, int toDictionaryId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(dictionaryId, nameof(toDictionaryId));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toDictionaryId, nameof(toDictionaryId));

        var dictionaries = await _dbContext.Dictionaries
            .Where(b => b.Id == dictionaryId || b.Id == toDictionaryId)
            .Include(dictionaryEntity => dictionaryEntity.Elements)
            .ToListAsync();
        if (dictionaries is null || dictionaries.Count <= 0) return false;

        var fromDictionary = dictionaries.FirstOrDefault(a => a.Id == dictionaryId);
        var toDictionary = dictionaries.FirstOrDefault(a => a.Id == toDictionaryId);
        if (fromDictionary is null || toDictionary is null) return false;

        return await _dbContext.ExecuteTransactionAsync(async () =>
        {
            toDictionary.Elements.AddRange(fromDictionary.Elements);
            fromDictionary.Elements.Clear();
            await _dbContext.SaveChangesAsync();
        });
    }

    /// <summary>
    /// Переносит выбранные элементы словаря в другой словарь (с удалением из первого)
    /// </summary>
    /// <param name="elementIds">Id элементов, которые нужно перенести</param>
    /// <param name="toDictionaryId">Id словаря, куда нужно перенести элементы</param>
    /// <returns>Результат переноса</returns>
    public async Task<bool> MoveElements(int[] elementIds, int toDictionaryId)
    {
        ArgumentNullException.ThrowIfNull(elementIds, nameof(elementIds));
        ArgumentOutOfRangeException.ThrowIfZero(elementIds.Length, nameof(elementIds));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toDictionaryId, nameof(toDictionaryId));

        var toDictionary = await _dbContext.Dictionaries.FirstOrDefaultAsync(a => a.Id == toDictionaryId);
        if (toDictionary is null || toDictionary.Id <= 0) return false;

        var elements = await _dbContext.DictionaryElements
            .Where(a => elementIds.Contains(a.Id))
            .ToListAsync();
        if (elements is null || elements.Count <= 0) return false;
        
        return await _dbContext.ExecuteTransactionAsync(async () =>
        {
            toDictionary.Elements.AddRange(elements);
            _dbContext.DictionaryElements.RemoveRange(elements);
            await _dbContext.SaveChangesAsync();
        });
    }

    /// <summary>
    /// Переносит выбранный элемент в другой словарь
    /// </summary>
    /// <param name="elementId">Id элемента, который нужно перенести переноса</param>
    /// <param name="toDictionaryId">Id слваря, куда нужно перенести</param>
    /// <returns>Результат переноса</returns>
    public async Task<bool> MoveElementById(int elementId, int toDictionaryId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(elementId, nameof(elementId));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toDictionaryId, nameof(toDictionaryId));

        var toDictionary = await _dbContext.Dictionaries.FirstOrDefaultAsync(a => a.Id == toDictionaryId);
        if (toDictionary is null || toDictionary.Id <= 0) return false;
        
        var element = await _dbContext.DictionaryElements.FirstOrDefaultAsync(a => a.Id == elementId);
        if (element is null || element.Id <= 0) return false;

        return await _dbContext.ExecuteTransactionAsync(async () =>
        {
            toDictionary.Elements.Add(element);
            _dbContext.DictionaryElements.Remove(element);
            await _dbContext.SaveChangesAsync();
        });
    }

    #endregion

    #region DELETE

    /// <summary>
    /// Удаляет все элементы словарей
    /// </summary>
    /// <returns>Результат удаления</returns>
    public async Task<bool> DeleteAllElements()
    {
        return await _dbContext.ClearTablesAsync(new List<TableNameEnum> { TableNameEnum.DictionaryElement });
    }

    /// <summary>
    /// Удаляет выбранные элементы словарей
    /// </summary>
    /// <param name="elementIds">Id элементов словарей</param>
    /// <returns>Результат удаления</returns>
    public async Task<bool> DeleteElements(int[] elementIds)
    {
        ArgumentNullException.ThrowIfNull(elementIds, nameof(elementIds));
        ArgumentOutOfRangeException.ThrowIfZero(elementIds.Length, nameof(elementIds));

        var elements = await _dbContext.DictionaryElements
            .Where(a => elementIds.Contains(a.Id))
            .ToListAsync();
        if (elements is null || elements.Count <= 0) return false;

        _dbContext.DictionaryElements.RemoveRange(elements);
        
        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    /// <summary>
    /// Удаляет выбранный элемент словаря
    /// </summary>
    /// <param name="elementId">Id элемента словаря</param>
    /// <returns>Результат удаления</returns>
    public async Task<bool> DeleteElementById(int elementId)
    {
        ArgumentOutOfRangeException.ThrowIfZero(elementId, nameof(elementId));

        var element = await _dbContext.DictionaryElements.FirstOrDefaultAsync(a => a.Id == elementId);
        if (element is null || element.Id <= 0) return false;

        _dbContext.DictionaryElements.Remove(element);
        
        var res = await _dbContext.SaveChangesAsync();
        return res > 0;
    }

    #endregion
}