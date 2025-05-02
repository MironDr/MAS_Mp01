using MP01.Models;
using MP01.Services;
using MP01.Utilities;
using MP01.View;


//Register services
ServiceLocator.Register(new GroupService());
ServiceLocator.Register(new CategoryService());
ServiceLocator.Register(new NoteService());




Main();



static void Main()
{
    
    NotesView notesView = new NotesView();
    notesView.TestRestrictions();
    
   
/*
    while (true)
    {
        Console.WriteLine("Select an action:");
        Console.WriteLine("1 - Create a category");
        Console.WriteLine("2 - Create a text note");
        Console.WriteLine("3 - Select category");
        Console.WriteLine("4 - View a note");
        Console.WriteLine("5 - Create a group");
        Console.WriteLine("6 - Set group to note");
        Console.WriteLine("7 - View groups");
        Console.WriteLine("8 - Create a SourceNote");
        Console.WriteLine("9 - Connect Source And Text Note");
        Console.WriteLine("10 - View Notes With Source");
        Console.WriteLine("11 - Exit");
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
                noteModel = notesView.GetNoteFromView();
                if (noteModel != null)
                {
                    notesView.CompleteNote(noteModel);
                }
                break;

            case "4":
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

            case "5":
                notesView.CreateGroup();
                break;

            case "6":
                notesView.SetGroupToNote();
                break;

            case "7":
                notesView.ViewGroups();
                break;

            case "8":
                notesView.CreateSourceNoteFromView();
                break;

            case "9":
                notesView.SetSourceToNote();
                break;

            case "10":
                notesView.ViewNotesWithSource();
                break;

            case "11":
                Console.WriteLine("Exiting program...");
                return;

            default:
                Console.WriteLine("Invalid input. Please try again.");
                break;
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        
    }
    */
}
