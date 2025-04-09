using System.Text;
using SQLite;

namespace MP01.Models;

public class GroupModel : BaseModel
{

    public readonly Dictionary<int, NoteModel> Notes = new();
    
    public string GroupName { get; set; }


    public GroupModel()
    {
        GroupName = string.Empty;
    }
    
    private GroupModel(string groupName)
    {
        GroupName = groupName;
    }

    public void RemoveNote(int id)
    {
        if(Notes.TryGetValue(id, out NoteModel note))
        {
            if (note != null) note.Group = null;
        }
    }

    public void AddNoteToGroup(NoteModel note)
    {
        Notes.TryAdd(note.Id, note);

        if (note.Group != this)
            note.Group = this;
        
        
    }
    
    public NoteModel? GetNoteFromGroup(int id)
    {
        return Notes.GetValueOrDefault(id);
    }
    
    public static GroupModel CreateGroup(string groupName)
    {
        return new GroupModel(groupName);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        
        string outline = $"-----{GroupName}--------------------------------------------------------------------------------------";
        
        
        
        sb.AppendLine(outline);
        foreach (int id in Notes.Keys)
        {
            string note ="|" + GetNoteFromGroup(id);
            sb.Append(note);
            int iter = outline.Length - note.Length;
            for (int i = 0; i < iter-1; i++)
            {
                sb.Append(' ');
            }
            sb.Append("|\n");
        }

        for (int i = 0; i < outline.Length; i++)
        {
            sb.Append('-');
        }
        
        return sb.ToString();
    }
}