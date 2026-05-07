using TtPlan.WebApi.Models.Db;

namespace TtPlan.WebApi.Repositories.MilestoneRepo;

public interface IMilestoneRepository
{
    Task<IEnumerable<MilestoneDb>> GetAll(CancellationToken ct);
    Task<Guid> Insert(MilestoneDb milestone, CancellationToken ct);
    Task Update(MilestoneDb milestone, CancellationToken ct);
    Task Delete(Guid milestoneId, CancellationToken ct);
}