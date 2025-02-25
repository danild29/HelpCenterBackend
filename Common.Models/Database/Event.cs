


namespace Common.Models.Database;

public enum EventStatus
{
    Unknown = 0,
    Waiting = 1,
    Complete = 2,
}

public class EventDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public EventStatus Status { get; set; }

    public DateTimeOffset StartDate { get; set; }

    public string Description { get; set; }

    public string Phone { get; set; }

    public UserDto Creator { get; set; }

    public string City { get; set; }

    public string Address { get; set; }

    public bool IsCreator { get; set; }

    public ICollection<UserEventDto> Paticipants { get; set; }

    public ICollection<PostDto> Posts { get; set; }
}

/*
 * event_id             integer  NOT NULL ,
  start_date           datetime  NULL ,
  description          varchar(20)  NULL ,
  organization_id      integer  NOT NULL ,
  phone                varchar(20)  NULL ,
  organization_name    varchar(20)  NULL ,
  address              char(18)  NULL 
*/


