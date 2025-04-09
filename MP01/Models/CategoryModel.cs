using MP01.DTOs;

namespace MP01.Models;

public class CategoryModel : BaseModel
{
    public string CategoryName { get; init; }

    public List<NoteModel> Notes = new(); 


    public static CategoryModel CreateCategory(CategoryDTO categoryDTO)
    {
        CategoryModel category = new CategoryModel
        {
            CategoryName = categoryDTO.CategoryName
        };
        
        return category;
        
    }
    
    
    
   
}