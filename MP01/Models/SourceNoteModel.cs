using MP01.DTOs;
using SQLite;

namespace MP01.Models;

public class SourceNoteModel : NoteModel
{
    public ReferenceType Type { get; init; } 
    public string Source { get; init; }
    public string? Author { get; init; }
    public DateTime PublishedDate { get; init; }

    private static string NoteTypeString = "Source Note";

    //Asocjacje z atrybutem
    private List<NoteWithSource> Sources = new();
    
    public void AddNote(NoteWithSource ns)
    {
        if (ns.Note == null)
        {
            Console.WriteLine("Note is null");
            return;
        }
        
        if(Sources.Contains(ns))
            return;
        
        if (ns.SourceNote == this)
        {
            Sources.Add(ns);
        }
    }

    public void RemoveNote(NoteWithSource ns)
    {
        if(!Sources.Contains(ns))   
            return;
        
        Sources.Remove(ns);
        
        if (ns.SourceNote == this)
        {
            ns.Remove();
        }
        
        
        
    }
    
    public List<TextNoteModel> GetTextNoteModelsLinks()
    {
        return Sources.Select(t => t.Note).ToList();
    }

    public List<NoteWithSource> GetNotesLinks()
    {
        return Sources.ToList();
    }
    //
    
    public override string ToString()
    {
        return base.ToString() + $", NoteType: {NoteTypeString}";
    }
}