using TtPlan.WebApi.Models.Db;

namespace TtPlan.WebApi.Repositories.MilestoneRepo;

public class MilestoneRepository : IMilestoneRepository
{
    public Task<IEnumerable<MilestoneDb>> GetAll(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> Insert(MilestoneDb milestone, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task Update(MilestoneDb milestone, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid milestoneId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}