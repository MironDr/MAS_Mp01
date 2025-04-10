using MP01.Models;


namespace MP01.Services;

public class GroupService
{

 
    private List<GroupModel> _groupies = new();
    
    
    
    public void AddGroup(GroupModel group)
    {
        _groupies.Add(group);
    }
    
    public List<GroupModel> GetGroups()
    {
        return _groupies;
    }
}