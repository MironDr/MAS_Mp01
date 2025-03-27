



using System.Text;
using MP01.DTOs;
using MP01.Services;
using MP01.Utilities;

namespace MP01.Models;

public class NoteModel : BaseModel
{
    public string Title {get; init; }
    
    public string? Description { get; init; }
    
    public DateTime CreatedAt { get; init; }

    public int? CategoryId { get; set; }


    private int DaysSinceCreation => (DateTime.Now - CreatedAt).Days;
    
    public void SetCategory(int? categoryId)
    {
        CategoryId = categoryId;
    }
    
    
    
    public static  NoteModel CreateNote(NoteDTO noteDTO)
    {
        if (noteDTO is TextNoteDTO textNoteDTO)
        {
            TextNoteModel newNote = new TextNoteModel
            {
                Id = ++MaxId,
                CreatedAt = DateTime.Now,
                Title = textNoteDTO.Title,
                Description = textNoteDTO.Description,
                Content = textNoteDTO.Content
            };
            return newNote;
        }

        if (noteDTO is AccountNoteDTO accountNoteModel)
        {
            AccountNoteModel newNote = new AccountNoteModel
            {
                Id = ++MaxId,
                CreatedAt = DateTime.Now,
                Title = accountNoteModel.Title,
                Description = accountNoteModel.Description,
                AccountLogin = accountNoteModel.AccountLogin,
                AccountPassword = accountNoteModel.AccountPassword
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
        
        var category = ServiceLocator.Get<CategoryService>().GetCategoryById(CategoryId);
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
    
