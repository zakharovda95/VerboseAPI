namespace DomainLayer.Interfaces.Repositories;

/// <summary>
/// Базовый интерфейс репозитория.
/// </summary>
/// <typeparam name="TModel">Основная модель (для чтения)</typeparam>
/// <typeparam name="TModelBase">Базовая модель (для создания/редактирования)</typeparam>
public interface IRepository<TModel, in TModelBase> where TModel: class where TModelBase: class
{
    Task CreateAsync(TModelBase data, int? toId = null);
    Task<IEnumerable<TModel>> GetAllAsync();
    Task<TModel?> GetByIdAsync(int id);
    Task UpdateAsync(int id, TModelBase newData);
    Task DeleteAllAsync();
    Task DeleteByIdAsync(int id);
}