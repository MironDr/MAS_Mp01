using MP01.Context;
using MP01.DTOs;
using MP01.Models;
using MP01.Utilities;

namespace MP01.Services;

public class TagService
{
    private readonly AppDbContext _context;
 
    private List<TagModel> _tags;

    public TagService()
    {
        _context = ServiceLocator.Get<AppDbContext>();
        _tags = _context.GetAllOfType<TagModel>();
    }
    
    public void AddTag(TagModel tag)
    {
        _context.Add(tag);
        _tags = _context.GetAllOfType<TagModel>();
    }
    
    
    public List<TagModel> GetTags()
    {
        return _tags;
    }
    
    
}