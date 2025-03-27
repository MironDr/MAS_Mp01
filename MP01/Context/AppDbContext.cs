using MP01.Models;
using MP01.Utilities;
using SQLite;

namespace MP01.Context;

public class AppDbContext
{
    private readonly SQLiteConnection _connection;
    private readonly List<Type> _noteTypes;
    public AppDbContext(string dbPath)
    {
        _connection = new SQLiteConnection(dbPath);
        _connection.CreateTable<CategoryModel>();

        _noteTypes = ServiceLocator.Get<NotesTypeManager>().GetNoteTypes();
        CreateTables();
    }
    
   
    
    private void CreateTables()
    {
     
        foreach (var noteType in _noteTypes)
        {
            _connection.CreateTable(noteType);
        }
    }

 
    
    public void Add<T>(T entity) where T : new()
    {
        _connection.Insert(entity);
    }
    
    public List<T> GetAllOfType<T>() where T : new()
    {
        return _connection.Table<T>().ToList();
    }

    
    public void Update<T>(T entity) where T : new()
    {
        _connection.Update(entity);
    }
    

    public T? GetById<T>(int id) where T : new()
    {
        return _connection.Find<T>(id);
    }

    
 
}