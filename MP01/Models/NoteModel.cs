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
                if (_group != null && _group.GetNotes().ContainsKey(Id))
                {
                    _group.RemoveNote(Id);
                }
                _group = value;
              
                if (_group != null)
                {
                    _group.AddNoteToGroup(this);
                }
            }
    }
    //
    
    
   
    
    
    public static  NoteModel CreateNote(NoteDTO noteDTO)
    {
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
    
