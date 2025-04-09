using SQLite;

namespace MP01.Models;

public abstract class BaseModel
{
    
    

    [PrimaryKey] [AutoIncrement] 
    public int Id { get; set; }
    


    
}