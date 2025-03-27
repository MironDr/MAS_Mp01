using System.Text;
using MP01.DTOs;
using MP01.Models;
using MP01.Services;
using MP01.Utilities;

namespace MP01.View;

public class NotesView
{

    private readonly NoteService _noteService;
    private readonly CategoryService _categoryService;

    private readonly HashSet<Type> _noteTypes = new();
    
    public NotesView()
    {
        _noteService = ServiceLocator.Get<NoteService>();
        _categoryService = ServiceLocator.Get<CategoryService>();
    }
    
    public void CreateCategoryFromView()
    {
        Console.WriteLine("<<<<<<CATEGORY CREATION>>>>>>");
        
        
        
        string categoryName = string.Empty;
        while (string.IsNullOrEmpty(categoryName))
        {
            Console.WriteLine("Enter the category name:");
            categoryName = Console.ReadLine() ?? string.Empty;
        }
        
        CategoryDTO categoryDTO = new CategoryDTO
        {
            CategoryName = categoryName
        };
        
        _categoryService.AddCategory(categoryDTO);
    }

    private NoteDTO CreateNoteFromView()
    {
        Console.WriteLine("<<<<<<NOTE CREATION>>>>>>");

        string title = string.Empty;
        while (string.IsNullOrEmpty(title))
        {
            Console.WriteLine("Enter the title of the note:");
            title = Console.ReadLine() ?? string.Empty;
        }

        Console.WriteLine("Enter the description of the note (press Enter to skip):");
        string descriptionInput = Console.ReadLine();


        string? description = string.IsNullOrEmpty(descriptionInput) ? null : descriptionInput;
        
        
        NoteDTO noteDTO = new NoteDTO
        {
            Title = title,
            Description = description
        };
        return noteDTO;
    }
    
    public void CreateAccountNoteFromView()
    {
        NoteDTO noteDTO = CreateNoteFromView();

        string login = string.Empty;
        while (string.IsNullOrEmpty(login))
        {
            Console.WriteLine("Enter the Login of the note:");
            login = Console.ReadLine() ?? string.Empty;
        }
        
        string password = string.Empty;
        while (string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Enter the Password of the note:");
            password = Console.ReadLine() ?? string.Empty;
        }
        

        
        AccountNoteDTO accountNoteDto = new AccountNoteDTO
        {
            Title = noteDTO.Title,
            Description = noteDTO.Description,
            AccountLogin = login,
            AccountPassword = password
        };

        _noteService.AddNote(accountNoteDto);
    }
    
    public void CreateTextNoteFromView()
    {
        NoteDTO noteDTO = CreateNoteFromView();

        List<string> contentList = new List<string>();
        Console.WriteLine("Enter the content of the note (leave empty to finish):");

        while (true)
        {
            string content = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(content))
                break; 

            contentList.Add(content);
        }

        TextNoteDTO textNoteDto = new TextNoteDTO
        {
            Title = noteDTO.Title,
            Description = noteDTO.Description,
            Content = contentList
        };

        _noteService.AddNote(textNoteDto);
    }

    public void CompleteNote(NoteModel note)
    {
        int? id = GetCategoryFromView()?.Id;

        if (id == null)
        {
            Console.WriteLine("Category not found");
            return;
        }
        
        note.SetCategory(id);
        _noteService.UpdateNote(note);
    }

    private CategoryModel? GetCategoryFromView()
    {
        List<CategoryModel> categories = _categoryService.GetCategories();
        
        if(categories.Count == 0)
            return null;
        
        Console.WriteLine("<<<<<<CATEGORIES>>>>>>");
        
        for(int i = 0; i < categories.Count; i++)
        {
            Console.WriteLine($"{i+1}. {categories[i].CategoryName}");
        }
        
        Console.WriteLine("Choose a category:");
        string input = Console.ReadLine() ?? string.Empty;

        if (int.TryParse(input, out var index))
        {
            if(index >= 1 && index <= categories.Count)
                return categories[index-1];
            
        }
        
        return null;
        
    }
    
    public NoteModel? GetNoteFromView()
    {
        
        List<NoteModel> notes = _noteService.GetAllNotes();
        
        if(notes.Count == 0)
            return null;
        
        Console.WriteLine("<<<<<<NOTES>>>>>>");

        
        for(int i = 0; i < notes.Count; i++)
        {
            Console.WriteLine($"{i+1}. {notes[i]}\n");
        }
        
        
        Console.WriteLine("Choose a note:");
        string input = Console.ReadLine() ?? string.Empty;

        if (int.TryParse(input, out var index))
        {
            if(index >= 1 && index <= notes.Count)
                return notes[index-1];
            
        }
        
        return null;
        
        
    }
    
    public void ViewNote(NoteModel note)
    {

        Console.WriteLine(note.ToStringFull());
        
    }


    
    
    
}