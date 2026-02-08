namespace Evently.Modules.Events.Api.Events;

#pragma warning disable CA1716
public sealed class Event
#pragma warning restore CA1716
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Location { get; set; }
    public DateTime StartsAtUtc { get; set; }
    public DateTime? EndsAtUtc { get; set; }
    public EventStatus Status { get; set; }
}
