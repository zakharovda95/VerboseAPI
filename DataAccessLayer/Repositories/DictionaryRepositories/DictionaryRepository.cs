using AutoMapper;
using DataAccessLayer.Database;
using DataAccessLayer.Entities.DictionaryEntities;
using DomainLayer.Interfaces.Repositories;
using DomainLayer.Models.DictionaryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.Repositories.DictionaryRepositories;

/// <summary>
/// Репозиторий cловарей.
/// </summary>
public class DictionaryRepository : IRepository<DictionaryModel, DictionaryModelBase>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<DictionaryRepository> _logger;

    public DictionaryRepository(AppDbContext dbContext, IMapper mapper, ILogger<DictionaryRepository> logger)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Добавить новый словарь.
    /// </summary>
    /// <param name="data">Данные словаря</param>
    /// <param name="toId">Внешний ключ (не требуется, родительский элемент)</param>
    public async Task AddAsync(DictionaryModelBase data, int? toId = null)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(data, nameof(data));

            var dictionary = _mapper.Map<DictionaryEntity>(data);
            await _dbContext.Dictionaries.AddAsync(dictionary);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method}; Параметры: data: {@Data}, toId: {@ToId}", nameof(AddAsync), data, toId);
            throw;
        }
    }
    
    /// <summary>
    /// Добавить несколько словарей.
    /// </summary>
    /// <param name="data">Массив данных словаря</param>
    /// <param name="toId">null (не нужен тк верхний уровень)</param>
    public async Task AddRangeAsync(IEnumerable<DictionaryModelBase> data, int? toId = null)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(data, nameof(data));
            ArgumentNullException.ThrowIfNull(toId, nameof(toId));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero((int)toId, nameof(toId));

            var dictionaries = data.Select(a => _mapper.Map<DictionaryEntity>(a));
            await _dbContext.Dictionaries.AddRangeAsync(dictionaries);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method}; Параметры: data: {@Data}, toId: {@ToId}", nameof(AddAsync), data, toId);
            throw;
        }
    }

    /// <summary>
    /// Получить все словари, включая элементы словарей.
    /// </summary>
    /// <returns>Список словарей</returns>
    public async Task<IEnumerable<DictionaryModel>> GetAllAsync()
    {
        try
        {
            var dictionaries = await _dbContext.Dictionaries
                .Include(a => a.Elements)
                .ToListAsync();
            return dictionaries.Select(a => _mapper.Map<DictionaryModel>(a));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method} Параметры: нет", nameof(AddAsync));
            throw;
        }
    }
    
    /// <summary>
    /// Получить выбранные словари, включая элементы словарей.
    /// </summary>
    /// <param name="ids">Массив ID словарей</param>
    /// <returns>Список словарей</returns>
    public async Task<IEnumerable<DictionaryModel>> GetAnyAsync(IEnumerable<int> ids)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(ids, nameof(ids));
        
            var dictionaries = await _dbContext.Dictionaries
                .Include(a => a.Elements)
                .Where(a => ids.Contains(a.Id))
                .ToListAsync();
            return dictionaries.Select(a => _mapper.Map<DictionaryModel>(a));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method} Параметры: ids: {@Ids}", nameof(AddAsync), ids);
            throw;
        }
    }

    /// <summary>
    /// Получить словарь по Id.
    /// </summary>
    /// <param name="id">Id словаря</param>
    /// <returns>Словарь</returns>
    public async Task<DictionaryModel?> GetByIdAsync(int id)
    {
        try
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));

            var dictionary = await _dbContext.Dictionaries
                .Include(a => a.Elements)
                .FirstOrDefaultAsync(b => b.Id == id);
            return dictionary is null || dictionary.Id <= 0 ? null : _mapper.Map<DictionaryModel>(dictionary);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method} Параметры: id: {@Id}", nameof(AddAsync), id);
            throw;
        }
    }

    /// <summary>
    /// Обновить данные словаря.
    /// </summary>
    /// <param name="id">Id словаря, который нужно обновить</param>
    /// <param name="newData">Новые данные словаря</param>
    public async Task UpdateAsync(int id, DictionaryModelBase newData)
    {
        try
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
            ArgumentNullException.ThrowIfNull(newData, nameof(newData));

            var dictionary = await _dbContext.Dictionaries.FirstOrDefaultAsync(b => b.Id == id);
            if (dictionary is null) return; // ошибка

            var newDictionaryDataEntity = _mapper.Map<DictionaryEntity>(newData);

            dictionary.LastUpdate = DateTime.Now;
            dictionary.Title = newDictionaryDataEntity.Title;
            dictionary.Description = newDictionaryDataEntity.Description;
            dictionary.Elements = newDictionaryDataEntity.Elements;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method} Параметры: id: {@Id}, newData: {@NewDate}", nameof(AddAsync), id, newData);
            throw;
        }
    }

    /// <summary>
    /// Удалить все словари и их элементы.
    /// </summary>
    public async Task DeleteAllAsync()
    {
        try
        {
            var dictionaries = await _dbContext.Dictionaries
                .Include(a => a.Elements)
                .ToListAsync();
            if (dictionaries.Count <= 0) return; // добавить ошибку
            _dbContext.Dictionaries.RemoveRange(dictionaries);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method} Параметры: нет", nameof(AddAsync));
            throw;
        }
    }

    /// <summary>
    /// Удалить выбранные словари и их элементы.
    /// </summary>
    /// <param name="ids">Массив ID словарей</param>
    public async Task DeleteAnyAsync(IEnumerable<int> ids)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(ids, nameof(ids));
        
            var dictionaries = await _dbContext.Dictionaries
                .Include(a => a.Elements)
                .Where(a => ids.Contains(a.Id))
                .ToListAsync();
            if (dictionaries.Count <= 0) return; // добавить ошибку
            _dbContext.Dictionaries.RemoveRange(dictionaries);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method} Параметры: ids: {@Ids}", nameof(AddAsync), ids);
            throw;
        }
    }
    
    /// <summary>
    /// Удалить словарь по Id.
    /// </summary>
    /// <param name="id">ID словаря</param>
    public async Task DeleteByIdAsync(int id)
    {
        try
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));

            var dictionary = await _dbContext.Dictionaries.FirstOrDefaultAsync(b => b.Id == id);
            if (dictionary is null || dictionary.Id <= 0) return; // ошибку
            _dbContext.Dictionaries.Remove(dictionary);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Метод: {@Method} Параметры: id: {@Id}", nameof(AddAsync), id);
            throw;
        }
    }
}