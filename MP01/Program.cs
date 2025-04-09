using MP01.Context;
using MP01.Models;
using MP01.Services;
using MP01.Utilities;
using MP01.View;


var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "mp01.sqlite");

//Register NotesTypeManager
ServiceLocator.Register(new NotesTypeManager());

//Register DB
ServiceLocator.Register(new AppDbContext(dbPath));

//Register services
ServiceLocator.Register(new GroupService());
ServiceLocator.Register(new CategoryService());
ServiceLocator.Register(new TagService());
ServiceLocator.Register(new NoteService());




Main();



static void Main()
{
    NotesView notesView = new NotesView();

    while (true)
    {
        Console.WriteLine("Select an action:");
        Console.WriteLine("1 - Create a category");
        Console.WriteLine("2 - Create a text note");
        Console.WriteLine("3 - Create an account note");
        Console.WriteLine("4 - Select category");
        Console.WriteLine("5 - View a note");
        Console.WriteLine("6 - Create a group");
        Console.WriteLine("7 - Set group to note");
        Console.WriteLine("8 - View groups");
        Console.WriteLine("9 - Create a tag");
        Console.WriteLine("10 - Tag a note");
        Console.WriteLine("11 - View tags and their notes");
        Console.WriteLine("12 - Exit");
        Console.Write("\nYour choice: ");

        string? choice = Console.ReadLine();
        NoteModel? noteModel;

        switch (choice)
        {
            case "1":
                notesView.CreateCategoryFromView();
                break;

            case "2":
                notesView.CreateTextNoteFromView();
                break;

            case "3":
                notesView.CreateAccountNoteFromView();
                break;

            case "4":
                noteModel = notesView.GetNoteFromView();
                if (noteModel != null)
                {
                    notesView.CompleteNote(noteModel);
                }
                break;

            case "5":
                noteModel = notesView.GetNoteFromView();
                if (noteModel != null)
                {
                    notesView.ViewNote(noteModel);
                }
                else
                {
                    Console.WriteLine("Error: Note not found.");
                }
                break;

            case "6":
                notesView.CreateGroup();
                break;

            case "7":
                notesView.SetGroupToNote();
                break;

            case "8":
                notesView.ViewGroups();
                break;

            case "9":
                notesView.CreateTag();
                break;

            case "10":
                notesView.TagNote();
                break;

            case "11":
                notesView.ViewTagsAndNotes();
                break;

            case "12":
                Console.WriteLine("Exiting program...");
                return;

            default:
                Console.WriteLine("Invalid input. Please try again.");
                break;
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}
