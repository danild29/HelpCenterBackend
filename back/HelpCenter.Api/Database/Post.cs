using System.Text.Json.Serialization;

namespace HelpCenter.Api.Database;

public class Post
{
    public Guid Id { get; set; }
    public DateTimeOffset Pusblished { get; set; }
    public string Content { get; set; }
    public string Title { get; set; }

    public User Publisher { get; set; }

    [JsonIgnore]
    public Event Event { get; set; }

    public ICollection<Message> Messages { get; set; }
}
