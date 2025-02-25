using Common.Models.Database;
using Common.Models.Request;
using HelpCenter.Api.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HelpCenter.Api.EndPoints;

public static class ConfigureEndpoints
{
    public static IEndpointRouteBuilder AddEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("users/me", async (ClaimsPrincipal user, ApplicationDbContext context) =>
        {
            string userId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return await context.Users.FindAsync(userId);
        })
        .RequireAuthorization();

        app.MapPost("event", async ([FromBody] EventCreateRequest data, ClaimsPrincipal user, ApplicationDbContext context) =>
        {
            string userId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            User creator = await context.Users.FindAsync(userId);

            var e = new Event
            {
                Id = Guid.NewGuid(),
                Title = data.Title,
                Status = Common.Models.Database.EventStatus.Unknown,
                Address = data.Address,
                City = data.City,
                Description = data.Description,
                Creator = creator!,
                Phone = data.Phone,
                StartDate = data.StartDate,
            };

            context.Events.Add(e);

            await context.SaveChangesAsync();
            return e;
        })
        .RequireAuthorization();

        app.MapPost("event/signIn", async ([FromBody] SignInRequest data, ClaimsPrincipal user, ApplicationDbContext context) =>
        {
            string userId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            User u = await context.Users.FindAsync(userId);
            Event e = await context.Events.FindAsync(data.eventId);

            var ue = new UserEvent
            {
                Id = Guid.NewGuid(),
                Event = e!,
                Participant = u!,
                SignInDate = DateTimeOffset.Now,
            };

            context.Participants.Add(ue);

            await context.SaveChangesAsync();
           
            return ue;
        })
        .RequireAuthorization();


        app.MapGet("event", async (ApplicationDbContext context) =>
        {
            return await context.Events.ToArrayAsync();
        })
        //.RequireAuthorization()
        ;

        app.MapGet("event/{eventId}", async (Guid eventId, ClaimsPrincipal user, ApplicationDbContext context) =>
        {
            Event e = await context.Events
                .Include(x => x.Creator)
                .Include(x => x.Paticipants)
                .ThenInclude(x => x.Participant)
                .Include(x => x.Posts)
                .ThenInclude(x => x.Messages)
                .OrderBy(x => x.StartDate)
                .FirstAsync(x => x.Id == eventId);


            foreach (Post post in e.Posts)
            {
                post.Messages = [.. post.Messages.OrderBy(x => x.Created)];
            }

            string userId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            e.IsCreator = e.Creator.Id == userId;
            return e;
        })
        .RequireAuthorization();


        app.MapPost("post", async ([FromBody] CreatePostRequest data, ClaimsPrincipal user, ApplicationDbContext context) =>
        {
            string userId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            User u = await context.Users.FindAsync(userId);
            Event e = await context.Events.FindAsync(data.eventId);

            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = data.Title,
                Event = e!,
                Content = data.Content,
                Publisher = u!,
                Pusblished = DateTimeOffset.Now,
            };

            context.Posts.Add(post);

            await context.SaveChangesAsync();

            return post;
        })
        .RequireAuthorization();


        app.MapPost("message", async ([FromBody] CreateMessageRequest data, ClaimsPrincipal user, ApplicationDbContext context) =>
        {
            string userId = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            User u = await context.Users.FindAsync(userId);
            Post p = await context.Posts.FindAsync(data.postId);

            var ue = new Message
            {
                Id = Guid.NewGuid(),
                Created = DateTimeOffset.Now,
                Content = data.Content,
                Creator = u!,
                Post = p!,
            };

            context.Messages.Add(ue);

            await context.SaveChangesAsync();

            return ue;
        })
        .RequireAuthorization();

        return app;
    }
}
