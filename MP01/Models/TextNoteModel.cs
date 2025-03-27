using System.Text;
using System.Text.Json;
using MP01.DTOs;
using SQLite;

namespace MP01.Models;

public class TextNoteModel: NoteModel
{
    public string? ContentJson { get; set; }

    private static string NoteTypeString => "Text Note";
    
    [Ignore] 
    public List<string>? Content
    {
        get => string.IsNullOrEmpty(ContentJson) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(ContentJson);
        set => ContentJson = JsonSerializer.Serialize(value);
    }

    public override string ToString()
    {
        return base.ToString()
            + $", NoteType: {NoteTypeString}";
    }

    public override string ToStringFull()
    {
        StringBuilder stringBuilder = new StringBuilder(base.ToStringFull());
        
        stringBuilder.Append($"NoteType: {NoteTypeString}\n");

        if (Content.Count > 0)
        {

            stringBuilder.Append("<<<<<<Text Blocks>>>>>>\n");

            foreach (string str in Content)
            {
                stringBuilder.Append("---------------------\n");
                stringBuilder.Append($"{str}\n");
            }
        }


        return stringBuilder.ToString();
    }
}