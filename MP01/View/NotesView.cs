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

    
    
    public NotesView()
    {
        _noteService = ServiceLocator.Get<NoteService>();
        _categoryService = ServiceLocator.Get<CategoryService>();
        _groupService = ServiceLocator.Get<GroupService>();
    }
    
    public CategoryModel CreateCategoryFromView()
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
        
        return _categoryService.AddCategory(categoryDTO);
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
    
    
    public TextNoteModel CreateTextNoteFromView()
    {
        NoteDTO noteDTO = CreateNoteFromView();

        TextNoteDTO textNoteDto = new TextNoteDTO
        {
            Title = noteDTO.Title,
            Description = noteDTO.Description,
        };
        
        TextNoteModel textNoteModel = (TextNoteModel)_noteService.AddNote(textNoteDto);

        
     
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

        return textNoteModel;
        
    }
    
    public SourceNoteModel CreateSourceNoteFromView()
    {
        NoteDTO noteDTO = CreateNoteFromView();

        Console.WriteLine("Enter the source type (Book, Website): ");
        string? typeInput = Console.ReadLine();
        ReferenceType type;
        while (!Enum.TryParse(typeInput, out type))
        {
            Console.WriteLine("Invalid type. Please enter a valid ReferenceType:");
            typeInput = Console.ReadLine();
        }

        Console.WriteLine("Enter the source (URL, file path, or other reference): ");
        string source = string.Empty;
        while (string.IsNullOrEmpty(source))
        {
            source = Console.ReadLine();
        }
     

        Console.WriteLine("Enter the author (leave blank if unknown): ");
        string? author = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(author))
            author = null;

        Console.WriteLine("Enter the published date (yyyy-MM-dd): ");
        DateTime publishedDate;
        while (!DateTime.TryParse(Console.ReadLine(), out publishedDate))
        {
            Console.WriteLine("Invalid date. Please use format yyyy-MM-dd:");
        }

        var sourceNoteDto = new SourceNoteDTO
        {
            Title = noteDTO.Title,
            Description = noteDTO.Description,
            Type = type,
            Source = source,
            Author = author,
            PublishedDate = publishedDate
        };

        
        
        return (SourceNoteModel)_noteService.AddNote(sourceNoteDto);
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

    

    public GroupModel CreateGroup()
    {
        Console.WriteLine("Set Group Name:");
        string groupName = Console.ReadLine()?.Trim() ?? string.Empty;

        GroupModel groupModel = GroupModel.CreateGroup(groupName);
        _groupService.AddGroup(groupModel);
        return groupModel;
    }

    public NoteModel? GetSpecialNoteFromView<T>() where T : NoteModel, new()
    {
        List<NoteModel> notes = _noteService.GetNotesByType<T>().Cast<NoteModel>().ToList();
        return ViewGetterNote(notes);
    }

    public NoteModel? GetNoteFromView()
    {
        
        List<NoteModel> notes = _noteService.GetAllNotes();
        return ViewGetterNote(notes);
    }

    private NoteModel? ViewGetterNote(List<NoteModel> notes)
    {
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
    
   

    public NoteWithSource? SetSourceToNote()
    {
        TextNoteModel? note = (TextNoteModel)GetSpecialNoteFromView<TextNoteModel>();
        SourceNoteModel? sourceNote = (SourceNoteModel)GetSpecialNoteFromView<SourceNoteModel>();
        
        if(sourceNote == null || note == null)
            return null;
        
        Console.WriteLine("Enter a quote from the source (or leave blank):");
        string? quote = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(quote)) quote = null;

        Console.WriteLine("Enter a comment (or leave blank):");
        string? comment = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(comment)) comment = null;

        Console.WriteLine("Enter the page number (or leave blank):");
        string? pageInput = Console.ReadLine();
        int? pageNumber = null;
        if (int.TryParse(pageInput, out int parsedPage))
            pageNumber = parsedPage;

        Console.WriteLine("Is this the primary source? (y/n):");
        bool isPrimary = Console.ReadLine()?.Trim().ToLower() == "y";

        NoteWithSourceDTO dto = new NoteWithSourceDTO
        {
            SourceNote = sourceNote,
            Note = note,
            Quote = quote,
            Comment = comment,
            PageNumber = pageNumber,
            IsPrimary = isPrimary
        };

        return NoteWithSource.Create(dto);
    }

    public void ViewNotesWithSource()
    {
        List<SourceNoteModel> sourceNoteModels = _noteService.GetNotesByType<SourceNoteModel>();
        
        
        foreach (SourceNoteModel sourceNote in sourceNoteModels)
        {
            foreach (NoteWithSource var in sourceNote.GetNotesLinks())
            {
                Console.WriteLine(var);
            }
        }
    }

    public  void TestAssociations()
    {
        Console.WriteLine("=== Testing Associations ===");
        /*
        // 1. Simple association: Note ↔ Category
        var category = CreateCategoryFromView();
        var note = new NoteModel { Title = "Work Note" };;

        // Link both sides
        note.Category = category;


        Console.WriteLine($"Category contains note: {category.GetNotes().Contains(note)}"); // true


        // Remove from Category side
        category.RemoveNote(note);
        Console.WriteLine($"After removing from category - category contains note: {category.GetNotes().Contains(note)}"); // false
        Console.WriteLine($"Note still references category: {note.Category != null}"); // false

        note.Category = category;
        
        // Now remove from Note side
        note.Category = null;
        Console.WriteLine($"After removing from note - note's category is null: {note.Category == null}, {category.GetNotes().Contains(note)}"); // true false

        
        //Add category to note from Category side
        category.AddNote(note);
        Console.WriteLine($"After removing from note - note's category is null: {note.Category == null}, {category.GetNotes().Contains(note)}"); // false true
        
        /*
        // 2. Composition: Note → TextBlock
        var textNote = CreateTextNoteFromView();

        Console.WriteLine($"Note has {textNote.GetTextCount()} text blocks"); 
        textNote.ClearTextBlocks();
        Console.WriteLine($"After clearing: note has {textNote.GetTextCount()} text blocks"); // 0
        */
        
        // 3. Association with attributes: NoteWithSource
        var textNote = new TextNoteModel{Title = "Text Note"};
        var sourceNote = new SourceNoteModel{Title = "Source Note"};

        NoteWithSourceDTO dto = new NoteWithSourceDTO{SourceNote = sourceNote, Note = textNote, Quote = "wee"};
        
        var link = NoteWithSource.Create(dto);

        Console.WriteLine($"Link connects: {link.Note.Title}, {link.SourceNote.Title}"); // Target ← Source
        Console.WriteLine($"Check sides: {sourceNote.GetNotesLinks().Contains(link)}, {link.Note.GetNotesLinks().Contains(link)}"); //true true
        
        // Remove by NoteWithSource
        link.Remove();
        Console.WriteLine($"After RemoveByNoteWithSource: Source is null? {link.SourceNote == null}"); // true
        Console.WriteLine($"After RemoveByNoteWithSource: Note is null? {link.Note == null}"); // true
        Console.WriteLine($"Check sides: {sourceNote.GetNotesLinks().Contains(link)}, {textNote.GetNotesLinks().Contains(link)}"); //false false
        
        
        link = NoteWithSource.Create(dto);
        
        // Remove by text note
        textNote.RemoveSourceLink(link);
        Console.WriteLine($"After RemoveByTextNote: Source is null? {link.SourceNote == null}"); // true
        Console.WriteLine($"After RemoveByTextNote: Note is null? {link.Note == null}"); // true
        Console.WriteLine($"Check sides: {sourceNote.GetNotesLinks().Contains(link)}, {textNote.GetNotesLinks().Contains(link)}"); //false false
        
        
        link = NoteWithSource.Create(dto);
        
        // Remove by source note
        sourceNote.RemoveNote(link);
        Console.WriteLine($"After RemoveBySourceNote: Source is null? {link.SourceNote == null}"); // true
        Console.WriteLine($"After RemoveBySourceNote: Note is null? {link.Note == null}"); // true
        Console.WriteLine($"Check sides: {sourceNote.GetNotesLinks().Contains(link)}, {textNote.GetNotesLinks().Contains(link)}"); //false false
         
        sourceNote.AddNote(link);
        Console.WriteLine($"After AddBySourceNote: Source is null? {link.SourceNote == null}"); // true
        Console.WriteLine($"After AddBySourceNote: Note is null? {link.Note == null}"); // true
        Console.WriteLine($"Check sides: {sourceNote.GetNotesLinks().Contains(link)}, {textNote.GetNotesLinks().Contains(link)}"); //false false
        textNote.AddSourceLink(link);
        Console.WriteLine($"After AddBySourceNote: Source is null? {link.SourceNote == null}"); // true
        Console.WriteLine($"After AddBySourceNote: Note is null? {link.Note == null}"); // true
        Console.WriteLine($"Check sides: {sourceNote.GetNotesLinks().Contains(link)}, {textNote.GetNotesLinks().Contains(link)}"); //false false
        
        /*
        // 4. Qualified association: Group ↔ Dictionary<Guid, NoteModel>
        var group = CreateGroup();
        var qualifiedNote = new NoteModel { Title = "Qualified Note" };

        qualifiedNote.Group = group;
        Console.WriteLine($"Group has note by ID: {group.GetNotes().ContainsKey(qualifiedNote.Id)}"); // true

        // Remove from note
        qualifiedNote.Group = null;
        Console.WriteLine($"After removal from group: Contains note? {group.GetNotes().ContainsKey(qualifiedNote.Id)}"); // false


        group.AddNoteToGroup(qualifiedNote);
        Console.WriteLine($"Group has note by ID: {group.GetNotes().ContainsKey(qualifiedNote.Id)}"); // true

        // Remove from group
        group.RemoveNote(qualifiedNote.Id);
        Console.WriteLine($"After removal from group: Contains note? {group.GetNotes().ContainsKey(qualifiedNote.Id)}"); // false

        */
        Console.WriteLine("=== Association Testing Complete ===");
    }
    
    
}