namespace TtPlan.WebApi.Models.Db;

public record MilestoneDb
{
    public Guid Id { get; init; }
    public required Guid ProjectId { get; init; }
    public required string Name { get; init; }
    public required DateTime StartDate { get; init; }
    public required DateTime? EndDate { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;
}