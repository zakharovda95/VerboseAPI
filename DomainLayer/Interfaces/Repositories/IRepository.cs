namespace DomainLayer.Interfaces.Repositories;

/// <summary>
/// Базовый интерфейс репозитория.
/// </summary>
/// <typeparam name="TModel">Основная модель (для чтения)</typeparam>
/// <typeparam name="TModelBase">Базовая модель (для создания/редактирования)</typeparam>
public interface IRepository<TModel, in TModelBase> where TModel: class where TModelBase: class
{
    /// <summary>
    /// Добавить запись.
    /// </summary>
    /// <param name="data">Данные</param>
    /// <param name="toId">Необязательный параметр: ID родительского элемента</param>
    Task AddAsync(TModelBase data, int? toId = null);
    
    /// <summary>
    /// Добавить несколько записей.
    /// </summary>
    /// <param name="data">Данные</param>
    /// <param name="toId">Необязательный параметр: ID родительского элемента</param>
    Task AddRangeAsync(IEnumerable<TModelBase> data, int? toId = null);
    
    /// <summary>
    /// Получить все записи.
    /// </summary>
    /// <returns>Массив записей</returns>
    Task<IEnumerable<TModel>> GetAllAsync();
    
    /// <summary>
    /// Получить выбранные записи.
    /// </summary>
    /// <param name="ids">Массив ID записей</param>
    /// <returns>Массив записей</returns>
    Task<IEnumerable<TModel>> GetAnyAsync(IEnumerable<int> ids);
    
    /// <summary>
    /// Получить запись по ID.
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <returns>Запись</returns>
    Task<TModel?> GetByIdAsync(int id);
    
    /// <summary>
    /// Обновить запись по ID.
    /// </summary>
    /// <param name="id">ID записи</param>
    /// <param name="newData">Новые данные</param>
    Task UpdateAsync(int id, TModelBase newData);
    
    /// <summary>
    /// Удалить все записи.
    /// </summary>
    Task DeleteAllAsync();
    
    /// <summary>
    /// Удалить выбранные записи.
    /// </summary>
    /// <param name="ids">Массив ID записей</param>
    Task DeleteAnyAsync(IEnumerable<int> ids);
    
    /// <summary>
    /// Удалить запись по ID.
    /// </summary>
    /// <param name="id">ID записи</param>
    Task DeleteByIdAsync(int id);
}