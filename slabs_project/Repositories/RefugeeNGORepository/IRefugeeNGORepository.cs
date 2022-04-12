using slabs_project.Models.Entities;
using slabs_project.Repositories.GenericRepository;

namespace slabs_project.Repositories
{
    public interface IRefugeeNGORepository : IGenericRepository<RefugeeNGO>
    {
        Task<RefugeeNGO> GetRefugeeNGOByIds(int refugeeId, int ngoId);
        Task<Refugee> GetRefugeeById(int id);
        Task<NGO> GetNgoById(int id);
        Task<List<RefugeeNGO>> GetRefugeesAssistedByNGOWithGivenId(int id);
        Task<List<RefugeeNGO>> GetNGOsAssistingRefugeeWithGivenId(int id);
        Task<List<RefugeeNGO>> GetAllRefugeeNGOs();
    }
}
