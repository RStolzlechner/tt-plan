namespace TtPlan.WebApi.Models.Enums;

public enum Status
{
    NotStarted,
    InProgress,
    Completed
}

public static class StatusExtensions
{
    public static string ToDbString(this Status status) => status switch
    {
        Status.NotStarted => "not_started",
        Status.InProgress => "in_progress",
        Status.Completed => "completed",
        _ => throw new ArgumentOutOfRangeException(nameof(status))
    };

    public static Status FromDbString(string value) => value switch
    {
        "not_started" => Status.NotStarted,
        "in_progress" => Status.InProgress,
        "completed" => Status.Completed,
        _ => throw new ArgumentOutOfRangeException(nameof(value))
    };
}