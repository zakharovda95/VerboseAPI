namespace DomainLayer.Interfaces.Repositories;

/// <summary>
/// Базовый интерфейс репозитория.
/// </summary>
/// <typeparam name="TModel">Основная модель (для чтения)</typeparam>
/// <typeparam name="TModelShort">Базовая модель (для создания/редактирования)</typeparam>
public interface IRepository<TModel, in TModelShort> where TModel: class where TModelShort: class
{
    Task<bool> CreateAsync(TModelShort data, int? toId = null);
    Task<IEnumerable<TModel>> GetAllAsync();
    Task<IEnumerable<TModel>> GetAnyAsync(int[] ids);
    Task<TModel?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, TModelShort newData);
    Task<bool> DeleteAllAsync();
    Task<bool> DeleteAnyAsync(int[] ids);
    Task<bool> DeleteByIdAsync(int id);
}