using System.Text.Json.Serialization;

namespace HelpCenter.Api.Database;


public class UserEvent
{
    public Guid Id { get; set; }
    public DateTimeOffset SignInDate { get; set; }
    public User Participant { get; set; }

    [JsonIgnore]
    public Event Event { get; set; }
}
