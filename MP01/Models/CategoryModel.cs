using MP01.DTOs;

namespace MP01.Models;

public class CategoryModel : BaseModel
{
    public string CategoryName { get; init; }


    public static CategoryModel CreateCategory(CategoryDTO categoryDTO)
    {
        CategoryModel category = new CategoryModel
        {
            Id = ++MaxId,
            CategoryName = categoryDTO.CategoryName
        };
        
        return category;
        
    }
    
   
}