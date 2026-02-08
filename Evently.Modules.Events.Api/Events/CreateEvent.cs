using Evently.Modules.Events.Api.Database;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Api.Events;

public static class CreateEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        _ = app.MapPost("events", static async (Request request, EventsDbContext context) =>
        {
            Event @event = new()
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                StartsAtUtc = request.StartsAtUtc,
                EndsAtUtc = request.EndsAtUtc,
                Status = EventStatus.Draft
            };
            _ = context.Events.Add(@event);

            _ = await context.SaveChangesAsync();

            return Results.Ok(@event.Id);
        })
        .WithTags(Tags.Events);
    }

    internal sealed class Request
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Location { get; set; }
        public DateTime StartsAtUtc { get; set; }
        public DateTime? EndsAtUtc { get; set; }
    }
}