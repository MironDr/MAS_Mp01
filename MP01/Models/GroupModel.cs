using System.Text;
using SQLite;

namespace MP01.Models;

public class GroupModel : BaseModel
{

   
    
    public string GroupName { get; set; }


    public GroupModel()
    {
        GroupName = string.Empty;
    }
    
    private GroupModel(string groupName)
    {
        GroupName = groupName;
    }

    //Asocjacje Kwalifikowana
    private readonly Dictionary<int, NoteModel> Notes = new();
    
    public void RemoveNote(int id)
    {
        NoteModel? model = null;
        if(Notes.TryGetValue(id, out var note))
           model = note;
        
        if (Notes.ContainsKey(id))
        {
            Notes.Remove(id);
        }
        
        if(model != null)
          if(model.Group == this)
              model.Group = null;
        

        
       
    }

    public void AddNoteToGroup(NoteModel note)
    {
        Notes.TryAdd(note.Id, note);

        if(note.Group != this)
            note.Group = this;
    }

    public Dictionary<int, NoteModel> GetNotes()
    {
        return Notes.ToDictionary(note => note.Key, note => note.Value);
    }
    //
    
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