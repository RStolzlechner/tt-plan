using TtPlan.WebApi.Models.Db;

namespace TtPlan.WebApi.Repositories.ProjectRepo;

public interface IProjectRepository
{
    Task<IEnumerable<ProjectDb>> GetAll(CancellationToken ct);
    Task<Guid> Insert(ProjectDb project, CancellationToken ct);
    Task Update(ProjectDb project, CancellationToken ct);
    Task Delete(Guid projectId, CancellationToken ct);
}