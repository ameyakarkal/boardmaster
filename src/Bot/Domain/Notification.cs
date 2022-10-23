
using System.Text.Json.Serialization;

namespace Bot.Domain;

public class Notification
{    
    public Notification(
        string summary,
        string title)
    {
        Type = "MessageCard";
        Context = "https://schema.org/extensions";
        ThemeColor = "0078D7";
        Summary = summary;
        Title = title;
    }

    public string Summary { get; set; }
    public string Title { get; set; }

    #region defaults for teams
    [JsonPropertyName("@type")]
    public string Type { get; }
    
    [JsonPropertyName("@context")]
    public string Context { get; }

    public string ThemeColor { get; }
    #endregion
}
