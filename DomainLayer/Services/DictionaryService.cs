using DomainLayer.Interfaces.Services;
using DomainLayer.Models.DictionaryModels;

namespace DomainLayer.Services;

// public class DictionaryService : IDictionaryService
// {
//     private readonly IDictionaryRepository _dictionaryRepository;
//     public DictionaryService(IDictionaryRepository dictionaryRepository)
//     {
//         ArgumentNullException.ThrowIfNull(dictionaryRepository);
//         _dictionaryRepository = dictionaryRepository;
//     }
//     
//     public async Task<bool> CreateAsync(DictionaryModelShort data, int? toId = null)
//     {
//         try
//         {
//             return await _dictionaryRepository.CreateAsync(data, toId);
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<IEnumerable<DictionaryModel>> GetAllAsync()
//     {
//         try
//         {
//             return await _dictionaryRepository.GetAllAsync();
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<IEnumerable<DictionaryModel>> GetAnyAsync(int[] ids)
//     {
//         try
//         {
//             return await _dictionaryRepository.GetAnyAsync(ids);
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<DictionaryModel?> GetByIdAsync(int id)
//     {
//         try
//         {
//             return await _dictionaryRepository.GetByIdAsync(id);
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<bool> UpdateAsync(int id, DictionaryModelShort newData)
//     {
//         try
//         {
//             return await _dictionaryRepository.UpdateAsync(id, newData);
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
//             return await _dictionaryRepository.DeleteAllAsync();
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
//             return await _dictionaryRepository.DeleteAnyAsync(ids);
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
//             return await _dictionaryRepository.DeleteByIdAsync(id);
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<IEnumerable<DictionaryModelBase>> GetListAsync()
//     {
//         try
//         {
//             return await _dictionaryRepository.GetListAsync();
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<bool> CleanAllAsync()
//     {
//         try
//         {
//             return await _dictionaryRepository.CleanAllAsync();
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<bool> CleanAnyAsync(int[] ids)
//     {
//         try
//         {
//             return await _dictionaryRepository.CleanAnyAsync(ids);
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
//
//     public async Task<bool> CleanByIdAsync(int id)
//     {
//         try
//         {
//             return await _dictionaryRepository.CleanByIdAsync(id);
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//     }
// }