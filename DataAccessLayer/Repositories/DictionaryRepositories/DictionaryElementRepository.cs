using AutoMapper;
using DataAccessLayer.Database;
using DataAccessLayer.Entities.DictionaryEntities;
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
    public async Task CreateAsync(DictionaryElementModelBase data, int? toId)
    {
        ArgumentNullException.ThrowIfNull(data, nameof(data));
        ArgumentNullException.ThrowIfNull(toId, nameof(toId));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero((int)toId, nameof(toId));

        var mappedData = _mapper.Map<DictionaryElementEntity>(data);
        await _dbContext.DictionaryElements.AddAsync(mappedData);
    }

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
    
    /// <summary>
    /// Возвращает выбранные элементы словарей
    /// </summary>
    /// <param name="ids">масив ID элементов словаря</param>
    public async Task<IEnumerable<DictionaryElementModel>> GetAnyAsync(int[] ids)
    {
        ArgumentNullException.ThrowIfNull(ids, nameof(ids));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(ids.Length, nameof(ids));
        
        var elements = await _dbContext.DictionaryElements
            .Where(a => ids.Contains(a.Id))
            .ToListAsync();
        
        return elements
            .Select(a => _mapper.Map<DictionaryElementModel>(a))
            .ToList();
    }

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

    /// <summary>
    /// Обновляет данные элемента словаря
    /// </summary>
    /// <param name="id">Id элемента словаря</param>
    /// <param name="newData">Новые данные элемента словаря</param>
    public async Task UpdateAsync(int id, DictionaryElementModelBase newData)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
        ArgumentNullException.ThrowIfNull(newData, nameof(newData));

        var element = await _dbContext.DictionaryElements.FirstOrDefaultAsync(a => a.Id == id);
        if (element is null || element.Id <= 0) return;

        element.LastUpdate = DateTime.Now;
        element.Title = newData.Title;
        element.Value = newData.Value;
    }

    /// <summary>
    /// Удаляет все элементы словарей
    /// </summary>
    public async Task DeleteAllAsync()
    {
        var elements = await _dbContext.DictionaryElements.ToListAsync();
        if (elements.Count <= 0) return; // добавить ошибку
        _dbContext.DictionaryElements.RemoveRange(elements);
    }
    
    /// <summary>
    /// Удаляет выбранные элементы словарей
    /// </summary>
    /// <param name="ids">масив ID элементов словаря</param>
    public async Task DeleteAnyAsync(int[] ids)
    {
        ArgumentNullException.ThrowIfNull(ids, nameof(ids));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(ids.Length, nameof(ids));
        
        var elementsForDeleting = await _dbContext.DictionaryElements
            .Where(a => ids.Contains(a.Id))
            .ToListAsync();
        if (elementsForDeleting.Count <= 0) return; // добавить ошибку
        _dbContext.DictionaryElements.RemoveRange(elementsForDeleting);
    }

    /// <summary>
    /// Удаляет выбранный элемент словаря
    /// </summary>
    /// <param name="id">Id элемента словаря</param>
    public async Task DeleteByIdAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfZero(id, nameof(id));

        var element = await _dbContext.DictionaryElements.FirstOrDefaultAsync(a => a.Id == id);
        if (element is null || element.Id <= 0) return;

        _dbContext.DictionaryElements.Remove(element);
    }
}