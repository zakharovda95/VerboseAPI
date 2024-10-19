using AutoMapper;
using DataAccessLayer.Database;
using DataAccessLayer.Entities.DictionaryEntities;
using DataAccessLayer.Enums;
using DomainLayer.Interfaces.Repositories;
using DomainLayer.Models.DictionaryModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.DictionaryRepositories;

/// <summary>
/// Репозиторий cловарей
/// </summary>
public class DictionaryRepository : IRepository<DictionaryModel, DictionaryModelBase>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public DictionaryRepository(AppDbContext dbContext, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

        _dbContext = dbContext;
        _mapper = mapper;
    }

    /// <summary>
    /// Добавляет новый словарь
    /// </summary>
    /// <param name="data">Данные словаря</param>
    /// <param name="toId">Внешний ключ (если есть)</param>
    public async Task CreateAsync(DictionaryModelBase data, int? toId = null)
    {
        ArgumentNullException.ThrowIfNull(data, nameof(data));

        var dictionary = _mapper.Map<DictionaryEntity>(data);
        await _dbContext.Dictionaries.AddAsync(dictionary);
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
        return dictionaries.Select(a => _mapper.Map<DictionaryModel>(a)).ToList();
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
        return dictionary is null || dictionary.Id <= 0 ? null : _mapper.Map<DictionaryModel>(dictionary);
    }

    /// <summary>
    /// Обновляет данные словаря
    /// </summary>
    /// <param name="id">Id словаря, который нужно обновить</param>
    /// <param name="newData">Новые данные словаря</param>
    public async Task UpdateAsync(int id, DictionaryModelBase newData)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
        ArgumentNullException.ThrowIfNull(newData, nameof(newData));

        var dictionary = await _dbContext.Dictionaries.FirstOrDefaultAsync(b => b.Id == id);
        if (dictionary is null) return;

        var newDictionaryDataEntity = _mapper.Map<DictionaryEntity>(newData);

        dictionary.LastUpdate = DateTime.Now;
        dictionary.Title = newDictionaryDataEntity.Title;
        dictionary.Description = newDictionaryDataEntity.Description;
        dictionary.Elements = newDictionaryDataEntity.Elements;
    }

    /// <summary>
    /// Удаляем все словари и их записи
    /// </summary>
    public async Task DeleteAllAsync()
    {
        var tables = new List<TableNameEnum> { TableNameEnum.DictionaryElement, TableNameEnum.Dictionary };
        await _dbContext.ClearTablesAsync(tables);
    }
    
    /// <summary>
    /// Удаляет элемент словаря
    /// </summary>
    /// <param name="id">ID словаря</param>
    public async Task DeleteByIdAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));

        var dictionary = await _dbContext.Dictionaries.FirstOrDefaultAsync(b => b.Id == id);
        if (dictionary is null || dictionary.Id <= 0) return;

        _dbContext.Dictionaries.Remove(dictionary);
    }
}