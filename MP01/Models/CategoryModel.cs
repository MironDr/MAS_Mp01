using MP01.DTOs;
using MP01.Services;
using MP01.Utilities;

namespace MP01.Models;

public class CategoryModel : BaseModel
{
    public string CategoryName { get; init; }

    //Asocjacje Zwykła
    private List<NoteModel> Notes = new(); 
    
    public void AddNote(NoteModel note)
    {
        if (!Notes.Contains(note) && note.Group == null)
        {
            Notes.Add(note);
        }
        
        if(note.Category != this)
            note.Category = this;
    }

    public void RemoveNote(NoteModel note)
    {
        if (Notes.Contains(note))
        {
            Notes.Remove(note);
        }
        
        if (note.Category == this)
        {
            note.Category = null;
        }
    }

    public List<NoteModel> GetNotes()
    {
        return Notes.ToList();
    }
    //
    
    
    

    public static CategoryModel CreateCategory(CategoryDTO categoryDTO)
    {
        var categoryModel = ServiceLocator.Get<CategoryService>().GetCategories().Find(x => x.CategoryName == categoryDTO.CategoryName);

        if(categoryModel != null)
            throw new Exception("Category with this name already exists");
        
        CategoryModel category = new CategoryModel
        {
            CategoryName = categoryDTO.CategoryName
        };
        
        return category;
        
    }
    
    
    
   
}