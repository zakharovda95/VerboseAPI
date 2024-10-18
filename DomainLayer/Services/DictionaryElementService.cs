using DomainLayer.Interfaces.Services;
using DomainLayer.Models.DictionaryModels;

namespace DomainLayer.Services;

// public class DictionaryElementService : IDictionaryElementService
// {
//     private IDictionaryElementRepository _dictionaryElementRepository;
//
//     public DictionaryElementService(IDictionaryElementRepository dictionaryElementRepository)
//     {
//         ArgumentNullException.ThrowIfNull(dictionaryElementRepository);
//         _dictionaryElementRepository = dictionaryElementRepository;
//     }
//
//     public async Task<bool> CreateAsync(DictionaryElementModelShort data, int? toId = null)
//     {
//         try
//         {
//             return await _dictionaryElementRepository.CreateAsync(data, toId);
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<IEnumerable<DictionaryElementModel>> GetAllAsync()
//     {
//         try
//         {
//             return await _dictionaryElementRepository.GetAllAsync();
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<IEnumerable<DictionaryElementModel>> GetAnyAsync(int[] ids)
//     {
//         try
//         {
//             return await _dictionaryElementRepository.GetAnyAsync(ids);
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<DictionaryElementModel?> GetByIdAsync(int id)
//     {
//         try
//         {
//             return await _dictionaryElementRepository.GetByIdAsync(id);
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<bool> UpdateAsync(int id, DictionaryElementModelShort newData)
//     {
//         try
//         {
//             return await _dictionaryElementRepository.UpdateAsync(id, newData);
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<bool> DeleteAllAsync()
//     {
//         try
//         {
//             return await _dictionaryElementRepository.DeleteAllAsync();
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<bool> DeleteAnyAsync(int[] ids)
//     {
//         try
//         {
//             return await _dictionaryElementRepository.DeleteAnyAsync(ids);
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<bool> DeleteByIdAsync(int id)
//     {
//         try
//         {
//             return await _dictionaryElementRepository.DeleteByIdAsync(id);
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<bool> CopyAllAsync(int fromDictionaryId, int toDictionaryId)
//     {
//         try
//         {
//             return await _dictionaryElementRepository.CopyAllAsync(fromDictionaryId, toDictionaryId);
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<bool> CopyAnyAsync(int[] ids, int toDictionaryId)
//     {
//         try
//         {
//             return await _dictionaryElementRepository.CopyAnyAsync(ids, toDictionaryId);
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<bool> CopyByIdAsync(int id, int toDictionaryId)
//     {
//         try
//         {
//             return await _dictionaryElementRepository.CopyByIdAsync(id, toDictionaryId);
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<bool> MoveAllAsync(int fromDictionaryId, int toDictionaryId)
//     {
//         try
//         {
//             return await _dictionaryElementRepository.MoveAllAsync(fromDictionaryId, toDictionaryId);
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<bool> MoveAnyAsync(int[] ids, int toDictionaryId)
//     {
//         try
//         {
//             return await _dictionaryElementRepository.MoveAnyAsync(ids, toDictionaryId);
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<bool> MoveByIdAsync(int id, int toDictionaryId)
//     {
//         try
//         {
//             return await _dictionaryElementRepository.MoveByIdAsync(id, toDictionaryId);
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
// }