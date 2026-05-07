namespace TtPlan.WebApi.Models.Db;

public record JournalDb
{
    public Guid Id { get; init; }
    public required Guid TaskId { get; init; }
    public required string Description { get; init; }
    public required double Hours { get; init; }
    public required DateTime DoneAt { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;
}