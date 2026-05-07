using TtPlan.WebApi.Models.Db;

namespace TtPlan.WebApi.Repositories.JournalRepo;

public class JournalRepository : IJournalRepository
{
    public Task<IEnumerable<JournalDb>> GetAllInMilestone(Guid milestoneId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<JournalDb>> GetAllInProject(Guid projectId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<JournalDb>> GetAllInTask(Guid taskId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<JournalDb>> GetAllDoneAt(DateOnly date, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> Insert(JournalDb journal, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task Update(JournalDb journal, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid journalId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}