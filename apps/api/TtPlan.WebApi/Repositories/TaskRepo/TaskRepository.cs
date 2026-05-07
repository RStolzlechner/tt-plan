using TtPlan.WebApi.Models.Db;

namespace TtPlan.WebApi.Repositories.TaskRepo;

public class TaskRepository : ITaskRepository
{
    public Task<IEnumerable<TaskDb>> GetAllInMilestone(Guid milestoneId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TaskDb>> GetAllInProject(Guid projectId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TaskDb>> GetAllScheduledAt(DateOnly date, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TaskDb>> GetAllScheduledStartToEnd(DateOnly startDate, DateOnly endDate, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> InsertAsync(TaskDb task)
    {
        throw new NotImplementedException();
    }

    public Task Update(TaskDb task, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid taskId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}