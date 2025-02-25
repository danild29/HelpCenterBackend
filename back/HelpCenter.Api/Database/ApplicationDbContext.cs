using System.Reflection.Emit;
using System.Xml;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HelpCenter.Api.Database;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public virtual DbSet<Event> Events { get; set; }
    public virtual DbSet<UserEvent> Participants { get; set; }
    public virtual DbSet<Post> Posts { get; set; }
    public virtual DbSet<Message> Messages { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>().Property(u => u.Initials).HasMaxLength(5);
        builder.Entity<Event>().Ignore(c => c.IsCreator);

        builder.HasDefaultSchema("identity");
    }
}
