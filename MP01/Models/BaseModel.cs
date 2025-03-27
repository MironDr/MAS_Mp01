using SQLite;

namespace MP01.Models;

public abstract class BaseModel
{
    
    protected static int MaxId;
    

    [PrimaryKey]
    public int Id 
    { 
        get => _id;
        init
        {
            if (value > MaxId)
            {
                MaxId = value;
                
            }
            _id = value;
        }
    }
    
    private int _id;

    
}