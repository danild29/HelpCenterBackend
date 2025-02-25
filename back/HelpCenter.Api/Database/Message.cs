using System.Text.Json.Serialization;

namespace HelpCenter.Api.Database;

public class Message
{
    public Guid Id { get; set; }
    public DateTimeOffset Created { get; set; }

    public string Content { get; set; }

    public User Creator { get; set; }

    [JsonIgnore]
    public Post Post { get; set; }
}
