using ControllerLayer.Models.DictionaryElements;

namespace ControllerLayer.Models.Dictionaries;

public class DictionaryBodyModel
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public IEnumerable<DictionaryElementBodyModel>? Items { get; init; } = null;

    public void Deconstruct(
        out string title, 
        out string description, 
        out IEnumerable<DictionaryElementBodyModel>? items)
    {
        title = Title;
        description = Description;
        items = Items;
    }
}