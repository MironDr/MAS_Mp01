namespace MP01.Models;

public class TagModel : BaseModel
{
    public string TagName { get; set; } = string.Empty;

    public List<TaggedNotes> _taggedNotes = new();

    public static TagModel CreateTag(string tagName)
    {
        return new TagModel { TagName = tagName };
    }

    public void AddNote(NoteModel note)
    {
        TaggedNotes.Create(this, note);
    }

    public void RemoveNote(NoteModel note)
    {
        TaggedNotes.Remove(this, note);
    }

    public List<NoteModel> GetNotes()
    {
        return _taggedNotes.Select(t => t.Note).ToList();
    }

}