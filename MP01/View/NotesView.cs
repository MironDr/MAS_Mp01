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
    private readonly GroupService _groupService;
    private readonly TagService _tagService;
    
    
    public NotesView()
    {
        _noteService = ServiceLocator.Get<NoteService>();
        _categoryService = ServiceLocator.Get<CategoryService>();
        _groupService = ServiceLocator.Get<GroupService>();
        _tagService = ServiceLocator.Get<TagService>();
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

        TextNoteDTO textNoteDto = new TextNoteDTO
        {
            Title = noteDTO.Title,
            Description = noteDTO.Description,
        };
        
        TextNoteModel textNoteModel = (TextNoteModel)_noteService.AddNote(textNoteDto);

        Console.WriteLine("Enter the content of the note: ");

     
        while (true)
        {
            Console.Write("Enter title for the block (leave empty to stop): ");
            string title = Console.ReadLine()?.Trim() ?? string.Empty;


            if (string.IsNullOrEmpty(title))
                break;

            Console.Write("Enter content for the block: ");
            string content = Console.ReadLine()?.Trim() ?? string.Empty;

         
            textNoteModel.AddTextBlock(title, content);
        }

        Console.WriteLine("Text note has been created successfully!");

        _noteService.UpdateNote(textNoteModel);

        
    }

    public void CompleteNote(NoteModel note)
    {
        CategoryModel? categoryModel = GetCategoryFromView();

        if (categoryModel == null)
        {
            Console.WriteLine("Category not found");
            return;
        }
        
        note.Category = categoryModel;
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

    

    public void CreateGroup()
    {
        Console.WriteLine("Set Group Name:");
        string groupName = Console.ReadLine()?.Trim() ?? string.Empty;
        
        
        _groupService.AddGroup(GroupModel.CreateGroup(groupName));
        
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

    public void ViewGroups()
    {
        Console.WriteLine("<<<<<<Groups>>>>>>>");
       
        List<GroupModel> groups = _groupService.GetGroups();
        
        foreach (GroupModel groupModel in groups)
        {
            Console.WriteLine("\n"+groupModel);
        }
        

       
    }

    private GroupModel? ChooseGroup()
    {
        Console.WriteLine("<<<<<<Groups>>>>>>>");
        
        List<GroupModel> groups = _groupService.GetGroups();
        
        for(int i = 0; i < groups.Count; i++)
        {
            Console.WriteLine($"{i+1}. {groups[i].GroupName}\n");
        }
        
        Console.WriteLine("Choose a note:");
        string input = Console.ReadLine() ?? string.Empty;

        if (int.TryParse(input, out var index))
        {
            if(index >= 1 && index <= groups.Count)
                return groups[index-1];
            
        }
        
        return null;
    }

    public void SetGroupToNote()
    {
        GroupModel? group = ChooseGroup();
        
        if(group == null)
            return;

        NoteModel? noteModel = GetNoteFromView();
        
        if(noteModel == null)
            return;
        
        
        group.AddNoteToGroup(noteModel);
        
        _noteService.UpdateNote(noteModel);
    }
    
    public TagModel CreateTag()
    {
        Console.Write("Enter tag name: ");
        string? tagName = Console.ReadLine();

        List<TagModel> tags =  _tagService.GetTags();

        TagModel? tagModel = tags.Find(t => t.TagName == tagName);
        
        if (tagModel != null)
        {
            return tagModel;
        }

        tagModel = TagModel.CreateTag(tagName);
        _tagService.AddTag(tagModel);
        Console.WriteLine($"Tag '{tagName}' created with ID {tagModel.Id}.");
        

        return tagModel;
    }

    public void TagNote()
    {
        NoteModel? note = GetNoteFromView();
        if (note == null)
        {
            Console.WriteLine("Note not found.");
            return;
        }

        TagModel tagModel = CreateTag();
        TaggedNotes.Create(tagModel , note);        
        Console.WriteLine($"Note '{note.Title}' tagged with '{tagModel.TagName}'.");
    }

    public void ViewTagsAndNotes()
    {
        List<TagModel> tags = _tagService.GetTags();
        
        Console.WriteLine(tags.Count);
        
        foreach (TagModel tag in tags)
        {
            foreach (TaggedNotes taggedNotes in tag._taggedNotes)
            {
                Console.WriteLine($"Note: {taggedNotes.Note.Title}, Tag: {taggedNotes.Tag.TagName}");
            }
        }
    }
    
    
}