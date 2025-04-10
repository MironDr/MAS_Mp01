using MP01.Models;

namespace MP01.DTOs;

public class NoteWithSourceDTO : BaseDTO
{
    public SourceNoteModel SourceNote { get; set; }
    public TextNoteModel Note { get; set; }
    public string? Quote { get; set; }
    public string? Comment { get; set; }
    public int? PageNumber { get; set; }
    public bool IsPrimary { get; set; }
}