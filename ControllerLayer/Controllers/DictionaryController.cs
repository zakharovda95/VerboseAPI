using DomainLayer.Interfaces.Services;
using DomainLayer.Models.DictionaryModels;
using Microsoft.AspNetCore.Mvc;

namespace ControllerLayer.Controllers;

[ApiController]
[Route("/api")]
public class DictionaryController : ControllerBase
{
    private readonly IDictionaryService? _dictionaryService;
    public DictionaryController(IDictionaryService dictionaryService)
    {
        ArgumentNullException.ThrowIfNull(dictionaryService);
        _dictionaryService = dictionaryService;
    }
    
    [HttpPost]
    [Route("/[controller]/add")]
    public async Task<ActionResult> AddDictionary(DictionaryModelShort body)
    {
        ArgumentNullException.ThrowIfNull(body);
        var (_, title, description, items ) = body;
        
        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description)) 
            return BadRequest("Не переданы обязательные параметры");

        var res = await _dictionaryService!.CreateAsync(body);
        if (!res) return BadRequest("Не удалось добавить словарь");
        return Ok("Успех!");
    }
}