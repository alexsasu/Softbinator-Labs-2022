using slabs_project.Models.Entities;
using slabs_project.Repositories.GenericRepository;

namespace slabs_project.Repositories
{
    public interface INGORepository : IGenericRepository<NGO>
    {
        Task<List<NGO>> GetAllNGOs();
    }
}
