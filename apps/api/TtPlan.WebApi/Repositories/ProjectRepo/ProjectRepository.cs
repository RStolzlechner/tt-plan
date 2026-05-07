using TtPlan.WebApi.Models.Db;

namespace TtPlan.WebApi.Repositories.ProjectRepo;

public class ProjectRepository : IProjectRepository
{
    public Task<IEnumerable<ProjectDb>> GetAll(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> Insert(ProjectDb project, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task Update(ProjectDb project, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid projectId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}