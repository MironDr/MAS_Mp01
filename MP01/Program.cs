using MP01.Context;
using MP01.Models;
using MP01.Services;
using MP01.Utilities;
using MP01.View;


var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "dbmp01.sqlite");

//Register NotesTypeManager
ServiceLocator.Register(new NotesTypeManager());

//Register DB
ServiceLocator.Register(new AppDbContext(dbPath));

//Register services
ServiceLocator.Register(new NoteService());
ServiceLocator.Register(new CategoryService());



Main();



static void Main()
{
        NotesView notesView = new NotesView();


        while (true)
        {
            Console.WriteLine("Select an action:");
            Console.WriteLine("1 - Create a category");
            Console.WriteLine("2 - Create a note");
            Console.WriteLine("3 - Select category");
            Console.WriteLine("4 - View a note");
            Console.WriteLine("5 - Exit");
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
