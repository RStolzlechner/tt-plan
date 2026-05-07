using TtPlan.WebApi.Models.Db;

namespace TtPlan.WebApi.Repositories.TaskRepo;

public interface ITaskRepository
{
    Task<IEnumerable<TaskDb>> GetAllInMilestone(Guid milestoneId, CancellationToken ct);
    Task<IEnumerable<TaskDb>> GetAllInProject(Guid projectId, CancellationToken ct);
    Task<IEnumerable<TaskDb>> GetAllScheduledAt(DateOnly date, CancellationToken ct);
    Task<IEnumerable<TaskDb>> GetAllScheduledStartToEnd(DateOnly startDate, DateOnly endDate, CancellationToken ct);
    
    Task<Guid> InsertAsync(TaskDb task);
    Task Update(TaskDb task, CancellationToken ct);
    Task Delete(Guid taskId, CancellationToken ct);
}