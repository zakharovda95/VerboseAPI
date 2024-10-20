using DomainLayer.Models.DictionaryModels;

namespace DomainLayer.Interfaces.Services;

public interface IDictionaryService
{
    /// <summary>
    /// Добавить новый словарь.
    /// </summary>
    /// <param name="data">Данные словаря (название, описание, опционально входящие элементы [DictionaryElementModelBase])</param>
    /// <param name="toId">null</param>
    /// <returns>Результат добавления</returns>
    Task<bool> AddDictionaryAsync(DictionaryModelBase data, int? toId = null);
    
    /// <summary>
    /// Получить все словари.
    /// </summary>
    /// <returns>Список словарей</returns>
    Task<IEnumerable<DictionaryModel>> GetAllDictionariesAsync();
    
    /// <summary>
    /// Получить словарь по его ID.
    /// </summary>
    /// <param name="id">ID словаря</param>
    /// <returns>Словарь</returns>
    Task<DictionaryModel?> GetDictionaryByIdAsync(int id);
    
    /// <summary>
    /// Обновить словарь.
    /// </summary>
    /// <param name="id">ID словаря для обновления</param>
    /// <param name="newData">Новые данные (название, описание, опционально входящие элементы [DictionaryElementModelBase])</param>
    /// <returns>Результат обновления</returns>
    Task<bool> UpdateDictionaryAsync(int id, DictionaryModelBase newData);
    
    /// <summary>
    /// Удалить все словари.
    /// </summary>
    /// <returns>Результат удаления</returns>
    Task<bool> DeleteAllDictionariesAsync();
    
    /// <summary>
    /// Удалить выбранные словари.
    /// </summary>
    /// <param name="ids">Массив ID словарей</param>
    /// <returns>Результат удаления</returns>
    Task<bool> DeleteAnyDictionariesAsync(IEnumerable<int> ids);
    
    /// <summary>
    /// Удалить словарь по его ID.
    /// </summary>
    /// <param name="id">ID словаря</param>
    /// <returns>Результат удаления</returns>
    Task<bool> DeleteDictionaryByIdAsync(int id);
    
    /// <summary>
    /// Получить список словарей (только информация, без его элементов).
    /// </summary>
    /// <returns>Список словарей только с основной информацией</returns>
    public Task<IEnumerable<DictionaryModelInfo>> GetDictionaryInfoListAsync();
    
    /// <summary>
    /// Очистить все словари (только элементы, основная и мета информация остаются).
    /// </summary>
    /// <returns>Результат очистки</returns>
    public Task<bool> CleanAllDictionariesAsync();
    
    /// <summary>
    /// Очистить выбранные словари.
    /// </summary>
    /// <param name="ids">Массив ID словарей</param>
    /// <returns>Результат очистки</returns>
    public Task<bool> CleanAnyDictionariesAsync(IEnumerable<int> ids);
    
    /// <summary>
    /// Очистить словарь по его ID.
    /// </summary>
    /// <param name="id">ID словаря</param>
    /// <returns>Результат очистки</returns>
    public Task<bool> CleanDictionaryByIdAsync(int id);
}