using DataAccessLayer.Database;
using DomainLayer.Interfaces.Repositories;
using DomainLayer.Interfaces.UnitOfWorks;
using DomainLayer.Models.DictionaryModels;

namespace DataAccessLayer.UnitOfWorks;

/// <summary>
/// UoW словарей
/// </summary>
public class DictionaryUnitOfWork : IDictionaryUnitOfWork, IAsyncDisposable
{
    private readonly AppDbContext _dbContext;
    private bool _disposed = false;
    
    public DictionaryUnitOfWork(
        AppDbContext appDbContext,
        IRepository<DictionaryModel, DictionaryModelBase> dictionaryRepository, 
        IRepository<DictionaryElementModel, DictionaryElementModelBase> dictionaryElementRepository)
    {
        ArgumentNullException.ThrowIfNull(appDbContext, nameof(appDbContext));
        ArgumentNullException.ThrowIfNull(dictionaryRepository, nameof(dictionaryRepository));
        ArgumentNullException.ThrowIfNull(dictionaryElementRepository, nameof(dictionaryElementRepository));
        
        _dbContext = appDbContext;
        DictionaryRepository = dictionaryRepository;
        DictionaryElementRepository = dictionaryElementRepository;
    }
    
    public IRepository<DictionaryModel, DictionaryModelBase> DictionaryRepository { get; init; }
    public IRepository<DictionaryElementModel, DictionaryElementModelBase> DictionaryElementRepository { get; init; }
    
    public async Task<int> CommitAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (!_disposed)
        {
            await _dbContext.DisposeAsync();
            _disposed = true;
        }
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();
        GC.SuppressFinalize(this);
    }
}