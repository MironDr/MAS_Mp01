using MP01.DTOs;
using MP01.Models;

namespace MP01.Services;



public class NoteService
{

    private readonly List<NoteModel> _notes = new();



    


    public void UpdateNote(NoteModel note)
    {
        NoteModel? noteModel = _notes.Find(x => x.Id == note.Id);
        if (noteModel != null)
        {
            _notes.Remove(noteModel);
            _notes.Add(note);
        }
       
    }

    
    public NoteModel AddNote(NoteDTO noteDTO)
    {
        NoteModel note = NoteModel.CreateNote(noteDTO);
        _notes.Add(note); 
        return note;
    }
    
    

    public List<NoteModel> GetAllNotes()
    {
        return _notes.OrderBy(x => x.CreatedAt).ToList();
    }
    
    
    public List<T> GetNotesByType<T>() where T : NoteModel, new()
    {
        return _notes.OfType<T>().ToList();
    }


}