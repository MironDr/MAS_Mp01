using MP01.DTOs;
using MP01.Services;
using MP01.Utilities;

namespace MP01.Models;

public class CategoryModel : BaseModel
{
    public string CategoryName { get; init; }
    
    private static List<CategoryModel> _categories = new();

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
    
    
    

    public static CategoryModel? CreateCategory(CategoryDTO categoryDTO)
    {
        var categoryModel = _categories.Find(x => x.CategoryName == categoryDTO.CategoryName);

        if (categoryModel != null)
        {
            Console.WriteLine($"Category {categoryModel.CategoryName} already exists");
            return null;
        }

        CategoryModel category = new CategoryModel
        {
            CategoryName = categoryDTO.CategoryName
        };
        
        _categories.Add(category);
        
        return category;
        
    }
    
    
    
   
}