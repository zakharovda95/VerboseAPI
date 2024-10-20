using DomainLayer.Models.DictionaryModels;

namespace DomainLayer.Interfaces.Services;

public interface IDictionaryElementService
{
    /// <summary>
    /// Добавить элемент в словарь.
    /// </summary>
    /// <param name="data">Данные</param>
    /// <param name="toId">ID словаря</param>
    /// <returns>Результат добавления</returns>
    Task<bool> AddDictionaryElementAsync(DictionaryElementModelBase data, int? toId = null);
    
    /// <summary>
    /// Получить все элементы словарей.
    /// </summary>
    /// <returns>Элементы словарей</returns>
    Task<IEnumerable<DictionaryElementModel>> GetAllDictionaryElementsAsync();
    
    /// <summary>
    /// Получить элемент словаря по ID.
    /// </summary>
    /// <param name="id">ID словаря</param>
    /// <returns>Элементы словаря или null</returns>
    Task<DictionaryElementModel?> GetDictionaryElementByIdAsync(int id);
    
    /// <summary>
    /// Обновить данные эелемента словаря.
    /// </summary>
    /// <param name="id">ID словаря</param>
    /// <param name="newData">Новые данные</param>
    /// <returns>Результат обновления</returns>
    Task<bool> UpdateDictionaryElementAsync(int id, DictionaryElementModelBase newData);
    
    /// <summary>
    /// Удалить все элементы словарей (все записи).
    /// </summary>
    /// <returns>Результат удаления</returns>
    Task<bool> DeleteAllDictionaryElementsAsync();
    
    /// <summary>
    /// Удалить выбранные элементы словарей.
    /// </summary>
    /// <param name="ids">Массив ID элементов словарей</param>
    /// <returns>Результат удаления</returns>
    Task<bool> DeleteAnyDictionaryElementsAsync(IEnumerable<int> ids);
    
    /// <summary>
    /// Удалить элемент словаря по его ID.
    /// </summary>
    /// <param name="id">ID элемента словаря</param>
    /// <returns>Результат удаления</returns>
    Task<bool> DeleteDictionaryElementByIdAsync(int id);
    
    /// <summary>
    /// Копировать все элементы из одного словаря в другой.
    /// </summary>
    /// <param name="fromDictionaryId">ID словаря, откуда копировать</param>
    /// <param name="toDictionaryId">ID словаря куда копировать</param>
    /// <returns>Результат копирования</returns>
    public Task<bool> CopyAllDictionaryElementsFromDictionaryAsync(int fromDictionaryId, int toDictionaryId);
    
    /// <summary>
    /// Копировать выбранные элементы словарей (без привязки к конкретному словарю) в другой словарь.
    /// </summary>
    /// <param name="ids">Массив ID элементов словарей</param>
    /// <param name="toDictionaryId">ID словаря куда копировать</param>
    /// <returns>Результат копирования</returns>
    public Task<bool> CopyAnyDictionaryElementsFromDictionaryAsync(IEnumerable<int> ids, int toDictionaryId);
    
    /// <summary>
    /// Копировать элемент словаря в другой словарь.
    /// </summary>
    /// <param name="id">ID элемента словаря</param>
    /// <param name="toDictionaryId">ID словаря куда копировать</param>
    /// <returns>Результат копирования</returns>
    public Task<bool> CopyDictionaryElementByIdFromDictionaryAsync(int id, int toDictionaryId);
    
    /// <summary>
    /// Переместить элементы словаря из одного словаря в другой.
    /// </summary>
    /// <param name="fromDictionaryId">ID словаря, откуда переместить</param>
    /// <param name="toDictionaryId">ID словаря, куда переместить</param>
    /// <returns>Результат перемещения</returns>
    public Task<bool> MoveAllDictionaryElementsFromDictionaryAsync(int fromDictionaryId, int toDictionaryId);
    
    /// <summary>
    /// Переместить выбранные эелементы словаря (без привязки к конкретному словарю) в другой словарь.
    /// </summary>
    /// <param name="ids">Массив ID элементов словаря</param>
    /// <param name="toDictionaryId">ID словаря, куда переместить</param>
    /// <returns>Результат перемещения</returns>
    public Task<bool> MoveAnyDictionaryElementsFromDictionaryAsync(IEnumerable<int> ids, int toDictionaryId);
    
    /// <summary>
    /// Переместить элемент словаря по его ID в другой словарь.
    /// </summary>
    /// <param name="id">ID элемента словаря</param>
    /// <param name="toDictionaryId">ID словаря, куда переместить</param>
    /// <returns>Результат перемещения</returns>
    public Task<bool> MoveDictionaryElementByIdFromDictionaryAsync(int id, int toDictionaryId);
}