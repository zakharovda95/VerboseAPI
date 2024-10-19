namespace DomainLayer.Interfaces.Repositories;

/// <summary>
/// Базовый интерфейс репозитория.
/// </summary>
/// <typeparam name="TModel">Основная модель (для чтения)</typeparam>
/// <typeparam name="TModelBase">Базовая модель (для создания/редактирования)</typeparam>
public interface IRepository<TModel, in TModelBase> where TModel: class where TModelBase: class
{
    Task<bool> CreateAsync(TModelBase data, int? toId = null);
    Task<IEnumerable<TModel>> GetAllAsync();
    Task<IEnumerable<TModel>> GetAnyAsync(int[] ids);
    Task<TModel?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, TModelBase newData);
    Task<bool> DeleteAllAsync();
    Task<bool> DeleteAnyAsync(int[] ids);
    Task<bool> DeleteByIdAsync(int id);
}