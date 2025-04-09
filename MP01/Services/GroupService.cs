using MP01.Context;
using MP01.DTOs;
using MP01.Models;
using MP01.Utilities;

namespace MP01.Services;

public class GroupService
{
    private readonly AppDbContext _context;
 
    private List<GroupModel> _groupies;
    

    public GroupService()
    {
        _context = ServiceLocator.Get<AppDbContext>();
        _groupies = _context.GetAllOfType<GroupModel>();
    }
    
    public void AddGroup(GroupModel group)
    {
        _context.Add(group);
        _groupies = _context.GetAllOfType<GroupModel>();
        ServiceLocator.Get<NoteService>().InitGroups();
    }
    
    public List<GroupModel> GetGroups()
    {
        return _groupies;
    }
}