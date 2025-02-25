namespace Common.Models.Database;

public class UserEventDto
{
    public Guid Id { get; set; }
    public DateTimeOffset SignInDate { get; set; }
    public UserDto Participant { get; set; }
    public EventDto Event { get; set; }
}
