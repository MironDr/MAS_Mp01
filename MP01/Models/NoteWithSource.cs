using MP01.DTOs;

namespace MP01.Models;

public class NoteWithSource
{
    //Asocjacje z atrybutem
    public SourceNoteModel? SourceNote { get; private set; }
    public TextNoteModel? Note { get; private set; }

    public string? Quote { get; set; }
    public string? Comment { get; set; }
    public int? PageNumber { get; set; }
    public bool IsPrimary { get; set; }
    public DateTime CreatedAt { get; }

    private NoteWithSource(SourceNoteModel sourceNote, TextNoteModel note)
    {
        SourceNote = sourceNote;
        Note = note;
        CreatedAt = DateTime.Now;
    }

    public static NoteWithSource? Create(NoteWithSourceDTO dto)
    {
        var ns = new NoteWithSource(dto.SourceNote, dto.Note)
        {
            Quote = dto.Quote,
            Comment = dto.Comment,
            PageNumber = dto.PageNumber,
            IsPrimary = dto.IsPrimary
        };
        
        
        dto.SourceNote.AddNote(ns);
        dto.Note.AddSourceLink(ns);

        return ns;
    }

    public void Remove()
    {
        if (SourceNote != null && Note != null)
        {
            SourceNote?.RemoveNote(this);
            Note?.RemoveSourceLink(this);
            
            Note = null;
            SourceNote = null;
            
        }

        
    }
    
    
    //

    public override string ToString()
    {
        return $"Source: {SourceNote}\nNote: {Note}\nQuote: {Quote}\nComment: {Comment}";
    }
}