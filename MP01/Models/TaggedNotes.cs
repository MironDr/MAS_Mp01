namespace MP01.Models;

public class TaggedNotes
{
    public TagModel Tag { get; }
    public NoteModel Note { get; }

    private TaggedNotes(TagModel tag, NoteModel note)
    {
        Tag = tag;
        Note = note;
    }

    public static void Create(TagModel tag, NoteModel note)
    {
        if (tag._taggedNotes.Any(t => t.Note == note)) return;

        var taggedNotes = new TaggedNotes(tag, note);

        tag._taggedNotes.Add(taggedNotes);
        note._taggedNotes.Add(taggedNotes);
    }

    public static void Remove(TagModel tag, NoteModel note)
    {
        var existing = tag._taggedNotes.FirstOrDefault(t => t.Note == note);
        if (existing != null)
        {
            tag._taggedNotes.Remove(existing);
            note._taggedNotes.Remove(existing);
        }
    }
}