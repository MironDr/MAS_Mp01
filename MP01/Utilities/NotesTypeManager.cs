using MP01.Models;

namespace MP01.Utilities;

public class NotesTypeManager
{
    
    private readonly List<Type> _noteTypes = new();
    
    public NotesTypeManager()
    {
        RegisterNoteTypes();
    }
    
    
    private void RegisterNoteTypes()
    {
        _noteTypes.Add(typeof(TextNoteModel));
        
    }
    
    
    public List<Type> GetNoteTypes() => _noteTypes;
}