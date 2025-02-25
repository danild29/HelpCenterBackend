using System.Text.Json.Serialization;

namespace Common.Models.Database;

public class PostDto
{
    public Guid Id { get; set; }

    public DateTimeOffset Pusblished { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public UserDto Publisher { get; set; }

    public ICollection<MessageDto> Messages { get; set; }

    [JsonIgnore]
    public EventDto Event { get; set; }
}


    