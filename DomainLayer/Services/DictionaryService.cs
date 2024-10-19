using DomainLayer.Interfaces.Services;
using DomainLayer.Interfaces.UnitOfWorks;
using DomainLayer.Models.DictionaryModels;

namespace DomainLayer.Services;

/// <summary>
/// Сервис работы со словарями
/// </summary>
public class DictionaryService : IDictionaryService
{
    private readonly IDictionaryUnitOfWork _dictionaryUnitOfWork;

    public DictionaryService(IDictionaryUnitOfWork dictionaryUnitOfWork)
    {
        ArgumentNullException.ThrowIfNull(dictionaryUnitOfWork, nameof(dictionaryUnitOfWork));
        _dictionaryUnitOfWork = dictionaryUnitOfWork;
    }

    /// <summary>
    /// Создает новый словарь
    /// </summary>
    /// <param name="data">Данные словаря (название, описание, опционально входящие элементы [DictionaryElementModelBase])</param>
    /// <param name="toId">ID родительского элемента, не используется для элементов верхнего уровня</param>
    /// <returns>Результат создания словаря</returns>
    public async Task<bool> CreateAsync(DictionaryModelBase data, int? toId = null)
    {
        ArgumentNullException.ThrowIfNull(data, nameof(data));
        await _dictionaryUnitOfWork.DictionaryRepository.CreateAsync(data);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }

    /// <summary>
    /// Получает все словари
    /// </summary>
    /// <returns>Список словарей</returns>
    public async Task<IEnumerable<DictionaryModel>> GetAllAsync()
    {
        return await _dictionaryUnitOfWork.DictionaryRepository.GetAllAsync();
    }

    /// <summary>
    /// Получает словарь по его ID
    /// </summary>
    /// <param name="id">ID словаря</param>
    /// <returns>Словарь</returns>
    public async Task<DictionaryModel?> GetByIdAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));

        return await _dictionaryUnitOfWork.DictionaryRepository.GetByIdAsync(id);
    }

    /// <summary>
    /// Обновляет словарь
    /// </summary>
    /// <param name="id">ID словаря для обновления</param>
    /// <param name="newData">Новые данные (название, описание, опционально входящие элементы [DictionaryElementModelBase])</param>
    /// <returns>Результат обновления словаря</returns>
    public async Task<bool> UpdateAsync(int id, DictionaryModelBase newData)
    {
        ArgumentNullException.ThrowIfNull(newData, nameof(newData));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));

        await _dictionaryUnitOfWork.DictionaryRepository.UpdateAsync(id, newData);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }

    /// <summary>
    /// Удаляет все словари
    /// </summary>
    /// <returns>Результат удаления</returns>
    public async Task<bool> DeleteAllAsync()
    {
        await _dictionaryUnitOfWork.DictionaryRepository.DeleteAllAsync();
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }

    /// <summary>
    /// Удаляет выбранные словари
    /// </summary>
    /// <param name="ids">массив ID словарей</param>
    /// <returns>Результат удаления</returns>
    public async Task<bool> DeleteAnyAsync(int[] ids)
    {
        ArgumentNullException.ThrowIfNull(ids, nameof(ids));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(ids.Length, nameof(ids));

        await _dictionaryUnitOfWork.DictionaryRepository.DeleteAnyAsync(ids);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }

    /// <summary>
    /// Удаляет словарь по его ID
    /// </summary>
    /// <param name="id">ID словаря</param>
    /// <returns>Результат удаления</returns>
    public async Task<bool> DeleteByIdAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
        await _dictionaryUnitOfWork.DictionaryRepository.DeleteByIdAsync(id);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }

    /// <summary>
    /// Получает список словарей (только информация, без его элементов)
    /// </summary>
    /// <returns>Список словарей только с основной информацией</returns>
    public async Task<IEnumerable<DictionaryModelInfo>> GetListAsync()
    {
        var repositories = await _dictionaryUnitOfWork.DictionaryRepository.GetAllAsync();
        return repositories.Select(a => a.ToDictionaryModelInfo()).ToList();
    }

    /// <summary>
    /// Очищает все словари (только элементы, основная и мета информация остаются)
    /// </summary>
    /// <returns>Результат очистки</returns>
    public async Task<bool> CleanAllAsync()
    {
        await _dictionaryUnitOfWork.DictionaryElementRepository.DeleteAllAsync();
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }

    /// <summary>
    /// Очищает выбранные словари
    /// </summary>
    /// <param name="ids">массив ID словарей</param>
    /// <returns>Результат очистки</returns>
    public async Task<bool> CleanAnyAsync(int[] ids)
    {
        ArgumentNullException.ThrowIfNull(ids, nameof(ids));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(ids.Length, nameof(ids));

        var dictionaries = await _dictionaryUnitOfWork.DictionaryRepository.GetAnyAsync(ids);
        ArgumentNullException.ThrowIfNull(dictionaries, nameof(dictionaries));
        
        var elementsForDeleting = new List<int>();
        foreach (var dictionary in dictionaries)
        {
            ArgumentNullException.ThrowIfNull(dictionary, nameof(dictionary));

            var dictionaryElements = dictionary.Elements;
            ArgumentNullException.ThrowIfNull(dictionaryElements, nameof(dictionaryElements));
            
            var elementsIds = dictionaryElements
                .Select(b => b.Id)
                .Where(id => id.HasValue)
                .Select(id => id!.Value)
                .ToList();
            if (elementsIds != null) elementsForDeleting.AddRange(elementsIds);
        }

        await _dictionaryUnitOfWork.DictionaryElementRepository.DeleteAnyAsync(elementsForDeleting.ToArray());
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }

    /// <summary>
    /// Очистка словаря по его ID
    /// </summary>
    /// <param name="id">ID словаря</param>
    /// <returns>Результат очистки</returns>
    public async Task<bool> CleanByIdAsync(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
        
        var dictionary = await _dictionaryUnitOfWork.DictionaryRepository.GetByIdAsync(id);
        ArgumentNullException.ThrowIfNull(dictionary, nameof(dictionary));
        
        var elements = dictionary.Elements;
        ArgumentNullException.ThrowIfNull(elements, nameof(elements));

        var elementIds = elements
            .Select(a => a.Id)
            .Where(b => b.HasValue)
            .Select(c => c!.Value)
            .ToArray();

        await _dictionaryUnitOfWork.DictionaryElementRepository.DeleteAnyAsync(elementIds);
        var res = await _dictionaryUnitOfWork.CommitAsync();
        return res > 0;
    }
}