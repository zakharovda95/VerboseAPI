using DomainLayer.Interfaces.Repositories.Dictionary;

namespace DomainLayer.UnitOfWorks;

public class DictionaryUnitOfWork : IDisposable
{
    private readonly IDictionaryRepository _dictionaryRepository;
    private readonly IDictionaryElementRepository _dictionaryElementRepository;
    
    public DictionaryUnitOfWork(IDictionaryRepository dictionaryRepository, IDictionaryElementRepository dictionaryElementRepository)
    {
        _dictionaryRepository = dictionaryRepository;
        _dictionaryElementRepository = dictionaryElementRepository;
    }
    
    public void Dispose()
    {
        // TODO release managed resources here
    }
}