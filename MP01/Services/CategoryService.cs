using MP01.Context;
using MP01.DTOs;
using MP01.Models;
using MP01.Utilities;

namespace MP01.Services;

public class CategoryService
{
    private readonly AppDbContext _context;
 
    private List<CategoryModel> _categories;

    public CategoryService()
    {
        _context = ServiceLocator.Get<AppDbContext>();
        _categories = _context.GetAllOfType<CategoryModel>();
    }
    
    public void AddCategory(CategoryDTO categoryDTO)
    {
        _context.Add(CategoryModel.CreateCategory(categoryDTO));
        _categories = _context.GetAllOfType<CategoryModel>();
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