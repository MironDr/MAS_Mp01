using MP01.DTOs;
using MP01.Models;
using MP01.Utilities;

namespace MP01.Services;

public class CategoryService
{

 
    private List<CategoryModel> _categories = new();


    public CategoryModel AddCategory(CategoryDTO categoryDTO)
    {
        CategoryModel categoryModel = CategoryModel.CreateCategory(categoryDTO);
        
        _categories.Add(categoryModel);
        
        return categoryModel;
    }
    

    public CategoryModel? GetCategoryById(int? id)
    {
        if (id == null)
            return null;
        
        return _categories.FirstOrDefault(c => c.Id == id);
    }
    
    
    public List<CategoryModel> GetCategories()
    {
        return _categories;
    }
    
    
}