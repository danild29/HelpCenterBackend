using Microsoft.AspNetCore.Identity;

namespace HelpCenter.Api.Database;

public class User : IdentityUser
{
    public string? Initials { get; set; }
}
