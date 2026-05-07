using TtPlan.WebApi.Models.Db;

namespace TtPlan.WebApi.Repositories.JournalRepo;

public interface IJournalRepository
{
    Task<IEnumerable<JournalDb>> GetAllInMilestone(Guid milestoneId, CancellationToken ct);
    Task<IEnumerable<JournalDb>> GetAllInProject(Guid projectId, CancellationToken ct);
    Task<IEnumerable<JournalDb>> GetAllInTask(Guid taskId, CancellationToken ct);
    Task<IEnumerable<JournalDb>> GetAllDoneAt(DateOnly date, CancellationToken ct);
    
    Task<Guid> Insert(JournalDb journal, CancellationToken ct);
    Task Update(JournalDb journal, CancellationToken ct);
    Task Delete(Guid journalId, CancellationToken ct);
}