using System.Text;
using SQLite;

namespace MP01.Models;

public class GroupModel : BaseModel
{


    private NoteModel? _mainNote;
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
    private readonly List<NoteModel> Notes = new();
    
    public void RemoveNote(NoteModel note)
    {
        if (Notes.Contains(note))
        {
            Notes.Remove(note);
        }
        
        if (note.Group == this)
        {
            note.Group = null;
        }
    }

    public void AddNoteToGroup(NoteModel note)
    {
        if (!Notes.Contains(note) && note.Category == null && Notes.Count < 4)
        {
            Notes.Add(note);
        }
        
        if(note.Group != this)
            note.Group = this;
    }
    
    public List<NoteModel> GetNotes()
    {
        return Notes.ToList();
    }
    //


    public void AddMainNote(NoteModel note)
    {
        if(!Notes.Contains(note))
            return;
        
        _mainNote = note;
        
        note.IsMainInGroup = true;
    }

    public void RemoveMainNote()
    {
        if(_mainNote == null)
            return;

        _mainNote.IsMainInGroup = false;
        
        _mainNote = null;
        
    }
    

    public NoteModel? GetMainNote()
    {
        return _mainNote;
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
        foreach (NoteModel noteModel in Notes)
        {
            string note = string.Empty;
            
            if (_mainNote != null && _mainNote == noteModel)
            {
                note ="|[MAIN]" + noteModel;
            }
            else
            {
                note ="|" + noteModel;
            }
            
            
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