using System.Text;
using System.Text.Json;
using MP01.DTOs;
using SQLite;

namespace MP01.Models;

public class TextNoteModel : NoteModel
{
    public string? TextBlocksJson { get; set; }

    private static string NoteTypeString => "Text Note";


    [Ignore]
    private List<TextBlock> TextBlocks
    {
        get => string.IsNullOrEmpty(TextBlocksJson)
            ? new List<TextBlock>()
            : JsonSerializer.Deserialize<List<TextBlock>>(TextBlocksJson);
        set => TextBlocksJson = JsonSerializer.Serialize(value);
    }


    public void AddTextBlock(string title, string content)
    {
        var list = TextBlocks;
        list.Add(new TextBlock
        {
            Title = title,
            Content = content
        });
        TextBlocks = list;
    }

    
    public void RemoveTextBlockAt(int index)
    {
        var list = TextBlocks;
        if (index >= 0 && index < list.Count)
        {
            list.RemoveAt(index);
            TextBlocks = list;
        }
    }


    public void ClearTextBlocks()
    {
        TextBlocks = new List<TextBlock>();
    }

    public override string ToString()
    {
        return base.ToString() + $", NoteType: {NoteTypeString}";
    }

    public override string ToStringFull()
    {
        StringBuilder stringBuilder = new StringBuilder(base.ToStringFull());
        stringBuilder.Append($"NoteType: {NoteTypeString}\n");
        
        
        if (TextBlocks.Count > 0)
        {
            stringBuilder.Append("<<<<<<Text Blocks>>>>>>\n");
            foreach (TextBlock block in TextBlocks)
            {
                stringBuilder.Append("---------------------\n");
                stringBuilder.Append(block);
                stringBuilder.AppendLine();
            }
        }

        return stringBuilder.ToString();
    }


    private class TextBlock : BaseModel
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(Title))
                sb.AppendLine($"Title: {Title}");
        
            if (!string.IsNullOrWhiteSpace(Content))
                sb.AppendLine($"Content: {Content}");

            return sb.ToString().Trim();
        }
    }
}