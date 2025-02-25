using Common.Models.Database;

namespace Common.Models.Request;

public record EventCreateRequest: BaseRequest
{
    public string Title { get; set; }

    public DateTimeOffset StartDate { get; set; }

    public string Description { get; set; }

    public string Phone { get; set; }

    public string City { get; set; }

    public string Address { get; set; }
}
