using System.Text;
using MP01.DTOs;
using MP01.Services;
using MP01.Utilities;
using SQLite;

namespace MP01.Models;

public class NoteModel : BaseModel
{
    public string Title {get; init; }
    
    public string? Description { get; init; }
    
    private int DaysSinceCreation => (DateTime.Now - CreatedAt).Days;
    public DateTime CreatedAt { get; init; }
    
    
    //Asocjacje Zwykła

    private CategoryModel? _category;
    
    public CategoryModel? Category
    {
        get => _category;
        
        set 
        {
            if(_group != null)
                return;
            
            if (_category != null && _category.GetNotes().Contains(this))
            {
                _category.RemoveNote(this);
            }
            
            _category = value;
              
            if (_category != null)
            {
                _category.AddNote(this);
            }
        }
    }
    //
    
    
    //Asocjacje Kwalifikowana
    
    private GroupModel? _group;
    
    public GroupModel? Group
    {
            get => _group;
            set 
            {
                if(_category != null)
                    return;
                
                if (_group != null && _group.GetNotes().Contains(this))
                {
                    _group.RemoveNote(this);
                }
                _group = value;
              
                if (_group != null)
                {
                    _group.AddNoteToGroup(this);
                }
            }
    }
    //

    private bool _isMainInGroup;
    public bool IsMainInGroup
    {
        get => _isMainInGroup;
        set
        {
            if (_group == null || value == _isMainInGroup)
                return;
            
            if (value)
            {
                if (_group.GetMainNote() == null)
                    _group.AddMainNote(this);
            }
            else
            {
                if (_group.GetMainNote() != null)
                    _group.RemoveMainNote();
            }


            _isMainInGroup = value;
           
        }
    }
   
    
    
    public static  NoteModel CreateNote(NoteDTO noteDTO)
    {
        if(noteDTO.Title.Length > 50)
            throw new ArgumentException("Title length must be less than 50 characters");
        
        
        if (noteDTO is TextNoteDTO textNoteDTO)
        {
            TextNoteModel newNote = new TextNoteModel
            {
                CreatedAt = DateTime.Now,
                Title = textNoteDTO.Title,
                Description = textNoteDTO.Description,
            };
            return newNote;
        }
        
        if (noteDTO is SourceNoteDTO sourceNoteDTO)
        {
            SourceNoteModel newNote = new SourceNoteModel
            {
                CreatedAt = DateTime.Now,
                Title = sourceNoteDTO.Title,
                Description = sourceNoteDTO.Description,
                PublishedDate = sourceNoteDTO.PublishedDate,
                Author = sourceNoteDTO.Author,
                Source = sourceNoteDTO.Source,
                Type = sourceNoteDTO.Type
            };
            return newNote;
        }
        


        throw new ArgumentException("Unknown note type");

    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append($"Tittle: {Title}");
        
        if (Description != null)
            stringBuilder.Append($", Description: {Description}");
        
        stringBuilder.Append($", CreatedAt: {GetCreatedAt()}");
        
        return stringBuilder.ToString();
    }
    
    public virtual string ToStringFull()
    {
        StringBuilder stringBuilder = new StringBuilder();
        
        stringBuilder.Append($"<<<<<<{Title}>>>>>>\n");
        
        stringBuilder.Append($"Id: {Id}\n");
        
        if(Description != null)
            stringBuilder.Append($"Description: {Description}\n");
        
        stringBuilder.Append($"CreatedAt: {GetCreatedAt("yyyy-MM-dd HH:mm:ss")}\n");
        
        stringBuilder.Append($"Days since creation: {DaysSinceCreation}\n");
        
        var category = ServiceLocator.Get<CategoryService>().GetCategoryById(Category?.Id);
        if (category != null)
        {
            stringBuilder.Append($"Category: {category.CategoryName}\n");
        }
        
        return stringBuilder.ToString();
    }

    private string GetCreatedAt(string format)
    {
        return CreatedAt.ToString(format);
    }

    private string GetCreatedAt()
    {
        return CreatedAt.ToString("yyyy-MM-dd");
    }
}
    
