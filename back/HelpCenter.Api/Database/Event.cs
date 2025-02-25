


using System.ComponentModel.DataAnnotations.Schema;
using Common.Models.Database;

namespace HelpCenter.Api.Database;

public class Event
{
    public Guid Id { get; set; }

    public DateTimeOffset StartDate { get; set; }

    public string Title { get; set; }

    public EventStatus Status { get; set; }

    public string Description { get; set; }

    public string Phone { get; set; }

    public User Creator { get; set; }

    public string City { get; set; }

    public string Address { get; set; }

    public bool IsCreator { get; set; }

    public ICollection<UserEvent> Paticipants { get; set; }
    public ICollection<Post> Posts { get; set; }
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


