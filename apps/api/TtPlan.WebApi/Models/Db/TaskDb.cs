using TtPlan.WebApi.Models.Enums;

namespace TtPlan.WebApi.Models.Db;

public record TaskDb
{
    public Guid Id { get; init; }
    public required Guid ProjectId { get; init; }
    public required Guid? MilestoneId { get; init; }
    public required string Name { get; init; }
    public required DateTime ScheduledAt { get; init; }
    public required int Estimate { get; init; }
    public Status Status { get; init; } = Status.NotStarted;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;
}