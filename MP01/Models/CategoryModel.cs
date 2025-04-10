using MP01.DTOs;

namespace MP01.Models;

public class CategoryModel : BaseModel
{
    public string CategoryName { get; init; }

    //Asocjacje Zwykła
    private List<NoteModel> Notes = new(); 
    
    public void AddNote(NoteModel note)
    {
        if (!Notes.Contains(note))
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
        CategoryModel category = new CategoryModel
        {
            CategoryName = categoryDTO.CategoryName
        };
        
        return category;
        
    }
    
    
    
   
}