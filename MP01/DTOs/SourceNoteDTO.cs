namespace MP01.DTOs;

public enum ReferenceType
{
    Book,
    Website
}

public class SourceNoteDTO : NoteDTO
{
    public ReferenceType Type { get; init; } 
    public string Source { get; init; }
    public string? Author { get; init; }
    public DateTime PublishedDate { get; init; }
}