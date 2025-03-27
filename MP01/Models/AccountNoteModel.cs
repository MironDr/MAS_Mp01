using System.Text;

namespace MP01.Models;

public class AccountNoteModel: NoteModel
{
    
    public string AccountLogin { get; set; }
    
    public string AccountPassword { get; set; }
    
    
    private static string NoteTypeString => "Account Note";


    public override string ToString()
    {
        return base.ToString()
               + $", NoteType: {NoteTypeString}";
    }


    public override string ToStringFull()
    {
        StringBuilder stringBuilder = new StringBuilder(base.ToStringFull());
        
        stringBuilder.Append($"NoteType: {NoteTypeString}\n");

        stringBuilder.Append($"Login: {AccountLogin}\n");

        stringBuilder.Append($"Password: {AccountPassword}\n");
        
        return stringBuilder.ToString();
    }
}
