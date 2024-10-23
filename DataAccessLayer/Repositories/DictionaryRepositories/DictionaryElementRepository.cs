using AutoMapper;
using DataAccessLayer.Database;
using DataAccessLayer.Entities.DictionaryEntities;
using DomainLayer.Interfaces.Repositories;
using DomainLayer.Models.DictionaryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.Repositories.DictionaryRepositories;

/// <summary>
/// Репозиторий элементов словарей.
/// </summary>
public class DictionaryElementRepository : IRepository<DictionaryElementModel, DictionaryElementModelBase>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<DictionaryElementRepository> _logger;

    public DictionaryElementRepository(AppDbContext dbContext, IMapper mapper, ILogger<DictionaryElementRepository> logger)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Добавить элемент в словарь.
    /// </summary>
    /// <param name="data">Данные элемента словаря</param>
    /// <param name="toId">Id словаря</param>
    public async Task AddAsync(DictionaryElementModelBase data, int? toId)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(data, nameof(data));
            ArgumentNullException.ThrowIfNull(toId, nameof(toId));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero((int)toId, nameof(toId));

            var mappedData = _mapper.Map<DictionaryElementEntity>(data);
            var dictionary = await _dbContext.Dictionaries.FirstOrDefaultAsync(a => a.Id == toId);
            if (dictionary is null) return; // ошибку
            dictionary.Elements.Add(mappedData);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method}; Параметры: data: {@Data}, toId: {@ToId}", nameof(AddAsync), data, toId);
            throw;
        }
    }
    
    /// <summary>
    /// Добавить выбранные элементы в словарь.
    /// </summary>
    /// <param name="data">Данные элемента словаря</param>
    /// <param name="toId">Id словаря</param>
    public async Task AddRangeAsync(IEnumerable<DictionaryElementModelBase> data, int? toId)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(data, nameof(data));
            ArgumentNullException.ThrowIfNull(toId, nameof(toId));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero((int)toId, nameof(toId));

            var mappedData = data.Select(a => _mapper.Map<DictionaryElementEntity>(a));
            var dictionary = await _dbContext.Dictionaries.FirstOrDefaultAsync(a => a.Id == toId);
            if (dictionary is null) return; // ошибку
            dictionary.Elements.AddRange(mappedData);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method}; Параметры: data: {@Data}, toId: {@ToId}", nameof(AddRangeAsync), data, toId);
            throw;
        }
    }

    /// <summary>
    /// Получить все элементы словарей.
    /// </summary>
    /// <returns>Элементы словарей</returns>
    public async Task<IEnumerable<DictionaryElementModel>> GetAllAsync()
    {
        try
        {
            var elements = await _dbContext.DictionaryElements.ToListAsync();
            return elements.Select(a => _mapper.Map<DictionaryElementModel>(a));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method}; Параметры: нет", nameof(GetAllAsync));
            throw;
        }
    }
    
    /// <summary>
    /// Получить выбранные элементы словарей.
    /// </summary>
    /// <param name="ids">Массив ID элементов словаря</param>
    public async Task<IEnumerable<DictionaryElementModel>> GetAnyAsync(IEnumerable<int> ids)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(ids, nameof(ids));
        
            var elements = await _dbContext.DictionaryElements
                .Where(a => ids.Contains(a.Id))
                .ToListAsync();

            return elements.Select(a => _mapper.Map<DictionaryElementModel>(a));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method}; Параметры: ids: {@Ids}", nameof(GetAnyAsync), ids);
            throw;
        }
    }

    /// <summary>
    /// Получить выбранный элемент словаря.
    /// </summary>
    /// <param name="id">Id элемента словаря</param>
    /// <returns>Элемент словаря</returns>
    public async Task<DictionaryElementModel?> GetByIdAsync(int id)
    {
        try
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));

            var element = await _dbContext.DictionaryElements.FirstOrDefaultAsync(a => a.Id == id);
            if (element is null || element.Id <= 0) return null;

            return _mapper.Map<DictionaryElementModel>(element);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method}; Параметры: id: {@Id}", nameof(GetByIdAsync), id);
            throw;
        }
    }

    /// <summary>
    /// Обновить данные элемента словаря.
    /// </summary>
    /// <param name="id">Id элемента словаря</param>
    /// <param name="newData">Новые данные элемента словаря</param>
    public async Task UpdateAsync(int id, DictionaryElementModelBase newData)
    {
        try
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
            ArgumentNullException.ThrowIfNull(newData, nameof(newData));

            var element = await _dbContext.DictionaryElements.FirstOrDefaultAsync(a => a.Id == id);
            if (element is null || element.Id <= 0) return;

            element.LastUpdate = DateTime.Now;
            element.Title = newData.Title;
            element.Value = newData.Value;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method}; Параметры: id: {@Id}, newData: {@NewData}", nameof(UpdateAsync), id, newData);
            throw;
        }
    }

    /// <summary>
    /// Удалить все элементы всех словарей.
    /// </summary>
    public async Task DeleteAllAsync()
    {
        try
        {
            var elements = await _dbContext.DictionaryElements.ToListAsync();
            if (elements.Count <= 0) return; // добавить ошибку
            _dbContext.DictionaryElements.RemoveRange(elements);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method}; Параметры: нет", nameof(DeleteAllAsync));
            throw;
        }
    }
    
    /// <summary>
    /// Удалить выбранные элементы словарей.
    /// </summary>
    /// <param name="ids">Массив ID элементов словаря</param>
    public async Task DeleteAnyAsync(IEnumerable<int> ids)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(ids, nameof(ids));
        
            var elementsForDeleting = await _dbContext.DictionaryElements
                .Where(a => ids.Contains(a.Id))
                .ToListAsync();
            if (elementsForDeleting.Count <= 0) return; // добавить ошибку
            _dbContext.DictionaryElements.RemoveRange(elementsForDeleting);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method}; Параметры: ids: {@Ids}", nameof(DeleteAnyAsync), ids);
            throw;
        }
    }

    /// <summary>
    /// Удалить выбранный элемент словаря.
    /// </summary>
    /// <param name="id">Id элемента словаря</param>
    public async Task DeleteByIdAsync(int id)
    {
        try
        {
            ArgumentOutOfRangeException.ThrowIfZero(id, nameof(id));

            var element = await _dbContext.DictionaryElements.FirstOrDefaultAsync(a => a.Id == id);
            if (element is null || element.Id <= 0) return;

            _dbContext.DictionaryElements.Remove(element);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method}; Параметры: id: {@Id}", nameof(DeleteByIdAsync), id);
            throw;
        }
    }
}