using System.Text;
using System.Text.Json;
using MP01.DTOs;
using SQLite;

namespace MP01.Models;

public class TextNoteModel : NoteModel
{
    private static string NoteTypeString = "Text Note";

    
    
    //Asocjacje z atrybutem
    private List<NoteWithSource> Sources = new();

    public void AddSourceLink(NoteWithSource ns)
    {
        if(Sources.Contains(ns))
            return;
        
        if (ns.Note == this)
        {
            Sources.Add(ns);
        }
    }

    public void RemoveSourceLink(NoteWithSource ns)
    {
        if(!Sources.Contains(ns))   
            return;
        
        Sources.Remove(ns);
        
        if (ns.Note == this)
        {
            ns.Remove();
        }

    }

    public List<SourceNoteModel> GetSourceLinks()
    {
        return Sources.Select(t => t.SourceNote).ToList();
    }
    
    
    public List<NoteWithSource> GetNotesLinks()
    {
        return Sources.ToList();
    }
    //
    

    //Asocjacje  Kompozycja 
    
    private List<TextBlock> TextBlocks = new();
   

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

    public int GetTextCount()
    {
        return TextBlocks.Count;
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
    //
    
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
}