using System.Text.Json.Serialization;

namespace Common.Models.Database;

public class MessageDto
{
    public Guid Id { get; set; }
    public DateTimeOffset Created { get; set; }

    public string Content { get; set; }


    public string CreatedAt => Created.DateTime.ToString();



    public UserDto Creator { get; set; }

    [JsonIgnore]
    public PostDto Post { get; set; }
}
