using System.Reflection;
using MP01.Context;
using MP01.DTOs;
using MP01.Models;
using MP01.Utilities;

namespace MP01.Services;



public class NoteService
{
    private readonly AppDbContext _context;
    private readonly List<Type> _noteTypes;
    private List<NoteModel> _notes;


    public NoteService()
    {
        _context = ServiceLocator.Get<AppDbContext>();
        _noteTypes = ServiceLocator.Get<NotesTypeManager>().GetNoteTypes();

        _notes = GetAllNotesFromDb();

    }


    public void UpdateNote(NoteModel note)
    {
        _context.Update(note);
        _notes = GetAllNotesFromDb();
    }

    
    public void AddNote(NoteDTO noteDTO)
    {
  
        _context.Add(NoteModel.CreateNote(noteDTO)); 
        _notes = GetAllNotesFromDb();
        
    }
    
    
    private List<NoteModel> GetAllNotesFromDb()
    {
        
        var notes = new List<NoteModel>();

        foreach (var noteType in _noteTypes)
        {
            
            var method = typeof(NoteService).GetMethod(nameof(GetNotesByType), BindingFlags.NonPublic | BindingFlags.Instance)?.MakeGenericMethod(noteType);
            
            if (method != null)
            {
          
                var result = method.Invoke(this, null);

                if (result is IEnumerable<NoteModel> noteList)
                {

                    notes.AddRange(noteList);

                }
            }
        }

      
        
        return notes;
        
        
    }

    public List<NoteModel> GetAllNotes()
    {
        return _notes.OrderBy(x => x.CreatedAt).ToList();
    }
    
    
    private List<NoteModel?> GetNotesByType<T>() where T : NoteModel, new()
    {
        
        return _context.GetAllOfType<T>().Cast<NoteModel?>().ToList();
    }


}